using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200005D RID: 93
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EasMissingOrBadUrlOnRedirectException : ConnectionsTransientException
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00004C1E File Offset: 0x00002E1E
		public EasMissingOrBadUrlOnRedirectException() : base(CXStrings.EasMissingOrBadUrlOnRedirectMsg)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004C2B File Offset: 0x00002E2B
		public EasMissingOrBadUrlOnRedirectException(Exception innerException) : base(CXStrings.EasMissingOrBadUrlOnRedirectMsg, innerException)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00004C39 File Offset: 0x00002E39
		protected EasMissingOrBadUrlOnRedirectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00004C43 File Offset: 0x00002E43
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
