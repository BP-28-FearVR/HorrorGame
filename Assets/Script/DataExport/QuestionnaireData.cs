using System.IO;
using System.Text;
using UnityEngine.SceneManagement;

public class QuestionnaireData
{

    //Reference to the Questionlist.
    private string[] _questionList = null;

    //Where the data is acctually saved.
    private int[] _data = null;

    // Indexer Getter for internal data.
    // If index is out of range, the temps array will throw the exception.
    public int this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }

    // Contructor for Data. Initialize with -1
    public QuestionnaireData(string[] questionList)
    {
        _questionList = questionList;
        _data = new int[questionList.Length];

        for (int i = 0; i < _data.Length; i++)
        {
            _data[i] = -1;
        }
    }

    // Converts a generic string to a CSV escaped string
    public static string StringToCSVCell(string str)
    {
        bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
        if (mustQuote)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char nextChar in str)
            {
                sb.Append(nextChar);
                if (nextChar == '"')
                    sb.Append("\"");
            }
            sb.Append("\"");
            return sb.ToString();
        }

        return str;
    }

    // Look up table for MapAnswer
    private static readonly string[] _answerLookUp =
        {
            "N/A",
            "Strongly agree",
            "Agree",
            "Neutral",
            "Disagree",
            "Strongly disagree"
        };

    //Uses LookupTable to resolve numbers to Answer String
    private string MapAnswer(int answer)
    {
        if (answer < -1 || answer > 4)
        {
            return answer.ToString();
        }

        int answerIndex = answer + 1;
        
        return _answerLookUp[answerIndex];
    }

    // Generate the CSV Content based on _data.
    private string DataAsCSVString()
    {
        const string seperator = ";";
        

        StringBuilder sb = new StringBuilder();

        sb
            .Append(StringToCSVCell("Question"))
            .Append(seperator)
            .Append(StringToCSVCell("Answer Text"))
            .Append(seperator)
            .Append(StringToCSVCell("Answer Number"))
            .Append("\n");

        for (int i = 0; i < _data.Length; i++)
        {
            sb
                .Append(StringToCSVCell(_questionList[i]))
                .Append(seperator)
                .Append(StringToCSVCell(MapAnswer(_data[i])))
                .Append(seperator)
                .Append(_data[i])
                .Append("\n");
        }

        return sb.ToString();
    }

    //Saves _data to questionnaire_[Scene name]_[ParticipantId].csv the Documents Folder
    public void SaveToFile() {
#if UNITY_EDITOR
        string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
#elif UNITY_ANDROID
        string basePath = "/sdcard/Documents/";
#else
        string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
#endif

        Scene currentScene = SceneManager.GetActiveScene();
        string participantId = ParticipantIDHandler.ParticipantID;
        
        string filename = (new StringBuilder())
            .Append("questionnaire_")
            .Append(currentScene.name)
            .Append("_")
            .Append(participantId)
            .Append(".csv")
            .ToString();

        string content = DataAsCSVString();

        StreamWriter streamWriter = new StreamWriter(Path.Combine(basePath, filename), false);
        streamWriter.Write(content);
        streamWriter.Close();
    }
}
