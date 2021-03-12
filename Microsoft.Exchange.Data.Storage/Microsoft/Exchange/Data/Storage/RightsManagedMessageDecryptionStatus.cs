using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B44 RID: 2884
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct RightsManagedMessageDecryptionStatus
	{
		// Token: 0x06006823 RID: 26659 RVA: 0x001B89A0 File Offset: 0x001B6BA0
		public RightsManagedMessageDecryptionStatus(RightsManagementFailureCode failureCode, Exception exception)
		{
			EnumValidator.ThrowIfInvalid<RightsManagementFailureCode>(failureCode, "failureCode");
			this.failureCode = failureCode;
			this.exception = exception;
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06006824 RID: 26660 RVA: 0x001B89BB File Offset: 0x001B6BBB
		public RightsManagementFailureCode FailureCode
		{
			get
			{
				return this.failureCode;
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x06006825 RID: 26661 RVA: 0x001B89C3 File Offset: 0x001B6BC3
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x06006826 RID: 26662 RVA: 0x001B89CB File Offset: 0x001B6BCB
		public bool Failed
		{
			get
			{
				return this.failureCode != RightsManagementFailureCode.Success;
			}
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x001B89DC File Offset: 0x001B6BDC
		public static RightsManagedMessageDecryptionStatus CreateFromException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			RightsManagementPermanentException ex = exception as RightsManagementPermanentException;
			if (ex != null)
			{
				return new RightsManagedMessageDecryptionStatus(ex.FailureCode, ex);
			}
			return new RightsManagedMessageDecryptionStatus(RightsManagementFailureCode.UnknownFailure, exception);
		}

		// Token: 0x04003B40 RID: 15168
		public static readonly RightsManagedMessageDecryptionStatus Success = new RightsManagedMessageDecryptionStatus(RightsManagementFailureCode.Success, null);

		// Token: 0x04003B41 RID: 15169
		public static readonly RightsManagedMessageDecryptionStatus FeatureDisabled = new RightsManagedMessageDecryptionStatus(RightsManagementFailureCode.FeatureDisabled, null);

		// Token: 0x04003B42 RID: 15170
		public static readonly RightsManagedMessageDecryptionStatus NotSupported = new RightsManagedMessageDecryptionStatus(RightsManagementFailureCode.NotSupported, null);

		// Token: 0x04003B43 RID: 15171
		private RightsManagementFailureCode failureCode;

		// Token: 0x04003B44 RID: 15172
		private Exception exception;
	}
}
