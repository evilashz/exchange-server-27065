using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200029E RID: 670
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderFilterPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060022D7 RID: 8919 RVA: 0x0004D954 File Offset: 0x0004BB54
		public FolderFilterPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0004D95D File Offset: 0x0004BB5D
		public FolderFilterPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0004D967 File Offset: 0x0004BB67
		protected FolderFilterPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x0004D971 File Offset: 0x0004BB71
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
