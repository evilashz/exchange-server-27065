using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200043C RID: 1084
	[KnownType(typeof(ItemId))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetLikersRequest : BaseRequest
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x000A1444 File Offset: 0x0009F644
		// (set) Token: 0x06001FD3 RID: 8147 RVA: 0x000A144C File Offset: 0x0009F64C
		[DataMember(Name = "ItemId", IsRequired = true)]
		public ItemId ItemId { get; set; }

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000A1455 File Offset: 0x0009F655
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000A1458 File Offset: 0x0009F658
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
