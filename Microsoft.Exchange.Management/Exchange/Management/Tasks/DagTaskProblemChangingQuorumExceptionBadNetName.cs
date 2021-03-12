using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200106C RID: 4204
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskProblemChangingQuorumExceptionBadNetName : LocalizedException
	{
		// Token: 0x0600B0F8 RID: 45304 RVA: 0x002972CE File Offset: 0x002954CE
		public DagTaskProblemChangingQuorumExceptionBadNetName(string clusterName, string fsw, Exception ex) : base(Strings.DagTaskProblemChangingQuorumExceptionBadNetName(clusterName, fsw, ex))
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B0F9 RID: 45305 RVA: 0x002972F3 File Offset: 0x002954F3
		public DagTaskProblemChangingQuorumExceptionBadNetName(string clusterName, string fsw, Exception ex, Exception innerException) : base(Strings.DagTaskProblemChangingQuorumExceptionBadNetName(clusterName, fsw, ex), innerException)
		{
			this.clusterName = clusterName;
			this.fsw = fsw;
			this.ex = ex;
		}

		// Token: 0x0600B0FA RID: 45306 RVA: 0x0029731C File Offset: 0x0029551C
		protected DagTaskProblemChangingQuorumExceptionBadNetName(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.clusterName = (string)info.GetValue("clusterName", typeof(string));
			this.fsw = (string)info.GetValue("fsw", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B0FB RID: 45307 RVA: 0x00297391 File Offset: 0x00295591
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("clusterName", this.clusterName);
			info.AddValue("fsw", this.fsw);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003865 RID: 14437
		// (get) Token: 0x0600B0FC RID: 45308 RVA: 0x002973CE File Offset: 0x002955CE
		public string ClusterName
		{
			get
			{
				return this.clusterName;
			}
		}

		// Token: 0x17003866 RID: 14438
		// (get) Token: 0x0600B0FD RID: 45309 RVA: 0x002973D6 File Offset: 0x002955D6
		public string Fsw
		{
			get
			{
				return this.fsw;
			}
		}

		// Token: 0x17003867 RID: 14439
		// (get) Token: 0x0600B0FE RID: 45310 RVA: 0x002973DE File Offset: 0x002955DE
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061CB RID: 25035
		private readonly string clusterName;

		// Token: 0x040061CC RID: 25036
		private readonly string fsw;

		// Token: 0x040061CD RID: 25037
		private readonly Exception ex;
	}
}
