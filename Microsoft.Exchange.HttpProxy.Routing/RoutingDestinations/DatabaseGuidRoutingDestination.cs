using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations
{
	// Token: 0x02000017 RID: 23
	internal class DatabaseGuidRoutingDestination : RoutingDestinationBase
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002E53 File Offset: 0x00001053
		public DatabaseGuidRoutingDestination(Guid databaseGuid, string domainName) : this(databaseGuid, domainName, null)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E5E File Offset: 0x0000105E
		public DatabaseGuidRoutingDestination(Guid databaseGuid, string domainName, string resourceForest)
		{
			if (domainName == null)
			{
				throw new ArgumentNullException("domainName");
			}
			this.databaseGuid = databaseGuid;
			this.domainName = domainName;
			this.resourceForest = resourceForest;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002E89 File Offset: 0x00001089
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002E91 File Offset: 0x00001091
		public override IList<string> Properties
		{
			get
			{
				return Array<string>.Empty;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002E98 File Offset: 0x00001098
		public string DomainName
		{
			get
			{
				return this.domainName;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002EA0 File Offset: 0x000010A0
		public string ResourceForest
		{
			get
			{
				return this.resourceForest;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002EA8 File Offset: 0x000010A8
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.DatabaseGuid;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EAC File Offset: 0x000010AC
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

		// Token: 0x06000063 RID: 99 RVA: 0x00002EFC File Offset: 0x000010FC
		public static bool TryParse(string value, IList<string> properties, out DatabaseGuidRoutingDestination destination)
		{
			destination = null;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
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
			destination = new DatabaseGuidRoutingDestination(guid, text, text2);
			return true;
		}

		// Token: 0x04000023 RID: 35
		private const char Separator = '@';

		// Token: 0x04000024 RID: 36
		private readonly Guid databaseGuid;

		// Token: 0x04000025 RID: 37
		private readonly string domainName;

		// Token: 0x04000026 RID: 38
		private readonly string resourceForest;
	}
}
