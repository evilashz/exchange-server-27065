using System;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000137 RID: 311
	internal abstract class ReadableWritableDataStorage : ReadableDataStorage
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0006B960 File Offset: 0x00069B60
		public ReadableWritableDataStorage()
		{
		}

		// Token: 0x06000C15 RID: 3093
		public abstract void Write(long position, byte[] buffer, int offset, int count);

		// Token: 0x06000C16 RID: 3094
		public abstract void SetLength(long length);

		// Token: 0x06000C17 RID: 3095 RVA: 0x0006B968 File Offset: 0x00069B68
		public virtual StreamOnDataStorage OpenWriteStream(bool append)
		{
			base.ThrowIfDisposed();
			if (append)
			{
				return new AppendStreamOnDataStorage(this);
			}
			return new ReadWriteStreamOnDataStorage(this);
		}
	}
}
