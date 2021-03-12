using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Serialization
{
	// Token: 0x020002A0 RID: 672
	internal static class Serialization
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x0004C798 File Offset: 0x0004A998
		public static byte[] ObjectToBytes(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			byte[] buffer;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, obj);
				buffer = memoryStream.GetBuffer();
			}
			return buffer;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0004C7E4 File Offset: 0x0004A9E4
		public static object BytesToObject(byte[] mBinaryData)
		{
			if (mBinaryData == null || mBinaryData.Length == 0)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			object result;
			using (MemoryStream memoryStream = new MemoryStream(mBinaryData, false))
			{
				result = binaryFormatter.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0004C830 File Offset: 0x0004AA30
		public static string ObjectToXML(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
				xmlSerializer.Serialize(stringWriter, obj);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0004C880 File Offset: 0x0004AA80
		public static object XMLToObject(string xmlText, Type objType)
		{
			if (xmlText == null || xmlText.Length == 0)
			{
				return null;
			}
			object result;
			using (StringReader stringReader = new StringReader(xmlText))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(objType);
				result = xmlSerializer.Deserialize(stringReader);
			}
			return result;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0004C8D0 File Offset: 0x0004AAD0
		public static void SerializeUInt64(byte[] buf, ref int bytePos, ulong val)
		{
			for (int i = 0; i < 8; i++)
			{
				buf[bytePos++] = (byte)(val & 255UL);
				val >>= 8;
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0004C904 File Offset: 0x0004AB04
		public static void SerializeUInt32(byte[] buf, ref int bytePos, uint val)
		{
			for (int i = 0; i < 4; i++)
			{
				buf[bytePos++] = (byte)(val & 255U);
				val >>= 8;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0004C938 File Offset: 0x0004AB38
		public static void SerializeUInt16(byte[] buf, ref int bytePos, ushort val)
		{
			buf[bytePos++] = (byte)(val & 255);
			buf[bytePos++] = (byte)(val >> 8 & 255);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0004C970 File Offset: 0x0004AB70
		public static void SerializeGuid(byte[] buf, ref int bytePos, Guid val)
		{
			byte[] array = val.ToByteArray();
			Buffer.BlockCopy(array, 0, buf, bytePos, array.Length);
			bytePos += array.Length;
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0004C99C File Offset: 0x0004AB9C
		public static ulong DeserializeUInt64(byte[] buf, ref int bytePos)
		{
			ulong num = 0UL;
			for (int i = 7; i >= 0; i--)
			{
				num <<= 8;
				num |= (ulong)buf[i + bytePos];
			}
			bytePos += 8;
			return num;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0004C9D0 File Offset: 0x0004ABD0
		public static uint DeserializeUInt32(byte[] buf, ref int bytePos)
		{
			uint num = 0U;
			for (int i = 3; i >= 0; i--)
			{
				num <<= 8;
				num |= (uint)buf[i + bytePos];
			}
			bytePos += 4;
			return num;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0004CA00 File Offset: 0x0004AC00
		public static ushort DeserializeUInt16(byte[] buf, ref int bytePos)
		{
			ushort num = (ushort)buf[bytePos++];
			return num | (ushort)(buf[bytePos++] << 8);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0004CA2C File Offset: 0x0004AC2C
		public static Guid DeserializeGuid(byte[] buf, ref int bytePos)
		{
			Guid result = new Guid(BitConverter.ToInt32(buf, bytePos), BitConverter.ToInt16(buf, bytePos + 4), BitConverter.ToInt16(buf, bytePos + 6), buf[bytePos + 8], buf[bytePos + 9], buf[bytePos + 10], buf[bytePos + 11], buf[bytePos + 12], buf[bytePos + 13], buf[bytePos + 14], buf[bytePos + 15]);
			bytePos += 16;
			return result;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0004CA9C File Offset: 0x0004AC9C
		public static void SerializeDateTime(byte[] buf, ref int bytePos, DateTime val)
		{
			long val2 = val.ToBinary();
			Serialization.SerializeUInt64(buf, ref bytePos, (ulong)val2);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0004CABC File Offset: 0x0004ACBC
		public static DateTime DeserializeDateTime(byte[] buf, ref int bytePos)
		{
			long dateData = (long)Serialization.DeserializeUInt64(buf, ref bytePos);
			return DateTime.FromBinary(dateData);
		}

		// Token: 0x04000E4C RID: 3660
		public const int DateTimeSerializationLength = 8;
	}
}
