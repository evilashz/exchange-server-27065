using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200099D RID: 2461
	[Cmdlet("Get", "ClientAccessArray", DefaultParameterSetName = "Identity")]
	public sealed class GetClientAccessArray : GetSystemConfigurationObjectTask<ClientAccessArrayIdParameter, ClientAccessArray>
	{
		// Token: 0x06005803 RID: 22531 RVA: 0x0016F994 File Offset: 0x0016DB94
		public GetClientAccessArray()
		{
			this.arrayTaskCommon = new ClientAccessArrayTaskHelper(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError));
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06005804 RID: 22532 RVA: 0x0016F9BF File Offset: 0x0016DBBF
		// (set) Token: 0x06005805 RID: 22533 RVA: 0x0016F9D6 File Offset: 0x0016DBD6
		[Parameter(Mandatory = false)]
		public AdSiteIdParameter Site
		{
			get
			{
				return (AdSiteIdParameter)base.Fields[ClientAccessArraySchema.Site];
			}
			set
			{
				base.Fields[ClientAccessArraySchema.Site] = value;
			}
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x06005806 RID: 22534 RVA: 0x0016F9E9 File Offset: 0x0016DBE9
		protected override ObjectId RootId
		{
			get
			{
				return ClientAccessArray.GetParentContainer((ITopologyConfigurationSession)this.ConfigurationSession);
			}
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x0016F9FC File Offset: 0x0016DBFC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.siteId = ((this.Site != null) ? this.arrayTaskCommon.GetADSite(this.Site, (ITopologyConfigurationSession)this.ConfigurationSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADSite>)).Id : null);
			TaskLogger.LogExit();
		}

		// Token: 0x06005808 RID: 22536 RVA: 0x0016FA58 File Offset: 0x0016DC58
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			ClientAccessArray clientAccessArray = (ClientAccessArray)dataObject;
			if (clientAccessArray.IsPriorTo15ExchangeObjectVersion)
			{
				Server[] cachedServers = ExchangeRpcClientAccess.GetAllPossibleServers((ITopologyConfigurationSession)this.ConfigurationSession, this.siteId).ToArray<Server>();
				ExchangeRpcClientAccess[] all = ExchangeRpcClientAccess.GetAll((ITopologyConfigurationSession)this.ConfigurationSession);
				clientAccessArray.CompleteAllCalculatedProperties(cachedServers, all);
			}
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x06005809 RID: 22537 RVA: 0x0016FAD0 File Offset: 0x0016DCD0
		protected override QueryFilter InternalFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					this.arrayTaskCommon.GetSiteFilter(this.siteId)
				});
			}
		}

		// Token: 0x040032B0 RID: 12976
		private readonly ClientAccessArrayTaskHelper arrayTaskCommon;

		// Token: 0x040032B1 RID: 12977
		private ADObjectId siteId;
	}
}
