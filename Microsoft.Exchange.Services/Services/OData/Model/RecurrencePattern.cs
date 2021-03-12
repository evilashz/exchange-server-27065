using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E80 RID: 3712
	internal class RecurrencePattern
	{
		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x0600608D RID: 24717 RVA: 0x0012D6AB File Offset: 0x0012B8AB
		// (set) Token: 0x0600608E RID: 24718 RVA: 0x0012D6B3 File Offset: 0x0012B8B3
		public RecurrencePatternType Type { get; set; }

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x0600608F RID: 24719 RVA: 0x0012D6BC File Offset: 0x0012B8BC
		// (set) Token: 0x06006090 RID: 24720 RVA: 0x0012D6C4 File Offset: 0x0012B8C4
		public int Interval { get; set; }

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06006091 RID: 24721 RVA: 0x0012D6CD File Offset: 0x0012B8CD
		// (set) Token: 0x06006092 RID: 24722 RVA: 0x0012D6D5 File Offset: 0x0012B8D5
		public int Month { get; set; }

		// Token: 0x1700163A RID: 5690
		// (get) Token: 0x06006093 RID: 24723 RVA: 0x0012D6DE File Offset: 0x0012B8DE
		// (set) Token: 0x06006094 RID: 24724 RVA: 0x0012D6E6 File Offset: 0x0012B8E6
		public int DayOfMonth { get; set; }

		// Token: 0x1700163B RID: 5691
		// (get) Token: 0x06006095 RID: 24725 RVA: 0x0012D6EF File Offset: 0x0012B8EF
		// (set) Token: 0x06006096 RID: 24726 RVA: 0x0012D6F7 File Offset: 0x0012B8F7
		public ISet<DayOfWeek> DaysOfWeek { get; set; }

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06006097 RID: 24727 RVA: 0x0012D700 File Offset: 0x0012B900
		// (set) Token: 0x06006098 RID: 24728 RVA: 0x0012D708 File Offset: 0x0012B908
		public DayOfWeek FirstDayOfWeek { get; set; }

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06006099 RID: 24729 RVA: 0x0012D711 File Offset: 0x0012B911
		// (set) Token: 0x0600609A RID: 24730 RVA: 0x0012D719 File Offset: 0x0012B919
		public WeekIndex Index { get; set; }

		// Token: 0x0400345F RID: 13407
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(RecurrencePattern).Namespace, typeof(RecurrencePattern).Name, null, false);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Type", new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(RecurrencePatternType)), true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Interval", EdmCoreModel.Instance.GetInt32(false)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "DayOfMonth", EdmCoreModel.Instance.GetInt32(false)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Month", EdmCoreModel.Instance.GetInt32(false)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "DaysOfWeek", new EdmCollectionTypeReference(new EdmCollectionType(new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(DayOfWeek)), true)))));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "FirstDayOfWeek", new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(DayOfWeek)), true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Index", new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(WeekIndex)), true)));
			return edmComplexType;
		});
	}
}
