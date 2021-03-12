using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005B7 RID: 1463
	public class ServicePlanInfo
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0002DC8A File Offset: 0x0002BE8A
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x0002DC92 File Offset: 0x0002BE92
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Guid? servicePlanId
		{
			get
			{
				return this._servicePlanId;
			}
			set
			{
				this._servicePlanId = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0002DC9B File Offset: 0x0002BE9B
		// (set) Token: 0x0600162C RID: 5676 RVA: 0x0002DCA3 File Offset: 0x0002BEA3
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string servicePlanName
		{
			get
			{
				return this._servicePlanName;
			}
			set
			{
				this._servicePlanName = value;
			}
		}

		// Token: 0x04001A0B RID: 6667
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _servicePlanId;

		// Token: 0x04001A0C RID: 6668
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _servicePlanName;
	}
}
