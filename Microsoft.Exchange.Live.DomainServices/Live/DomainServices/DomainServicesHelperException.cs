using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200005A RID: 90
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainServicesHelperException : LocalizedException
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x000070B8 File Offset: 0x000052B8
		public DomainServicesHelperException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000070C1 File Offset: 0x000052C1
		public DomainServicesHelperException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000070CB File Offset: 0x000052CB
		protected DomainServicesHelperException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000070D5 File Offset: 0x000052D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
