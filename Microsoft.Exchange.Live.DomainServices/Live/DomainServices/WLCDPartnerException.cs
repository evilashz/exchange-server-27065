using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200005C RID: 92
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDPartnerException : DomainServicesHelperException
	{
		// Token: 0x060002BB RID: 699 RVA: 0x00007106 File Offset: 0x00005306
		public WLCDPartnerException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000710F File Offset: 0x0000530F
		public WLCDPartnerException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00007119 File Offset: 0x00005319
		protected WLCDPartnerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00007123 File Offset: 0x00005323
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
