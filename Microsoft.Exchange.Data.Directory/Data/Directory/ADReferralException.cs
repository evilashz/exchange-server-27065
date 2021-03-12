using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A72 RID: 2674
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADReferralException : ADOperationException
	{
		// Token: 0x06007F25 RID: 32549 RVA: 0x001A3E53 File Offset: 0x001A2053
		public ADReferralException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x001A3E5C File Offset: 0x001A205C
		public ADReferralException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x001A3E66 File Offset: 0x001A2066
		protected ADReferralException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x001A3E70 File Offset: 0x001A2070
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
