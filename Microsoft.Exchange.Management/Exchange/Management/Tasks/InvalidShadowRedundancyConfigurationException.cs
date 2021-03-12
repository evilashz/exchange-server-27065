using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC7 RID: 4039
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidShadowRedundancyConfigurationException : LocalizedException
	{
		// Token: 0x0600ADBA RID: 44474 RVA: 0x00292239 File Offset: 0x00290439
		public InvalidShadowRedundancyConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600ADBB RID: 44475 RVA: 0x00292242 File Offset: 0x00290442
		public InvalidShadowRedundancyConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600ADBC RID: 44476 RVA: 0x0029224C File Offset: 0x0029044C
		protected InvalidShadowRedundancyConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADBD RID: 44477 RVA: 0x00292256 File Offset: 0x00290456
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
