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
	// Token: 0x02000032 RID: 50
	[Cmdlet("Get", "EdgeSyncEhfConnector", DefaultParameterSetName = "Identity")]
	public sealed class GetEdgeSyncEhfConnector : GetSystemConfigurationObjectTask<EdgeSyncEhfConnectorIdParameter, EdgeSyncEhfConnector>
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006922 File Offset: 0x00004B22
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006939 File Offset: 0x00004B39
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000694C File Offset: 0x00004B4C
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000694F File Offset: 0x00004B4F
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006957 File Offset: 0x00004B57
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 71, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\EdgeSync\\GetEdgeSyncEHFConnector.cs");
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006978 File Offset: 0x00004B78
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

		// Token: 0x04000088 RID: 136
		private ADObjectId rootId;
	}
}
