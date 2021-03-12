using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200040D RID: 1037
	internal abstract class SmtpInParser : ISmtpInStreamBuilder
	{
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000BE722 File Offset: 0x000BC922
		// (set) Token: 0x06002F9A RID: 12186 RVA: 0x000BE72A File Offset: 0x000BC92A
		public bool IsDiscardingData
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

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06002F9B RID: 12187 RVA: 0x000BE733 File Offset: 0x000BC933
		// (set) Token: 0x06002F9C RID: 12188 RVA: 0x000BE73B File Offset: 0x000BC93B
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
					throw new ArgumentException(Strings.DiscardingDataFalse, "BodyStream");
				}
				this.bodyStream = value;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000BE764 File Offset: 0x000BC964
		// (set) Token: 0x06002F9E RID: 12190 RVA: 0x000BE76C File Offset: 0x000BC96C
		public long TotalBytesRead
		{
			get
			{
				return this.totalBytesRead;
			}
			protected set
			{
				this.totalBytesRead = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06002F9F RID: 12191 RVA: 0x000BE775 File Offset: 0x000BC975
		// (set) Token: 0x06002FA0 RID: 12192 RVA: 0x000BE77D File Offset: 0x000BC97D
		public long TotalBytesWritten
		{
			get
			{
				return this.totalBytesWritten;
			}
			protected set
			{
				this.totalBytesWritten = value;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x000BE786 File Offset: 0x000BC986
		// (set) Token: 0x06002FA2 RID: 12194 RVA: 0x000BE78E File Offset: 0x000BC98E
		public long EohPos
		{
			get
			{
				return this.eohPos;
			}
			protected set
			{
				this.eohPos = value;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06002FA3 RID: 12195
		public abstract bool IsEodSeen { get; }

		// Token: 0x17000E89 RID: 3721
		// (set) Token: 0x06002FA4 RID: 12196 RVA: 0x000BE797 File Offset: 0x000BC997
		public ExceptionFilter ExceptionFilter
		{
			set
			{
				this.exceptionFilterDelegate = value;
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000BE7A0 File Offset: 0x000BC9A0
		public virtual void Reset()
		{
			this.discardingData = false;
			this.totalBytesRead = 0L;
			this.totalBytesWritten = 0L;
			this.eohPos = -1L;
			this.exceptionFilterDelegate = null;
		}

		// Token: 0x06002FA6 RID: 12198
		public abstract bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed);

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000BE7C8 File Offset: 0x000BC9C8
		public bool Write(CommandContext commandContext, out int numBytesConsumed)
		{
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			return this.Write(commandContext.Command, commandContext.Offset, commandContext.Length, out numBytesConsumed);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000BE7F0 File Offset: 0x000BC9F0
		protected void Write(byte[] data, int offset, int count)
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

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000BE85C File Offset: 0x000BCA5C
		internal void FilterException(Exception e)
		{
			if (this.exceptionFilterDelegate != null)
			{
				this.exceptionFilterDelegate(e);
			}
		}

		// Token: 0x04001770 RID: 6000
		public const byte CR = 13;

		// Token: 0x04001771 RID: 6001
		public const byte LF = 10;

		// Token: 0x04001772 RID: 6002
		public const byte DOT = 46;

		// Token: 0x04001773 RID: 6003
		public static readonly byte[] EodSequence = new byte[]
		{
			13,
			10,
			46,
			13,
			10
		};

		// Token: 0x04001774 RID: 6004
		private bool discardingData;

		// Token: 0x04001775 RID: 6005
		private long totalBytesRead;

		// Token: 0x04001776 RID: 6006
		private long totalBytesWritten;

		// Token: 0x04001777 RID: 6007
		private long eohPos = -1L;

		// Token: 0x04001778 RID: 6008
		private Stream bodyStream;

		// Token: 0x04001779 RID: 6009
		private ExceptionFilter exceptionFilterDelegate;
	}
}
