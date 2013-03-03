using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Layout;

namespace MefAddIns
{
    /// <summary>
    /// This is just a simple object that is itereated over and
    /// any "patterns" are looked for in the currently searched word document
    /// </summary>
    public class Advice
    {
        public string sPattern;
        public string sAdvice;
        public bool bOverUsedPhrase;
        public Advice()
        {
            sPattern = "test";
            sAdvice = "test";
            bOverUsedPhrase = false;

        }
		public Advice (string pattern, string advice, bool overused)
		{
			sPattern = pattern;
			sAdvice = advice;
			bOverUsedPhrase = overused;
		}
		public static List<Advice> ConvertTableToList (ref string version)
		{
			List<Advice> result = new List<Advice>();
			List<string> patterns = LayoutDetails.Instance.TableLayout.GetListOfStringsFromSystemTable (LayoutDetails.SYSTEM_GRAMMAR, 1);
			List<string> advice = LayoutDetails.Instance.TableLayout.GetListOfStringsFromSystemTable (LayoutDetails.SYSTEM_GRAMMAR, 2);
			List<string> overused = LayoutDetails.Instance.TableLayout.GetListOfStringsFromSystemTable (LayoutDetails.SYSTEM_GRAMMAR, 3);
			if (patterns.Count != advice.Count || patterns.Count != overused.Count) {
				throw new Exception ("grammar table is not equal");
			}
			for (int i = 0; i < patterns.Count; i++) {

				if (i == 0)
				{
					// this is the version Row
					version =  patterns[i].ToString();
				}
				else
				{
				bool IsOverUsed = false;
				if (overused[i] == "1")
				{
					IsOverUsed = true;
				}
				result.Add (new Advice(patterns[i], advice[i], IsOverUsed));
				}
			}
			return result;
		}
    }
}
