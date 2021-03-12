using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F4 RID: 4340
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToAccessIrmConfigurationException : LocalizedException
	{
		// Token: 0x0600B3AF RID: 45999 RVA: 0x0029B938 File Offset: 0x00299B38
		public FailedToAccessIrmConfigurationException() : base(Strings.FailedToAccessIrmConfiguration)
		{
		}

		// Token: 0x0600B3B0 RID: 46000 RVA: 0x0029B945 File Offset: 0x00299B45
		public FailedToAccessIrmConfigurationException(Exception innerException) : base(Strings.FailedToAccessIrmConfiguration, innerException)
		{
		}

		// Token: 0x0600B3B1 RID: 46001 RVA: 0x0029B953 File Offset: 0x00299B53
		protected FailedToAccessIrmConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B3B2 RID: 46002 RVA: 0x0029B95D File Offset: 0x00299B5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
