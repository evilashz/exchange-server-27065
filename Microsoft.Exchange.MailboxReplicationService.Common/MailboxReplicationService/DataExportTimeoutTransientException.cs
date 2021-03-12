using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002E3 RID: 739
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataExportTimeoutTransientException : MailboxReplicationTransientException
	{
		// Token: 0x06002430 RID: 9264 RVA: 0x0004FAC1 File Offset: 0x0004DCC1
		public DataExportTimeoutTransientException() : base(MrsStrings.DataExportTimeout)
		{
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0004FACE File Offset: 0x0004DCCE
		public DataExportTimeoutTransientException(Exception innerException) : base(MrsStrings.DataExportTimeout, innerException)
		{
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0004FADC File Offset: 0x0004DCDC
		protected DataExportTimeoutTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0004FAE6 File Offset: 0x0004DCE6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
