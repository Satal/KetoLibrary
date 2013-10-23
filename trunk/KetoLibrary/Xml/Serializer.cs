using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace KetoLibrary.Xml
{
    public static class Serializer
    {
        private static readonly Dictionary<Type, XmlSerializer> Serializers = new Dictionary<Type, XmlSerializer>();

        public static string Serialize<T>(T obj)
        {
            // If we are looking to serialize a null then just return an empty string
            if (obj == null)
            {
                return "";
            }

            var serializer = GetSerializer<T>();

            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(xmlWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static T Deserialize<T>(string val)
        {
            // If we've been passed a null or empty string just return the default value
            if (String.IsNullOrEmpty(val))
            {
                return default(T);
            }

            var serializer = GetSerializer<T>();

            var stringReader = new StringReader(val);
            using (var xmlReader = XmlReader.Create(stringReader))
            {
                if (!serializer.CanDeserialize(xmlReader))
                {
                    return default(T);
                }

                var deserialized = default(T);

                try
                {
                    deserialized = (T)serializer.Deserialize(xmlReader);
                }
                catch (InvalidOperationException ioe)
                {
                    // We had an issue while deserializing the XML document, this is probably that the
                    // XML was not structured properly i.e. missing the end doc tag
                }

                return deserialized;
            }
        }

        private static XmlSerializer GetSerializer<T>()
        {
            var type = typeof(T);
            if (!Serializers.ContainsKey(type))
            {
                Serializers.Add(type, new XmlSerializer(type));
            }

            return Serializers[type];
        }
    }
}
