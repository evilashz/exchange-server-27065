using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x020001AB RID: 427
	public class LazyWriterException : Exception
	{
		// Token: 0x060010C6 RID: 4294 RVA: 0x00062618 File Offset: 0x00060818
		public LazyWriterException(Exception innerException) : base(innerException.Message, innerException)
		{
		}
	}
}
