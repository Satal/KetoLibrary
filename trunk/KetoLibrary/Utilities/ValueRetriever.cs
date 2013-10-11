using System;
using System.IO;

namespace KetoLibrary.Utilities
{
    public static class ValueRetriever
    {
        /// <summary>
        /// Retrieves a value from an object, handling nulls and if specified all other exceptions
        /// </summary>
        /// <typeparam name="TInputType">The type of object that the value is going to be retrieved from</typeparam>
        /// <typeparam name="TOutputType">The output type to be retrieved</typeparam>
        /// <param name="obj">The object to have the value retrieved from</param>
        /// <param name="valueLocation">The location within the object that we want value from</param>
        /// <param name="defaultValue">The value to be returned if a null is encountered, if not specified then it is the default for TOutputType</param>
        /// <param name="throwAnythingButNullException"></param>
        /// <returns>The value at the specified location or the default value of TOutputType</returns>
        public static TOutputType RetrieveValue<TInputType, TOutputType>(TInputType obj,
                                                                         Func<TInputType, TOutputType> valueLocation,
                                                                         TOutputType defaultValue = default(TOutputType),
                                                                         bool throwAnythingButNullException = true)
        {
            var returnValue = defaultValue;

            try
            {
                returnValue = valueLocation.Invoke(obj);

                // If we got null returned by the Invoke then lets supply the default value
                // which could be null too but that's what the user wants
                if (Equals(returnValue, default(TOutputType)))
                {
                    returnValue = defaultValue;
                }
            }
            catch (NullReferenceException)
            {
                // We weren't able to execute the method as we had a null somewhere in there
                // Do nothing as we have already assigned the default value to returnValue
            }
            catch
            {
                // Generic catch, at this point we are throwing anything that isn't a null
                // reference exception, so for example a file not found excception will be
                // thrown if we have speccified throwAnythingButNullException to true.
                if (throwAnythingButNullException) throw;
            }

            return returnValue;
        }

public static void Test()
{
    var file = new XfeFileInfo(@"C:\Users\satal");

    // Specify an external method to perform to retrieve the value, good if you need to do a bit more complex processing
    var fileLocation = RetrieveValue<XfeFileInfo, string>(file, ValueLocationMethod);

    // Specifying a Lambda method to specify the location of the value
    var parentLocation = RetrieveValue<XfeFileInfo, string>(file, info => info.FileInfo.Directory.Parent.FullName);
    
    // Specifying that we want RetrieveValue to handle all exceptions and return the default value
    var fileSize = RetrieveValue(file, info => info.FileInfo.Length, throwAnythingButNullException: false);

    // Passing in the value to return if we get a null at any point
    fileSize = RetrieveValue(file, info => info.FileInfo.Length, -1, false);

    Console.WriteLine("{0} - {1}", parentLocation, fileSize);
}

private static string ValueLocationMethod(XfeFileInfo xfeFileInfo)
{
    return xfeFileInfo.FileInfo.FullName;
}

        public class XfeFileInfo
        {
            public FileInfo FileInfo { get; set; }
            public XfeFileInfo(string fileLocation)
            {
                throw new NotImplementedException();
            }
        }
    }
}