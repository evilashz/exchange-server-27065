using System;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000119 RID: 281
	public class SenderNotUniqueException : Exception
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x00023369 File Offset: 0x00021569
		public SenderNotUniqueException(string message) : base(message)
		{
		}
	}
}
