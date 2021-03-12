using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200030A RID: 778
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UploadItemType
	{
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000288C6 File Offset: 0x00026AC6
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x000288CE File Offset: 0x00026ACE
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

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x000288D7 File Offset: 0x00026AD7
		// (set) Token: 0x060019BC RID: 6588 RVA: 0x000288DF File Offset: 0x00026ADF
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x000288E8 File Offset: 0x00026AE8
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x000288F0 File Offset: 0x00026AF0
		[XmlElement(DataType = "base64Binary")]
		public byte[] Data
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x000288F9 File Offset: 0x00026AF9
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x00028901 File Offset: 0x00026B01
		[XmlAttribute]
		public CreateActionType CreateAction
		{
			get
			{
				return this.createActionField;
			}
			set
			{
				this.createActionField = value;
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0002890A File Offset: 0x00026B0A
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x00028912 File Offset: 0x00026B12
		[XmlAttribute]
		public bool IsAssociated
		{
			get
			{
				return this.isAssociatedField;
			}
			set
			{
				this.isAssociatedField = value;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0002891B File Offset: 0x00026B1B
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x00028923 File Offset: 0x00026B23
		[XmlIgnore]
		public bool IsAssociatedSpecified
		{
			get
			{
				return this.isAssociatedFieldSpecified;
			}
			set
			{
				this.isAssociatedFieldSpecified = value;
			}
		}

		// Token: 0x04001150 RID: 4432
		private FolderIdType parentFolderIdField;

		// Token: 0x04001151 RID: 4433
		private ItemIdType itemIdField;

		// Token: 0x04001152 RID: 4434
		private byte[] dataField;

		// Token: 0x04001153 RID: 4435
		private CreateActionType createActionField;

		// Token: 0x04001154 RID: 4436
		private bool isAssociatedField;

		// Token: 0x04001155 RID: 4437
		private bool isAssociatedFieldSpecified;
	}
}
