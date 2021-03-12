using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001D3 RID: 467
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptedPAAStoreException : LocalizedException
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x000362F0 File Offset: 0x000344F0
		public CorruptedPAAStoreException() : base(Strings.CorruptedPAAStore)
		{
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000362FD File Offset: 0x000344FD
		public CorruptedPAAStoreException(Exception innerException) : base(Strings.CorruptedPAAStore, innerException)
		{
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003630B File Offset: 0x0003450B
		protected CorruptedPAAStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00036315 File Offset: 0x00034515
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
