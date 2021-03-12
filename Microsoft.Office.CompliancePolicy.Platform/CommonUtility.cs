using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000032 RID: 50
	internal static class CommonUtility
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000331C File Offset: 0x0000151C
		public static string ObjectToString(object valueToConvert)
		{
			string empty = string.Empty;
			if (valueToConvert != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
				{
					new XmlSerializer(valueToConvert.GetType()).Serialize(stringWriter, valueToConvert);
				}
				return stringBuilder.ToString();
			}
			return empty;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000337C File Offset: 0x0000157C
		public static T StringToObject<T>(string str)
		{
			T result = default(T);
			if (!string.IsNullOrEmpty(str))
			{
				using (StringReader stringReader = new StringReader(str))
				{
					result = (T)((object)new XmlSerializer(typeof(T)).Deserialize(XmlReader.Create(stringReader)));
				}
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000033E0 File Offset: 0x000015E0
		public static byte[] ObjectToBytes(object valueToConvert)
		{
			byte[] result = null;
			if (valueToConvert != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					binaryFormatter.Serialize(memoryStream, valueToConvert);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000342C File Offset: 0x0000162C
		public static object BytesToObject(byte[] valueToConvert)
		{
			object result = null;
			if (valueToConvert != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(valueToConvert))
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
					result = binaryFormatter.Deserialize(memoryStream);
				}
			}
			return result;
		}
	}
}
