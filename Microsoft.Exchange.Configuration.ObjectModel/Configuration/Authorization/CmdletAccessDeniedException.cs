using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002D4 RID: 724
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CmdletAccessDeniedException : AuthorizationException
	{
		// Token: 0x0600198D RID: 6541 RVA: 0x0005D4F8 File Offset: 0x0005B6F8
		public CmdletAccessDeniedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0005D501 File Offset: 0x0005B701
		public CmdletAccessDeniedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0005D50B File Offset: 0x0005B70B
		protected CmdletAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0005D515 File Offset: 0x0005B715
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
