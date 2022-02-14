using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.Helpers.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            if (arrBytes == null)
                throw new ArgumentNullException(nameof(arrBytes));

            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            ms.Write(arrBytes, 0, arrBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);

            return (Object)bf.Deserialize(ms);
        }
    }
}
