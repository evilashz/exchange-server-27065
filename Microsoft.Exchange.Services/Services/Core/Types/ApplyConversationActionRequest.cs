using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003F7 RID: 1015
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ApplyConversationActionType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ApplyConversationActionRequest : BaseRequest
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0009E483 File Offset: 0x0009C683
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x0009E48B File Offset: 0x0009C68B
		[XmlArray(ElementName = "ConversationActions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember]
		[XmlArrayItem("ConversationAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ConversationAction))]
		public ConversationAction[] ConversationActions { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0009E494 File Offset: 0x0009C694
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x0009E49C File Offset: 0x0009C69C
		[XmlIgnore]
		[DataMember(Name = "ReturnMovedItemIds", IsRequired = false, Order = 2)]
		public bool ReturnMovedItemIds { get; set; }

		// Token: 0x06001C85 RID: 7301 RVA: 0x0009E4A5 File Offset: 0x0009C6A5
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ApplyConversationAction(callContext, this);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0009E4AE File Offset: 0x0009C6AE
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForItemId(callContext, this.ConversationActions[0].ConversationId);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0009E4C3 File Offset: 0x0009C6C3
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.ConversationActions == null || taskStep > this.ConversationActions.Length)
			{
				return null;
			}
			return base.GetResourceKeysForItemId(true, callContext, this.ConversationActions[taskStep].ConversationId);
		}

		// Token: 0x040012C9 RID: 4809
		internal const string ConversationActionsElementName = "ConversationActions";
	}
}
