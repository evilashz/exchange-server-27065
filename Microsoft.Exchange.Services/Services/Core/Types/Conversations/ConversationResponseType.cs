using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types.Conversations
{
	// Token: 0x0200068D RID: 1677
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Conversation")]
	[Serializable]
	public class ConversationResponseType : IConversationDataResponse, IConversationResponseType
	{
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000B8547 File Offset: 0x000B6747
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x000B854F File Offset: 0x000B674F
		[DataMember(Name = "ConversationId", IsRequired = true, Order = 1)]
		[XmlElement("ConversationId", IsNullable = false)]
		public ItemId ConversationId { get; set; }

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000B8558 File Offset: 0x000B6758
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000B8560 File Offset: 0x000B6760
		[IgnoreDataMember]
		[XmlElement(DataType = "base64Binary")]
		public byte[] SyncState { get; set; }

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000B856C File Offset: 0x000B676C
		// (set) Token: 0x06003346 RID: 13126 RVA: 0x000B858B File Offset: 0x000B678B
		[XmlIgnore]
		[DataMember(Name = "SyncState", EmitDefaultValue = false, Order = 2)]
		public string SyncStateString
		{
			get
			{
				byte[] syncState = this.SyncState;
				if (syncState == null)
				{
					return null;
				}
				return Convert.ToBase64String(syncState);
			}
			set
			{
				this.SyncState = (string.IsNullOrEmpty(value) ? null : Convert.FromBase64String(value));
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000B85A4 File Offset: 0x000B67A4
		// (set) Token: 0x06003348 RID: 13128 RVA: 0x000B85C9 File Offset: 0x000B67C9
		[XmlArrayItem("ConversationNode", typeof(ConversationNode), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "ConversationNodes", EmitDefaultValue = false, Order = 3)]
		public ConversationNode[] ConversationNodes
		{
			get
			{
				if (this.conversationNodes != null && this.conversationNodes.Count > 0)
				{
					return this.conversationNodes.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.conversationNodes = null;
					return;
				}
				this.conversationNodes = new List<ConversationNode>(value);
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000B85E7 File Offset: 0x000B67E7
		// (set) Token: 0x0600334A RID: 13130 RVA: 0x000B85EF File Offset: 0x000B67EF
		[XmlIgnore]
		[DataMember(Name = "TotalConversationNodesCount", IsRequired = false, Order = 4)]
		public int TotalConversationNodesCount { get; set; }

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600334B RID: 13131 RVA: 0x000B85F8 File Offset: 0x000B67F8
		// (set) Token: 0x0600334C RID: 13132 RVA: 0x000B861D File Offset: 0x000B681D
		[DataMember(Name = "ToRecipients", EmitDefaultValue = false, Order = 6)]
		[XmlIgnore]
		public EmailAddressWrapper[] ToRecipients
		{
			get
			{
				if (this.toRecipients != null && this.toRecipients.Count > 0)
				{
					return this.toRecipients.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.toRecipients = null;
					return;
				}
				this.toRecipients = new List<EmailAddressWrapper>(value);
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000B863B File Offset: 0x000B683B
		// (set) Token: 0x0600334E RID: 13134 RVA: 0x000B8660 File Offset: 0x000B6860
		[DataMember(Name = "CcRecipients", EmitDefaultValue = false, Order = 7)]
		[XmlIgnore]
		public EmailAddressWrapper[] CcRecipients
		{
			get
			{
				if (this.ccRecipients != null && this.ccRecipients.Count > 0)
				{
					return this.ccRecipients.ToArray();
				}
				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.ccRecipients = null;
					return;
				}
				this.ccRecipients = new List<EmailAddressWrapper>(value);
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000B867E File Offset: 0x000B687E
		// (set) Token: 0x06003350 RID: 13136 RVA: 0x000B8686 File Offset: 0x000B6886
		[DataMember(Name = "LastModifiedTime", IsRequired = false, Order = 8)]
		[XmlIgnore]
		[DateTimeString]
		public string LastModifiedTime { get; set; }

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000B868F File Offset: 0x000B688F
		// (set) Token: 0x06003352 RID: 13138 RVA: 0x000B8697 File Offset: 0x000B6897
		[XmlIgnore]
		[DataMember(Name = "CanDelete", IsRequired = false, Order = 9)]
		public bool CanDelete { get; set; }

		// Token: 0x04001CFE RID: 7422
		private List<ConversationNode> conversationNodes;

		// Token: 0x04001CFF RID: 7423
		private List<EmailAddressWrapper> toRecipients;

		// Token: 0x04001D00 RID: 7424
		private List<EmailAddressWrapper> ccRecipients;
	}
}
