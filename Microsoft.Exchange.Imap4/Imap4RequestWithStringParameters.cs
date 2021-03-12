using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200001D RID: 29
	internal abstract class Imap4RequestWithStringParameters : Imap4Request
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00009F0E File Offset: 0x0000810E
		public Imap4RequestWithStringParameters(Imap4ResponseFactory factory, string tag, string data, int minNumberOfArguments, int maxNumberOfArguments) : base(factory, tag, data)
		{
			this.minNumberOfArguments = minNumberOfArguments;
			this.maxNumberOfArguments = maxNumberOfArguments;
			this.IsComplete = false;
			this.arrayOfArguments = new List<string>();
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00009F3B File Offset: 0x0000813B
		public override bool NeedsStoreConnection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00009F3E File Offset: 0x0000813E
		protected List<string> ArrayOfArguments
		{
			get
			{
				return this.arrayOfArguments;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009F46 File Offset: 0x00008146
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00009F4E File Offset: 0x0000814E
		protected int NextLiteralSize
		{
			get
			{
				return this.nextLiteralSize;
			}
			set
			{
				this.nextLiteralSize = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00009F57 File Offset: 0x00008157
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00009F5F File Offset: 0x0000815F
		protected int MinNumberOfArguments
		{
			get
			{
				return this.minNumberOfArguments;
			}
			set
			{
				this.minNumberOfArguments = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00009F68 File Offset: 0x00008168
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00009F70 File Offset: 0x00008170
		protected int MaxNumberOfArguments
		{
			get
			{
				return this.maxNumberOfArguments;
			}
			set
			{
				this.maxNumberOfArguments = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00009F79 File Offset: 0x00008179
		protected virtual int StoredDataLength
		{
			get
			{
				return base.Arguments.Length;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009F88 File Offset: 0x00008188
		public static bool TryParseLiteral(string argument, out int nextLiteralSize)
		{
			bool flag;
			int num;
			return Imap4RequestWithStringParameters.TryParseLiteral(argument, out nextLiteralSize, out flag, out num);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009FA0 File Offset: 0x000081A0
		public override void ParseArguments()
		{
			if (this.IsComplete)
			{
				this.ParseResult = ParseResult.success;
				return;
			}
			ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.Session.SessionId, "ParseArguments >{0}<.", base.Arguments);
			List<string> list = new List<string>();
			if (this.nextLiteralSize > 0)
			{
				if (base.Arguments.Length < this.nextLiteralSize)
				{
					this.ParseResult = ParseResult.invalidArgument;
					this.IsComplete = true;
					return;
				}
				list.Add(base.Arguments.Substring(0, this.nextLiteralSize));
				base.Arguments = base.Arguments.Substring(this.nextLiteralSize);
				this.nextLiteralSize = 0;
				if (base.Arguments.EndsWith(Strings.CRLF, StringComparison.OrdinalIgnoreCase))
				{
					base.Arguments = base.Arguments.Substring(0, base.Arguments.Length - 2);
				}
				if (base.Arguments.Length > 0)
				{
					if (base.Arguments[0] != ' ')
					{
						this.ParseResult = ParseResult.invalidArgument;
						this.IsComplete = true;
						return;
					}
					base.Arguments = base.Arguments.Substring(1);
				}
			}
			else if (base.Arguments.EndsWith(Strings.CRLF, StringComparison.OrdinalIgnoreCase))
			{
				base.Arguments = base.Arguments.Substring(0, base.Arguments.Length - 2);
			}
			if (base.Arguments.Length > 0 && base.Arguments[base.Arguments.Length - 1] == '}')
			{
				bool sendSyncResponse;
				int length;
				if (Imap4RequestWithStringParameters.TryParseLiteral(base.Arguments, out this.nextLiteralSize, out sendSyncResponse, out length))
				{
					base.SendSyncResponse = sendSyncResponse;
					if (!this.IsValidLiteral())
					{
						this.ParseResult = ParseResult.invalidArgument;
						this.IsComplete = true;
						return;
					}
					base.Arguments = base.Arguments.Substring(0, length);
				}
				else
				{
					base.SendSyncResponse = sendSyncResponse;
					this.ParseResult = ParseResult.success;
					this.IsComplete = true;
				}
			}
			else
			{
				this.ParseResult = ParseResult.success;
				this.IsComplete = true;
			}
			if (base.Arguments.Length > 0 && base.Arguments.Trim().Length == 0)
			{
				this.ParseResult = ParseResult.invalidArgument;
				this.IsComplete = true;
				return;
			}
			if (base.Arguments.Length > 0)
			{
				if (!Imap4RequestWithStringParameters.TryParseArguments(ref list, base.Arguments))
				{
					this.ParseResult = ParseResult.invalidArgument;
					this.IsComplete = true;
					return;
				}
				base.Arguments = string.Empty;
			}
			if (list.Count > 0)
			{
				this.arrayOfArguments.AddRange(list);
			}
			if (this.IsComplete && (this.arrayOfArguments.Count < this.minNumberOfArguments || this.arrayOfArguments.Count > this.maxNumberOfArguments))
			{
				this.ParseResult = ParseResult.invalidNumberOfArguments;
				this.IsComplete = true;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000A23C File Offset: 0x0000843C
		public override void AddData(byte[] data, int offset, int dataLength, out int bytesConsumed)
		{
			ProtocolBaseServices.Assert(!this.IsComplete, "Imap4RequestWithStringParameters.AddData is only valid if the request is not complete.", new object[0]);
			bytesConsumed = 0;
			int num = offset;
			while (!this.IsComplete && bytesConsumed < dataLength)
			{
				int num2 = this.AddOneLine(data, num, dataLength - bytesConsumed, ref bytesConsumed);
				ProtocolBaseServices.Assert(num2 <= dataLength, "bytesProcessed {0} >= dataLength {1}", new object[]
				{
					num2,
					dataLength
				});
				num += num2;
				if (this.ParseResult > ParseResult.success)
				{
					return;
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000A2C4 File Offset: 0x000084C4
		protected static bool TryParseLiteral(string argument, out int nextLiteralSize, out bool sendSyncResponse, out int startsAt)
		{
			nextLiteralSize = 0;
			sendSyncResponse = false;
			startsAt = 0;
			Match match = Imap4RequestWithStringParameters.literalRegEx.Match(argument);
			if (!match.Success)
			{
				return false;
			}
			if (!int.TryParse(match.Groups["literalSize"].Value, out nextLiteralSize) || nextLiteralSize < 1)
			{
				return false;
			}
			sendSyncResponse = !(match.Groups["sendSyncResponse"].Value == "+");
			startsAt = match.Index;
			return true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000A344 File Offset: 0x00008544
		protected static bool TryParseArguments(ref List<string> argumentsArray, string argumentsString)
		{
			Match match = Imap4RequestWithStringParameters.ArgumentsRegEx.Match(argumentsString);
			if (!match.Success)
			{
				return false;
			}
			while (match.Success)
			{
				argumentsArray.Add(match.Groups["argument"].Value.Replace("\\\"", "\"").Replace("\\\\", "\\"));
				if (match.Groups["eol"].Value.Length != 0 && !match.NextMatch().Success)
				{
					return false;
				}
				match = match.NextMatch();
			}
			return true;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000A3DC File Offset: 0x000085DC
		protected bool IsValidLiteral()
		{
			int num;
			if ((base.Factory.SessionState & Imap4State.Nonauthenticated) == Imap4State.None)
			{
				num = base.Factory.MaxReceiveSize;
			}
			else
			{
				num = 65536;
			}
			if (this.nextLiteralSize > num)
			{
				ProtocolBaseServices.SessionTracer.TraceDebug<int, int>(base.Session.SessionId, "Literal {0} is larger than maxLiteralSize {1}", this.nextLiteralSize, num);
				this.ParseResult = ParseResult.invalidArgument;
				this.IsComplete = true;
				return false;
			}
			if (this.nextLiteralSize == 0)
			{
				this.ParseResult = ParseResult.success;
				this.IsComplete = true;
				return false;
			}
			return true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000A460 File Offset: 0x00008660
		protected virtual int AddOneLine(byte[] data, int offset, int dataLength, ref int bytesConsumed)
		{
			if (dataLength < this.nextLiteralSize - this.StoredDataLength + 2)
			{
				this.AppendArray(data, offset, dataLength);
				bytesConsumed += dataLength;
				return dataLength;
			}
			if (this.nextLiteralSize + 1 == this.StoredDataLength && data[offset] == 10)
			{
				this.RemoveFromArgumentsString(1);
				bytesConsumed++;
				this.ParseArguments();
				return 1;
			}
			int num = Math.Max(this.nextLiteralSize - this.StoredDataLength, 0);
			while (dataLength > num + 1 && (data[offset + num++] != 13 || data[offset + num++] != 10))
			{
			}
			if (dataLength < num || data[offset + num - 2] != 13 || data[offset + num - 1] != 10)
			{
				this.AppendArray(data, offset, dataLength);
				bytesConsumed += dataLength;
				return dataLength;
			}
			this.AppendArray(data, offset, num - 2);
			bytesConsumed += num;
			this.ParseArguments();
			return num;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000A537 File Offset: 0x00008737
		protected virtual void AppendArray(byte[] data, int offset, int dataLength)
		{
			base.Arguments += Encoding.ASCII.GetString(data, offset, dataLength);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000A557 File Offset: 0x00008757
		protected virtual void RemoveFromArgumentsString(int charsToRemove)
		{
			base.Arguments = base.Arguments.Remove(base.Arguments.Length - charsToRemove);
		}

		// Token: 0x04000106 RID: 262
		internal const string SyncResponseChar = "+";

		// Token: 0x04000107 RID: 263
		internal const int MaxLiteralSizePreAuth = 65536;

		// Token: 0x04000108 RID: 264
		private const string ParenStringExpression = "(?<argument>\\([^\\(\\)]*(((?<Open>\\()[^\\(\\)]*)+((?<Close-Open>\\))[^\\(\\)]*)+)*(?(Open)(?!))\\))";

		// Token: 0x04000109 RID: 265
		private static readonly Regex literalRegEx = new Regex(" ?\\{(?<literalSize>\\d+)(?<sendSyncResponse>\\+?)\\}\\z", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400010A RID: 266
		private static readonly Regex ArgumentsRegEx = new Regex("\\G((?<argument>[\\w\\.]+\\[[^\\]]*?\\](\\<[^\\>]*?\\>)?)|\"(?<argument>([^\"]|\\\")*?)\"|(?<argument>\\([^\\(\\)]*(((?<Open>\\()[^\\(\\)]*)+((?<Close-Open>\\))[^\\(\\)]*)+)*(?(Open)(?!))\\))|(?<argument>\\w+\\[[^\\]]*?\\])|(?<argument>[^ ]+))(?<eol> |$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400010B RID: 267
		private List<string> arrayOfArguments;

		// Token: 0x0400010C RID: 268
		private int nextLiteralSize;

		// Token: 0x0400010D RID: 269
		private int minNumberOfArguments;

		// Token: 0x0400010E RID: 270
		private int maxNumberOfArguments;
	}
}
