using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A31 RID: 2609
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RespondToCalendarEventRequest : BaseRequest
	{
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x00102A3F File Offset: 0x00100C3F
		// (set) Token: 0x0600499C RID: 18844 RVA: 0x00102A47 File Offset: 0x00100C47
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600499D RID: 18845 RVA: 0x00102A50 File Offset: 0x00100C50
		// (set) Token: 0x0600499E RID: 18846 RVA: 0x00102A58 File Offset: 0x00100C58
		[DataMember]
		public ItemId EventId { get; set; }

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x00102A61 File Offset: 0x00100C61
		// (set) Token: 0x060049A0 RID: 18848 RVA: 0x00102A69 File Offset: 0x00100C69
		public RespondToEventParameters Parameters { get; set; }

		// Token: 0x060049A1 RID: 18849 RVA: 0x00102A74 File Offset: 0x00100C74
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.EventId == null)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.EventId);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForItemId);
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x00102A9F File Offset: 0x00100C9F
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
