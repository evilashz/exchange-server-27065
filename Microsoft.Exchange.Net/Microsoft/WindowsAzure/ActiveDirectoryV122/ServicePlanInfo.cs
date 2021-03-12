using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005D4 RID: 1492
	public class ServicePlanInfo
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x0002F556 File Offset: 0x0002D756
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x0002F55E File Offset: 0x0002D75E
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

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x0002F567 File Offset: 0x0002D767
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x0002F56F File Offset: 0x0002D76F
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

		// Token: 0x04001AF7 RID: 6903
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _servicePlanId;

		// Token: 0x04001AF8 RID: 6904
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _servicePlanName;
	}
}
