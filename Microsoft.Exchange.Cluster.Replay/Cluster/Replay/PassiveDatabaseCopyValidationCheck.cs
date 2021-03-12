using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000208 RID: 520
	internal abstract class PassiveDatabaseCopyValidationCheck : DatabaseValidationCheck
	{
		// Token: 0x0600145A RID: 5210 RVA: 0x00051CCA File Offset: 0x0004FECA
		protected PassiveDatabaseCopyValidationCheck(DatabaseValidationCheck.ID checkId) : base(checkId)
		{
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00051CD3 File Offset: 0x0004FED3
		protected override bool IsPrerequisiteMetForCheck(DatabaseValidationCheck.Arguments args)
		{
			return !args.Status.IsActive;
		}
	}
}
