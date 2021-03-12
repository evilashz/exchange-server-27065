using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200005D RID: 93
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPartnerAccessException : WLCDPartnerException
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000712D File Offset: 0x0000532D
		public WLCDPartnerAccessException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00007136 File Offset: 0x00005336
		public WLCDPartnerAccessException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00007140 File Offset: 0x00005340
		protected WLCDPartnerAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000714A File Offset: 0x0000534A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
