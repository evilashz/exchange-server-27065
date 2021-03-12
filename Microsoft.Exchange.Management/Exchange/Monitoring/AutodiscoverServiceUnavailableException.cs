using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001104 RID: 4356
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutodiscoverServiceUnavailableException : LocalizedException
	{
		// Token: 0x0600B3FF RID: 46079 RVA: 0x0029C0D9 File Offset: 0x0029A2D9
		public AutodiscoverServiceUnavailableException() : base(Strings.messageAutodiscoverServiceUnavailableException)
		{
		}

		// Token: 0x0600B400 RID: 46080 RVA: 0x0029C0E6 File Offset: 0x0029A2E6
		public AutodiscoverServiceUnavailableException(Exception innerException) : base(Strings.messageAutodiscoverServiceUnavailableException, innerException)
		{
		}

		// Token: 0x0600B401 RID: 46081 RVA: 0x0029C0F4 File Offset: 0x0029A2F4
		protected AutodiscoverServiceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B402 RID: 46082 RVA: 0x0029C0FE File Offset: 0x0029A2FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
