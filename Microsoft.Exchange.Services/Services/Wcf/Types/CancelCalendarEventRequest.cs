using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A17 RID: 2583
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CancelCalendarEventRequest : BaseRequest
	{
		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x00101F4A File Offset: 0x0010014A
		// (set) Token: 0x060048E5 RID: 18661 RVA: 0x00101F52 File Offset: 0x00100152
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x00101F5B File Offset: 0x0010015B
		// (set) Token: 0x060048E7 RID: 18663 RVA: 0x00101F63 File Offset: 0x00100163
		[DataMember]
		public BaseItemId EventId { get; set; }

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x00101F6C File Offset: 0x0010016C
		// (set) Token: 0x060048E9 RID: 18665 RVA: 0x00101F74 File Offset: 0x00100174
		public CancelEventParameters Parameters { get; set; }

		// Token: 0x060048EA RID: 18666 RVA: 0x00101F80 File Offset: 0x00100180
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.EventId == null)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.EventId);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForItemId);
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x00101FAB File Offset: 0x001001AB
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.EventId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemId(callContext, this.EventId);
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x00101FC3 File Offset: 0x001001C3
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CancelCalendarEvent(callContext, this);
		}
	}
}
