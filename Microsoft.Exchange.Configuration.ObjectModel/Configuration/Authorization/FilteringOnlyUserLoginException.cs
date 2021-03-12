using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002DB RID: 731
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FilteringOnlyUserLoginException : AuthorizationException
	{
		// Token: 0x060019A9 RID: 6569 RVA: 0x0005D609 File Offset: 0x0005B809
		public FilteringOnlyUserLoginException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0005D612 File Offset: 0x0005B812
		public FilteringOnlyUserLoginException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0005D61C File Offset: 0x0005B81C
		protected FilteringOnlyUserLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0005D626 File Offset: 0x0005B826
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
