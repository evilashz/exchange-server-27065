using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x0200012A RID: 298
	public class MachineSettingsContext : IConstraintProvider
	{
		// Token: 0x06000E44 RID: 3652 RVA: 0x00022851 File Offset: 0x00020A51
		protected MachineSettingsContext()
		{
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00022859 File Offset: 0x00020A59
		public static MachineSettingsContext Local
		{
			get
			{
				return MachineSettingsContext.LocalContext;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x00022860 File Offset: 0x00020A60
		public ConstraintCollection Constraints
		{
			get
			{
				return ConstraintCollection.CreateGlobal();
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00022867 File Offset: 0x00020A67
		public string RotationId
		{
			get
			{
				return "Global";
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0002286E File Offset: 0x00020A6E
		public string RampId
		{
			get
			{
				return "Global";
			}
		}

		// Token: 0x04000487 RID: 1159
		private const string SnapshotId = "Global";

		// Token: 0x04000488 RID: 1160
		private static readonly MachineSettingsContext LocalContext = new MachineSettingsContext();
	}
}
