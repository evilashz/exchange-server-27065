using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000042 RID: 66
	internal class MoveJob : IComparable<MoveJob>, ISettingsContextProvider
	{
		// Token: 0x06000351 RID: 849 RVA: 0x000158B8 File Offset: 0x00013AB8
		public MoveJob(PropValue[] properties, Guid requestQueueGuid)
		{
			this.JobType = MapiUtils.GetValue<MRSJobType>(properties[9], MRSJobType.Unknown);
			if (!RequestJobXML.IsKnownJobType(this.JobType))
			{
				MrsTracer.Service.Debug("Skipping unknown jobType {0}", new object[]
				{
					(int)this.JobType
				});
				return;
			}
			this.RequestGuid = MapiUtils.GetValue<Guid>(properties[26], Guid.Empty);
			this.ExchangeGuid = MapiUtils.GetValue<Guid>(properties[5], Guid.Empty);
			this.ArchiveGuid = MapiUtils.GetValue<Guid>(properties[6], Guid.Empty);
			this.CancelRequest = MapiUtils.GetValue<bool>(properties[4], false);
			this.MrsServerName = MapiUtils.GetValue<string>(properties[2], null);
			this.Status = MapiUtils.GetValue<RequestStatus>(properties[0], RequestStatus.None);
			this.JobState = MapiUtils.GetValue<JobProcessingState>(properties[1], JobProcessingState.NotReady);
			this.LastUpdateTimeStamp = MapiUtils.GetValue<DateTime>(properties[7], DateTime.MinValue);
			this.Flags = MapiUtils.GetValue<RequestFlags>(properties[10], RequestFlags.None);
			this.SourceDatabaseGuid = MapiUtils.GetValue<Guid>(properties[11], Guid.Empty);
			this.TargetDatabaseGuid = MapiUtils.GetValue<Guid>(properties[12], Guid.Empty);
			this.SourceArchiveDatabaseGuid = MapiUtils.GetValue<Guid>(properties[15], Guid.Empty);
			this.TargetArchiveDatabaseGuid = MapiUtils.GetValue<Guid>(properties[16], Guid.Empty);
			this.Priority = MapiUtils.GetValue<int>(properties[17], -1);
			this.DoNotPickUntilTimestamp = MapiUtils.GetValue<DateTime>(properties[13], DateTime.MinValue);
			this.RequestType = MapiUtils.GetValue<MRSRequestType>(properties[14], MRSRequestType.Move);
			this.MessageID = MapiUtils.GetValue<byte[]>(properties[27], null);
			this.SourceExchangeGuid = MapiUtils.GetValue<Guid>(properties[18], Guid.Empty);
			this.TargetExchangeGuid = MapiUtils.GetValue<Guid>(properties[19], Guid.Empty);
			this.RehomeRequest = MapiUtils.GetValue<bool>(properties[20], false);
			this.InternalFlags = MapiUtils.GetValue<RequestJobInternalFlags>(properties[21], RequestJobInternalFlags.None);
			this.PoisonCount = MapiUtils.GetValue<int>(properties[23], 0);
			this.FailureType = MapiUtils.GetValue<string>(properties[24], null);
			this.WorkloadType = MapiUtils.GetValue<RequestWorkloadType>(properties[25], RequestWorkloadType.None);
			byte[] value = MapiUtils.GetValue<byte[]>(properties[22], null);
			this.PartitionHint = ((value != null && value.Length > 0) ? TenantPartitionHint.FromPersistablePartitionHint(value) : null);
			this.RequestQueueGuid = requestQueueGuid;
			this.IsActiveOnThisMRSInstance = MRSService.JobIsActive(this.RequestGuid);
			this.isInteractive = MoveJob.IsInteractive(this.RequestType, this.WorkloadType);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00015BEF File Offset: 0x00013DEF
		public static bool IsInteractive(MRSRequestType requestType, RequestWorkloadType workloadType)
		{
			return ConfigBase<MRSConfigSchema>.GetConfig<bool>("AllAggregationSyncJobsInteractive") && requestType == MRSRequestType.Sync && workloadType == RequestWorkloadType.SyncAggregation;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00015C07 File Offset: 0x00013E07
		public static bool CacheJobQueues
		{
			get
			{
				return MoveJob.cacheJobQueues.Value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00015C13 File Offset: 0x00013E13
		internal Guid IdentifyingGuid
		{
			get
			{
				if (this.RequestType != MRSRequestType.Move)
				{
					return this.RequestGuid;
				}
				return this.ExchangeGuid;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00015C2A File Offset: 0x00013E2A
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00015C32 File Offset: 0x00013E32
		public Guid RequestGuid { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00015C3B File Offset: 0x00013E3B
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00015C43 File Offset: 0x00013E43
		public Guid ExchangeGuid { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00015C4C File Offset: 0x00013E4C
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00015C54 File Offset: 0x00013E54
		public Guid ArchiveGuid { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00015C5D File Offset: 0x00013E5D
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00015C65 File Offset: 0x00013E65
		public Guid SourceDatabaseGuid { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00015C6E File Offset: 0x00013E6E
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00015C76 File Offset: 0x00013E76
		public Guid TargetDatabaseGuid { get; private set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00015C7F File Offset: 0x00013E7F
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00015C87 File Offset: 0x00013E87
		public Guid SourceArchiveDatabaseGuid { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00015C90 File Offset: 0x00013E90
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00015C98 File Offset: 0x00013E98
		public Guid TargetArchiveDatabaseGuid { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00015CA1 File Offset: 0x00013EA1
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00015CA9 File Offset: 0x00013EA9
		public int Priority { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00015CB2 File Offset: 0x00013EB2
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00015CBA File Offset: 0x00013EBA
		public Guid RequestQueueGuid { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00015CC3 File Offset: 0x00013EC3
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00015CCB File Offset: 0x00013ECB
		public RequestFlags Flags { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00015CD4 File Offset: 0x00013ED4
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00015CDC File Offset: 0x00013EDC
		public RequestStatus Status { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00015CE5 File Offset: 0x00013EE5
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00015CED File Offset: 0x00013EED
		public Guid SourceExchangeGuid { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00015CF6 File Offset: 0x00013EF6
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00015CFE File Offset: 0x00013EFE
		public Guid TargetExchangeGuid { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00015D07 File Offset: 0x00013F07
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00015D0F File Offset: 0x00013F0F
		public bool RehomeRequest { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00015D18 File Offset: 0x00013F18
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00015D20 File Offset: 0x00013F20
		public JobProcessingState JobState { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00015D29 File Offset: 0x00013F29
		// (set) Token: 0x06000374 RID: 884 RVA: 0x00015D31 File Offset: 0x00013F31
		public DateTime LastUpdateTimeStamp { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00015D3A File Offset: 0x00013F3A
		public TimeSpan IdleTime
		{
			get
			{
				return (DateTime)ExDateTime.UtcNow - this.LastUpdateTimeStamp;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00015D51 File Offset: 0x00013F51
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00015D59 File Offset: 0x00013F59
		public MRSJobType JobType { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00015D62 File Offset: 0x00013F62
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00015D6A File Offset: 0x00013F6A
		public MRSRequestType RequestType { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00015D73 File Offset: 0x00013F73
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00015D7B File Offset: 0x00013F7B
		public RequestWorkloadType WorkloadType { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00015D84 File Offset: 0x00013F84
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00015D8C File Offset: 0x00013F8C
		public string MrsServerName { get; private set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00015D95 File Offset: 0x00013F95
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00015D9D File Offset: 0x00013F9D
		public bool CancelRequest { get; private set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00015DA6 File Offset: 0x00013FA6
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00015DAE File Offset: 0x00013FAE
		public DateTime DoNotPickUntilTimestamp { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00015DB7 File Offset: 0x00013FB7
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00015DBF File Offset: 0x00013FBF
		public bool IsActiveOnThisMRSInstance { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00015DC8 File Offset: 0x00013FC8
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public byte[] MessageID { get; private set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00015DD9 File Offset: 0x00013FD9
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00015DE1 File Offset: 0x00013FE1
		public RequestJobInternalFlags InternalFlags { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00015DEA File Offset: 0x00013FEA
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00015DF2 File Offset: 0x00013FF2
		public TenantPartitionHint PartitionHint { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00015DFB File Offset: 0x00013FFB
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00015E03 File Offset: 0x00014003
		public int PoisonCount { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00015E0C File Offset: 0x0001400C
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00015E14 File Offset: 0x00014014
		public string FailureType { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00015E1D File Offset: 0x0001401D
		public bool Suspend
		{
			get
			{
				return (this.Flags & RequestFlags.Suspend) != RequestFlags.None;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00015E31 File Offset: 0x00014031
		public bool SourceIsLocal
		{
			get
			{
				return (this.Flags & RequestFlags.IntraOrg) != RequestFlags.None || (this.Flags & RequestFlags.Push) != RequestFlags.None;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00015E4D File Offset: 0x0001404D
		public bool TargetIsLocal
		{
			get
			{
				return (this.Flags & RequestFlags.IntraOrg) != RequestFlags.None || (this.Flags & RequestFlags.Pull) != RequestFlags.None;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00015E69 File Offset: 0x00014069
		public bool ToBeCanceled
		{
			get
			{
				return this.CancelRequest && !this.RehomeRequest;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00015E7E File Offset: 0x0001407E
		public bool ToBeContinued
		{
			get
			{
				return !this.Suspend && !this.CancelRequest && !this.RehomeRequest && (this.Status == RequestStatus.InProgress || this.Status == RequestStatus.CompletionInProgress);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00015EAE File Offset: 0x000140AE
		public bool ToBeStartedFromScratch
		{
			get
			{
				return !this.Suspend && !this.CancelRequest && !this.RehomeRequest && this.Status == RequestStatus.Queued;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00015ED3 File Offset: 0x000140D3
		public bool IsCrossOrg
		{
			get
			{
				return (this.Flags & RequestFlags.CrossOrg) != RequestFlags.None;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00015EE3 File Offset: 0x000140E3
		public bool DoNotPickUntilHasElapsed
		{
			get
			{
				return DateTime.UtcNow >= this.DoNotPickUntilTimestamp || (MoveJob.CacheJobQueues && this.DoNotPickUntilTimestamp < MRSService.NextFullScanTime);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00015F14 File Offset: 0x00014114
		public bool IsLightRequest
		{
			get
			{
				return this.RehomeRequest || (this.Suspend && this.Status != RequestStatus.Failed && this.Status != RequestStatus.Suspended && this.Status != RequestStatus.AutoSuspended && this.Status != RequestStatus.Completed && this.Status != RequestStatus.CompletedWithWarning) || (!this.Suspend && (this.Status == RequestStatus.Failed || this.Status == RequestStatus.Suspended || this.Status == RequestStatus.AutoSuspended)) || this.Status == RequestStatus.Completed || QuarantinedJobs.Contains(this.IdentifyingGuid);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00015FA4 File Offset: 0x000141A4
		public static void ReserveLocalForestResources(ReservationContext reservation, WorkloadType workloadType, MRSRequestType requestType, RequestFlags requestFlags, Guid archiveGuid, Guid exchangeGuid, Guid sourceExchangeGuid, Guid targetExchangeGuid, TenantPartitionHint partitionHint, ADObjectId sourceDatabase, ADObjectId sourceArchiveDatabase, ADObjectId targetDatabase, ADObjectId targetArchiveDatabase, Guid sourceDatabaseGuid, Guid sourceArchiveDatabaseGuid, Guid targetDatabaseGuid, Guid targetArchiveDatabaseGuid)
		{
			ReservationFlags reservationFlags;
			Guid guid;
			Guid guid2;
			if (requestType == MRSRequestType.Move || requestType == MRSRequestType.MailboxRelocation)
			{
				reservationFlags = ReservationFlags.Move;
				if (requestFlags.HasFlag(RequestFlags.MoveOnlyArchiveMailbox) && archiveGuid != Guid.Empty)
				{
					guid = archiveGuid;
				}
				else
				{
					guid = exchangeGuid;
				}
				guid2 = guid;
			}
			else
			{
				reservationFlags = ReservationFlags.Merge;
				guid = sourceExchangeGuid;
				guid2 = targetExchangeGuid;
			}
			if (workloadType != Microsoft.Exchange.WorkloadManagement.WorkloadType.MailboxReplicationServiceHighPriority)
			{
				switch (workloadType)
				{
				case Microsoft.Exchange.WorkloadManagement.WorkloadType.MailboxReplicationServiceInternalMaintenance:
					reservationFlags |= ReservationFlags.InternalMaintenance;
					break;
				case Microsoft.Exchange.WorkloadManagement.WorkloadType.MailboxReplicationServiceInteractive:
					reservationFlags |= ReservationFlags.Interactive;
					break;
				}
			}
			else
			{
				reservationFlags |= ReservationFlags.HighPriority;
			}
			reservation.ReserveResource((guid2 == Guid.Empty) ? guid : guid2, partitionHint, MRSResource.Id, reservationFlags);
			if (targetDatabaseGuid != Guid.Empty)
			{
				reservation.ReserveResource(guid2, partitionHint, targetDatabase, reservationFlags | ReservationFlags.Write);
			}
			if (targetArchiveDatabaseGuid != Guid.Empty && targetArchiveDatabaseGuid != targetDatabaseGuid && archiveGuid != Guid.Empty)
			{
				reservation.ReserveResource(archiveGuid, partitionHint, targetArchiveDatabase, reservationFlags | ReservationFlags.Write | ReservationFlags.Archive);
			}
			if (sourceDatabaseGuid != Guid.Empty)
			{
				reservation.ReserveResource(guid, partitionHint, sourceDatabase, reservationFlags | ReservationFlags.Read);
			}
			if (sourceArchiveDatabaseGuid != Guid.Empty && sourceArchiveDatabaseGuid != sourceDatabaseGuid && archiveGuid != Guid.Empty)
			{
				reservation.ReserveResource(archiveGuid, partitionHint, sourceArchiveDatabase, reservationFlags | ReservationFlags.Read | ReservationFlags.Archive);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000160F4 File Offset: 0x000142F4
		public static void AddRemoteHostInProxyBackoff(string remoteHostName, DateTime nextCheckTime)
		{
			if (TestIntegration.Instance.DisableRemoteHostNameBlacklisting)
			{
				return;
			}
			if (string.IsNullOrEmpty(remoteHostName))
			{
				return;
			}
			remoteHostName = remoteHostName.ToLowerInvariant();
			DateTime dateTime;
			if (MoveJob.RemoteHostsInProxyBackoff.TryGetValue(remoteHostName, out dateTime))
			{
				return;
			}
			MoveJob.RemoteHostsInProxyBackoff.InsertAbsolute(remoteHostName, nextCheckTime, nextCheckTime, null);
			MrsTracer.Service.Debug("RemoteHostName {0} is added to proxy backoff blacklist until {1}.", new object[]
			{
				remoteHostName,
				nextCheckTime.ToLocalTime()
			});
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00016168 File Offset: 0x00014368
		public JobPickupRec AttemptToPick(MapiStore systemMailbox)
		{
			if (!RequestJobXML.IsKnownRequestType(this.RequestType))
			{
				return new JobPickupRec(this, JobPickupResult.UnknownJobType, DateTime.MaxValue, MrsStrings.PickupStatusRequestTypeNotSupported(this.RequestType.ToString()), null);
			}
			if (!RequestJobXML.IsKnownJobType(this.JobType))
			{
				return new JobPickupRec(this, JobPickupResult.UnknownJobType, DateTime.MaxValue, MrsStrings.PickupStatusJobTypeNotSupported(this.JobType.ToString()), null);
			}
			if (this.PoisonCount >= ConfigBase<MRSConfigSchema>.GetConfig<int>("HardPoisonLimit"))
			{
				return new JobPickupRec(this, JobPickupResult.PoisonedJob, DateTime.MaxValue, MrsStrings.PickupStatusJobPoisoned(this.PoisonCount), null);
			}
			if (this.IsActiveOnThisMRSInstance)
			{
				return new JobPickupRec(this, JobPickupResult.JobAlreadyActive, DateTime.MaxValue, LocalizedString.Empty, null);
			}
			DateTime utcNow = DateTime.UtcNow;
			if (this.Status == RequestStatus.Completed && (!this.DoNotPickUntilHasElapsed || this.DoNotPickUntilTimestamp == DateTime.MinValue) && !this.RehomeRequest)
			{
				return new JobPickupRec(this, JobPickupResult.CompletedJobSkipped, (this.DoNotPickUntilTimestamp == DateTime.MinValue) ? DateTime.MaxValue : this.DoNotPickUntilTimestamp, LocalizedString.Empty, null);
			}
			if (this.CancelRequest && !this.DoNotPickUntilHasElapsed)
			{
				return new JobPickupRec(this, JobPickupResult.PostponeCancel, this.DoNotPickUntilTimestamp, LocalizedString.Empty, null);
			}
			if (!this.isInteractive && !this.IsLightRequest && !this.DoNotPickUntilHasElapsed)
			{
				MrsTracer.Service.Debug("Ignoring MoveJob '{0}' on queue '{1}' having DoNotPickUntilTimestamp of {2}.", new object[]
				{
					this.RequestGuid,
					this.RequestQueueGuid,
					this.DoNotPickUntilTimestamp.ToLocalTime()
				});
				return new JobPickupRec(this, JobPickupResult.JobIsPostponed, this.DoNotPickUntilTimestamp, LocalizedString.Empty, null);
			}
			if (this.InternalFlags.HasFlag(RequestJobInternalFlags.ExecutedByTransportSync))
			{
				MrsTracer.Service.Debug("Ignoring MoveJob '{0}' since Tranport Sync Owns Execution of the job.", new object[]
				{
					this.RequestGuid
				});
				return new JobPickupRec(this, JobPickupResult.JobOwnedByTransportSync, DateTime.MaxValue, LocalizedString.Empty, null);
			}
			JobPickupRec result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				using (RequestJobProvider requestJobProvider = new RequestJobProvider(this.RequestQueueGuid, systemMailbox))
				{
					using (TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)requestJobProvider.Read<TransactionalRequestJob>(new RequestJobObjectId(this.RequestGuid, this.RequestQueueGuid, this.MessageID)))
					{
						if (transactionalRequestJob == null)
						{
							result = new JobPickupRec(this, JobPickupResult.InvalidJob, DateTime.MaxValue, MrsStrings.PickupStatusCorruptJob, null);
						}
						else if (!transactionalRequestJob.IsSupported())
						{
							result = new JobPickupRec(this, JobPickupResult.UnknownJobType, DateTime.MaxValue, MrsStrings.PickupStatusSubTypeNotSupported(transactionalRequestJob.RequestType.ToString()), null);
						}
						else if (transactionalRequestJob.ValidationResult != RequestJobBase.ValidationResultEnum.Valid)
						{
							this.ProcessInvalidJob(transactionalRequestJob, requestJobProvider);
							result = new JobPickupRec(this, JobPickupResult.InvalidJob, DateTime.MaxValue, MrsStrings.PickupStatusInvalidJob(transactionalRequestJob.ValidationResult.ToString(), transactionalRequestJob.ValidationMessage), null);
						}
						else if (transactionalRequestJob.Status == RequestStatus.Completed && !transactionalRequestJob.RehomeRequest)
						{
							this.CleanupCompletedJob(transactionalRequestJob, requestJobProvider);
							result = new JobPickupRec(this, JobPickupResult.CompletedJobCleanedUp, DateTime.MaxValue, MrsStrings.PickupStatusCompletedJob, null);
						}
						else if (!transactionalRequestJob.ShouldProcessJob())
						{
							result = new JobPickupRec(this, JobPickupResult.DisabledJobPickup, DateTime.MaxValue, MrsStrings.PickupStatusDisabled, null);
						}
						else
						{
							ReservationContext reservationContext = null;
							if (!this.IsLightRequest && !MoveJob.CacheJobQueues)
							{
								reservationContext = new ReservationContext();
								disposeGuard.Add<ReservationContext>(reservationContext);
								try
								{
									this.ReserveLocalForestResources(reservationContext, transactionalRequestJob);
								}
								catch (LocalizedException ex)
								{
									if (CommonUtils.ExceptionIs(ex, new WellKnownException[]
									{
										WellKnownException.ResourceReservation
									}))
									{
										return new JobPickupRec(this, JobPickupResult.ReservationFailure, utcNow + MoveJob.JobPickupRetryInterval, MrsStrings.PickupStatusReservationFailure(CommonUtils.FullExceptionMessage(ex)), ex as ResourceReservationException);
									}
									throw;
								}
							}
							if (!TestIntegration.Instance.DisableRemoteHostNameBlacklisting && transactionalRequestJob.RequestType == MRSRequestType.Move && (transactionalRequestJob.Flags & RequestFlags.CrossOrg) != RequestFlags.None && (transactionalRequestJob.Flags & RequestFlags.RemoteLegacy) == RequestFlags.None && !string.IsNullOrEmpty(transactionalRequestJob.RemoteHostName))
							{
								string key = transactionalRequestJob.RemoteHostName.ToLowerInvariant();
								DateTime nextRecommendedPickup;
								if (MoveJob.RemoteHostsInProxyBackoff.TryGetValue(key, out nextRecommendedPickup))
								{
									return new JobPickupRec(this, JobPickupResult.ProxyBackoff, nextRecommendedPickup, MrsStrings.PickupStatusProxyBackoff(transactionalRequestJob.RemoteHostName), null);
								}
							}
							MrsTracer.Service.Debug("Attempting to take over MoveJob '{0}' on queue '{1}', priority={2}", new object[]
							{
								transactionalRequestJob,
								this.RequestQueueGuid,
								transactionalRequestJob.Priority
							});
							transactionalRequestJob.RequestJobState = JobProcessingState.InProgress;
							transactionalRequestJob.MRSServerName = CommonUtils.LocalComputerName;
							if (!this.IsLightRequest)
							{
								transactionalRequestJob.PoisonCount++;
								transactionalRequestJob.LastPickupTime = new DateTime?(DateTime.UtcNow);
							}
							if (!transactionalRequestJob.Suspend && !transactionalRequestJob.RehomeRequest && transactionalRequestJob.Status != RequestStatus.Suspended && transactionalRequestJob.Status != RequestStatus.AutoSuspended && transactionalRequestJob.Status != RequestStatus.Failed && transactionalRequestJob.Status != RequestStatus.Completed && transactionalRequestJob.Status != RequestStatus.CompletedWithWarning)
							{
								transactionalRequestJob.Status = ((reservationContext == null) ? RequestStatus.Queued : RequestStatus.InProgress);
							}
							requestJobProvider.Save(transactionalRequestJob);
							this.Status = transactionalRequestJob.Status;
							JobPickupRec jobPickupRec;
							if (this.IsLightRequest)
							{
								jobPickupRec = new JobPickupRec(this, JobPickupResult.JobPickedUp, DateTime.MaxValue, MrsStrings.PickupStatusLightJob(transactionalRequestJob.Suspend, transactionalRequestJob.RehomeRequest, transactionalRequestJob.Priority.ToString()), null);
								MoveJob.PerformLightJobAction(requestJobProvider.SystemMailbox, RequestJobProvider.CreateRequestStatistics(transactionalRequestJob));
							}
							else
							{
								MailboxSyncerJobs.CreateJob(transactionalRequestJob, reservationContext);
								jobPickupRec = new JobPickupRec(this, JobPickupResult.JobPickedUp, DateTime.MaxValue, MrsStrings.PickupStatusCreateJob(transactionalRequestJob.SyncStage.ToString(), transactionalRequestJob.CancelRequest, transactionalRequestJob.Priority.ToString()), null);
							}
							disposeGuard.Success();
							result = jobPickupRec;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000167B0 File Offset: 0x000149B0
		public int CompareTo(MoveJob other)
		{
			if (object.ReferenceEquals(this, other) || this.RequestGuid.Equals(other.RequestGuid))
			{
				return 0;
			}
			if (this.Priority > other.Priority)
			{
				return -1;
			}
			if (this.Priority < other.Priority)
			{
				return 1;
			}
			if (this.ToBeCanceled && !other.ToBeCanceled)
			{
				return -1;
			}
			if (!this.ToBeCanceled && other.ToBeCanceled)
			{
				return 1;
			}
			if (this.ToBeCanceled && other.ToBeCanceled)
			{
				return this.CompareLastUpdateTimestamps(this, other);
			}
			if (this.ToBeContinued && !other.ToBeContinued)
			{
				return -1;
			}
			if (!this.ToBeContinued && other.ToBeContinued)
			{
				return 1;
			}
			if (this.ToBeContinued && other.ToBeContinued)
			{
				return this.CompareLastUpdateTimestamps(this, other);
			}
			if (this.ToBeStartedFromScratch && !other.ToBeStartedFromScratch)
			{
				return -1;
			}
			if (!this.ToBeStartedFromScratch && other.ToBeStartedFromScratch)
			{
				return 1;
			}
			if (this.ToBeStartedFromScratch && other.ToBeStartedFromScratch)
			{
				return this.CompareLastUpdateTimestamps(this, other);
			}
			int num = this.CompareLastUpdateTimestamps(this, other);
			if (num != 0)
			{
				return num;
			}
			return this.RequestGuid.CompareTo(other.RequestGuid);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00016B30 File Offset: 0x00014D30
		private static void PerformLightJobAction(MapiStore systemMailbox, RequestStatisticsBase requestJobStats)
		{
			CommonUtils.CatchKnownExceptions(delegate
			{
				bool flag = false;
				LightJobBase lightJobBase;
				if (QuarantinedJobs.Contains(requestJobStats.IdentifyingGuid))
				{
					lightJobBase = new QuarantineJob(requestJobStats.IdentifyingGuid, requestJobStats.WorkItemQueueMdb.ObjectGuid, systemMailbox, requestJobStats.MessageId);
				}
				else if (requestJobStats.ShouldRehomeRequest)
				{
					lightJobBase = new RehomeJob(requestJobStats.IdentifyingGuid, requestJobStats.RequestQueue, requestJobStats.OptimalRequestQueue, systemMailbox, requestJobStats.MessageId);
				}
				else if (requestJobStats.ShouldClearRehomeRequest)
				{
					lightJobBase = new ClearRehomeJob(requestJobStats.IdentifyingGuid, requestJobStats.WorkItemQueueMdb.ObjectGuid, systemMailbox, requestJobStats.MessageId);
				}
				else if (requestJobStats.ShouldSuspendRequest)
				{
					lightJobBase = new SuspendJob(requestJobStats.IdentifyingGuid, requestJobStats.WorkItemQueueMdb.ObjectGuid, systemMailbox, requestJobStats.MessageId);
				}
				else
				{
					lightJobBase = new ResumeJob(requestJobStats.IdentifyingGuid, requestJobStats.WorkItemQueueMdb.ObjectGuid, systemMailbox, requestJobStats.MessageId);
					flag = true;
				}
				using (lightJobBase)
				{
					lightJobBase.Run();
					if (flag)
					{
						MRSService.Tickle(requestJobStats.IdentifyingGuid, requestJobStats.WorkItemQueueMdb.ObjectGuid, MoveRequestNotification.Created);
					}
				}
			}, delegate(Exception failure)
			{
				LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
				MrsTracer.Service.Debug("Unexpected failure occurred trying to perform a light pipe action on MoveJob '{0}' from queue '{1}', skipping it. {2}", new object[]
				{
					requestJobStats.RequestGuid,
					requestJobStats.RequestQueue,
					localizedString
				});
				MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_UnableToProcessRequest, new object[]
				{
					requestJobStats.RequestGuid.ToString(),
					requestJobStats.WorkItemQueueMdb.ObjectGuid.ToString(),
					localizedString
				});
			});
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00016B70 File Offset: 0x00014D70
		private void ReserveLocalForestResources(ReservationContext reservation, TransactionalRequestJob requestJob)
		{
			MoveJob.ReserveLocalForestResources(reservation, CommonUtils.ComputeWlmWorkloadType(this.Priority, this.isInteractive, ConfigBase<MRSConfigSchema>.GetConfig<WorkloadType>("WlmWorkloadType")), this.RequestType, this.Flags, this.ArchiveGuid, this.ExchangeGuid, this.SourceExchangeGuid, this.TargetExchangeGuid, this.PartitionHint, requestJob.SourceDatabase, requestJob.SourceArchiveDatabase, requestJob.TargetDatabase, requestJob.TargetArchiveDatabase, this.SourceDatabaseGuid, this.SourceArchiveDatabaseGuid, this.TargetDatabaseGuid, this.TargetArchiveDatabaseGuid);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00016C3C File Offset: 0x00014E3C
		private void ProcessInvalidJob(TransactionalRequestJob requestJob, RequestJobProvider rjProvider)
		{
			MrsTracer.Service.Warning("MoveJob '{0}' on queue '{1}' failed validation: {2}.", new object[]
			{
				requestJob,
				this.RequestQueueGuid,
				requestJob.ValidationMessage
			});
			if (requestJob.IdleTime < MoveJob.MaxADReplicationWaitTime)
			{
				MrsTracer.Service.Warning("MoveJob '{0}' on queue '{1}' appears invalid.  Waiting for {2} for AD Replication.  Already have waited {3}...", new object[]
				{
					requestJob,
					this.RequestQueueGuid,
					MoveJob.MaxADReplicationWaitTime,
					requestJob.IdleTime
				});
				return;
			}
			if (requestJob.ValidationResult == RequestJobBase.ValidationResultEnum.Orphaned)
			{
				MrsTracer.Service.Warning("MoveJob '{0}' on queue '{1}' is orphaned, removing it.", new object[]
				{
					requestJob,
					this.RequestQueueGuid
				});
				rjProvider.Delete(requestJob);
				CommonUtils.CatchKnownExceptions(delegate
				{
					ReportData reportData2 = new ReportData(requestJob.IdentifyingGuid, requestJob.ReportVersion);
					reportData2.Delete(rjProvider.SystemMailbox);
				}, null);
				requestJob.RemoveAsyncNotification();
				MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RemovedOrphanedMoveRequest, new object[]
				{
					this.RequestQueueGuid.ToString(),
					this.RequestGuid.ToString(),
					requestJob.ToString(),
					requestJob.ValidationMessage
				});
				return;
			}
			ReportData reportData = new ReportData(requestJob.IdentifyingGuid, requestJob.ReportVersion);
			reportData.Append(MrsStrings.ReportFailingInvalidMoveRequest(requestJob.ValidationMessage));
			reportData.Flush(rjProvider.SystemMailbox);
			requestJob.Status = RequestStatus.Failed;
			requestJob.FailureCode = new int?(-2147024809);
			requestJob.FailureType = "InvalidRequest";
			requestJob.FailureSide = new ExceptionSide?(ExceptionSide.None);
			requestJob.Message = MrsStrings.MoveRequestMessageError(MrsStrings.MoveRequestDataIsCorrupt(requestJob.ValidationMessage));
			requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.Failure, new DateTime?(DateTime.UtcNow));
			requestJob.TimeTracker.CurrentState = RequestState.Failed;
			rjProvider.Save(requestJob);
			requestJob.UpdateAsyncNotification(reportData);
			MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_FailedInvalidRequest, new object[]
			{
				this.RequestQueueGuid.ToString(),
				this.RequestGuid.ToString(),
				requestJob.ToString(),
				requestJob.ValidationMessage
			});
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00016FB4 File Offset: 0x000151B4
		private void CleanupCompletedJob(TransactionalRequestJob requestJob, RequestJobProvider rjProvider)
		{
			DateTime t = requestJob.TimeTracker.GetTimestamp(RequestJobTimestamp.DoNotPickUntil) ?? DateTime.MaxValue;
			if (DateTime.UtcNow < t)
			{
				return;
			}
			MrsTracer.Service.Debug("Cleaning up expired completed job '{0}' on queue '{1}'", new object[]
			{
				requestJob,
				this.RequestQueueGuid
			});
			rjProvider.Delete(requestJob);
			CommonUtils.CatchKnownExceptions(delegate
			{
				ReportData reportData = new ReportData(requestJob.IdentifyingGuid, requestJob.ReportVersion);
				reportData.Delete(rjProvider.SystemMailbox);
			}, null);
			CommonUtils.CatchKnownExceptions(delegate
			{
				rjProvider.DeleteIndexEntries(requestJob);
			}, null);
			requestJob.RemoveAsyncNotification();
			MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_RemovedCompletedRequest, new object[]
			{
				requestJob.ToString(),
				this.RequestGuid.ToString(),
				requestJob.WorkItemQueueMdbName
			});
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000170C6 File Offset: 0x000152C6
		private int CompareLastUpdateTimestamps(MoveJob job1, MoveJob job2)
		{
			if (job1.IdleTime > job2.IdleTime)
			{
				return -1;
			}
			if (job1.IdleTime < job2.IdleTime)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000170F4 File Offset: 0x000152F4
		ISettingsContext ISettingsContextProvider.GetSettingsContext()
		{
			Guid guid = this.ExchangeGuid;
			if (guid == Guid.Empty)
			{
				guid = ((this.TargetExchangeGuid != Guid.Empty) ? this.TargetExchangeGuid : this.SourceExchangeGuid);
			}
			return CommonUtils.CreateConfigContext(guid, this.RequestQueueGuid, null, this.WorkloadType, this.RequestType, SyncProtocol.None);
		}

		// Token: 0x04000155 RID: 341
		public static readonly TimeSpan JobPickupRetryInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000156 RID: 342
		private static readonly TimeSpan MaxADReplicationWaitTime = TimeSpan.FromDays(10.0);

		// Token: 0x04000157 RID: 343
		private static readonly TimeoutCache<string, DateTime> RemoteHostsInProxyBackoff = new TimeoutCache<string, DateTime>(16, 1024, false);

		// Token: 0x04000158 RID: 344
		private static readonly Lazy<bool> cacheJobQueues = new Lazy<bool>(() => ConfigBase<MRSConfigSchema>.GetConfig<bool>("CacheJobQueues"));

		// Token: 0x04000159 RID: 345
		private readonly bool isInteractive;
	}
}
