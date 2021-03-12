using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000026 RID: 38
	internal abstract class UMClientCommonBase : DisposableBase
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008AE8 File Offset: 0x00006CE8
		protected static UMClientAccessCountersInstance Counters
		{
			get
			{
				return UMClientCommonBase.counters;
			}
		}

		// Token: 0x1700009C RID: 156
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00008AEF File Offset: 0x00006CEF
		protected string TracePrefix
		{
			set
			{
				this.tracePrefix = value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public static void InitializePerformanceCounters(bool isWebService)
		{
			try
			{
				UMClientCommonBase.counters = UMClientAccessCounters.GetInstance(isWebService ? "Outlook" : "OWA");
				UMClientAccessCounters.ResetInstance(UMClientCommonBase.counters.Name);
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					UMClientCommonBase.counters.PID.RawValue = (long)currentProcess.Id;
				}
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ClientAccessTracer, 0, "Failed to initialize perf counters: {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00008B98 File Offset: 0x00006D98
		public UMCallInfoEx GetCallInfo(string callId)
		{
			UMCallInfoEx result;
			try
			{
				this.DebugTrace("GetCallInfo({0})", new object[]
				{
					callId
				});
				string fqdn;
				string text;
				this.DecodeCallId(callId, out fqdn, out text);
				UMServerProxy server = UMServerManager.GetServer(fqdn);
				UMCallInfoEx callInfo = server.GetCallInfo(text);
				this.DebugTrace("GetCallInfo({0}): CallId:{1} CallState:{2}, EvtCause:{3}, RespCode:{4}, RespText:{5}", new object[]
				{
					callId,
					text,
					callInfo.CallState,
					callInfo.EventCause,
					callInfo.ResponseCode,
					callInfo.ResponseText
				});
				result = callInfo;
			}
			catch (LocalizedException exception)
			{
				this.LogException(exception);
				throw;
			}
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00008C54 File Offset: 0x00006E54
		public void Disconnect(string callId)
		{
			try
			{
				this.DebugTrace("Disconnect({0})", new object[]
				{
					callId
				});
				string fqdn;
				string callId2;
				this.DecodeCallId(callId, out fqdn, out callId2);
				UMServerProxy server = UMServerManager.GetServer(fqdn);
				if (!UMServerManager.IsAuthorizedUMServer(server))
				{
					throw new FormatException("Invalid tokens in callId: " + callId);
				}
				UMCallInfoEx callInfo = server.GetCallInfo(callId2);
				if (callInfo == null || !Enum.IsDefined(typeof(UMCallState), callInfo.CallState))
				{
					throw new FormatException("Invalid tokens in callId: " + callId);
				}
				server.Disconnect(callId2);
			}
			catch (LocalizedException exception)
			{
				this.LogException(exception);
				throw;
			}
			catch (FormatException ex)
			{
				this.LogException(ex);
				throw new InvalidCallIdException(ex);
			}
		}

		// Token: 0x06000231 RID: 561
		protected abstract void DisposeMe();

		// Token: 0x06000232 RID: 562
		protected abstract string GetUserContext();

		// Token: 0x06000233 RID: 563 RVA: 0x00008D24 File Offset: 0x00006F24
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeMe();
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008D2F File Offset: 0x00006F2F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UMClientCommonBase>(this);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008D37 File Offset: 0x00006F37
		protected void DebugTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ClientAccessTracer, this.GetHashCode(), this.tracePrefix + formatString, formatObjects);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008D5C File Offset: 0x00006F5C
		protected void LogException(Exception exception)
		{
			this.DebugTrace("{0}", new object[]
			{
				exception
			});
			StackTrace stackTrace = new StackTrace();
			string name = stackTrace.GetFrame(1).GetMethod().Name;
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMClientAccessError, null, new object[]
			{
				name,
				this.GetUserContext(),
				exception.Message
			});
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008DC8 File Offset: 0x00006FC8
		protected string EncodeCallId(string serverFqdn, string sessionId)
		{
			this.DebugTrace("EncodeCallId: ServerFqdn:{0} SessionID:{1}", new object[]
			{
				serverFqdn,
				sessionId
			});
			string s = sessionId + "|" + serverFqdn;
			string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(s), Base64FormattingOptions.None);
			this.DebugTrace("EncodeCallId(Base64): {0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008E28 File Offset: 0x00007028
		private void DecodeCallId(string base64CallId, out string serverFqdn, out string sessionId)
		{
			serverFqdn = null;
			sessionId = null;
			try
			{
				this.DebugTrace("DecodeCallId(Base64CallId:{0})", new object[]
				{
					base64CallId
				});
				if (string.IsNullOrEmpty(base64CallId) || base64CallId.Length > 1420)
				{
					throw new FormatException();
				}
				byte[] array = Convert.FromBase64String(base64CallId);
				if (array == null || array.Length == 0)
				{
					throw new FormatException();
				}
				string @string = Encoding.UTF8.GetString(array);
				string[] array2 = @string.Split(new char[]
				{
					'|'
				});
				if (array2 == null || array2.Length != 2)
				{
					throw new FormatException("Invalid tokens in callId: " + @string);
				}
				serverFqdn = array2[1];
				sessionId = array2[0];
				this.DebugTrace("DecodeCallId: ServerFqdn:{0} SessionID:{1}", new object[]
				{
					serverFqdn,
					sessionId
				});
			}
			catch (FormatException innerException)
			{
				throw new InvalidCallIdException(innerException);
			}
		}

		// Token: 0x040000B6 RID: 182
		private const int MaxBase64CallIdLength = 1420;

		// Token: 0x040000B7 RID: 183
		private static UMClientAccessCountersInstance counters;

		// Token: 0x040000B8 RID: 184
		private string tracePrefix = string.Empty;
	}
}
