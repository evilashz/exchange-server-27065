using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000021 RID: 33
	internal class AirSyncRootInfo : SyncStateDataInfo
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public AirSyncRootInfo(CustomSyncState wrappedState) : base(wrappedState)
		{
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		public ExDateTime? LastMaxDevicesExceededMailSentTime
		{
			get
			{
				return base.Fetch<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.LastMaxDevicesExceededMailSentTime, null);
			}
			set
			{
				base.Assign<NullableData<DateTimeData, ExDateTime>, ExDateTime?>(CustomStateDatumType.LastMaxDevicesExceededMailSentTime, value);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000EF00 File Offset: 0x0000D100
		public static AirSyncRootInfo LoadFromMailbox(MailboxSession mailboxSession, SyncStateRootStorage syncStateRootStorage)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("syncStateRootStorage", syncStateRootStorage);
			AirSyncRootInfo airSyncRootInfo = null;
			CustomSyncState customSyncState = null;
			bool flag = false;
			AirSyncRootInfo result;
			try
			{
				bool isDirty = false;
				AirSyncRootSyncStateInfo syncStateInfo = new AirSyncRootSyncStateInfo();
				customSyncState = syncStateRootStorage.GetCustomSyncState(syncStateInfo);
				if (customSyncState == null)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[AirSyncRootInfo.LoadFromMailbox] Had to create root sync state.");
					customSyncState = syncStateRootStorage.CreateCustomSyncState(syncStateInfo);
					isDirty = true;
				}
				airSyncRootInfo = new AirSyncRootInfo(customSyncState);
				airSyncRootInfo.IsDirty = isDirty;
				flag = true;
				result = airSyncRootInfo;
			}
			finally
			{
				if (!flag)
				{
					if (airSyncRootInfo != null)
					{
						airSyncRootInfo.Dispose();
					}
					else if (customSyncState != null)
					{
						customSyncState.Dispose();
					}
				}
			}
			return result;
		}
	}
}
