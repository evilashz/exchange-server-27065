using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Cluster.Common.Extensions
{
	// Token: 0x02000005 RID: 5
	internal class Serialization : ISerialization
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000021FA File Offset: 0x000003FA
		private Serialization()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002202 File Offset: 0x00000402
		public static Serialization Instance
		{
			get
			{
				return Serialization.instance;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000220C File Offset: 0x0000040C
		public string ObjectToXml(object obj, out Exception ex)
		{
			ex = null;
			if (obj == null)
			{
				return null;
			}
			try
			{
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					XmlSerializer xmlSerializer = new SafeXmlSerializer(obj.GetType());
					xmlSerializer.Serialize(stringWriter, obj);
					return stringWriter.ToString();
				}
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			return null;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000228C File Offset: 0x0000048C
		public string ObjectToXml(object obj)
		{
			Exception ex;
			string result = this.ObjectToXml(obj, out ex);
			if (ex != null)
			{
				throw new SerializationException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022B4 File Offset: 0x000004B4
		public object XmlToObject(string xmlText, Type objType, out Exception ex)
		{
			ex = null;
			if (xmlText == null || xmlText.Length == 0)
			{
				return null;
			}
			try
			{
				using (StringReader stringReader = new StringReader(xmlText))
				{
					XmlSerializer xmlSerializer = new SafeXmlSerializer(objType);
					return xmlSerializer.Deserialize(stringReader);
				}
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000232C File Offset: 0x0000052C
		public object XmlToObject(string xmlText, Type objType)
		{
			Exception ex;
			object result = this.XmlToObject(xmlText, objType, out ex);
			if (ex != null)
			{
				throw new SerializationException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002358 File Offset: 0x00000558
		public static void SerializeUInt64(byte[] buf, ref int bytePos, ulong val)
		{
			for (int i = 0; i < 8; i++)
			{
				buf[bytePos++] = (byte)(val & 255UL);
				val >>= 8;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000238C File Offset: 0x0000058C
		public static void SerializeUInt32(byte[] buf, ref int bytePos, uint val)
		{
			for (int i = 0; i < 4; i++)
			{
				buf[bytePos++] = (byte)(val & 255U);
				val >>= 8;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023C0 File Offset: 0x000005C0
		public static void SerializeUInt16(byte[] buf, ref int bytePos, ushort val)
		{
			buf[bytePos++] = (byte)(val & 255);
			buf[bytePos++] = (byte)(val >> 8 & 255);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023F8 File Offset: 0x000005F8
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

		// Token: 0x06000023 RID: 35 RVA: 0x0000242C File Offset: 0x0000062C
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

		// Token: 0x06000024 RID: 36 RVA: 0x0000245C File Offset: 0x0000065C
		public static ushort DeserializeUInt16(byte[] buf, ref int bytePos)
		{
			ushort num = (ushort)buf[bytePos++];
			return num | (ushort)(buf[bytePos++] << 8);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002488 File Offset: 0x00000688
		public static void SerializeDateTime(byte[] buf, ref int bytePos, DateTime val)
		{
			long val2 = val.ToBinary();
			Serialization.SerializeUInt64(buf, ref bytePos, (ulong)val2);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024A8 File Offset: 0x000006A8
		public static DateTime DeserializeDateTime(byte[] buf, ref int bytePos)
		{
			long dateData = (long)Serialization.DeserializeUInt64(buf, ref bytePos);
			return DateTime.FromBinary(dateData);
		}

		// Token: 0x04000006 RID: 6
		public const int DateTimeSerializationLength = 8;

		// Token: 0x04000007 RID: 7
		private static Serialization instance = new Serialization();
	}
}
