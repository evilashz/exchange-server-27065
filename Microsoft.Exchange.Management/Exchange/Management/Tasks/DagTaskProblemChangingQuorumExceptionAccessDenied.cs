using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200106D RID: 4205
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskProblemChangingQuorumExceptionAccessDenied : LocalizedException
	{
		// Token: 0x0600B0FF RID: 45311 RVA: 0x002973E6 File Offset: 0x002955E6
		public DagTaskProblemChangingQuorumExceptionAccessDenied(string clusterName, string fsw, Exception ex) : base(Strings.DagTaskProblemChangingQuorumExceptionAccessDenied(clusterName, fsw, ex))
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B100 RID: 45312 RVA: 0x0029740B File Offset: 0x0029560B
		public DagTaskProblemChangingQuorumExceptionAccessDenied(string clusterName, string fsw, Exception ex, Exception innerException) : base(Strings.DagTaskProblemChangingQuorumExceptionAccessDenied(clusterName, fsw, ex), innerException)
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B101 RID: 45313 RVA: 0x00297434 File Offset: 0x00295634
		protected DagTaskProblemChangingQuorumExceptionAccessDenied(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.fsw = (string)info.GetValue("fsw", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B102 RID: 45314 RVA: 0x002974A9 File Offset: 0x002956A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("fsw", this.fsw);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003868 RID: 14440
		// (get) Token: 0x0600B103 RID: 45315 RVA: 0x002974E6 File Offset: 0x002956E6
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17003869 RID: 14441
		// (get) Token: 0x0600B104 RID: 45316 RVA: 0x002974EE File Offset: 0x002956EE
		public string Fsw
		{
			get
			{
				return this.fsw;
			}
		}

		// Token: 0x1700386A RID: 14442
		// (get) Token: 0x0600B105 RID: 45317 RVA: 0x002974F6 File Offset: 0x002956F6
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061CE RID: 25038
		private readonly string clusterName;

		// Token: 0x040061CF RID: 25039
		private readonly string fsw;

		// Token: 0x040061D0 RID: 25040
		private readonly Exception ex;
	}
}
