using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A2 RID: 930
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FindFolderType : BaseRequestType
	{
		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x0002A57A File Offset: 0x0002877A
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x0002A582 File Offset: 0x00028782
		public FolderResponseShapeType FolderShape
		{
			get
			{
				return this.folderShapeField;
			}
			set
			{
				this.folderShapeField = value;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x0002A58B File Offset: 0x0002878B
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x0002A593 File Offset: 0x00028793
		[XmlElement("FractionalPageFolderView", typeof(FractionalPageViewType))]
		[XmlElement("IndexedPageFolderView", typeof(IndexedPageViewType))]
		public BasePagingType Item
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

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x0002A59C File Offset: 0x0002879C
		// (set) Token: 0x06001D25 RID: 7461 RVA: 0x0002A5A4 File Offset: 0x000287A4
		public RestrictionType Restriction
		{
			get
			{
				return this.restrictionField;
			}
			set
			{
				this.restrictionField = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0002A5AD File Offset: 0x000287AD
		// (set) Token: 0x06001D27 RID: 7463 RVA: 0x0002A5B5 File Offset: 0x000287B5
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] ParentFolderIds
		{
			get
			{
				return this.parentFolderIdsField;
			}
			set
			{
				this.parentFolderIdsField = value;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0002A5BE File Offset: 0x000287BE
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x0002A5C6 File Offset: 0x000287C6
		[XmlAttribute]
		public FolderQueryTraversalType Traversal
		{
			get
			{
				return this.traversalField;
			}
			set
			{
				this.traversalField = value;
			}
		}

		// Token: 0x0400134D RID: 4941
		private FolderResponseShapeType folderShapeField;

		// Token: 0x0400134E RID: 4942
		private BasePagingType itemField;

		// Token: 0x0400134F RID: 4943
		private RestrictionType restrictionField;

		// Token: 0x04001350 RID: 4944
		private BaseFolderIdType[] parentFolderIdsField;

		// Token: 0x04001351 RID: 4945
		private FolderQueryTraversalType traversalField;
	}
}
