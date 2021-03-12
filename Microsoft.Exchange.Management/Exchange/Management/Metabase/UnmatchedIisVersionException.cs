using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FA6 RID: 4006
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnmatchedIisVersionException : DataSourceOperationException
	{
		// Token: 0x0600AD08 RID: 44296 RVA: 0x00290E57 File Offset: 0x0028F057
		public UnmatchedIisVersionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600AD09 RID: 44297 RVA: 0x00290E60 File Offset: 0x0028F060
		public UnmatchedIisVersionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600AD0A RID: 44298 RVA: 0x00290E6A File Offset: 0x0028F06A
		protected UnmatchedIisVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD0B RID: 44299 RVA: 0x00290E74 File Offset: 0x0028F074
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
