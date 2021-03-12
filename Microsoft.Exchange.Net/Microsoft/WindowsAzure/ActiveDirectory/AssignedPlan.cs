using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x0200059D RID: 1437
	public class AssignedPlan
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0002C19A File Offset: 0x0002A39A
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x0002C1A2 File Offset: 0x0002A3A2
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public DateTime? assignedTimestamp
		{
			get
			{
				return this._assignedTimestamp;
			}
			set
			{
				this._assignedTimestamp = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0002C1AB File Offset: 0x0002A3AB
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x0002C1B3 File Offset: 0x0002A3B3
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

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0002C1BC File Offset: 0x0002A3BC
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x0002C1C4 File Offset: 0x0002A3C4
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

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0002C1CD File Offset: 0x0002A3CD
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0002C1D5 File Offset: 0x0002A3D5
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

		// Token: 0x04001909 RID: 6409
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _assignedTimestamp;

		// Token: 0x0400190A RID: 6410
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x0400190B RID: 6411
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;

		// Token: 0x0400190C RID: 6412
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _servicePlanId;
	}
}
