using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200036A RID: 874
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FastTransferArgumentException : MailboxReplicationPermanentException
	{
		// Token: 0x060026D0 RID: 9936 RVA: 0x00053B89 File Offset: 0x00051D89
		public FastTransferArgumentException() : base(MrsStrings.FastTransferArgumentError)
		{
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x00053B96 File Offset: 0x00051D96
		public FastTransferArgumentException(Exception innerException) : base(MrsStrings.FastTransferArgumentError, innerException)
		{
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x00053BA4 File Offset: 0x00051DA4
		protected FastTransferArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x00053BAE File Offset: 0x00051DAE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
