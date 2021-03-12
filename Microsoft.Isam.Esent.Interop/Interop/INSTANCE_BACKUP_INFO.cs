using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000028 RID: 40
	[StructLayout(LayoutKind.Sequential)]
	public class INSTANCE_BACKUP_INFO
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00008961 File Offset: 0x00006B61
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00008969 File Offset: 0x00006B69
		public long hInstanceId { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00008972 File Offset: 0x00006B72
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000897A File Offset: 0x00006B7A
		public string wszInstanceName { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00008983 File Offset: 0x00006B83
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000898B File Offset: 0x00006B8B
		public int ulIconIndexInstance { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00008994 File Offset: 0x00006B94
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000899C File Offset: 0x00006B9C
		public DATABASE_BACKUP_INFO[] rgDatabase { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000089A5 File Offset: 0x00006BA5
		// (set) Token: 0x0600033D RID: 829 RVA: 0x000089AD File Offset: 0x00006BAD
		public ESE_ICON_DESCRIPTION[] rgIconDescription { get; set; }

		// Token: 0x0600033E RID: 830 RVA: 0x000089B8 File Offset: 0x00006BB8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "INSTANCE_BACKUP_INFO({0})", new object[]
			{
				this.wszInstanceName
			});
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000089E8 File Offset: 0x00006BE8
		internal NATIVE_INSTANCE_BACKUP_INFO GetNativeInstanceBackupInfo()
		{
			NATIVE_ESE_ICON_DESCRIPTION[] nativeEseIconDescriptions = INSTANCE_BACKUP_INFO.GetNativeEseIconDescriptions(this.rgIconDescription);
			NATIVE_DATABASE_BACKUP_INFO[] nativeDatabaseBackupInfos = INSTANCE_BACKUP_INFO.GetNativeDatabaseBackupInfos(this.rgDatabase);
			int cDatabase = (nativeDatabaseBackupInfos == null) ? 0 : nativeDatabaseBackupInfos.Length;
			int cIconDescription = (nativeEseIconDescriptions == null) ? 0 : nativeEseIconDescriptions.Length;
			IntPtr rgIconDescription = Eseback.ConvertStructArrayToIntPtr<NATIVE_ESE_ICON_DESCRIPTION>(nativeEseIconDescriptions);
			IntPtr rgDatabase = Eseback.ConvertStructArrayToIntPtr<NATIVE_DATABASE_BACKUP_INFO>(nativeDatabaseBackupInfos);
			return new NATIVE_INSTANCE_BACKUP_INFO
			{
				hInstanceId = this.hInstanceId,
				rgIconDescription = rgIconDescription,
				cDatabase = (uint)cDatabase,
				rgDatabase = rgDatabase,
				wszInstanceName = Marshal.StringToHGlobalUni(this.wszInstanceName),
				cIconDescription = (uint)cIconDescription,
				ulIconIndexInstance = (uint)this.ulIconIndexInstance
			};
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00008A90 File Offset: 0x00006C90
		private static NATIVE_DATABASE_BACKUP_INFO[] GetNativeDatabaseBackupInfos(DATABASE_BACKUP_INFO[] databaseBackupInfos)
		{
			NATIVE_DATABASE_BACKUP_INFO[] array = null;
			if (databaseBackupInfos != null)
			{
				array = new NATIVE_DATABASE_BACKUP_INFO[databaseBackupInfos.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = databaseBackupInfos[i].GetNativeDatabaseBackupInfo();
				}
			}
			return array;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008AD0 File Offset: 0x00006CD0
		private static NATIVE_ESE_ICON_DESCRIPTION[] GetNativeEseIconDescriptions(ESE_ICON_DESCRIPTION[] eseIconDescriptions)
		{
			NATIVE_ESE_ICON_DESCRIPTION[] array = null;
			if (eseIconDescriptions != null)
			{
				array = new NATIVE_ESE_ICON_DESCRIPTION[eseIconDescriptions.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = eseIconDescriptions[i].GetNativeEseIconDescription();
				}
			}
			return array;
		}
	}
}
