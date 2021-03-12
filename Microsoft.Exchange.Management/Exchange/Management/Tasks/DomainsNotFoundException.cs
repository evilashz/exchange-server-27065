using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DFA RID: 3578
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainsNotFoundException : LocalizedException
	{
		// Token: 0x0600A4DF RID: 42207 RVA: 0x0028514D File Offset: 0x0028334D
		public DomainsNotFoundException() : base(Strings.DomainsNotFoundException)
		{
		}

		// Token: 0x0600A4E0 RID: 42208 RVA: 0x0028515A File Offset: 0x0028335A
		public DomainsNotFoundException(Exception innerException) : base(Strings.DomainsNotFoundException, innerException)
		{
		}

		// Token: 0x0600A4E1 RID: 42209 RVA: 0x00285168 File Offset: 0x00283368
		protected DomainsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A4E2 RID: 42210 RVA: 0x00285172 File Offset: 0x00283372
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
