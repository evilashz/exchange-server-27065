using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000163 RID: 355
	[Serializable]
	public class RetentionPolicyTagIdParameter : ADIdParameter
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00027D57 File Offset: 0x00025F57
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00027D5A File Offset: 0x00025F5A
		public RetentionPolicyTagIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00027D63 File Offset: 0x00025F63
		public RetentionPolicyTagIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00027D6C File Offset: 0x00025F6C
		public RetentionPolicyTagIdParameter(RetentionPolicyTag policyTag) : base(policyTag.Id)
		{
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00027D7A File Offset: 0x00025F7A
		public RetentionPolicyTagIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00027D83 File Offset: 0x00025F83
		public RetentionPolicyTagIdParameter()
		{
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00027D8B File Offset: 0x00025F8B
		public static RetentionPolicyTagIdParameter Parse(string rawString)
		{
			return new RetentionPolicyTagIdParameter(rawString);
		}
	}
}
