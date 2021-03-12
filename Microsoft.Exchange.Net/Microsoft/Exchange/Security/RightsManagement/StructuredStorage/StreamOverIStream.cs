using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x02000A18 RID: 2584
	internal class StreamOverIStream : Stream
	{
		// Token: 0x06003873 RID: 14451 RVA: 0x0008FEB3 File Offset: 0x0008E0B3
		public StreamOverIStream(IStream stream)
		{
			this.stream = stream;
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x0008FEC2 File Offset: 0x0008E0C2
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x0008FEC5 File Offset: 0x0008E0C5
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x0008FEC8 File Offset: 0x0008E0C8
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x0008FECC File Offset: 0x0008E0CC
		public override long Length
		{
			get
			{
				STATSTG statstg = default(STATSTG);
				this.stream.Stat(out statstg, STATFLAG.NoName);
				return statstg.cbSize;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x0008FEF6 File Offset: 0x0008E0F6
		// (set) Token: 0x06003879 RID: 14457 RVA: 0x0008FF06 File Offset: 0x0008E106
		public override long Position
		{
			get
			{
				return this.stream.Seek(0L, 1);
			}
			set
			{
				this.stream.Seek(value, 0);
			}
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x0008FF16 File Offset: 0x0008E116
		public override void Flush()
		{
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x0008FF18 File Offset: 0x0008E118
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, (int)origin);
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x0008FF27 File Offset: 0x0008E127
		public override void SetLength(long value)
		{
			this.stream.SetSize(value);
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x0008FF38 File Offset: 0x0008E138
		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			fixed (byte* ptr = buffer)
			{
				return this.stream.Read(new IntPtr((void*)((byte*)ptr + offset)), count);
			}
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x0008FF78 File Offset: 0x0008E178
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			int num;
			fixed (byte* ptr = buffer)
			{
				num = this.stream.Write(new IntPtr((void*)((byte*)ptr + offset)), count);
			}
			if (num != count)
			{
				throw new InvalidOperationException("Failed to write to IStream.");
			}
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x0008FFC4 File Offset: 0x0008E1C4
		internal void ReplaceIStream(IStream newStream)
		{
			this.stream = newStream;
		}

		// Token: 0x04002F9A RID: 12186
		private IStream stream;
	}
}
