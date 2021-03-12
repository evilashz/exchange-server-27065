using System;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000014 RID: 20
	internal class SetupWizardEventArgs : EventArgs
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000AA4C File Offset: 0x00008C4C
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000AA53 File Offset: 0x00008C53
		public static Exception ErrorException
		{
			get
			{
				return SetupWizardEventArgs.errorException;
			}
			set
			{
				SetupWizardEventArgs.errorException = value;
			}
		}

		// Token: 0x04000096 RID: 150
		private static Exception errorException;
	}
}
