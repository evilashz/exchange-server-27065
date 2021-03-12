using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000393 RID: 915
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class UpdateItemInRecoverableItemsType : BaseRequestType
	{
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0002A2D8 File Offset: 0x000284D8
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0002A2E0 File Offset: 0x000284E0
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

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0002A2E9 File Offset: 0x000284E9
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x0002A2F1 File Offset: 0x000284F1
		[XmlArrayItem("AppendToItemField", typeof(AppendToItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SetItemField", typeof(SetItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DeleteItemField", typeof(DeleteItemFieldType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemChangeDescriptionType[] Updates
		{
			get
			{
				return this.updatesField;
			}
			set
			{
				this.updatesField = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0002A2FA File Offset: 0x000284FA
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x0002A302 File Offset: 0x00028502
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

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0002A30B File Offset: 0x0002850B
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0002A313 File Offset: 0x00028513
		public bool MakeItemImmutable
		{
			get
			{
				return this.makeItemImmutableField;
			}
			set
			{
				this.makeItemImmutableField = value;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x0002A31C File Offset: 0x0002851C
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0002A324 File Offset: 0x00028524
		[XmlIgnore]
		public bool MakeItemImmutableSpecified
		{
			get
			{
				return this.makeItemImmutableFieldSpecified;
			}
			set
			{
				this.makeItemImmutableFieldSpecified = value;
			}
		}

		// Token: 0x0400131D RID: 4893
		private ItemIdType itemIdField;

		// Token: 0x0400131E RID: 4894
		private ItemChangeDescriptionType[] updatesField;

		// Token: 0x0400131F RID: 4895
		private AttachmentType[] attachmentsField;

		// Token: 0x04001320 RID: 4896
		private bool makeItemImmutableField;

		// Token: 0x04001321 RID: 4897
		private bool makeItemImmutableFieldSpecified;
	}
}
