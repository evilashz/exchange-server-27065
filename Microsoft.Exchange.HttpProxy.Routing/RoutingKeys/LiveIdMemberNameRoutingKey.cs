using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x0200002D RID: 45
	public class LiveIdMemberNameRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00003790 File Offset: 0x00001990
		public LiveIdMemberNameRoutingKey(SmtpAddress liveIdMemberName, string organizationContext)
		{
			if (!liveIdMemberName.IsValidAddress)
			{
				throw new ArgumentException("The liveid member name is not valid", "liveIdMemberName");
			}
			if (!string.IsNullOrEmpty(organizationContext) && !SmtpAddress.IsValidDomain(organizationContext))
			{
				throw new ArgumentException("The organizationContext is not valid", "organizationContext");
			}
			this.liveIdMemberName = liveIdMemberName;
			this.organizationContext = organizationContext;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000037EA File Offset: 0x000019EA
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.LiveIdMemberName;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000037ED File Offset: 0x000019ED
		public override string Value
		{
			get
			{
				return this.LiveIdMemberName + ';' + this.OrganizationContext;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000380C File Offset: 0x00001A0C
		public SmtpAddress LiveIdMemberName
		{
			get
			{
				return this.liveIdMemberName;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003814 File Offset: 0x00001A14
		public string OrganizationContext
		{
			get
			{
				return this.organizationContext;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000381C File Offset: 0x00001A1C
		public string OrganizationDomain
		{
			get
			{
				string domain = this.LiveIdMemberName.Domain;
				if (!string.IsNullOrEmpty(this.OrganizationContext))
				{
					domain = this.OrganizationContext;
				}
				return domain;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003850 File Offset: 0x00001A50
		public static bool TryParse(string value, out LiveIdMemberNameRoutingKey key)
		{
			key = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			int num = value.IndexOf(';');
			if (num == -1)
			{
				return false;
			}
			string address = value.Substring(0, num);
			if (!SmtpAddress.IsValidSmtpAddress(address))
			{
				return false;
			}
			string domain = null;
			if (num + 1 < value.Length)
			{
				domain = value.Substring(num + 1);
				if (!SmtpAddress.IsValidDomain(domain))
				{
					return false;
				}
			}
			key = new LiveIdMemberNameRoutingKey(new SmtpAddress(address), domain);
			return true;
		}

		// Token: 0x04000050 RID: 80
		private const char Separator = ';';

		// Token: 0x04000051 RID: 81
		private readonly SmtpAddress liveIdMemberName;

		// Token: 0x04000052 RID: 82
		private readonly string organizationContext;
	}
}
