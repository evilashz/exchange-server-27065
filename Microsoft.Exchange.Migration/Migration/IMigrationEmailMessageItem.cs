using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000BF RID: 191
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IMigrationEmailMessageItem : IMigrationAttachmentMessage, IDisposable
	{
		// Token: 0x06000A31 RID: 2609
		void Send(IEnumerable<SmtpAddress> toAddresses, string subject, string body);
	}
}
