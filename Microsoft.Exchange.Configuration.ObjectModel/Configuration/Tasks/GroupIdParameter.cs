using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200016D RID: 365
	[Serializable]
	public class GroupIdParameter : RecipientIdParameter
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x000283B2 File Offset: 0x000265B2
		public GroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000283BB File Offset: 0x000265BB
		public GroupIdParameter()
		{
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000283C3 File Offset: 0x000265C3
		public GroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x000283CC File Offset: 0x000265CC
		public GroupIdParameter(WindowsGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000283DA File Offset: 0x000265DA
		public GroupIdParameter(DistributionGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x000283E8 File Offset: 0x000265E8
		public GroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000283F1 File Offset: 0x000265F1
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return GroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000283F8 File Offset: 0x000265F8
		public new static GroupIdParameter Parse(string identity)
		{
			return new GroupIdParameter(identity);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00028400 File Offset: 0x00026600
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeGroup(id);
		}

		// Token: 0x040002EC RID: 748
		internal new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.Group,
			RecipientType.MailUniversalDistributionGroup,
			RecipientType.MailUniversalSecurityGroup,
			RecipientType.MailNonUniversalGroup
		};
	}
}
