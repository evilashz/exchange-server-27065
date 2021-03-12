using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004DE RID: 1246
	[XmlType("FindItemResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindItemResponseMessage : ResponseMessage
	{
		// Token: 0x0600246F RID: 9327 RVA: 0x000A4D72 File Offset: 0x000A2F72
		public FindItemResponseMessage()
		{
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000A4D85 File Offset: 0x000A2F85
		internal FindItemResponseMessage(ServiceResultCode code, ServiceError error, FindItemParentWrapper parentWrapper, HighlightTermType[] highlightTerms, bool isSearchInProgress, FolderId searchFolderId) : base(code, error)
		{
			this.parentFolder = parentWrapper;
			this.HighlightTerms = highlightTerms;
			this.IsSearchInProgress = isSearchInProgress;
			this.SearchFolderId = searchFolderId;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x000A4DB9 File Offset: 0x000A2FB9
		// (set) Token: 0x06002472 RID: 9330 RVA: 0x000A4DC1 File Offset: 0x000A2FC1
		[DataMember(Name = "RootFolder")]
		[XmlElement("RootFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public FindItemParentWrapper ParentFolder
		{
			get
			{
				return this.parentFolder;
			}
			set
			{
				this.parentFolder = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000A4DCA File Offset: 0x000A2FCA
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x000A4DD2 File Offset: 0x000A2FD2
		[DataMember]
		[XmlArrayItem(ElementName = "Term", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray(ElementName = "HighlightTerms")]
		public HighlightTermType[] HighlightTerms { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000A4DDB File Offset: 0x000A2FDB
		// (set) Token: 0x06002476 RID: 9334 RVA: 0x000A4DE3 File Offset: 0x000A2FE3
		[DataMember(Name = "IsSearchInProgress", EmitDefaultValue = false)]
		[XmlIgnore]
		public bool IsSearchInProgress { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x000A4DEC File Offset: 0x000A2FEC
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x000A4DF4 File Offset: 0x000A2FF4
		[DataMember(Name = "SearchFolderId", EmitDefaultValue = false)]
		[XmlIgnore]
		public FolderId SearchFolderId { get; set; }

		// Token: 0x0400157D RID: 5501
		private FindItemParentWrapper parentFolder = new FindItemParentWrapper();
	}
}
