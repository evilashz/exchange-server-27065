using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A3B RID: 2619
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateCalendarEventRequest : BaseRequest
	{
		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x060049F6 RID: 18934 RVA: 0x00102FFA File Offset: 0x001011FA
		// (set) Token: 0x060049F7 RID: 18935 RVA: 0x00103002 File Offset: 0x00101202
		[DataMember]
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x060049F8 RID: 18936 RVA: 0x0010300B File Offset: 0x0010120B
		// (set) Token: 0x060049F9 RID: 18937 RVA: 0x00103013 File Offset: 0x00101213
		public Event[] Events { get; set; }

		// Token: 0x060049FA RID: 18938 RVA: 0x0010301C File Offset: 0x0010121C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			BaseServerIdInfo baseServerIdInfo = (this.CalendarId == null || this.CalendarId.BaseFolderId == null) ? null : BaseRequest.GetServerInfoForFolderId(callContext, this.CalendarId.BaseFolderId);
			return BaseRequest.ServerInfosToResourceKeys(true, new BaseServerIdInfo[]
			{
				baseServerIdInfo
			});
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x00103065 File Offset: 0x00101265
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
