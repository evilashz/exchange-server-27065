using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200009A RID: 154
	internal static class ProcessWideServiceType
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x000115FB File Offset: 0x0000F7FB
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00011604 File Offset: 0x0000F804
		public static ServiceType ServiceType
		{
			get
			{
				return ProcessWideServiceType.serviceType;
			}
			set
			{
				lock (ProcessWideServiceType.serviceTypeLock)
				{
					ProcessWideServiceType.serviceType = value;
				}
			}
		}

		// Token: 0x04000349 RID: 841
		private static ServiceType serviceType = ServiceType.EOPService;

		// Token: 0x0400034A RID: 842
		private static object serviceTypeLock = new object();

		// Token: 0x0400034B RID: 843
		public static readonly HygienePropertyDefinition ServiceTypeProp = new HygienePropertyDefinition("serviceType", typeof(ServiceType));
	}
}
