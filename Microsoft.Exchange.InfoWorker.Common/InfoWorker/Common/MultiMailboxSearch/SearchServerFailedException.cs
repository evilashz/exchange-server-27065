using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001CD RID: 461
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchServerFailedException : MultiMailboxSearchException
	{
		// Token: 0x06000C47 RID: 3143 RVA: 0x000355B1 File Offset: 0x000337B1
		public SearchServerFailedException(MailboxInfo info, int responseCode, string exceptionMessage) : base(Strings.SearchServerFailed(info.DisplayName, responseCode, exceptionMessage))
		{
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000355C6 File Offset: 0x000337C6
		protected SearchServerFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
