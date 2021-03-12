using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BFB RID: 3067
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InvalidRuleException : StoragePermanentException
	{
		// Token: 0x06006D6C RID: 28012 RVA: 0x001D4020 File Offset: 0x001D2220
		public InvalidRuleException(string message) : base(new LocalizedString(message))
		{
		}

		// Token: 0x06006D6D RID: 28013 RVA: 0x001D402E File Offset: 0x001D222E
		public InvalidRuleException(string message, Exception innerException) : base(new LocalizedString(message), innerException)
		{
		}
	}
}
