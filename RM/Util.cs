using System.Globalization;
using System.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.DB;
using System.IO;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.RevitAddIns;
using System.Windows.Data;

namespace RM
{
    class Util
    {
        // Define cultureInfo
        public static ResourceManager GetLanguageResources;   // declare Resource manager to access to specific cultureinfo
        public static CultureInfo Cult;        // declare culture info

        public static void GetLocalisationValues(UIControlledApplication application)
        {
            string lang = application.ControlledApplication.Language.ToString();
            if (lang == "Russian")
            {
                // Create the culture for russian
                Util.Cult = CultureInfo.CreateSpecificCulture("ru");
                Util.GetLanguageResources = new System.Resources.ResourceManager("RM.Resources.rus", System.Reflection.Assembly.GetExecutingAssembly());
            }
            else
            {
                // Create the culture for english
                Util.Cult = CultureInfo.CreateSpecificCulture("en");
                Util.GetLanguageResources = new System.Resources.ResourceManager("RM.Resources.eng", System.Reflection.Assembly.GetExecutingAssembly());
            }
        }

        public static double? GetFromString(string heightValueString, Units units)
        {
            // Check the string value
            double lenght;
            if (Autodesk.Revit.DB.UnitFormatUtils.TryParse(units, UnitType.UT_Length, heightValueString, out lenght))
            {
                return lenght;
            }
            else
            {
                return null;
            }
        }



        public static void ExtractRessource(string resourceName, string path)
        {
            using (Stream input = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (Stream output = File.Create(path))
            {

                // Insert null checking here for production
                byte[] buffer = new byte[8192];

                int bytesRead;
                while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, bytesRead);
                }

            }
        }
    }

    /// <summary>
    /// Retrive the error message for displaying it in the Revit interface
    /// </summary>
    public class ErrorMessageException : ApplicationException
    {
        /// <summary>
        /// constructor entirely using baseclass'
        /// </summary>
        public ErrorMessageException()
            : base()
        {
        }

        /// <summary>
        /// constructor entirely using baseclass'
        /// </summary>
        /// <param name="message">error message</param>
        public ErrorMessageException(String message)
            : base(message)
        {
        }
    }


    /// <summary>
    /// Manage Warning in the Revit interface
    /// </summary>
    public class PlintePreprocessor : IFailuresPreprocessor
    {
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            // Inside event handler, get all warnings
            IList<FailureMessageAccessor> failList = failuresAccessor.GetFailureMessages();
            foreach (FailureMessageAccessor failure in failList)
            {
                // check FailureDefinitionIds against ones that you want to dismiss,
                FailureDefinitionId failId = failure.GetFailureDefinitionId();
                // prevent Revit from showing Unenclosed room warnings
                if (failId == BuiltInFailures.OverlapFailures.WallsOverlap)
                {
                    failuresAccessor.DeleteWarning(failure);
                }
            }

            return FailureProcessingResult.Continue;
        }
    }

}
