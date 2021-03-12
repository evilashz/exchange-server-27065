using System;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x0200002B RID: 43
	internal class DatabaseGuidRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000035D0 File Offset: 0x000017D0
		public DatabaseGuidRoutingKey(Guid databaseGuid, string domainName) : this(databaseGuid, domainName, null)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000035DB File Offset: 0x000017DB
		public DatabaseGuidRoutingKey(Guid databaseGuid, string domainName, string resourceForest)
		{
			if (domainName == null)
			{
				throw new ArgumentNullException("domainName");
			}
			this.databaseGuid = databaseGuid;
			this.domainName = domainName;
			this.resourceForest = resourceForest;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003606 File Offset: 0x00001806
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.DatabaseGuid;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000360C File Offset: 0x0000180C
		public override string Value
		{
			get
			{
				return string.Concat(new object[]
				{
					this.DatabaseGuid,
					'@',
					this.DomainName,
					'@',
					this.ResourceForest
				});
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000365A File Offset: 0x0000185A
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003662 File Offset: 0x00001862
		public string DomainName
		{
			get
			{
				return this.domainName;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000366A File Offset: 0x0000186A
		public string ResourceForest
		{
			get
			{
				return this.resourceForest;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003674 File Offset: 0x00001874
		public static bool TryParse(string value, out DatabaseGuidRoutingKey key)
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
			string text;
			string text2;
			Utilities.GetTwoSubstrings(value.Substring(num + 1), '@', out text, out text2);
			key = new DatabaseGuidRoutingKey(guid, text, text2);
			return true;
		}

		// Token: 0x04000049 RID: 73
		private const char Separator = '@';

		// Token: 0x0400004A RID: 74
		private readonly Guid databaseGuid;

		// Token: 0x0400004B RID: 75
		private readonly string domainName;

		// Token: 0x0400004C RID: 76
		private readonly string resourceForest;
	}
}
