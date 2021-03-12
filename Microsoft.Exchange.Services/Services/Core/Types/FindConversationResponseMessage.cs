using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D8 RID: 1240
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("FindConversationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class FindConversationResponseMessage : ResponseMessage
	{
		// Token: 0x06002444 RID: 9284 RVA: 0x000A4AC8 File Offset: 0x000A2CC8
		public FindConversationResponseMessage()
		{
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000A4AD0 File Offset: 0x000A2CD0
		internal FindConversationResponseMessage(ServiceResultCode code, ServiceError error, ConversationType[] findConversationResults, HighlightTermType[] highlightTerms, int? totalConversationsInView, int? indexedOffset, bool isSearchInProgress) : base(code, error)
		{
			this.Conversations = findConversationResults;
			if (highlightTerms != null)
			{
				this.HighlightTerms = highlightTerms;
			}
			if (totalConversationsInView != null)
			{
				this.TotalConversationsInView = totalConversationsInView.Value;
			}
			else
			{
				this.TotalConversationsInViewSpecified = false;
			}
			if (indexedOffset != null)
			{
				this.IndexedOffset = indexedOffset.Value;
			}
			else
			{
				this.IndexedOffsetSpecified = false;
			}
			this.IsSearchInProgress = isSearchInProgress;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000A4B3E File Offset: 0x000A2D3E
		public override ResponseType GetResponseType()
		{
			return ResponseType.FindConversationResponseMessage;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x000A4B42 File Offset: 0x000A2D42
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x000A4B4A File Offset: 0x000A2D4A
		[XmlArray(ElementName = "Conversations")]
		[DataMember]
		[XmlArrayItem(ElementName = "Conversation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public ConversationType[] Conversations { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000A4B53 File Offset: 0x000A2D53
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x000A4B5B File Offset: 0x000A2D5B
		[XmlArray(ElementName = "HighlightTerms")]
		[XmlArrayItem(ElementName = "Term", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(Name = "HighlightTerms", IsRequired = false, EmitDefaultValue = false)]
		public HighlightTermType[] HighlightTerms { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x000A4B64 File Offset: 0x000A2D64
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x000A4B6C File Offset: 0x000A2D6C
		[XmlElement("TotalConversationsInView")]
		public int TotalConversationsInView
		{
			get
			{
				return this.totalConversationsInView;
			}
			set
			{
				this.TotalConversationsInViewSpecified = true;
				this.totalConversationsInView = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000A4B7C File Offset: 0x000A2D7C
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x000A4BA6 File Offset: 0x000A2DA6
		[DataMember(Name = "TotalConversationsInView", EmitDefaultValue = false)]
		[XmlIgnore]
		public int? TotalConversationsInViewNullable
		{
			get
			{
				if (this.TotalConversationsInViewSpecified)
				{
					return new int?(this.TotalConversationsInView);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.TotalConversationsInView = value.Value;
					return;
				}
				this.TotalConversationsInViewSpecified = false;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x000A4BC6 File Offset: 0x000A2DC6
		// (set) Token: 0x06002450 RID: 9296 RVA: 0x000A4BCE File Offset: 0x000A2DCE
		[IgnoreDataMember]
		[XmlIgnore]
		public bool TotalConversationsInViewSpecified { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000A4BD7 File Offset: 0x000A2DD7
		// (set) Token: 0x06002452 RID: 9298 RVA: 0x000A4BDF File Offset: 0x000A2DDF
		[XmlElement("IndexedOffset")]
		public int IndexedOffset
		{
			get
			{
				return this.indexedOffset;
			}
			set
			{
				this.IndexedOffsetSpecified = true;
				this.indexedOffset = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x000A4BF0 File Offset: 0x000A2DF0
		// (set) Token: 0x06002454 RID: 9300 RVA: 0x000A4C1A File Offset: 0x000A2E1A
		[XmlIgnore]
		[DataMember(Name = "IndexedOffset", EmitDefaultValue = false)]
		public int? IndexedOffsetNullable
		{
			get
			{
				if (this.IndexedOffsetSpecified)
				{
					return new int?(this.IndexedOffset);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.IndexedOffset = value.Value;
					return;
				}
				this.IndexedOffsetSpecified = false;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x000A4C3A File Offset: 0x000A2E3A
		// (set) Token: 0x06002456 RID: 9302 RVA: 0x000A4C42 File Offset: 0x000A2E42
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IndexedOffsetSpecified { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x000A4C4B File Offset: 0x000A2E4B
		// (set) Token: 0x06002458 RID: 9304 RVA: 0x000A4C53 File Offset: 0x000A2E53
		[DataMember(Name = "IsSearchInProgress", EmitDefaultValue = false)]
		[XmlIgnore]
		public bool IsSearchInProgress { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x000A4C5C File Offset: 0x000A2E5C
		// (set) Token: 0x0600245A RID: 9306 RVA: 0x000A4C64 File Offset: 0x000A2E64
		[XmlIgnore]
		[DataMember(Name = "SearchFolderId", EmitDefaultValue = false)]
		public FolderId SearchFolderId { get; set; }

		// Token: 0x04001571 RID: 5489
		private int totalConversationsInView;

		// Token: 0x04001572 RID: 5490
		private int indexedOffset;
	}
}
