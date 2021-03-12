using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000060 RID: 96
	internal class ReservationContext : DisposeTrackableBase
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x0001D31B File Offset: 0x0001B51B
		public ReservationContext()
		{
			this.Reservations = new List<IReservation>();
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001D32E File Offset: 0x0001B52E
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001D336 File Offset: 0x0001B536
		public List<IReservation> Reservations { get; private set; }

		// Token: 0x060004DF RID: 1247 RVA: 0x0001D33F File Offset: 0x0001B53F
		public void AddReservation(IReservation reservation)
		{
			this.Reservations.Add(reservation);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001D350 File Offset: 0x0001B550
		public IReservation GetReservation(Guid mdbGuid, ReservationFlags flags)
		{
			flags &= (ReservationFlags.Read | ReservationFlags.Write);
			foreach (IReservation reservation in this.Reservations)
			{
				if (reservation.ResourceId == mdbGuid && reservation.Flags.HasFlag(flags))
				{
					return reservation;
				}
			}
			return null;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		public void ReserveResource(Guid mailboxGuid, TenantPartitionHint partitionHint, ADObjectId resourceId, ReservationFlags flags)
		{
			string serverFQDN = null;
			if (!(resourceId.ObjectGuid == MRSResource.Id.ObjectGuid))
			{
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(resourceId, null, null, FindServerFlags.None);
				if (databaseInformation.ServerVersion < Server.E15MinVersion)
				{
					return;
				}
				if (!databaseInformation.IsOnThisServer)
				{
					serverFQDN = databaseInformation.ServerFqdn;
				}
			}
			this.AddReservation(ReservationWrapper.CreateReservation(serverFQDN, null, mailboxGuid, partitionHint, resourceId.ObjectGuid, flags));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001D438 File Offset: 0x0001B638
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				foreach (IReservation reservation in this.Reservations)
				{
					reservation.Dispose();
				}
				this.Reservations.Clear();
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001D498 File Offset: 0x0001B698
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ReservationContext>(this);
		}
	}
}
