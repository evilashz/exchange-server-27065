using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A5F RID: 2655
	[Serializable]
	public abstract class VersionedXmlConfigurationObject : XsoMailboxConfigurationObject
	{
		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x060060E0 RID: 24800
		internal abstract string UserConfigurationName { get; }

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x060060E1 RID: 24801
		internal abstract ProviderPropertyDefinition RawVersionedXmlPropertyDefinition { get; }
	}
}
