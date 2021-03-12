using System;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000CD RID: 205
	public class EmailRecipient
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0000C8B6 File Offset: 0x0000AAB6
		public EmailRecipient(string displayName, string smtpAddress)
		{
			this.mimeRecipient = new MimeRecipient(displayName, smtpAddress);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000C8CB File Offset: 0x0000AACB
		internal EmailRecipient(MimeRecipient recipient)
		{
			this.mimeRecipient = recipient;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000C8DA File Offset: 0x0000AADA
		internal EmailRecipient(TnefRecipient tnefRecipient)
		{
			this.tnefRecipient = tnefRecipient;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0000C929 File Offset: 0x0000AB29
		public string DisplayName
		{
			get
			{
				if (this.mimeRecipient != null)
				{
					string empty = string.Empty;
					DecodingResults decodingResults;
					this.mimeRecipient.TryGetDisplayName(Utility.DecodeOrFallBack, out decodingResults, out empty);
					return empty;
				}
				return this.tnefRecipient.DisplayName;
			}
			set
			{
				if (this.mimeRecipient != null)
				{
					this.mimeRecipient.DisplayName = value;
					return;
				}
				this.tnefRecipient.DisplayName = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000C94C File Offset: 0x0000AB4C
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0000C96D File Offset: 0x0000AB6D
		public string SmtpAddress
		{
			get
			{
				if (this.mimeRecipient == null)
				{
					return this.tnefRecipient.SmtpAddress;
				}
				return this.mimeRecipient.Email;
			}
			set
			{
				if (this.mimeRecipient != null)
				{
					this.mimeRecipient.Email = value;
					return;
				}
				this.tnefRecipient.SmtpAddress = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000C990 File Offset: 0x0000AB90
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0000C9B1 File Offset: 0x0000ABB1
		public string NativeAddress
		{
			get
			{
				if (this.mimeRecipient == null)
				{
					return this.tnefRecipient.NativeAddress;
				}
				return this.mimeRecipient.Email;
			}
			set
			{
				if (this.mimeRecipient != null)
				{
					throw new InvalidOperationException(EmailMessageStrings.CannotSetNativePropertyForMimeRecipient);
				}
				this.tnefRecipient.NativeAddress = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0000C9ED File Offset: 0x0000ABED
		public string NativeAddressType
		{
			get
			{
				if (this.mimeRecipient == null)
				{
					return this.tnefRecipient.NativeAddressType;
				}
				return "SMTP";
			}
			set
			{
				if (this.mimeRecipient != null)
				{
					throw new InvalidOperationException(EmailMessageStrings.CannotSetNativePropertyForMimeRecipient);
				}
				this.tnefRecipient.NativeAddressType = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000CA0E File Offset: 0x0000AC0E
		internal MimeRecipient MimeRecipient
		{
			get
			{
				if (this.mimeRecipient == null)
				{
					this.mimeRecipient = new MimeRecipient(this.DisplayName, this.SmtpAddress);
				}
				return this.mimeRecipient;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000CA35 File Offset: 0x0000AC35
		internal TnefRecipient TnefRecipient
		{
			get
			{
				if (this.tnefRecipient == null)
				{
					this.tnefRecipient = new TnefRecipient(null, int.MinValue, this.DisplayName, this.SmtpAddress, this.NativeAddress, this.NativeAddressType);
				}
				return this.tnefRecipient;
			}
		}

		// Token: 0x040002BA RID: 698
		private MimeRecipient mimeRecipient;

		// Token: 0x040002BB RID: 699
		private TnefRecipient tnefRecipient;
	}
}
