using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A61 RID: 2657
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADWriteDisabledException : ADTransientException
	{
		// Token: 0x06007EE1 RID: 32481 RVA: 0x001A3BBC File Offset: 0x001A1DBC
		public ADWriteDisabledException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007EE2 RID: 32482 RVA: 0x001A3BC5 File Offset: 0x001A1DC5
		public ADWriteDisabledException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x001A3BCF File Offset: 0x001A1DCF
		protected ADWriteDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x001A3BD9 File Offset: 0x001A1DD9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
