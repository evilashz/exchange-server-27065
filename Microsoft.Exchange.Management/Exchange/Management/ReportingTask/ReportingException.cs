using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x0200115C RID: 4444
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReportingException : LocalizedException
	{
		// Token: 0x0600B5B8 RID: 46520 RVA: 0x0029EB81 File Offset: 0x0029CD81
		public ReportingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B5B9 RID: 46521 RVA: 0x0029EB8A File Offset: 0x0029CD8A
		public ReportingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B5BA RID: 46522 RVA: 0x0029EB94 File Offset: 0x0029CD94
		protected ReportingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B5BB RID: 46523 RVA: 0x0029EB9E File Offset: 0x0029CD9E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
