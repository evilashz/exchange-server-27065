using System;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E2 RID: 1250
	internal class CommandContext
	{
		// Token: 0x060039D0 RID: 14800 RVA: 0x000EEDC9 File Offset: 0x000ECFC9
		public static CommandContext FromAsyncResult(NetworkConnection.LazyAsyncResultWithTimeout asyncResult)
		{
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			return new CommandContext(asyncResult.Buffer, asyncResult.Size, asyncResult.Offset);
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000EEDF0 File Offset: 0x000ECFF0
		public static CommandContext FromSmtpCommand(ILegacySmtpCommand command)
		{
			ArgumentValidator.ThrowIfNull("command", command);
			CommandContext commandContext = new CommandContext(command.ProtocolCommand, command.ProtocolCommandLength, 0);
			string text;
			commandContext.GetNextArgument(out text);
			return commandContext;
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000EEE25 File Offset: 0x000ED025
		public static CommandContext FromByteArrayLegacyCodeOnly(byte[] bytes, int offset)
		{
			ArgumentValidator.ThrowIfNull("bytes", bytes);
			ArgumentValidator.ThrowIfOutOfRange<int>("offset", offset, 0, bytes.Length);
			return new CommandContext(bytes, bytes.Length - offset, offset);
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000EEE4D File Offset: 0x000ED04D
		// (set) Token: 0x060039D4 RID: 14804 RVA: 0x000EEE55 File Offset: 0x000ED055
		public byte[] Command { get; private set; }

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000EEE5E File Offset: 0x000ED05E
		public int OriginalLength
		{
			get
			{
				return this.originalLength;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x060039D6 RID: 14806 RVA: 0x000EEE66 File Offset: 0x000ED066
		// (set) Token: 0x060039D7 RID: 14807 RVA: 0x000EEE6E File Offset: 0x000ED06E
		public int Length
		{
			get
			{
				return this.length;
			}
			private set
			{
				this.length = Math.Min(this.originalLength, value);
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000EEE82 File Offset: 0x000ED082
		public int OriginalOffset
		{
			get
			{
				return this.originalOffset;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000EEE8A File Offset: 0x000ED08A
		// (set) Token: 0x060039DA RID: 14810 RVA: 0x000EEE94 File Offset: 0x000ED094
		public int Offset
		{
			get
			{
				return this.offset;
			}
			private set
			{
				ArgumentValidator.ThrowIfOutOfRange<int>("value", value, this.originalOffset, this.originalOffset + this.originalLength + 1);
				this.Length = Math.Abs(value - (this.offset + this.length));
				this.offset = value;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x060039DB RID: 14811 RVA: 0x000EEEE4 File Offset: 0x000ED0E4
		public bool HasArguments
		{
			get
			{
				Offset offset;
				return this.GetNextArgumentOffset(out offset, false);
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000EEEFA File Offset: 0x000ED0FA
		public bool IsEndOfCommand
		{
			get
			{
				return this.length == 0;
			}
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000EEF08 File Offset: 0x000ED108
		public override string ToString()
		{
			return string.Format("Command.Length: {0}, offset: {1}, originalOffset: {2}, Length: {3}, originalLength: {4}, Command: '{5}'", new object[]
			{
				this.Command.Length,
				this.offset,
				this.originalOffset,
				this.Length,
				this.originalLength,
				ByteString.BytesToString(this.Command, this.originalOffset, this.originalLength, true)
			});
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000EEF8C File Offset: 0x000ED18C
		public SmtpInCommand IdentifySmtpCommand()
		{
			int num;
			SmtpInCommand result = SmtpInSessionUtils.IdentifySmtpCommand(this.Command, this.Offset, this.Length, out num);
			this.Offset = num;
			return result;
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000EEFBC File Offset: 0x000ED1BC
		public void LogReceivedCommand(IProtocolLogSession protocolLogSession)
		{
			ArgumentValidator.ThrowIfNull("protocolLogSession", protocolLogSession);
			if (this.originalLength != 0)
			{
				byte[] array = new byte[this.originalLength];
				Buffer.BlockCopy(this.Command, this.originalOffset, array, 0, this.originalLength);
				protocolLogSession.LogReceive(array);
			}
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000EF008 File Offset: 0x000ED208
		public void TrimLeadingWhitespace()
		{
			if (this.IsEndOfCommand)
			{
				return;
			}
			int i;
			for (i = this.Offset; i < this.Offset + this.Length; i++)
			{
				byte b = this.Command[i];
				bool flag = 9 == b || 32 == b;
				if (!flag)
				{
					break;
				}
			}
			this.Offset = i;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000EF05C File Offset: 0x000ED25C
		public bool GetNextArgumentOffset(out Offset nextTokenOffset)
		{
			return this.GetNextArgumentOffset(out nextTokenOffset, true);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000EF068 File Offset: 0x000ED268
		public bool GetNextArgument(out string argument)
		{
			argument = string.Empty;
			if (!this.HasArguments)
			{
				return false;
			}
			Offset offset;
			this.GetNextArgumentOffset(out offset);
			argument = ByteString.BytesToString(this.Command, offset.Start, offset.Length, true);
			this.Offset = offset.End;
			return true;
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000EF0B6 File Offset: 0x000ED2B6
		public void PushBackOffset(int howManyChars)
		{
			ArgumentValidator.ThrowIfOutOfRange<int>("howManyChars", howManyChars, 0, this.Offset - this.originalOffset);
			this.Offset -= howManyChars;
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000EF0E0 File Offset: 0x000ED2E0
		public bool GetCommandArguments(out string args)
		{
			if (this.IsEndOfCommand)
			{
				args = string.Empty;
				return false;
			}
			args = ByteString.BytesToString(this.Command, this.Offset, this.Length, true);
			this.Offset += this.Length;
			return true;
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000EF12C File Offset: 0x000ED32C
		public bool ParseTokenAndVerifyCommand(byte[] cmdPart, byte delimToken)
		{
			ArgumentValidator.ThrowIfNull("cmdPart", cmdPart);
			this.TrimLeadingWhitespace();
			if (this.IsEndOfCommand)
			{
				return false;
			}
			int num = cmdPart.Length;
			if (num > this.length)
			{
				return false;
			}
			if (!BufferParser.CompareArg(cmdPart, this.Command, this.Offset, num))
			{
				this.Offset += num;
				return false;
			}
			this.Offset += num;
			bool flag = false;
			int i;
			for (i = this.Offset; i < this.Offset + this.length; i++)
			{
				byte b = this.Command[i];
				if (9 != b && 32 != b)
				{
					if (delimToken != b || flag)
					{
						break;
					}
					flag = true;
				}
			}
			this.Offset = i;
			return flag;
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000EF1DC File Offset: 0x000ED3DC
		private CommandContext(byte[] command, int length, int offset)
		{
			ArgumentValidator.ThrowIfNull("command", command);
			ArgumentValidator.ThrowIfOutOfRange<int>("length", length, 0, command.Length - offset);
			ArgumentValidator.ThrowIfOutOfRange<int>("offset", offset, 0, command.Length);
			this.originalOffset = offset;
			this.originalLength = length;
			this.Command = command;
			this.length = length;
			this.offset = offset;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000EF240 File Offset: 0x000ED440
		private bool GetNextArgumentOffset(out Offset nextTokenOffset, bool updateOffset)
		{
			if (this.IsEndOfCommand)
			{
				nextTokenOffset = new Offset(0, 0);
				return false;
			}
			int end;
			int nextToken = BufferParser.GetNextToken(this.Command, this.Offset, this.Length, out end);
			if (nextToken >= this.Offset + this.Length)
			{
				nextTokenOffset = new Offset(0, 0);
				return false;
			}
			if (updateOffset)
			{
				this.Offset = end;
			}
			nextTokenOffset = new Offset(nextToken, end);
			return true;
		}

		// Token: 0x04001D4E RID: 7502
		private readonly int originalOffset;

		// Token: 0x04001D4F RID: 7503
		private readonly int originalLength;

		// Token: 0x04001D50 RID: 7504
		private int offset;

		// Token: 0x04001D51 RID: 7505
		private int length;
	}
}
