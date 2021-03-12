using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD3 RID: 2771
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantNotFoundException : PermanentGlobalException
	{
		// Token: 0x060080E4 RID: 32996 RVA: 0x001A60C0 File Offset: 0x001A42C0
		public TenantNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080E5 RID: 32997 RVA: 0x001A60C9 File Offset: 0x001A42C9
		public TenantNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080E6 RID: 32998 RVA: 0x001A60D3 File Offset: 0x001A42D3
		protected TenantNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080E7 RID: 32999 RVA: 0x001A60DD File Offset: 0x001A42DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
