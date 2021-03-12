using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005BE RID: 1470
	public class ProvisioningError
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0002DFD7 File Offset: 0x0002C1D7
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0002DFDF File Offset: 0x0002C1DF
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

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0002DFE8 File Offset: 0x0002C1E8
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
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

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0002DFF9 File Offset: 0x0002C1F9
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x0002E001 File Offset: 0x0002C201
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

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0002E00A File Offset: 0x0002C20A
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x0002E012 File Offset: 0x0002C212
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

		// Token: 0x04001A2A RID: 6698
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorDetail;

		// Token: 0x04001A2B RID: 6699
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _resolved;

		// Token: 0x04001A2C RID: 6700
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;

		// Token: 0x04001A2D RID: 6701
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _timestamp;
	}
}
