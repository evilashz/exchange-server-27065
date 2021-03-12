using System;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x0200021A RID: 538
	internal class IllegalOperationCallerNotCookieOwnerException : Exception
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x0004507C File Offset: 0x0004327C
		public IllegalOperationCallerNotCookieOwnerException() : base("Calling daemon is not the cookie owner")
		{
		}

		// Token: 0x04000B3D RID: 2877
		private const string ErrorMessage = "Calling daemon is not the cookie owner";
	}
}
