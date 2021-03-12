using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000788 RID: 1928
	[Serializable]
	public class UserHasNoMailboxException : ObjectNotFoundException
	{
		// Token: 0x060048F4 RID: 18676 RVA: 0x00131A68 File Offset: 0x0012FC68
		public UserHasNoMailboxException() : base(ServerStrings.ADUserNoMailbox)
		{
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x00131A75 File Offset: 0x0012FC75
		public UserHasNoMailboxException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x00131A7F File Offset: 0x0012FC7F
		protected UserHasNoMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
