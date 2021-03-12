using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001050 RID: 4176
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswServerNotAccessibleToDeleteFswException : LocalizedException
	{
		// Token: 0x0600B051 RID: 45137 RVA: 0x00295D02 File Offset: 0x00293F02
		public DagFswServerNotAccessibleToDeleteFswException(string fswMachine, string share, Exception ex) : base(Strings.DagFswServerNotAccessibleToDeleteFswException(fswMachine, share, ex))
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B052 RID: 45138 RVA: 0x00295D27 File Offset: 0x00293F27
		public DagFswServerNotAccessibleToDeleteFswException(string fswMachine, string share, Exception ex, Exception innerException) : base(Strings.DagFswServerNotAccessibleToDeleteFswException(fswMachine, share, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B053 RID: 45139 RVA: 0x00295D50 File Offset: 0x00293F50
		protected DagFswServerNotAccessibleToDeleteFswException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.share = (string)info.GetValue("share", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B054 RID: 45140 RVA: 0x00295DC5 File Offset: 0x00293FC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("share", this.share);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x1700382E RID: 14382
		// (get) Token: 0x0600B055 RID: 45141 RVA: 0x00295E02 File Offset: 0x00294002
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x1700382F RID: 14383
		// (get) Token: 0x0600B056 RID: 45142 RVA: 0x00295E0A File Offset: 0x0029400A
		public string Share
		{
			get
			{
				return this.share;
			}
		}

		// Token: 0x17003830 RID: 14384
		// (get) Token: 0x0600B057 RID: 45143 RVA: 0x00295E12 File Offset: 0x00294012
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006194 RID: 24980
		private readonly string fswMachine;

		// Token: 0x04006195 RID: 24981
		private readonly string share;

		// Token: 0x04006196 RID: 24982
		private readonly Exception ex;
	}
}
