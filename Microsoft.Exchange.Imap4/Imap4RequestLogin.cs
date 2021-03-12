using System;
using System.Security;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PopImap.Core;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200002C RID: 44
	internal sealed class Imap4RequestLogin : Imap4RequestWithStringParameters, IProxyLogin
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public Imap4RequestLogin(Imap4ResponseFactory factory, string tag, byte[] buf, int offset, int size) : base(factory, tag, null, 2, 2)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_LOGIN_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_LOGIN_Failures;
			base.AllowedStates = (Imap4State.Nonauthenticated | Imap4State.AuthenticatedButFailed);
			this.AppendArray(buf, offset, size);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000BF01 File Offset: 0x0000A101
		public override bool NeedsStoreConnection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000BF04 File Offset: 0x0000A104
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000BF0C File Offset: 0x0000A10C
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000BF15 File Offset: 0x0000A115
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000BF1D File Offset: 0x0000A11D
		public SecureString Password
		{
			get
			{
				return this.securePassword;
			}
			set
			{
				throw new InvalidOperationException("Password is not settable in Imap4RequestLogin command!");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000BF29 File Offset: 0x0000A129
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000BF31 File Offset: 0x0000A131
		public string ClientIp { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000BF3A File Offset: 0x0000A13A
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000BF42 File Offset: 0x0000A142
		public string ClientPort { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000BF4B File Offset: 0x0000A14B
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000BF53 File Offset: 0x0000A153
		public string AuthenticationType { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000BF5C File Offset: 0x0000A15C
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000BF64 File Offset: 0x0000A164
		public string AuthenticationError { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000BF6D File Offset: 0x0000A16D
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000BF75 File Offset: 0x0000A175
		public string ProxyDestination { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000BF7E File Offset: 0x0000A17E
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000BF86 File Offset: 0x0000A186
		public ILiveIdBasicAuthentication LiveIdBasicAuth { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000BF8F File Offset: 0x0000A18F
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000BF97 File Offset: 0x0000A197
		public ADUser AdUser { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		protected override int StoredDataLength
		{
			get
			{
				if (this.byteArgs == null)
				{
					return 0;
				}
				return this.byteArgs.Length;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public override void ParseArguments()
		{
			try
			{
				if (this.byteArgs.Length == 0)
				{
					this.ParseResult = ParseResult.invalidNumberOfArguments;
				}
				else
				{
					int num = 0;
					bool literally = false;
					bool sendSyncResponse;
					for (;;)
					{
						int num2;
						if (base.NextLiteralSize != 0)
						{
							num2 = 0;
							num = base.NextLiteralSize;
							base.NextLiteralSize = 0;
							literally = true;
							if (num < this.byteArgs.Length && this.byteArgs[num] != 32)
							{
								break;
							}
						}
						else
						{
							num2 = Imap4Session.GetNextToken(this.byteArgs, num, this.byteArgs.Length - num, out num);
						}
						if (num == this.byteArgs.Length && num2 < num - 2 && this.byteArgs[num2] == 123 && this.byteArgs[num - 1] == 125)
						{
							string @string = Encoding.ASCII.GetString(this.byteArgs, num2, num - num2);
							int nextLiteralSize;
							int num3;
							if (!Imap4RequestWithStringParameters.TryParseLiteral(@string, out nextLiteralSize, out sendSyncResponse, out num3))
							{
								goto Block_10;
							}
							base.SendSyncResponse = sendSyncResponse;
							base.NextLiteralSize = nextLiteralSize;
							if (!base.IsValidLiteral())
							{
								goto Block_11;
							}
							if (base.NextLiteralSize != 0)
							{
								goto Block_12;
							}
						}
						else
						{
							this.IsComplete = true;
						}
						switch (base.ArrayOfArguments.Count)
						{
						case 0:
							this.userName = Imap4RequestLogin.GetString(this.byteArgs, num2, num - num2, literally);
							base.ArrayOfArguments.Add(this.userName);
							if (base.Session.LightLogSession != null)
							{
								base.Session.LightLogSession.Parameters = this.userName;
							}
							break;
						case 1:
						{
							int passwordCodePage = base.Session.Server.PasswordCodePage;
							Decoder passwordDecoder = base.GetPasswordDecoder(passwordCodePage);
							this.securePassword = Imap4RequestLogin.GetSecureString(this.byteArgs, num2, num - num2, literally, passwordDecoder);
							base.ArrayOfArguments.Add("*****");
							this.ParseResult = ParseResult.success;
							if (base.Session.LightLogSession != null)
							{
								LightWeightLogSession lightLogSession = base.Session.LightLogSession;
								lightLogSession.Parameters += " *****";
							}
							break;
						}
						default:
							this.ParseResult = ParseResult.invalidNumberOfArguments;
							if (base.Session.LightLogSession != null)
							{
								LightWeightLogSession lightLogSession2 = base.Session.LightLogSession;
								lightLogSession2.Parameters = lightLogSession2.Parameters + " " + base.ArrayOfArguments.Count.ToString();
							}
							break;
						}
						if (this.ParseResult != ParseResult.notYetParsed || num >= this.byteArgs.Length)
						{
							goto IL_27A;
						}
					}
					this.ParseResult = ParseResult.invalidArgument;
					this.IsComplete = true;
					return;
					Block_10:
					base.SendSyncResponse = sendSyncResponse;
					this.ParseResult = ParseResult.invalidArgument;
					this.IsComplete = true;
					return;
					Block_11:
					this.ParseResult = ParseResult.invalidArgument;
					this.IsComplete = true;
					return;
					Block_12:
					this.IsComplete = false;
					return;
					IL_27A:
					if (base.ArrayOfArguments.Count != 2 || num < this.byteArgs.Length)
					{
						this.ParseResult = ParseResult.invalidNumberOfArguments;
					}
				}
			}
			finally
			{
				this.byteArgs = null;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C284 File Offset: 0x0000A484
		public override ProtocolResponse Process()
		{
			base.Factory.UserName = this.userName;
			return base.Factory.DoConnect(this, this.securePassword);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
		public override bool VerifyState()
		{
			return base.VerifyState() && (base.Factory.Session.IsTls || base.Session.Server.LoginType < LoginOptions.PlainTextAuthentication);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		internal static SecureString GetSecureString(byte[] buf, int offset, int size, bool literally, Decoder passwordDecoder)
		{
			SecureString secureString = new SecureString();
			if (size > 0)
			{
				if (passwordDecoder != null)
				{
					char[] array = new char[passwordDecoder.GetCharCount(buf, offset, size)];
					passwordDecoder.GetChars(buf, offset, size, array, 0);
					int num = array.Length;
					for (int i = 0; i < num; i++)
					{
						if (literally || !Imap4RequestLogin.SkipTheChar(array, num, i))
						{
							secureString.AppendChar(array[i]);
						}
					}
				}
				else
				{
					for (int j = 0; j < size; j++)
					{
						if (literally || !Imap4RequestLogin.SkipTheChar(buf, offset, size, j))
						{
							secureString.AppendChar((char)buf[offset + j]);
						}
					}
				}
				Array.Clear(buf, offset, size);
			}
			return secureString;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C370 File Offset: 0x0000A570
		internal static string GetString(byte[] buf, int offset, int size, bool literally)
		{
			StringBuilder stringBuilder = new StringBuilder(size);
			for (int i = 0; i < size; i++)
			{
				if (literally || !Imap4RequestLogin.SkipTheChar(buf, offset, size, i))
				{
					stringBuilder.Append((char)buf[offset + i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		internal static bool SkipTheChar(byte[] buf, int offset, int size, int i)
		{
			return ((i == 0 || i == size - 1) && buf[offset] == 34 && buf[offset + size - 1] == 34) || ((i < size - 2 || (i == size - 2 && buf[offset + size - 1] != 34)) && buf[offset + i] == 92 && (buf[offset + i + 1] == 92 || buf[offset + i + 1] == 34));
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C414 File Offset: 0x0000A614
		protected override void AppendArray(byte[] data, int offset, int dataLength)
		{
			if (this.byteArgs != null)
			{
				byte[] destinationArray = new byte[this.byteArgs.Length + dataLength];
				Array.Copy(this.byteArgs, 0, destinationArray, 0, this.byteArgs.Length);
				Array.Copy(data, offset, destinationArray, this.byteArgs.Length, dataLength);
				Array.Clear(this.byteArgs, 0, this.byteArgs.Length);
				this.byteArgs = destinationArray;
				return;
			}
			this.byteArgs = new byte[dataLength];
			Array.Copy(data, offset, this.byteArgs, 0, dataLength);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C498 File Offset: 0x0000A698
		internal static bool SkipTheChar(char[] chars, int size, int i)
		{
			return ((i == 0 || i == size - 1) && chars[0] == '"' && chars[size - 1] == '"') || ((i < size - 2 || (i == size - 2 && chars[size - 1] != '"')) && chars[i] == '\\' && (chars[i + 1] == '\\' || chars[i + 1] == '"'));
		}

		// Token: 0x04000154 RID: 340
		internal const string LOGINResponseCompleted = "LOGIN completed.";

		// Token: 0x04000155 RID: 341
		internal const string LOGINResponseFailed = "LOGIN failed.";

		// Token: 0x04000156 RID: 342
		internal const string LOGINResponseFailedLastTime = "LOGIN failed.\r\n* BYE Connection is closed. 14";

		// Token: 0x04000157 RID: 343
		private byte[] byteArgs;

		// Token: 0x04000158 RID: 344
		private string userName;

		// Token: 0x04000159 RID: 345
		private SecureString securePassword;
	}
}
