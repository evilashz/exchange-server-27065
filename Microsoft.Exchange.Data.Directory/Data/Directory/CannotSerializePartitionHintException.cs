using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A82 RID: 2690
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSerializePartitionHintException : DataSourceOperationException
	{
		// Token: 0x06007F65 RID: 32613 RVA: 0x001A40C3 File Offset: 0x001A22C3
		public CannotSerializePartitionHintException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x001A40CC File Offset: 0x001A22CC
		public CannotSerializePartitionHintException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F67 RID: 32615 RVA: 0x001A40D6 File Offset: 0x001A22D6
		protected CannotSerializePartitionHintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F68 RID: 32616 RVA: 0x001A40E0 File Offset: 0x001A22E0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
