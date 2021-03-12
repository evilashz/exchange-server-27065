using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.RightsManagementServices.Core;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E0 RID: 4320
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToValidateSLCCertChainException : LocalizedException
	{
		// Token: 0x0600B34C RID: 45900 RVA: 0x0029AFBF File Offset: 0x002991BF
		public FailedToValidateSLCCertChainException(WellKnownErrorCode eCode) : base(Strings.FailedToValidateSLCCertChain(eCode))
		{
			this.eCode = eCode;
		}

		// Token: 0x0600B34D RID: 45901 RVA: 0x0029AFD4 File Offset: 0x002991D4
		public FailedToValidateSLCCertChainException(WellKnownErrorCode eCode, Exception innerException) : base(Strings.FailedToValidateSLCCertChain(eCode), innerException)
		{
			this.eCode = eCode;
		}

		// Token: 0x0600B34E RID: 45902 RVA: 0x0029AFEA File Offset: 0x002991EA
		protected FailedToValidateSLCCertChainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.eCode = (WellKnownErrorCode)info.GetValue("eCode", typeof(WellKnownErrorCode));
		}

		// Token: 0x0600B34F RID: 45903 RVA: 0x0029B014 File Offset: 0x00299214
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("eCode", this.eCode);
		}

		// Token: 0x170038E9 RID: 14569
		// (get) Token: 0x0600B350 RID: 45904 RVA: 0x0029B034 File Offset: 0x00299234
		public WellKnownErrorCode ECode
		{
			get
			{
				return this.eCode;
			}
		}

		// Token: 0x0400624F RID: 25167
		private readonly WellKnownErrorCode eCode;
	}
}
