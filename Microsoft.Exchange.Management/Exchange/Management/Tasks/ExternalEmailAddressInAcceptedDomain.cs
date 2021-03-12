using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E4D RID: 3661
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExternalEmailAddressInAcceptedDomain : RecipientTaskException
	{
		// Token: 0x0600A67B RID: 42619 RVA: 0x002877F2 File Offset: 0x002859F2
		public ExternalEmailAddressInAcceptedDomain(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A67C RID: 42620 RVA: 0x002877FB File Offset: 0x002859FB
		public ExternalEmailAddressInAcceptedDomain(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A67D RID: 42621 RVA: 0x00287805 File Offset: 0x00285A05
		protected ExternalEmailAddressInAcceptedDomain(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A67E RID: 42622 RVA: 0x0028780F File Offset: 0x00285A0F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
