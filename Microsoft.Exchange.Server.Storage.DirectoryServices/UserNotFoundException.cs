using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x0200000F RID: 15
	public class UserNotFoundException : StoreException
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000023C6 File Offset: 0x000005C6
		public UserNotFoundException(LID lid, Guid userMailboxGuid) : base(lid, ErrorCodeValue.UnknownUser, string.Format("User not found: {0}", userMailboxGuid))
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000023E4 File Offset: 0x000005E4
		public UserNotFoundException(LID lid, string userId) : base(lid, ErrorCodeValue.UnknownUser, string.Format("User not found: {0}", userId))
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000023FD File Offset: 0x000005FD
		public UserNotFoundException(LID lid, Guid userMailboxGuid, Exception innerException) : base(lid, ErrorCodeValue.UnknownUser, string.Format("User not found: {0}", userMailboxGuid), innerException)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000241C File Offset: 0x0000061C
		public UserNotFoundException(LID lid, string userId, Exception innerException) : base(lid, ErrorCodeValue.UnknownUser, string.Format("User not found: {0}", userId), innerException)
		{
		}

		// Token: 0x0400000E RID: 14
		private const string UserNotFoundTemplate = "User not found: {0}";
	}
}
