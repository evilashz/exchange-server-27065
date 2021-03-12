using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000281 RID: 641
	internal static class ReservationManager
	{
		// Token: 0x06001F99 RID: 8089 RVA: 0x00042A9C File Offset: 0x00040C9C
		static ReservationManager()
		{
			ReservationManager.wlmResourceStatsLastLoggingTimeUtc = DateTime.UtcNow;
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x00042AFC File Offset: 0x00040CFC
		private static bool WLMResourceStatsLogEnabled
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<bool>("WLMResourceStatsLogEnabled");
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x00042B08 File Offset: 0x00040D08
		private static TimeSpan WLMResourceStatsLoggingPeriod
		{
			get
			{
				return ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("WLMResourceStatsLoggingPeriod");
			}
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00042B14 File Offset: 0x00040D14
		public static ReservationBase CreateReservation(Guid mailboxGuid, TenantPartitionHint partitionHint, Guid resourceId, ReservationFlags flags, string clientName)
		{
			ReservationBase result;
			lock (ReservationManager.Locker)
			{
				ReservationBase reservationBase = ReservationBase.CreateReservation(mailboxGuid, partitionHint, resourceId, flags, clientName);
				ReservationManager.reservations.TryInsertSliding(reservationBase.ReservationId, reservationBase, ConfigBase<MRSConfigSchema>.GetConfig<TimeSpan>("ReservationExpirationInterval"));
				reservationBase.AddReleaseAction(new Action<ReservationBase>(ReservationManager.ReleaseReservation));
				result = reservationBase;
			}
			return result;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00042B8C File Offset: 0x00040D8C
		public static ReservationBase FindReservation(Guid reservationId)
		{
			if (reservationId == Guid.Empty)
			{
				return null;
			}
			ReservationBase result;
			lock (ReservationManager.Locker)
			{
				ReservationBase reservationBase;
				if (!ReservationManager.reservations.TryGetValue(reservationId, out reservationBase))
				{
					throw new ExpiredReservationException();
				}
				result = reservationBase;
			}
			return result;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00042C3C File Offset: 0x00040E3C
		public static void UpdateHealthState()
		{
			foreach (WorkloadType workloadType in ReservationManager.interestingWorkloadTypes)
			{
				MRSResource.Cache.GetInstance(MRSResource.Id.ObjectGuid, workloadType);
				LocalServerReadResource.Cache.GetInstance(LocalServerResource.ResourceId, workloadType);
				LocalServerWriteResource.Cache.GetInstance(LocalServerResource.ResourceId, workloadType);
			}
			foreach (Guid resourceID in MapiUtils.GetDatabasesOnThisServer())
			{
				foreach (WorkloadType workloadType2 in ReservationManager.interestingWorkloadTypes)
				{
					DatabaseReadResource.Cache.GetInstance(resourceID, workloadType2);
					DatabaseWriteResource.Cache.GetInstance(resourceID, workloadType2);
				}
			}
			bool logHealthState = false;
			TimeSpan t = DateTime.UtcNow - ReservationManager.wlmResourceStatsLastLoggingTimeUtc;
			if (ReservationManager.WLMResourceStatsLogEnabled && t >= ReservationManager.WLMResourceStatsLoggingPeriod)
			{
				logHealthState = true;
				ReservationManager.wlmResourceStatsLastLoggingTimeUtc = DateTime.UtcNow;
			}
			MRSResource.Cache.ForEach(delegate(MRSResource m)
			{
				m.UpdateHealthState(logHealthState);
			});
			LocalServerReadResource.Cache.ForEach(delegate(LocalServerReadResource m)
			{
				m.UpdateHealthState(logHealthState);
			});
			LocalServerWriteResource.Cache.ForEach(delegate(LocalServerWriteResource m)
			{
				m.UpdateHealthState(logHealthState);
			});
			DatabaseReadResource.Cache.ForEach(delegate(DatabaseReadResource m)
			{
				m.UpdateHealthState(logHealthState);
			});
			DatabaseWriteResource.Cache.ForEach(delegate(DatabaseWriteResource m)
			{
				m.UpdateHealthState(logHealthState);
			});
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00042DFC File Offset: 0x00040FFC
		public static XElement GetReservationsDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			XElement xelement = new XElement("Reservations");
			lock (ReservationManager.Locker)
			{
				using (List<ReservationBase>.Enumerator enumerator = ReservationManager.reservations.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReservationBase reservation = enumerator.Current;
						xelement.Add(arguments.RunDiagnosticOperation(() => reservation.GetDiagnosticInfo(arguments)));
					}
				}
			}
			return xelement;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0004325C File Offset: 0x0004145C
		public static XElement GetResourcesDiagnosticInfo(MRSDiagnosticArgument arguments)
		{
			XElement root = new XElement("Resources");
			lock (ReservationManager.Locker)
			{
				MRSResource.Cache.ForEach(delegate(MRSResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				LocalServerReadResource.Cache.ForEach(delegate(LocalServerReadResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				LocalServerWriteResource.Cache.ForEach(delegate(LocalServerWriteResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				DatabaseReadResource.Cache.ForEach(delegate(DatabaseReadResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				DatabaseWriteResource.Cache.ForEach(delegate(DatabaseWriteResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				MailboxMoveSourceResource.Cache.ForEach(delegate(MailboxMoveSourceResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				MailboxMoveTargetResource.Cache.ForEach(delegate(MailboxMoveTargetResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				MailboxMergeSourceResource.Cache.ForEach(delegate(MailboxMergeSourceResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
				MailboxMergeTargetResource.Cache.ForEach(delegate(MailboxMergeTargetResource m)
				{
					root.Add(arguments.RunDiagnosticOperation(() => m.GetDiagnosticInfo(arguments)));
				});
			}
			return root;
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x000433F8 File Offset: 0x000415F8
		private static void ReleaseReservation(ReservationBase reservation)
		{
			lock (ReservationManager.Locker)
			{
				ReservationManager.reservations.Remove(reservation.ReservationId);
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00043444 File Offset: 0x00041644
		private static bool ShouldRemoveReservation(Guid reservationID, ReservationBase reservation)
		{
			return !reservation.IsActive;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00043450 File Offset: 0x00041650
		private static void RemoveReservationCallback(Guid reservationID, ReservationBase reservation, RemoveReason reason)
		{
			lock (ReservationManager.Locker)
			{
				if (!reservation.IsDisposed)
				{
					reservation.Dispose();
				}
			}
		}

		// Token: 0x04000CBD RID: 3261
		internal static readonly object Locker = new object();

		// Token: 0x04000CBE RID: 3262
		private static DateTime wlmResourceStatsLastLoggingTimeUtc;

		// Token: 0x04000CBF RID: 3263
		private static readonly ExactTimeoutCache<Guid, ReservationBase> reservations = new ExactTimeoutCache<Guid, ReservationBase>(new RemoveItemDelegate<Guid, ReservationBase>(ReservationManager.RemoveReservationCallback), new ShouldRemoveDelegate<Guid, ReservationBase>(ReservationManager.ShouldRemoveReservation), null, 10000, true);

		// Token: 0x04000CC0 RID: 3264
		private static readonly WorkloadType[] interestingWorkloadTypes = new WorkloadType[]
		{
			WorkloadType.MailboxReplicationService,
			WorkloadType.MailboxReplicationServiceHighPriority
		};
	}
}
