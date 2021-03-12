using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x0200005B RID: 91
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainServicesHelperConfigException : DomainServicesHelperException
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x000070DF File Offset: 0x000052DF
		public DomainServicesHelperConfigException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000070E8 File Offset: 0x000052E8
		public DomainServicesHelperConfigException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000070F2 File Offset: 0x000052F2
		protected DomainServicesHelperConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000070FC File Offset: 0x000052FC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
