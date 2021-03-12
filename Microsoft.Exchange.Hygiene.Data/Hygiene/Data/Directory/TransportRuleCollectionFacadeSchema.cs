using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Transport;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x0200010F RID: 271
	internal class TransportRuleCollectionFacadeSchema : ADObjectSchema
	{
		// Token: 0x04000562 RID: 1378
		public static readonly HygienePropertyDefinition FileData = new HygienePropertyDefinition("FileData", typeof(byte[]));

		// Token: 0x04000563 RID: 1379
		public static readonly HygienePropertyDefinition MigrationSource = new HygienePropertyDefinition("MigrationSource", typeof(MigrationSourceType));
	}
}
