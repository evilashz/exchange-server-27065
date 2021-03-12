using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002BE RID: 702
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderHierarchyIsInconsistentPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600237B RID: 9083 RVA: 0x0004EA0D File Offset: 0x0004CC0D
		public FolderHierarchyIsInconsistentPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x0004EA16 File Offset: 0x0004CC16
		public FolderHierarchyIsInconsistentPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x0004EA20 File Offset: 0x0004CC20
		protected FolderHierarchyIsInconsistentPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0004EA2A File Offset: 0x0004CC2A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
