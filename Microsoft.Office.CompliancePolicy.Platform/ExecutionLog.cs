using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200004C RID: 76
	public abstract class ExecutionLog
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00005EE4 File Offset: 0x000040E4
		public static void GetExceptionTypeAndDetails(Exception e, out List<string> types, out List<string> messages, out string chain, bool chainOnly)
		{
			Exception ex = e;
			chain = string.Empty;
			types = null;
			messages = null;
			if (!chainOnly)
			{
				types = new List<string>();
				messages = new List<string>();
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			for (;;)
			{
				string text = ex.GetType().ToString();
				string message = ex.Message;
				if (!chainOnly)
				{
					types.Add(text);
					messages.Add(message);
				}
				stringBuilder.Append("[Type:");
				stringBuilder.Append(text);
				stringBuilder.Append("]");
				stringBuilder.Append("[Message:");
				stringBuilder.Append(message);
				stringBuilder.Append("]");
				stringBuilder.Append("[Stack:");
				stringBuilder.Append(string.IsNullOrEmpty(ex.StackTrace) ? string.Empty : ex.StackTrace.Replace("\r\n", string.Empty));
				stringBuilder.Append("]");
				if (ex.InnerException == null || num > 10)
				{
					break;
				}
				ex = ex.InnerException;
				num++;
			}
			chain = stringBuilder.ToString();
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005FF1 File Offset: 0x000041F1
		public virtual void LogInformation(string client, string tenantId, string correlationId, string contextData, params KeyValuePair<string, object>[] customData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005FF8 File Offset: 0x000041F8
		public virtual void LogVerbose(string client, string tenantId, string correlationId, string contextData, params KeyValuePair<string, object>[] customData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005FFF File Offset: 0x000041FF
		public virtual void LogWarnining(string client, string tenantId, string correlationId, string contextData, params KeyValuePair<string, object>[] customData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006006 File Offset: 0x00004206
		public virtual void LogError(string client, string tenantId, string correlationId, Exception exception, string contextData, params KeyValuePair<string, object>[] customData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A9 RID: 425
		public abstract void LogOneEntry(string client, string tenantId, string correlationId, ExecutionLog.EventType eventType, string tag, string contextData, Exception exception, params KeyValuePair<string, object>[] customData);

		// Token: 0x060001AA RID: 426
		public abstract void LogOneEntry(string client, string correlationId, ExecutionLog.EventType eventType, string contextData, Exception exception);

		// Token: 0x060001AB RID: 427 RVA: 0x0000600D File Offset: 0x0000420D
		public void LogOneEntry(ExecutionLog.EventType eventType, string client, string correlationId, Exception exception, string format, params object[] args)
		{
			this.LogOneEntry(client, correlationId, eventType, string.Format(CultureInfo.InvariantCulture, format, args), exception);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006028 File Offset: 0x00004228
		public void LogOneEntry(ExecutionLog.EventType eventType, string client, string correlationId, string format, params object[] args)
		{
			this.LogOneEntry(eventType, client, correlationId, null, format, args);
		}

		// Token: 0x040000E3 RID: 227
		public const string TagKey = "Tag";

		// Token: 0x040000E4 RID: 228
		private const string CRLF = "\r\n";

		// Token: 0x0200004D RID: 77
		public enum EventType
		{
			// Token: 0x040000E6 RID: 230
			Verbose,
			// Token: 0x040000E7 RID: 231
			Information,
			// Token: 0x040000E8 RID: 232
			Warning,
			// Token: 0x040000E9 RID: 233
			Error,
			// Token: 0x040000EA RID: 234
			CriticalError
		}
	}
}
