using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200003B RID: 59
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TopologyServiceTransientException : DataSourceTransientException
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000F07B File Offset: 0x0000D27B
		public TopologyServiceTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F084 File Offset: 0x0000D284
		public TopologyServiceTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F08E File Offset: 0x0000D28E
		protected TopologyServiceTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F098 File Offset: 0x0000D298
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
