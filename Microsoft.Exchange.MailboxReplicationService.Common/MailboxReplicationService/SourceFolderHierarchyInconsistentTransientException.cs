using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F7 RID: 759
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceFolderHierarchyInconsistentTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002490 RID: 9360 RVA: 0x00050343 File Offset: 0x0004E543
		public SourceFolderHierarchyInconsistentTransientException() : base(MrsStrings.SourceFolderHierarchyInconsistent)
		{
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00050350 File Offset: 0x0004E550
		public SourceFolderHierarchyInconsistentTransientException(Exception innerException) : base(MrsStrings.SourceFolderHierarchyInconsistent, innerException)
		{
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0005035E File Offset: 0x0004E55E
		protected SourceFolderHierarchyInconsistentTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00050368 File Offset: 0x0004E568
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
