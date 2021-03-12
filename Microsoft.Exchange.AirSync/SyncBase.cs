using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000B7 RID: 183
	internal abstract class SyncBase : Command
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x00039ABA File Offset: 0x00037CBA
		public SyncBase()
		{
			this.collections = new Dictionary<string, SyncCollection>();
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00039AE6 File Offset: 0x00037CE6
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00039AEE File Offset: 0x00037CEE
		public string DevicePhoneNumberForSms { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00039AF7 File Offset: 0x00037CF7
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00039AFF File Offset: 0x00037CFF
		public bool DeviceEnableOutboundSMS { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00039B08 File Offset: 0x00037D08
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00039B10 File Offset: 0x00037D10
		internal IAirSyncVersionFactory VersionFactory
		{
			get
			{
				return this.versionFactory;
			}
			set
			{
				this.versionFactory = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00039B19 File Offset: 0x00037D19
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00039B21 File Offset: 0x00037D21
		internal SyncBase.ErrorCodeStatus GlobalStatus
		{
			get
			{
				return this.globalStatus;
			}
			set
			{
				this.globalStatus = value;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00039B2A File Offset: 0x00037D2A
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00039B32 File Offset: 0x00037D32
		internal Exception LastException
		{
			get
			{
				return this.lastException;
			}
			set
			{
				this.lastException = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00039B3B File Offset: 0x00037D3B
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00039B43 File Offset: 0x00037D43
		internal uint GlobalWindowSize
		{
			get
			{
				return this.globalWindowSize;
			}
			set
			{
				if (value > (uint)GlobalSettings.MaxWindowSize)
				{
					this.globalWindowSize = (uint)GlobalSettings.MaxWindowSize;
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Window size is capped to: " + this.globalWindowSize);
					return;
				}
				this.globalWindowSize = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00039B80 File Offset: 0x00037D80
		internal Dictionary<string, SyncCollection> Collections
		{
			get
			{
				return this.collections;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00039B88 File Offset: 0x00037D88
		internal int DeviceSettingsHash
		{
			get
			{
				if (this.deviceSettingsHash == 0)
				{
					this.deviceSettingsHash = (this.DeviceEnableOutboundSMS.GetHashCode() ^ ((this.DevicePhoneNumberForSms == null) ? 0 : this.DevicePhoneNumberForSms.GetHashCode()));
				}
				return this.deviceSettingsHash;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00039BCE File Offset: 0x00037DCE
		internal override Command.ExecutionState ExecuteCommand()
		{
			throw new InvalidOperationException("Microsoft.Exchange.AirSync.SyncBase is not an executable command class");
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00039BDA File Offset: 0x00037DDA
		internal override void SetStateData(Command.StateData data)
		{
			this.DevicePhoneNumberForSms = data.DevicePhoneNumberForSms;
			this.DeviceEnableOutboundSMS = data.DeviceEnableOutboundSMS;
		}

		// Token: 0x060009D6 RID: 2518
		protected abstract string GetStatusString(SyncBase.ErrorCodeStatus error);

		// Token: 0x060009D7 RID: 2519 RVA: 0x00039BF4 File Offset: 0x00037DF4
		protected void InitializeVersionFactory(int version)
		{
			if (this.versionFactory == null)
			{
				this.versionFactory = AirSyncProtocolVersionParserBuilder.FromVersion(version);
			}
		}

		// Token: 0x04000618 RID: 1560
		private const int RootFolderHierarchyListMaxCapacity = 1000;

		// Token: 0x04000619 RID: 1561
		private const int RootFolderHierarchyListInitialSize = 100;

		// Token: 0x0400061A RID: 1562
		private IAirSyncVersionFactory versionFactory;

		// Token: 0x0400061B RID: 1563
		private SyncBase.ErrorCodeStatus globalStatus = SyncBase.ErrorCodeStatus.Success;

		// Token: 0x0400061C RID: 1564
		private Exception lastException;

		// Token: 0x0400061D RID: 1565
		private uint globalWindowSize = uint.MaxValue;

		// Token: 0x0400061E RID: 1566
		private Dictionary<string, SyncCollection> collections = new Dictionary<string, SyncCollection>();

		// Token: 0x0400061F RID: 1567
		private int deviceSettingsHash;

		// Token: 0x020000B8 RID: 184
		internal enum ErrorCodeStatus
		{
			// Token: 0x04000623 RID: 1571
			Success = 1,
			// Token: 0x04000624 RID: 1572
			ProtocolVersionMismatch,
			// Token: 0x04000625 RID: 1573
			InvalidSyncKey,
			// Token: 0x04000626 RID: 1574
			ProtocolError,
			// Token: 0x04000627 RID: 1575
			ServerError,
			// Token: 0x04000628 RID: 1576
			ClientServerConversion,
			// Token: 0x04000629 RID: 1577
			Conflict,
			// Token: 0x0400062A RID: 1578
			ObjectNotFound,
			// Token: 0x0400062B RID: 1579
			OutOfDisk,
			// Token: 0x0400062C RID: 1580
			NotificationGUID,
			// Token: 0x0400062D RID: 1581
			NotificationsNotProvisioned,
			// Token: 0x0400062E RID: 1582
			InvalidCollection,
			// Token: 0x0400062F RID: 1583
			UnprimedSyncState
		}

		// Token: 0x020000B9 RID: 185
		internal enum SyncCommandType
		{
			// Token: 0x04000631 RID: 1585
			Invalid,
			// Token: 0x04000632 RID: 1586
			Add,
			// Token: 0x04000633 RID: 1587
			Change,
			// Token: 0x04000634 RID: 1588
			Delete,
			// Token: 0x04000635 RID: 1589
			SoftDelete,
			// Token: 0x04000636 RID: 1590
			Fetch
		}
	}
}
