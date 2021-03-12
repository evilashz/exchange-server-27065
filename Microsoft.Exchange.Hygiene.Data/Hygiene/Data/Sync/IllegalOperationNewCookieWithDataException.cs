using System;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200021C RID: 540
	internal class IllegalOperationNewCookieWithDataException : Exception
	{
		// Token: 0x06001633 RID: 5683 RVA: 0x00045096 File Offset: 0x00043296
		public IllegalOperationNewCookieWithDataException() : base("Attempted to create a new cookie with a non-null data field.")
		{
		}

		// Token: 0x04000B3F RID: 2879
		private const string ErrorMessage = "Attempted to create a new cookie with a non-null data field.";
	}
}
