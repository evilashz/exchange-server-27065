using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A7F RID: 2687
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TenantNameTooLongException : ADOperationException
	{
		// Token: 0x06007F59 RID: 32601 RVA: 0x001A404E File Offset: 0x001A224E
		public TenantNameTooLongException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F5A RID: 32602 RVA: 0x001A4057 File Offset: 0x001A2257
		public TenantNameTooLongException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F5B RID: 32603 RVA: 0x001A4061 File Offset: 0x001A2261
		protected TenantNameTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F5C RID: 32604 RVA: 0x001A406B File Offset: 0x001A226B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
