using System;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200021D RID: 541
	internal class IllegalOperationNoCallerSpecifiedException : Exception
	{
		// Token: 0x06001634 RID: 5684 RVA: 0x000450A3 File Offset: 0x000432A3
		public IllegalOperationNoCallerSpecifiedException() : base("No caller specified")
		{
		}

		// Token: 0x04000B40 RID: 2880
		private const string ErrorMessage = "No caller specified";
	}
}
