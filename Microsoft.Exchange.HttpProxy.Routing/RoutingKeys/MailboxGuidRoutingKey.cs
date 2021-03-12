using System;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x0200002E RID: 46
	internal class MailboxGuidRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000038BC File Offset: 0x00001ABC
		public MailboxGuidRoutingKey(Guid mailboxGuid, string tenantDomain)
		{
			if (tenantDomain == null)
			{
				throw new ArgumentNullException("tenantDomain");
			}
			this.mailboxGuid = mailboxGuid;
			this.tenantDomain = tenantDomain;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000038E0 File Offset: 0x00001AE0
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.MailboxGuid;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000038E3 File Offset: 0x00001AE3
		public override string Value
		{
			get
			{
				return this.MailboxGuid + '@' + this.TenantDomain;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003902 File Offset: 0x00001B02
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000390A File Offset: 0x00001B0A
		public string TenantDomain
		{
			get
			{
				return this.tenantDomain;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003914 File Offset: 0x00001B14
		public static bool TryParse(string value, out MailboxGuidRoutingKey key)
		{
			key = null;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int num = value.IndexOf('@');
			if (num == -1)
			{
				return false;
			}
			string input = value.Substring(0, num);
			Guid guid;
			if (!Guid.TryParse(input, out guid))
			{
				return false;
			}
			key = new MailboxGuidRoutingKey(guid, value.Substring(num + 1));
			return true;
		}

		// Token: 0x04000053 RID: 83
		private const char Separator = '@';

		// Token: 0x04000054 RID: 84
		private readonly Guid mailboxGuid;

		// Token: 0x04000055 RID: 85
		private readonly string tenantDomain;
	}
}
