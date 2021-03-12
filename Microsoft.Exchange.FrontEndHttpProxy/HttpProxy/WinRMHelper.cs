using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200009C RID: 156
	internal static class WinRMHelper
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001AAFC File Offset: 0x00018CFC
		internal static IntAppSettingsEntry MaxBytesToPeekIntoRequestStream
		{
			get
			{
				return WinRMHelper.maxBytesToPeekIntoRequestStream;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001AB03 File Offset: 0x00018D03
		internal static BoolAppSettingsEntry WinRMParserEnabled
		{
			get
			{
				return WinRMHelper.winRMParserEnabled;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001AB0A File Offset: 0x00018D0A
		internal static BoolAppSettingsEntry FriendlyErrorEnabled
		{
			get
			{
				return WinRMHelper.friendlyErrorEnabled;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001AB14 File Offset: 0x00018D14
		internal static string GetDiagnosticsInfo(HttpContext context)
		{
			RequestDetailsLogger current = RequestDetailsLoggerBase<RequestDetailsLogger>.GetCurrent(context);
			Guid guid = Guid.Empty;
			IActivityScope activityScope = null;
			if (current != null)
			{
				guid = current.ActivityId;
				activityScope = current.ActivityScope;
			}
			string text = string.Format("[ClientAccessServer={0},BackEndServer={1},RequestId={2},TimeStamp={3}] ", new object[]
			{
				Environment.MachineName,
				(activityScope == null) ? "UnKown" : activityScope.GetProperty(HttpProxyMetadata.TargetServer),
				guid,
				DateTime.UtcNow
			});
			string text2 = string.Empty;
			if (context != null)
			{
				text2 = WinRMInfo.GetFailureCategoryInfo(context);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text += string.Format("[FailureCategory={0}] ", text2);
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[WinRMHelper::GetDiagnosticsInfo] diagnosticsInfo = {0}.", text);
			return text;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001ABD3 File Offset: 0x00018DD3
		internal static void SetDiagnosticsInfoWrittenFlag(NameValueCollection headers)
		{
			if (headers == null)
			{
				return;
			}
			headers["X-Rps-DiagInfoWritten"] = "true";
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001ABE9 File Offset: 0x00018DE9
		internal static bool DiagnosticsInfoHasBeenWritten(NameValueCollection headers)
		{
			return headers != null && headers["X-Rps-DiagInfoWritten"] != null;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001AC04 File Offset: 0x00018E04
		internal static bool TryConvertStatusCode(int originalStatusCode, out int newStatusCode)
		{
			newStatusCode = 0;
			if (originalStatusCode == 401)
			{
				newStatusCode = 400;
				return true;
			}
			if (originalStatusCode == 404)
			{
				newStatusCode = 400;
				return true;
			}
			if (originalStatusCode != 503)
			{
				return false;
			}
			newStatusCode = 500;
			return true;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001AC4C File Offset: 0x00018E4C
		internal static bool IsPingRequest(WebException ex)
		{
			if (ex.Response == null)
			{
				return false;
			}
			if (ex.Response.Headers == null)
			{
				return false;
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[WinRMHelper::IsPingRequest] ex.Response.Headers[WinRMInfo.PingHeaderKey] = {0}.", ex.Response.Headers["X-RemotePS-Ping"]);
			return "Ping".Equals(ex.Response.Headers["X-RemotePS-Ping"], StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		internal static bool CouldBePingRequest(WebException ex)
		{
			if (ex.Status != WebExceptionStatus.ProtocolError)
			{
				return false;
			}
			if (ex.Response == null)
			{
				return false;
			}
			if (WinRMHelper.IsPingRequest(ex))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[WinRMHelper::CouldBePingRequest] ex.Response.Headers[WinRMInfo.PingHeaderKey] = {0}.", ex.Response.Headers["X-RemotePS-Ping"]);
				return false;
			}
			if (!(ex.Response is HttpWebResponse))
			{
				return false;
			}
			HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
			return httpWebResponse.StatusCode == HttpStatusCode.InternalServerError;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001AD38 File Offset: 0x00018F38
		internal static bool TryInsertDiagnosticsInfo(ArraySegment<byte> buffer, Func<string> getDiagnosticInfo, out byte[] updatedBuffer, out string failureHint, Action<string> logging = null)
		{
			failureHint = null;
			updatedBuffer = null;
			if (buffer.Count == 0)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug(0L, "[WinRMHelper::TryInsertDiagnosticsInfo] buffer == null || buffer.Count = 0.");
				failureHint = "buffer Null/Empty";
				return false;
			}
			string @string = Encoding.UTF8.GetString(buffer.Array, 0, 15);
			if (string.IsNullOrEmpty(@string))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[WinRMHelper::TryInsertDiagnosticsInfo] EnvelopString is null/empty.", @string);
				failureHint = "EnvelopString Null/Empty";
				return false;
			}
			if (!@string.StartsWith("<s:Envelope", StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[WinRMHelper::TryInsertDiagnosticsInfo] EnvelopString = {0}, Not start with <s:Envelop.", @string);
				failureHint = "No s:Envelop";
				return false;
			}
			string text = Encoding.UTF8.GetString(buffer.Array, 0, buffer.Count);
			if (string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[WinRMHelper::TryInsertDiagnosticsInfo] Output is null/empty.", text);
				failureHint = "Output Null/Empty";
				return false;
			}
			int num = text.IndexOf("<f:Message>");
			if (num < 0)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<string>(0L, "[WinRMHelper::TryInsertDiagnosticsInfo] Output = {0}, faultMsgIndex < 0.", text);
				failureHint = "No f:Message";
				return false;
			}
			int length = text.Length;
			ExTraceGlobals.VerboseTracer.TraceDebug<string, int>(0L, "[WinRMHelper::TryInsertDiagnosticsInfo] Output(faultMsgIndex + 100) = {0}, faultMsgIndex = {1}.", text.Substring(num, (num + 100 < length) ? 100 : (length - num)), num);
			string text2 = "f:Message";
			int num2 = num + "<f:Message>".Length;
			if (text.IndexOf("<f:ProviderFault", num2, "<f:ProviderFault".Length) >= 0)
			{
				text2 = "<f:ProviderFault";
				text2 = text2.TrimStart(new char[]
				{
					'<'
				});
				num2 = text.IndexOf('>', num2) + 1;
				ExTraceGlobals.VerboseTracer.TraceDebug<string, int>(0L, "[WinRMHelper::TryInsertDiagnosticsInfo] Output(positionToInsert + 100) = {0}, positionToInsert = {1}.", text.Substring(num, (num2 + 100 < length) ? 100 : (length - num2)), num2);
			}
			if (logging != null)
			{
				int num3 = text.IndexOf("<s:Fault");
				if (num3 > 0)
				{
					int num4 = text.IndexOf("<s:Code", num3);
					if (num4 > 0)
					{
						string tagValue = WinRMHelper.GetTagValue(text, "s:Value", num4);
						string text3 = string.Empty;
						int num5 = text.IndexOf("<s:Subcode", num4);
						if (num5 > 0)
						{
							text3 = WinRMHelper.GetTagValue(text, "s:Value", num5);
						}
						else
						{
							WinRMHelper.TraceDebugTagNotFound("TryInsertDiagnosticsInfo", text, "<s:Subcode", 0);
						}
						string tagValue2 = WinRMHelper.GetTagValue(text, text2, num4);
						logging(string.Concat(new string[]
						{
							tagValue,
							"/",
							text3,
							"/",
							tagValue2
						}));
					}
					else
					{
						WinRMHelper.TraceDebugTagNotFound("TryInsertDiagnosticsInf", text, "<s:Code", 0);
					}
				}
				else
				{
					WinRMHelper.TraceDebugTagNotFound("TryInsertDiagnosticsInfo", text, "<s:Fault", 0);
				}
			}
			string value = getDiagnosticInfo();
			text = text.Insert(num2, value);
			updatedBuffer = Encoding.UTF8.GetBytes(text);
			return true;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001AFFC File Offset: 0x000191FC
		private static string GetTagValue(string text, string tag, int offset)
		{
			string funcName = "GetTagValue";
			int num = text.IndexOf("<" + tag, offset);
			if (num > 0)
			{
				int num2 = num + tag.Length + 1;
				num2 = text.IndexOf(">", num);
				if (num2 > 0)
				{
					num2++;
					int num3 = text.IndexOf("</" + tag, num2);
					if (num3 > num2)
					{
						return text.Substring(num2, num3 - num2);
					}
					WinRMHelper.TraceDebugTagNotFound(funcName, text, "</" + tag, num2);
				}
				else
				{
					WinRMHelper.TraceDebugTagNotFound(funcName, text, ">", num);
				}
			}
			else
			{
				WinRMHelper.TraceDebugTagNotFound(funcName, text, "<" + tag, offset);
			}
			return string.Empty;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001B0A4 File Offset: 0x000192A4
		private static void TraceDebugTagNotFound(string funcName, string text, string tag, int offset)
		{
			int num = offset + 100;
			if (num >= text.Length)
			{
				num = text.Length - offset;
			}
			ExTraceGlobals.VerboseTracer.TraceDebug(0L, "[WinRMHelper::{0}] {1} not found in output(+{2}) = {3}", new object[]
			{
				funcName,
				"<s:Subcode",
				offset,
				text.Substring(offset, num)
			});
		}

		// Token: 0x0400038B RID: 907
		internal const string DiagnosticsInfoFlagKey = "X-Rps-DiagInfoWritten";

		// Token: 0x0400038C RID: 908
		internal const string WSManContentType = "application/soap+xml;charset=UTF-8";

		// Token: 0x0400038D RID: 909
		internal const int StreamLookAheadBufferSize = 10000;

		// Token: 0x0400038E RID: 910
		private const string EnvelopStartTag = "<s:Envelope";

		// Token: 0x0400038F RID: 911
		private const int EnvelopToPeekLength = 15;

		// Token: 0x04000390 RID: 912
		private const string MessageTag = "<f:Message>";

		// Token: 0x04000391 RID: 913
		private const string ProviderFaultStartTag = "<f:ProviderFault";

		// Token: 0x04000392 RID: 914
		private const string FaultStartTag = "<s:Fault";

		// Token: 0x04000393 RID: 915
		private const string CodeStartTag = "<s:Code";

		// Token: 0x04000394 RID: 916
		private const string SubcodeStartTag = "<s:Subcode";

		// Token: 0x04000395 RID: 917
		private const string ValueTag = "s:Value";

		// Token: 0x04000396 RID: 918
		private const string FaultMessage = "f:Message";

		// Token: 0x04000397 RID: 919
		private const int MaxBufferCharactersToLogInTrace = 100;

		// Token: 0x04000398 RID: 920
		private static IntAppSettingsEntry maxBytesToPeekIntoRequestStream = new IntAppSettingsEntry("MaxBytesToPeekIntoRequestStream", 2000, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000399 RID: 921
		private static BoolAppSettingsEntry winRMParserEnabled = new BoolAppSettingsEntry("WinRMParserEnabled", true, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400039A RID: 922
		private static BoolAppSettingsEntry friendlyErrorEnabled = new BoolAppSettingsEntry("FriendlyErrorEnabled", true, ExTraceGlobals.VerboseTracer);
	}
}
