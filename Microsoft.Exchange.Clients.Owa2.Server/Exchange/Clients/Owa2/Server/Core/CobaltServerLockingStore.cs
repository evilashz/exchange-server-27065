using System;
using System.Collections.Generic;
using Cobalt;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000042 RID: 66
	internal class CobaltServerLockingStore : HostLockingStore
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00007095 File Offset: 0x00005295
		public CobaltServerLockingStore(CobaltStore blobStore)
		{
			this.blobStore = blobStore;
			this.editorsTable = new Dictionary<string, EditorsTableEntry>();
			this.editorsTableWaterline = 0UL;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000070B7 File Offset: 0x000052B7
		public override ulong GetEditorsTableWaterline()
		{
			return this.editorsTableWaterline;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000070C0 File Offset: 0x000052C0
		public override AmIAloneRequest.OutputType HandleAmIAlone(AmIAloneRequest.InputType input)
		{
			return new AmIAloneRequest.OutputType
			{
				AmIAlone = true
			};
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000070DC File Offset: 0x000052DC
		public override CheckCoauthLockAvailabilityRequest.OutputType HandleCheckCoauthLockAvailability(CheckCoauthLockAvailabilityRequest.InputType input)
		{
			return new CheckCoauthLockAvailabilityRequest.OutputType();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000070F0 File Offset: 0x000052F0
		public override CheckExclusiveLockAvailabilityRequest.OutputType HandleCheckExclusiveLockAvailability(CheckExclusiveLockAvailabilityRequest.InputType input)
		{
			return new CheckExclusiveLockAvailabilityRequest.OutputType();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007104 File Offset: 0x00005304
		public override CheckSchemaLockAvailabilityRequest.OutputType HandleCheckSchemaLockAvailability(CheckSchemaLockAvailabilityRequest.InputType input)
		{
			return new CheckSchemaLockAvailabilityRequest.OutputType();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007118 File Offset: 0x00005318
		public override ConvertCoauthLockToExclusiveLockRequest.OutputType HandleConvertCoauthLockToExclusiveLock(ConvertCoauthLockToExclusiveLockRequest.InputType input)
		{
			return new ConvertCoauthLockToExclusiveLockRequest.OutputType();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000712C File Offset: 0x0000532C
		public override ConvertExclusiveLockToSchemaLockRequest.OutputType HandleConvertExclusiveLockToSchemaLock(ConvertExclusiveLockToSchemaLockRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			return new ConvertExclusiveLockToSchemaLockRequest.OutputType();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007140 File Offset: 0x00005340
		public override ConvertExclusiveLockWithCoauthTransitionRequest.OutputType HandleConvertExclusiveLockWithCoauthTransition(ConvertExclusiveLockWithCoauthTransitionRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			return new ConvertExclusiveLockWithCoauthTransitionRequest.OutputType();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007154 File Offset: 0x00005354
		public override ConvertSchemaLockToExclusiveLockRequest.OutputType HandleConvertSchemaLockToExclusiveLock(ConvertSchemaLockToExclusiveLockRequest.InputType input)
		{
			return new ConvertSchemaLockToExclusiveLockRequest.OutputType();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007168 File Offset: 0x00005368
		public override DocMetaInfoRequest.OutputType HandleDocMetaInfo(DocMetaInfoRequest.InputType input)
		{
			return new DocMetaInfoRequest.OutputType();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000717C File Offset: 0x0000537C
		public override ExitCoauthoringRequest.OutputType HandleExitCoauthoring(ExitCoauthoringRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			if (this.editorsTable.ContainsKey(input.ClientId.ToString()))
			{
				this.editorsTable.Remove(input.ClientId.ToString());
			}
			return new ExitCoauthoringRequest.OutputType();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000071D4 File Offset: 0x000053D4
		public override GetCoauthoringStatusRequest.OutputType HandleGetCoauthoringStatus(GetCoauthoringStatusRequest.InputType input)
		{
			return new GetCoauthoringStatusRequest.OutputType
			{
				CoauthStatus = 1
			};
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000071F0 File Offset: 0x000053F0
		public override GetExclusiveLockRequest.OutputType HandleGetExclusiveLock(GetExclusiveLockRequest.InputType input)
		{
			return new GetExclusiveLockRequest.OutputType();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007204 File Offset: 0x00005404
		public override GetSchemaLockRequest.OutputType HandleGetSchemaLock(GetSchemaLockRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			return new GetSchemaLockRequest.OutputType
			{
				ExclusiveLockReturnReason = 3,
				Lock = 1
			};
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007228 File Offset: 0x00005428
		public override JoinCoauthoringRequest.OutputType HandleJoinCoauthoring(JoinCoauthoringRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			EditorsTableEntry editorsTableEntry = this.CreateEditorsTableEntry();
			this.editorsTable.Add(editorsTableEntry.ClientId, editorsTableEntry);
			this.editorsTableWaterline += 1UL;
			return new JoinCoauthoringRequest.OutputType
			{
				CoauthStatus = 1,
				ExclusiveLockReturnReason = 0,
				Lock = 1,
				TransitionId = Guid.NewGuid()
			};
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007284 File Offset: 0x00005484
		public override JoinEditingSessionRequest.OutputType HandleJoinEditingSession(JoinEditingSessionRequest.InputType input)
		{
			return new JoinEditingSessionRequest.OutputType();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007298 File Offset: 0x00005498
		public override LeaveEditingSessionRequest.OutputType HandleLeaveEditingSession(LeaveEditingSessionRequest.InputType input)
		{
			return new LeaveEditingSessionRequest.OutputType();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000072AC File Offset: 0x000054AC
		public override LockAndCheckOutStatusRequest.OutputType HandleLockAndCheckOutStatus(LockAndCheckOutStatusRequest.InputType input)
		{
			return new LockAndCheckOutStatusRequest.OutputType();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000072C0 File Offset: 0x000054C0
		public override MarkCoauthTransitionCompleteRequest.OutputType HandleMarkCoauthTransitionComplete(MarkCoauthTransitionCompleteRequest.InputType input)
		{
			return new MarkCoauthTransitionCompleteRequest.OutputType();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000072D4 File Offset: 0x000054D4
		public override RefreshCoauthoringSessionRequest.OutputType HandleRefreshCoauthoring(RefreshCoauthoringSessionRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			return new RefreshCoauthoringSessionRequest.OutputType();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000072E8 File Offset: 0x000054E8
		public override RefreshEditingSessionRequest.OutputType HandleRefreshEditingSession(RefreshEditingSessionRequest.InputType input)
		{
			return new RefreshEditingSessionRequest.OutputType();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000072FC File Offset: 0x000054FC
		public override RefreshExclusiveLockRequest.OutputType HandleRefreshExclusiveLock(RefreshExclusiveLockRequest.InputType input)
		{
			return new RefreshExclusiveLockRequest.OutputType();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007310 File Offset: 0x00005510
		public override RefreshSchemaLockRequest.OutputType HandleRefreshSchemaLock(RefreshSchemaLockRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			return new RefreshSchemaLockRequest.OutputType();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007324 File Offset: 0x00005524
		public override ReleaseExclusiveLockRequest.OutputType HandleReleaseExclusiveLock(ReleaseExclusiveLockRequest.InputType input)
		{
			return new ReleaseExclusiveLockRequest.OutputType();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007338 File Offset: 0x00005538
		public override ReleaseSchemaLockRequest.OutputType HandleReleaseSchemaLock(ReleaseSchemaLockRequest.InputType input, int protocolMajorVersion, int protocolMinorVersion)
		{
			return new ReleaseSchemaLockRequest.OutputType();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000734C File Offset: 0x0000554C
		public override RemoveEditorMetadataRequest.OutputType HandleRemoveEditorMetadata(RemoveEditorMetadataRequest.InputType input)
		{
			RemoveEditorMetadataRequest.OutputType result = new RemoveEditorMetadataRequest.OutputType();
			EditorsTableEntry editorsTableEntry;
			if (this.editorsTable.TryGetValue(input.ClientId.ToString(), out editorsTableEntry))
			{
				editorsTableEntry.Properties.Remove(input.Key);
			}
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007398 File Offset: 0x00005598
		public override ServerTimeRequest.OutputType HandleServerTime(ServerTimeRequest.InputType input)
		{
			return new ServerTimeRequest.OutputType
			{
				ServerTime = (DateTime)ExDateTime.Now
			};
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000073BC File Offset: 0x000055BC
		public override UpdateEditorMetadataRequest.OutputType HandleUpdateEditorMetadata(UpdateEditorMetadataRequest.InputType input)
		{
			UpdateEditorMetadataRequest.OutputType result = new UpdateEditorMetadataRequest.OutputType();
			EditorsTableEntry editorsTableEntry;
			if (this.editorsTable.TryGetValue(input.ClientId.ToString(), out editorsTableEntry))
			{
				editorsTableEntry.Properties[input.Key] = input.Value;
			}
			return result;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000740C File Offset: 0x0000560C
		public override VersionsRequest.OutputType HandleVersions(VersionsRequest.InputType input)
		{
			return new VersionsRequest.OutputType
			{
				Enabled = false
			};
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007428 File Offset: 0x00005628
		public override WhoAmIRequest.OutputType HandleWhoAmI(WhoAmIRequest.InputType input)
		{
			return new WhoAmIRequest.OutputType
			{
				UserEmailAddress = this.blobStore.OwnerEmailAddress,
				UserIsAnonymous = false,
				UserLogin = this.blobStore.OwnerEmailAddress,
				UserName = this.blobStore.OwnerEmailAddress,
				UserSipAddress = "sip:" + this.blobStore.OwnerEmailAddress
			};
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007491 File Offset: 0x00005691
		public override Dictionary<string, EditorsTableEntry> QueryEditorsTable()
		{
			return this.editorsTable;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000749C File Offset: 0x0000569C
		private EditorsTableEntry CreateEditorsTableEntry()
		{
			return new EditorsTableEntry
			{
				ClientId = Guid.NewGuid().ToString(),
				DtTimeout = (DateTime)ExDateTime.Now.Add(new TimeSpan(1, 0, 0, 0)),
				EmailAddress = this.blobStore.OwnerEmailAddress,
				HasEditPermission = true,
				Properties = new Dictionary<string, Atom>(),
				SipAddress = "sip:" + this.blobStore.OwnerEmailAddress,
				UserLogin = this.blobStore.OwnerEmailAddress,
				UserName = this.blobStore.OwnerEmailAddress
			};
		}

		// Token: 0x040000AF RID: 175
		private CobaltStore blobStore;

		// Token: 0x040000B0 RID: 176
		private Dictionary<string, EditorsTableEntry> editorsTable;

		// Token: 0x040000B1 RID: 177
		private ulong editorsTableWaterline;
	}
}
