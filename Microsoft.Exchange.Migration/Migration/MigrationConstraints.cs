using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000053 RID: 83
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationConstraints
	{
		// Token: 0x04000123 RID: 291
		public const int MaxImapPortValue = 65535;

		// Token: 0x04000124 RID: 292
		public static readonly StringLengthConstraint NameLengthConstraint = new StringLengthConstraint(1, 256);

		// Token: 0x04000125 RID: 293
		public static readonly StringLengthConstraint RemoteServerNameConstraint = new StringLengthConstraint(1, 256);

		// Token: 0x04000126 RID: 294
		public static readonly RangedValueConstraint<int> PortRangeConstraint = new RangedValueConstraint<int>(1, 65535);

		// Token: 0x04000127 RID: 295
		public static readonly RangedValueConstraint<Unlimited<int>> MaxConcurrentMigrationsConstraint = new RangedValueConstraint<Unlimited<int>>(1, Unlimited<int>.UnlimitedValue);

		// Token: 0x04000128 RID: 296
		public static readonly RangedValueConstraint<int> PasswordRangeConstraint = new RangedValueConstraint<int>(1, 256);

		// Token: 0x04000129 RID: 297
		public static readonly RangedValueConstraint<int> ExportMigrationReportRowCountConstraint = new RangedValueConstraint<int>(0, 2000);
	}
}
