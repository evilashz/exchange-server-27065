using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x0200003F RID: 63
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SpecialMailboxPlacementConstraint : IAllocationConstraint
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00007E33 File Offset: 0x00006033
		public SpecialMailboxPlacementConstraint(IList<Guid> nonMovableOrganizations)
		{
			this.nonMovableOrganizations = nonMovableOrganizations;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00007E4C File Offset: 0x0000604C
		public ConstraintValidationResult Accept(LoadEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			DirectoryMailbox directoryMailbox = entity.DirectoryObject as DirectoryMailbox;
			if (directoryMailbox == null)
			{
				return new ConstraintValidationResult(this, true);
			}
			if (!(directoryMailbox is NonConnectedMailbox))
			{
				if (!directoryMailbox.PhysicalMailboxes.Any((IPhysicalMailbox pm) => pm.IsQuarantined))
				{
					Guid organizationId = directoryMailbox.OrganizationId;
					if (this.nonMovableOrganizations.Contains(organizationId))
					{
						return new ConstraintValidationResult(this, false);
					}
					return new ConstraintValidationResult(this, true);
				}
			}
			return new ConstraintValidationResult(this, false);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00007EDC File Offset: 0x000060DC
		public IAllocationConstraint CloneForContainer(LoadContainer container)
		{
			return new SpecialMailboxPlacementConstraint(this.nonMovableOrganizations);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00007EE9 File Offset: 0x000060E9
		public override string ToString()
		{
			return string.Format("IsSpecialMailbox", new object[0]);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007EFC File Offset: 0x000060FC
		public void ValidateAccepted(LoadEntity entity)
		{
			if (!this.Accept(entity))
			{
				Guid guid = Guid.Empty;
				Guid guid2 = entity.Guid;
				DirectoryMailbox directoryMailbox = entity.DirectoryObject as DirectoryMailbox;
				if (directoryMailbox != null)
				{
					guid = directoryMailbox.OrganizationId;
				}
				throw new EntityIsNonMovableException(guid.ToString(), guid2.ToString());
			}
		}

		// Token: 0x040000A6 RID: 166
		[DataMember]
		private readonly IList<Guid> nonMovableOrganizations;
	}
}
