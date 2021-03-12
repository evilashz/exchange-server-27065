using System;
using System.DirectoryServices.ActiveDirectory;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C0 RID: 2496
	[Cmdlet("New", "ExchangeServer")]
	public sealed class NewExchangeServer : NewFixedNameSystemConfigurationObjectTask<Server>
	{
		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x060058EE RID: 22766 RVA: 0x00174BAA File Offset: 0x00172DAA
		// (set) Token: 0x060058EF RID: 22767 RVA: 0x00174BB7 File Offset: 0x00172DB7
		[Parameter]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x00174BC8 File Offset: 0x00172DC8
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.DataSession;
			this.DataObject.Edition = ServerEditionType.StandardEvaluation;
			this.DataObject.AdminDisplayVersion = ConfigurationContext.Setup.InstalledVersion;
			this.DataObject.VersionNumber = SystemConfigurationTasksHelper.GenerateVersionNumber(ConfigurationContext.Setup.InstalledVersion);
			this.DataObject.MailboxRelease = MailboxRelease.E15;
			if (string.IsNullOrEmpty(this.Name))
			{
				string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
				int num = localComputerFqdn.IndexOf('.');
				this.DataObject.Name = ((num == -1) ? localComputerFqdn : localComputerFqdn.Substring(0, num));
				NewExchangeServer.TcpNetworkAddress value = new NewExchangeServer.TcpNetworkAddress(NetworkProtocol.TcpIP, localComputerFqdn);
				this.DataObject.NetworkAddress = new NetworkAddressCollection(value);
			}
			this.DataObject.FaultZone = "FaultZone1";
			ADObjectId childId = topologyConfigurationSession.GetAdministrativeGroupId().GetChildId("Servers");
			ADObjectId childId2 = childId.GetChildId(this.DataObject.Name);
			this.DataObject.SetId(childId2);
			this.DataObject.ExchangeLegacyDN = LegacyDN.GenerateLegacyDN(Server.GetParentLegacyDN(topologyConfigurationSession), this.DataObject);
			using (RegistryKey registryKey = RegistryUtil.OpenRemoteBaseKey(RegistryHive.LocalMachine, NativeHelpers.GetLocalComputerFqdn(true)))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(NewExchangeServer.EdgeKeyName))
				{
					if (registryKey2 == null && this.IsDomainJoined())
					{
						this.SetServerSiteInformation(topologyConfigurationSession);
					}
				}
			}
			return this.DataObject;
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x00174D50 File Offset: 0x00172F50
		private void SetServerSiteInformation(ITopologyConfigurationSession scSession)
		{
			if (scSession == null)
			{
				throw new ArgumentNullException("scSession");
			}
			Server dataObject = this.DataObject;
			if (dataObject == null)
			{
				this.WriteWarning(Strings.ServerObjectIsNullWarning);
				return;
			}
			if (dataObject.ServerSite != null)
			{
				base.WriteVerbose(Strings.ServerSiteInformationAlreadySet(dataObject.ServerSite.Name));
				return;
			}
			string siteName = NativeHelpers.GetSiteName(false);
			if (!string.IsNullOrEmpty(siteName))
			{
				string format = "CN={0},CN=Sites,{1}";
				ADObjectId configurationNamingContext = scSession.GetConfigurationNamingContext();
				if (configurationNamingContext != null)
				{
					ADObjectId serverSite = new ADObjectId(string.Format(format, siteName, configurationNamingContext.DistinguishedName));
					dataObject.ServerSite = serverSite;
					return;
				}
			}
			else
			{
				base.WriteVerbose(Strings.LocalSiteNameIsEmpty);
			}
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x00174DE8 File Offset: 0x00172FE8
		private bool IsDomainJoined()
		{
			bool result = true;
			try
			{
				using (Domain.GetComputerDomain())
				{
				}
			}
			catch (ActiveDirectoryObjectNotFoundException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x00174E30 File Offset: 0x00173030
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				ADObjectId orgContainerId = ((IConfigurationSession)base.DataSession).GetOrgContainerId();
				ADObjectId childId = orgContainerId.GetChildId(NewExchangeServer.adminGroupContainer).GetChildId(NewExchangeServer.adminGroup).GetChildId(NewExchangeServer.serversContainer);
				ADObjectId childId2 = childId.GetChildId(this.DataObject.Name);
				ProtocolsContainer protocolsContainer = new ProtocolsContainer();
				ADObjectId childId3 = childId2.GetChildId(NewExchangeServer.protocolsContainer);
				protocolsContainer.SetId(childId3);
				base.DataSession.Save(protocolsContainer);
				SmtpContainer smtpContainer = new SmtpContainer();
				ADObjectId childId4 = childId3.GetChildId(NewExchangeServer.smtpContainer);
				smtpContainer.SetId(childId4);
				base.DataSession.Save(smtpContainer);
			}
		}

		// Token: 0x040032FB RID: 13051
		private static readonly string adminGroupContainer = "Administrative Groups";

		// Token: 0x040032FC RID: 13052
		private static readonly string serversContainer = "Servers";

		// Token: 0x040032FD RID: 13053
		private static readonly string protocolsContainer = "Protocols";

		// Token: 0x040032FE RID: 13054
		private static readonly string smtpContainer = "SMTP";

		// Token: 0x040032FF RID: 13055
		private static readonly string adminGroup = AdministrativeGroup.DefaultName;

		// Token: 0x04003300 RID: 13056
		private static readonly string EdgeKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole";

		// Token: 0x020009C1 RID: 2497
		internal class TcpNetworkAddress : NetworkAddress
		{
			// Token: 0x060058F5 RID: 22773 RVA: 0x00174F21 File Offset: 0x00173121
			public TcpNetworkAddress(NetworkProtocol protocol, string address) : base(protocol, address)
			{
			}
		}
	}
}
