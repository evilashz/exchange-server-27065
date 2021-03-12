using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002D6 RID: 726
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AppPasswordLoginException : AuthorizationException
	{
		// Token: 0x06001995 RID: 6549 RVA: 0x0005D546 File Offset: 0x0005B746
		public AppPasswordLoginException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0005D54F File Offset: 0x0005B74F
		public AppPasswordLoginException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0005D559 File Offset: 0x0005B759
		protected AppPasswordLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0005D563 File Offset: 0x0005B763
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
