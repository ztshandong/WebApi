using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebApi.Public
{
    public class ZipTools
    {
        public static byte[] CompressionObject(object DataOriginal)
        {
            if (DataOriginal == null) return null;
            BinaryFormatter bFormatter = new BinaryFormatter();
            MemoryStream mStream = new MemoryStream();
            bFormatter.Serialize(mStream, DataOriginal);
            byte[] bytes = mStream.ToArray();
            MemoryStream oStream = new MemoryStream();
            DeflateStream zipStream = new DeflateStream(oStream, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
            zipStream.Flush();
            zipStream.Close();
            return oStream.ToArray();
        }

        public static object DecompressionObject(byte[] bytes)
        {
            if (bytes == null) return null;
            MemoryStream mStream = new MemoryStream(bytes);
            mStream.Seek(0, SeekOrigin.Begin);
            DeflateStream unZipStream = new DeflateStream(mStream, CompressionMode.Decompress, true);
            object dsResult = null;
            BinaryFormatter bFormatter = new BinaryFormatter();
            dsResult = (object)bFormatter.Deserialize(unZipStream);
            return dsResult;
        }
    }
}
