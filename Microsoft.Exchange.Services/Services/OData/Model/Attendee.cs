using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E6B RID: 3691
	internal class Attendee : Recipient
	{
		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x0012C3F3 File Offset: 0x0012A5F3
		// (set) Token: 0x06006024 RID: 24612 RVA: 0x0012C3FB File Offset: 0x0012A5FB
		public ResponseStatus Status { get; set; }

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06006025 RID: 24613 RVA: 0x0012C404 File Offset: 0x0012A604
		// (set) Token: 0x06006026 RID: 24614 RVA: 0x0012C40C File Offset: 0x0012A60C
		public AttendeeType Type { get; set; }

		// Token: 0x04003435 RID: 13365
		internal new static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(Attendee).Namespace, typeof(Attendee).Name, Recipient.EdmComplexType.Member, false);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Status", new EdmComplexTypeReference(ResponseStatus.EdmComplexType.Member, true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Type", new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(AttendeeType)), true)));
			return edmComplexType;
		});
	}
}
