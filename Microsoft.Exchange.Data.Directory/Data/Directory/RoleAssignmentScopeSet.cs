using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200017F RID: 383
	[Serializable]
	internal class RoleAssignmentScopeSet : SimpleScopeSet<RbacScope>
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x0004F90E File Offset: 0x0004DB0E
		public RoleAssignmentScopeSet(RbacScope recipientReadScope, RbacScope recipientWriteScope, RbacScope configReadScope, RbacScope configWriteScope) : base(recipientReadScope, recipientWriteScope, configReadScope, configWriteScope)
		{
		}
	}
}
