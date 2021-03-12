using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F15 RID: 3861
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthCouldNotObtainFqdnException : LocalizedException
	{
		// Token: 0x0600AA59 RID: 43609 RVA: 0x0028D45B File Offset: 0x0028B65B
		public CasHealthCouldNotObtainFqdnException() : base(Strings.CasHealthCouldNotObtainFqdn)
		{
		}

		// Token: 0x0600AA5A RID: 43610 RVA: 0x0028D468 File Offset: 0x0028B668
		public CasHealthCouldNotObtainFqdnException(Exception innerException) : base(Strings.CasHealthCouldNotObtainFqdn, innerException)
		{
		}

		// Token: 0x0600AA5B RID: 43611 RVA: 0x0028D476 File Offset: 0x0028B676
		protected CasHealthCouldNotObtainFqdnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AA5C RID: 43612 RVA: 0x0028D480 File Offset: 0x0028B680
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
