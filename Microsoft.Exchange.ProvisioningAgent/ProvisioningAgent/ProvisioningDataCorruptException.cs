using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000065 RID: 101
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProvisioningDataCorruptException : ProvisioningException
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0001151A File Offset: 0x0000F71A
		public ProvisioningDataCorruptException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00011523 File Offset: 0x0000F723
		public ProvisioningDataCorruptException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001152D File Offset: 0x0000F72D
		protected ProvisioningDataCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011537 File Offset: 0x0000F737
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
