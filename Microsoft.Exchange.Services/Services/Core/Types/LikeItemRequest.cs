using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000462 RID: 1122
	[KnownType(typeof(ItemId))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LikeItemRequest : BaseRequest
	{
		// Token: 0x06002104 RID: 8452 RVA: 0x000A2155 File Offset: 0x000A0355
		public LikeItemRequest(ItemId itemId, bool isUnlike = false)
		{
			this.ItemId = itemId;
			this.IsUnlike = isUnlike;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002105 RID: 8453 RVA: 0x000A216B File Offset: 0x000A036B
		// (set) Token: 0x06002106 RID: 8454 RVA: 0x000A2173 File Offset: 0x000A0373
		[DataMember(Name = "ItemId", IsRequired = true)]
		public ItemId ItemId { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x000A217C File Offset: 0x000A037C
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x000A2184 File Offset: 0x000A0384
		[DataMember(Name = "IsUnlike")]
		public bool IsUnlike { get; set; }

		// Token: 0x06002109 RID: 8457 RVA: 0x000A218D File Offset: 0x000A038D
		internal override void Validate()
		{
			base.Validate();
			if (this.ItemId == null)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdEmpty), FaultParty.Sender);
			}
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000A21B3 File Offset: 0x000A03B3
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForItemId(true, callContext, this.ItemId);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000A21C4 File Offset: 0x000A03C4
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemIdList(callContext, new ItemId[]
			{
				this.ItemId
			});
		}
	}
}
