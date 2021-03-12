using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200036F RID: 879
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecoverySyncNotImplementedException : MailboxReplicationPermanentException
	{
		// Token: 0x060026E6 RID: 9958 RVA: 0x00053D06 File Offset: 0x00051F06
		public RecoverySyncNotImplementedException() : base(MrsStrings.RecoverySyncNotImplemented)
		{
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x00053D13 File Offset: 0x00051F13
		public RecoverySyncNotImplementedException(Exception innerException) : base(MrsStrings.RecoverySyncNotImplemented, innerException)
		{
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00053D21 File Offset: 0x00051F21
		protected RecoverySyncNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x00053D2B File Offset: 0x00051F2B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
