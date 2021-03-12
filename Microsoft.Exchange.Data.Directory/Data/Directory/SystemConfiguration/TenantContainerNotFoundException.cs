using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A90 RID: 2704
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantContainerNotFoundException : ADTransientException
	{
		// Token: 0x06007FA2 RID: 32674 RVA: 0x001A4498 File Offset: 0x001A2698
		public TenantContainerNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x001A44A1 File Offset: 0x001A26A1
		public TenantContainerNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007FA4 RID: 32676 RVA: 0x001A44AB File Offset: 0x001A26AB
		protected TenantContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x001A44B5 File Offset: 0x001A26B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
