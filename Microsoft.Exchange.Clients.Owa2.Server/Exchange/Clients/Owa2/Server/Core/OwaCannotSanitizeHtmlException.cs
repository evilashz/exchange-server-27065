using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public sealed class OwaCannotSanitizeHtmlException : OwaPermanentException
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x00022A07 File Offset: 0x00020C07
		public OwaCannotSanitizeHtmlException(string message, Exception innerException, object thisObject) : base(message, innerException, thisObject)
		{
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00022A12 File Offset: 0x00020C12
		public OwaCannotSanitizeHtmlException(string message) : base(message)
		{
		}
	}
}
