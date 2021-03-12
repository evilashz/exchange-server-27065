using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200104E RID: 4174
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswServerNotAccessibleException : LocalizedException
	{
		// Token: 0x0600B044 RID: 45124 RVA: 0x00295B1D File Offset: 0x00293D1D
		public DagFswServerNotAccessibleException(string fswMachine, Exception ex) : base(Strings.DagFswServerNotAccessibleException(fswMachine, ex))
		{
			this.fswMachine = fswMachine;
			this.ex = ex;
		}

		// Token: 0x0600B045 RID: 45125 RVA: 0x00295B3A File Offset: 0x00293D3A
		public DagFswServerNotAccessibleException(string fswMachine, Exception ex, Exception innerException) : base(Strings.DagFswServerNotAccessibleException(fswMachine, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.ex = ex;
		}

		// Token: 0x0600B046 RID: 45126 RVA: 0x00295B58 File Offset: 0x00293D58
		protected DagFswServerNotAccessibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B047 RID: 45127 RVA: 0x00295BAD File Offset: 0x00293DAD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003829 RID: 14377
		// (get) Token: 0x0600B048 RID: 45128 RVA: 0x00295BD9 File Offset: 0x00293DD9
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x1700382A RID: 14378
		// (get) Token: 0x0600B049 RID: 45129 RVA: 0x00295BE1 File Offset: 0x00293DE1
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x0400618F RID: 24975
		private readonly string fswMachine;

		// Token: 0x04006190 RID: 24976
		private readonly Exception ex;
	}
}
