using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E72 RID: 3698
	internal class Location
	{
		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x0012CA76 File Offset: 0x0012AC76
		// (set) Token: 0x06006045 RID: 24645 RVA: 0x0012CA7E File Offset: 0x0012AC7E
		public string DisplayName { get; set; }

		// Token: 0x0400343F RID: 13375
		internal static readonly LazyMember<EdmComplexType> EdmComplexType = new LazyMember<EdmComplexType>(delegate()
		{
			EdmComplexType edmComplexType = new EdmComplexType(typeof(Location).Namespace, typeof(Location).Name);
			edmComplexType.AddProperty(new EdmStructuralProperty(edmComplexType, "DisplayName", EdmCoreModel.Instance.GetString(true)));
			return edmComplexType;
		});
	}
}
