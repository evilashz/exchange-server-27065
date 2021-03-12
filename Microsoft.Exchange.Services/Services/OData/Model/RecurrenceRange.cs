using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E81 RID: 3713
	internal class RecurrenceRange
	{
		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x0012D87F File Offset: 0x0012BA7F
		// (set) Token: 0x0600609F RID: 24735 RVA: 0x0012D887 File Offset: 0x0012BA87
		public RecurrenceRangeType Type { get; set; }

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x0012D890 File Offset: 0x0012BA90
		// (set) Token: 0x060060A1 RID: 24737 RVA: 0x0012D898 File Offset: 0x0012BA98
		public DateTimeOffset StartDate { get; set; }

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x060060A2 RID: 24738 RVA: 0x0012D8A1 File Offset: 0x0012BAA1
		// (set) Token: 0x060060A3 RID: 24739 RVA: 0x0012D8A9 File Offset: 0x0012BAA9
		public DateTimeOffset EndDate { get; set; }

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x060060A4 RID: 24740 RVA: 0x0012D8B2 File Offset: 0x0012BAB2
		// (set) Token: 0x060060A5 RID: 24741 RVA: 0x0012D8BA File Offset: 0x0012BABA
		public int NumberOfOccurrences { get; set; }

		// Token: 0x04003468 RID: 13416
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(RecurrenceRange).Namespace, typeof(RecurrenceRange).Name, null, false);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Type", new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(RecurrenceRangeType)), true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "StartDate", EdmCoreModel.Instance.GetDateTimeOffset(true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "EndDate", EdmCoreModel.Instance.GetDateTimeOffset(true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "NumberOfOccurrences", EdmCoreModel.Instance.GetInt32(false)));
			return edmComplexType;
		});
	}
}
