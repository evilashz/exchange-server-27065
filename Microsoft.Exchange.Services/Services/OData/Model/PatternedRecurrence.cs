using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E7F RID: 3711
	internal class PatternedRecurrence
	{
		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06006086 RID: 24710 RVA: 0x0012D5DF File Offset: 0x0012B7DF
		// (set) Token: 0x06006087 RID: 24711 RVA: 0x0012D5E7 File Offset: 0x0012B7E7
		public RecurrencePattern Pattern { get; set; }

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06006088 RID: 24712 RVA: 0x0012D5F0 File Offset: 0x0012B7F0
		// (set) Token: 0x06006089 RID: 24713 RVA: 0x0012D5F8 File Offset: 0x0012B7F8
		public RecurrenceRange Range { get; set; }

		// Token: 0x0400345B RID: 13403
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(PatternedRecurrence).Namespace, typeof(PatternedRecurrence).Name, null, false);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Pattern", new EdmComplexTypeReference(RecurrencePattern.EdmComplexType.Member, true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Range", new EdmComplexTypeReference(RecurrenceRange.EdmComplexType.Member, true)));
			return edmComplexType;
		});
	}
}
