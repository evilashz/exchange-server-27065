using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000024 RID: 36
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CompressionOutOfMemoryException : LocalizedException
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public CompressionOutOfMemoryException() : base(CoreStrings.CompressionOutOfMemory)
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000D3C9 File Offset: 0x0000B5C9
		public CompressionOutOfMemoryException(Exception innerException) : base(CoreStrings.CompressionOutOfMemory, innerException)
		{
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		protected CompressionOutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D3E1 File Offset: 0x0000B5E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
