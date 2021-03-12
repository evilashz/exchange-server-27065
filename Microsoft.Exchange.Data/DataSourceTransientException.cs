using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D9 RID: 217
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataSourceTransientException : TransientException
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x0001A6DE File Offset: 0x000188DE
		public DataSourceTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001A6E7 File Offset: 0x000188E7
		public DataSourceTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001A6F1 File Offset: 0x000188F1
		protected DataSourceTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001A6FB File Offset: 0x000188FB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
