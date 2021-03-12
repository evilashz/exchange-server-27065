using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x0200059A RID: 1434
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoMonitoringAccountsAvailableException : LocalizedException
	{
		// Token: 0x0600269F RID: 9887 RVA: 0x000DDC2A File Offset: 0x000DBE2A
		public NoMonitoringAccountsAvailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000DDC33 File Offset: 0x000DBE33
		public NoMonitoringAccountsAvailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000DDC3D File Offset: 0x000DBE3D
		protected NoMonitoringAccountsAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000DDC47 File Offset: 0x000DBE47
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
