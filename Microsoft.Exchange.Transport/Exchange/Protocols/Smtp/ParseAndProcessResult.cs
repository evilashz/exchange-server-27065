using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000508 RID: 1288
	internal struct ParseAndProcessResult<TEvent> : IEquatable<ParseAndProcessResult<TEvent>> where TEvent : struct
	{
		// Token: 0x06003B84 RID: 15236 RVA: 0x000F8B26 File Offset: 0x000F6D26
		public ParseAndProcessResult(ParseResult parseResult, TEvent smtpEvent)
		{
			this = default(ParseAndProcessResult<TEvent>);
			this.parseResult = parseResult;
			this.SmtpEvent = smtpEvent;
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x000F8B3D File Offset: 0x000F6D3D
		public ParseAndProcessResult(ParsingStatus parsingStatus, SmtpResponse smtpResponse, TEvent smtpEvent, bool disconnectClient = false)
		{
			this = new ParseAndProcessResult<TEvent>(new ParseResult(parsingStatus, smtpResponse, disconnectClient), smtpEvent);
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x000F8B4F File Offset: 0x000F6D4F
		public ParseResult ParseResult
		{
			get
			{
				return this.parseResult;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x000F8B58 File Offset: 0x000F6D58
		public ParsingStatus ParsingStatus
		{
			get
			{
				return this.parseResult.ParsingStatus;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000F8B74 File Offset: 0x000F6D74
		public SmtpResponse SmtpResponse
		{
			get
			{
				return this.parseResult.SmtpResponse;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x000F8B8F File Offset: 0x000F6D8F
		// (set) Token: 0x06003B8A RID: 15242 RVA: 0x000F8B97 File Offset: 0x000F6D97
		public TEvent SmtpEvent { get; private set; }

		// Token: 0x06003B8B RID: 15243 RVA: 0x000F8BA0 File Offset: 0x000F6DA0
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.parseResult, this.SmtpEvent);
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x000F8BC2 File Offset: 0x000F6DC2
		public override bool Equals(object other)
		{
			return other is ParseAndProcessResult<TEvent> && this.Equals((ParseAndProcessResult<TEvent>)other);
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x000F8BDC File Offset: 0x000F6DDC
		public override int GetHashCode()
		{
			int num = 17 + 31 * this.parseResult.GetHashCode();
			int num2 = 31;
			TEvent smtpEvent = this.SmtpEvent;
			return num + num2 * smtpEvent.GetHashCode();
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x000F8C1B File Offset: 0x000F6E1B
		public static bool operator ==(ParseAndProcessResult<TEvent> lhs, ParseAndProcessResult<TEvent> rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x000F8C25 File Offset: 0x000F6E25
		public static bool operator !=(ParseAndProcessResult<TEvent> lhs, ParseAndProcessResult<TEvent> rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x000F8C34 File Offset: 0x000F6E34
		public bool Equals(ParseAndProcessResult<TEvent> other)
		{
			if (this.parseResult.Equals(other.parseResult))
			{
				TEvent smtpEvent = this.SmtpEvent;
				return smtpEvent.Equals(other.SmtpEvent);
			}
			return false;
		}

		// Token: 0x04001DF3 RID: 7667
		private readonly ParseResult parseResult;
	}
}
