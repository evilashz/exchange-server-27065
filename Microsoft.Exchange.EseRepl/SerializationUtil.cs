using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000019 RID: 25
	internal static class SerializationUtil
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003860 File Offset: 0x00001A60
		public static void ThrowGeneralSerializationException(Exception ex)
		{
			string text = ex.ToString() + Environment.StackTrace;
			ReplayCrimsonEvents.GeneralSerializationError.LogPeriodic<string>(Environment.MachineName, Parameters.CurrentValues.DefaultEventSuppressionInterval, text);
			if (ex is SerializationException)
			{
				throw ex;
			}
			throw new SerializationException(ex.Message, ex);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000038AF File Offset: 0x00001AAF
		public static string ObjectToXml(object obj, out Exception ex)
		{
			return Serialization.Instance.ObjectToXml(obj, out ex);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000038C0 File Offset: 0x00001AC0
		public static string ObjectToXml(object obj)
		{
			Exception ex;
			string result = SerializationUtil.ObjectToXml(obj, out ex);
			if (ex != null)
			{
				SerializationUtil.ThrowGeneralSerializationException(ex);
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000038E0 File Offset: 0x00001AE0
		public static object XmlToObject(string xmlText, Type objType, out Exception ex)
		{
			return Serialization.Instance.XmlToObject(xmlText, objType, out ex);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000038F0 File Offset: 0x00001AF0
		public static object XmlToObject(string xmlText, Type objType)
		{
			Exception ex;
			object result = SerializationUtil.XmlToObject(xmlText, objType, out ex);
			if (ex != null)
			{
				SerializationUtil.ThrowGeneralSerializationException(ex);
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003914 File Offset: 0x00001B14
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

		// Token: 0x060000B0 RID: 176 RVA: 0x00003960 File Offset: 0x00001B60
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
	}
}
