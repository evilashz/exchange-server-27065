using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x02000030 RID: 48
	internal class SmtpRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00003A5F File Offset: 0x00001C5F
		public SmtpRoutingKey(SmtpAddress smtpAddress)
		{
			if (!smtpAddress.IsValidAddress)
			{
				throw new ArgumentException("The SMTP address is not a valid address", "smtpAddress");
			}
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003A87 File Offset: 0x00001C87
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.Smtp;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003A8C File Offset: 0x00001C8C
		public override string Value
		{
			get
			{
				return this.SmtpAddress.ToString();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00003AAD File Offset: 0x00001CAD
		public SmtpAddress SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public static bool TryParse(string value, out SmtpRoutingKey key)
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
			key = new SmtpRoutingKey(new SmtpAddress(value));
			return true;
		}

		// Token: 0x04000059 RID: 89
		private readonly SmtpAddress smtpAddress;
	}
}
