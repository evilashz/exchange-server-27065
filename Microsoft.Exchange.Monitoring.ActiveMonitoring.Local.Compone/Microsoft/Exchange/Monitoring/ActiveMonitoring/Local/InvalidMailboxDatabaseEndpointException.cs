using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x02000598 RID: 1432
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidMailboxDatabaseEndpointException : LocalizedException
	{
		// Token: 0x06002697 RID: 9879 RVA: 0x000DDBDC File Offset: 0x000DBDDC
		public InvalidMailboxDatabaseEndpointException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000DDBE5 File Offset: 0x000DBDE5
		public InvalidMailboxDatabaseEndpointException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000DDBEF File Offset: 0x000DBDEF
		protected InvalidMailboxDatabaseEndpointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000DDBF9 File Offset: 0x000DBDF9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
