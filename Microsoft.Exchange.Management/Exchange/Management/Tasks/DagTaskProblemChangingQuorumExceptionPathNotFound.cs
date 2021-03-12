using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200106E RID: 4206
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskProblemChangingQuorumExceptionPathNotFound : LocalizedException
	{
		// Token: 0x0600B106 RID: 45318 RVA: 0x002974FE File Offset: 0x002956FE
		public DagTaskProblemChangingQuorumExceptionPathNotFound(string clusterName, string fsw, Exception ex) : base(Strings.DagTaskProblemChangingQuorumExceptionPathNotFound(clusterName, fsw, ex))
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B107 RID: 45319 RVA: 0x00297523 File Offset: 0x00295723
		public DagTaskProblemChangingQuorumExceptionPathNotFound(string clusterName, string fsw, Exception ex, Exception innerException) : base(Strings.DagTaskProblemChangingQuorumExceptionPathNotFound(clusterName, fsw, ex), innerException)
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B108 RID: 45320 RVA: 0x0029754C File Offset: 0x0029574C
		protected DagTaskProblemChangingQuorumExceptionPathNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.fsw = (string)info.GetValue("fsw", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B109 RID: 45321 RVA: 0x002975C1 File Offset: 0x002957C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("fsw", this.fsw);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x1700386B RID: 14443
		// (get) Token: 0x0600B10A RID: 45322 RVA: 0x002975FE File Offset: 0x002957FE
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x1700386C RID: 14444
		// (get) Token: 0x0600B10B RID: 45323 RVA: 0x00297606 File Offset: 0x00295806
		public string Fsw
		{
			get
			{
				return this.fsw;
			}
		}

		// Token: 0x1700386D RID: 14445
		// (get) Token: 0x0600B10C RID: 45324 RVA: 0x0029760E File Offset: 0x0029580E
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061D1 RID: 25041
		private readonly string clusterName;

		// Token: 0x040061D2 RID: 25042
		private readonly string fsw;

		// Token: 0x040061D3 RID: 25043
		private readonly Exception ex;
	}
}
