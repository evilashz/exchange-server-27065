using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000276 RID: 630
	internal abstract class MailboxMoveResource : MailboxResource
	{
		// Token: 0x06001F5E RID: 8030 RVA: 0x00041B74 File Offset: 0x0003FD74
		public MailboxMoveResource(Guid mailboxGuid) : base(mailboxGuid)
		{
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x00041B7D File Offset: 0x0003FD7D
		public override int StaticCapacity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00041B80 File Offset: 0x0003FD80
		protected override void ThrowStaticCapacityExceededException()
		{
			using (Dictionary<Guid, ReservationBase>.ValueCollection.Enumerator enumerator = base.Reservations.Values.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ReservationBase reservationBase = enumerator.Current;
					throw new MoveInProgressReservationException(string.Format("{0}({1})", this.ResourceType, this.ResourceName), reservationBase.ClientName);
				}
			}
		}
	}
}
