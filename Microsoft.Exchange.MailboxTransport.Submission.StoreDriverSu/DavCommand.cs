using System;
using System.Text;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000037 RID: 55
	internal abstract class DavCommand
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		protected DavCommand(byte[] commandBytes)
		{
			this.commandBytes = commandBytes;
			this.CheckExpectedToken(this.FirstToken);
			this.CheckExpectedToken(this.SecondToken);
			this.CheckExpectedToken(DavCommand.Colon);
			this.GetAddress();
			this.ParseArguments();
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000CB22 File Offset: 0x0000AD22
		public int CurrentOffset
		{
			get
			{
				return this.currentOffset;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000CB2A File Offset: 0x0000AD2A
		public byte[] CommandBytes
		{
			get
			{
				return this.commandBytes;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000CB32 File Offset: 0x0000AD32
		public RoutingAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000225 RID: 549
		protected abstract byte[] FirstToken { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000226 RID: 550
		protected abstract byte[] SecondToken { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000CB3A File Offset: 0x0000AD3A
		private int RemainingBufferLength
		{
			get
			{
				return this.commandBytes.Length - this.currentOffset;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000CB4B File Offset: 0x0000AD4B
		private bool EndOfCommand
		{
			get
			{
				return this.currentOffset == this.commandBytes.Length;
			}
		}

		// Token: 0x06000229 RID: 553
		protected abstract void ParseArguments();

		// Token: 0x0600022A RID: 554 RVA: 0x0000CB5D File Offset: 0x0000AD5D
		private static bool IsWhiteSpace(byte ch)
		{
			return ch == 9 || ch == 32;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		private void CheckExpectedToken(byte[] token)
		{
			this.TrimStart();
			if (token.Length >= this.RemainingBufferLength)
			{
				throw new FormatException("Expected token is missing");
			}
			if (!SmtpCommand.CompareArg(token, this.commandBytes, this.currentOffset, token.Length))
			{
				throw new FormatException("Expected token is missing");
			}
			this.currentOffset += token.Length;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000CBC7 File Offset: 0x0000ADC7
		private void TrimStart()
		{
			if (this.EndOfCommand)
			{
				return;
			}
			while (this.currentOffset < this.commandBytes.Length && DavCommand.IsWhiteSpace(this.commandBytes[this.currentOffset]))
			{
				this.currentOffset++;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000CC04 File Offset: 0x0000AE04
		private void GetAddress()
		{
			this.TrimStart();
			if (this.EndOfCommand)
			{
				throw new FormatException("No Address");
			}
			string @string = Encoding.ASCII.GetString(this.commandBytes, this.currentOffset, this.commandBytes.Length - this.currentOffset);
			string text = null;
			this.address = Parse821.ParseAddressLine(@string, out text);
			if (!this.address.IsValid)
			{
				throw new FormatException("Invalid Address");
			}
			this.currentOffset = this.commandBytes.Length - ((text != null) ? text.Length : 0);
		}

		// Token: 0x04000127 RID: 295
		private static readonly byte[] Colon = Util.AsciiStringToBytes(":");

		// Token: 0x04000128 RID: 296
		private RoutingAddress address;

		// Token: 0x04000129 RID: 297
		private int currentOffset;

		// Token: 0x0400012A RID: 298
		private byte[] commandBytes;
	}
}
