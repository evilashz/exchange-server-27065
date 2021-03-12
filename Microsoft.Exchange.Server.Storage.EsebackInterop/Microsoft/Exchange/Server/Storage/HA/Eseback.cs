using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000010 RID: 16
	public static class Eseback
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00001948 File Offset: 0x00000D48
		public unsafe static int BackupRestoreRegister(string displayName, Eseback.Flags flags, string endpointAnnotation, IEsebackCallbacks callbacks, Guid crimsonPublisher)
		{
			EsebackCallbacks.ManagedCallbacks = callbacks;
			_ESEBACK_CALLBACKS _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z = <Module>.__unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z;
			*(ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z + 8) = <Module>.__unep@?DoneWithInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KKPEAX@Z;
			*(ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z + 16) = <Module>.__unep@?GetDatabasesInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@PEAKPEAPEAU_INSTANCE_BACKUP_INFO@@K@Z;
			*(ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z + 24) = <Module>.__unep@?FreeDatabasesInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@KPEAU_INSTANCE_BACKUP_INFO@@@Z;
			*(ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z + 32) = <Module>.__unep@?IsSGReplicated@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAHKPEAGPEAKPEAPEAU_LOGSHIP_INFO@@@Z;
			*(ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z + 40) = <Module>.__unep@?FreeShipLogInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJKPEAU_LOGSHIP_INFO@@@Z;
			*(ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z + 48) = <Module>.__unep@?ServerAccessCheck@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJXZ;
			*(ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z + 56) = <Module>.__unep@?ErrESECBTrace@NativeCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJQEBDZZ;
			Guid guid = crimsonPublisher;
			ref void void& = ref guid;
			_GUID guid2;
			cpblk(ref guid2, ref void&, 16);
			ushort* ptr = null;
			ushort* ptr2 = null;
			int result;
			try
			{
				ptr = StructConversion.WszFromString(displayName);
				ptr2 = StructConversion.WszFromString(endpointAnnotation);
				result = <Module>.HrESEBackupRestoreRegister2(ptr, flags, ptr2, ref _unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z, ref guid2);
			}
			finally
			{
				StructConversion.FreeWsz(ptr);
				StructConversion.FreeWsz(ptr2);
			}
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00001298 File Offset: 0x00000698
		public static int BackupRestoreUnregister()
		{
			int result = <Module>.HrESEBackupRestoreUnregister();
			EsebackCallbacks.ManagedCallbacks = null;
			return result;
		}

		// Token: 0x02000011 RID: 17
		[Flags]
		public enum Flags : uint
		{
			// Token: 0x04000097 RID: 151
			BACKUP_DATA_TRANSFER_SHAREDMEM = 2048U,
			// Token: 0x04000098 RID: 152
			BACKUP_DATA_TRANSFER_SOCKETS = 1024U,
			// Token: 0x04000099 RID: 153
			REGISTER_BACKUP_LOCAL = 4096U,
			// Token: 0x0400009A RID: 154
			REGISTER_BACKUP_REMOTE = 8192U,
			// Token: 0x0400009B RID: 155
			REGISTER_ONLINE_RESTORE_LOCAL = 16384U,
			// Token: 0x0400009C RID: 156
			REGISTER_ONLINE_RESTORE_REMOTE = 32768U,
			// Token: 0x0400009D RID: 157
			REGISTER_SURROGATE_BACKUP_LOCAL = 4194304U,
			// Token: 0x0400009E RID: 158
			REGISTER_SURROGATE_BACKUP_REMOTE = 8388608U,
			// Token: 0x0400009F RID: 159
			REGISTER_LOGSHIP_SEED_LOCAL = 16777216U,
			// Token: 0x040000A0 RID: 160
			REGISTER_LOGSHIP_SEED_REMOTE = 33554432U,
			// Token: 0x040000A1 RID: 161
			REGISTER_LOGSHIP_UPDATER_LOCAL = 67108864U,
			// Token: 0x040000A2 RID: 162
			REGISTER_LOGSHIP_UPDATER_REMOTE = 134217728U,
			// Token: 0x040000A3 RID: 163
			REGISTER_EXCHANGE_SERVER_LOCAL = 268435456U,
			// Token: 0x040000A4 RID: 164
			REGISTER_EXCHANGE_SERVER_REMOTE = 536870912U
		}
	}
}
