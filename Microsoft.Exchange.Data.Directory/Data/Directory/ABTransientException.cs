using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A62 RID: 2658
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ABTransientException : DataSourceTransientException
	{
		// Token: 0x06007EE5 RID: 32485 RVA: 0x001A3BE3 File Offset: 0x001A1DE3
		public ABTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x001A3BEC File Offset: 0x001A1DEC
		public ABTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x001A3BF6 File Offset: 0x001A1DF6
		protected ABTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EE8 RID: 32488 RVA: 0x001A3C00 File Offset: 0x001A1E00
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
