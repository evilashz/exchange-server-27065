using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010DE RID: 4318
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoKeyIDTypeInImportedTrustedPublishingDomainException : LocalizedException
	{
		// Token: 0x0600B344 RID: 45892 RVA: 0x0029AF61 File Offset: 0x00299161
		public NoKeyIDTypeInImportedTrustedPublishingDomainException() : base(Strings.NoKeyIDInImportedTrustedPublishingDomain)
		{
		}

		// Token: 0x0600B345 RID: 45893 RVA: 0x0029AF6E File Offset: 0x0029916E
		public NoKeyIDTypeInImportedTrustedPublishingDomainException(Exception innerException) : base(Strings.NoKeyIDInImportedTrustedPublishingDomain, innerException)
		{
		}

		// Token: 0x0600B346 RID: 45894 RVA: 0x0029AF7C File Offset: 0x0029917C
		protected NoKeyIDTypeInImportedTrustedPublishingDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B347 RID: 45895 RVA: 0x0029AF86 File Offset: 0x00299186
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
