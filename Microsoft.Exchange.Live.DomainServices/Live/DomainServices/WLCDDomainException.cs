using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200005E RID: 94
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDDomainException : WLCDPartnerException
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00007154 File Offset: 0x00005354
		public WLCDDomainException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000715D File Offset: 0x0000535D
		public WLCDDomainException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00007167 File Offset: 0x00005367
		protected WLCDDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00007171 File Offset: 0x00005371
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
