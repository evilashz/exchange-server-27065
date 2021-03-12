using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200002D RID: 45
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClusterException : TransientException
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000084C3 File Offset: 0x000066C3
		public ClusterException(string clusterError) : base(Strings.ClusterException(clusterError))
		{
			this.clusterError = clusterError;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000084D8 File Offset: 0x000066D8
		public ClusterException(string clusterError, Exception innerException) : base(Strings.ClusterException(clusterError), innerException)
		{
			this.clusterError = clusterError;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000084EE File Offset: 0x000066EE
		protected ClusterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterError = (string)info.GetValue("clusterError", typeof(string));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008518 File Offset: 0x00006718
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterError", this.clusterError);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00008533 File Offset: 0x00006733
		public string ClusterError
		{
			get
			{
				return this.clusterError;
			}
		}

		// Token: 0x0400007A RID: 122
		private readonly string clusterError;
	}
}
