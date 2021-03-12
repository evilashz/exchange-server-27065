using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200038C RID: 908
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ArchiveItemType : BaseRequestType
	{
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x0002A1C2 File Offset: 0x000283C2
		// (set) Token: 0x06001CB0 RID: 7344 RVA: 0x0002A1CA File Offset: 0x000283CA
		public TargetFolderIdType ArchiveSourceFolderId
		{
			get
			{
				return this.archiveSourceFolderIdField;
			}
			set
			{
				this.archiveSourceFolderIdField = value;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x0002A1D3 File Offset: 0x000283D3
		// (set) Token: 0x06001CB2 RID: 7346 RVA: 0x0002A1DB File Offset: 0x000283DB
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("ItemId", typeof(ItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemIdRanges", typeof(RecurringMasterItemIdRangesType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseItemIdType[] ItemIds
		{
			get
			{
				return this.itemIdsField;
			}
			set
			{
				this.itemIdsField = value;
			}
		}

		// Token: 0x04001308 RID: 4872
		private TargetFolderIdType archiveSourceFolderIdField;

		// Token: 0x04001309 RID: 4873
		private BaseItemIdType[] itemIdsField;
	}
}
