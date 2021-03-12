using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001047 RID: 4167
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskRestoreDagCouldNotOpenClusterException : LocalizedException
	{
		// Token: 0x0600B021 RID: 45089 RVA: 0x002957BD File Offset: 0x002939BD
		public DagTaskRestoreDagCouldNotOpenClusterException() : base(Strings.DagTaskRestoreDagCouldNotOpenCluster)
		{
		}

		// Token: 0x0600B022 RID: 45090 RVA: 0x002957CA File Offset: 0x002939CA
		public DagTaskRestoreDagCouldNotOpenClusterException(Exception innerException) : base(Strings.DagTaskRestoreDagCouldNotOpenCluster, innerException)
		{
		}

		// Token: 0x0600B023 RID: 45091 RVA: 0x002957D8 File Offset: 0x002939D8
		protected DagTaskRestoreDagCouldNotOpenClusterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B024 RID: 45092 RVA: 0x002957E2 File Offset: 0x002939E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
