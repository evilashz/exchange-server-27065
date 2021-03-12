using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200104F RID: 4175
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswInsufficientPermissionsToDeleteFswException : LocalizedException
	{
		// Token: 0x0600B04A RID: 45130 RVA: 0x00295BE9 File Offset: 0x00293DE9
		public DagFswInsufficientPermissionsToDeleteFswException(string fswMachine, string share, Exception ex) : base(Strings.DagFswInsufficientPermissionsToDeleteFswException(fswMachine, share, ex))
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B04B RID: 45131 RVA: 0x00295C0E File Offset: 0x00293E0E
		public DagFswInsufficientPermissionsToDeleteFswException(string fswMachine, string share, Exception ex, Exception innerException) : base(Strings.DagFswInsufficientPermissionsToDeleteFswException(fswMachine, share, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.share = share;
			this.ex = ex;
		}

		// Token: 0x0600B04C RID: 45132 RVA: 0x00295C38 File Offset: 0x00293E38
		protected DagFswInsufficientPermissionsToDeleteFswException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.share = (string)info.GetValue("share", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B04D RID: 45133 RVA: 0x00295CAD File Offset: 0x00293EAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("share", this.share);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x1700382B RID: 14379
		// (get) Token: 0x0600B04E RID: 45134 RVA: 0x00295CEA File Offset: 0x00293EEA
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x1700382C RID: 14380
		// (get) Token: 0x0600B04F RID: 45135 RVA: 0x00295CF2 File Offset: 0x00293EF2
		public string Share
		{
			get
			{
				return this.share;
			}
		}

		// Token: 0x1700382D RID: 14381
		// (get) Token: 0x0600B050 RID: 45136 RVA: 0x00295CFA File Offset: 0x00293EFA
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006191 RID: 24977
		private readonly string fswMachine;

		// Token: 0x04006192 RID: 24978
		private readonly string share;

		// Token: 0x04006193 RID: 24979
		private readonly Exception ex;
	}
}
