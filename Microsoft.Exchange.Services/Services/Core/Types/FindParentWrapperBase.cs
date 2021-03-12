using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Search;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000785 RID: 1925
	[KnownType(typeof(FindFolderParentWrapper))]
	[KnownType(typeof(FindItemParentWrapper))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public abstract class FindParentWrapperBase
	{
		// Token: 0x06003971 RID: 14705 RVA: 0x000CB40B File Offset: 0x000C960B
		public FindParentWrapperBase()
		{
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000CB414 File Offset: 0x000C9614
		internal FindParentWrapperBase(BasePageResult pageResult)
		{
			IndexedPageResult indexedPageResult = pageResult as IndexedPageResult;
			if (indexedPageResult != null)
			{
				this.Initialize(indexedPageResult.IndexedOffset, indexedPageResult.View.TotalItems, indexedPageResult.View.RetrievedLastItem);
				return;
			}
			FractionalPageResult fractionalPageResult = pageResult as FractionalPageResult;
			if (fractionalPageResult != null)
			{
				this.Initialize(fractionalPageResult.NumeratorOffset, fractionalPageResult.AbsoluteDenominator, fractionalPageResult.View.TotalItems, fractionalPageResult.View.RetrievedLastItem);
				return;
			}
			this.Initialize(pageResult.View.TotalItems, pageResult.View.RetrievedLastItem);
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000CB4A3 File Offset: 0x000C96A3
		private void Initialize(int totalItemsInView, bool includesLastItemInRange)
		{
			this.TotalItemsInView = totalItemsInView;
			this.IncludesLastItemInRange = includesLastItemInRange;
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000CB4B3 File Offset: 0x000C96B3
		private void Initialize(int indexedPagingOffset, int totalItemsInView, bool includesLastItemInRange)
		{
			this.IndexedPagingOffset = indexedPagingOffset;
			this.TotalItemsInView = totalItemsInView;
			this.IncludesLastItemInRange = includesLastItemInRange;
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000CB4CA File Offset: 0x000C96CA
		private void Initialize(int numeratorOffset, int absoluteDenominator, int totalItemsInView, bool includesLastItemInRange)
		{
			this.NumeratorOffset = numeratorOffset;
			this.AbsoluteDenominator = absoluteDenominator;
			this.TotalItemsInView = totalItemsInView;
			this.IncludesLastItemInRange = includesLastItemInRange;
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000CB4E9 File Offset: 0x000C96E9
		// (set) Token: 0x06003977 RID: 14711 RVA: 0x000CB4F1 File Offset: 0x000C96F1
		[IgnoreDataMember]
		[XmlAttribute]
		public int IndexedPagingOffset
		{
			get
			{
				return this.indexedPagingOffset;
			}
			set
			{
				this.IndexedPagingOffsetSpecified = true;
				this.indexedPagingOffset = value;
			}
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x000CB504 File Offset: 0x000C9704
		// (set) Token: 0x06003979 RID: 14713 RVA: 0x000CB52E File Offset: 0x000C972E
		[XmlIgnore]
		[DataMember(Name = "IndexedPagingOffset", EmitDefaultValue = false)]
		public int? IndexedPagingOffsetNullable
		{
			get
			{
				if (this.IndexedPagingOffsetSpecified)
				{
					return new int?(this.IndexedPagingOffset);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.IndexedPagingOffset = value.Value;
					return;
				}
				this.IndexedPagingOffsetSpecified = false;
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600397A RID: 14714 RVA: 0x000CB54E File Offset: 0x000C974E
		// (set) Token: 0x0600397B RID: 14715 RVA: 0x000CB556 File Offset: 0x000C9756
		[IgnoreDataMember]
		[XmlAttribute]
		public int NumeratorOffset
		{
			get
			{
				return this.numeratorOffset;
			}
			set
			{
				this.NumeratorOffsetSpecified = true;
				this.numeratorOffset = value;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000CB568 File Offset: 0x000C9768
		// (set) Token: 0x0600397D RID: 14717 RVA: 0x000CB592 File Offset: 0x000C9792
		[XmlIgnore]
		[DataMember(Name = "NumeratorOffset", EmitDefaultValue = false)]
		public int? NumeratorOffsetNullable
		{
			get
			{
				if (this.NumeratorOffsetSpecified)
				{
					return new int?(this.NumeratorOffset);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.NumeratorOffset = value.Value;
					return;
				}
				this.NumeratorOffsetSpecified = false;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600397E RID: 14718 RVA: 0x000CB5B2 File Offset: 0x000C97B2
		// (set) Token: 0x0600397F RID: 14719 RVA: 0x000CB5BA File Offset: 0x000C97BA
		[XmlAttribute]
		[IgnoreDataMember]
		public int AbsoluteDenominator
		{
			get
			{
				return this.absoluteDenominator;
			}
			set
			{
				this.AbsoluteDenominatorSpecified = true;
				this.absoluteDenominator = value;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003980 RID: 14720 RVA: 0x000CB5CC File Offset: 0x000C97CC
		// (set) Token: 0x06003981 RID: 14721 RVA: 0x000CB5F6 File Offset: 0x000C97F6
		[DataMember(Name = "AbsoluteDenominator", EmitDefaultValue = false)]
		[XmlIgnore]
		public int? AbsoluteDenominatorNullable
		{
			get
			{
				if (this.AbsoluteDenominatorSpecified)
				{
					return new int?(this.AbsoluteDenominator);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.AbsoluteDenominator = value.Value;
					return;
				}
				this.AbsoluteDenominatorSpecified = false;
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x000CB616 File Offset: 0x000C9816
		// (set) Token: 0x06003983 RID: 14723 RVA: 0x000CB61E File Offset: 0x000C981E
		[XmlAttribute]
		[IgnoreDataMember]
		public int TotalItemsInView
		{
			get
			{
				return this.totalItemsInView;
			}
			set
			{
				this.TotalItemsInViewSpecified = true;
				this.totalItemsInView = value;
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x000CB630 File Offset: 0x000C9830
		// (set) Token: 0x06003985 RID: 14725 RVA: 0x000CB65A File Offset: 0x000C985A
		[XmlIgnore]
		[DataMember(Name = "TotalItemsInView", EmitDefaultValue = false)]
		public int? TotalItemsInViewNullable
		{
			get
			{
				if (this.TotalItemsInViewSpecified)
				{
					return new int?(this.TotalItemsInView);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.TotalItemsInView = value.Value;
					return;
				}
				this.TotalItemsInViewSpecified = false;
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x000CB67A File Offset: 0x000C987A
		// (set) Token: 0x06003987 RID: 14727 RVA: 0x000CB682 File Offset: 0x000C9882
		[XmlAttribute]
		[IgnoreDataMember]
		public bool IncludesLastItemInRange
		{
			get
			{
				return this.includesLastItemInRange;
			}
			set
			{
				this.IncludesLastItemInRangeSpecified = true;
				this.includesLastItemInRange = value;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06003988 RID: 14728 RVA: 0x000CB694 File Offset: 0x000C9894
		// (set) Token: 0x06003989 RID: 14729 RVA: 0x000CB6BE File Offset: 0x000C98BE
		[XmlIgnore]
		[DataMember(Name = "IncludesLastItemInRange", EmitDefaultValue = false)]
		public bool? IncludesLastItemInRangeNullable
		{
			get
			{
				if (this.IncludesLastItemInRangeSpecified)
				{
					return new bool?(this.IncludesLastItemInRange);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.IncludesLastItemInRangeSpecified = value.Value;
					return;
				}
				this.IncludesLastItemInRangeSpecified = false;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000CB6DE File Offset: 0x000C98DE
		// (set) Token: 0x0600398B RID: 14731 RVA: 0x000CB6E6 File Offset: 0x000C98E6
		[IgnoreDataMember]
		[XmlIgnore]
		public bool TotalItemsInViewSpecified { get; set; }

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000CB6EF File Offset: 0x000C98EF
		// (set) Token: 0x0600398D RID: 14733 RVA: 0x000CB6F7 File Offset: 0x000C98F7
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IndexedPagingOffsetSpecified { get; set; }

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x0600398E RID: 14734 RVA: 0x000CB700 File Offset: 0x000C9900
		// (set) Token: 0x0600398F RID: 14735 RVA: 0x000CB708 File Offset: 0x000C9908
		[IgnoreDataMember]
		[XmlIgnore]
		public bool NumeratorOffsetSpecified { get; set; }

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000CB711 File Offset: 0x000C9911
		// (set) Token: 0x06003991 RID: 14737 RVA: 0x000CB719 File Offset: 0x000C9919
		[IgnoreDataMember]
		[XmlIgnore]
		public bool AbsoluteDenominatorSpecified { get; set; }

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000CB722 File Offset: 0x000C9922
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x000CB72A File Offset: 0x000C992A
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IncludesLastItemInRangeSpecified { get; set; }

		// Token: 0x04001FFA RID: 8186
		protected int indexedPagingOffset;

		// Token: 0x04001FFB RID: 8187
		protected int numeratorOffset;

		// Token: 0x04001FFC RID: 8188
		protected int absoluteDenominator;

		// Token: 0x04001FFD RID: 8189
		protected bool includesLastItemInRange;

		// Token: 0x04001FFE RID: 8190
		protected int totalItemsInView;
	}
}
