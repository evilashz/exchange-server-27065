using System;

namespace Microsoft.Exchange.VariantConfiguration.Parser
{
	// Token: 0x02000136 RID: 310
	public class VariantConfigurationIniParseException : Exception
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x00023868 File Offset: 0x00021A68
		public VariantConfigurationIniParseException(string errorMessage) : this(errorMessage, null)
		{
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00023872 File Offset: 0x00021A72
		public VariantConfigurationIniParseException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
		}
	}
}
