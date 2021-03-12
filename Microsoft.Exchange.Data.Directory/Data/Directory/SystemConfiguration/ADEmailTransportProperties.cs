using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000346 RID: 838
	internal class ADEmailTransportProperties : ADPropertyUnionSchema
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x000A4FF0 File Offset: 0x000A31F0
		public override ReadOnlyCollection<ADObjectSchema> ObjectSchemas
		{
			get
			{
				return ADEmailTransportProperties.allEmailTransportSchemas;
			}
		}

		// Token: 0x040017BF RID: 6079
		private static ReadOnlyCollection<ADObjectSchema> allEmailTransportSchemas = new ReadOnlyCollection<ADObjectSchema>(new ADObjectSchema[]
		{
			ObjectSchema.GetInstance<Pop3AdConfigurationSchema>(),
			ObjectSchema.GetInstance<Imap4AdConfigurationSchema>()
		});
	}
}
