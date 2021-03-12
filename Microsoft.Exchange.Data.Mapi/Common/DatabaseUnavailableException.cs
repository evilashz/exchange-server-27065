using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000049 RID: 73
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseUnavailableException : MapiTransientException
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000E415 File Offset: 0x0000C615
		public DatabaseUnavailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E41E File Offset: 0x0000C61E
		public DatabaseUnavailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E428 File Offset: 0x0000C628
		protected DatabaseUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E432 File Offset: 0x0000C632
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
