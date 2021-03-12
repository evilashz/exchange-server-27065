using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x0200002A RID: 42
	internal class ArchiveSmtpRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003550 File Offset: 0x00001750
		public ArchiveSmtpRoutingKey(SmtpAddress smtpAddress)
		{
			if (!smtpAddress.IsValidAddress)
			{
				throw new ArgumentException("The SMTP address is not a valid address", "smtpAddress");
			}
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003578 File Offset: 0x00001778
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.ArchiveSmtp;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000357C File Offset: 0x0000177C
		public override string Value
		{
			get
			{
				return this.SmtpAddress.ToString();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000359D File Offset: 0x0000179D
		public SmtpAddress SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000035A5 File Offset: 0x000017A5
		public static bool TryParse(string value, out ArchiveSmtpRoutingKey key)
		{
			key = null;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!SmtpAddress.IsValidSmtpAddress(value))
			{
				return false;
			}
			key = new ArchiveSmtpRoutingKey(new SmtpAddress(value));
			return true;
		}

		// Token: 0x04000048 RID: 72
		private readonly SmtpAddress smtpAddress;
	}
}
