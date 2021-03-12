using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002DA RID: 730
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DelegatedUserNotInOrgException : AuthorizationException
	{
		// Token: 0x060019A5 RID: 6565 RVA: 0x0005D5E2 File Offset: 0x0005B7E2
		public DelegatedUserNotInOrgException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0005D5EB File Offset: 0x0005B7EB
		public DelegatedUserNotInOrgException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0005D5F5 File Offset: 0x0005B7F5
		protected DelegatedUserNotInOrgException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0005D5FF File Offset: 0x0005B7FF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
