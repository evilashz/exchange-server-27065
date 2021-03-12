using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200039D RID: 925
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[XmlInclude(typeof(CopyFolderType))]
	[XmlInclude(typeof(MoveFolderType))]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class BaseMoveCopyFolderType : BaseRequestType
	{
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0002A4DB File Offset: 0x000286DB
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x0002A4E3 File Offset: 0x000286E3
		public TargetFolderIdType ToFolderId
		{
			get
			{
				return this.toFolderIdField;
			}
			set
			{
				this.toFolderIdField = value;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x0002A4EC File Offset: 0x000286EC
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x0002A4F4 File Offset: 0x000286F4
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
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

		// Token: 0x04001346 RID: 4934
		private TargetFolderIdType toFolderIdField;

		// Token: 0x04001347 RID: 4935
		private BaseFolderIdType[] folderIdsField;
	}
}
