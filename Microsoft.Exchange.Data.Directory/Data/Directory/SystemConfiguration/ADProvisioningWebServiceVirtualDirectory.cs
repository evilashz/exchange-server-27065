using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200037D RID: 893
	[Serializable]
	public sealed class ADProvisioningWebServiceVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x000AD828 File Offset: 0x000ABA28
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADProvisioningWebServiceVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x0400192E RID: 6446
		public static readonly string MostDerivedClass = "msExchVirtualDirectory";
	}
}
