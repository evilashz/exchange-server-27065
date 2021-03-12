using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000333 RID: 819
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExpiredReservationException : ResourceReservationException
	{
		// Token: 0x060025BC RID: 9660 RVA: 0x000520A5 File Offset: 0x000502A5
		public ExpiredReservationException() : base(MrsStrings.ErrorReservationExpired)
		{
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000520B2 File Offset: 0x000502B2
		public ExpiredReservationException(Exception innerException) : base(MrsStrings.ErrorReservationExpired, innerException)
		{
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000520C0 File Offset: 0x000502C0
		protected ExpiredReservationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000520CA File Offset: 0x000502CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
