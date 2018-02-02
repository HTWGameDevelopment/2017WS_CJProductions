using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SongAnalyzer : MonoBehaviour
{
    public AudioSource audioSource;

    // Vars to be set/adjusted in the according scene
    public int numSamples = 1024; // Number of Samples to acquire per step
    public float sensitivity = 200f; // Sensitivity of the beat detection
    public int songTempo = 100; // Estimated Aberage BPM of the song

    private int samplingRate = 44100;
    private int numSubbands = 128;

    private int framesSinceLastBeat = 0;

    private int maxValues = 120;
    float[] curSpectrum;
    float[] averages;
    float[] scores;
    float[] oldSpectrum; // the oldSpectrum of the previous step

    // current score index
    int currentScoreIndex = 0;

    [Header("Events")]
    public OnBeatEventHandler onBeat;

    // Use this for initialization
    void Start()
    {
        audioSource.Play();

        scores = new float[maxValues];
        curSpectrum = new float[numSamples];
        averages = new float[numSubbands];

        samplingRate = audioSource.clip.frequency;

        //initialize record of previous curSpectrum
        oldSpectrum = new float[numSubbands];

        for ( int i = 0; i < numSubbands; ++i )
        {
            oldSpectrum[i] = 100.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( audioSource.isPlaying )
        {
            audioSource.GetSpectrumData( curSpectrum, 0, FFTWindow.BlackmanHarris );
            computeAverages( curSpectrum );

            // calculate the current Energy of our potential beat
            float curEnergy = 0;

            for ( int i = 0; i < numSubbands; i++ )
            {
                float curDezibel = (float)System.Math.Max( -100.0f, 20.0f * (float)System.Math.Log10( averages[i] ) + 160 ); // dB value of current band
                    curDezibel *= 0.025f;
                float dezibelIncrement = curDezibel - oldSpectrum[i]; // dB increment since last frame

                oldSpectrum[i] = curDezibel; 
                curEnergy += dezibelIncrement; // curEnergy is the sum of all dB increments
            }

            float scoreMax = -999999;
            int scoreMaxIndex = 0;

            // consider all preceding beats from 0.5 to 2.0 x current songTempo
            for ( int i = songTempo / 2; i < System.Math.Min( maxValues, 2 * songTempo ); ++i )
            {
                // calculate the currentScore via a comparison of the current (potential) beat energy to the previous scores
                float currentScore = curEnergy + scores[( currentScoreIndex - i + maxValues ) % maxValues] - sensitivity * (float)System.Math.Pow( System.Math.Log( (float)i / (float)songTempo ), 2 );
                
                // keep track of the best-scoring predecesor
                if ( currentScore > scoreMax )
                {
                    scoreMax = currentScore;
                    scoreMaxIndex = i;
                }
            }

            // keep the smallest value in the currentScore fn window as zero, by subtracing the min val
            scores[currentScoreIndex] = scoreMax;

            float scoreMin = scores[0];

            for ( int i = 0; i < maxValues; ++i )
            {
                if ( scores[i] < scoreMin )
                {
                    scoreMin = scores[i];
                }
            }

            for ( int i = 0; i < maxValues; ++i )
            {
                scores[i] -= scoreMin;
            }

            // find the largest score in our current sampling window
            scoreMax = scores[0];
            scoreMaxIndex = 0;

            for ( int i = 0; i < maxValues; ++i )
            {
                if ( scores[i] > scoreMax )
                {
                    scoreMax = scores[i];
                    scoreMaxIndex = i;
                }
            }

            ++framesSinceLastBeat;

            // if current value is the largest in the array, we probably have a beat
            if ( scoreMaxIndex == currentScoreIndex )
            {
                // make sure the most recent beat wasn't too recently
                if ( framesSinceLastBeat > songTempo / 4 )
                {
                    onBeat.Invoke();

                    // reset counter of frames since last beat
                    framesSinceLastBeat = 0;
                }
            }

            // Update column index 
            if ( ++currentScoreIndex == maxValues )
            {
                currentScoreIndex = 0;
            }
        }
    }

    public float getBandWidth()
    {
        return ( 2f / (float)numSamples ) * ( samplingRate / 2f );
    }

    public int freqencyToIndex( int freqency )
    {
        // freqency is lower than the bandwidth of curSpectrum[0]
        if ( freqency < getBandWidth() / 2 )
        {
            return 0;
        }

        // freqency is within the bandwidth of curSpectrum[512]
        if ( freqency > samplingRate / 2 - getBandWidth() / 2 )
        {
            return ( numSamples / 2 );
        }

        // all other cases
        float fraction = (float)freqency / (float)samplingRate;
        int i = (int)System.Math.Round( numSamples * fraction );

        return i;
    }

    public void computeAverages( float[] data )
    {
        for ( int i = 0; i < 12; i++ )
        {
            float avg = 0;
            int lowFrequency;

            if ( i == 0 )
            {
                lowFrequency = 0;
            }
            else
            {
                lowFrequency = (int)( ( samplingRate / 2 ) / (float)System.Math.Pow( 2, 12 - i ) );
            }

            int highFreq = (int)( ( samplingRate / 2 ) / (float)System.Math.Pow( 2, 11 - i ) );
            int lowerBound = freqencyToIndex( lowFrequency );
            int higherBound = freqencyToIndex( highFreq );

            for (int j = lowerBound; j <= higherBound; j++)
            {
                avg += data[j];
            }

            avg /= ( higherBound - lowerBound + 1 );
            averages[i] = avg;
        }
    }

    [System.Serializable]
    public class OnBeatEventHandler : UnityEngine.Events.UnityEvent
    {

    }
}
