using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001AA RID: 426
	internal sealed class UnmanagedMemoryStreamWrapper : MemoryStream
	{
		// Token: 0x06001AC4 RID: 6852 RVA: 0x0005A228 File Offset: 0x00058428
		internal UnmanagedMemoryStreamWrapper(UnmanagedMemoryStream stream)
		{
			this._unmanagedStream = stream;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0005A237 File Offset: 0x00058437
		public override bool CanRead
		{
			get
			{
				return this._unmanagedStream.CanRead;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0005A244 File Offset: 0x00058444
		public override bool CanSeek
		{
			get
			{
				return this._unmanagedStream.CanSeek;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x0005A251 File Offset: 0x00058451
		public override bool CanWrite
		{
			get
			{
				return this._unmanagedStream.CanWrite;
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0005A260 File Offset: 0x00058460
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._unmanagedStream.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0005A298 File Offset: 0x00058498
		public override void Flush()
		{
			this._unmanagedStream.Flush();
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0005A2A5 File Offset: 0x000584A5
		public override byte[] GetBuffer()
		{
			throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_MemStreamBuffer"));
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0005A2B6 File Offset: 0x000584B6
		public override bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			buffer = default(ArraySegment<byte>);
			return false;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0005A2C0 File Offset: 0x000584C0
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x0005A2CE File Offset: 0x000584CE
		public override int Capacity
		{
			get
			{
				return (int)this._unmanagedStream.Capacity;
			}
			set
			{
				throw new IOException(Environment.GetResourceString("IO.IO_FixedCapacity"));
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0005A2DF File Offset: 0x000584DF
		public override long Length
		{
			get
			{
				return this._unmanagedStream.Length;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0005A2EC File Offset: 0x000584EC
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x0005A2F9 File Offset: 0x000584F9
		public override long Position
		{
			get
			{
				return this._unmanagedStream.Position;
			}
			set
			{
				this._unmanagedStream.Position = value;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0005A307 File Offset: 0x00058507
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			return this._unmanagedStream.Read(buffer, offset, count);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0005A317 File Offset: 0x00058517
		public override int ReadByte()
		{
			return this._unmanagedStream.ReadByte();
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0005A324 File Offset: 0x00058524
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this._unmanagedStream.Seek(offset, loc);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0005A334 File Offset: 0x00058534
		[SecuritySafeCritical]
		public override byte[] ToArray()
		{
			if (!this._unmanagedStream._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this._unmanagedStream.CanRead)
			{
				__Error.ReadNotSupported();
			}
			byte[] array = new byte[this._unmanagedStream.Length];
			Buffer.Memcpy(array, 0, this._unmanagedStream.Pointer, 0, (int)this._unmanagedStream.Length);
			return array;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0005A397 File Offset: 0x00058597
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._unmanagedStream.Write(buffer, offset, count);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x0005A3A7 File Offset: 0x000585A7
		public override void WriteByte(byte value)
		{
			this._unmanagedStream.WriteByte(value);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x0005A3B8 File Offset: 0x000585B8
		public override void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("ArgumentNull_Stream"));
			}
			if (!this._unmanagedStream._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			byte[] array = this.ToArray();
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0005A40E File Offset: 0x0005860E
		public override void SetLength(long value)
		{
			base.SetLength(value);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0005A418 File Offset: 0x00058618
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			return this._unmanagedStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0005A4D0 File Offset: 0x000586D0
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._unmanagedStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x0005A4DE File Offset: 0x000586DE
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0005A4F0 File Offset: 0x000586F0
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x04000941 RID: 2369
		private UnmanagedMemoryStream _unmanagedStream;
	}
}
