using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200036E RID: 878
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ActionNotSupportedException : MailboxReplicationPermanentException
	{
		// Token: 0x060026E2 RID: 9954 RVA: 0x00053CD7 File Offset: 0x00051ED7
		public ActionNotSupportedException() : base(MrsStrings.ActionNotSupported)
		{
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00053CE4 File Offset: 0x00051EE4
		public ActionNotSupportedException(Exception innerException) : base(MrsStrings.ActionNotSupported, innerException)
		{
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00053CF2 File Offset: 0x00051EF2
		protected ActionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x00053CFC File Offset: 0x00051EFC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
