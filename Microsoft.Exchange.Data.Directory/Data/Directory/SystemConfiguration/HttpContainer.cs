using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000486 RID: 1158
	[ObjectScope(ConfigScopes.Server)]
	[Serializable]
	public class HttpContainer : Container
	{
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000D1862 File Offset: 0x000CFA62
		internal override ADObjectSchema Schema
		{
			get
			{
				return HttpContainer.schema;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000D1869 File Offset: 0x000CFA69
		internal override string MostDerivedObjectClass
		{
			get
			{
				return HttpContainer.mostDerivedClass;
			}
		}

		// Token: 0x040023DB RID: 9179
		private static HttpContainerSchema schema = ObjectSchema.GetInstance<HttpContainerSchema>();

		// Token: 0x040023DC RID: 9180
		private static string mostDerivedClass = "msExchProtocolCfgHTTPContainer";
	}
}
