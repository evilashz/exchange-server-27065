using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x02000604 RID: 1540
	public class ServicePlanInfo
	{
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x00032016 File Offset: 0x00030216
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0003201E File Offset: 0x0003021E
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00032027 File Offset: 0x00030227
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0003202F File Offset: 0x0003022F
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

		// Token: 0x04001C86 RID: 7302
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _servicePlanId;

		// Token: 0x04001C87 RID: 7303
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _servicePlanName;
	}
}
