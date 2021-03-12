using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C91 RID: 3217
	internal class SessionKey
	{
		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x000BEB6A File Offset: 0x000BCD6A
		public virtual byte[] Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x000BEB72 File Offset: 0x000BCD72
		protected SessionKey()
		{
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x000BEB7C File Offset: 0x000BCD7C
		internal unsafe SessionKey(byte[] memory)
		{
			fixed (IntPtr* ptr = memory)
			{
				SessionKey.KerberosSessionKey kerberosSessionKey = (SessionKey.KerberosSessionKey)Marshal.PtrToStructure(new IntPtr((void*)ptr), typeof(SessionKey.KerberosSessionKey));
				using (SafeContextBuffer safeContextBuffer = new SafeContextBuffer(kerberosSessionKey.Key))
				{
					if (kerberosSessionKey.Length <= 0)
					{
						this.key = new byte[0];
					}
					else
					{
						this.key = new byte[kerberosSessionKey.Length];
						Marshal.Copy(safeContextBuffer.DangerousGetHandle(), this.key, 0, kerberosSessionKey.Length);
					}
				}
			}
		}

		// Token: 0x04003C15 RID: 15381
		private readonly byte[] key;

		// Token: 0x04003C16 RID: 15382
		public static readonly int Size = Marshal.SizeOf(typeof(SessionKey.KerberosSessionKey));

		// Token: 0x02000C92 RID: 3218
		private struct KerberosSessionKey
		{
			// Token: 0x04003C17 RID: 15383
			public readonly int Length;

			// Token: 0x04003C18 RID: 15384
			public readonly IntPtr Key;
		}
	}
}
