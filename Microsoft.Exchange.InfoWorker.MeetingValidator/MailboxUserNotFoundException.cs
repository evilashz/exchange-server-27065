using System;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000038 RID: 56
	internal class MailboxUserNotFoundException : Exception
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000C518 File Offset: 0x0000A718
		internal MailboxUserNotFoundException(string user) : base(string.Format("User not found {0}", user))
		{
		}
	}
}
