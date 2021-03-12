using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000126 RID: 294
	internal class OwaUserHasNoMailboxAndNoLicenseAssignedException : OwaPermanentException
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x00022D3A File Offset: 0x00020F3A
		public OwaUserHasNoMailboxAndNoLicenseAssignedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
