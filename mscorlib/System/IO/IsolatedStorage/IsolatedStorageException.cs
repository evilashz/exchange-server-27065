using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020001B1 RID: 433
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		// Token: 0x06001B4D RID: 6989 RVA: 0x0005C8B6 File Offset: 0x0005AAB6
		public IsolatedStorageException() : base(Environment.GetResourceString("IsolatedStorage_Exception"))
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0005C8D3 File Offset: 0x0005AAD3
		public IsolatedStorageException(string message) : base(message)
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0005C8E7 File Offset: 0x0005AAE7
		public IsolatedStorageException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0005C8FC File Offset: 0x0005AAFC
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
