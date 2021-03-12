using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000264 RID: 612
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class UpdateItemInRecoverableItemsResponseMessageType : ItemInfoResponseMessageType
	{
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00027250 File Offset: 0x00025450
		// (set) Token: 0x0600170D RID: 5901 RVA: 0x00027258 File Offset: 0x00025458
		[XmlArrayItem("ItemAttachment", typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FileAttachment", typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public AttachmentType[] Attachments
		{
			get
			{
				return this.attachmentsField;
			}
			set
			{
				this.attachmentsField = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00027261 File Offset: 0x00025461
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x00027269 File Offset: 0x00025469
		public ConflictResultsType ConflictResults
		{
			get
			{
				return this.conflictResultsField;
			}
			set
			{
				this.conflictResultsField = value;
			}
		}

		// Token: 0x04000FA0 RID: 4000
		private AttachmentType[] attachmentsField;

		// Token: 0x04000FA1 RID: 4001
		private ConflictResultsType conflictResultsField;
	}
}
