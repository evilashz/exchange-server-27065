using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000097 RID: 151
	internal interface IJobSubscriptionSettings : ISubscriptionSettings, IMigrationSerializable
	{
		// Token: 0x060008C3 RID: 2243
		void WriteToBatch(MigrationBatch batch);

		// Token: 0x060008C4 RID: 2244
		void WriteExtendedProperties(PersistableDictionary dictionary);

		// Token: 0x060008C5 RID: 2245
		bool ReadExtendedProperties(PersistableDictionary dictionary);
	}
}
