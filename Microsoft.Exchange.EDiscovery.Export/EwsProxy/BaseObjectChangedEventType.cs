using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000230 RID: 560
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(ModifiedEventType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(MovedCopiedEventType))]
	[Serializable]
	public class BaseObjectChangedEventType : BaseNotificationEventType
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00026453 File Offset: 0x00024653
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x0002645B File Offset: 0x0002465B
		public DateTime TimeStamp
		{
			get
			{
				return this.timeStampField;
			}
			set
			{
				this.timeStampField = value;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00026464 File Offset: 0x00024664
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x0002646C File Offset: 0x0002466C
		[XmlElement("ItemId", typeof(ItemIdType))]
		[XmlElement("FolderId", typeof(FolderIdType))]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x00026475 File Offset: 0x00024675
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x0002647D File Offset: 0x0002467D
		public FolderIdType ParentFolderId
		{
			get
			{
				return this.parentFolderIdField;
			}
			set
			{
				this.parentFolderIdField = value;
			}
		}

		// Token: 0x04000EB8 RID: 3768
		private DateTime timeStampField;

		// Token: 0x04000EB9 RID: 3769
		private object itemField;

		// Token: 0x04000EBA RID: 3770
		private FolderIdType parentFolderIdField;
	}
}
