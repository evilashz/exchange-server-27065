using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.ServiceHost.Common.Powershell
{
	// Token: 0x02000015 RID: 21
	internal class LocalPowerShellProvider : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003D8A File Offset: 0x00001F8A
		public LocalPowerShellProvider() : this(TopologyProvider.LocalForestFqdn)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003D98 File Offset: 0x00001F98
		public LocalPowerShellProvider(string partitionFqdn)
		{
			this.disposeTracker = this.GetDisposeTracker();
			RunspaceMediator runspaceMediator = new RunspaceMediator(new RunspaceFactory(new LocalSessionStateFactory(), new LocalRunspaceHostFactory()), new EmptyRunspaceCache());
			this.runspaceProxy = new RunspaceProxy(runspaceMediator);
			this.runspaceProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, RunspaceServerSettings.CreateRunspaceServerSettings(partitionFqdn, false));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public Collection<PSObject> ExecuteCommand(PSCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			Collection<PSObject> result = null;
			PowerShellProxy powerShellProxy = new PowerShellProxy(this.runspaceProxy, command);
			lock (powerShellProxy)
			{
				result = powerShellProxy.Invoke<PSObject>();
				StringBuilder stringBuilder = new StringBuilder();
				List<Exception> list = null;
				if (powerShellProxy.Errors != null)
				{
					int num = 0;
					foreach (ErrorRecord errorRecord in powerShellProxy.Errors)
					{
						if (num != 0)
						{
							stringBuilder.Append("\r\n");
						}
						stringBuilder.Append(string.Format("ErrorRecord{0}={1}", ++num, (errorRecord.Exception != null) ? errorRecord.Exception.ToString() : errorRecord.ToString()));
						if (errorRecord.Exception != null)
						{
							if (list == null)
							{
								list = new List<Exception>();
							}
							list.Add(errorRecord.Exception);
						}
					}
				}
				string text = stringBuilder.ToString();
				if (!text.Equals(string.Empty))
				{
					if (list != null)
					{
						throw new AggregateException(text, list);
					}
					throw new AggregateException(text);
				}
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003F38 File Offset: 0x00002138
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LocalPowerShellProvider>(this);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003F40 File Offset: 0x00002140
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003F5C File Offset: 0x0000215C
		public void Dispose()
		{
			if (this.runspaceProxy != null)
			{
				this.runspaceProxy.Close();
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
		}

		// Token: 0x04000051 RID: 81
		private readonly RunspaceProxy runspaceProxy;

		// Token: 0x04000052 RID: 82
		private DisposeTracker disposeTracker;
	}
}
