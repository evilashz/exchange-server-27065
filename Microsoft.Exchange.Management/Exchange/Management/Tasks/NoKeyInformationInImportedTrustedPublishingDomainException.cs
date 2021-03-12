using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010DC RID: 4316
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoKeyInformationInImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B33C RID: 45884 RVA: 0x0029AF03 File Offset: 0x00299103
		public NoKeyInformationInImportedTrustedPublishingDomainException() : base(Strings.NoKeyInformationInImportedTrustedPublishingDomain)
		{
		}

		// Token: 0x0600B33D RID: 45885 RVA: 0x0029AF10 File Offset: 0x00299110
		public NoKeyInformationInImportedTrustedPublishingDomainException(Exception innerException) : base(Strings.NoKeyInformationInImportedTrustedPublishingDomain, innerException)
		{
		}

		// Token: 0x0600B33E RID: 45886 RVA: 0x0029AF1E File Offset: 0x0029911E
		protected NoKeyInformationInImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B33F RID: 45887 RVA: 0x0029AF28 File Offset: 0x00299128
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
