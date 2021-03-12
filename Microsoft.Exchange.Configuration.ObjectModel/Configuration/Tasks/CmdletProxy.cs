using System;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.Diagnostics.Components.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000282 RID: 642
	internal static class CmdletProxy
	{
		// Token: 0x0600160D RID: 5645 RVA: 0x00052978 File Offset: 0x00050B78
		public static void ThrowExceptionIfProxyIsNeeded(TaskContext context, ADUser user, bool shouldAsyncProxy, LocalizedString confirmationMessage, CmdletProxyInfo.ChangeCmdletProxyParametersDelegate changeCmdletProxyParameters)
		{
			if (user == null)
			{
				return;
			}
			int remoteServerVersion;
			string remoteServerForADUser = TaskHelper.GetRemoteServerForADUser(user, new Task.TaskVerboseLoggingDelegate(context.CommandShell.WriteVerbose), out remoteServerVersion);
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(context, remoteServerForADUser, remoteServerVersion, shouldAsyncProxy, confirmationMessage, changeCmdletProxyParameters);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x000529B0 File Offset: 0x00050BB0
		public static void ThrowExceptionIfProxyIsNeeded(TaskContext context, string remoteServerFqdn, int remoteServerVersion, bool shouldAsyncProxy, LocalizedString confirmationMessage, CmdletProxyInfo.ChangeCmdletProxyParametersDelegate changeCmdletProxyParameters)
		{
			if (CmdletProxy.ShouldProxyCmdlet(context, remoteServerFqdn, remoteServerVersion))
			{
				throw new CmdletNeedsProxyException(new CmdletProxyInfo(remoteServerFqdn, remoteServerVersion, shouldAsyncProxy, confirmationMessage, changeCmdletProxyParameters));
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000529D0 File Offset: 0x00050BD0
		public static bool TryToProxyOutputObject(ICmdletProxyable cmdletProxyableObject, TaskContext context, ADUser user, bool shouldAsyncProxy, LocalizedString confirmationMessage, CmdletProxyInfo.ChangeCmdletProxyParametersDelegate changeCmdletProxyParameters)
		{
			if (user == null)
			{
				return false;
			}
			int remoteServerVersion;
			string remoteServerForADUser = TaskHelper.GetRemoteServerForADUser(user, new Task.TaskVerboseLoggingDelegate(context.CommandShell.WriteVerbose), out remoteServerVersion);
			return CmdletProxy.TryToProxyOutputObject(cmdletProxyableObject, context, remoteServerForADUser, remoteServerVersion, shouldAsyncProxy, confirmationMessage, changeCmdletProxyParameters);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00052A0B File Offset: 0x00050C0B
		public static bool TryToProxyOutputObject(ICmdletProxyable cmdletProxyableObject, TaskContext context, string remoteServerFqdn, int remoteServerVersion, bool shouldAsyncProxy, LocalizedString confirmationMessage, CmdletProxyInfo.ChangeCmdletProxyParametersDelegate changeCmdletProxyParameters)
		{
			if (CmdletProxy.ShouldProxyCmdlet(context, remoteServerFqdn, remoteServerVersion))
			{
				cmdletProxyableObject.SetProxyInfo(new CmdletProxyInfo(remoteServerFqdn, remoteServerVersion, shouldAsyncProxy, confirmationMessage, changeCmdletProxyParameters));
				return true;
			}
			return false;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00052A90 File Offset: 0x00050C90
		public static CmdletProxyInfo.ChangeCmdletProxyParametersDelegate AppendIdentityToProxyCmdlet(ADObject adObject)
		{
			return delegate(PropertyBag parameters)
			{
				if (adObject != null && adObject.Id != null)
				{
					if (parameters.Contains("Identity"))
					{
						parameters.Remove("Identity");
					}
					parameters.Add("Identity", adObject.Id.DistinguishedName);
				}
			};
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00052AB6 File Offset: 0x00050CB6
		public static void AppendForceToProxyCmdlet(PropertyBag parameters)
		{
			if (parameters.Contains("Force"))
			{
				parameters.Remove("Force");
			}
			parameters.Add("Force", true);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00052AE4 File Offset: 0x00050CE4
		private static bool ShouldProxyCmdlet(TaskContext context, string remoteServerFqdn, int remoteServerVersion)
		{
			if (string.IsNullOrWhiteSpace(remoteServerFqdn))
			{
				return false;
			}
			if (context.ExchangeRunspaceConfig == null)
			{
				return false;
			}
			if (ExTraceGlobals.FaultInjectionTracer.IsTraceEnabled(TraceType.FaultInjection))
			{
				bool flag = false;
				ProxyHelper.FaultInjection_ShouldForcedlyProxyCmdlet(context.ExchangeRunspaceConfig.ConfigurationSettings.OriginalConnectionUri, remoteServerFqdn, ref flag);
				if (flag)
				{
					return true;
				}
			}
			string localServerFqdn = TaskHelper.GetLocalServerFqdn(null);
			if (string.IsNullOrEmpty(localServerFqdn))
			{
				CmdletLogger.SafeAppendGenericError(context.UniqueId, "ShouldProxyCmdlet", "GetLocalServerFqdn returns null/empty value", false);
				return false;
			}
			if (string.Equals(remoteServerFqdn, localServerFqdn, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (CmdletProxy.CanServerBeProxiedTo(remoteServerVersion))
			{
				return true;
			}
			CmdletLogger.SafeAppendGenericInfo(context.UniqueId, "ShouldProxyCmdlet", string.Format("Remote server version {0} doesn't support be proxied.", ProxyHelper.GetFriendlyVersionInformation(remoteServerVersion)));
			return false;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00052B90 File Offset: 0x00050D90
		private static bool CanServerBeProxiedTo(int serverVersion)
		{
			return serverVersion > CmdletProxy.E14SupportProxyMinimumVersion;
		}

		// Token: 0x040006BA RID: 1722
		private static readonly int E14SupportProxyMinimumVersion = new ServerVersion(14, 3, 79, 0).ToInt();
	}
}
