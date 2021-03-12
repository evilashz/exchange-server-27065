using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000005 RID: 5
	internal class Imap4BufferBuilder : BufferBuilder
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002706 File Offset: 0x00000906
		internal Imap4BufferBuilder(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002710 File Offset: 0x00000910
		internal new void Append(SecureString value)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				base.Secure = true;
				base.EnsureBuffer(value.Length);
				intPtr = Marshal.SecureStringToCoTaskMemUnicode(value);
				for (int i = 0; i < value.Length; i++)
				{
					char c = (char)Marshal.ReadInt16(intPtr, i * Marshal.SizeOf(typeof(short)));
					if (c > 'ÿ')
					{
						throw new ArgumentException(NetException.StringContainsInvalidCharacters, "value");
					}
					if (c == '\\' || c == '"')
					{
						base.Append(92);
					}
					base.Append((byte)c);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
				}
			}
		}
	}
}
