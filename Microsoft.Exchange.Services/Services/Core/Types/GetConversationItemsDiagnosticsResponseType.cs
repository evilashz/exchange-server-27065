using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005DC RID: 1500
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ConversationItemsDiagnostics")]
	[Serializable]
	public class GetConversationItemsDiagnosticsResponseType
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000B1E1D File Offset: 0x000B001D
		// (set) Token: 0x06002D2F RID: 11567 RVA: 0x000B1E25 File Offset: 0x000B0025
		[DataMember(Name = "ConversationId", IsRequired = true, Order = 1)]
		public ItemId ConversationId { get; set; }

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000B1E2E File Offset: 0x000B002E
		// (set) Token: 0x06002D31 RID: 11569 RVA: 0x000B1E36 File Offset: 0x000B0036
		[DataMember(Name = "Recipients", IsRequired = true, Order = 2)]
		public SingleRecipientType[] Recipients { get; set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x000B1E3F File Offset: 0x000B003F
		// (set) Token: 0x06002D33 RID: 11571 RVA: 0x000B1E64 File Offset: 0x000B0064
		[DataMember(Name = "ConversationNodeMetadatum", EmitDefaultValue = false, Order = 3)]
		public ConversationNodeMetadata[] ConversationNodeMetadatum
		{
			get
			{
				if (this.conversationNodeMetadatum != null && this.conversationNodeMetadatum.Count > 0)
				{
					return this.conversationNodeMetadatum.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.conversationNodeMetadatum = null;
					return;
				}
				this.conversationNodeMetadatum = new List<ConversationNodeMetadata>(value);
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x000B1E82 File Offset: 0x000B0082
		// (set) Token: 0x06002D35 RID: 11573 RVA: 0x000B1E8A File Offset: 0x000B008A
		[DataMember(Name = "CanDelete", IsRequired = true, Order = 4)]
		public bool CanDelete { get; set; }

		// Token: 0x04001B19 RID: 6937
		private List<ConversationNodeMetadata> conversationNodeMetadatum;
	}
}
