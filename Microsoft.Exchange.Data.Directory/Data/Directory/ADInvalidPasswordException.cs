using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A77 RID: 2679
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADInvalidPasswordException : ADOperationException
	{
		// Token: 0x06007F39 RID: 32569 RVA: 0x001A3F16 File Offset: 0x001A2116
		public ADInvalidPasswordException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F3A RID: 32570 RVA: 0x001A3F1F File Offset: 0x001A211F
		public ADInvalidPasswordException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F3B RID: 32571 RVA: 0x001A3F29 File Offset: 0x001A2129
		protected ADInvalidPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F3C RID: 32572 RVA: 0x001A3F33 File Offset: 0x001A2133
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
