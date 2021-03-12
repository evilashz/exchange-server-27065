using System;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000044 RID: 68
	internal static class HttpContextLoggingExtensionMethods
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000E504 File Offset: 0x0000C704
		public static void AppendLogResponseInfo(this HttpContextBase context, ResponseCode? responseCode, LID? failureLID, AsyncOperation asyncOperation)
		{
			StringBuilder stringBuilder = context.Items["MapiHttpLoggingModuleLogger"] as StringBuilder;
			if (stringBuilder == null)
			{
				return;
			}
			string value = null;
			if (asyncOperation != null)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				asyncOperation.AppendLogString(stringBuilder2);
				value = stringBuilder2.ToString();
			}
			if (responseCode != null || failureLID != null || !string.IsNullOrEmpty(value))
			{
				string arg = string.Empty;
				stringBuilder.Append("&ResponseInfo=");
				if (responseCode != null)
				{
					stringBuilder.Append(string.Format("XRC:{0}", (int)responseCode.Value));
					arg = ";";
				}
				if (failureLID != null)
				{
					stringBuilder.Append(string.Format("{0}LID:{1}", arg, (int)failureLID.Value));
				}
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.Append(value);
				}
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public static void AppendLogExceptionInfo(this HttpContextBase context, Exception failureException)
		{
			StringBuilder stringBuilder = context.Items["MapiHttpLoggingModuleLogger"] as StringBuilder;
			if (stringBuilder == null)
			{
				return;
			}
			if (failureException != null)
			{
				stringBuilder.Append("&ExceptionInfo=");
				stringBuilder.Append("type:");
				stringBuilder.Append(failureException.GetType().ToString());
				stringBuilder.Append(";message:");
				stringBuilder.Append(failureException.Message);
			}
		}

		// Token: 0x0400010D RID: 269
		private const string ResponseInfoLogParameter = "&ResponseInfo=";

		// Token: 0x0400010E RID: 270
		private const string ExceptionInfoLogParameter = "&ExceptionInfo=";
	}
}
