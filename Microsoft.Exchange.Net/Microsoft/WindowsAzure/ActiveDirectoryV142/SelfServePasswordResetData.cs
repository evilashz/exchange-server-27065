using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E9 RID: 1513
	public class SelfServePasswordResetData
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x000300B9 File Offset: 0x0002E2B9
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x000300C1 File Offset: 0x0002E2C1
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? lastRegisteredTime
		{
			get
			{
				return this._lastRegisteredTime;
			}
			set
			{
				this._lastRegisteredTime = value;
			}
		}

		// Token: 0x04001B5E RID: 7006
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _lastRegisteredTime;
	}
}
