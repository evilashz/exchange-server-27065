using System;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000027 RID: 39
	internal class SetupWizardEventArgs : EventArgs
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000AE5C File Offset: 0x0000905C
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000AE63 File Offset: 0x00009063
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

		// Token: 0x04000106 RID: 262
		private static Exception errorException;
	}
}
