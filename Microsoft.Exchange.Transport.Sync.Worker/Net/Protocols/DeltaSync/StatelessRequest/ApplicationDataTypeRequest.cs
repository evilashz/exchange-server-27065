using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200016C RID: 364
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class ApplicationDataTypeRequest
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001D779 File Offset: 0x0001B979
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0001D781 File Offset: 0x0001B981
		[XmlElement("Subject", typeof(stringWithEncodingType), Namespace = "EMAIL:")]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("ConfirmedJunk", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("ConversationIndex", typeof(stringWithEncodingType1), Namespace = "HMMAIL:")]
		[XmlElement("Version", typeof(int), Namespace = "HMMAIL:")]
		[XmlElement("Flag", typeof(ApplicationDataTypeRequestFlag))]
		[XmlElement("Size", typeof(ulong))]
		[XmlElement("TotalMessageCount", typeof(uint))]
		[XmlElement("UnreadMessageCount", typeof(uint))]
		[XmlElement("DateReceived", typeof(string), Namespace = "EMAIL:")]
		[XmlElement("From", typeof(stringWithEncodingType), Namespace = "EMAIL:")]
		[XmlElement("Importance", typeof(byte), Namespace = "EMAIL:")]
		[XmlElement("MessageClass", typeof(string), Namespace = "EMAIL:")]
		[XmlElement("Read", typeof(byte), Namespace = "EMAIL:")]
		[XmlElement("DisplayName", typeof(DisplayName), Namespace = "HMFOLDER:")]
		[XmlElement("ParentId", typeof(ParentId), Namespace = "HMFOLDER:")]
		[XmlElement("Version", typeof(int), Namespace = "HMFOLDER:")]
		[XmlElement("Categories", typeof(stringWithCharSetType))]
		[XmlElement("ConversationTopic", typeof(stringWithEncodingType1), Namespace = "HMMAIL:")]
		[XmlElement("FolderId", typeof(FolderId), Namespace = "HMMAIL:")]
		[XmlElement("HasAttachments", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("IsBondedSender", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("IsFromSomeoneAddressBook", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("IsToAllowList", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("LegacyId", typeof(string), Namespace = "HMMAIL:")]
		[XmlElement("Message", typeof(string), Namespace = "HMMAIL:")]
		[XmlElement("PopAccountID", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("ReplyToOrForwardState", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("Sensitivity", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("Size", typeof(uint), Namespace = "HMMAIL:")]
		[XmlElement("TrustedSource", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("TypeData", typeof(byte), Namespace = "HMMAIL:")]
		public object[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0001D78A File Offset: 0x0001B98A
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0001D792 File Offset: 0x0001B992
		[XmlElement("ItemsElementName")]
		[XmlIgnore]
		public ItemsChoiceType1[] ItemsElementName
		{
			get
			{
				return this.itemsElementNameField;
			}
			set
			{
				this.itemsElementNameField = value;
			}
		}

		// Token: 0x040005E4 RID: 1508
		private object[] itemsField;

		// Token: 0x040005E5 RID: 1509
		private ItemsChoiceType1[] itemsElementNameField;
	}
}
