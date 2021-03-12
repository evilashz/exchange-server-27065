using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200003D RID: 61
	internal sealed class RoleDefinition : BackgroundJobBackendBase
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007928 File Offset: 0x00005B28
		// (set) Token: 0x06000201 RID: 513 RVA: 0x0000793A File Offset: 0x00005B3A
		public Guid RoleId
		{
			get
			{
				return (Guid)this[RoleDefinition.RoleIdProperty];
			}
			set
			{
				this[RoleDefinition.RoleIdProperty] = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000794D File Offset: 0x00005B4D
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000795F File Offset: 0x00005B5F
		public string RoleName
		{
			get
			{
				return (string)this[RoleDefinition.RoleNameProperty];
			}
			set
			{
				this[RoleDefinition.RoleNameProperty] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000796D File Offset: 0x00005B6D
		// (set) Token: 0x06000205 RID: 517 RVA: 0x0000797F File Offset: 0x00005B7F
		public string RoleVersion
		{
			get
			{
				return (string)this[RoleDefinition.RoleVersionProperty];
			}
			set
			{
				this[RoleDefinition.RoleVersionProperty] = value;
			}
		}

		// Token: 0x04000160 RID: 352
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = JobDefinitionProperties.RoleIdProperty;

		// Token: 0x04000161 RID: 353
		internal static readonly BackgroundJobBackendPropertyDefinition RoleNameProperty = new BackgroundJobBackendPropertyDefinition("RoleName", typeof(string), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x04000162 RID: 354
		internal static readonly BackgroundJobBackendPropertyDefinition RoleVersionProperty = new BackgroundJobBackendPropertyDefinition("RoleVersion", typeof(string), PropertyDefinitionFlags.Mandatory, null);
	}
}
