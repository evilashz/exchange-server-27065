using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002AF RID: 687
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotAcceptedDomainException : LocalizedException
	{
		// Token: 0x060018E0 RID: 6368 RVA: 0x0005C714 File Offset: 0x0005A914
		public NotAcceptedDomainException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005C71D File Offset: 0x0005A91D
		public NotAcceptedDomainException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005C727 File Offset: 0x0005A927
		protected NotAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005C731 File Offset: 0x0005A931
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
