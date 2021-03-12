using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001D9 RID: 473
	internal class MonadRemoteRunspaceFactory : RemoteRunspaceFactory
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x0003458B File Offset: 0x0003278B
		static MonadRemoteRunspaceFactory()
		{
			AppDomain.CurrentDomain.DomainUnload += MonadRemoteRunspaceFactory.AppDomainUnloadEventHandler;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000345BF File Offset: 0x000327BF
		public MonadRemoteRunspaceFactory(MonadConnectionInfo connectionInfo) : this(connectionInfo, null)
		{
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000345C9 File Offset: 0x000327C9
		public MonadRemoteRunspaceFactory(MonadConnectionInfo connectionInfo, RunspaceServerSettingsPresentationObject serverSettings) : base(new RunspaceConfigurationFactory(), MonadHostFactory.GetInstance(), connectionInfo)
		{
			this.clientVersion = connectionInfo.ClientVersion;
			this.serverSettings = serverSettings;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000345F0 File Offset: 0x000327F0
		public static SupportedVersionList TestConnection(Uri uri, string shell, PSCredential credential, AuthenticationMechanism mechanism, int maxRedirectionCount, bool skipCertificateCheck)
		{
			WSManConnectionInfo wsmanConnectionInfo = new WSManConnectionInfo(uri, shell, credential);
			wsmanConnectionInfo.AuthenticationMechanism = mechanism;
			wsmanConnectionInfo.MaximumConnectionRedirectionCount = maxRedirectionCount;
			if (skipCertificateCheck)
			{
				wsmanConnectionInfo.SkipCACheck = true;
				wsmanConnectionInfo.SkipCNCheck = true;
				wsmanConnectionInfo.SkipRevocationCheck = true;
			}
			Runspace runspace = null;
			SupportedVersionList result = null;
			Runspace runspace2;
			runspace = (runspace2 = RunspaceFactory.CreateRunspace(wsmanConnectionInfo));
			try
			{
				lock (MonadRemoteRunspaceFactory.syncObject)
				{
					MonadRemoteRunspaceFactory.runspaceInstances.Add(runspace);
				}
				runspace.Open();
				result = MonadRemoteRunspaceFactory.ExtractSupportedVersionList(runspace);
			}
			finally
			{
				if (runspace2 != null)
				{
					((IDisposable)runspace2).Dispose();
				}
			}
			lock (MonadRemoteRunspaceFactory.syncObject)
			{
				MonadRemoteRunspaceFactory.runspaceInstances.Remove(runspace);
			}
			return result;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000346D8 File Offset: 0x000328D8
		protected override Runspace CreateRunspace(PSHost host)
		{
			Runspace runspace = base.CreateRunspace(host);
			if (!string.IsNullOrEmpty(this.clientVersion))
			{
				SupportedVersionList supportedVersionList = MonadRemoteRunspaceFactory.ExtractSupportedVersionList(runspace);
				if (supportedVersionList.IsSupported(this.clientVersion))
				{
					runspace.Dispose();
					throw new VersionMismatchException(Strings.VersionMismatchDuringCreateRemoteRunspace, supportedVersionList);
				}
			}
			return runspace;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00034724 File Offset: 0x00032924
		internal static void ClearAppDomainRemoteRunspaceConnections()
		{
			lock (MonadRemoteRunspaceFactory.syncObject)
			{
				MonadRemoteRunspaceFactory.accessingRunspaceList = true;
				foreach (Runspace runspace in MonadRemoteRunspaceFactory.runspaceInstances)
				{
					if (runspace.RunspaceStateInfo.State == RunspaceState.Closed)
					{
						if (runspace.RunspaceStateInfo.State == RunspaceState.Closing)
						{
							continue;
						}
					}
					try
					{
						runspace.Close();
						runspace.Dispose();
					}
					catch (Exception)
					{
					}
				}
				MonadRemoteRunspaceFactory.runspaceInstances.Clear();
				MonadRemoteRunspaceFactory.accessingRunspaceList = false;
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000347EC File Offset: 0x000329EC
		protected override void InitializeRunspace(Runspace runspace)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand("ConsoleInitialize.ps1");
			this.ExecuteCommand(pscommand, runspace);
			if (this.serverSettings != null)
			{
				pscommand = new PSCommand();
				pscommand.AddCommand("Set-ADServerSettingsForLogonUser");
				pscommand.AddParameter("RunspaceServerSettings", MonadCommand.Serialize(this.serverSettings));
				this.ExecuteCommand(pscommand, runspace);
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003484C File Offset: 0x00032A4C
		protected override void ConfigureRunspace(Runspace runspace)
		{
			lock (MonadRemoteRunspaceFactory.syncObject)
			{
				MonadRemoteRunspaceFactory.runspaceInstances.Add(runspace);
			}
			base.ConfigureRunspace(runspace);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00034898 File Offset: 0x00032A98
		protected override void OnRunspaceDisposed(Runspace runspace)
		{
			if (!MonadRemoteRunspaceFactory.accessingRunspaceList)
			{
				lock (MonadRemoteRunspaceFactory.syncObject)
				{
					MonadRemoteRunspaceFactory.runspaceInstances.Remove(runspace);
				}
			}
			base.OnRunspaceDisposed(runspace);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000348F0 File Offset: 0x00032AF0
		private static void AppDomainUnloadEventHandler(object sender, EventArgs args)
		{
			MonadRemoteRunspaceFactory.ClearAppDomainRemoteRunspaceConnections();
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000348F8 File Offset: 0x00032AF8
		private void ExecuteCommand(PSCommand command, Runspace runspace)
		{
			using (PowerShell powerShell = PowerShell.Create())
			{
				powerShell.Commands = command;
				powerShell.Runspace = runspace;
				try
				{
					powerShell.Invoke();
				}
				catch (CmdletInvocationException ex)
				{
					if (ex.InnerException != null)
					{
						throw ex.InnerException;
					}
					throw;
				}
				if (powerShell.Streams.Error.Count > 0)
				{
					ErrorRecord errorRecord = powerShell.Streams.Error[0];
					throw new CmdletInvocationException(errorRecord.Exception.Message, errorRecord.Exception);
				}
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00034998 File Offset: 0x00032B98
		private static SupportedVersionList ExtractSupportedVersionList(Runspace newRunspace)
		{
			PSPrimitiveDictionary applicationPrivateData = newRunspace.GetApplicationPrivateData();
			object obj = applicationPrivateData["SupportedVersions"];
			return SupportedVersionList.Parse((obj != null) ? obj.ToString() : string.Empty);
		}

		// Token: 0x040003C5 RID: 965
		private static List<Runspace> runspaceInstances = new List<Runspace>();

		// Token: 0x040003C6 RID: 966
		private static volatile bool accessingRunspaceList = false;

		// Token: 0x040003C7 RID: 967
		private static object syncObject = new object();

		// Token: 0x040003C8 RID: 968
		private string clientVersion;

		// Token: 0x040003C9 RID: 969
		private RunspaceServerSettingsPresentationObject serverSettings;
	}
}
