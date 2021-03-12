using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B0 RID: 4528
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMMailboxPolicyNotFoundException : LocalizedException
	{
		// Token: 0x0600B85B RID: 47195 RVA: 0x002A478D File Offset: 0x002A298D
		public UMMailboxPolicyNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B85C RID: 47196 RVA: 0x002A4796 File Offset: 0x002A2996
		public UMMailboxPolicyNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B85D RID: 47197 RVA: 0x002A47A0 File Offset: 0x002A29A0
		protected UMMailboxPolicyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B85E RID: 47198 RVA: 0x002A47AA File Offset: 0x002A29AA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
