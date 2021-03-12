using System;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000020 RID: 32
	internal class AsyncQueueRequestExistsException : Exception
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000045E8 File Offset: 0x000027E8
		public AsyncQueueRequestExistsException() : base("Request exists for the Tenant/Owner combination specified.")
		{
		}

		// Token: 0x0400007E RID: 126
		private const string ErrorMessage = "Request exists for the Tenant/Owner combination specified.";
	}
}
