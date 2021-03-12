using System;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200001C RID: 28
	internal abstract class SmtpInParser
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00006277 File Offset: 0x00004477
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000627F File Offset: 0x0000447F
		public bool DiscardingData
		{
			get
			{
				return this.discardingData;
			}
			set
			{
				this.discardingData = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006288 File Offset: 0x00004488
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00006290 File Offset: 0x00004490
		public Stream BodyStream
		{
			get
			{
				return this.bodyStream;
			}
			set
			{
				if (value == null && !this.discardingData)
				{
					throw new ArgumentException("BodyStream");
				}
				this.bodyStream = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000062AF File Offset: 0x000044AF
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000062B7 File Offset: 0x000044B7
		public long TotalBytesRead
		{
			get
			{
				return this.totalBytesRead;
			}
			internal set
			{
				this.totalBytesRead = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000062C0 File Offset: 0x000044C0
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000062C8 File Offset: 0x000044C8
		public long TotalBytesWritten
		{
			get
			{
				return this.totalBytesWritten;
			}
			internal set
			{
				this.totalBytesWritten = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000062D1 File Offset: 0x000044D1
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000062D9 File Offset: 0x000044D9
		public long EohPos
		{
			get
			{
				return this.eohPos;
			}
			internal set
			{
				this.eohPos = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000062E2 File Offset: 0x000044E2
		public SmtpInParser.ExceptionFilterDelegate ExceptionFilter
		{
			set
			{
				this.exceptionFilterDelegate = value;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000062EB File Offset: 0x000044EB
		public virtual void Reset()
		{
			this.discardingData = false;
			this.totalBytesRead = 0L;
			this.totalBytesWritten = 0L;
			this.eohPos = -1L;
			this.exceptionFilterDelegate = null;
		}

		// Token: 0x06000102 RID: 258
		public abstract bool ParseAndWrite(byte[] data, int offset, int numBytes, out int numBytesConsumed);

		// Token: 0x06000103 RID: 259 RVA: 0x00006314 File Offset: 0x00004514
		internal void Write(byte[] data, int offset, int count)
		{
			try
			{
				this.bodyStream.Write(data, offset, count);
				this.totalBytesWritten += (long)count;
			}
			catch (IOException e)
			{
				this.FilterException(e);
				this.discardingData = true;
			}
			catch (ExchangeDataException e2)
			{
				this.FilterException(e2);
				this.discardingData = true;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006380 File Offset: 0x00004580
		internal void FilterException(Exception e)
		{
			if (this.exceptionFilterDelegate != null)
			{
				this.exceptionFilterDelegate(e);
			}
		}

		// Token: 0x040000B8 RID: 184
		public const byte CR = 13;

		// Token: 0x040000B9 RID: 185
		public const byte LF = 10;

		// Token: 0x040000BA RID: 186
		public const byte DOT = 46;

		// Token: 0x040000BB RID: 187
		public static readonly byte[] EodSequence = new byte[]
		{
			13,
			10,
			46,
			13,
			10
		};

		// Token: 0x040000BC RID: 188
		private bool discardingData;

		// Token: 0x040000BD RID: 189
		private long totalBytesRead;

		// Token: 0x040000BE RID: 190
		private long totalBytesWritten;

		// Token: 0x040000BF RID: 191
		private long eohPos = -1L;

		// Token: 0x040000C0 RID: 192
		private Stream bodyStream;

		// Token: 0x040000C1 RID: 193
		private SmtpInParser.ExceptionFilterDelegate exceptionFilterDelegate;

		// Token: 0x0200001D RID: 29
		// (Invoke) Token: 0x06000108 RID: 264
		public delegate void ExceptionFilterDelegate(Exception e);
	}
}
