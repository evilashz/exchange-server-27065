using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C85 RID: 3205
	[StructLayout(LayoutKind.Sequential)]
	internal class ChannelBindingToken
	{
		// Token: 0x060046DA RID: 18138 RVA: 0x000BE654 File Offset: 0x000BC854
		internal unsafe ChannelBindingToken(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				ChannelBindingToken.CBT cbt = (ChannelBindingToken.CBT)Marshal.PtrToStructure(new IntPtr((void*)ptr), typeof(ChannelBindingToken.CBT));
				using (SafeContextBuffer safeContextBuffer = new SafeContextBuffer(cbt.Token))
				{
					if (cbt.Length == 0)
					{
						this.Buffer = new byte[0];
					}
					else
					{
						this.Buffer = new byte[cbt.Length];
						Marshal.Copy(safeContextBuffer.DangerousGetHandle(), this.Buffer, 0, cbt.Length);
					}
				}
			}
		}

		// Token: 0x04003BBC RID: 15292
		public readonly byte[] Buffer;

		// Token: 0x04003BBD RID: 15293
		public static readonly int Size = Marshal.SizeOf(typeof(ChannelBindingToken.CBT));

		// Token: 0x02000C86 RID: 3206
		private struct CBT
		{
			// Token: 0x04003BBE RID: 15294
			public int Length;

			// Token: 0x04003BBF RID: 15295
			public IntPtr Token;
		}
	}
}
