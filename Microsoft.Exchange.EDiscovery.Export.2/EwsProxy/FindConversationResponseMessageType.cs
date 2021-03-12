using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200025F RID: 607
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class FindConversationResponseMessageType : ResponseMessageType
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00026DC6 File Offset: 0x00024FC6
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x00026DCE File Offset: 0x00024FCE
		[XmlArrayItem("Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ConversationType[] Conversations
		{
			get
			{
				return this.conversationsField;
			}
			set
			{
				this.conversationsField = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00026DD7 File Offset: 0x00024FD7
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x00026DDF File Offset: 0x00024FDF
		[XmlArrayItem("Term", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public HighlightTermType[] HighlightTerms
		{
			get
			{
				return this.highlightTermsField;
			}
			set
			{
				this.highlightTermsField = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00026DE8 File Offset: 0x00024FE8
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x00026DF0 File Offset: 0x00024FF0
		public int TotalConversationsInView
		{
			get
			{
				return this.totalConversationsInViewField;
			}
			set
			{
				this.totalConversationsInViewField = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00026DF9 File Offset: 0x00024FF9
		// (set) Token: 0x0600168A RID: 5770 RVA: 0x00026E01 File Offset: 0x00025001
		[XmlIgnore]
		public bool TotalConversationsInViewSpecified
		{
			get
			{
				return this.totalConversationsInViewFieldSpecified;
			}
			set
			{
				this.totalConversationsInViewFieldSpecified = value;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x00026E0A File Offset: 0x0002500A
		// (set) Token: 0x0600168C RID: 5772 RVA: 0x00026E12 File Offset: 0x00025012
		public int IndexedOffset
		{
			get
			{
				return this.indexedOffsetField;
			}
			set
			{
				this.indexedOffsetField = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00026E1B File Offset: 0x0002501B
		// (set) Token: 0x0600168E RID: 5774 RVA: 0x00026E23 File Offset: 0x00025023
		[XmlIgnore]
		public bool IndexedOffsetSpecified
		{
			get
			{
				return this.indexedOffsetFieldSpecified;
			}
			set
			{
				this.indexedOffsetFieldSpecified = value;
			}
		}

		// Token: 0x04000F5E RID: 3934
		private ConversationType[] conversationsField;

		// Token: 0x04000F5F RID: 3935
		private HighlightTermType[] highlightTermsField;

		// Token: 0x04000F60 RID: 3936
		private int totalConversationsInViewField;

		// Token: 0x04000F61 RID: 3937
		private bool totalConversationsInViewFieldSpecified;

		// Token: 0x04000F62 RID: 3938
		private int indexedOffsetField;

		// Token: 0x04000F63 RID: 3939
		private bool indexedOffsetFieldSpecified;
	}
}
