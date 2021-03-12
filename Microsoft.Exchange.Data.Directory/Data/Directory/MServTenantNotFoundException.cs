using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD7 RID: 2775
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MServTenantNotFoundException : TenantNotFoundException
	{
		// Token: 0x060080F4 RID: 33012 RVA: 0x001A615C File Offset: 0x001A435C
		public MServTenantNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x001A6165 File Offset: 0x001A4365
		public MServTenantNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x001A616F File Offset: 0x001A436F
		protected MServTenantNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x001A6179 File Offset: 0x001A4379
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
