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
	// Token: 0x02000034 RID: 52
	[Cmdlet("Get", "EdgeSyncServiceConfig")]
	public sealed class GetEdgeSyncServiceConfig : GetSystemConfigurationObjectTask<EdgeSyncServiceConfigIdParameter, EdgeSyncServiceConfig>
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006AC3 File Offset: 0x00004CC3
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00006ADA File Offset: 0x00004CDA
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006AED File Offset: 0x00004CED
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006AF0 File Offset: 0x00004CF0
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006AF8 File Offset: 0x00004CF8
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 67, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\GetEdgeSyncServiceConfig.cs");
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006B18 File Offset: 0x00004D18
		protected override void InternalValidate()
		{
			if (this.Site != null)
			{
				ADSite adsite = (ADSite)base.GetDataObject<ADSite>(this.Site, base.DataSession, null, new LocalizedString?(Strings.ErrorSiteNotFound(this.Site.ToString())), new LocalizedString?(Strings.ErrorSiteNotUnique(this.Site.ToString())));
				if (base.HasErrors)
				{
					return;
				}
				this.rootId = adsite.Id;
			}
			base.InternalValidate();
		}

		// Token: 0x0400008A RID: 138
		private ADObjectId rootId;
	}
}
