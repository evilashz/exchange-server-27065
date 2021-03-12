using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A2C RID: 2604
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarViewRequest : BaseRequest
	{
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06004976 RID: 18806 RVA: 0x001028A2 File Offset: 0x00100AA2
		// (set) Token: 0x06004977 RID: 18807 RVA: 0x001028AA File Offset: 0x00100AAA
		public TargetFolderId CalendarId { get; set; }

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x001028B3 File Offset: 0x00100AB3
		// (set) Token: 0x06004979 RID: 18809 RVA: 0x001028BB File Offset: 0x00100ABB
		public ExDateTime StartRange { get; set; }

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x001028C4 File Offset: 0x00100AC4
		// (set) Token: 0x0600497B RID: 18811 RVA: 0x001028CC File Offset: 0x00100ACC
		public ExDateTime EndRange { get; set; }

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x001028D5 File Offset: 0x00100AD5
		// (set) Token: 0x0600497D RID: 18813 RVA: 0x001028DD File Offset: 0x00100ADD
		public bool? ReturnMasterItems { get; set; }

		// Token: 0x0600497E RID: 18814 RVA: 0x001028E8 File Offset: 0x00100AE8
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.CalendarId == null)
			{
				return null;
			}
			BaseServerIdInfo serverInfoForFolderId = BaseRequest.GetServerInfoForFolderId(callContext, this.CalendarId.BaseFolderId);
			return BaseRequest.ServerInfoToResourceKeys(false, serverInfoForFolderId);
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x00102918 File Offset: 0x00100B18
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.CalendarId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.CalendarId.BaseFolderId);
		}
	}
}
