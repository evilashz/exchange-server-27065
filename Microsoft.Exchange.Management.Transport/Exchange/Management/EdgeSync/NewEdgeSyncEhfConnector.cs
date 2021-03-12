using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.EdgeSync;
using Microsoft.Exchange.EdgeSync.Ehf;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000036 RID: 54
	[Cmdlet("New", "EdgeSyncEhfConnector", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewEdgeSyncEhfConnector : NewSystemConfigurationObjectTask<EdgeSyncEhfConnector>
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006C28 File Offset: 0x00004E28
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006C3F File Offset: 0x00004E3F
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006C52 File Offset: 0x00004E52
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00006C73 File Offset: 0x00004E73
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006C8B File Offset: 0x00004E8B
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00006CA2 File Offset: 0x00004EA2
		[Parameter(Mandatory = true)]
		public Uri ProvisioningUrl
		{
			get
			{
				return (Uri)base.Fields["ProvisioningUrl"];
			}
			set
			{
				base.Fields["ProvisioningUrl"] = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00006CB5 File Offset: 0x00004EB5
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00006CCC File Offset: 0x00004ECC
		[Parameter(Mandatory = true)]
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006CDF File Offset: 0x00004EDF
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00006CF6 File Offset: 0x00004EF6
		[Parameter(Mandatory = true)]
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

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006D09 File Offset: 0x00004F09
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00006D20 File Offset: 0x00004F20
		[Parameter]
		public PSCredential AuthenticationCredential
		{
			get
			{
				return (PSCredential)base.Fields["AuthenticationCredential"];
			}
			set
			{
				base.Fields["AuthenticationCredential"] = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006D33 File Offset: 0x00004F33
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00006D4A File Offset: 0x00004F4A
		[Parameter(Mandatory = true)]
		public string ResellerId
		{
			get
			{
				return (string)base.Fields["ResellerId"];
			}
			set
			{
				base.Fields["ResellerId"] = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006D5D File Offset: 0x00004F5D
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00006D7E File Offset: 0x00004F7E
		[Parameter(Mandatory = false)]
		public bool AdminSyncEnabled
		{
			get
			{
				return (bool)(base.Fields["AdminSyncEnabled"] ?? false);
			}
			set
			{
				base.Fields["AdminSyncEnabled"] = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006D96 File Offset: 0x00004F96
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00006DB7 File Offset: 0x00004FB7
		[Parameter(Mandatory = false)]
		public int Version
		{
			get
			{
				return (int)(base.Fields["Version"] ?? 1);
			}
			set
			{
				base.Fields["Version"] = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006DCF File Offset: 0x00004FCF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.Site == null)
				{
					return Strings.ConfirmationMessageNewEdgeSyncEhfConnectorOnLocalSite;
				}
				return Strings.ConfirmationMessageNewEdgeSyncEhfConnectorWithSiteSpecified(this.Site.ToString());
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006DEF File Offset: 0x00004FEF
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 151, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\NewEdgeSyncEHFConnector.cs");
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006E14 File Offset: 0x00005014
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			this.SetID();
			this.DataObject.PrimaryLeaseLocation = this.PrimaryLeaseLocation;
			this.DataObject.BackupLeaseLocation = this.BackupLeaseLocation;
			this.DataObject.AuthenticationCredential = this.AuthenticationCredential;
			this.DataObject.Enabled = this.Enabled;
			this.DataObject.ProvisioningUrl = this.ProvisioningUrl;
			this.DataObject.ResellerId = this.ResellerId;
			this.DataObject.SynchronizationProvider = "Microsoft.Exchange.EdgeSync.Ehf.EhfSynchronizationProvider";
			this.DataObject.AssemblyPath = "Microsoft.Exchange.EdgeSync.DatacenterProviders.dll";
			this.DataObject.AdminSyncEnabled = this.AdminSyncEnabled;
			this.DataObject.Version = this.Version;
			return this.DataObject;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006EDC File Offset: 0x000050DC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!Utils.IsLeaseDirectoryValidPath(this.PrimaryLeaseLocation))
			{
				base.WriteError(new ArgumentException(Strings.InvalidPrimaryLeaseLocation), ErrorCategory.InvalidArgument, this.DataObject);
			}
			if (!Utils.IsLeaseDirectoryValidPath(this.BackupLeaseLocation))
			{
				base.WriteError(new ArgumentException(Strings.InvalidBackupLeaseLocation), ErrorCategory.InvalidArgument, this.DataObject);
			}
			try
			{
				EhfSynchronizationProvider.ValidateProvisioningUrl(this.ProvisioningUrl, this.AuthenticationCredential, base.Name);
			}
			catch (ExDirectoryException ex)
			{
				base.WriteError(ex.InnerException ?? ex, ErrorCategory.InvalidArgument, this.DataObject);
			}
			if (this.Enabled)
			{
				EdgeSyncEhfConnector edgeSyncEhfConnector = Utils.FindEnabledEhfSyncConnector((IConfigurationSession)base.DataSession, null);
				if (edgeSyncEhfConnector != null)
				{
					base.WriteError(new ArgumentException(Strings.EnabledEhfConnectorAlreadyExists(edgeSyncEhfConnector.DistinguishedName)), ErrorCategory.InvalidArgument, this.DataObject);
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006FD0 File Offset: 0x000051D0
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
	}
}
