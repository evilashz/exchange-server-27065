using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B1C RID: 2844
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetModernConversationAttachmentsRequest : BaseRequest
	{
		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x060050AD RID: 20653 RVA: 0x00109CF5 File Offset: 0x00107EF5
		// (set) Token: 0x060050AE RID: 20654 RVA: 0x00109CFD File Offset: 0x00107EFD
		[DataMember(Name = "FoldersToIgnore", IsRequired = false)]
		public BaseFolderId[] FoldersToIgnore { get; set; }

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x060050AF RID: 20655 RVA: 0x00109D06 File Offset: 0x00107F06
		// (set) Token: 0x060050B0 RID: 20656 RVA: 0x00109D0E File Offset: 0x00107F0E
		[DataMember(IsRequired = false)]
		public bool ClientSupportsIrm
		{
			get
			{
				return this.clientSupportsIrm;
			}
			set
			{
				this.clientSupportsIrm = value;
			}
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x060050B1 RID: 20657 RVA: 0x00109D17 File Offset: 0x00107F17
		// (set) Token: 0x060050B2 RID: 20658 RVA: 0x00109D1F File Offset: 0x00107F1F
		[DataMember(Name = "Conversations", IsRequired = true)]
		public ConversationRequestType[] Conversations { get; set; }

		// Token: 0x060050B3 RID: 20659 RVA: 0x00109D28 File Offset: 0x00107F28
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetModernConversationAttachments(callContext, this);
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x00109D31 File Offset: 0x00107F31
		internal override void Validate()
		{
			base.Validate();
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x00109D39 File Offset: 0x00107F39
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.Conversations[0].ConversationId);
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x00109D4E File Offset: 0x00107F4E
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.Conversations == null || taskStep > this.Conversations.Length)
			{
				return null;
			}
			return base.GetResourceKeysForItemId(false, callContext, this.Conversations[taskStep].ConversationId);
		}

		// Token: 0x04002D19 RID: 11545
		private bool clientSupportsIrm;
	}
}
