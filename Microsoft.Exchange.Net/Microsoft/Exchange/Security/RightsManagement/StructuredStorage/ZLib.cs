using System;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A1A RID: 2586
	internal static class ZLib
	{
		// Token: 0x04002F9B RID: 12187
		internal const string ZLibVersion = "1.2.3";

		// Token: 0x02000A1B RID: 2587
		internal enum ErrorCode
		{
			// Token: 0x04002F9D RID: 12189
			Success,
			// Token: 0x04002F9E RID: 12190
			StreamEnd,
			// Token: 0x04002F9F RID: 12191
			NeedDictionary,
			// Token: 0x04002FA0 RID: 12192
			ErrorNo = -1,
			// Token: 0x04002FA1 RID: 12193
			StreamError = -2,
			// Token: 0x04002FA2 RID: 12194
			DataError = -3,
			// Token: 0x04002FA3 RID: 12195
			MemError = -4,
			// Token: 0x04002FA4 RID: 12196
			BufError = -5,
			// Token: 0x04002FA5 RID: 12197
			VersionError = -6
		}

		// Token: 0x02000A1C RID: 2588
		internal enum FlushCodes
		{
			// Token: 0x04002FA7 RID: 12199
			NoFlush,
			// Token: 0x04002FA8 RID: 12200
			SyncFlush = 2,
			// Token: 0x04002FA9 RID: 12201
			FullFlush,
			// Token: 0x04002FAA RID: 12202
			Finish
		}

		// Token: 0x02000A1D RID: 2589
		internal struct ZStream
		{
			// Token: 0x04002FAB RID: 12203
			public IntPtr PInBuf;

			// Token: 0x04002FAC RID: 12204
			public uint CbIn;

			// Token: 0x04002FAD RID: 12205
			public uint CbTotalIn;

			// Token: 0x04002FAE RID: 12206
			public IntPtr POutBuf;

			// Token: 0x04002FAF RID: 12207
			public uint CbOut;

			// Token: 0x04002FB0 RID: 12208
			public uint CbTotalOut;

			// Token: 0x04002FB1 RID: 12209
			public IntPtr PErrorMsgString;

			// Token: 0x04002FB2 RID: 12210
			public IntPtr PState;

			// Token: 0x04002FB3 RID: 12211
			public IntPtr PAlloc;

			// Token: 0x04002FB4 RID: 12212
			public IntPtr PFree;

			// Token: 0x04002FB5 RID: 12213
			public IntPtr POpaque;

			// Token: 0x04002FB6 RID: 12214
			public int DataType;

			// Token: 0x04002FB7 RID: 12215
			public uint Adler;

			// Token: 0x04002FB8 RID: 12216
			public uint Reserved;
		}
	}
}
