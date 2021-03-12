using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CrossServerDiagnostics : ICrossServerDiagnostics
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002E41 File Offset: 0x00001041
		internal static ICrossServerDiagnostics Instance
		{
			get
			{
				return CrossServerDiagnostics.instance;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E48 File Offset: 0x00001048
		private string AppName
		{
			get
			{
				if (this.appName == null)
				{
					this.appName = ExWatson.AppName;
				}
				return this.appName;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002E64 File Offset: 0x00001064
		private string AppVersion
		{
			get
			{
				if (this.appVersion == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						Version version;
						if (ExWatson.TryGetRealApplicationVersion(currentProcess, out version))
						{
							this.appVersion = version.ToString();
						}
						else
						{
							this.appVersion = "0";
						}
					}
				}
				return this.appVersion;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002EC4 File Offset: 0x000010C4
		private string GetSuitableMethodNameFromStackTrace(StackTrace stackTrace)
		{
			StackFrame[] frames = stackTrace.GetFrames();
			int i;
			MethodBase method;
			for (i = 0; i < frames.Length; i++)
			{
				method = frames[i].GetMethod();
				string fullName = method.ReflectedType.FullName;
				if (!fullName.StartsWith("Microsoft.Mapi") && !fullName.StartsWith("Microsoft.Exchange.Data.Storage"))
				{
					break;
				}
			}
			if (i == frames.Length)
			{
				i = 0;
			}
			method = frames[i].GetMethod();
			return method.ReflectedType.FullName + "." + method.Name;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F44 File Offset: 0x00001144
		public void LogInfoWatson(ExRpcConnectionInfo connectionInfo)
		{
			if (!CrossServerConnectionRegistryParameters.IsCrossServerLoggingEnabled())
			{
				return;
			}
			StackTrace stackTrace = new StackTrace(1);
			MethodBase method = stackTrace.GetFrame(0).GetMethod();
			AssemblyName assemblyName = (method.DeclaringType != null) ? method.DeclaringType.Assembly.GetName() : Assembly.GetCallingAssembly().GetName();
			string suitableMethodNameFromStackTrace = this.GetSuitableMethodNameFromStackTrace(stackTrace);
			string normalizedClientInfo = connectionInfo.ApplicationId.GetNormalizedClientInfo();
			int hashCode = (suitableMethodNameFromStackTrace + normalizedClientInfo).GetHashCode();
			string detailedExceptionInformation = string.Format("Connection from {0} to {1} is disallowed. ConnectionInfo: {2}", ExRpcConnectionInfo.GetLocalServerFQDN(), connectionInfo.GetDestinationServerName(), connectionInfo.ToString());
			bool flag;
			ExWatson.SendThrottledGenericWatsonReport("E12", this.AppVersion, this.AppName, assemblyName.Version.ToString(), assemblyName.Name, "IllegalCrossServerConnectionEx4: " + normalizedClientInfo, stackTrace.ToString(), hashCode.ToString("x"), suitableMethodNameFromStackTrace, detailedExceptionInformation, CrossServerConnectionRegistryParameters.GetInfoWatsonThrottlingInterval(), out flag);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000302B File Offset: 0x0000122B
		public void TraceCrossServerCall(string serverDn)
		{
			if (ComponentTrace<MapiNetTags>.IsTraceEnabled(80))
			{
				ComponentTrace<MapiNetTags>.Trace<string, string, string>(36048, 80, 0L, "CrossServerDiagnostics::TraceCrossServerCall. Cross-server call found. serverDn = {0}; machineName = {1}; Call stack:\n{2}", serverDn, Environment.MachineName, Environment.StackTrace);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003054 File Offset: 0x00001254
		public void BlockCrossServerCall(ExRpcConnectionInfo connectionInfo, string mailboxDescription)
		{
			if (CrossServerConnectionRegistryParameters.IsCrossServerBlockEnabled())
			{
				string message;
				if (!string.IsNullOrEmpty(mailboxDescription))
				{
					message = string.Format("{0} mailbox [{1}] with application ID [{2}] is not allowed to make cross-server calls from [{3}] to [{4}]", new object[]
					{
						mailboxDescription,
						connectionInfo.UserName,
						connectionInfo.ApplicationId.GetNormalizedClientInfo(),
						ExRpcConnectionInfo.GetLocalServerFQDN(),
						connectionInfo.ServerDn
					});
				}
				else
				{
					message = string.Format("Mailbox [{0}] with application ID [{1}] is not allowed to make cross-server calls from [{2}] to [{3}]", new object[]
					{
						connectionInfo.UserName,
						connectionInfo.ApplicationId.GetNormalizedClientInfo(),
						ExRpcConnectionInfo.GetLocalServerFQDN(),
						connectionInfo.ServerDn
					});
				}
				throw MapiExceptionHelper.IllegalCrossServerConnection(message);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000030F6 File Offset: 0x000012F6
		public void BlockMonitoringCrossServerCall(ExRpcConnectionInfo connectionInfo)
		{
			if (CrossServerConnectionRegistryParameters.IsCrossServerMonitoringBlockEnabled())
			{
				this.BlockCrossServerCall(connectionInfo, "Monitoring");
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000310B File Offset: 0x0000130B
		public void BlockCrossServerCall(ExRpcConnectionInfo connectionInfo)
		{
			if (CrossServerConnectionRegistryParameters.IsCrossServerBlockEnabled())
			{
				this.BlockCrossServerCall(connectionInfo, null);
			}
		}

		// Token: 0x04000079 RID: 121
		private static ICrossServerDiagnostics instance = new CrossServerDiagnostics();

		// Token: 0x0400007A RID: 122
		private string appName;

		// Token: 0x0400007B RID: 123
		private string appVersion;
	}
}
