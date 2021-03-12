using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000302 RID: 770
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FolderChangeType
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x000287A0 File Offset: 0x000269A0
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x000287A8 File Offset: 0x000269A8
		[XmlElement("DistinguishedFolderId", typeof(DistinguishedFolderIdType))]
		[XmlElement("FolderId", typeof(FolderIdType))]
		public BaseFolderIdType Item
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

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x000287B1 File Offset: 0x000269B1
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x000287B9 File Offset: 0x000269B9
		[XmlArrayItem("AppendToFolderField", typeof(AppendToFolderFieldType), IsNullable = false)]
		[XmlArrayItem("SetFolderField", typeof(SetFolderFieldType), IsNullable = false)]
		[XmlArrayItem("DeleteFolderField", typeof(DeleteFolderFieldType), IsNullable = false)]
		public FolderChangeDescriptionType[] Updates
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

		// Token: 0x0400113F RID: 4415
		private BaseFolderIdType itemField;

		// Token: 0x04001140 RID: 4416
		private FolderChangeDescriptionType[] updatesField;
	}
}
