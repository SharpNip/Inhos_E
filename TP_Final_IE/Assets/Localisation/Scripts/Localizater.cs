using UnityEngine;
using UnityEditor;
using System.Collections;

public class Localizater 
    : MonoBehaviour 
{

	public string language;
	public string country;

    private const string SAVE_PATH = "Assets/Localisation/Data/data.asset";
	private static SavedData savedData;

    //Will print an examepl of use of the localization system.
	void Awake()
	{
		savedData = AssetDatabase.LoadAssetAtPath<SavedData>(SAVE_PATH);
		
		if (savedData == null) {
			savedData = ScriptableObject.CreateInstance<SavedData> ();
			AssetDatabase.CreateAsset (savedData, SAVE_PATH);
		}

		Debug.Log ("Untranslated word: Meow [Invalid]");
		Debug.Log ("Attempt at translating unrecognized word: " + IDToWord("Meow"));

		Debug.Log ("Untranslated word: Banane [Valid]");
		Debug.Log ("Attempt at translating unrecognized word: " + IDToWord("Banane"));
		
		Debug.Log ("Untranslated word: Fraise [Fallback]");
		Debug.Log ("Attempt at translating unrecognized word: " + IDToWord("Fraise"));
	}
	
	//Fetch translation based on Language and Country for the specific ID
	public string IDToWord(string ID)
	{
		//If the word wasn't found, some people might
		int theIndex = -1;
		string theTranslation = "UNHANDLED TRANSLATION";

		//If the ID was Legit, ignoring language/country restrictions
		theIndex = savedData.savedIDs.FindIndex(x => x == ID);
		if (theIndex != -1) {
			//If the language was valid
			Language zeLang = savedData.savedLanguages.Find (x => x.mName == language);
			if (zeLang != null) {
				//If the Country for that language was invalid or if the country for that language was valid
				// but the translation wasn't enabled, fallback to default country.
				Country zeCountry = zeLang.countries.Find (x => x.mName == country);
				if (zeCountry == null || !zeCountry.entries[theIndex].mEnabled)
				{
					zeCountry = zeLang.countries.Find (x => x.mName == "DEFAULT");
				}
				//If the final found ID is enabled, return the translation
				if (zeCountry.entries[theIndex].mEnabled)
				{
					theTranslation = zeCountry.entries[theIndex].mTranslation;
				}
			}
		}
		return theTranslation;
	}
}
