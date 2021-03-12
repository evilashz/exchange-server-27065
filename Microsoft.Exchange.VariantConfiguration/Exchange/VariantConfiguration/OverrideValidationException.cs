using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000002 RID: 2
	public abstract class OverrideValidationException : Exception
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public OverrideValidationException(string message, VariantConfigurationOverride o, Exception innerException = null) : base(message, innerException)
		{
			this.Override = o;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020E9 File Offset: 0x000002E9
		public VariantConfigurationOverride Override { get; private set; }
	}
}
