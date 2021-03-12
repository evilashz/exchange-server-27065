using System;
using System.Globalization;
using System.Security;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Worker.Framework;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001F2 RID: 498
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Pop3Command
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x00035FBD File Offset: 0x000341BD
		private Pop3Command(Pop3CommandType commandType, bool listings, string formatString, params object[] formatStringArguments) : this(commandType, listings, string.Format(CultureInfo.InvariantCulture, formatString, formatStringArguments))
		{
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00035FD4 File Offset: 0x000341D4
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

		// Token: 0x06001087 RID: 4231 RVA: 0x00036034 File Offset: 0x00034234
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

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000360B4 File Offset: 0x000342B4
		internal Pop3CommandType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000360BC File Offset: 0x000342BC
		internal byte[] Buffer
		{
			get
			{
				return this.commandBytes;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x000360C4 File Offset: 0x000342C4
		internal bool Listings
		{
			get
			{
				return this.listings;
			}
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000360CC File Offset: 0x000342CC
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

		// Token: 0x0600108C RID: 4236 RVA: 0x0003610C File Offset: 0x0003430C
		internal static Pop3Command Blob(byte[] blob)
		{
			return new Pop3Command(Pop3CommandType.Blob, false, "{0}\r\n", new object[]
			{
				Encoding.ASCII.GetString(blob)
			});
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0003613C File Offset: 0x0003433C
		internal static Pop3Command List()
		{
			return Pop3Command.list;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00036143 File Offset: 0x00034343
		internal static Pop3Command Uidl()
		{
			return Pop3Command.uidl;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0003614C File Offset: 0x0003434C
		internal static Pop3Command Retr(int messageNumber)
		{
			return new Pop3Command(Pop3CommandType.Retr, true, "retr {0}\r\n", new object[]
			{
				messageNumber
			});
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00036178 File Offset: 0x00034378
		internal static Pop3Command Top(int messageNumber, int lineCount)
		{
			return new Pop3Command(Pop3CommandType.Top, true, "top {0} {1}\r\n", new object[]
			{
				messageNumber,
				lineCount
			});
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000361AC File Offset: 0x000343AC
		internal static Pop3Command Dele(int messageNumber)
		{
			return new Pop3Command(Pop3CommandType.Dele, false, "dele {0}\r\n", new object[]
			{
				messageNumber
			});
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000361D8 File Offset: 0x000343D8
		internal static Pop3Command User(string userName)
		{
			return new Pop3Command(Pop3CommandType.User, false, "user {0}\r\n", new object[]
			{
				userName
			});
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000361FE File Offset: 0x000343FE
		internal static Pop3Command Pass(SecureString password)
		{
			return new Pop3Command(password);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00036208 File Offset: 0x00034408
		internal static Pop3Command Auth(string mechanism)
		{
			return new Pop3Command(Pop3CommandType.Auth, false, "auth {0}\r\n", new object[]
			{
				mechanism
			});
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0003622E File Offset: 0x0003442E
		internal void ClearBuffer()
		{
			if (this.type == Pop3CommandType.Pass)
			{
				Array.Clear(this.commandBytes, 5, this.commandBytes.Length - 7);
			}
		}

		// Token: 0x04000946 RID: 2374
		private const string PassString = "PASS *****";

		// Token: 0x04000947 RID: 2375
		private const string BlobString = "<BLOB>";

		// Token: 0x04000948 RID: 2376
		internal static readonly Pop3Command AuthNtlm = new Pop3Command(Pop3CommandType.Auth, false, "auth ntlm\r\n");

		// Token: 0x04000949 RID: 2377
		internal static readonly Pop3Command Capa = new Pop3Command(Pop3CommandType.Capa, true, "capa\r\n");

		// Token: 0x0400094A RID: 2378
		internal static readonly Pop3Command Stls = new Pop3Command(Pop3CommandType.Stls, false, "stls\r\n");

		// Token: 0x0400094B RID: 2379
		internal static readonly Pop3Command Stat = new Pop3Command(Pop3CommandType.Stat, false, "stat\r\n");

		// Token: 0x0400094C RID: 2380
		internal static readonly Pop3Command Quit = new Pop3Command(Pop3CommandType.Quit, false, "quit\r\n");

		// Token: 0x0400094D RID: 2381
		private static readonly Pop3Command list = new Pop3Command(Pop3CommandType.List, true, "list\r\n");

		// Token: 0x0400094E RID: 2382
		private static readonly Pop3Command uidl = new Pop3Command(Pop3CommandType.Uidl, true, "uidl\r\n");

		// Token: 0x0400094F RID: 2383
		private static readonly byte[] PassBytes = Encoding.ASCII.GetBytes("pass ");

		// Token: 0x04000950 RID: 2384
		private static readonly byte[] CrLfBytes = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x04000951 RID: 2385
		private readonly Pop3CommandType type;

		// Token: 0x04000952 RID: 2386
		private readonly byte[] commandBytes;

		// Token: 0x04000953 RID: 2387
		private readonly bool listings;
	}
}
