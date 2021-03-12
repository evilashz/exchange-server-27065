using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.Shared.SubmissionItem
{
	// Token: 0x0200002E RID: 46
	internal class QuarantineMailboxConfigurationLoadException : LocalizedException
	{
		// Token: 0x0600014E RID: 334 RVA: 0x0000811A File Offset: 0x0000631A
		internal QuarantineMailboxConfigurationLoadException(string errorString) : base(new LocalizedString(errorString))
		{
		}
	}
}
