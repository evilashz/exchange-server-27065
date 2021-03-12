using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E7 RID: 1511
	public class ProvisionedPlan
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x00030032 File Offset: 0x0002E232
		// (set) Token: 0x06001903 RID: 6403 RVA: 0x0003003A File Offset: 0x0002E23A
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

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x00030043 File Offset: 0x0002E243
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0003004B File Offset: 0x0002E24B
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

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x00030054 File Offset: 0x0002E254
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x0003005C File Offset: 0x0002E25C
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

		// Token: 0x04001B57 RID: 6999
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001B58 RID: 7000
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _provisioningStatus;

		// Token: 0x04001B59 RID: 7001
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;
	}
}
