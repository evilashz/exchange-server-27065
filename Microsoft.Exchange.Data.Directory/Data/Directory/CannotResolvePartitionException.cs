using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A7C RID: 2684
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolvePartitionException : DataSourceOperationException
	{
		// Token: 0x06007F4D RID: 32589 RVA: 0x001A3FD9 File Offset: 0x001A21D9
		public CannotResolvePartitionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F4E RID: 32590 RVA: 0x001A3FE2 File Offset: 0x001A21E2
		public CannotResolvePartitionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x001A3FEC File Offset: 0x001A21EC
		protected CannotResolvePartitionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x001A3FF6 File Offset: 0x001A21F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
