using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x0200014A RID: 330
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IOCostStream : BaseStream
	{
		// Token: 0x06000D62 RID: 3426 RVA: 0x00038686 File Offset: 0x00036886
		public IOCostStream(Stream stream) : base(stream)
		{
			this.writing = new Stopwatch();
			this.reading = new Stopwatch();
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000386A5 File Offset: 0x000368A5
		public TimeSpan Writing
		{
			get
			{
				return this.writing.Elapsed;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000386B2 File Offset: 0x000368B2
		public TimeSpan Reading
		{
			get
			{
				return this.reading.Elapsed;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x000386BF File Offset: 0x000368BF
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x000386C7 File Offset: 0x000368C7
		public long BytesRead { get; private set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000386D0 File Offset: 0x000368D0
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x000386D8 File Offset: 0x000368D8
		public long BytesWritten { get; private set; }

		// Token: 0x06000D69 RID: 3433 RVA: 0x000386E4 File Offset: 0x000368E4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.reading.Start();
			int result;
			try
			{
				int num = base.Read(buffer, offset, count);
				this.BytesRead += (long)num;
				result = num;
			}
			finally
			{
				this.reading.Stop();
			}
			return result;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00038738 File Offset: 0x00036938
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.writing.Start();
			try
			{
				base.Write(buffer, offset, count);
				this.BytesWritten += (long)count;
			}
			finally
			{
				this.writing.Stop();
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00038788 File Offset: 0x00036988
		public override void Close()
		{
			base.Close();
			if (IOCostStream.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				IOCostStream.Tracer.TraceDebug((long)this.GetHashCode(), "IOCostStream: time spent writing: {0}ms, time spent reading: {1}ms. Bytes read: {2} bytes, bytes written: {3} bytes. File: {4}", new object[]
				{
					this.writing.Elapsed.TotalMilliseconds,
					this.reading.Elapsed.TotalMilliseconds,
					this.BytesRead,
					this.BytesWritten,
					IOCostStream.GetBaseStreamFileName(base.InnerStream)
				});
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00038828 File Offset: 0x00036A28
		private static string GetBaseStreamFileName(Stream stream)
		{
			FileStream fileStream;
			for (;;)
			{
				fileStream = (stream as FileStream);
				if (fileStream != null)
				{
					break;
				}
				BaseStream baseStream = stream as BaseStream;
				if (baseStream == null)
				{
					goto IL_25;
				}
				stream = baseStream.InnerStream;
			}
			return fileStream.Name;
			IL_25:
			return "unknown";
		}

		// Token: 0x0400071F RID: 1823
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.DataTracer;

		// Token: 0x04000720 RID: 1824
		private Stopwatch writing;

		// Token: 0x04000721 RID: 1825
		private Stopwatch reading;
	}
}
