using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E2 RID: 738
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataExportCanceledPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600242C RID: 9260 RVA: 0x0004FA92 File Offset: 0x0004DC92
		public DataExportCanceledPermanentException() : base(MrsStrings.DataExportCanceled)
		{
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0004FA9F File Offset: 0x0004DC9F
		public DataExportCanceledPermanentException(Exception innerException) : base(MrsStrings.DataExportCanceled, innerException)
		{
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0004FAAD File Offset: 0x0004DCAD
		protected DataExportCanceledPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0004FAB7 File Offset: 0x0004DCB7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
