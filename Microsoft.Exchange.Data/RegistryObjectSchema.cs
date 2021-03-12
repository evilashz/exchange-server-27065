using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000298 RID: 664
	internal abstract class RegistryObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x0600180B RID: 6155
		public abstract string DefaultRegistryKeyPath { get; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600180C RID: 6156
		public abstract string DefaultName { get; }
	}
}
