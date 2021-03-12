using System;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x0200011C RID: 284
	internal class CommonDomainProperties
	{
		// Token: 0x04000591 RID: 1425
		public static readonly HygienePropertyDefinition CallerId = new HygienePropertyDefinition("CallerId", typeof(string));

		// Token: 0x04000592 RID: 1426
		public static readonly HygienePropertyDefinition TransactionId = new HygienePropertyDefinition("TransactionId", typeof(string));
	}
}
