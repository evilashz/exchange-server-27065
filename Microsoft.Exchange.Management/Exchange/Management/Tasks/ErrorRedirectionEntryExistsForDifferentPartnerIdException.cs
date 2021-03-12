using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010AB RID: 4267
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorRedirectionEntryExistsForDifferentPartnerIdException : LocalizedException
	{
		// Token: 0x0600B24E RID: 45646 RVA: 0x0029997D File Offset: 0x00297B7D
		public ErrorRedirectionEntryExistsForDifferentPartnerIdException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B24F RID: 45647 RVA: 0x00299986 File Offset: 0x00297B86
		public ErrorRedirectionEntryExistsForDifferentPartnerIdException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B250 RID: 45648 RVA: 0x00299990 File Offset: 0x00297B90
		protected ErrorRedirectionEntryExistsForDifferentPartnerIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B251 RID: 45649 RVA: 0x0029999A File Offset: 0x00297B9A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
