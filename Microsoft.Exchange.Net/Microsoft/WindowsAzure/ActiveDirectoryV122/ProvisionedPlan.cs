using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005BD RID: 1469
	public class ProvisionedPlan
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x0002DF9C File Offset: 0x0002C19C
		// (set) Token: 0x06001669 RID: 5737 RVA: 0x0002DFA4 File Offset: 0x0002C1A4
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

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0002DFAD File Offset: 0x0002C1AD
		// (set) Token: 0x0600166B RID: 5739 RVA: 0x0002DFB5 File Offset: 0x0002C1B5
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

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0002DFBE File Offset: 0x0002C1BE
		// (set) Token: 0x0600166D RID: 5741 RVA: 0x0002DFC6 File Offset: 0x0002C1C6
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

		// Token: 0x04001A27 RID: 6695
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001A28 RID: 6696
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _provisioningStatus;

		// Token: 0x04001A29 RID: 6697
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;
	}
}
