using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A7 RID: 935
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetFolderType : BaseRequestType
	{
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0002A633 File Offset: 0x00028833
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x0002A63B File Offset: 0x0002883B
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

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0002A644 File Offset: 0x00028844
		// (set) Token: 0x06001D39 RID: 7481 RVA: 0x0002A64C File Offset: 0x0002884C
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] FolderIds
		{
			get
			{
				return this.folderIdsField;
			}
			set
			{
				this.folderIdsField = value;
			}
		}

		// Token: 0x0400135A RID: 4954
		private FolderResponseShapeType folderShapeField;

		// Token: 0x0400135B RID: 4955
		private BaseFolderIdType[] folderIdsField;
	}
}
