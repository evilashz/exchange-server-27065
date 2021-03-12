using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A79 RID: 2681
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADExternalException : ADOperationException
	{
		// Token: 0x06007F41 RID: 32577 RVA: 0x001A3F64 File Offset: 0x001A2164
		public ADExternalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x001A3F6D File Offset: 0x001A216D
		public ADExternalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x001A3F77 File Offset: 0x001A2177
		protected ADExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F44 RID: 32580 RVA: 0x001A3F81 File Offset: 0x001A2181
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
