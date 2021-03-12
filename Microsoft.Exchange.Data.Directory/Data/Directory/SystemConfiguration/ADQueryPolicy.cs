using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000383 RID: 899
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	public class ADQueryPolicy : ADNonExchangeObject
	{
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x000ADBB9 File Offset: 0x000ABDB9
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADQueryPolicy.schema;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002954 RID: 10580 RVA: 0x000ADBC0 File Offset: 0x000ABDC0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADQueryPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x000ADBC7 File Offset: 0x000ABDC7
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x000ADBDE File Offset: 0x000ABDDE
		[ValidateRange(5, 2147483647)]
		[Parameter]
		public int? MaxNotificationPerConnection
		{
			get
			{
				return (int?)this.propertyBag[ADQueryPolicySchema.MaxNotificationPerConn];
			}
			set
			{
				this.propertyBag[ADQueryPolicySchema.MaxNotificationPerConn] = value;
			}
		}

		// Token: 0x0400193D RID: 6461
		private static ADQueryPolicySchema schema = ObjectSchema.GetInstance<ADQueryPolicySchema>();

		// Token: 0x0400193E RID: 6462
		private static string mostDerivedClass = "queryPolicy";

		// Token: 0x0400193F RID: 6463
		public static readonly string ADDefaultQueryPolicyName = "Default Query Policy";
	}
}
