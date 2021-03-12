using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D3 RID: 723
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToReadADPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023DD RID: 9181 RVA: 0x0004F1FF File Offset: 0x0004D3FF
		public UnableToReadADPermanentException() : base(MrsStrings.UnableToReadAD)
		{
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x0004F20C File Offset: 0x0004D40C
		public UnableToReadADPermanentException(Exception innerException) : base(MrsStrings.UnableToReadAD, innerException)
		{
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x0004F21A File Offset: 0x0004D41A
		protected UnableToReadADPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0004F224 File Offset: 0x0004D424
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
