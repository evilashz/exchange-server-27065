using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000021 RID: 33
	public class ESEBACK_CALLBACKS
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000318D File Offset: 0x0000138D
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003195 File Offset: 0x00001395
		public PfnErrESECBPrepareInstanceForBackup pfnPrepareInstance { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000319E File Offset: 0x0000139E
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000031A6 File Offset: 0x000013A6
		public PfnErrESECBDoneWithInstanceForBackup pfnDoneWithInstance { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000031AF File Offset: 0x000013AF
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000031B7 File Offset: 0x000013B7
		public PfnErrESECBGetDatabasesInfo pfnGetDatabasesInfo { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000031C0 File Offset: 0x000013C0
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000031C8 File Offset: 0x000013C8
		public PfnErrESECBIsSGReplicated pfnIsSGReplicated { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000031D1 File Offset: 0x000013D1
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000031D9 File Offset: 0x000013D9
		public PfnErrESECBServerAccessCheck pfnServerAccessCheck { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000031E2 File Offset: 0x000013E2
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000031EA File Offset: 0x000013EA
		public PfnErrESECBTrace pfnTrace { get; set; }

		// Token: 0x06000084 RID: 132 RVA: 0x000031F3 File Offset: 0x000013F3
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ESEBACK_CALLBACKS()", new object[0]);
		}
	}
}
