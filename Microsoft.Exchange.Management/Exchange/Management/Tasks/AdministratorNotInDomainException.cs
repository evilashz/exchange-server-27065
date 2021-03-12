using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200109F RID: 4255
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdministratorNotInDomainException : LocalizedException
	{
		// Token: 0x0600B21C RID: 45596 RVA: 0x002996C2 File Offset: 0x002978C2
		public AdministratorNotInDomainException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B21D RID: 45597 RVA: 0x002996CB File Offset: 0x002978CB
		public AdministratorNotInDomainException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B21E RID: 45598 RVA: 0x002996D5 File Offset: 0x002978D5
		protected AdministratorNotInDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B21F RID: 45599 RVA: 0x002996DF File Offset: 0x002978DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
