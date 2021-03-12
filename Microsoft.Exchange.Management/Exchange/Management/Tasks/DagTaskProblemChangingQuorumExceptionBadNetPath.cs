using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200106B RID: 4203
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskProblemChangingQuorumExceptionBadNetPath : LocalizedException
	{
		// Token: 0x0600B0F1 RID: 45297 RVA: 0x002971B5 File Offset: 0x002953B5
		public DagTaskProblemChangingQuorumExceptionBadNetPath(string clusterName, string fsw, Exception ex) : base(Strings.DagTaskProblemChangingQuorumExceptionBadNetPath(clusterName, fsw, ex))
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B0F2 RID: 45298 RVA: 0x002971DA File Offset: 0x002953DA
		public DagTaskProblemChangingQuorumExceptionBadNetPath(string clusterName, string fsw, Exception ex, Exception innerException) : base(Strings.DagTaskProblemChangingQuorumExceptionBadNetPath(clusterName, fsw, ex), innerException)
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B0F3 RID: 45299 RVA: 0x00297204 File Offset: 0x00295404
		protected DagTaskProblemChangingQuorumExceptionBadNetPath(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.fsw = (string)info.GetValue("fsw", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B0F4 RID: 45300 RVA: 0x00297279 File Offset: 0x00295479
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("fsw", this.fsw);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003862 RID: 14434
		// (get) Token: 0x0600B0F5 RID: 45301 RVA: 0x002972B6 File Offset: 0x002954B6
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17003863 RID: 14435
		// (get) Token: 0x0600B0F6 RID: 45302 RVA: 0x002972BE File Offset: 0x002954BE
		public string Fsw
		{
			get
			{
				return this.fsw;
			}
		}

		// Token: 0x17003864 RID: 14436
		// (get) Token: 0x0600B0F7 RID: 45303 RVA: 0x002972C6 File Offset: 0x002954C6
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061C8 RID: 25032
		private readonly string clusterName;

		// Token: 0x040061C9 RID: 25033
		private readonly string fsw;

		// Token: 0x040061CA RID: 25034
		private readonly Exception ex;
	}
}
