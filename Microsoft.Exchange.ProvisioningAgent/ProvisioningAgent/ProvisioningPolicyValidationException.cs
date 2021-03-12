using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000066 RID: 102
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProvisioningPolicyValidationException : ProvisioningDataCorruptException
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00011541 File Offset: 0x0000F741
		public ProvisioningPolicyValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001154A File Offset: 0x0000F74A
		public ProvisioningPolicyValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011554 File Offset: 0x0000F754
		protected ProvisioningPolicyValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001155E File Offset: 0x0000F75E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
