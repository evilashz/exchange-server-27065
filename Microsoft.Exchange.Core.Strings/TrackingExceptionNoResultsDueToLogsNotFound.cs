using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000020 RID: 32
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TrackingExceptionNoResultsDueToLogsNotFound : LocalizedException
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000D300 File Offset: 0x0000B500
		public TrackingExceptionNoResultsDueToLogsNotFound() : base(CoreStrings.TrackingWarningNoResultsDueToLogsNotFound)
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000D30D File Offset: 0x0000B50D
		public TrackingExceptionNoResultsDueToLogsNotFound(Exception innerException) : base(CoreStrings.TrackingWarningNoResultsDueToLogsNotFound, innerException)
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000D31B File Offset: 0x0000B51B
		protected TrackingExceptionNoResultsDueToLogsNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000D325 File Offset: 0x0000B525
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
