using System;

namespace Microsoft.Isam.Esent.Interop.Windows7
{
	// Token: 0x0200030B RID: 779
	public static class Windows7Api
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x0001CA0D File Offset: 0x0001AC0D
		public static void JetConfigureProcessForCrashDump(CrashDumpGrbit grbit)
		{
			Api.Check(Api.Impl.JetConfigureProcessForCrashDump(grbit));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0001CA20 File Offset: 0x0001AC20
		public static void JetPrereadKeys(JET_SESID sesid, JET_TABLEID tableid, byte[][] keys, int[] keyLengths, int keyIndex, int keyCount, out int keysPreread, PrereadKeysGrbit grbit)
		{
			Api.Check(Api.Impl.JetPrereadKeys(sesid, tableid, keys, keyLengths, keyIndex, keyCount, out keysPreread, grbit));
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0001CA49 File Offset: 0x0001AC49
		public static void JetPrereadKeys(JET_SESID sesid, JET_TABLEID tableid, byte[][] keys, int[] keyLengths, int keyCount, out int keysPreread, PrereadKeysGrbit grbit)
		{
			Windows7Api.JetPrereadKeys(sesid, tableid, keys, keyLengths, 0, keyCount, out keysPreread, grbit);
		}
	}
}
