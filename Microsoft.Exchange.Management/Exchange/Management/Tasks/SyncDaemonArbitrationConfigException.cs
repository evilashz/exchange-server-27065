using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001130 RID: 4400
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SyncDaemonArbitrationConfigException : LocalizedException
	{
		// Token: 0x0600B4DB RID: 46299 RVA: 0x0029D633 File Offset: 0x0029B833
		public SyncDaemonArbitrationConfigException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B4DC RID: 46300 RVA: 0x0029D63C File Offset: 0x0029B83C
		public SyncDaemonArbitrationConfigException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B4DD RID: 46301 RVA: 0x0029D646 File Offset: 0x0029B846
		protected SyncDaemonArbitrationConfigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4DE RID: 46302 RVA: 0x0029D650 File Offset: 0x0029B850
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
