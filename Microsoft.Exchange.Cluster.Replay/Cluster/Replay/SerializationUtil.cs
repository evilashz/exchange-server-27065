using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000152 RID: 338
	internal static class SerializationUtil
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00039D40 File Offset: 0x00037F40
		public static void ThrowGeneralSerializationException(Exception ex)
		{
			string text = ex.ToString() + Environment.StackTrace;
			ReplayCrimsonEvents.GeneralSerializationError.LogPeriodic<string>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, text);
			if (ex is SerializationException)
			{
				throw ex;
			}
			throw new SerializationException(ex.Message, ex);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00039D8A File Offset: 0x00037F8A
		public static string ObjectToXml(object obj, out Exception ex)
		{
			return Serialization.Instance.ObjectToXml(obj, out ex);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00039D98 File Offset: 0x00037F98
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

		// Token: 0x06000D24 RID: 3364 RVA: 0x00039DB8 File Offset: 0x00037FB8
		public static object XmlToObject(string xmlText, Type objType, out Exception ex)
		{
			return Serialization.Instance.XmlToObject(xmlText, objType, out ex);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00039DC8 File Offset: 0x00037FC8
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
	}
}
