using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	public class DistributionGroupMemberIdParameter : DistributionGroupIdParameter
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x000280FF File Offset: 0x000262FF
		public DistributionGroupMemberIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00028108 File Offset: 0x00026308
		public DistributionGroupMemberIdParameter()
		{
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00028110 File Offset: 0x00026310
		public DistributionGroupMemberIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00028119 File Offset: 0x00026319
		public DistributionGroupMemberIdParameter(ReducedRecipient recipient) : base(recipient.Id)
		{
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00028127 File Offset: 0x00026327
		public DistributionGroupMemberIdParameter(DistributionGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00028135 File Offset: 0x00026335
		public DistributionGroupMemberIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0002813E File Offset: 0x0002633E
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return GeneralRecipientIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00028145 File Offset: 0x00026345
		public new static DistributionGroupMemberIdParameter Parse(string identity)
		{
			return new DistributionGroupMemberIdParameter(identity);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00028150 File Offset: 0x00026350
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			DistributionGroupIdParameter @object = new DistributionGroupIdParameter(base.RawIdentity);
			ReducedRecipient object2 = base.GetObject<ReducedRecipient>(rootId, session, subTreeSession, optionalData, new RecipientIdParameter.GetObjectsDelegate<ReducedRecipient>(@object.GetObjects<ReducedRecipient>), out notFoundReason);
			if (object2 == null)
			{
				return EnumerableWrapper<T>.Empty;
			}
			if (object2.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || object2.RecipientTypeDetails == RecipientTypeDetails.RemoteGroupMailbox)
			{
				throw new ArgumentException(Strings.WrongTypeMailboxRecipient(object2.Id.ToString()));
			}
			IDirectorySession directorySession = TaskHelper.UnderscopeSessionToOrganization(session, object2.OrganizationId, true);
			IDirectorySession reducedRecipientSession = DirectorySessionFactory.Default.GetReducedRecipientSession((IRecipientSession)directorySession, 155, "GetObjects", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\RecipientParameters\\DistributionGroupMemberIdParameter.cs");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, object2.Id);
			return base.PerformPrimarySearch<T>(filter, null, reducedRecipientSession, true, optionalData);
		}
	}
}
