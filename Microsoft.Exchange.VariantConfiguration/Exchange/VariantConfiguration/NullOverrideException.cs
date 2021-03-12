using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200012B RID: 299
	public sealed class NullOverrideException : OverrideValidationException
	{
		// Token: 0x06000E4A RID: 3658 RVA: 0x00022881 File Offset: 0x00020A81
		public NullOverrideException(Exception innerException = null) : base("A null override is passed in for validation.", null, innerException)
		{
		}
	}
}
