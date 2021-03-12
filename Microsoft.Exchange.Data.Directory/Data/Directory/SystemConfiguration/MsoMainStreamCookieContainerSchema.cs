using System;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000504 RID: 1284
	internal sealed class MsoMainStreamCookieContainerSchema : SyncServiceInstanceSchema
	{
		// Token: 0x040026EC RID: 9964
		public static readonly ADPropertyDefinition MsoForwardSyncRecipientCookie = new ADPropertyDefinition("MsoForwardSyncRecipientCookie", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchMsoForwardSyncRecipientCookie", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 49152)
		}, null, null);

		// Token: 0x040026ED RID: 9965
		public static readonly ADPropertyDefinition MsoForwardSyncNonRecipientCookie = new ADPropertyDefinition("MsoForwardSyncNonRecipientCookie", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchMsoForwardSyncNonRecipientCookie", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ByteArrayLengthConstraint(1, 49152)
		}, null, null);

		// Token: 0x040026EE RID: 9966
		public static readonly ADPropertyDefinition MsoForwardSyncObjectFullSyncRequests = new ADPropertyDefinition("MsoForwardSyncObjectFullSyncRequests", ExchangeObjectVersion.Exchange2003, typeof(FullSyncObjectRequest), "msExchMsoForwardSyncReplayList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
