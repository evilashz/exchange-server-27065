using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000FA2 RID: 4002
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultiSiteSourceServersException : LocalizedException
	{
		// Token: 0x0600ACF7 RID: 44279 RVA: 0x00290D62 File Offset: 0x0028EF62
		public MultiSiteSourceServersException() : base(Strings.WarningMultiSiteSourceServers)
		{
		}

		// Token: 0x0600ACF8 RID: 44280 RVA: 0x00290D6F File Offset: 0x0028EF6F
		public MultiSiteSourceServersException(Exception innerException) : base(Strings.WarningMultiSiteSourceServers, innerException)
		{
		}

		// Token: 0x0600ACF9 RID: 44281 RVA: 0x00290D7D File Offset: 0x0028EF7D
		protected MultiSiteSourceServersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ACFA RID: 44282 RVA: 0x00290D87 File Offset: 0x0028EF87
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
