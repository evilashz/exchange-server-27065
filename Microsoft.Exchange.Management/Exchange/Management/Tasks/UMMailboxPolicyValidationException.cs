using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F3C RID: 3900
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMMailboxPolicyValidationException : LocalizedException
	{
		// Token: 0x0600AB27 RID: 43815 RVA: 0x0028EA89 File Offset: 0x0028CC89
		public UMMailboxPolicyValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AB28 RID: 43816 RVA: 0x0028EA92 File Offset: 0x0028CC92
		public UMMailboxPolicyValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AB29 RID: 43817 RVA: 0x0028EA9C File Offset: 0x0028CC9C
		protected UMMailboxPolicyValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB2A RID: 43818 RVA: 0x0028EAA6 File Offset: 0x0028CCA6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
