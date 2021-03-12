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
	// Token: 0x02000038 RID: 56
	[Cmdlet("New", "EdgeSyncServiceConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewEdgeSyncServiceConfig : NewFixedNameSystemConfigurationObjectTask<EdgeSyncServiceConfig>
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007547 File Offset: 0x00005747
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000755E File Offset: 0x0000575E
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00007571 File Offset: 0x00005771
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000757E File Offset: 0x0000577E
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConfigurationSyncInterval
		{
			get
			{
				return this.DataObject.ConfigurationSyncInterval;
			}
			set
			{
				this.DataObject.ConfigurationSyncInterval = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000758C File Offset: 0x0000578C
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00007599 File Offset: 0x00005799
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan RecipientSyncInterval
		{
			get
			{
				return this.DataObject.RecipientSyncInterval;
			}
			set
			{
				this.DataObject.RecipientSyncInterval = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000075A7 File Offset: 0x000057A7
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000075B4 File Offset: 0x000057B4
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan LockDuration
		{
			get
			{
				return this.DataObject.LockDuration;
			}
			set
			{
				this.DataObject.LockDuration = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000075C2 File Offset: 0x000057C2
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000075CF File Offset: 0x000057CF
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan LockRenewalDuration
		{
			get
			{
				return this.DataObject.LockRenewalDuration;
			}
			set
			{
				this.DataObject.LockRenewalDuration = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000075DD File Offset: 0x000057DD
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000075EA File Offset: 0x000057EA
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan OptionDuration
		{
			get
			{
				return this.DataObject.OptionDuration;
			}
			set
			{
				this.DataObject.OptionDuration = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000075F8 File Offset: 0x000057F8
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00007605 File Offset: 0x00005805
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan CookieValidDuration
		{
			get
			{
				return this.DataObject.CookieValidDuration;
			}
			set
			{
				this.DataObject.CookieValidDuration = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00007613 File Offset: 0x00005813
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00007620 File Offset: 0x00005820
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan FailoverDCInterval
		{
			get
			{
				return this.DataObject.FailoverDCInterval;
			}
			set
			{
				this.DataObject.FailoverDCInterval = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000762E File Offset: 0x0000582E
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000763B File Offset: 0x0000583B
		[Parameter(Mandatory = false)]
		public bool LogEnabled
		{
			get
			{
				return this.DataObject.LogEnabled;
			}
			set
			{
				this.DataObject.LogEnabled = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00007649 File Offset: 0x00005849
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00007656 File Offset: 0x00005856
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan LogMaxAge
		{
			get
			{
				return this.DataObject.LogMaxAge;
			}
			set
			{
				this.DataObject.LogMaxAge = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007664 File Offset: 0x00005864
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00007671 File Offset: 0x00005871
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> LogMaxDirectorySize
		{
			get
			{
				return this.DataObject.LogMaxDirectorySize;
			}
			set
			{
				this.DataObject.LogMaxDirectorySize = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000767F File Offset: 0x0000587F
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x0000768C File Offset: 0x0000588C
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> LogMaxFileSize
		{
			get
			{
				return this.DataObject.LogMaxFileSize;
			}
			set
			{
				this.DataObject.LogMaxFileSize = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000769A File Offset: 0x0000589A
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000076A7 File Offset: 0x000058A7
		[Parameter(Mandatory = false)]
		public EdgeSyncLoggingLevel LogLevel
		{
			get
			{
				return this.DataObject.LogLevel;
			}
			set
			{
				this.DataObject.LogLevel = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000076B5 File Offset: 0x000058B5
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x000076C2 File Offset: 0x000058C2
		[Parameter(Mandatory = false)]
		public string LogPath
		{
			get
			{
				return this.DataObject.LogPath;
			}
			set
			{
				this.DataObject.LogPath = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000076D0 File Offset: 0x000058D0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.Site == null)
				{
					return Strings.ConfirmationMessageNewEdgeSyncServiceConfigOnLocalSite;
				}
				return Strings.ConfirmationMessageNewEdgeSyncServiceConfigWithSiteSpecified(this.Site.ToString());
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000076F0 File Offset: 0x000058F0
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 213, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\NewEdgeSyncServiceConfig.cs");
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007714 File Offset: 0x00005914
		protected override IConfigurable PrepareDataObject()
		{
			base.PrepareDataObject();
			this.DataObject.Name = "EdgeSyncService";
			if (this.Site == null)
			{
				this.siteObject = ((ITopologyConfigurationSession)base.DataSession).GetLocalSite();
				if (this.siteObject == null)
				{
					base.WriteError(new NeedToSpecifyADSiteObjectException(), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			else
			{
				this.siteObject = (ADSite)base.GetDataObject<ADSite>(this.Site, base.DataSession, null, new LocalizedString?(Strings.ErrorSiteNotFound(this.Site.ToString())), new LocalizedString?(Strings.ErrorSiteNotUnique(this.Site.ToString())));
			}
			ADObjectId childId = this.siteObject.Id.GetChildId("EdgeSyncService");
			this.DataObject.SetId(childId);
			return this.DataObject;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000077E4 File Offset: 0x000059E4
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!EdgeSyncServiceConfig.ValidLogSizeCompatibility(this.LogMaxFileSize, this.LogMaxDirectorySize, this.siteObject.Id, (ITopologyConfigurationSession)base.DataSession))
			{
				base.WriteError(new InvalidOperationException(), ErrorCategory.InvalidOperation, this.DataObject);
			}
		}

		// Token: 0x04000095 RID: 149
		private const string DefaultCommonName = "EdgeSyncService";

		// Token: 0x04000096 RID: 150
		private ADSite siteObject;
	}
}
