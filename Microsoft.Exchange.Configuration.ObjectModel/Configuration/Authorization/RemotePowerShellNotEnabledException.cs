using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002D7 RID: 727
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemotePowerShellNotEnabledException : AuthorizationException
	{
		// Token: 0x06001999 RID: 6553 RVA: 0x0005D56D File Offset: 0x0005B76D
		public RemotePowerShellNotEnabledException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0005D576 File Offset: 0x0005B776
		public RemotePowerShellNotEnabledException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0005D580 File Offset: 0x0005B780
		protected RemotePowerShellNotEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005D58A File Offset: 0x0005B78A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
