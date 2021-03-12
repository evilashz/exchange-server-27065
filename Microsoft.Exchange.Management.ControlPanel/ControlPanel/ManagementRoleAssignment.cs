using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000518 RID: 1304
	public class ManagementRoleAssignment : BaseRow
	{
		// Token: 0x1700246B RID: 9323
		// (get) Token: 0x06003E79 RID: 15993 RVA: 0x000BCEE4 File Offset: 0x000BB0E4
		// (set) Token: 0x06003E7A RID: 15994 RVA: 0x000BCEEC File Offset: 0x000BB0EC
		internal Identity RoleAssignmentId { get; private set; }

		// Token: 0x1700246C RID: 9324
		// (get) Token: 0x06003E7B RID: 15995 RVA: 0x000BCEF5 File Offset: 0x000BB0F5
		// (set) Token: 0x06003E7C RID: 15996 RVA: 0x000BCEFD File Offset: 0x000BB0FD
		internal ADObjectId Role { get; private set; }

		// Token: 0x1700246D RID: 9325
		// (get) Token: 0x06003E7D RID: 15997 RVA: 0x000BCF06 File Offset: 0x000BB106
		// (set) Token: 0x06003E7E RID: 15998 RVA: 0x000BCF0E File Offset: 0x000BB10E
		internal string Name { get; private set; }

		// Token: 0x1700246E RID: 9326
		// (get) Token: 0x06003E7F RID: 15999 RVA: 0x000BCF17 File Offset: 0x000BB117
		// (set) Token: 0x06003E80 RID: 16000 RVA: 0x000BCF1F File Offset: 0x000BB11F
		internal RoleAssignmentDelegationType DelegationType { get; private set; }

		// Token: 0x06003E81 RID: 16001 RVA: 0x000BCF28 File Offset: 0x000BB128
		public ManagementRoleAssignment(ExchangeRoleAssignmentPresentation assignment) : base(assignment.ToIdentity(), assignment)
		{
			this.Role = assignment.Role;
			this.RoleAssignmentId = assignment.ToIdentity();
			this.Name = assignment.Name;
			this.DelegationType = assignment.RoleAssignmentDelegationType;
		}
	}
}
