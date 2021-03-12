using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A1D RID: 2589
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExpandCalendarEventRequest : BaseRequest
	{
		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x001021EB File Offset: 0x001003EB
		// (set) Token: 0x06004908 RID: 18696 RVA: 0x001021F3 File Offset: 0x001003F3
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06004909 RID: 18697 RVA: 0x001021FC File Offset: 0x001003FC
		// (set) Token: 0x0600490A RID: 18698 RVA: 0x00102204 File Offset: 0x00100404
		[DataMember]
		public ItemId EventId { get; set; }

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x0600490B RID: 18699 RVA: 0x0010220D File Offset: 0x0010040D
		// (set) Token: 0x0600490C RID: 18700 RVA: 0x00102215 File Offset: 0x00100415
		public ExpandEventParameters Parameters { get; set; }

		// Token: 0x0600490D RID: 18701 RVA: 0x00102220 File Offset: 0x00100420
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.EventId == null)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForItemId = BaseRequest.GetServerInfoForItemId(callContext, this.EventId);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForItemId);
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x0010224B File Offset: 0x0010044B
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
