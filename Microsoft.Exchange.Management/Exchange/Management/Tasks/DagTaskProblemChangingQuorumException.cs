using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200106A RID: 4202
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskProblemChangingQuorumException : LocalizedException
	{
		// Token: 0x0600B0EB RID: 45291 RVA: 0x002970E6 File Offset: 0x002952E6
		public DagTaskProblemChangingQuorumException(string clusterName, Exception ex) : base(Strings.DagTaskProblemChangingQuorumException(clusterName, ex))
		{
			this.clusterName = clusterName;
			this.ex = ex;
		}

		// Token: 0x0600B0EC RID: 45292 RVA: 0x00297103 File Offset: 0x00295303
		public DagTaskProblemChangingQuorumException(string clusterName, Exception ex, Exception innerException) : base(Strings.DagTaskProblemChangingQuorumException(clusterName, ex), innerException)
		{
			this.clusterName = clusterName;
			this.ex = ex;
		}

		// Token: 0x0600B0ED RID: 45293 RVA: 0x00297124 File Offset: 0x00295324
		protected DagTaskProblemChangingQuorumException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B0EE RID: 45294 RVA: 0x00297179 File Offset: 0x00295379
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003860 RID: 14432
		// (get) Token: 0x0600B0EF RID: 45295 RVA: 0x002971A5 File Offset: 0x002953A5
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17003861 RID: 14433
		// (get) Token: 0x0600B0F0 RID: 45296 RVA: 0x002971AD File Offset: 0x002953AD
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061C6 RID: 25030
		private readonly string clusterName;

		// Token: 0x040061C7 RID: 25031
		private readonly Exception ex;
	}
}
