using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000972 RID: 2418
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class BadDRMPropsSignatureException : RightsManagementException
	{
		// Token: 0x06003455 RID: 13397 RVA: 0x00080AED File Offset: 0x0007ECED
		public BadDRMPropsSignatureException() : this(DrmStrings.BadDRMPropsSignature)
		{
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00080AFA File Offset: 0x0007ECFA
		public BadDRMPropsSignatureException(LocalizedString message) : this(message, null)
		{
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00080B04 File Offset: 0x0007ED04
		public BadDRMPropsSignatureException(LocalizedString message, Exception innerException) : base(RightsManagementFailureCode.BadDRMPropsSignature, message, innerException)
		{
		}
	}
}
