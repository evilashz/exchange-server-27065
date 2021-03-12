using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E6A RID: 3690
	internal class Recipient
	{
		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x0600601C RID: 24604 RVA: 0x0012C333 File Offset: 0x0012A533
		// (set) Token: 0x0600601D RID: 24605 RVA: 0x0012C33B File Offset: 0x0012A53B
		public string Name { get; set; }

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x0600601E RID: 24606 RVA: 0x0012C344 File Offset: 0x0012A544
		// (set) Token: 0x0600601F RID: 24607 RVA: 0x0012C34C File Offset: 0x0012A54C
		public string Address { get; set; }

		// Token: 0x04003431 RID: 13361
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(Recipient).Namespace, typeof(Recipient).Name);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Name", EdmCoreModel.Instance.GetString(true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Address", EdmCoreModel.Instance.GetString(true)));
			return edmComplexType;
		});
	}
}
