using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003A1 RID: 929
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class DeleteFolderType : BaseRequestType
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x0002A550 File Offset: 0x00028750
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x0002A558 File Offset: 0x00028758
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

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0002A561 File Offset: 0x00028761
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x0002A569 File Offset: 0x00028769
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

		// Token: 0x0400134B RID: 4939
		private BaseFolderIdType[] folderIdsField;

		// Token: 0x0400134C RID: 4940
		private DisposalType deleteTypeField;
	}
}
