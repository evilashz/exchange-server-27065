using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000FD2 RID: 4050
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SyncNowAlreadyStartedException : LocalizedException
	{
		// Token: 0x0600ADED RID: 44525 RVA: 0x00292645 File Offset: 0x00290845
		public SyncNowAlreadyStartedException() : base(Strings.SyncNowAlreadyStartedException)
		{
		}

		// Token: 0x0600ADEE RID: 44526 RVA: 0x00292652 File Offset: 0x00290852
		public SyncNowAlreadyStartedException(Exception innerException) : base(Strings.SyncNowAlreadyStartedException, innerException)
		{
		}

		// Token: 0x0600ADEF RID: 44527 RVA: 0x00292660 File Offset: 0x00290860
		protected SyncNowAlreadyStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADF0 RID: 44528 RVA: 0x0029266A File Offset: 0x0029086A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
