using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E46 RID: 3654
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeNotAuthorizedForDomainException : RecipientTaskException
	{
		// Token: 0x0600A65F RID: 42591 RVA: 0x002876E1 File Offset: 0x002858E1
		public ExchangeNotAuthorizedForDomainException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A660 RID: 42592 RVA: 0x002876EA File Offset: 0x002858EA
		public ExchangeNotAuthorizedForDomainException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A661 RID: 42593 RVA: 0x002876F4 File Offset: 0x002858F4
		protected ExchangeNotAuthorizedForDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A662 RID: 42594 RVA: 0x002876FE File Offset: 0x002858FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
