using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000316 RID: 790
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RelinquishJobResourceReservationTransientException : RelinquishJobTransientException
	{
		// Token: 0x06002524 RID: 9508 RVA: 0x00051048 File Offset: 0x0004F248
		public RelinquishJobResourceReservationTransientException(LocalizedString error) : base(MrsStrings.JobHasBeenRelinquishedDueToResourceReservation(error))
		{
			this.error = error;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x0005105D File Offset: 0x0004F25D
		public RelinquishJobResourceReservationTransientException(LocalizedString error, Exception innerException) : base(MrsStrings.JobHasBeenRelinquishedDueToResourceReservation(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x00051073 File Offset: 0x0004F273
		protected RelinquishJobResourceReservationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (LocalizedString)info.GetValue("error", typeof(LocalizedString));
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0005109D File Offset: 0x0004F29D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x000510BD File Offset: 0x0004F2BD
		public LocalizedString Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001015 RID: 4117
		private readonly LocalizedString error;
	}
}
