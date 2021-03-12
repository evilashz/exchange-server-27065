using System;

namespace Microsoft.Exchange.VariantConfiguration.Parser
{
	// Token: 0x02000137 RID: 311
	public class VariantConfigurationSyntaxException : Exception
	{
		// Token: 0x06000E94 RID: 3732 RVA: 0x0002387C File Offset: 0x00021A7C
		public VariantConfigurationSyntaxException(string errorMessage) : this(errorMessage, null)
		{
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00023886 File Offset: 0x00021A86
		public VariantConfigurationSyntaxException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
		}
	}
}
