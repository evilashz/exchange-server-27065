using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200015F RID: 351
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidCompliancePolicyExBindingException : InvalidCompliancePolicyBindingException
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x000358CF File Offset: 0x00033ACF
		public InvalidCompliancePolicyExBindingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000358D8 File Offset: 0x00033AD8
		public InvalidCompliancePolicyExBindingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000358E2 File Offset: 0x00033AE2
		protected InvalidCompliancePolicyExBindingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000358EC File Offset: 0x00033AEC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
