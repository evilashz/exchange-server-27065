using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Management.Tasks.UM;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001F9 RID: 505
	public static class ErrorHandlingUtil
	{
		// Token: 0x0600266C RID: 9836 RVA: 0x00076EE8 File Offset: 0x000750E8
		static ErrorHandlingUtil()
		{
			string value = WebConfigurationManager.AppSettings["ShowDebugInformation"];
			ErrorHandlingUtil.ShowDebugInformation = (string.IsNullOrEmpty(value) ? ShowDebugInforamtion.None : ((ShowDebugInforamtion)Enum.Parse(typeof(ShowDebugInforamtion), value, true)));
			ErrorHandlingUtil.AddSourceToErrorMessages = ConfigUtil.ReadBool("AddSourceToErrorMessages", false);
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x0600266D RID: 9837 RVA: 0x0007728B File Offset: 0x0007548B
		// (set) Token: 0x0600266E RID: 9838 RVA: 0x00077292 File Offset: 0x00075492
		public static ShowDebugInforamtion ShowDebugInformation { get; private set; }

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x0600266F RID: 9839 RVA: 0x0007729A File Offset: 0x0007549A
		// (set) Token: 0x06002670 RID: 9840 RVA: 0x000772A1 File Offset: 0x000754A1
		public static bool AddSourceToErrorMessages { get; private set; }

		// Token: 0x06002671 RID: 9841 RVA: 0x000772A9 File Offset: 0x000754A9
		public static bool CanShowDebugInfo(Exception exception)
		{
			return ErrorHandlingUtil.ShowDebugInformation == ShowDebugInforamtion.All || (ErrorHandlingUtil.ShowDebugInformation == ShowDebugInforamtion.Unknown && !ErrorHandlingUtil.KnownExceptionList.Contains(exception.GetType()) && !ErrorHandlingUtil.KnownReflectedExceptions.Value.ContainsValue(exception.GetType()));
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000772E8 File Offset: 0x000754E8
		public static void TransferToErrorPage(string cause)
		{
			ErrorHandlingModule.TransferToErrorPage(cause, false);
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000772F4 File Offset: 0x000754F4
		public static void ShowServerError(string errorString, string detailString, System.Web.UI.Page page)
		{
			ModalDialog current = ModalDialog.GetCurrent(page);
			current.ShowDialog(ClientStrings.Error, errorString, string.Empty, ModalDialogType.Error);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0007731A File Offset: 0x0007551A
		public static void ShowServerErrors(ErrorInformationBase[] errorInfos, System.Web.UI.Page page)
		{
			if (errorInfos != null && errorInfos.Length > 0)
			{
				ErrorHandlingUtil.ShowServerErrors(errorInfos.ToInfos(), page);
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00077334 File Offset: 0x00075534
		public static void ShowServerError(InfoCore errorInfo, System.Web.UI.Page page)
		{
			if (errorInfo != null)
			{
				ErrorHandlingUtil.ShowServerErrors(new InfoCore[]
				{
					errorInfo
				}, page);
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x00077388 File Offset: 0x00075588
		public static void ShowServerErrors(InfoCore[] errorInfos, System.Web.UI.Page page)
		{
			if (errorInfos != null && errorInfos.Length > 0)
			{
				Dictionary<string, InfoCore> dictionary = new Dictionary<string, InfoCore>();
				Array.ForEach<InfoCore>(errorInfos, delegate(InfoCore x)
				{
					if (!dictionary.ContainsKey(x.Message))
					{
						dictionary.Add(x.Message, x);
					}
				});
				ModalDialog current = ModalDialog.GetCurrent(page);
				current.ShowDialog(dictionary.Values.ToArray<InfoCore>());
			}
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000773E0 File Offset: 0x000755E0
		public static void SendReportForCriticalException(HttpContext context, Exception exception)
		{
			if (exception.IsUICriticalException() && exception.IsControlPanelException())
			{
				ExWatson.AddExtraData(ErrorHandlingUtil.GetEcpWatsonExtraData(context, exception));
				EcpPerfCounters.SendWatson.Increment();
				RbacPrincipal rbacPrincipal = context.User as RbacPrincipal;
				if (rbacPrincipal != null && ExTraceConfiguration.Instance.InMemoryTracingEnabled)
				{
					TroubleshootingContext troubleshootingContext = rbacPrincipal.RbacConfiguration.TroubleshootingContext;
					troubleshootingContext.SendTroubleshootingReportWithTraces(exception, ErrorHandlingUtil.GetEcpWatsonTitle(exception, context));
					return;
				}
				ExWatson.SendReport(exception, ReportOptions.ReportTerminateAfterSend, exception.GetCustomMessage());
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00077460 File Offset: 0x00075660
		public static string GetEcpWatsonExtraData(HttpContext context, Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (context != null && context.Request != null)
			{
				stringBuilder.Append("context.Request.Url = ");
				stringBuilder.AppendLine(context.GetRequestUrlForLog());
				if (context.HasTargetTenant() || context.IsExplicitSignOn())
				{
					stringBuilder.Append("RawUrl = ");
					stringBuilder.AppendLine(context.Request.RawUrl);
				}
			}
			if (context != null && context.Request.Cookies != null)
			{
				HttpCookie httpCookie = context.Request.Cookies["TCMID"];
				if (httpCookie != null)
				{
					stringBuilder.Append("TestCaseID: ");
					stringBuilder.AppendLine(httpCookie.Value);
				}
			}
			foreach (object obj in ex.Data.Keys)
			{
				object obj2 = ex.Data[obj];
				stringBuilder.Append(obj);
				stringBuilder.Append(": ");
				stringBuilder.AppendLine((obj2 == null) ? "null" : obj2.ToString());
			}
			if (ex is OutOfMemoryException)
			{
				stringBuilder.Append("Managed Memory: ").AppendLine(GC.GetTotalMemory(false).ToString());
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					stringBuilder.Append("Private Bytes: ").AppendLine(currentProcess.PrivateMemorySize64.ToString());
					stringBuilder.Append("Working Set: ").AppendLine(currentProcess.WorkingSet64.ToString());
				}
				NativeMethods.MEMORYSTATUSEX memorystatusex = default(NativeMethods.MEMORYSTATUSEX);
				memorystatusex.dwLength = (uint)Marshal.SizeOf(memorystatusex);
				if (NativeMethods.GlobalMemoryStatusEx(ref memorystatusex))
				{
					stringBuilder.Append("Available Physical Memory: ").Append(memorystatusex.ullAvailPhys.ToString()).Append("/").Append(memorystatusex.ullTotalPhys.ToString()).Append("(").Append((memorystatusex.ullAvailPhys * 100UL / memorystatusex.ullTotalPhys).ToString()).AppendLine("%)");
					stringBuilder.Append("Available Virtual Memory: ").Append(memorystatusex.ullAvailVirtual.ToString()).Append("/").Append(memorystatusex.ullTotalPhys.ToString()).Append("(").Append((memorystatusex.ullAvailVirtual * 100UL / memorystatusex.ullTotalVirtual).ToString()).AppendLine("%)");
				}
				stringBuilder.AppendLine("**Top 5 Apps:**");
				foreach (Process process in (from x in Process.GetProcesses()
				orderby x.PrivateMemorySize64 descending
				select x).Take(5))
				{
					using (process)
					{
						stringBuilder.Append(process.ProcessName).Append("#").Append(process.Id.ToString()).Append(": ").AppendLine(process.PrivateMemorySize64.ToString());
					}
				}
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append("Couldn't extract extra watson data from the context or exception.");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0007780C File Offset: 0x00075A0C
		public static string GetEcpWatsonTitle(Exception exception, HttpContext context)
		{
			string str;
			if (exception.TargetSite != null && exception.TargetSite.ReflectedType != null && exception.TargetSite.ReflectedType.FullName != null && exception.TargetSite.Name != null)
			{
				str = exception.TargetSite.ReflectedType.FullName + "." + exception.TargetSite.Name;
			}
			else
			{
				str = "unknown function";
			}
			return str + " " + context.GetRequestUrlAbsolutePath();
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0007789C File Offset: 0x00075A9C
		private static Dictionary<string, Type> InitializeKnownReflectedExceptions()
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
			foreach (string text in ErrorHandlingUtil.ReflectedExceptionDefinitions)
			{
				try
				{
					Type type = Type.GetType(text);
					if (null != type)
					{
						dictionary.Add(text, type);
					}
				}
				catch (ArgumentException)
				{
				}
				catch (TypeLoadException)
				{
				}
				catch (FileLoadException)
				{
				}
				catch (BadImageFormatException)
				{
				}
			}
			return dictionary;
		}

		// Token: 0x04001F67 RID: 8039
		public const string EcpErrorHeaderName = "X-ECP-ERROR";

		// Token: 0x04001F68 RID: 8040
		private static readonly Type[] KnownExceptionList = new Type[]
		{
			typeof(ParameterBindingException),
			typeof(DataValidationException),
			typeof(WLCDDomainException),
			typeof(ManagementObjectNotFoundException),
			typeof(ADNoSuchObjectException),
			typeof(ShouldContinueException),
			typeof(MapiObjectNotFoundException),
			typeof(ADInvalidPasswordException),
			typeof(ADObjectAlreadyExistsException),
			typeof(MapiObjectAlreadyExistsException),
			typeof(TrackingSearchException),
			typeof(ADScopeException),
			typeof(SecurityException),
			typeof(SelfMemberAlreadyExistsException),
			typeof(SelfMemberNotFoundException),
			typeof(DefaultPinGenerationException),
			typeof(UMRecipient),
			typeof(UMRpcException),
			typeof(ExtensionNotUniqueException),
			typeof(RpcUMServerNotFoundException),
			typeof(GlobalRoutingEntryNotFoundException),
			typeof(InvalidOperationForGetUMMailboxPinException),
			typeof(WeakPinException),
			typeof(InvalidExtensionException),
			typeof(ExtensionNotUniqueException),
			typeof(UserAlreadyUmEnabledException),
			typeof(InvalidSipNameResourceIdException),
			typeof(InvalidE164ResourceIdException),
			typeof(CannotDeleteAssociatedMailboxPolicyException),
			typeof(InvalidUMFaxServerURIValue),
			typeof(UMMailboxPolicyValidationException),
			typeof(UnsupportedCustomGreetingWaveFormatException),
			typeof(UnsupportedCustomGreetingWmaFormatException),
			typeof(UnsupportedCustomGreetingWmaFormatException),
			typeof(UnsupportedCustomGreetingSizeFormatException),
			typeof(UnsupportedCustomGreetingLegacyFormatException),
			typeof(DialPlanAssociatedWithPoliciesException),
			typeof(UnableToCreateGatewayObjectException),
			typeof(AutoAttendantExistsException),
			typeof(SIPResouceIdConflictWithExistingValue),
			typeof(InvalidDtmfFallbackAutoAttendantException),
			typeof(InvalidAutoAttendantException),
			typeof(InvalidCustomMenuException),
			typeof(UserAlreadyUmDisabledException),
			typeof(ProxyAddressExistsException),
			typeof(OverBudgetException),
			typeof(AdUserNotFoundException),
			typeof(NonUniqueRecipientException),
			typeof(ServerNotInSiteException),
			typeof(LowVersionUserDeniedException),
			typeof(CmdletAccessDeniedException),
			typeof(UrlNotFoundOrNoAccessException),
			typeof(BadRequestException),
			typeof(BadQueryParameterException),
			typeof(ProxyCantFindCasServerException),
			typeof(CasServerNotSupportEsoException)
		};

		// Token: 0x04001F69 RID: 8041
		internal static Lazy<Dictionary<string, Type>> KnownReflectedExceptions = new Lazy<Dictionary<string, Type>>(new Func<Dictionary<string, Type>>(ErrorHandlingUtil.InitializeKnownReflectedExceptions));

		// Token: 0x04001F6A RID: 8042
		private static readonly string[] ReflectedExceptionDefinitions = new string[]
		{
			"Microsoft.Exchange.Hygiene.Security.Authorization.NoValidRolesAssociatedToUserException, Microsoft.Exchange.Hygiene.Security.Authorization"
		};

		// Token: 0x04001F6B RID: 8043
		public static readonly bool ShowIisNativeErrorPage = ConfigUtil.ReadBool("ShowIisNativeErrorPage", false);
	}
}
