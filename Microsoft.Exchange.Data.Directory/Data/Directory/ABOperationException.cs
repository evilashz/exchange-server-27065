using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A64 RID: 2660
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ABOperationException : DataSourceOperationException
	{
		// Token: 0x06007EED RID: 32493 RVA: 0x001A3C31 File Offset: 0x001A1E31
		public ABOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EEE RID: 32494 RVA: 0x001A3C3A File Offset: 0x001A1E3A
		public ABOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x001A3C44 File Offset: 0x001A1E44
		protected ABOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x001A3C4E File Offset: 0x001A1E4E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
