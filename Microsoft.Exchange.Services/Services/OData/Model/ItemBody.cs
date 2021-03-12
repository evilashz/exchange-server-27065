using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E6F RID: 3695
	internal class ItemBody
	{
		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06006036 RID: 24630 RVA: 0x0012C7C1 File Offset: 0x0012A9C1
		// (set) Token: 0x06006037 RID: 24631 RVA: 0x0012C7C9 File Offset: 0x0012A9C9
		public BodyType ContentType { get; set; }

		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06006038 RID: 24632 RVA: 0x0012C7D2 File Offset: 0x0012A9D2
		// (set) Token: 0x06006039 RID: 24633 RVA: 0x0012C7DA File Offset: 0x0012A9DA
		public string Content { get; set; }

		// Token: 0x0400343B RID: 13371
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(ItemBody).Namespace, typeof(ItemBody).Name);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "ContentType", new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(BodyType)), true)));
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "Content", EdmCoreModel.Instance.GetString(true)));
			return edmComplexType;
		});
	}
}
