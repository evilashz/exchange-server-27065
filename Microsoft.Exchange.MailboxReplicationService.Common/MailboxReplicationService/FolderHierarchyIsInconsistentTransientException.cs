using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002BF RID: 703
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderHierarchyIsInconsistentTransientException : MailboxReplicationTransientException
	{
		// Token: 0x0600237F RID: 9087 RVA: 0x0004EA34 File Offset: 0x0004CC34
		public FolderHierarchyIsInconsistentTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0004EA3D File Offset: 0x0004CC3D
		public FolderHierarchyIsInconsistentTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x0004EA47 File Offset: 0x0004CC47
		protected FolderHierarchyIsInconsistentTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x0004EA51 File Offset: 0x0004CC51
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
