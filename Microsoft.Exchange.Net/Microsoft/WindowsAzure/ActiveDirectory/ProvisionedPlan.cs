using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x0200059F RID: 1439
	public class ProvisionedPlan
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0002C210 File Offset: 0x0002A410
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0002C218 File Offset: 0x0002A418
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string capabilityStatus
		{
			get
			{
				return this._capabilityStatus;
			}
			set
			{
				this._capabilityStatus = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0002C221 File Offset: 0x0002A421
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0002C229 File Offset: 0x0002A429
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string provisioningStatus
		{
			get
			{
				return this._provisioningStatus;
			}
			set
			{
				this._provisioningStatus = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0002C232 File Offset: 0x0002A432
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x0002C23A File Offset: 0x0002A43A
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string service
		{
			get
			{
				return this._service;
			}
			set
			{
				this._service = value;
			}
		}

		// Token: 0x0400190F RID: 6415
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001910 RID: 6416
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _provisioningStatus;

		// Token: 0x04001911 RID: 6417
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;
	}
}
