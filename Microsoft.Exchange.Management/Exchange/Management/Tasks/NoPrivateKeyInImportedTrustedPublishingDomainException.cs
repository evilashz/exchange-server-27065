using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010DA RID: 4314
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoPrivateKeyInImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B333 RID: 45875 RVA: 0x0029AE5C File Offset: 0x0029905C
		public NoPrivateKeyInImportedTrustedPublishingDomainException() : base(Strings.NoPrivateKeyInImportedTrustedPublishingDomain)
		{
		}

		// Token: 0x0600B334 RID: 45876 RVA: 0x0029AE69 File Offset: 0x00299069
		public NoPrivateKeyInImportedTrustedPublishingDomainException(Exception innerException) : base(Strings.NoPrivateKeyInImportedTrustedPublishingDomain, innerException)
		{
		}

		// Token: 0x0600B335 RID: 45877 RVA: 0x0029AE77 File Offset: 0x00299077
		protected NoPrivateKeyInImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B336 RID: 45878 RVA: 0x0029AE81 File Offset: 0x00299081
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
