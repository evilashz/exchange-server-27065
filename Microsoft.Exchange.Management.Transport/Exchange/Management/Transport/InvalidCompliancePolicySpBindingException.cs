using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000162 RID: 354
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidCompliancePolicySpBindingException : InvalidCompliancePolicyBindingException
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x0003599D File Offset: 0x00033B9D
		public InvalidCompliancePolicySpBindingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000359A6 File Offset: 0x00033BA6
		public InvalidCompliancePolicySpBindingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000359B0 File Offset: 0x00033BB0
		protected InvalidCompliancePolicySpBindingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000359BA File Offset: 0x00033BBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
