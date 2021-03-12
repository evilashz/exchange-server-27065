using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A0 RID: 928
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class EmptyFolderType : BaseRequestType
	{
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0002A515 File Offset: 0x00028715
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0002A51D File Offset: 0x0002871D
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

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0002A526 File Offset: 0x00028726
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0002A52E File Offset: 0x0002872E
		[XmlAttribute]
		public DisposalType DeleteType
		{
			get
			{
				return this.deleteTypeField;
			}
			set
			{
				this.deleteTypeField = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0002A537 File Offset: 0x00028737
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x0002A53F File Offset: 0x0002873F
		[XmlAttribute]
		public bool DeleteSubFolders
		{
			get
			{
				return this.deleteSubFoldersField;
			}
			set
			{
				this.deleteSubFoldersField = value;
			}
		}

		// Token: 0x04001348 RID: 4936
		private BaseFolderIdType[] folderIdsField;

		// Token: 0x04001349 RID: 4937
		private DisposalType deleteTypeField;

		// Token: 0x0400134A RID: 4938
		private bool deleteSubFoldersField;
	}
}
