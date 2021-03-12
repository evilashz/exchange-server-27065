using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200015E RID: 350
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidCompliancePolicyBindingException : LocalizedException
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x000358A8 File Offset: 0x00033AA8
		public InvalidCompliancePolicyBindingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000358B1 File Offset: 0x00033AB1
		public InvalidCompliancePolicyBindingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000358BB File Offset: 0x00033ABB
		protected InvalidCompliancePolicyBindingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000358C5 File Offset: 0x00033AC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
