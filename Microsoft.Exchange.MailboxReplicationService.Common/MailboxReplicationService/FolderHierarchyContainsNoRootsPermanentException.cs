using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002C0 RID: 704
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderHierarchyContainsNoRootsPermanentException : FolderHierarchyIsInconsistentPermanentException
	{
		// Token: 0x06002383 RID: 9091 RVA: 0x0004EA5B File Offset: 0x0004CC5B
		public FolderHierarchyContainsNoRootsPermanentException() : base(MrsStrings.FolderHierarchyContainsNoRoots)
		{
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x0004EA68 File Offset: 0x0004CC68
		public FolderHierarchyContainsNoRootsPermanentException(Exception innerException) : base(MrsStrings.FolderHierarchyContainsNoRoots, innerException)
		{
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x0004EA76 File Offset: 0x0004CC76
		protected FolderHierarchyContainsNoRootsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x0004EA80 File Offset: 0x0004CC80
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
