using System;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000026 RID: 38
	internal class AsyncQueueStaleObjectException : Exception
	{
		// Token: 0x06000153 RID: 339 RVA: 0x000058F1 File Offset: 0x00003AF1
		public AsyncQueueStaleObjectException() : base("The AsyncQueue Request object has been modified in the background. Please refresh the object and save.")
		{
		}

		// Token: 0x040000A0 RID: 160
		private const string ErrorMessage = "The AsyncQueue Request object has been modified in the background. Please refresh the object and save.";
	}
}
