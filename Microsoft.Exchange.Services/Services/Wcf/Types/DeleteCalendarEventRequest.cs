using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A1B RID: 2587
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteCalendarEventRequest : BaseRequest
	{
		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x00102135 File Offset: 0x00100335
		// (set) Token: 0x060048FE RID: 18686 RVA: 0x0010213D File Offset: 0x0010033D
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x00102146 File Offset: 0x00100346
		// (set) Token: 0x06004900 RID: 18688 RVA: 0x0010214E File Offset: 0x0010034E
		[DataMember]
		public ItemId EventId { get; set; }

		// Token: 0x06004901 RID: 18689 RVA: 0x00102158 File Offset: 0x00100358
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.EventId == null)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.EventId);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForItemId);
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00102183 File Offset: 0x00100383
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.EventId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemId(callContext, this.EventId);
		}
	}
}
