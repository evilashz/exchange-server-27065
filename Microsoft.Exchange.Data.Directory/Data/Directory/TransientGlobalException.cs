using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AD2 RID: 2770
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TransientGlobalException : DataSourceTransientException
	{
		// Token: 0x060080E0 RID: 32992 RVA: 0x001A6099 File Offset: 0x001A4299
		public TransientGlobalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060080E1 RID: 32993 RVA: 0x001A60A2 File Offset: 0x001A42A2
		public TransientGlobalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060080E2 RID: 32994 RVA: 0x001A60AC File Offset: 0x001A42AC
		protected TransientGlobalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080E3 RID: 32995 RVA: 0x001A60B6 File Offset: 0x001A42B6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
