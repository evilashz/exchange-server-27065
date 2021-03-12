using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C9 RID: 713
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderIsMissingTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060023B2 RID: 9138 RVA: 0x0004EF49 File Offset: 0x0004D149
		public FolderIsMissingTransientException() : base(MrsStrings.FolderIsMissingInMerge)
		{
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0004EF56 File Offset: 0x0004D156
		public FolderIsMissingTransientException(Exception innerException) : base(MrsStrings.FolderIsMissingInMerge, innerException)
		{
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0004EF64 File Offset: 0x0004D164
		protected FolderIsMissingTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0004EF6E File Offset: 0x0004D16E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
