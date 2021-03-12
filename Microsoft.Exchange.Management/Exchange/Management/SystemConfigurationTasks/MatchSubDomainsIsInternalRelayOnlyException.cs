using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F6D RID: 3949
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MatchSubDomainsIsInternalRelayOnlyException : LocalizedException
	{
		// Token: 0x0600AC10 RID: 44048 RVA: 0x0028FE31 File Offset: 0x0028E031
		public MatchSubDomainsIsInternalRelayOnlyException() : base(Strings.MatchSubDomainsIsInternalRelayOnly)
		{
		}

		// Token: 0x0600AC11 RID: 44049 RVA: 0x0028FE3E File Offset: 0x0028E03E
		public MatchSubDomainsIsInternalRelayOnlyException(Exception innerException) : base(Strings.MatchSubDomainsIsInternalRelayOnly, innerException)
		{
		}

		// Token: 0x0600AC12 RID: 44050 RVA: 0x0028FE4C File Offset: 0x0028E04C
		protected MatchSubDomainsIsInternalRelayOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC13 RID: 44051 RVA: 0x0028FE56 File Offset: 0x0028E056
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
