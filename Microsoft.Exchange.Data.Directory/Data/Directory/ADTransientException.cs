using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000079 RID: 121
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADTransientException : DataSourceTransientException
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x0001FDA4 File Offset: 0x0001DFA4
		public ADTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001FDAD File Offset: 0x0001DFAD
		public ADTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001FDB7 File Offset: 0x0001DFB7
		protected ADTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001FDC1 File Offset: 0x0001DFC1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
