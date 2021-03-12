using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002CF RID: 719
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToApplyFolderHierarchyChangesTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x0004F143 File Offset: 0x0004D343
		public UnableToApplyFolderHierarchyChangesTransientException() : base(MrsStrings.UnableToApplyFolderHierarchyChanges)
		{
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0004F150 File Offset: 0x0004D350
		public UnableToApplyFolderHierarchyChangesTransientException(Exception innerException) : base(MrsStrings.UnableToApplyFolderHierarchyChanges, innerException)
		{
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x0004F15E File Offset: 0x0004D35E
		protected UnableToApplyFolderHierarchyChangesTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x0004F168 File Offset: 0x0004D368
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
