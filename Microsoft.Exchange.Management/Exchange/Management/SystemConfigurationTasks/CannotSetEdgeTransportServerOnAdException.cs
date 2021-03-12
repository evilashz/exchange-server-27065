using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F6E RID: 3950
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSetEdgeTransportServerOnAdException : LocalizedException
	{
		// Token: 0x0600AC14 RID: 44052 RVA: 0x0028FE60 File Offset: 0x0028E060
		public CannotSetEdgeTransportServerOnAdException() : base(Strings.CannotSetEdgeTransportServerOnAd)
		{
		}

		// Token: 0x0600AC15 RID: 44053 RVA: 0x0028FE6D File Offset: 0x0028E06D
		public CannotSetEdgeTransportServerOnAdException(Exception innerException) : base(Strings.CannotSetEdgeTransportServerOnAd, innerException)
		{
		}

		// Token: 0x0600AC16 RID: 44054 RVA: 0x0028FE7B File Offset: 0x0028E07B
		protected CannotSetEdgeTransportServerOnAdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC17 RID: 44055 RVA: 0x0028FE85 File Offset: 0x0028E085
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
