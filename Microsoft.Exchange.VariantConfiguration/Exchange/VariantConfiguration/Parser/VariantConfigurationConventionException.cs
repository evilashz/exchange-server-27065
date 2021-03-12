using System;

namespace Microsoft.Exchange.VariantConfiguration.Parser
{
	// Token: 0x02000135 RID: 309
	public class VariantConfigurationConventionException : Exception
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00023854 File Offset: 0x00021A54
		public VariantConfigurationConventionException(string errorMessage) : this(errorMessage, null)
		{
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002385E File Offset: 0x00021A5E
		public VariantConfigurationConventionException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
		}
	}
}
