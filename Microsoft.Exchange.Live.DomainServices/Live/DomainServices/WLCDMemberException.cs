using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200005F RID: 95
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDMemberException : WLCDPartnerException
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000717B File Offset: 0x0000537B
		public WLCDMemberException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00007184 File Offset: 0x00005384
		public WLCDMemberException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000718E File Offset: 0x0000538E
		protected WLCDMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00007198 File Offset: 0x00005398
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
