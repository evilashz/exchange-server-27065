using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.LiveIDAuthentication
{
	// Token: 0x02000762 RID: 1890
	internal sealed class AuthenticationToken : BaseAuthenticationToken
	{
		// Token: 0x06002525 RID: 9509 RVA: 0x0004D99E File Offset: 0x0004BB9E
		public AuthenticationToken(string rawToken, DateTime expiry, string binarySecret, string puid)
		{
			AuthenticationToken.ThrowIfInvalid(rawToken);
			ArgumentValidator.ThrowIfNullOrEmpty("puid", puid);
			this.rawToken = rawToken;
			this.expiry = expiry;
			this.binarySecret = binarySecret;
			this.puid = puid;
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x0004D9D5 File Offset: 0x0004BBD5
		public string RawToken
		{
			get
			{
				return this.rawToken;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x0004D9DD File Offset: 0x0004BBDD
		public DateTime Expiry
		{
			get
			{
				return this.expiry;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x0004D9E8 File Offset: 0x0004BBE8
		public bool IsExpired
		{
			get
			{
				return this.expiry.ToUniversalTime() <= DateTime.UtcNow;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x0004DA0D File Offset: 0x0004BC0D
		public string Ticket
		{
			get
			{
				if (!this.tokenized)
				{
					this.Tokenize();
					this.tokenized = true;
				}
				return this.ticket;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x0004DA2A File Offset: 0x0004BC2A
		public string Passport
		{
			get
			{
				if (!this.tokenized)
				{
					this.Tokenize();
					this.tokenized = true;
				}
				return this.passport;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x0004DA47 File Offset: 0x0004BC47
		public string BinarySecret
		{
			get
			{
				return this.binarySecret;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x0004DA4F File Offset: 0x0004BC4F
		public string PUID
		{
			get
			{
				return this.puid;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x0004DA57 File Offset: 0x0004BC57
		public string UrlEncodedTicket
		{
			get
			{
				if (!this.ticketEncoded)
				{
					this.urlEncodedTicket = HttpUtility.UrlEncode(this.Ticket);
					this.ticketEncoded = true;
				}
				return this.urlEncodedTicket;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x0004DA7F File Offset: 0x0004BC7F
		public string EncodedQueryStringTicket
		{
			get
			{
				return "t=" + this.UrlEncodedTicket;
			}
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0004DA91 File Offset: 0x0004BC91
		public override string ToString()
		{
			return this.rawToken;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0004DA99 File Offset: 0x0004BC99
		private static void ThrowIfInvalid(string rawToken)
		{
			if (rawToken == null || !rawToken.Contains("&") || !rawToken.Contains("t=") || !rawToken.Contains("p="))
			{
				throw new ArgumentException("Invalid token", "rawToken");
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0004DAD8 File Offset: 0x0004BCD8
		private void Tokenize()
		{
			string[] array = this.rawToken.Split(new char[]
			{
				'&'
			});
			foreach (string text in array)
			{
				string text2 = text.Substring(0, text.IndexOf('='));
				string text3 = text.Substring(text.IndexOf('=') + 1);
				string a;
				if ((a = text2.ToLowerInvariant()) != null)
				{
					if (!(a == "t"))
					{
						if (a == "p")
						{
							this.passport = text3;
						}
					}
					else
					{
						this.ticket = text3;
					}
				}
			}
		}

		// Token: 0x04002291 RID: 8849
		private readonly string rawToken;

		// Token: 0x04002292 RID: 8850
		private readonly DateTime expiry;

		// Token: 0x04002293 RID: 8851
		private string ticket;

		// Token: 0x04002294 RID: 8852
		private string passport;

		// Token: 0x04002295 RID: 8853
		private bool tokenized;

		// Token: 0x04002296 RID: 8854
		private string binarySecret;

		// Token: 0x04002297 RID: 8855
		private string puid;

		// Token: 0x04002298 RID: 8856
		private string urlEncodedTicket;

		// Token: 0x04002299 RID: 8857
		private bool ticketEncoded;
	}
}
