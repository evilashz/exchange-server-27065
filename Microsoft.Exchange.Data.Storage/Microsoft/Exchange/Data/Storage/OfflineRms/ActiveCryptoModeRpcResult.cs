using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AD4 RID: 2772
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class ActiveCryptoModeRpcResult
	{
		// Token: 0x060064AC RID: 25772 RVA: 0x001AB3C4 File Offset: 0x001A95C4
		internal ActiveCryptoModeRpcResult(ActiveCryptoModeResult originalResult)
		{
			if (originalResult == null)
			{
				throw new ArgumentNullException("originalResult");
			}
			this.ActiveCryptoMode = originalResult.ActiveCryptoMode;
			if (originalResult.Error != null)
			{
				this.isPermanentFailure = originalResult.Error.IsPermanentFailure;
				this.wellKnownErrorCode = originalResult.Error.WellKnownErrorCode;
				this.errorResults = ErrorResult.GetErrorResultListFromException(originalResult.Error);
			}
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x060064AD RID: 25773 RVA: 0x001AB437 File Offset: 0x001A9637
		// (set) Token: 0x060064AE RID: 25774 RVA: 0x001AB43F File Offset: 0x001A963F
		public int ActiveCryptoMode { get; private set; }

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x060064AF RID: 25775 RVA: 0x001AB448 File Offset: 0x001A9648
		public bool IsPermanentFailure
		{
			get
			{
				return this.isPermanentFailure;
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x060064B0 RID: 25776 RVA: 0x001AB450 File Offset: 0x001A9650
		public WellKnownErrorCode WellKnownErrorCode
		{
			get
			{
				return this.wellKnownErrorCode;
			}
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x060064B1 RID: 25777 RVA: 0x001AB458 File Offset: 0x001A9658
		public List<ErrorResult> ErrorResults
		{
			get
			{
				return this.errorResults;
			}
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x001AB460 File Offset: 0x001A9660
		public string GetSerializedString()
		{
			return ErrorResult.GetSerializedString(this.errorResults);
		}

		// Token: 0x04003954 RID: 14676
		private readonly List<ErrorResult> errorResults = new List<ErrorResult>();

		// Token: 0x04003955 RID: 14677
		private readonly bool isPermanentFailure;

		// Token: 0x04003956 RID: 14678
		private readonly WellKnownErrorCode wellKnownErrorCode;
	}
}
