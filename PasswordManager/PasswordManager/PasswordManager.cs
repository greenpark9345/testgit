/*
 * Program:         PasswordManager.exe
 * Module:          PasswordManager.cs
 * Date:            <enter a date>
 * Author:          <enter your name>
 * Description:     Some free starting code for INFO-3110 project 1, the PasswordManager
 *                  application.
 *                  
 *                  Note that password strings can be tested for "strength" using the Password
 *                  class provided in this project.  Here's a quick example:
 *                  
 *                  Password pw = new Password("somepassword");
 *                  Console.WriteLine("That password is " + pw.StrengthLabel);
 *                  Console.WriteLine("That password has a strength of " + pw.StrengthPercent + "%");
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Namespaces added manually
using System.Xml;           // XmlReader, XmlDocument and XmlReaderSetting classes
using System.Xml.Schema;    // XmlSchemaValidationFlags class
using System.IO;            // File class

namespace PasswordManager
{
    class Program
    {
        // Member variables
        private static string xmlFile = "";
        private static bool valid_xml = true;
        private static XmlDocument doc = null;

        static void Main(string[] args)
        {
            try
            {
                // Print a blank line
                Console.WriteLine();

                // Get the name of the XML file 
                if (args.Count() > 0 && File.Exists(args[0]))
                {
                    // Getting XML file name from the command line
                    xmlFile = args[0];
                }
                else
                {
                    // Ask the user to input the file name 
                    bool invalidFile = true;
                    do
                    {
                        Console.Write("Enter the path name of your passwords file: ");
                        xmlFile = Console.ReadLine();

                        if (!File.Exists(xmlFile))
                            Console.WriteLine("ERROR: The file '{0}' can't be found!", xmlFile);
                        else
                            invalidFile = false;

                    } while (invalidFile);

                    // Print a blank line
                    Console.WriteLine();
                }

                // Set the validation settings
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
                ValidationEventHandler handler
                        = new ValidationEventHandler(ValidationCallback);
                settings.ValidationEventHandler += handler;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

                // Create the XmlReader object and read/validate the XML file
                XmlReader reader = XmlReader.Create(xmlFile, settings);

                // Load the xml into the DOM
                doc = new XmlDocument();
                doc.Load(reader);

                if (valid_xml)
                {
                    // ************************** ADD YOUR MAIN CODE HERE! **************************

                }
            }
            catch (XmlException ex)
            {
                Console.WriteLine("{0,-20}{1}", "ERROR:", ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine("{0,-20}{1}", "ERROR:", ex.Message);
            }

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();

        } // end Main()

        // Callback method to display validation errors and warnings if the xml file is invalid
        // according to its schema
        private static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\n{0,-20}{1}", "WARNING:", args.Message);
            else
            {
                Console.WriteLine("\n{0,-20}{1}", "SCHEMA ERROR:", args.Message);
                valid_xml = false;
            }
        } // end ValidationCallback()
 
    } // end class
}
