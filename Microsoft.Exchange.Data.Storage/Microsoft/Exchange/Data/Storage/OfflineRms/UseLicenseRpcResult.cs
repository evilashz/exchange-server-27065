using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AD3 RID: 2771
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class UseLicenseRpcResult
	{
		// Token: 0x060064A5 RID: 25765 RVA: 0x001AB318 File Offset: 0x001A9518
		internal UseLicenseRpcResult(UseLicenseResult originalResult)
		{
			if (originalResult == null)
			{
				throw new ArgumentNullException("originalResult");
			}
			this.EndUseLicense = originalResult.EndUseLicense;
			if (originalResult.Error != null)
			{
				this.isPermanentFailure = originalResult.Error.IsPermanentFailure;
				this.wellKnownErrorCode = originalResult.Error.WellKnownErrorCode;
				this.errorResults = ErrorResult.GetErrorResultListFromException(originalResult.Error);
			}
		}

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x060064A6 RID: 25766 RVA: 0x001AB38B File Offset: 0x001A958B
		// (set) Token: 0x060064A7 RID: 25767 RVA: 0x001AB393 File Offset: 0x001A9593
		public string EndUseLicense { get; private set; }

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x001AB39C File Offset: 0x001A959C
		public bool IsPermanentFailure
		{
			get
			{
				return this.isPermanentFailure;
			}
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x060064A9 RID: 25769 RVA: 0x001AB3A4 File Offset: 0x001A95A4
		public WellKnownErrorCode WellKnownErrorCode
		{
			get
			{
				return this.wellKnownErrorCode;
			}
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x060064AA RID: 25770 RVA: 0x001AB3AC File Offset: 0x001A95AC
		public List<ErrorResult> ErrorResults
		{
			get
			{
				return this.errorResults;
			}
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x001AB3B4 File Offset: 0x001A95B4
		public string GetSerializedString()
		{
			return ErrorResult.GetSerializedString(this.errorResults);
		}

		// Token: 0x04003950 RID: 14672
		private readonly List<ErrorResult> errorResults = new List<ErrorResult>();

		// Token: 0x04003951 RID: 14673
		private readonly bool isPermanentFailure;

		// Token: 0x04003952 RID: 14674
		private readonly WellKnownErrorCode wellKnownErrorCode;
	}
}
