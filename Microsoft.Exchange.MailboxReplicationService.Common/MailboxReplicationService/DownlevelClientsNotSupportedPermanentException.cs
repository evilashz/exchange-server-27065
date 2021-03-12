using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F9 RID: 761
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DownlevelClientsNotSupportedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002498 RID: 9368 RVA: 0x000503A1 File Offset: 0x0004E5A1
		public DownlevelClientsNotSupportedPermanentException() : base(MrsStrings.ErrorDownlevelClientsNotSupported)
		{
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000503AE File Offset: 0x0004E5AE
		public DownlevelClientsNotSupportedPermanentException(Exception innerException) : base(MrsStrings.ErrorDownlevelClientsNotSupported, innerException)
		{
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000503BC File Offset: 0x0004E5BC
		protected DownlevelClientsNotSupportedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000503C6 File Offset: 0x0004E5C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
