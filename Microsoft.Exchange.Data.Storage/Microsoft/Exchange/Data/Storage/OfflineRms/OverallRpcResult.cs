using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AD2 RID: 2770
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class OverallRpcResult
	{
		// Token: 0x060064A1 RID: 25761 RVA: 0x001AB2A0 File Offset: 0x001A94A0
		internal OverallRpcResult(Exception e)
		{
			if (e == null)
			{
				this.status = OverallRpcStatus.Success;
				return;
			}
			RightsManagementServerException ex = e as RightsManagementServerException;
			this.status = ((ex != null && !ex.IsPermanentFailure) ? OverallRpcStatus.TransientFailure : OverallRpcStatus.PermanentFailure);
			this.errorResults = ErrorResult.GetErrorResultListFromException(e);
			if (ex != null)
			{
				this.wellKnownErrorCode = ex.WellKnownErrorCode;
			}
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x060064A2 RID: 25762 RVA: 0x001AB300 File Offset: 0x001A9500
		public List<ErrorResult> ErrorResults
		{
			get
			{
				return this.errorResults;
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x060064A3 RID: 25763 RVA: 0x001AB308 File Offset: 0x001A9508
		public OverallRpcStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x060064A4 RID: 25764 RVA: 0x001AB310 File Offset: 0x001A9510
		public WellKnownErrorCode WellKnownErrorCode
		{
			get
			{
				return this.wellKnownErrorCode;
			}
		}

		// Token: 0x0400394D RID: 14669
		private readonly List<ErrorResult> errorResults = new List<ErrorResult>();

		// Token: 0x0400394E RID: 14670
		private readonly OverallRpcStatus status;

		// Token: 0x0400394F RID: 14671
		private readonly WellKnownErrorCode wellKnownErrorCode;
	}
}
