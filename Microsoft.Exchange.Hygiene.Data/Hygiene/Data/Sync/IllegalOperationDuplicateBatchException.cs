using System;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200021B RID: 539
	internal class IllegalOperationDuplicateBatchException : Exception
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x00045089 File Offset: 0x00043289
		public IllegalOperationDuplicateBatchException() : base("Calling daemon specified a duplicate batch id")
		{
		}

		// Token: 0x04000B3E RID: 2878
		private const string ErrorMessage = "Calling daemon specified a duplicate batch id";
	}
}
