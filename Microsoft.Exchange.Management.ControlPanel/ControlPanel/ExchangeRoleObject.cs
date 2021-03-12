using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000500 RID: 1280
	[DataContract]
	public class ExchangeRoleObject : AdObjectResolverRow
	{
		// Token: 0x06003DA2 RID: 15778 RVA: 0x000B8D99 File Offset: 0x000B6F99
		public ExchangeRoleObject(ADRawEntry entry) : base(entry)
		{
		}

		// Token: 0x17002433 RID: 9267
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x000B8DA2 File Offset: 0x000B6FA2
		// (set) Token: 0x06003DA4 RID: 15780 RVA: 0x000B8DB9 File Offset: 0x000B6FB9
		public string MailboxPlanIndex
		{
			get
			{
				return (string)base.ADRawEntry[ExchangeRoleSchema.MailboxPlanIndex];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002434 RID: 9268
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x000B8DC0 File Offset: 0x000B6FC0
		// (set) Token: 0x06003DA6 RID: 15782 RVA: 0x000B8DD7 File Offset: 0x000B6FD7
		public bool IsEndUserRole
		{
			get
			{
				return (bool)base.ADRawEntry[ExchangeRoleSchema.IsEndUserRole];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002435 RID: 9269
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000B8DDE File Offset: 0x000B6FDE
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x000B8DF5 File Offset: 0x000B6FF5
		public bool IsRootRole
		{
			get
			{
				return (bool)base.ADRawEntry[ExchangeRoleSchema.IsRootRole];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002436 RID: 9270
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000B8DFC File Offset: 0x000B6FFC
		// (set) Token: 0x06003DAA RID: 15786 RVA: 0x000B8E13 File Offset: 0x000B7013
		public RoleType RoleType
		{
			get
			{
				return (RoleType)base.ADRawEntry[ExchangeRoleSchema.RoleType];
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400281E RID: 10270
		public new static PropertyDefinition[] Properties = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			ExchangeRoleSchema.IsEndUserRole,
			ExchangeRoleSchema.IsRootRole,
			ExchangeRoleSchema.MailboxPlanIndex,
			ExchangeRoleSchema.RoleType
		};
	}
}
