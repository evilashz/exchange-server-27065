using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020002C7 RID: 711
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TranslatableProvisioningException : ProvisioningException
	{
		// Token: 0x0600194C RID: 6476 RVA: 0x0005CEBE File Offset: 0x0005B0BE
		public TranslatableProvisioningException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0005CEC7 File Offset: 0x0005B0C7
		public TranslatableProvisioningException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005CED1 File Offset: 0x0005B0D1
		protected TranslatableProvisioningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0005CEDB File Offset: 0x0005B0DB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
