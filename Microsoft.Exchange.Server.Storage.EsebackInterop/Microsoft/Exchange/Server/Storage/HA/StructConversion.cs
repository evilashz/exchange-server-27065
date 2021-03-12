using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200000F RID: 15
	internal static class StructConversion
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000012B0 File Offset: 0x000006B0
		private unsafe static _DATABASE_BACKUP_INFO* AllocateUnmanagedArray<_DATABASE_BACKUP_INFO>(Array managed)
		{
			ulong num = (ulong)((long)managed.Length);
			_DATABASE_BACKUP_INFO* ptr = <Module>.@new((num > 384307168202282325UL) ? ulong.MaxValue : (num * 48UL));
			if (0L == ptr)
			{
				throw new OutOfMemoryException("Failed to allocate unmanaged array");
			}
			initblk(ptr, 0, (long)managed.Length * 48L);
			return ptr;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00001320 File Offset: 0x00000720
		private unsafe static _INSTANCE_BACKUP_INFO* AllocateUnmanagedArray<_INSTANCE_BACKUP_INFO>(Array managed)
		{
			ulong num = (ulong)((long)managed.Length);
			_INSTANCE_BACKUP_INFO* ptr = <Module>.@new((num > 384307168202282325UL) ? ulong.MaxValue : (num * 48UL));
			if (0L == ptr)
			{
				throw new OutOfMemoryException("Failed to allocate unmanaged array");
			}
			initblk(ptr, 0, (long)managed.Length * 48L);
			return ptr;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00001390 File Offset: 0x00000790
		private unsafe static _LOGSHIP_INFO* AllocateUnmanagedArray<_LOGSHIP_INFO>(Array managed)
		{
			ulong num = (ulong)((long)managed.Length);
			_LOGSHIP_INFO* ptr = <Module>.@new((num > 1152921504606846975UL) ? ulong.MaxValue : (num * 16UL));
			if (0L == ptr)
			{
				throw new OutOfMemoryException("Failed to allocate unmanaged array");
			}
			initblk(ptr, 0, (long)managed.Length * 16L);
			return ptr;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00001308 File Offset: 0x00000708
		private unsafe static void FreeUnmanagedArray<_DATABASE_BACKUP_INFO>(_DATABASE_BACKUP_INFO* rgT)
		{
			if (rgT != null)
			{
				<Module>.delete(rgT);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00001378 File Offset: 0x00000778
		private unsafe static void FreeUnmanagedArray<_INSTANCE_BACKUP_INFO>(_INSTANCE_BACKUP_INFO* rgT)
		{
			if (rgT != null)
			{
				<Module>.delete(rgT);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000013E8 File Offset: 0x000007E8
		private unsafe static void FreeUnmanagedArray<_LOGSHIP_INFO>(_LOGSHIP_INFO* rgT)
		{
			if (rgT != null)
			{
				<Module>.delete(rgT);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00001400 File Offset: 0x00000800
		private unsafe static _DATABASE_BACKUP_INFO MakeUnmanagedDatabaseBackupInfo(MDATABASE_BACKUP_INFO managed)
		{
			_DATABASE_BACKUP_INFO result = Marshal.StringToHGlobalUni(managed.Name).ToPointer();
			string text = string.Format("{0}\0\0\0", managed.Path);
			*(ref result + 8) = text.Length;
			IntPtr intPtr = Marshal.StringToHGlobalUni(text);
			*(ref result + 16) = intPtr.ToPointer();
			Guid guid = managed.Guid;
			ref void void& = ref guid;
			cpblk(ref result + 24, ref void&, 16);
			*(ref result + 40) = 0;
			*(ref result + 44) = (int)managed.Flags;
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00001484 File Offset: 0x00000884
		private unsafe static void FreeUnmanagedDatabaseBackupInfo(_DATABASE_BACKUP_INFO databaseInfo)
		{
			if (databaseInfo != null)
			{
				IntPtr hglobal = new IntPtr(databaseInfo);
				Marshal.FreeHGlobal(hglobal);
			}
			if (*(ref databaseInfo + 16) != 0L)
			{
				IntPtr hglobal2 = new IntPtr(*(ref databaseInfo + 16));
				Marshal.FreeHGlobal(hglobal2);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000014C4 File Offset: 0x000008C4
		private unsafe static _DATABASE_BACKUP_INFO* MakeUnmanagedDatabaseBackupInfos(MDATABASE_BACKUP_INFO[] databases)
		{
			_DATABASE_BACKUP_INFO* ptr = StructConversion.AllocateUnmanagedArray<_DATABASE_BACKUP_INFO>(databases);
			int num = 0;
			if (0 < databases.Length)
			{
				_DATABASE_BACKUP_INFO* ptr2 = ptr;
				do
				{
					_DATABASE_BACKUP_INFO database_BACKUP_INFO = StructConversion.MakeUnmanagedDatabaseBackupInfo(databases[num]);
					cpblk(ptr2, ref database_BACKUP_INFO, 48);
					num++;
					ptr2 += 48L;
				}
				while (num < databases.Length);
			}
			return ptr;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000150C File Offset: 0x0000090C
		private unsafe static void FreeUnmanagedDatabaseBackupInfos(int cInfo, _DATABASE_BACKUP_INFO* rgDatabaseInfo)
		{
			if (rgDatabaseInfo != null)
			{
				if (0 < cInfo)
				{
					_DATABASE_BACKUP_INFO* ptr = rgDatabaseInfo;
					int num = cInfo;
					do
					{
						StructConversion.FreeUnmanagedDatabaseBackupInfo(*ptr);
						ptr += 48L;
						num += -1;
					}
					while (num > 0);
				}
				<Module>.delete(rgDatabaseInfo);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00001544 File Offset: 0x00000944
		private unsafe static _INSTANCE_BACKUP_INFO MakeUnmanagedBackupInfo(MINSTANCE_BACKUP_INFO managed)
		{
			_INSTANCE_BACKUP_INFO result = managed.Instance.Value.ToInt64();
			IntPtr intPtr = Marshal.StringToHGlobalUni(managed.Name);
			*(ref result + 8) = intPtr.ToPointer();
			*(ref result + 16) = 0;
			*(ref result + 20) = managed.Databases.Length;
			*(ref result + 24) = StructConversion.MakeUnmanagedDatabaseBackupInfos(managed.Databases);
			*(ref result + 32) = 0;
			*(ref result + 40) = 0L;
			return result;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000015B8 File Offset: 0x000009B8
		private unsafe static void FreeUnmanagedBackupInfo(_INSTANCE_BACKUP_INFO instanceInfo)
		{
			if (*(ref instanceInfo + 8) != 0L)
			{
				IntPtr hglobal = new IntPtr(*(ref instanceInfo + 8));
				Marshal.FreeHGlobal(hglobal);
			}
			StructConversion.FreeUnmanagedDatabaseBackupInfos(*(ref instanceInfo + 20), *(ref instanceInfo + 24));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000015F0 File Offset: 0x000009F0
		private unsafe static _LOGSHIP_INFO MakeUnmanagedLogshipInfo(MLOGSHIP_INFO managed)
		{
			_LOGSHIP_INFO type = managed.Type;
			*(ref type + 4) = managed.Name.Length;
			IntPtr intPtr = Marshal.StringToHGlobalUni(managed.Name);
			*(ref type + 8) = intPtr.ToPointer();
			return type;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00001634 File Offset: 0x00000A34
		private unsafe static void FreeUnmanagedLogshipInfo(_LOGSHIP_INFO logshipInfo)
		{
			if (*(ref logshipInfo + 8) != 0L)
			{
				IntPtr hglobal = new IntPtr(*(ref logshipInfo + 8));
				Marshal.FreeHGlobal(hglobal);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000011A0 File Offset: 0x000005A0
		public unsafe static ushort* WszFromString(string s)
		{
			return (ushort*)Marshal.StringToHGlobalUni(s).ToPointer();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000011BC File Offset: 0x000005BC
		public unsafe static void FreeWsz(ushort* wsz)
		{
			if (wsz != null)
			{
				IntPtr hglobal = new IntPtr(wsz);
				Marshal.FreeHGlobal(hglobal);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000011DC File Offset: 0x000005DC
		public static _GUID GetUnmanagedGuid(Guid managed)
		{
			ref void void& = ref managed;
			_GUID result;
			cpblk(ref result, ref void&, 16);
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000173C File Offset: 0x00000B3C
		public unsafe static _INSTANCE_BACKUP_INFO* MakeUnmanagedBackupInfos(MINSTANCE_BACKUP_INFO[] instances)
		{
			int num = instances.Length;
			_INSTANCE_BACKUP_INFO* ptr = null;
			_INSTANCE_BACKUP_INFO* result;
			try
			{
				ptr = StructConversion.AllocateUnmanagedArray<_INSTANCE_BACKUP_INFO>(instances);
				for (int i = 0; i < num; i++)
				{
					_INSTANCE_BACKUP_INFO instance_BACKUP_INFO = StructConversion.MakeUnmanagedBackupInfo(instances[i]);
					cpblk((long)i * 48L / (long)sizeof(_INSTANCE_BACKUP_INFO) + ptr, ref instance_BACKUP_INFO, 48);
				}
				result = ptr;
			}
			catch (object obj)
			{
				NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(null);
				StructConversion.FreeUnmanagedBackupInfos(num, ptr);
				throw;
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000165C File Offset: 0x00000A5C
		public unsafe static void FreeUnmanagedBackupInfos(int cInfo, _INSTANCE_BACKUP_INFO* rgInfo)
		{
			if (rgInfo != null)
			{
				if (0 < cInfo)
				{
					_INSTANCE_BACKUP_INFO* ptr = rgInfo;
					int num = cInfo;
					do
					{
						_INSTANCE_BACKUP_INFO instance_BACKUP_INFO = *ptr;
						if (*(ref instance_BACKUP_INFO + 8) != 0L)
						{
							IntPtr hglobal = new IntPtr(*(ref instance_BACKUP_INFO + 8));
							Marshal.FreeHGlobal(hglobal);
						}
						StructConversion.FreeUnmanagedDatabaseBackupInfos(*(ref instance_BACKUP_INFO + 20), *(ref instance_BACKUP_INFO + 24));
						ptr += 48L;
						num += -1;
					}
					while (num > 0);
				}
				<Module>.delete(rgInfo);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000017CC File Offset: 0x00000BCC
		public unsafe static _LOGSHIP_INFO* MakeUnmanagedLogshipInfos(MLOGSHIP_INFO[] infos)
		{
			int num = infos.Length;
			_LOGSHIP_INFO* ptr = null;
			_LOGSHIP_INFO* result;
			try
			{
				ptr = StructConversion.AllocateUnmanagedArray<_LOGSHIP_INFO>(infos);
				for (int i = 0; i < num; i++)
				{
					_LOGSHIP_INFO logship_INFO = StructConversion.MakeUnmanagedLogshipInfo(infos[i]);
					cpblk((long)i * 16L / (long)sizeof(_LOGSHIP_INFO) + ptr, ref logship_INFO, 16);
				}
				result = ptr;
			}
			catch (object obj)
			{
				NullExecutionContext.Instance.Diagnostics.OnExceptionCatch(null);
				StructConversion.FreeUnmanagedLogshipInfos(num, ptr);
				throw;
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000016BC File Offset: 0x00000ABC
		public unsafe static void FreeUnmanagedLogshipInfos(int cInfo, _LOGSHIP_INFO* rgInfo)
		{
			if (rgInfo != null)
			{
				if (0 < cInfo)
				{
					_LOGSHIP_INFO* ptr = rgInfo;
					int num = cInfo;
					do
					{
						_LOGSHIP_INFO logship_INFO = *ptr;
						if (*(ref logship_INFO + 8) != 0L)
						{
							IntPtr hglobal = new IntPtr(*(ref logship_INFO + 8));
							Marshal.FreeHGlobal(hglobal);
						}
						ptr += 16L;
						num += -1;
					}
					while (num > 0);
				}
				<Module>.delete(rgInfo);
			}
		}
	}
}
