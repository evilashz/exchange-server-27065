using System;
using Microsoft.Exchange.Assistants.EventLog;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000054 RID: 84
	internal sealed class MissingSystemMailboxException : MissingObjectException
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000ED4E File Offset: 0x0000CF4E
		public MissingSystemMailboxException(string databaseName, EventLogger logger) : this(databaseName, null, logger)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		public MissingSystemMailboxException(string databaseName, Exception innerException, EventLogger logger) : base(Strings.descMissingSystemMailbox(databaseName), innerException)
		{
			logger.LogEvent(AssistantsEventLogConstants.Tuple_MissingSystemMailbox, databaseName, new object[]
			{
				databaseName,
				(innerException != null) ? innerException.ToString() : string.Empty
			});
		}
	}
}
