using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F4B RID: 3915
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveDefaultAcceptedDomainException : LocalizedException
	{
		// Token: 0x0600AB71 RID: 43889 RVA: 0x0028F14D File Offset: 0x0028D34D
		public CannotRemoveDefaultAcceptedDomainException() : base(Strings.CannotRemoveDefaultAcceptedDomain)
		{
		}

		// Token: 0x0600AB72 RID: 43890 RVA: 0x0028F15A File Offset: 0x0028D35A
		public CannotRemoveDefaultAcceptedDomainException(Exception innerException) : base(Strings.CannotRemoveDefaultAcceptedDomain, innerException)
		{
		}

		// Token: 0x0600AB73 RID: 43891 RVA: 0x0028F168 File Offset: 0x0028D368
		protected CannotRemoveDefaultAcceptedDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AB74 RID: 43892 RVA: 0x0028F172 File Offset: 0x0028D372
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
