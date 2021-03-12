using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F66 RID: 3942
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExternalRelayDomainsAreNotAllowedInDatacenterAndFfoException : LocalizedException
	{
		// Token: 0x0600ABF1 RID: 44017 RVA: 0x0028FC0D File Offset: 0x0028DE0D
		public ExternalRelayDomainsAreNotAllowedInDatacenterAndFfoException() : base(Strings.ExternalRelayDomainsAreNotAllowedInDatacenterAndFfo)
		{
		}

		// Token: 0x0600ABF2 RID: 44018 RVA: 0x0028FC1A File Offset: 0x0028DE1A
		public ExternalRelayDomainsAreNotAllowedInDatacenterAndFfoException(Exception innerException) : base(Strings.ExternalRelayDomainsAreNotAllowedInDatacenterAndFfo, innerException)
		{
		}

		// Token: 0x0600ABF3 RID: 44019 RVA: 0x0028FC28 File Offset: 0x0028DE28
		protected ExternalRelayDomainsAreNotAllowedInDatacenterAndFfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ABF4 RID: 44020 RVA: 0x0028FC32 File Offset: 0x0028DE32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
