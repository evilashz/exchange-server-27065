using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A7A RID: 2682
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotDetermineDataSessionTypeException : DataSourceOperationException
	{
		// Token: 0x06007F45 RID: 32581 RVA: 0x001A3F8B File Offset: 0x001A218B
		public CannotDetermineDataSessionTypeException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F46 RID: 32582 RVA: 0x001A3F94 File Offset: 0x001A2194
		public CannotDetermineDataSessionTypeException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F47 RID: 32583 RVA: 0x001A3F9E File Offset: 0x001A219E
		protected CannotDetermineDataSessionTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F48 RID: 32584 RVA: 0x001A3FA8 File Offset: 0x001A21A8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
