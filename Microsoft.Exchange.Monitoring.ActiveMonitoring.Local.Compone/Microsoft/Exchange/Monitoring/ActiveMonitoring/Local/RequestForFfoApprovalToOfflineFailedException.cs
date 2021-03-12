using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005AB RID: 1451
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestForFfoApprovalToOfflineFailedException : RecoveryActionExceptionCommon
	{
		// Token: 0x060026F1 RID: 9969 RVA: 0x000DE376 File Offset: 0x000DC576
		public RequestForFfoApprovalToOfflineFailedException() : base(Strings.RequestForFfoApprovalToOfflineFailed)
		{
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x000DE388 File Offset: 0x000DC588
		public RequestForFfoApprovalToOfflineFailedException(Exception innerException) : base(Strings.RequestForFfoApprovalToOfflineFailed, innerException)
		{
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x000DE39B File Offset: 0x000DC59B
		protected RequestForFfoApprovalToOfflineFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000DE3A5 File Offset: 0x000DC5A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
