using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD4 RID: 2772
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlsDomainNotFoundException : PermanentGlobalException
	{
		// Token: 0x060080E8 RID: 33000 RVA: 0x001A60E7 File Offset: 0x001A42E7
		public GlsDomainNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080E9 RID: 33001 RVA: 0x001A60F0 File Offset: 0x001A42F0
		public GlsDomainNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080EA RID: 33002 RVA: 0x001A60FA File Offset: 0x001A42FA
		protected GlsDomainNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080EB RID: 33003 RVA: 0x001A6104 File Offset: 0x001A4304
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
