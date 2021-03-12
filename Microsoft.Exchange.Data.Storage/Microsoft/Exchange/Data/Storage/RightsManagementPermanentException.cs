using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.Protectors;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200074F RID: 1871
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class RightsManagementPermanentException : StoragePermanentException
	{
		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x00130ADE File Offset: 0x0012ECDE
		// (set) Token: 0x06004835 RID: 18485 RVA: 0x00130AE6 File Offset: 0x0012ECE6
		public virtual RightsManagementFailureCode FailureCode { get; private set; }

		// Token: 0x06004836 RID: 18486 RVA: 0x00130AEF File Offset: 0x0012ECEF
		public RightsManagementPermanentException(RightsManagementFailureCode failureCode, LocalizedString message) : base(message)
		{
			EnumValidator.ThrowIfInvalid<RightsManagementFailureCode>(failureCode, "failureCode");
			this.FailureCode = failureCode;
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x00130B0A File Offset: 0x0012ED0A
		public RightsManagementPermanentException(LocalizedString message, LocalizedException innerException) : base(message, innerException)
		{
			this.FailureCode = RightsManagementPermanentException.GetRightsManagementFailureCode(innerException);
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00130B20 File Offset: 0x0012ED20
		protected RightsManagementPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.FailureCode = (RightsManagementFailureCode)info.GetValue("failureCode", typeof(RightsManagementFailureCode));
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x00130B4A File Offset: 0x0012ED4A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failureCode", this.FailureCode);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00130B6C File Offset: 0x0012ED6C
		private static RightsManagementFailureCode GetRightsManagementFailureCode(Exception exception)
		{
			if (exception is InvalidRpmsgFormatException)
			{
				return RightsManagementFailureCode.CorruptData;
			}
			RightsManagementException ex = exception as RightsManagementException;
			if (exception is AttachmentProtectionException)
			{
				ex = (exception.InnerException as RightsManagementException);
				if (ex == null)
				{
					return RightsManagementFailureCode.CorruptData;
				}
			}
			if (ex != null)
			{
				return ex.FailureCode;
			}
			return RightsManagementFailureCode.UnknownFailure;
		}

		// Token: 0x04002740 RID: 10048
		private const string FailureCodeLabel = "failureCode";
	}
}
