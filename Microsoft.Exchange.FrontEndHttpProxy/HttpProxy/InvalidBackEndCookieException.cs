using System;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	internal class InvalidBackEndCookieException : Exception
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000BE15 File Offset: 0x0000A015
		public InvalidBackEndCookieException() : base(InvalidBackEndCookieException.ErrorMessage)
		{
		}

		// Token: 0x04000117 RID: 279
		private static readonly string ErrorMessage = "Invalid back end cookie entry.";
	}
}
