using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002C8 RID: 712
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidOperationInDehydratedContextException : LocalizedException
	{
		// Token: 0x06001950 RID: 6480 RVA: 0x0005CEE5 File Offset: 0x0005B0E5
		public InvalidOperationInDehydratedContextException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0005CEEE File Offset: 0x0005B0EE
		public InvalidOperationInDehydratedContextException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0005CEF8 File Offset: 0x0005B0F8
		protected InvalidOperationInDehydratedContextException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0005CF02 File Offset: 0x0005B102
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
