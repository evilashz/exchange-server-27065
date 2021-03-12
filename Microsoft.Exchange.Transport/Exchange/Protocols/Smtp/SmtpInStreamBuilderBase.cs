using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F4 RID: 1268
	internal abstract class SmtpInStreamBuilderBase : ISmtpInStreamBuilder
	{
		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000F34BC File Offset: 0x000F16BC
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x000F34C4 File Offset: 0x000F16C4
		public bool IsDiscardingData { get; set; }

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x000F34CD File Offset: 0x000F16CD
		// (set) Token: 0x06003A82 RID: 14978 RVA: 0x000F34D5 File Offset: 0x000F16D5
		public Stream BodyStream { get; set; }

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06003A83 RID: 14979 RVA: 0x000F34DE File Offset: 0x000F16DE
		// (set) Token: 0x06003A84 RID: 14980 RVA: 0x000F34E6 File Offset: 0x000F16E6
		public long TotalBytesRead { get; protected set; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06003A85 RID: 14981 RVA: 0x000F34EF File Offset: 0x000F16EF
		// (set) Token: 0x06003A86 RID: 14982 RVA: 0x000F34F7 File Offset: 0x000F16F7
		public long TotalBytesWritten { get; protected set; }

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000F3500 File Offset: 0x000F1700
		// (set) Token: 0x06003A88 RID: 14984 RVA: 0x000F3508 File Offset: 0x000F1708
		public long EohPos { get; protected set; }

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06003A89 RID: 14985
		public abstract bool IsEodSeen { get; }

		// Token: 0x06003A8A RID: 14986 RVA: 0x000F3511 File Offset: 0x000F1711
		public virtual void Reset()
		{
			this.IsDiscardingData = false;
			this.TotalBytesRead = 0L;
			this.TotalBytesWritten = 0L;
			this.EohPos = -1L;
		}

		// Token: 0x06003A8B RID: 14987
		public abstract bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed);

		// Token: 0x06003A8C RID: 14988 RVA: 0x000F3532 File Offset: 0x000F1732
		public bool Write(CommandContext commandContext, out int numBytesConsumed)
		{
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			return this.Write(commandContext.Command, commandContext.Offset, commandContext.Length, out numBytesConsumed);
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x000F3558 File Offset: 0x000F1758
		protected void Write(byte[] data, int offset, int count)
		{
			try
			{
				this.BodyStream.Write(data, offset, count);
				this.TotalBytesWritten += (long)count;
			}
			catch (Exception)
			{
				this.IsDiscardingData = true;
				throw;
			}
		}

		// Token: 0x04001D74 RID: 7540
		public const byte CR = 13;

		// Token: 0x04001D75 RID: 7541
		public const byte LF = 10;

		// Token: 0x04001D76 RID: 7542
		public const byte DOT = 46;

		// Token: 0x04001D77 RID: 7543
		public static readonly byte[] EodSequence = new byte[]
		{
			13,
			10,
			46,
			13,
			10
		};
	}
}
