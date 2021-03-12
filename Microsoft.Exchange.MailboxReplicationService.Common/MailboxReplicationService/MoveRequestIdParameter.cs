using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	public class MoveRequestIdParameter : MailboxOrMailUserIdParameter
	{
		// Token: 0x06001094 RID: 4244 RVA: 0x00026EE1 File Offset: 0x000250E1
		public MoveRequestIdParameter()
		{
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00026EE9 File Offset: 0x000250E9
		public MoveRequestIdParameter(MoveRequestStatistics moveRequest) : this(moveRequest.MailboxIdentity)
		{
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00026EF7 File Offset: 0x000250F7
		public MoveRequestIdParameter(MoveRequest moveRequest) : this(moveRequest.Id)
		{
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00026F05 File Offset: 0x00025105
		public MoveRequestIdParameter(ADObjectId userId) : base(userId)
		{
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00026F0E File Offset: 0x0002510E
		public MoveRequestIdParameter(Mailbox user) : base(user)
		{
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00026F17 File Offset: 0x00025117
		public MoveRequestIdParameter(MailUser user) : base(user)
		{
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00026F20 File Offset: 0x00025120
		public MoveRequestIdParameter(User user) : base(user.Id)
		{
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00026F2E File Offset: 0x0002512E
		public MoveRequestIdParameter(Guid guid) : base(guid.ToString())
		{
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00026F43 File Offset: 0x00025143
		public MoveRequestIdParameter(string user) : base(user)
		{
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00026F4C File Offset: 0x0002514C
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.NotEqual, ADUserSchema.MailboxMoveStatus, RequestStatus.None),
					new ExistsFilter(ADUserSchema.MailboxMoveStatus)
				});
				if (base.AdditionalQueryFilter != null)
				{
					return new AndFilter(new QueryFilter[]
					{
						queryFilter,
						base.AdditionalQueryFilter
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00026FAC File Offset: 0x000251AC
		public new static MoveRequestIdParameter Parse(string identity)
		{
			return new MoveRequestIdParameter(identity);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00026FB4 File Offset: 0x000251B4
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			IEnumerable<T> objects = base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			if (notFoundReason == null)
			{
				notFoundReason = new LocalizedString?(MrsStrings.ErrorCouldNotFindMoveRequest(this.ToString()));
			}
			return objects;
		}
	}
}
