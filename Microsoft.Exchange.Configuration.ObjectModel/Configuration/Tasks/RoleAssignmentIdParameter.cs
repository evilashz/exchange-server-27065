using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	public class RoleAssignmentIdParameter : ADIdParameter
	{
		// Token: 0x06000E4B RID: 3659 RVA: 0x0002A99E File Offset: 0x00028B9E
		public RoleAssignmentIdParameter()
		{
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0002A9AD File Offset: 0x00028BAD
		public RoleAssignmentIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002A9BD File Offset: 0x00028BBD
		public RoleAssignmentIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0002A9CD File Offset: 0x00028BCD
		public RoleAssignmentIdParameter(ExchangeRoleAssignment roleAssignment) : base(roleAssignment.Id)
		{
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002A9E2 File Offset: 0x00028BE2
		public RoleAssignmentIdParameter(ExchangeRoleAssignmentPresentation roleAssignmentPresentation) : base(roleAssignmentPresentation.Id)
		{
			this.user = roleAssignmentPresentation.User;
			this.assignmentMethod = roleAssignmentPresentation.AssignmentMethod;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0002AA0F File Offset: 0x00028C0F
		public RoleAssignmentIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0002AA1F File Offset: 0x00028C1F
		public AssignmentMethod AssignmentMethod
		{
			get
			{
				return this.assignmentMethod;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0002AA27 File Offset: 0x00028C27
		public ADObjectId User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0002AA2F File Offset: 0x00028C2F
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x0002AA37 File Offset: 0x00028C37
		internal QueryFilter InternalFilter
		{
			get
			{
				return this.internalFilter;
			}
			set
			{
				this.internalFilter = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0002AA40 File Offset: 0x00028C40
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					this.internalFilter
				});
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0002AA6C File Offset: 0x00028C6C
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0002AA6F File Offset: 0x00028C6F
		public static RoleAssignmentIdParameter Parse(string identity)
		{
			return new RoleAssignmentIdParameter(identity);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0002AA78 File Offset: 0x00028C78
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			IEnumerable<T> objects = base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(objects);
			if (!wrapper.HasElements())
			{
				LocalizedString value;
				if (((IConfigDataProvider)session).Source != null)
				{
					value = Strings.ErrorManagementObjectNotFoundWithSource(this.ToString(), ((IConfigDataProvider)session).Source);
				}
				else
				{
					value = Strings.ErrorManagementObjectNotFound(this.ToString());
				}
				if (notFoundReason != null)
				{
					string notFound = value;
					LocalizedString? localizedString = notFoundReason;
					value = Strings.ErrorNotFoundWithReason(notFound, (localizedString != null) ? localizedString.GetValueOrDefault() : null);
				}
				notFoundReason = new LocalizedString?(Strings.ErrorRoleAssignmentNotFound(value));
			}
			return objects;
		}

		// Token: 0x04000312 RID: 786
		private AssignmentMethod assignmentMethod = AssignmentMethod.Direct;

		// Token: 0x04000313 RID: 787
		private ADObjectId user;

		// Token: 0x04000314 RID: 788
		private QueryFilter internalFilter;
	}
}
