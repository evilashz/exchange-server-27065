using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000438 RID: 1080
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class EdgeSyncServiceConfig : ADContainer
	{
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x000C3220 File Offset: 0x000C1420
		public string SiteName
		{
			get
			{
				if (string.IsNullOrEmpty(this.siteName))
				{
					string[] array = this.Identity.ToString().Split(new char[]
					{
						'/'
					});
					if (array.Length == 5)
					{
						this.siteName = array[3];
					}
				}
				return this.siteName;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000C326D File Offset: 0x000C146D
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x000C327F File Offset: 0x000C147F
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ConfigurationSyncInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.ConfigurationSyncInterval];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.ConfigurationSyncInterval] = value;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000C3292 File Offset: 0x000C1492
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x000C32A4 File Offset: 0x000C14A4
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan RecipientSyncInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.RecipientSyncInterval];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.RecipientSyncInterval] = value;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000C32B7 File Offset: 0x000C14B7
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000C32C9 File Offset: 0x000C14C9
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan LockDuration
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.LockDuration];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LockDuration] = value;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x000C32DC File Offset: 0x000C14DC
		// (set) Token: 0x06003083 RID: 12419 RVA: 0x000C32EE File Offset: 0x000C14EE
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan LockRenewalDuration
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.LockRenewalDuration];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LockRenewalDuration] = value;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000C3301 File Offset: 0x000C1501
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x000C3313 File Offset: 0x000C1513
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan OptionDuration
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.OptionDuration];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.OptionDuration] = value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003086 RID: 12422 RVA: 0x000C3326 File Offset: 0x000C1526
		// (set) Token: 0x06003087 RID: 12423 RVA: 0x000C3338 File Offset: 0x000C1538
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan CookieValidDuration
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.CookieValidDuration];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.CookieValidDuration] = value;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000C334B File Offset: 0x000C154B
		// (set) Token: 0x06003089 RID: 12425 RVA: 0x000C335D File Offset: 0x000C155D
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan FailoverDCInterval
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.FailoverDCInterval];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.FailoverDCInterval] = value;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x000C3370 File Offset: 0x000C1570
		// (set) Token: 0x0600308B RID: 12427 RVA: 0x000C3382 File Offset: 0x000C1582
		[Parameter(Mandatory = false)]
		public bool LogEnabled
		{
			get
			{
				return (bool)this[EdgeSyncServiceConfigSchema.LogEnabled];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LogEnabled] = value;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000C3395 File Offset: 0x000C1595
		// (set) Token: 0x0600308D RID: 12429 RVA: 0x000C33A7 File Offset: 0x000C15A7
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan LogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)this[EdgeSyncServiceConfigSchema.LogMaxAge];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LogMaxAge] = value;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x000C33BA File Offset: 0x000C15BA
		// (set) Token: 0x0600308F RID: 12431 RVA: 0x000C33CC File Offset: 0x000C15CC
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> LogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[EdgeSyncServiceConfigSchema.LogMaxDirectorySize];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LogMaxDirectorySize] = value;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000C33DF File Offset: 0x000C15DF
		// (set) Token: 0x06003091 RID: 12433 RVA: 0x000C33F1 File Offset: 0x000C15F1
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> LogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[EdgeSyncServiceConfigSchema.LogMaxFileSize];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LogMaxFileSize] = value;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000C3404 File Offset: 0x000C1604
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x000C3416 File Offset: 0x000C1616
		[Parameter(Mandatory = false)]
		public EdgeSyncLoggingLevel LogLevel
		{
			get
			{
				return (EdgeSyncLoggingLevel)this[EdgeSyncServiceConfigSchema.LogLevel];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LogLevel] = value;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000C3429 File Offset: 0x000C1629
		// (set) Token: 0x06003095 RID: 12437 RVA: 0x000C343B File Offset: 0x000C163B
		[Parameter(Mandatory = false)]
		public string LogPath
		{
			get
			{
				return (string)this[EdgeSyncServiceConfigSchema.LogPath];
			}
			set
			{
				this[EdgeSyncServiceConfigSchema.LogPath] = value;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000C3449 File Offset: 0x000C1649
		internal override ADObjectSchema Schema
		{
			get
			{
				return EdgeSyncServiceConfig.schema;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000C3450 File Offset: 0x000C1650
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchEdgeSyncServiceConfig";
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000C3458 File Offset: 0x000C1658
		internal static bool ValidLogSizeCompatibility(Unlimited<ByteQuantifiedSize> logMaxFileSize, Unlimited<ByteQuantifiedSize> logMaxDirectorySize, ADObjectId siteId, ITopologyConfigurationSession session)
		{
			ByteQuantifiedSize value = new ByteQuantifiedSize(2147483647UL);
			if ((!logMaxFileSize.IsUnlimited && logMaxFileSize.Value > value) || (!logMaxDirectorySize.IsUnlimited && logMaxDirectorySize.Value > value))
			{
				AndFilter filter = new AndFilter(new QueryFilter[]
				{
					new BitMaskAndFilter(ServerSchema.CurrentServerRole, 32UL),
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, siteId)
				});
				foreach (Server server in session.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0))
				{
					ServerVersion a = new ServerVersion(server.VersionNumber);
					if (ServerVersion.Compare(a, EdgeSyncServiceConfig.LogSizeBreakageVersion) < 0)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x04002099 RID: 8345
		private const string MostDerivedObjectClassInternal = "msExchEdgeSyncServiceConfig";

		// Token: 0x0400209A RID: 8346
		private const char CommonNameSeperatorChar = '/';

		// Token: 0x0400209B RID: 8347
		public const string CommonName = "EdgeSyncService";

		// Token: 0x0400209C RID: 8348
		private static readonly ServerVersion LogSizeBreakageVersion = new ServerVersion(14, 1, 187, 0);

		// Token: 0x0400209D RID: 8349
		private static readonly EdgeSyncServiceConfigSchema schema = ObjectSchema.GetInstance<EdgeSyncServiceConfigSchema>();

		// Token: 0x0400209E RID: 8350
		private string siteName;
	}
}
