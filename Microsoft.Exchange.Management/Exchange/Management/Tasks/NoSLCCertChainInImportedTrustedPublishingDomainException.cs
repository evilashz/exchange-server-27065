using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010DF RID: 4319
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoSLCCertChainInImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B348 RID: 45896 RVA: 0x0029AF90 File Offset: 0x00299190
		public NoSLCCertChainInImportedTrustedPublishingDomainException() : base(Strings.NoSLCCertChainInImportedTrustedPublishingDomain)
		{
		}

		// Token: 0x0600B349 RID: 45897 RVA: 0x0029AF9D File Offset: 0x0029919D
		public NoSLCCertChainInImportedTrustedPublishingDomainException(Exception innerException) : base(Strings.NoSLCCertChainInImportedTrustedPublishingDomain, innerException)
		{
		}

		// Token: 0x0600B34A RID: 45898 RVA: 0x0029AFAB File Offset: 0x002991AB
		protected NoSLCCertChainInImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B34B RID: 45899 RVA: 0x0029AFB5 File Offset: 0x002991B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
