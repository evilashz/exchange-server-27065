using System;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000A RID: 10
	internal class RemoteReservation : DisposeTrackableBase, IReservation, IDisposable
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00004CCF File Offset: 0x00002ECF
		public RemoteReservation(Guid reservationID, string serverFQDN, NetworkCredential credentials, Guid mailboxGuid, Guid mdbGuid, ReservationFlags flags)
		{
			this.reservationId = reservationID;
			this.serverFQDN = serverFQDN;
			this.credentials = credentials;
			this.mailboxGuid = mailboxGuid;
			this.mdbGuid = mdbGuid;
			this.flags = flags;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004D04 File Offset: 0x00002F04
		Guid IReservation.Id
		{
			get
			{
				return this.reservationId;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004D0C File Offset: 0x00002F0C
		ReservationFlags IReservation.Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004D14 File Offset: 0x00002F14
		Guid IReservation.ResourceId
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004D1C File Offset: 0x00002F1C
		public static RemoteReservation Create(string serverFQDN, NetworkCredential credentials, Guid mailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, ReservationFlags flags)
		{
			RemoteReservation result;
			using (MailboxReplicationProxyClient mailboxReplicationProxyClient = MailboxReplicationProxyClient.CreateWithoutThrottling(serverFQDN, credentials, mailboxGuid, mdbGuid))
			{
				Guid reservationID;
				if (mailboxReplicationProxyClient.ServerVersion[37])
				{
					byte[] partitionHintBytes = (partitionHint != null) ? partitionHint.GetPersistablePartitionHint() : null;
					reservationID = ((IMailboxReplicationProxyService)mailboxReplicationProxyClient).IReservationManager_ReserveResources(mailboxGuid, partitionHintBytes, mdbGuid, (int)flags);
				}
				else if (mailboxReplicationProxyClient.ServerVersion[28])
				{
					reservationID = Guid.NewGuid();
					LegacyReservationStatus legacyReservationStatus = (LegacyReservationStatus)((IMailboxReplicationProxyService)mailboxReplicationProxyClient).IMailbox_ReserveResources(reservationID, mdbGuid, (int)RemoteReservation.ConvertReservationFlagsToLegacy(flags, true));
					if (legacyReservationStatus != LegacyReservationStatus.Success)
					{
						throw new CapacityExceededReservationException(string.Format("{0}:{1}:{2}", serverFQDN, mdbGuid, flags), 1);
					}
				}
				else
				{
					reservationID = RemoteReservation.DownlevelReservationId;
				}
				result = new RemoteReservation(reservationID, serverFQDN, credentials, mailboxGuid, mdbGuid, flags);
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public void ConfirmLegacyReservation(MailboxReplicationProxyClient client)
		{
			if (client.ServerVersion[28])
			{
				LegacyReservationStatus legacyReservationStatus = (LegacyReservationStatus)((IMailboxReplicationProxyService)client).IMailbox_ReserveResources(this.reservationId, this.mdbGuid, (int)RemoteReservation.ConvertReservationFlagsToLegacy(this.flags, false));
				if (legacyReservationStatus != LegacyReservationStatus.Success)
				{
					throw new CapacityExceededReservationException(string.Format("{0}:{1}:{2}", this.serverFQDN, this.mdbGuid, this.flags), 1);
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004ED4 File Offset: 0x000030D4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.reservationId != Guid.Empty)
			{
				if (this.reservationId == RemoteReservation.DownlevelReservationId)
				{
					return;
				}
				CommonUtils.CatchKnownExceptions(delegate
				{
					using (MailboxReplicationProxyClient mailboxReplicationProxyClient = MailboxReplicationProxyClient.CreateWithoutThrottling(this.serverFQDN, this.credentials, this.mailboxGuid, this.mdbGuid))
					{
						if (mailboxReplicationProxyClient.ServerVersion[37])
						{
							((IMailboxReplicationProxyService)mailboxReplicationProxyClient).IReservationManager_ReleaseResources(this.reservationId);
						}
						else if (mailboxReplicationProxyClient.ServerVersion[28])
						{
							((IMailboxReplicationProxyService)mailboxReplicationProxyClient).IMailbox_ReserveResources(this.reservationId, Guid.Empty, 3);
						}
					}
				}, null);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004F22 File Offset: 0x00003122
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RemoteReservation>(this);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004F2C File Offset: 0x0000312C
		private static LegacyReservationType ConvertReservationFlagsToLegacy(ReservationFlags flags, bool expiring)
		{
			LegacyReservationType result;
			if (flags.HasFlag(ReservationFlags.Read))
			{
				if (expiring)
				{
					result = LegacyReservationType.ExpiredRead;
				}
				else
				{
					result = LegacyReservationType.Read;
				}
			}
			else if (expiring)
			{
				result = LegacyReservationType.ExpiredWrite;
			}
			else
			{
				result = LegacyReservationType.Write;
			}
			return result;
		}

		// Token: 0x04000026 RID: 38
		private static readonly Guid DownlevelReservationId = new Guid("c83d3976-43cc-4fbc-afe9-ed5bce7f3acb");

		// Token: 0x04000027 RID: 39
		private readonly Guid reservationId;

		// Token: 0x04000028 RID: 40
		private readonly Guid mailboxGuid;

		// Token: 0x04000029 RID: 41
		private readonly Guid mdbGuid;

		// Token: 0x0400002A RID: 42
		private readonly ReservationFlags flags;

		// Token: 0x0400002B RID: 43
		private readonly string serverFQDN;

		// Token: 0x0400002C RID: 44
		private readonly NetworkCredential credentials;
	}
}
