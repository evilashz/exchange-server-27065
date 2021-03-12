using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002D9 RID: 729
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UrlInValidException : AuthorizationException
	{
		// Token: 0x060019A1 RID: 6561 RVA: 0x0005D5BB File Offset: 0x0005B7BB
		public UrlInValidException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0005D5C4 File Offset: 0x0005B7C4
		public UrlInValidException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005D5CE File Offset: 0x0005B7CE
		protected UrlInValidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005D5D8 File Offset: 0x0005B7D8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
