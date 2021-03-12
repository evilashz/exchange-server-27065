using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x0200018D RID: 397
	[XmlType(Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DesignerCategory("code")]
	[Serializable]
	public class ApplicationDataTypeResponse
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001DCBA File Offset: 0x0001BEBA
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0001DCC2 File Offset: 0x0001BEC2
		[XmlElement("Version", typeof(int), Namespace = "HMFOLDER:")]
		[XmlElement("ConversationIndex", typeof(stringWithEncodingType1), Namespace = "HMMAIL:")]
		[XmlElement("Categories", typeof(stringWithCharSetType))]
		[XmlElement("DisplayName", typeof(stringWithCharSetType))]
		[XmlElement("Flag", typeof(ApplicationDataTypeResponseFlag))]
		[XmlElement("Size", typeof(ulong))]
		[XmlElement("TotalMessageCount", typeof(uint))]
		[XmlElement("UnreadMessageCount", typeof(uint))]
		[XmlElement("DateReceived", typeof(string), Namespace = "EMAIL:")]
		[XmlElement("From", typeof(stringWithEncodingType), Namespace = "EMAIL:")]
		[XmlElement("Importance", typeof(byte), Namespace = "EMAIL:")]
		[XmlElement("MessageClass", typeof(string), Namespace = "EMAIL:")]
		[XmlElement("Read", typeof(byte), Namespace = "EMAIL:")]
		[XmlElement("Subject", typeof(stringWithEncodingType), Namespace = "EMAIL:")]
		[XmlElement("ParentId", typeof(ParentId), Namespace = "HMFOLDER:")]
		[XmlElement("LegacyId", typeof(string), Namespace = "HMMAIL:")]
		[XmlElement("ConfirmedJunk", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("Message", typeof(string), Namespace = "HMMAIL:")]
		[XmlElement("ConversationTopic", typeof(stringWithEncodingType1), Namespace = "HMMAIL:")]
		[XmlElement("FolderId", typeof(FolderId), Namespace = "HMMAIL:")]
		[XmlElement("HasAttachments", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("IsBondedSender", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("IsFromSomeoneAddressBook", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("IsToAllowList", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("Size", typeof(uint), Namespace = "HMMAIL:")]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("PopAccountID", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("ReplyToOrForwardState", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("Sensitivity", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("TrustedSource", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("TypeData", typeof(byte), Namespace = "HMMAIL:")]
		[XmlElement("Version", typeof(int), Namespace = "HMMAIL:")]
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0001DCCB File Offset: 0x0001BECB
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x0001DCD3 File Offset: 0x0001BED3
		[XmlIgnore]
		[XmlElement("ItemsElementName")]
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

		// Token: 0x04000657 RID: 1623
		private object[] itemsField;

		// Token: 0x04000658 RID: 1624
		private ItemsChoiceType1[] itemsElementNameField;
	}
}
