using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010DD RID: 4317
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoKeyIDInImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B340 RID: 45888 RVA: 0x0029AF32 File Offset: 0x00299132
		public NoKeyIDInImportedTrustedPublishingDomainException() : base(Strings.NoKeyIDInImportedTrustedPublishingDomain)
		{
		}

		// Token: 0x0600B341 RID: 45889 RVA: 0x0029AF3F File Offset: 0x0029913F
		public NoKeyIDInImportedTrustedPublishingDomainException(Exception innerException) : base(Strings.NoKeyIDInImportedTrustedPublishingDomain, innerException)
		{
		}

		// Token: 0x0600B342 RID: 45890 RVA: 0x0029AF4D File Offset: 0x0029914D
		protected NoKeyIDInImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B343 RID: 45891 RVA: 0x0029AF57 File Offset: 0x00299157
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
