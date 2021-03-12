using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E8 RID: 1512
	public class ProvisioningError
	{
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0003006D File Offset: 0x0002E26D
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x00030075 File Offset: 0x0002E275
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string errorDetail
		{
			get
			{
				return this._errorDetail;
			}
			set
			{
				this._errorDetail = value;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0003007E File Offset: 0x0002E27E
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x00030086 File Offset: 0x0002E286
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? resolved
		{
			get
			{
				return this._resolved;
			}
			set
			{
				this._resolved = value;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0003008F File Offset: 0x0002E28F
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x00030097 File Offset: 0x0002E297
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

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x000300A0 File Offset: 0x0002E2A0
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x000300A8 File Offset: 0x0002E2A8
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				this._timestamp = value;
			}
		}

		// Token: 0x04001B5A RID: 7002
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorDetail;

		// Token: 0x04001B5B RID: 7003
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _resolved;

		// Token: 0x04001B5C RID: 7004
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;

		// Token: 0x04001B5D RID: 7005
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _timestamp;
	}
}
