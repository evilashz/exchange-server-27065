using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF7 RID: 3575
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSpecifyBothAllDomainsAndDomainException : LocalizedException
	{
		// Token: 0x0600A4D0 RID: 42192 RVA: 0x00284FD9 File Offset: 0x002831D9
		public CannotSpecifyBothAllDomainsAndDomainException() : base(Strings.CannotSpecifyBothAllDomainsAndDomain)
		{
		}

		// Token: 0x0600A4D1 RID: 42193 RVA: 0x00284FE6 File Offset: 0x002831E6
		public CannotSpecifyBothAllDomainsAndDomainException(Exception innerException) : base(Strings.CannotSpecifyBothAllDomainsAndDomain, innerException)
		{
		}

		// Token: 0x0600A4D2 RID: 42194 RVA: 0x00284FF4 File Offset: 0x002831F4
		protected CannotSpecifyBothAllDomainsAndDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A4D3 RID: 42195 RVA: 0x00284FFE File Offset: 0x002831FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
