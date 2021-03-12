using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A19 RID: 2585
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateCalendarEventRequest : BaseRequest
	{
		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x0010201B File Offset: 0x0010021B
		// (set) Token: 0x060048F2 RID: 18674 RVA: 0x00102023 File Offset: 0x00100223
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x0010202C File Offset: 0x0010022C
		// (set) Token: 0x060048F4 RID: 18676 RVA: 0x00102034 File Offset: 0x00100234
		public Event[] Events { get; set; }

		// Token: 0x060048F5 RID: 18677 RVA: 0x00102040 File Offset: 0x00100240
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			BaseServerIdInfo baseServerIdInfo = (this.CalendarId == null || this.CalendarId.BaseFolderId == null) ? null : BaseRequest.GetServerInfoForFolderId(callContext, this.CalendarId.BaseFolderId);
			return BaseRequest.ServerInfosToResourceKeys(true, new BaseServerIdInfo[]
			{
				baseServerIdInfo
			});
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x00102089 File Offset: 0x00100289
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.CalendarId != null)
			{
				return BaseRequest.GetServerInfoForFolderId(callContext, this.CalendarId.BaseFolderId);
			}
			return null;
		}
	}
}
