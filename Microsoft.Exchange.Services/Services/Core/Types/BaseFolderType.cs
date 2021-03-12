using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A3 RID: 1443
	[KnownType(typeof(SearchFolderType))]
	[XmlInclude(typeof(FolderType))]
	[KnownType(typeof(FolderType))]
	[KnownType(typeof(TasksFolderType))]
	[XmlInclude(typeof(ContactsFolderType))]
	[XmlInclude(typeof(CalendarFolderType))]
	[KnownType(typeof(CalendarFolderType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(TasksFolderType))]
	[XmlInclude(typeof(SearchFolderType))]
	[KnownType(typeof(ContactsFolderType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public abstract class BaseFolderType : ServiceObject
	{
		// Token: 0x060028EA RID: 10474 RVA: 0x000ACA53 File Offset: 0x000AAC53
		internal static BaseFolderType CreateFromStoreObjectType(StoreObjectType storeObjectType)
		{
			if (BaseFolderType.createMethods.Member.ContainsKey(storeObjectType))
			{
				return BaseFolderType.createMethods.Member[storeObjectType]();
			}
			return BaseFolderType.createMethods.Member[StoreObjectType.Folder]();
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x000ACA92 File Offset: 0x000AAC92
		public BaseFolderType()
		{
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x000ACA9A File Offset: 0x000AAC9A
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x000ACAAC File Offset: 0x000AACAC
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public FolderId FolderId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<FolderId>(BaseFolderSchema.FolderId);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.FolderId] = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x000ACABF File Offset: 0x000AACBF
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x000ACAD1 File Offset: 0x000AACD1
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public FolderId ParentFolderId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<FolderId>(BaseFolderSchema.ParentFolderId);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ParentFolderId] = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x000ACAE4 File Offset: 0x000AACE4
		// (set) Token: 0x060028F1 RID: 10481 RVA: 0x000ACAF6 File Offset: 0x000AACF6
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string FolderClass
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(BaseFolderSchema.FolderClass);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.FolderClass] = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x000ACB09 File Offset: 0x000AAD09
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x000ACB1B File Offset: 0x000AAD1B
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string DisplayName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(BaseFolderSchema.DisplayName);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x000ACB2E File Offset: 0x000AAD2E
		// (set) Token: 0x060028F5 RID: 10485 RVA: 0x000ACB40 File Offset: 0x000AAD40
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public int? TotalCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(BaseFolderSchema.TotalCount);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.TotalCount] = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060028F6 RID: 10486 RVA: 0x000ACB58 File Offset: 0x000AAD58
		// (set) Token: 0x060028F7 RID: 10487 RVA: 0x000ACB65 File Offset: 0x000AAD65
		[XmlIgnore]
		[IgnoreDataMember]
		public bool TotalCountSpecified
		{
			get
			{
				return base.IsSet(BaseFolderSchema.TotalCount);
			}
			set
			{
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x000ACB67 File Offset: 0x000AAD67
		// (set) Token: 0x060028F9 RID: 10489 RVA: 0x000ACB79 File Offset: 0x000AAD79
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public int? ChildFolderCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(BaseFolderSchema.ChildFolderCount);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ChildFolderCount] = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060028FA RID: 10490 RVA: 0x000ACB91 File Offset: 0x000AAD91
		// (set) Token: 0x060028FB RID: 10491 RVA: 0x000ACB9E File Offset: 0x000AAD9E
		[IgnoreDataMember]
		[XmlIgnore]
		public bool ChildFolderCountSpecified
		{
			get
			{
				return base.IsSet(BaseFolderSchema.ChildFolderCount);
			}
			set
			{
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000ACBA0 File Offset: 0x000AADA0
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x000ACBB2 File Offset: 0x000AADB2
		[DataMember(EmitDefaultValue = false, Order = 7)]
		[XmlElement("ExtendedProperty")]
		public ExtendedPropertyType[] ExtendedProperty
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ExtendedPropertyType[]>(BaseFolderSchema.ExtendedProperty);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ExtendedProperty] = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x000ACBC5 File Offset: 0x000AADC5
		// (set) Token: 0x060028FF RID: 10495 RVA: 0x000ACBD7 File Offset: 0x000AADD7
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public ManagedFolderInformationType ManagedFolderInformation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ManagedFolderInformationType>(BaseFolderSchema.ManagedFolderInformation);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ManagedFolderInformation] = value;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000ACBEA File Offset: 0x000AADEA
		// (set) Token: 0x06002901 RID: 10497 RVA: 0x000ACBFC File Offset: 0x000AADFC
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public EffectiveRightsType EffectiveRights
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EffectiveRightsType>(BaseFolderSchema.EffectiveRights);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.EffectiveRights] = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000ACC0F File Offset: 0x000AAE0F
		// (set) Token: 0x06002903 RID: 10499 RVA: 0x000ACC21 File Offset: 0x000AAE21
		[DataMember(Name = "DistinguishedFolderId", EmitDefaultValue = false, Order = 9)]
		public string DistinguishedFolderId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(BaseFolderSchema.DistinguishedFolderId);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.DistinguishedFolderId] = value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x000ACC34 File Offset: 0x000AAE34
		// (set) Token: 0x06002905 RID: 10501 RVA: 0x000ACC41 File Offset: 0x000AAE41
		[XmlIgnore]
		[IgnoreDataMember]
		public bool DistinguishedFolderIdSpecified
		{
			get
			{
				return base.IsSet(BaseFolderSchema.DistinguishedFolderId);
			}
			set
			{
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x000ACC43 File Offset: 0x000AAE43
		// (set) Token: 0x06002907 RID: 10503 RVA: 0x000ACC55 File Offset: 0x000AAE55
		[DataMember(Name = "PolicyTag", EmitDefaultValue = false, Order = 11)]
		public RetentionTagType PolicyTag
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<RetentionTagType>(BaseFolderSchema.PolicyTag);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.PolicyTag] = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x000ACC68 File Offset: 0x000AAE68
		// (set) Token: 0x06002909 RID: 10505 RVA: 0x000ACC7A File Offset: 0x000AAE7A
		[DataMember(Name = "ArchiveTag", EmitDefaultValue = false, Order = 12)]
		public RetentionTagType ArchiveTag
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<RetentionTagType>(BaseFolderSchema.ArchiveTag);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ArchiveTag] = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x000ACC8D File Offset: 0x000AAE8D
		// (set) Token: 0x0600290B RID: 10507 RVA: 0x000ACC9F File Offset: 0x000AAE9F
		[XmlIgnore]
		[DataMember(Name = "UnClutteredViewFolderEntryId", EmitDefaultValue = false, Order = 13)]
		public FolderId UnClutteredViewFolderEntryId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<FolderId>(BaseFolderSchema.UnClutteredViewFolderEntryId);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.UnClutteredViewFolderEntryId] = value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x000ACCB2 File Offset: 0x000AAEB2
		// (set) Token: 0x0600290D RID: 10509 RVA: 0x000ACCC4 File Offset: 0x000AAEC4
		[DataMember(Name = "ClutteredViewFolderEntryId", EmitDefaultValue = false, Order = 14)]
		[XmlIgnore]
		public FolderId ClutteredViewFolderEntryId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<FolderId>(BaseFolderSchema.ClutteredViewFolderEntryId);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ClutteredViewFolderEntryId] = value;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600290E RID: 10510 RVA: 0x000ACCD7 File Offset: 0x000AAED7
		// (set) Token: 0x0600290F RID: 10511 RVA: 0x000ACCE9 File Offset: 0x000AAEE9
		[DataMember(Name = "ClutterCount", EmitDefaultValue = false, Order = 15)]
		[XmlIgnore]
		public int? ClutterCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(BaseFolderSchema.ClutterCount);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ClutterCount] = value;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x000ACD01 File Offset: 0x000AAF01
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x000ACD0E File Offset: 0x000AAF0E
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ClutterCountSpecified
		{
			get
			{
				return base.IsSet(BaseFolderSchema.ClutterCount);
			}
			set
			{
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x000ACD10 File Offset: 0x000AAF10
		// (set) Token: 0x06002913 RID: 10515 RVA: 0x000ACD22 File Offset: 0x000AAF22
		[DataMember(Name = "UnreadClutterCount", EmitDefaultValue = false, Order = 16)]
		[XmlIgnore]
		public int? UnreadClutterCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(BaseFolderSchema.UnreadClutterCount);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.UnreadClutterCount] = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002914 RID: 10516 RVA: 0x000ACD3A File Offset: 0x000AAF3A
		// (set) Token: 0x06002915 RID: 10517 RVA: 0x000ACD47 File Offset: 0x000AAF47
		[IgnoreDataMember]
		[XmlIgnore]
		public bool UnreadClutterCountSpecified
		{
			get
			{
				return base.IsSet(BaseFolderSchema.UnreadClutterCount);
			}
			set
			{
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002916 RID: 10518 RVA: 0x000ACD49 File Offset: 0x000AAF49
		// (set) Token: 0x06002917 RID: 10519 RVA: 0x000ACD5B File Offset: 0x000AAF5B
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 17)]
		public string[] ReplicaList
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(BaseFolderSchema.ReplicaList);
			}
			set
			{
				base.PropertyBag[BaseFolderSchema.ReplicaList] = value;
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000ACD70 File Offset: 0x000AAF70
		internal override void AddExtendedPropertyValue(ExtendedPropertyType extendedPropertyToAdd)
		{
			ExtendedPropertyType[] extendedProperty = this.ExtendedProperty;
			int num = (extendedProperty == null) ? 0 : extendedProperty.Length;
			ExtendedPropertyType[] array = new ExtendedPropertyType[num + 1];
			if (num > 0)
			{
				Array.Copy(extendedProperty, array, num);
			}
			array[num] = extendedPropertyToAdd;
			this.ExtendedProperty = array;
		}

		// Token: 0x040019F2 RID: 6642
		private static LazyMember<Dictionary<StoreObjectType, Func<BaseFolderType>>> createMethods = new LazyMember<Dictionary<StoreObjectType, Func<BaseFolderType>>>(delegate()
		{
			Dictionary<StoreObjectType, Func<BaseFolderType>> dictionary = new Dictionary<StoreObjectType, Func<BaseFolderType>>();
			dictionary.Add(StoreObjectType.Folder, () => new FolderType());
			dictionary.Add(StoreObjectType.CalendarFolder, () => new CalendarFolderType());
			dictionary.Add(StoreObjectType.ContactsFolder, () => new ContactsFolderType());
			dictionary.Add(StoreObjectType.JournalFolder, () => new FolderType());
			dictionary.Add(StoreObjectType.NotesFolder, () => new FolderType());
			dictionary.Add(StoreObjectType.OutlookSearchFolder, () => new SearchFolderType());
			dictionary.Add(StoreObjectType.SearchFolder, () => new SearchFolderType());
			dictionary.Add(StoreObjectType.TasksFolder, () => new TasksFolderType());
			return dictionary;
		});
	}
}
