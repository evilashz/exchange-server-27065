using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000287 RID: 647
	internal class ProxyPSCommand
	{
		// Token: 0x0600164B RID: 5707 RVA: 0x00054272 File Offset: 0x00052472
		public ProxyPSCommand(RemoteConnectionInfo connectionInfo, PSCommand cmd, Task.TaskWarningLoggingDelegate writeWarning) : this(connectionInfo, cmd, false, writeWarning)
		{
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00054280 File Offset: 0x00052480
		public ProxyPSCommand(RemoteConnectionInfo connectionInfo, PSCommand cmd, bool asyncInvoke, Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			if (cmd == null)
			{
				throw new ArgumentNullException("cmd");
			}
			RemoteRunspaceFactory remoteRunspaceFactory = new RemoteRunspaceFactory(new RunspaceConfigurationFactory(), null, connectionInfo);
			this.runspaceMediator = new RunspaceMediator(remoteRunspaceFactory, new BasicRunspaceCache(1));
			this.cmd = cmd;
			this.asyncInvoke = asyncInvoke;
			this.writeWarning = writeWarning;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000542E4 File Offset: 0x000524E4
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x000542EB File Offset: 0x000524EB
		public static Func<RunspaceProxy, PSCommand, IPowerShellProxy> PowerShellProxyFactory
		{
			get
			{
				return ProxyPSCommand.powerShellProxyFactory;
			}
			set
			{
				ProxyPSCommand.powerShellProxyFactory = value;
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x000542F4 File Offset: 0x000524F4
		public IEnumerable<PSObject> Invoke()
		{
			if (!this.asyncInvoke)
			{
				Collection<PSObject> result = null;
				using (RunspaceProxy runspaceProxy = new RunspaceProxy(this.runspaceMediator, true))
				{
					IPowerShellProxy powerShellProxy = ProxyPSCommand.PowerShellProxyFactory(runspaceProxy, this.cmd);
					result = powerShellProxy.Invoke<PSObject>();
					if (powerShellProxy.Errors != null && powerShellProxy.Errors.Count != 0 && powerShellProxy.Errors[0].Exception != null)
					{
						throw powerShellProxy.Errors[0].Exception;
					}
					if (powerShellProxy.Warnings != null && powerShellProxy.Warnings.Count != 0)
					{
						foreach (WarningRecord warningRecord in powerShellProxy.Warnings)
						{
							this.writeWarning(new LocalizedString(warningRecord.Message));
						}
					}
				}
				return result;
			}
			return new CmdletProxyDataReader(this.runspaceMediator, this.cmd, this.writeWarning);
		}

		// Token: 0x040006CF RID: 1743
		private RunspaceMediator runspaceMediator;

		// Token: 0x040006D0 RID: 1744
		private PSCommand cmd;

		// Token: 0x040006D1 RID: 1745
		private readonly bool asyncInvoke;

		// Token: 0x040006D2 RID: 1746
		private readonly Task.TaskWarningLoggingDelegate writeWarning;

		// Token: 0x040006D3 RID: 1747
		private static Func<RunspaceProxy, PSCommand, IPowerShellProxy> powerShellProxyFactory = (RunspaceProxy runspace, PSCommand cmd) => new PowerShellProxy(runspace, cmd);
	}
}
