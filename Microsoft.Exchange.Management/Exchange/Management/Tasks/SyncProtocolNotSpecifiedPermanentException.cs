using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000ED1 RID: 3793
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SyncProtocolNotSpecifiedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A8EF RID: 43247 RVA: 0x0028ACCA File Offset: 0x00288ECA
		public SyncProtocolNotSpecifiedPermanentException() : base(Strings.ErrorSyncProtocolNotSpecified)
		{
		}

		// Token: 0x0600A8F0 RID: 43248 RVA: 0x0028ACD7 File Offset: 0x00288ED7
		public SyncProtocolNotSpecifiedPermanentException(Exception innerException) : base(Strings.ErrorSyncProtocolNotSpecified, innerException)
		{
		}

		// Token: 0x0600A8F1 RID: 43249 RVA: 0x0028ACE5 File Offset: 0x00288EE5
		protected SyncProtocolNotSpecifiedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A8F2 RID: 43250 RVA: 0x0028ACEF File Offset: 0x00288EEF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
