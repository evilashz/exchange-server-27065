using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001CC RID: 460
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchUserNotFoundException : MultiMailboxSearchException
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x00035594 File Offset: 0x00033794
		public SearchUserNotFoundException(MailboxInfo info) : base(Strings.SearchUserNotFound(info.DisplayName))
		{
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000355A7 File Offset: 0x000337A7
		protected SearchUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
