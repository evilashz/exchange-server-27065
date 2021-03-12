using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000E0 RID: 224
	internal class MeetingOrganizerSyncState : SyncStateDataInfo
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x000447B8 File Offset: 0x000429B8
		public MeetingOrganizerSyncState(CustomSyncState wrappedSyncState) : base(wrappedSyncState)
		{
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x000447C4 File Offset: 0x000429C4
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x000447F4 File Offset: 0x000429F4
		public MeetingOrganizerInfo MeetingOrganizerInfo
		{
			get
			{
				MeetingOrganizerInfo meetingOrganizerInfo = base.Fetch<MeetingOrganizerInfoData, MeetingOrganizerInfo>(CustomStateDatumType.MeetingOrganizerInfo, null);
				if (meetingOrganizerInfo == null)
				{
					meetingOrganizerInfo = new MeetingOrganizerInfo();
					base.Assign<MeetingOrganizerInfoData, MeetingOrganizerInfo>(CustomStateDatumType.MeetingOrganizerInfo, meetingOrganizerInfo);
				}
				return meetingOrganizerInfo;
			}
			set
			{
				base.Assign<MeetingOrganizerInfoData, MeetingOrganizerInfo>(CustomStateDatumType.MeetingOrganizerInfo, value);
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00044804 File Offset: 0x00042A04
		public static MeetingOrganizerSyncState LoadFromMailbox(MailboxSession mailboxSession, SyncStateStorage syncStateStorage, ProtocolLogger protocolLogger)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (syncStateStorage == null)
			{
				throw new ArgumentNullException("syncStateStorage");
			}
			bool flag = false;
			MeetingOrganizerSyncState meetingOrganizerSyncState = null;
			CustomSyncState customSyncState = null;
			MeetingOrganizerSyncState result;
			try
			{
				MeetingOrganizerSyncStateInfo meetingOrganizerSyncStateInfo = new MeetingOrganizerSyncStateInfo();
				bool flag2 = false;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3104189757U, ref flag2);
				if (flag2)
				{
					throw new CorruptSyncStateException(meetingOrganizerSyncStateInfo.UniqueName, new LocalizedString("FaultInjection"));
				}
				bool isDirty = false;
				customSyncState = syncStateStorage.GetCustomSyncState(meetingOrganizerSyncStateInfo, new PropertyDefinition[0]);
				if (customSyncState == null)
				{
					customSyncState = syncStateStorage.CreateCustomSyncState(meetingOrganizerSyncStateInfo);
					isDirty = true;
				}
				else if (customSyncState.BackendVersion != null && customSyncState.BackendVersion.Value != customSyncState.Version)
				{
					isDirty = true;
				}
				meetingOrganizerSyncState = new MeetingOrganizerSyncState(customSyncState);
				meetingOrganizerSyncState.IsDirty = isDirty;
				flag = true;
				result = meetingOrganizerSyncState;
			}
			finally
			{
				if (!flag)
				{
					if (meetingOrganizerSyncState != null)
					{
						meetingOrganizerSyncState.Dispose();
					}
					else if (customSyncState != null)
					{
						customSyncState.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x040007E8 RID: 2024
		private const uint lidCorruptMeetingOrganizerSyncState = 3104189757U;
	}
}
