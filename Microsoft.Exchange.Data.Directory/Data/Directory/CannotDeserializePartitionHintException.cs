using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A81 RID: 2689
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotDeserializePartitionHintException : DataSourceOperationException
	{
		// Token: 0x06007F61 RID: 32609 RVA: 0x001A409C File Offset: 0x001A229C
		public CannotDeserializePartitionHintException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x001A40A5 File Offset: 0x001A22A5
		public CannotDeserializePartitionHintException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x001A40AF File Offset: 0x001A22AF
		protected CannotDeserializePartitionHintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x001A40B9 File Offset: 0x001A22B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
