using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F8 RID: 760
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DestinationFolderHierarchyInconsistentTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002494 RID: 9364 RVA: 0x00050372 File Offset: 0x0004E572
		public DestinationFolderHierarchyInconsistentTransientException() : base(MrsStrings.DestinationFolderHierarchyInconsistent)
		{
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x0005037F File Offset: 0x0004E57F
		public DestinationFolderHierarchyInconsistentTransientException(Exception innerException) : base(MrsStrings.DestinationFolderHierarchyInconsistent, innerException)
		{
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x0005038D File Offset: 0x0004E58D
		protected DestinationFolderHierarchyInconsistentTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00050397 File Offset: 0x0004E597
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
