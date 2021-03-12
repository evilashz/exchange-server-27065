using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000286 RID: 646
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IUserConfiguration : IReadableUserConfiguration, IDisposable
	{
		// Token: 0x06001ACC RID: 6860
		void Save();

		// Token: 0x06001ACD RID: 6861
		ConflictResolutionResult Save(SaveMode saveMode);

		// Token: 0x06001ACE RID: 6862
		ConfigurationDictionary GetConfigurationDictionary();
	}
}
