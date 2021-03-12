using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x020009B4 RID: 2484
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class RightsManagementServerException : LocalizedException
	{
		// Token: 0x060035DE RID: 13790 RVA: 0x00088C59 File Offset: 0x00086E59
		public RightsManagementServerException(LocalizedString message, bool isPermanentFailure) : base(message)
		{
			this.isPermanentFailure = isPermanentFailure;
			this.wellKnownErrorCode = 0;
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x00088C70 File Offset: 0x00086E70
		public RightsManagementServerException(LocalizedString message, WellKnownErrorCode wellKnownErrorCode, bool isPermanentFailure) : base(message)
		{
			this.wellKnownErrorCode = wellKnownErrorCode;
			this.isPermanentFailure = isPermanentFailure;
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x00088C87 File Offset: 0x00086E87
		public RightsManagementServerException(string info, bool isPermanentFailure) : base(new LocalizedString(info))
		{
			this.isPermanentFailure = isPermanentFailure;
			this.wellKnownErrorCode = 0;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x00088CA3 File Offset: 0x00086EA3
		public RightsManagementServerException(LocalizedString message, CoreException innerException) : base(message, innerException)
		{
			this.wellKnownErrorCode = innerException.ErrorCode;
			this.isPermanentFailure = innerException.IsPermanentFailure;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x00088CC5 File Offset: 0x00086EC5
		public RightsManagementServerException(LocalizedString message, BaseException innerException) : base(message, innerException)
		{
			this.isPermanentFailure = innerException.IsPermanentFailure;
			this.wellKnownErrorCode = 0;
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x00088CE2 File Offset: 0x00086EE2
		public RightsManagementServerException(LocalizedString message, Exception innerException, bool isPermanentFailure) : base(message, innerException)
		{
			this.isPermanentFailure = isPermanentFailure;
			this.wellKnownErrorCode = 0;
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060035E4 RID: 13796 RVA: 0x00088CFA File Offset: 0x00086EFA
		public bool IsPermanentFailure
		{
			get
			{
				return this.isPermanentFailure;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060035E5 RID: 13797 RVA: 0x00088D02 File Offset: 0x00086F02
		public WellKnownErrorCode WellKnownErrorCode
		{
			get
			{
				return this.wellKnownErrorCode;
			}
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x00088D0C File Offset: 0x00086F0C
		protected RightsManagementServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.isPermanentFailure = info.GetBoolean("IsPermanentFailure");
			this.wellKnownErrorCode = (WellKnownErrorCode)info.GetValue("WellKnownErrorCode", this.wellKnownErrorCode.GetType());
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x00088D58 File Offset: 0x00086F58
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("IsPermanentFailure", this.isPermanentFailure);
			info.AddValue("WellKnownErrorCode", this.wellKnownErrorCode);
		}

		// Token: 0x04002E5C RID: 11868
		private const string SerializationIsPermanent = "IsPermanentFailure";

		// Token: 0x04002E5D RID: 11869
		private const string SerializationErrorCode = "WellKnownErrorCode";

		// Token: 0x04002E5E RID: 11870
		private readonly bool isPermanentFailure;

		// Token: 0x04002E5F RID: 11871
		private readonly WellKnownErrorCode wellKnownErrorCode;
	}
}
