using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x020005A0 RID: 1440
	public class ProvisioningError
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0002C24B File Offset: 0x0002A44B
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x0002C253 File Offset: 0x0002A453
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0002C25C File Offset: 0x0002A45C
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x0002C264 File Offset: 0x0002A464
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

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0002C26D File Offset: 0x0002A46D
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x0002C275 File Offset: 0x0002A475
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

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0002C27E File Offset: 0x0002A47E
		// (set) Token: 0x0600141B RID: 5147 RVA: 0x0002C286 File Offset: 0x0002A486
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

		// Token: 0x04001912 RID: 6418
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _errorDetail;

		// Token: 0x04001913 RID: 6419
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _resolved;

		// Token: 0x04001914 RID: 6420
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;

		// Token: 0x04001915 RID: 6421
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _timestamp;
	}
}
