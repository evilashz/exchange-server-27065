using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ADF RID: 2783
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlsTenantNotFoundException : TenantNotFoundException
	{
		// Token: 0x06008114 RID: 33044 RVA: 0x001A62A4 File Offset: 0x001A44A4
		public GlsTenantNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008115 RID: 33045 RVA: 0x001A62AD File Offset: 0x001A44AD
		public GlsTenantNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008116 RID: 33046 RVA: 0x001A62B7 File Offset: 0x001A44B7
		protected GlsTenantNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008117 RID: 33047 RVA: 0x001A62C1 File Offset: 0x001A44C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
