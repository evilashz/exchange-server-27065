using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005FD RID: 1533
	public class SelfServePasswordResetPolicy
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00031AA6 File Offset: 0x0002FCA6
		// (set) Token: 0x06001B11 RID: 6929 RVA: 0x00031AAE File Offset: 0x0002FCAE
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string enforcedRegistrationEnablement
		{
			get
			{
				return this._enforcedRegistrationEnablement;
			}
			set
			{
				this._enforcedRegistrationEnablement = value;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00031AB7 File Offset: 0x0002FCB7
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x00031ABF File Offset: 0x0002FCBF
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public int? enforcedRegistrationIntervalInDays
		{
			get
			{
				return this._enforcedRegistrationIntervalInDays;
			}
			set
			{
				this._enforcedRegistrationIntervalInDays = value;
			}
		}

		// Token: 0x04001C50 RID: 7248
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _enforcedRegistrationEnablement;

		// Token: 0x04001C51 RID: 7249
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private int? _enforcedRegistrationIntervalInDays;
	}
}
