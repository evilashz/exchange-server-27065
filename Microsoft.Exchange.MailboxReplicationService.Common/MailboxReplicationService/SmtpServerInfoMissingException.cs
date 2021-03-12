using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200036D RID: 877
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SmtpServerInfoMissingException : MailboxReplicationPermanentException
	{
		// Token: 0x060026DE RID: 9950 RVA: 0x00053CA8 File Offset: 0x00051EA8
		public SmtpServerInfoMissingException() : base(MrsStrings.SmtpServerInfoMissing)
		{
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00053CB5 File Offset: 0x00051EB5
		public SmtpServerInfoMissingException(Exception innerException) : base(MrsStrings.SmtpServerInfoMissing, innerException)
		{
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00053CC3 File Offset: 0x00051EC3
		protected SmtpServerInfoMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00053CCD File Offset: 0x00051ECD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
