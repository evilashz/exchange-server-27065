using System;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x0200002C RID: 44
	public class ExternalDirectoryObjectIdRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000036D6 File Offset: 0x000018D6
		public ExternalDirectoryObjectIdRoutingKey(Guid userGuid, Guid tenantGuid)
		{
			this.userGuid = userGuid;
			this.tenantGuid = tenantGuid;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000036EC File Offset: 0x000018EC
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.ExternalDirectoryObjectId;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000036EF File Offset: 0x000018EF
		public override string Value
		{
			get
			{
				return this.UserGuid + '@' + this.TenantGuid;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003713 File Offset: 0x00001913
		public Guid UserGuid
		{
			get
			{
				return this.userGuid;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000371B File Offset: 0x0000191B
		public Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003724 File Offset: 0x00001924
		public static bool TryParse(string value, out ExternalDirectoryObjectIdRoutingKey key)
		{
			key = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
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
			if (num + 1 == value.Length)
			{
				return false;
			}
			string input2 = value.Substring(num + 1);
			Guid guid2;
			if (!Guid.TryParse(input2, out guid2))
			{
				return false;
			}
			key = new ExternalDirectoryObjectIdRoutingKey(guid, guid2);
			return true;
		}

		// Token: 0x0400004D RID: 77
		private const char Separator = '@';

		// Token: 0x0400004E RID: 78
		private readonly Guid userGuid;

		// Token: 0x0400004F RID: 79
		private readonly Guid tenantGuid;
	}
}
