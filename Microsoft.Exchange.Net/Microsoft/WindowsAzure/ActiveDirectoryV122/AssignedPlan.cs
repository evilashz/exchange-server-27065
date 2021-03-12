using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005BB RID: 1467
	public class AssignedPlan
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0002DF26 File Offset: 0x0002C126
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x0002DF2E File Offset: 0x0002C12E
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

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0002DF37 File Offset: 0x0002C137
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x0002DF3F File Offset: 0x0002C13F
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

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0002DF48 File Offset: 0x0002C148
		// (set) Token: 0x0600165F RID: 5727 RVA: 0x0002DF50 File Offset: 0x0002C150
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

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x0002DF59 File Offset: 0x0002C159
		// (set) Token: 0x06001661 RID: 5729 RVA: 0x0002DF61 File Offset: 0x0002C161
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

		// Token: 0x04001A21 RID: 6689
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private DateTime? _assignedTimestamp;

		// Token: 0x04001A22 RID: 6690
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _capabilityStatus;

		// Token: 0x04001A23 RID: 6691
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _service;

		// Token: 0x04001A24 RID: 6692
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Guid? _servicePlanId;
	}
}
