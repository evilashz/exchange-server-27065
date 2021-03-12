using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200039C RID: 924
	internal class ADVirtualDirectoryProperties : ADPropertyUnionSchema
	{
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000B2CED File Offset: 0x000B0EED
		public override ReadOnlyCollection<ADObjectSchema> ObjectSchemas
		{
			get
			{
				return ADVirtualDirectoryProperties.allVirtualDirectorySchemas;
			}
		}

		// Token: 0x04001999 RID: 6553
		private static ReadOnlyCollection<ADObjectSchema> allVirtualDirectorySchemas = new ReadOnlyCollection<ADObjectSchema>(new ADObjectSchema[]
		{
			ObjectSchema.GetInstance<ADAutodiscoverVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADMobileVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADWebServicesVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADOwaVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADRpcHttpVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADO365SuiteServiceVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADSnackyServiceVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADOabVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADAvailabilityForeignConnectorVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADEcpVirtualDirectorySchema>(),
			ObjectSchema.GetInstance<ADMapiVirtualDirectorySchema>()
		});
	}
}
