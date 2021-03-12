using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000207 RID: 519
	internal abstract class ActiveDatabaseCopyValidationCheck : DatabaseValidationCheck
	{
		// Token: 0x06001458 RID: 5208 RVA: 0x00051CAF File Offset: 0x0004FEAF
		protected ActiveDatabaseCopyValidationCheck(DatabaseValidationCheck.ID checkId) : base(checkId)
		{
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00051CB8 File Offset: 0x0004FEB8
		protected override bool IsPrerequisiteMetForCheck(DatabaseValidationCheck.Arguments args)
		{
			return args.Status.IsActive;
		}
	}
}
