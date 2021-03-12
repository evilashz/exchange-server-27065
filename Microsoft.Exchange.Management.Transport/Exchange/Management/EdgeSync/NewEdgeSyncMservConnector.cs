using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000037 RID: 55
	[Cmdlet("New", "EdgeSyncMservConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Simple")]
	public sealed class NewEdgeSyncMservConnector : NewSystemConfigurationObjectTask<EdgeSyncMservConnector>
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000708D File Offset: 0x0000528D
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000070A4 File Offset: 0x000052A4
		[Parameter(ParameterSetName = "Simple", Mandatory = false)]
		[Parameter(ParameterSetName = "Custom", Mandatory = false)]
		public AdSiteIdParameter Site
		{
			get
			{
				return (AdSiteIdParameter)base.Fields["Site"];
			}
			set
			{
				base.Fields["Site"] = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000070B7 File Offset: 0x000052B7
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000070CE File Offset: 0x000052CE
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		public Uri ProvisionUrl
		{
			get
			{
				return (Uri)base.Fields["ProvisionUrl"];
			}
			set
			{
				base.Fields["ProvisionUrl"] = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000070E1 File Offset: 0x000052E1
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000070F8 File Offset: 0x000052F8
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		public Uri SettingUrl
		{
			get
			{
				return (Uri)base.Fields["SettingUrl"];
			}
			set
			{
				base.Fields["SettingUrl"] = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000710B File Offset: 0x0000530B
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00007122 File Offset: 0x00005322
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		public string LocalCertificate
		{
			get
			{
				return (string)base.Fields["LocalCertificate"];
			}
			set
			{
				base.Fields["LocalCertificate"] = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007135 File Offset: 0x00005335
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000714C File Offset: 0x0000534C
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		public string RemoteCertificate
		{
			get
			{
				return (string)base.Fields["RemoteCertificate"];
			}
			set
			{
				base.Fields["RemoteCertificate"] = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000715F File Offset: 0x0000535F
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00007176 File Offset: 0x00005376
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		[Parameter(ParameterSetName = "Simple", Mandatory = true)]
		public string PrimaryLeaseLocation
		{
			get
			{
				return (string)base.Fields["PrimaryLeaseLocation"];
			}
			set
			{
				base.Fields["PrimaryLeaseLocation"] = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007189 File Offset: 0x00005389
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000071A0 File Offset: 0x000053A0
		[Parameter(ParameterSetName = "Simple", Mandatory = true)]
		[Parameter(ParameterSetName = "Custom", Mandatory = true)]
		public string BackupLeaseLocation
		{
			get
			{
				return (string)base.Fields["BackupLeaseLocation"];
			}
			set
			{
				base.Fields["BackupLeaseLocation"] = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000071B3 File Offset: 0x000053B3
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000071D4 File Offset: 0x000053D4
		[Parameter(ParameterSetName = "Custom", Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.Fields["Enabled"] ?? true);
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000071EC File Offset: 0x000053EC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.Site == null)
				{
					return Strings.ConfirmationMessageNewEdgeSyncMservConnectorOnLocalSite;
				}
				return Strings.ConfirmationMessageNewEdgeSyncMservConnectorWithSiteSpecified(this.Site.ToString());
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000720C File Offset: 0x0000540C
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			this.SetID();
			this.SetDefaultValue();
			this.DataObject.SynchronizationProvider = "Microsoft.Exchange.EdgeSync.Mserve.MserveSynchronizationProvider";
			this.DataObject.AssemblyPath = "Microsoft.Exchange.EdgeSync.DatacenterProviders.dll";
			this.DataObject.ProvisionUrl = this.ProvisionUrl;
			this.DataObject.SettingUrl = this.SettingUrl;
			this.DataObject.LocalCertificate = this.LocalCertificate;
			this.DataObject.RemoteCertificate = this.RemoteCertificate;
			this.DataObject.PrimaryLeaseLocation = this.PrimaryLeaseLocation;
			this.DataObject.BackupLeaseLocation = this.BackupLeaseLocation;
			this.DataObject.Enabled = this.Enabled;
			return this.DataObject;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000072CC File Offset: 0x000054CC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsLeaseDirectoryValidPath(this.PrimaryLeaseLocation))
			{
				base.WriteError(new InvalidOperationException(Strings.InvalidPrimaryLeaseLocation), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (!Utils.IsLeaseDirectoryValidPath(this.BackupLeaseLocation))
			{
				base.WriteError(new InvalidOperationException(Strings.InvalidBackupLeaseLocation), ErrorCategory.InvalidOperation, this.DataObject);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000733A File Offset: 0x0000553A
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 251, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\NewEdgeSyncMservConnector.cs");
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000735C File Offset: 0x0000555C
		private void SetDefaultValue()
		{
			try
			{
				ServiceEndpointContainer endpointContainer = ((ITopologyConfigurationSession)base.DataSession).GetEndpointContainer();
				ServiceEndpoint endpoint = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerProvision);
				ServiceEndpoint endpoint2 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerSettings);
				ServiceEndpoint endpoint3 = endpointContainer.GetEndpoint(ServiceEndpointId.DeltaSyncPartnerClientCertificate);
				if (!base.Fields.Contains("ProvisionUrl"))
				{
					this.ProvisionUrl = endpoint.Uri;
				}
				if (!base.Fields.Contains("SettingUrl"))
				{
					this.SettingUrl = endpoint2.Uri;
				}
				if (!base.Fields.Contains("LocalCertificate"))
				{
					this.LocalCertificate = endpoint3.CertificateSubject;
				}
				if (!base.Fields.Contains("RemoteCertificate"))
				{
					this.RemoteCertificate = endpoint.CertificateSubject;
				}
			}
			catch (ServiceEndpointNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.ObjectNotFound, null);
			}
			catch (EndpointContainerNotFoundException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ObjectNotFound, null);
			}
			catch (ADTransientException exception3)
			{
				base.WriteError(exception3, ErrorCategory.NotSpecified, null);
			}
			catch (ADOperationException exception4)
			{
				base.WriteError(exception4, ErrorCategory.NotSpecified, null);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007488 File Offset: 0x00005688
		private void SetID()
		{
			ADSite adsite;
			if (this.Site == null)
			{
				adsite = ((ITopologyConfigurationSession)base.DataSession).GetLocalSite();
				if (adsite == null)
				{
					base.WriteError(new NeedToSpecifyADSiteObjectException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			else
			{
				adsite = (ADSite)base.GetDataObject<ADSite>(this.Site, base.DataSession, null, new LocalizedString?(Strings.ErrorSiteNotFound(this.Site.ToString())), new LocalizedString?(Strings.ErrorSiteNotUnique(this.Site.ToString())));
				if (adsite == null)
				{
					return;
				}
			}
			ADObjectId id = adsite.Id;
			ADObjectId childId = id.GetChildId("EdgeSyncService").GetChildId(this.DataObject.Name);
			this.DataObject.SetId(childId);
		}

		// Token: 0x0400008B RID: 139
		private const string SimpleParameterSetName = "Simple";

		// Token: 0x0400008C RID: 140
		private const string CustomParameterSetName = "Custom";

		// Token: 0x0400008D RID: 141
		private const string SiteParam = "Site";

		// Token: 0x0400008E RID: 142
		private const string ProvisionUrlParam = "ProvisionUrl";

		// Token: 0x0400008F RID: 143
		private const string SettingUrlParam = "SettingUrl";

		// Token: 0x04000090 RID: 144
		private const string LocalCertificateParam = "LocalCertificate";

		// Token: 0x04000091 RID: 145
		private const string RemoteCertificateParam = "RemoteCertificate";

		// Token: 0x04000092 RID: 146
		private const string PrimaryLeaseLocationParam = "PrimaryLeaseLocation";

		// Token: 0x04000093 RID: 147
		private const string BackupLeaseLocationParam = "BackupLeaseLocation";

		// Token: 0x04000094 RID: 148
		private const string EnabledParam = "Enabled";
	}
}
