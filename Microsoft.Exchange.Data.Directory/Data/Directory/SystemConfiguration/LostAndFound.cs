using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A4 RID: 1188
	[Serializable]
	public class LostAndFound : ADConfigurationObject
	{
		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000D5350 File Offset: 0x000D3550
		internal override ADObjectSchema Schema
		{
			get
			{
				return LostAndFound.schema;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x000D5357 File Offset: 0x000D3557
		internal override string MostDerivedObjectClass
		{
			get
			{
				return LostAndFound.MostDerivedClass;
			}
		}

		// Token: 0x040024A0 RID: 9376
		private static LostAndFoundSchema schema = ObjectSchema.GetInstance<LostAndFoundSchema>();

		// Token: 0x040024A1 RID: 9377
		internal static string MostDerivedClass = "lostandfound";
	}
}
