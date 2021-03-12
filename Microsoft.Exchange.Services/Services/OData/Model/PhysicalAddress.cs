using System;
using System.Collections.Generic;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E7D RID: 3709
	internal class PhysicalAddress
	{
		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x0600606D RID: 24685 RVA: 0x0012D106 File Offset: 0x0012B306
		// (set) Token: 0x0600606E RID: 24686 RVA: 0x0012D10E File Offset: 0x0012B30E
		internal List<PhysicalAddressFields> Properties { get; set; }

		// Token: 0x0600606F RID: 24687 RVA: 0x0012D117 File Offset: 0x0012B317
		public PhysicalAddress()
		{
			this.Properties = new List<PhysicalAddressFields>();
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06006070 RID: 24688 RVA: 0x0012D12A File Offset: 0x0012B32A
		// (set) Token: 0x06006071 RID: 24689 RVA: 0x0012D132 File Offset: 0x0012B332
		public string Street { get; set; }

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x0012D13B File Offset: 0x0012B33B
		// (set) Token: 0x06006073 RID: 24691 RVA: 0x0012D143 File Offset: 0x0012B343
		public string City { get; set; }

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06006074 RID: 24692 RVA: 0x0012D14C File Offset: 0x0012B34C
		// (set) Token: 0x06006075 RID: 24693 RVA: 0x0012D154 File Offset: 0x0012B354
		public string State { get; set; }

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06006076 RID: 24694 RVA: 0x0012D15D File Offset: 0x0012B35D
		// (set) Token: 0x06006077 RID: 24695 RVA: 0x0012D165 File Offset: 0x0012B365
		public string CountryOrRegion { get; set; }

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06006078 RID: 24696 RVA: 0x0012D16E File Offset: 0x0012B36E
		// (set) Token: 0x06006079 RID: 24697 RVA: 0x0012D176 File Offset: 0x0012B376
		public string PostalCode { get; set; }

		// Token: 0x0400344E RID: 13390
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(PhysicalAddress).Namespace, typeof(PhysicalAddress).Name);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, PhysicalAddressFields.Street.ToString(), EdmCoreModel.Instance.GetString(true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, PhysicalAddressFields.City.ToString(), EdmCoreModel.Instance.GetString(true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, PhysicalAddressFields.State.ToString(), EdmCoreModel.Instance.GetString(true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, PhysicalAddressFields.CountryOrRegion.ToString(), EdmCoreModel.Instance.GetString(true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, PhysicalAddressFields.PostalCode.ToString(), EdmCoreModel.Instance.GetString(true)));
			return edmComplexType;
		});
	}
}
