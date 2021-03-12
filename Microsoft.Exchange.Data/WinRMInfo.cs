using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Security;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002BD RID: 701
	internal class WinRMInfo
	{
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0004F25D File Offset: 0x0004D45D
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x0004F265 File Offset: 0x0004D465
		public string Action { get; set; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0004F26E File Offset: 0x0004D46E
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x0004F276 File Offset: 0x0004D476
		public string RawAction { get; set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x0004F27F File Offset: 0x0004D47F
		// (set) Token: 0x0600192B RID: 6443 RVA: 0x0004F287 File Offset: 0x0004D487
		public string SessionId { get; set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x0004F290 File Offset: 0x0004D490
		public string FomattedSessionId
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.SessionId))
				{
					return null;
				}
				string text = this.SessionId;
				if (text.StartsWith("uuid:"))
				{
					text = text.Substring("uuid:".Length);
				}
				Guid guid;
				if (Guid.TryParse(text, out guid))
				{
					return text;
				}
				return null;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x0004F2DE File Offset: 0x0004D4DE
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0004F2E6 File Offset: 0x0004D4E6
		public string ShellId { get; set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x0004F2EF File Offset: 0x0004D4EF
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x0004F2F7 File Offset: 0x0004D4F7
		public string CommandId { get; set; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0004F300 File Offset: 0x0004D500
		// (set) Token: 0x06001932 RID: 6450 RVA: 0x0004F308 File Offset: 0x0004D508
		public string CommandName { get; set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0004F311 File Offset: 0x0004D511
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x0004F319 File Offset: 0x0004D519
		public string SignalCode { get; set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0004F322 File Offset: 0x0004D522
		public string SessionUniqueId
		{
			get
			{
				return this.SessionId ?? this.ShellId;
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0004F334 File Offset: 0x0004D534
		public static void StampToHttpHeaders(WinRMInfo winRMInfo, NameValueCollection httpHeaders)
		{
			httpHeaders["X-RemotePs-Action"] = winRMInfo.Action;
			httpHeaders["X-RemotePs-RawAction"] = winRMInfo.RawAction;
			httpHeaders["X-RemotePs-SessionId"] = winRMInfo.SessionId;
			httpHeaders["X-RemotePs-ShellId"] = winRMInfo.ShellId;
			httpHeaders["X-RemotePs-CommandId"] = winRMInfo.CommandId;
			httpHeaders["X-RemotePs-CommandName"] = winRMInfo.CommandName;
			httpHeaders["X-RemotePs-SignalCode"] = winRMInfo.SignalCode;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0004F3B8 File Offset: 0x0004D5B8
		public static WinRMInfo GetWinRMInfoFromHttpHeaders(NameValueCollection httpHeaders)
		{
			WinRMInfo winRMInfo = null;
			if (httpHeaders != null)
			{
				winRMInfo = new WinRMInfo();
				winRMInfo.Action = httpHeaders["X-RemotePs-Action"];
				winRMInfo.RawAction = httpHeaders["X-RemotePs-RawAction"];
				winRMInfo.SessionId = httpHeaders["X-RemotePs-SessionId"];
				winRMInfo.ShellId = httpHeaders["X-RemotePs-ShellId"];
				winRMInfo.CommandId = httpHeaders["X-RemotePs-CommandId"];
				winRMInfo.CommandName = httpHeaders["X-RemotePs-CommandName"];
				winRMInfo.SignalCode = httpHeaders["X-RemotePs-SignalCode"];
			}
			return winRMInfo;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0004F448 File Offset: 0x0004D648
		public static bool IsHeaderReserverd(string headerName)
		{
			return "X-RemotePs-Action".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "X-RemotePs-RawAction".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "X-RemotePs-SessionId".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "X-RemotePs-ShellId".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "X-RemotePs-CommandId".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "X-RemotePs-CommandName".Equals(headerName, StringComparison.OrdinalIgnoreCase) || "X-RemotePs-SignalCode".Equals(headerName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0004F4B7 File Offset: 0x0004D6B7
		public static void SetFailureCategoryInfo(NameValueCollection httpHeaders, FailureCategory fc, string fcSubInfo)
		{
			if (string.IsNullOrEmpty(httpHeaders["X-RemotePS-FailureCategory"]))
			{
				httpHeaders["X-RemotePS-FailureCategory"] = string.Format("{0}-{1}", fc, fcSubInfo);
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0004F4E7 File Offset: 0x0004D6E7
		public static void ClearFailureCategoryInfo(NameValueCollection httpHeaders)
		{
			httpHeaders.Remove("X-RemotePS-FailureCategory");
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
		public static string GetFailureCategoryInfo(HttpContext context)
		{
			if (!string.IsNullOrEmpty(context.Response.Headers["X-RemotePS-FailureCategory"]))
			{
				return context.Response.Headers["X-RemotePS-FailureCategory"];
			}
			if (context.Items["LiveIdBasicAuthResult"] != null && string.Compare(context.Items["LiveIdBasicAuthResult"].ToString(), LiveIdAuthResult.Success.ToString()) != 0)
			{
				return string.Format("{0}-{1}", FailureCategory.LiveID, context.Items["LiveIdBasicAuthResult"]);
			}
			return null;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0004F5A8 File Offset: 0x0004D7A8
		public static void SetWSManFailureCategory(NameValueCollection httpHeaders, string wsmanFaultMessage)
		{
			if (string.IsNullOrEmpty(wsmanFaultMessage) || wsmanFaultMessage.Contains("FailureCategory"))
			{
				return;
			}
			string fcSubInfo = "Others";
			string text = WinRMInfo.knownWSManError.Keys.FirstOrDefault((string key) => wsmanFaultMessage.Contains(key));
			if (!string.IsNullOrEmpty(text))
			{
				fcSubInfo = WinRMInfo.knownWSManError[text];
			}
			WinRMInfo.SetFailureCategoryInfo(httpHeaders, FailureCategory.WSMan, fcSubInfo);
		}

		// Token: 0x04000EE7 RID: 3815
		private const string ActionHeaderKey = "X-RemotePs-Action";

		// Token: 0x04000EE8 RID: 3816
		private const string RawActionHeaderKey = "X-RemotePs-RawAction";

		// Token: 0x04000EE9 RID: 3817
		private const string SessionIdHeaderKey = "X-RemotePs-SessionId";

		// Token: 0x04000EEA RID: 3818
		private const string ShellIdHeaderKey = "X-RemotePs-ShellId";

		// Token: 0x04000EEB RID: 3819
		private const string CommandIdHeaderKey = "X-RemotePs-CommandId";

		// Token: 0x04000EEC RID: 3820
		private const string CommandNameHeaderKey = "X-RemotePs-CommandName";

		// Token: 0x04000EED RID: 3821
		private const string SignalCodeHeaderKey = "X-RemotePs-SignalCode";

		// Token: 0x04000EEE RID: 3822
		private const string UuidPrefix = "uuid:";

		// Token: 0x04000EEF RID: 3823
		internal const string PingHeaderKey = "X-RemotePS-Ping";

		// Token: 0x04000EF0 RID: 3824
		internal const string RevisedActionHeaderKey = "X-RemotePS-RevisedAction";

		// Token: 0x04000EF1 RID: 3825
		internal const string WinRMInfoSessionItemKey = "X-RemotePS-WinRMInfo";

		// Token: 0x04000EF2 RID: 3826
		internal const string FailureCategoryItemKey = "X-RemotePS-FailureCategory";

		// Token: 0x04000EF3 RID: 3827
		internal const string NewPSSessionAction = "http://schemas.xmlsoap.org/ws/2004/09/transfer/Create";

		// Token: 0x04000EF4 RID: 3828
		internal const string CommandAction = "http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Command";

		// Token: 0x04000EF5 RID: 3829
		internal const string SignalAction = "http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Signal";

		// Token: 0x04000EF6 RID: 3830
		internal const string TerminateSignalCode = "http://schemas.microsoft.com/wbem/wsman/1/windows/shell/signal/terminate";

		// Token: 0x04000EF7 RID: 3831
		internal const string KnownPingActionName = "Ping";

		// Token: 0x04000EF8 RID: 3832
		internal const string KnownNonPingActionName = "Non-Ping";

		// Token: 0x04000EF9 RID: 3833
		internal const string KnownPossiblePingActionName = "Possible-Ping";

		// Token: 0x04000EFA RID: 3834
		internal const string KnownTerminateActionName = "Terminate";

		// Token: 0x04000EFB RID: 3835
		internal const string KnownReceiveActionName = "Receive";

		// Token: 0x04000EFC RID: 3836
		internal const string KnownCommandReceiveActionName = "Command:Receive";

		// Token: 0x04000EFD RID: 3837
		internal const string KnownNewPSSessionActionName = "New-PSSession";

		// Token: 0x04000EFE RID: 3838
		internal const string KnownRemovePSSessionActionName = "Remove-PSSession";

		// Token: 0x04000EFF RID: 3839
		internal const string DiagnosticsInfoFmt = "[Server={0},RequestId={1},TimeStamp={2}] ";

		// Token: 0x04000F00 RID: 3840
		internal const string CafeDiagnosticsInfoFmt = "[ClientAccessServer={0},BackEndServer={1},RequestId={2},TimeStamp={3}] ";

		// Token: 0x04000F01 RID: 3841
		internal const string FailureCategoryInfoFmt = "[FailureCategory={0}] ";

		// Token: 0x04000F02 RID: 3842
		internal const string LiveIdBasicAuthResultKey = "LiveIdBasicAuthResult";

		// Token: 0x04000F03 RID: 3843
		internal static Dictionary<string, string> KnownActions = new Dictionary<string, string>
		{
			{
				"http://schemas.xmlsoap.org/ws/2004/09/transfer/Create",
				"New-PSSession"
			},
			{
				"http://schemas.xmlsoap.org/ws/2004/09/transfer/Delete",
				"Remove-PSSession"
			},
			{
				"http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Receive",
				"Receive"
			},
			{
				"http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Send",
				"Send"
			},
			{
				"http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Command",
				"Command"
			},
			{
				"http://schemas.microsoft.com/wbem/wsman/1/windows/shell/Signal",
				"Signal"
			}
		};

		// Token: 0x04000F04 RID: 3844
		private static Dictionary<string, string> knownWSManError = new Dictionary<string, string>
		{
			{
				"because the shell was not found on the server.",
				"InvalidShellID"
			},
			{
				"Access is denied.",
				"AccessIsDenied"
			},
			{
				"Unable to load assembly",
				"UnableToLoadAssembly"
			},
			{
				"The supplied command context is not valid.",
				"InvalidCommandContext"
			},
			{
				"The WS-Management service cannot complete the operation within the time specified in OperationTimeout.",
				"OperationTimeout"
			},
			{
				"identifier does not give a valid initial session state on the remote computer.",
				"InvalidInitialSessionState"
			},
			{
				"The supplied shell context is not valid",
				"InvalidShellContext"
			},
			{
				"The Windows Remote Shell received a request to perform an operation on a command identifier that does not exist.",
				"NonExistCommandID"
			},
			{
				"The I/O operation has been aborted because of either a thread exit or an application request.",
				"IOOperationAborted"
			},
			{
				"PowerShell plugin operation is shutting down. This may happen if the hosting service or application is shutting down.",
				"PluginOperationShutDown"
			},
			{
				"A device attached to the system is not functioning.",
				"DeviceNotFunctioning"
			},
			{
				"The operation was canceled by the user.",
				"OperationCancelled"
			},
			{
				"WinRM cannot process the request because the input XML contains a syntax error.",
				"XMLSyntaxError"
			},
			{
				"The WS-Management service cannot process the request. Operations involving multiple messages from the client are not supported.",
				"MultipleClientMessages"
			}
		};
	}
}
