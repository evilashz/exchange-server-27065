using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A21 RID: 2593
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarEventRequest : BaseRequest
	{
		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x00102387 File Offset: 0x00100587
		// (set) Token: 0x06004921 RID: 18721 RVA: 0x0010238F File Offset: 0x0010058F
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x00102398 File Offset: 0x00100598
		// (set) Token: 0x06004923 RID: 18723 RVA: 0x001023A0 File Offset: 0x001005A0
		[DataMember]
		public ItemId[] EventIds { get; set; }

		// Token: 0x06004924 RID: 18724 RVA: 0x001023AC File Offset: 0x001005AC
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.EventIds == null || this.EventIds.Length < taskStep)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.EventIds[taskStep]);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForItemId);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x001023E4 File Offset: 0x001005E4
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.EventIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemIdList(callContext, this.EventIds);
		}
	}
}
