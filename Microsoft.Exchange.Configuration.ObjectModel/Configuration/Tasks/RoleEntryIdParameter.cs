using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000186 RID: 390
	[Serializable]
	public class RoleEntryIdParameter : ADIdParameter
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x0002A458 File Offset: 0x00028658
		public RoleEntryIdParameter()
		{
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002A46C File Offset: 0x0002866C
		public RoleEntryIdParameter(string identity) : base(identity)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentNullException("identity");
			}
			int num = identity.LastIndexOf('\\');
			if (num == -1 || num == 0 || num == identity.Length - 1)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat(base.GetType().ToString()), "identity");
			}
			this.cmdletOrScriptName = identity.Substring(num + 1);
			this.roleId = new RoleIdParameter(identity.Substring(0, num));
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002A4FC File Offset: 0x000286FC
		public RoleEntryIdParameter(RoleIdParameter roleId, string cmdletOrScriptName, ManagementRoleEntryType type) : base(roleId.RawIdentity)
		{
			if (string.IsNullOrEmpty(cmdletOrScriptName))
			{
				this.cmdletOrScriptName = "*";
			}
			else
			{
				this.cmdletOrScriptName = cmdletOrScriptName;
			}
			this.roleId = roleId;
			this.Type = type;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002A54A File Offset: 0x0002874A
		public RoleEntryIdParameter(ADObjectId adObjectId, string cmdletOrScriptName, ManagementRoleEntryType type) : this(new RoleIdParameter(adObjectId), cmdletOrScriptName, type)
		{
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002A55C File Offset: 0x0002875C
		public RoleEntryIdParameter(ExchangeRoleEntryPresentation roleEntry) : this(roleEntry.Role, roleEntry.Name, roleEntry.Type)
		{
			this.Parameters = new string[roleEntry.Parameters.Count];
			this.PSSnapinName = roleEntry.PSSnapinName;
			roleEntry.Parameters.CopyTo(this.Parameters, 0);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002A5B5 File Offset: 0x000287B5
		public RoleEntryIdParameter(ExchangeRole role) : this(new RoleIdParameter(role.Id), null, ManagementRoleEntryType.All)
		{
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002A5CE File Offset: 0x000287CE
		public RoleEntryIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0002A5E2 File Offset: 0x000287E2
		internal string CmdletOrScriptName
		{
			get
			{
				return this.cmdletOrScriptName;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0002A5EA File Offset: 0x000287EA
		internal RoleIdParameter RoleId
		{
			get
			{
				return this.roleId;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0002A5F2 File Offset: 0x000287F2
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0002A5FA File Offset: 0x000287FA
		internal ManagementRoleEntryType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0002A603 File Offset: 0x00028803
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0002A60B File Offset: 0x0002880B
		internal string[] Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0002A614 File Offset: 0x00028814
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0002A61C File Offset: 0x0002881C
		internal string PSSnapinName
		{
			get
			{
				return this.snapinName;
			}
			set
			{
				this.snapinName = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0002A628 File Offset: 0x00028828
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.AdditionalQueryFilter,
					this.additionalFilter
				});
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0002A654 File Offset: 0x00028854
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002A657 File Offset: 0x00028857
		public static RoleEntryIdParameter Parse(string identity)
		{
			return new RoleEntryIdParameter(identity);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002A660 File Offset: 0x00028860
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}\\{1}", this.RoleId.ToString(), this.CmdletOrScriptName);
			switch (this.Type)
			{
			case ManagementRoleEntryType.Cmdlet:
			case ManagementRoleEntryType.Script:
			case ManagementRoleEntryType.ApplicationPermission:
				stringBuilder.AppendFormat(", type {0}", this.Type);
				break;
			}
			if (!string.IsNullOrEmpty(this.PSSnapinName))
			{
				stringBuilder.AppendFormat(", PSSnapinName {0}", this.PSSnapinName);
			}
			if (this.Parameters != null)
			{
				stringBuilder.AppendFormat(", Parameters: {0}", string.Join(",", this.Parameters));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0002A714 File Offset: 0x00028914
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ExchangeRoleEntryPresentation) && typeof(T) != typeof(ExchangeRole))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			notFoundReason = null;
			if (typeof(T) == typeof(ExchangeRole))
			{
				return this.RoleId.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			}
			List<ExchangeRoleEntryPresentation> list = new List<ExchangeRoleEntryPresentation>();
			this.additionalFilter = this.CreateCmdletAndParametersFilter();
			IEnumerable<ExchangeRole> objects = this.roleId.GetObjects<ExchangeRole>(rootId, session, subTreeSession, optionalData, out notFoundReason);
			foreach (ExchangeRole exchangeRole in objects)
			{
				foreach (RoleEntry roleEntry in exchangeRole.RoleEntries)
				{
					if (RBACHelper.DoesRoleEntryMatchNameAndParameters(roleEntry, this.Type, this.CmdletOrScriptName, this.Parameters, this.PSSnapinName))
					{
						IEnumerator<ExchangeRole> enumerator;
						list.Add(new ExchangeRoleEntryPresentation(enumerator.Current, roleEntry));
					}
				}
			}
			return (IEnumerable<T>)list;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0002A87C File Offset: 0x00028A7C
		private QueryFilter CreateCmdletAndParametersFilter()
		{
			List<QueryFilter> list = new List<QueryFilter>();
			if ((this.Type & ManagementRoleEntryType.Cmdlet) != (ManagementRoleEntryType)0)
			{
				list.Add(RBACHelper.ConstructRoleEntryFilter(this.CmdletOrScriptName, ManagementRoleEntryType.Cmdlet, this.PSSnapinName));
			}
			if ((this.Type & ManagementRoleEntryType.Script) != (ManagementRoleEntryType)0)
			{
				list.Add(new AndFilter(new QueryFilter[]
				{
					RBACHelper.ScriptEnabledRoleEntryTypeFilter,
					RBACHelper.ConstructRoleEntryFilter(this.CmdletOrScriptName, ManagementRoleEntryType.Script)
				}));
			}
			if ((this.Type & ManagementRoleEntryType.ApplicationPermission) != (ManagementRoleEntryType)0)
			{
				list.Add(new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ExchangeRoleSchema.RoleType, RoleType.ApplicationImpersonation),
					RBACHelper.ConstructRoleEntryFilter(this.cmdletOrScriptName, ManagementRoleEntryType.ApplicationPermission)
				}));
			}
			List<QueryFilter> list2 = new List<QueryFilter>();
			if (1 < list.Count)
			{
				list2.Add(new OrFilter(list.ToArray()));
			}
			else if (1 == list.Count)
			{
				list2.Add(list[0]);
			}
			if (this.Parameters != null)
			{
				list2.Add(RBACHelper.ConstructRoleEntryParameterFilter(this.Parameters));
			}
			if (1 < list2.Count)
			{
				return new AndFilter(list2.ToArray());
			}
			if (1 == list2.Count)
			{
				return list2[0];
			}
			return null;
		}

		// Token: 0x0400030C RID: 780
		private RoleIdParameter roleId;

		// Token: 0x0400030D RID: 781
		private string cmdletOrScriptName;

		// Token: 0x0400030E RID: 782
		private ManagementRoleEntryType type = ManagementRoleEntryType.All;

		// Token: 0x0400030F RID: 783
		private string[] parameters;

		// Token: 0x04000310 RID: 784
		private string snapinName;

		// Token: 0x04000311 RID: 785
		private QueryFilter additionalFilter;
	}
}
