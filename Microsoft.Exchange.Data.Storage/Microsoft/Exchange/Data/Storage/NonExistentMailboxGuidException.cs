using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D78 RID: 3448
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NonExistentMailboxGuidException : StoragePermanentException
	{
		// Token: 0x0600770E RID: 30478 RVA: 0x0020DA95 File Offset: 0x0020BC95
		internal NonExistentMailboxGuidException(Guid mailboxGuid) : base(LocalizedString.Empty)
		{
			this.MailboxGuid = mailboxGuid;
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x0020DAA9 File Offset: 0x0020BCA9
		private NonExistentMailboxGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x06007710 RID: 30480 RVA: 0x0020DAB3 File Offset: 0x0020BCB3
		// (set) Token: 0x06007711 RID: 30481 RVA: 0x0020DABB File Offset: 0x0020BCBB
		public Guid MailboxGuid { get; private set; }
	}
}
