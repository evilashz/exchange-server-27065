using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200001D RID: 29
	internal abstract class AssistantsRpcErrorCode
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x000053D8 File Offset: 0x000035D8
		public static int GetHRFromException(Exception ex)
		{
			if (ex is MailboxOrDatabaseNotSpecifiedException)
			{
				return -2147220991;
			}
			if (ex is UnknownAssistantException)
			{
				return -2147220990;
			}
			if (ex is UnknownDatabaseException)
			{
				return -2147220989;
			}
			if (ex is TransientException)
			{
				return -2147220988;
			}
			return -2147220992;
		}

		// Token: 0x040000EA RID: 234
		public const int E_GENERIC = -2147220992;

		// Token: 0x040000EB RID: 235
		public const int E_MAILBOXORDATABASENOTSPECIFIED = -2147220991;

		// Token: 0x040000EC RID: 236
		public const int E_UNKNOWNASSISTANT = -2147220990;

		// Token: 0x040000ED RID: 237
		public const int E_UNKNOWNDATABASE = -2147220989;

		// Token: 0x040000EE RID: 238
		public const int E_TRANSIENT = -2147220988;
	}
}
