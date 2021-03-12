using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200000D RID: 13
	internal sealed class MsiNativeMethods
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002F1C File Offset: 0x0000111C
		private MsiNativeMethods()
		{
		}

		// Token: 0x06000043 RID: 67
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiDetermineApplicablePatchesW")]
		public static extern uint DetermineApplicablePatches(string packagePath, int count, [In] [Out] MsiNativeMethods.PatchSequenceInfo[] patches);

		// Token: 0x06000044 RID: 68
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiOpenDatabaseW")]
		public static extern uint OpenDatabase(string databasePath, IntPtr pPersist, out SafeMsiHandle database);

		// Token: 0x06000045 RID: 69
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiDatabaseOpenViewW")]
		public static extern uint DatabaseOpenView(SafeMsiHandle database, string query, out SafeMsiHandle view);

		// Token: 0x06000046 RID: 70
		[DllImport("msi", CharSet = CharSet.Auto, EntryPoint = "MsiViewExecute")]
		public static extern uint ViewExecute(SafeMsiHandle view, IntPtr record);

		// Token: 0x06000047 RID: 71
		[DllImport("msi", CharSet = CharSet.Auto, EntryPoint = "MsiViewFetch")]
		public static extern uint ViewFetch(SafeMsiHandle view, out SafeMsiHandle record);

		// Token: 0x06000048 RID: 72
		[DllImport("msi", CharSet = CharSet.Unicode, EntryPoint = "MsiRecordGetStringW")]
		public static extern uint RecordGetString(SafeMsiHandle record, uint field, StringBuilder data, ref uint count);

		// Token: 0x06000049 RID: 73
		[DllImport("msi", CharSet = CharSet.Auto, EntryPoint = "MsiRecordDataSize")]
		public static extern uint RecordDataSize(SafeMsiHandle record, uint field);

		// Token: 0x0600004A RID: 74
		[DllImport("msi", CharSet = CharSet.Auto, EntryPoint = "MsiRecordReadStream")]
		public static extern uint RecordReadStream(SafeMsiHandle record, uint field, byte[] buffer, ref int bufferLength);

		// Token: 0x0600004B RID: 75 RVA: 0x00002F24 File Offset: 0x00001124
		public static int ComparePatchSequence(MsiNativeMethods.PatchSequenceInfo p1, MsiNativeMethods.PatchSequenceInfo p2)
		{
			if (p1.Order < p2.Order)
			{
				return -1;
			}
			if (p1.Order == p2.Order)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0200000E RID: 14
		public enum PatchDataType
		{
			// Token: 0x04000025 RID: 37
			PatchFile,
			// Token: 0x04000026 RID: 38
			XmlPath,
			// Token: 0x04000027 RID: 39
			XmlBlob
		}

		// Token: 0x0200000F RID: 15
		public enum ReturnCode
		{
			// Token: 0x04000029 RID: 41
			ErrorSuccess,
			// Token: 0x0400002A RID: 42
			ErrorMoreData = 234,
			// Token: 0x0400002B RID: 43
			ErrorNoMoreItems = 259
		}

		// Token: 0x02000010 RID: 16
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct PatchSequenceInfo
		{
			// Token: 0x0400002C RID: 44
			public string PatchData;

			// Token: 0x0400002D RID: 45
			public MsiNativeMethods.PatchDataType PatchDataType;

			// Token: 0x0400002E RID: 46
			public int Order;

			// Token: 0x0400002F RID: 47
			public uint Status;
		}
	}
}
