using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Data.Transport.Interop
{
	// Token: 0x02000004 RID: 4
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0597316F-7D23-3B38-9E6B-7B26F0867152")]
	[ComVisible(true)]
	public interface IProxyCallback
	{
		// Token: 0x06000009 RID: 9
		void AsyncCompletion();

		// Token: 0x0600000A RID: 10
		void SetWriteStream([MarshalAs(UnmanagedType.Interface)] IStream writeStream);

		// Token: 0x0600000B RID: 11
		void PutProperty(int id, byte[] value);

		// Token: 0x0600000C RID: 12
		void GetProperty(int id, out byte[] value);
	}
}
