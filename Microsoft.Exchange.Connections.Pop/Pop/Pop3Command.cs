using System;
using System.Globalization;
using System.Security;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Pop3Command
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00005256 File Offset: 0x00003456
		private Pop3Command(Pop3CommandType commandType, bool listings, string formatString, params object[] formatStringArguments) : this(commandType, listings, string.Format(CultureInfo.InvariantCulture, formatString, formatStringArguments))
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005270 File Offset: 0x00003470
		private Pop3Command(Pop3CommandType commandType, bool listings, string commandText)
		{
			this.type = commandType;
			BufferBuilder bufferBuilder = new BufferBuilder(commandText.Length);
			try
			{
				bufferBuilder.Append(commandText);
			}
			catch (ArgumentException innerException)
			{
				throw new Pop3InvalidCommandException("All characters in the string must be in the range 0x00 - 0xff.", innerException);
			}
			this.commandBytes = bufferBuilder.GetBuffer();
			this.listings = listings;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000052D0 File Offset: 0x000034D0
		private Pop3Command(SecureString password)
		{
			this.type = Pop3CommandType.Pass;
			BufferBuilder bufferBuilder = new BufferBuilder(Pop3Command.PassBytes.Length + password.Length + Pop3Command.CrLfBytes.Length);
			bufferBuilder.Append(Pop3Command.PassBytes);
			try
			{
				bufferBuilder.Append(password);
			}
			catch (ArgumentException innerException)
			{
				throw new Pop3InvalidCommandException("All characters in the string must be in the range 0x00 - 0xff.", innerException);
			}
			bufferBuilder.Append(Pop3Command.CrLfBytes);
			this.commandBytes = bufferBuilder.GetBuffer();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00005350 File Offset: 0x00003550
		internal Pop3CommandType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00005358 File Offset: 0x00003558
		internal byte[] Buffer
		{
			get
			{
				return this.commandBytes;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00005360 File Offset: 0x00003560
		internal bool Listings
		{
			get
			{
				return this.listings;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005368 File Offset: 0x00003568
		public override string ToString()
		{
			if (this.type == Pop3CommandType.Pass)
			{
				return "PASS *****";
			}
			if (this.type == Pop3CommandType.Blob)
			{
				return "<BLOB>";
			}
			return Encoding.ASCII.GetString(this.commandBytes).TrimEnd(new char[0]);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000053A8 File Offset: 0x000035A8
		internal static Pop3Command Blob(byte[] blob)
		{
			return new Pop3Command(Pop3CommandType.Blob, false, "{0}\r\n", new object[]
			{
				Encoding.ASCII.GetString(blob)
			});
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000053D8 File Offset: 0x000035D8
		internal static Pop3Command List()
		{
			return Pop3Command.list;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000053DF File Offset: 0x000035DF
		internal static Pop3Command Uidl()
		{
			return Pop3Command.uidl;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000053E8 File Offset: 0x000035E8
		internal static Pop3Command Retr(int messageNumber)
		{
			return new Pop3Command(Pop3CommandType.Retr, true, "retr {0}\r\n", new object[]
			{
				messageNumber
			});
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005414 File Offset: 0x00003614
		internal static Pop3Command Top(int messageNumber, int lineCount)
		{
			return new Pop3Command(Pop3CommandType.Top, true, "top {0} {1}\r\n", new object[]
			{
				messageNumber,
				lineCount
			});
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005448 File Offset: 0x00003648
		internal static Pop3Command Dele(int messageNumber)
		{
			return new Pop3Command(Pop3CommandType.Dele, false, "dele {0}\r\n", new object[]
			{
				messageNumber
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005474 File Offset: 0x00003674
		internal static Pop3Command User(string userName)
		{
			return new Pop3Command(Pop3CommandType.User, false, "user {0}\r\n", new object[]
			{
				userName
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000549A File Offset: 0x0000369A
		internal static Pop3Command Pass(SecureString password)
		{
			return new Pop3Command(password);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000054A4 File Offset: 0x000036A4
		internal static Pop3Command Auth(string mechanism)
		{
			return new Pop3Command(Pop3CommandType.Auth, false, "auth {0}\r\n", new object[]
			{
				mechanism
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000054CA File Offset: 0x000036CA
		internal void ClearBuffer()
		{
			if (this.type == Pop3CommandType.Pass)
			{
				Array.Clear(this.commandBytes, 5, this.commandBytes.Length - 7);
			}
		}

		// Token: 0x04000073 RID: 115
		private const string PassString = "PASS *****";

		// Token: 0x04000074 RID: 116
		private const string BlobString = "<BLOB>";

		// Token: 0x04000075 RID: 117
		internal static readonly Pop3Command AuthNtlm = new Pop3Command(Pop3CommandType.Auth, false, "auth ntlm\r\n");

		// Token: 0x04000076 RID: 118
		internal static readonly Pop3Command Capa = new Pop3Command(Pop3CommandType.Capa, true, "capa\r\n");

		// Token: 0x04000077 RID: 119
		internal static readonly Pop3Command Stls = new Pop3Command(Pop3CommandType.Stls, false, "stls\r\n");

		// Token: 0x04000078 RID: 120
		internal static readonly Pop3Command Stat = new Pop3Command(Pop3CommandType.Stat, false, "stat\r\n");

		// Token: 0x04000079 RID: 121
		internal static readonly Pop3Command Quit = new Pop3Command(Pop3CommandType.Quit, false, "quit\r\n");

		// Token: 0x0400007A RID: 122
		private static readonly Pop3Command list = new Pop3Command(Pop3CommandType.List, true, "list\r\n");

		// Token: 0x0400007B RID: 123
		private static readonly Pop3Command uidl = new Pop3Command(Pop3CommandType.Uidl, true, "uidl\r\n");

		// Token: 0x0400007C RID: 124
		private static readonly byte[] PassBytes = Encoding.ASCII.GetBytes("pass ");

		// Token: 0x0400007D RID: 125
		private static readonly byte[] CrLfBytes = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x0400007E RID: 126
		private readonly Pop3CommandType type;

		// Token: 0x0400007F RID: 127
		private readonly byte[] commandBytes;

		// Token: 0x04000080 RID: 128
		private readonly bool listings;
	}
}
