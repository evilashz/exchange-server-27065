using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000339 RID: 825
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class MarkAllItemsAsReadType : BaseRequestType
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x00028FF7 File Offset: 0x000271F7
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x00028FFF File Offset: 0x000271FF
		public bool ReadFlag
		{
			get
			{
				return this.readFlagField;
			}
			set
			{
				this.readFlagField = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00029008 File Offset: 0x00027208
		// (set) Token: 0x06001A97 RID: 6807 RVA: 0x00029010 File Offset: 0x00027210
		public bool SuppressReadReceipts
		{
			get
			{
				return this.suppressReadReceiptsField;
			}
			set
			{
				this.suppressReadReceiptsField = value;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00029019 File Offset: 0x00027219
		// (set) Token: 0x06001A99 RID: 6809 RVA: 0x00029021 File Offset: 0x00027221
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

		// Token: 0x040011C8 RID: 4552
		private bool readFlagField;

		// Token: 0x040011C9 RID: 4553
		private bool suppressReadReceiptsField;

		// Token: 0x040011CA RID: 4554
		private BaseFolderIdType[] folderIdsField;
	}
}
