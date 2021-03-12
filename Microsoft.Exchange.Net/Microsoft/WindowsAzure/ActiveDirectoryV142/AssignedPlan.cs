using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E2 RID: 1506
	public class AssignedPlan
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0002FF3E File Offset: 0x0002E13E
		// (set) Token: 0x060018E6 RID: 6374 RVA: 0x0002FF46 File Offset: 0x0002E146
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

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0002FF4F File Offset: 0x0002E14F
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x0002FF57 File Offset: 0x0002E157
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

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0002FF60 File Offset: 0x0002E160
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x0002FF68 File Offset: 0x0002E168
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

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0002FF71 File Offset: 0x0002E171
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x0002FF79 File Offset: 0x0002E179
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

		// Token: 0x04001B4B RID: 6987
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _assignedTimestamp;

		// Token: 0x04001B4C RID: 6988
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001B4D RID: 6989
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;

		// Token: 0x04001B4E RID: 6990
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _servicePlanId;
	}
}
