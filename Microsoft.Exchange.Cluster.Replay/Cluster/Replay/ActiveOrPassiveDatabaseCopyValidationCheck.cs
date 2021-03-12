using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000206 RID: 518
	internal abstract class ActiveOrPassiveDatabaseCopyValidationCheck : DatabaseValidationCheck
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x00051CA3 File Offset: 0x0004FEA3
		protected ActiveOrPassiveDatabaseCopyValidationCheck(DatabaseValidationCheck.ID checkId) : base(checkId)
		{
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00051CAC File Offset: 0x0004FEAC
		protected override bool IsPrerequisiteMetForCheck(DatabaseValidationCheck.Arguments args)
		{
			return true;
		}
	}
}
