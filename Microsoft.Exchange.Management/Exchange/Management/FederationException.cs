using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02000335 RID: 821
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FederationException : LocalizedException
	{
		// Token: 0x06001BDC RID: 7132 RVA: 0x0007C361 File Offset: 0x0007A561
		public FederationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0007C36A File Offset: 0x0007A56A
		public FederationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0007C374 File Offset: 0x0007A574
		protected FederationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0007C37E File Offset: 0x0007A57E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
