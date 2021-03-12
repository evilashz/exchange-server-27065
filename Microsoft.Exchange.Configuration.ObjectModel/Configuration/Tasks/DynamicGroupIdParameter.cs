using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200016A RID: 362
	[Serializable]
	public class DynamicGroupIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x0002821A File Offset: 0x0002641A
		public DynamicGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00028223 File Offset: 0x00026423
		public DynamicGroupIdParameter()
		{
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002822B File Offset: 0x0002642B
		public DynamicGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00028234 File Offset: 0x00026434
		public DynamicGroupIdParameter(DynamicDistributionGroup ddl) : base(ddl.Id)
		{
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00028242 File Offset: 0x00026442
		public DynamicGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0002824B File Offset: 0x0002644B
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return DynamicGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00028252 File Offset: 0x00026452
		public new static DynamicGroupIdParameter Parse(string identity)
		{
			return new DynamicGroupIdParameter(identity);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002825A File Offset: 0x0002645A
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeDynamicGroup(id);
		}

		// Token: 0x040002E9 RID: 745
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.DynamicDistributionGroup
		};
	}
}
