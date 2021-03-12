using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A1F RID: 2591
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ForwardCalendarEventRequest : BaseRequest
	{
		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x001022BF File Offset: 0x001004BF
		// (set) Token: 0x06004915 RID: 18709 RVA: 0x001022C7 File Offset: 0x001004C7
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x001022D0 File Offset: 0x001004D0
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x001022D8 File Offset: 0x001004D8
		[DataMember]
		public ItemId EventId { get; set; }

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x001022E1 File Offset: 0x001004E1
		// (set) Token: 0x06004919 RID: 18713 RVA: 0x001022E9 File Offset: 0x001004E9
		public ForwardEventParameters Parameters { get; set; }

		// Token: 0x0600491A RID: 18714 RVA: 0x001022F4 File Offset: 0x001004F4
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.EventId == null)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.EventId);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForItemId);
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x0010231F File Offset: 0x0010051F
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
