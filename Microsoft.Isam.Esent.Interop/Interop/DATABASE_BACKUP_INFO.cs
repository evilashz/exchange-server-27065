using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000006 RID: 6
	public class DATABASE_BACKUP_INFO
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000023BC File Offset: 0x000005BC
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000023C4 File Offset: 0x000005C4
		public string wszDatabaseDisplayName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000023CD File Offset: 0x000005CD
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000023D5 File Offset: 0x000005D5
		public string[] wszDatabaseStreams { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000023DE File Offset: 0x000005DE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000023E6 File Offset: 0x000005E6
		public Guid guidDatabase { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000023EF File Offset: 0x000005EF
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000023F7 File Offset: 0x000005F7
		public int ulIconIndexDatabase { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002400 File Offset: 0x00000600
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002408 File Offset: 0x00000608
		public DatabaseBackupInfoFlags fDatabaseFlags { get; set; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002414 File Offset: 0x00000614
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "DATABASE_BACKUP_INFO({0}:{1}:{2})", new object[]
			{
				this.wszDatabaseDisplayName,
				(this.wszDatabaseStreams == null) ? string.Empty : this.wszDatabaseStreams[0],
				this.fDatabaseFlags
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000246C File Offset: 0x0000066C
		internal NATIVE_DATABASE_BACKUP_INFO GetNativeDatabaseBackupInfo()
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			for (int i = 0; i < this.wszDatabaseStreams.Length; i++)
			{
				stringBuilder.Append(this.wszDatabaseStreams[i]);
				stringBuilder.Append('\0');
			}
			stringBuilder.Append('\0');
			string text = stringBuilder.ToString();
			return new NATIVE_DATABASE_BACKUP_INFO
			{
				cwDatabaseStreams = (uint)(text.Length + 1),
				fDatabaseFlags = (uint)this.fDatabaseFlags,
				guidDatabase = this.guidDatabase,
				ulIconIndexDatabase = (uint)this.ulIconIndexDatabase,
				wszDatabaseDisplayName = Marshal.StringToHGlobalUni(this.wszDatabaseDisplayName),
				wszDatabaseStreams = Marshal.StringToHGlobalUni(text)
			};
		}
	}
}
