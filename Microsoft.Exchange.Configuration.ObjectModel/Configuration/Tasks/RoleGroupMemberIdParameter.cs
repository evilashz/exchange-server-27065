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
	// Token: 0x0200017E RID: 382
	[Serializable]
	public class RoleGroupMemberIdParameter : RecipientIdParameter
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002965A File Offset: 0x0002785A
		public RoleGroupMemberIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00029663 File Offset: 0x00027863
		public RoleGroupMemberIdParameter()
		{
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002966B File Offset: 0x0002786B
		public RoleGroupMemberIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00029674 File Offset: 0x00027874
		public RoleGroupMemberIdParameter(RoleGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00029682 File Offset: 0x00027882
		public RoleGroupMemberIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0002968B File Offset: 0x0002788B
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return GeneralRecipientIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00029692 File Offset: 0x00027892
		public new static RoleGroupMemberIdParameter Parse(string identity)
		{
			return new RoleGroupMemberIdParameter(identity);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002969C File Offset: 0x0002789C
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ReducedRecipient))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			OptionalIdentityData optionalIdentityData;
			if (optionalData == null)
			{
				optionalIdentityData = new OptionalIdentityData();
			}
			else
			{
				optionalIdentityData = optionalData.Clone();
			}
			RoleGroupIdParameter @object = new RoleGroupIdParameter(base.RawIdentity);
			ADGroup object2 = base.GetObject<ADGroup>(rootId, session, subTreeSession, optionalIdentityData, new RecipientIdParameter.GetObjectsDelegate<ADGroup>(@object.GetObjects<ADGroup>), out notFoundReason);
			if (object2 == null)
			{
				return EnumerableWrapper<T>.Empty;
			}
			optionalIdentityData.RootOrgDomainContainerId = null;
			IDirectorySession directorySession = TaskHelper.UnderscopeSessionToOrganization(session, object2.OrganizationId, true);
			IDirectorySession reducedRecipientSession = DirectorySessionFactory.Default.GetReducedRecipientSession((IRecipientSession)directorySession, 160, "GetObjects", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\RecipientParameters\\RoleGroupMemberIdParameter.cs");
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.MemberOfGroup, object2.Id);
			return base.PerformPrimarySearch<T>(filter, null, reducedRecipientSession, true, optionalIdentityData);
		}
	}
}
