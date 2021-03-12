using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F98 RID: 3992
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorDashdashAddressSpaceNotUniqueException : LocalizedException
	{
		// Token: 0x0600ACCD RID: 44237 RVA: 0x00290AFA File Offset: 0x0028ECFA
		public SendConnectorDashdashAddressSpaceNotUniqueException() : base(Strings.SendConnectorDashdashAddressSpaceNotUnique)
		{
		}

		// Token: 0x0600ACCE RID: 44238 RVA: 0x00290B07 File Offset: 0x0028ED07
		public SendConnectorDashdashAddressSpaceNotUniqueException(Exception innerException) : base(Strings.SendConnectorDashdashAddressSpaceNotUnique, innerException)
		{
		}

		// Token: 0x0600ACCF RID: 44239 RVA: 0x00290B15 File Offset: 0x0028ED15
		protected SendConnectorDashdashAddressSpaceNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACD0 RID: 44240 RVA: 0x00290B1F File Offset: 0x0028ED1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
