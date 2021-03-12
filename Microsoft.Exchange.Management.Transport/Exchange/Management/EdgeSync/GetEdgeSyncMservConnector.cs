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
	// Token: 0x02000033 RID: 51
	[Cmdlet("Get", "EdgeSyncMservConnector")]
	public sealed class GetEdgeSyncMservConnector : GetSystemConfigurationObjectTask<EdgeSyncMservConnectorIdParameter, EdgeSyncMservConnector>
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000069F3 File Offset: 0x00004BF3
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00006A0A File Offset: 0x00004C0A
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006A1D File Offset: 0x00004C1D
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006A20 File Offset: 0x00004C20
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006A28 File Offset: 0x00004C28
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 69, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\GetEdgeSyncMservConnector.cs");
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006A48 File Offset: 0x00004C48
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

		// Token: 0x04000089 RID: 137
		private ADObjectId rootId;
	}
}
