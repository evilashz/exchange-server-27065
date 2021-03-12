using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F67 RID: 3943
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CatchAllRecipientNotAllowedForNonAuthoritativeAcceptedDomainsException : LocalizedException
	{
		// Token: 0x0600ABF5 RID: 44021 RVA: 0x0028FC3C File Offset: 0x0028DE3C
		public CatchAllRecipientNotAllowedForNonAuthoritativeAcceptedDomainsException() : base(Strings.CatchAllRecipientNotAllowedForNonAuthoritativeAcceptedDomains)
		{
		}

		// Token: 0x0600ABF6 RID: 44022 RVA: 0x0028FC49 File Offset: 0x0028DE49
		public CatchAllRecipientNotAllowedForNonAuthoritativeAcceptedDomainsException(Exception innerException) : base(Strings.CatchAllRecipientNotAllowedForNonAuthoritativeAcceptedDomains, innerException)
		{
		}

		// Token: 0x0600ABF7 RID: 44023 RVA: 0x0028FC57 File Offset: 0x0028DE57
		protected CatchAllRecipientNotAllowedForNonAuthoritativeAcceptedDomainsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ABF8 RID: 44024 RVA: 0x0028FC61 File Offset: 0x0028DE61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
