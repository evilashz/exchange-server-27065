using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002D5 RID: 725
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ImpersonationDeniedException : AuthorizationException
	{
		// Token: 0x06001991 RID: 6545 RVA: 0x0005D51F File Offset: 0x0005B71F
		public ImpersonationDeniedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0005D528 File Offset: 0x0005B728
		public ImpersonationDeniedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0005D532 File Offset: 0x0005B732
		protected ImpersonationDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0005D53C File Offset: 0x0005B73C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
