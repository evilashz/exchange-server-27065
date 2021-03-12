using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200002D RID: 45
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorEmailMessageItem : IAnchorAttachmentMessage, IDisposable
	{
		// Token: 0x060001F1 RID: 497
		void Send(IEnumerable<SmtpAddress> toAddresses, string subject, string body);
	}
}
