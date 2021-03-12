using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001E4 RID: 484
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetSharingMetadataResponseMessageType : ResponseMessageType
	{
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x000257FE File Offset: 0x000239FE
		// (set) Token: 0x060013EE RID: 5102 RVA: 0x00025806 File Offset: 0x00023A06
		[XmlArrayItem("EncryptedSharedFolderData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public EncryptedSharedFolderDataType[] EncryptedSharedFolderDataCollection
		{
			get
			{
				return this.encryptedSharedFolderDataCollectionField;
			}
			set
			{
				this.encryptedSharedFolderDataCollectionField = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0002580F File Offset: 0x00023A0F
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x00025817 File Offset: 0x00023A17
		[XmlArrayItem("InvalidRecipient", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public InvalidRecipientType[] InvalidRecipients
		{
			get
			{
				return this.invalidRecipientsField;
			}
			set
			{
				this.invalidRecipientsField = value;
			}
		}

		// Token: 0x04000DC1 RID: 3521
		private EncryptedSharedFolderDataType[] encryptedSharedFolderDataCollectionField;

		// Token: 0x04000DC2 RID: 3522
		private InvalidRecipientType[] invalidRecipientsField;
	}
}
