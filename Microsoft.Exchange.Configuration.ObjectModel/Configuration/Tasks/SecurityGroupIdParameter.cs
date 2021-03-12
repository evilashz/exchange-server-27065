using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000180 RID: 384
	[Serializable]
	public class SecurityGroupIdParameter : SecurityPrincipalIdParameter
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0002A078 File Offset: 0x00028278
		public SecurityGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002A081 File Offset: 0x00028281
		public SecurityGroupIdParameter()
		{
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0002A089 File Offset: 0x00028289
		public SecurityGroupIdParameter(DistributionGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0002A097 File Offset: 0x00028297
		public SecurityGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0002A0A0 File Offset: 0x000282A0
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return SecurityGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0002A0A7 File Offset: 0x000282A7
		public new static SecurityGroupIdParameter Parse(string identity)
		{
			return new SecurityGroupIdParameter(identity);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0002A0AF File Offset: 0x000282AF
		protected override SecurityPrincipalIdParameter CreateSidParameter(string identity)
		{
			return new SecurityGroupIdParameter(identity);
		}

		// Token: 0x04000305 RID: 773
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.Group,
			RecipientType.MailUniversalSecurityGroup
		};
	}
}
