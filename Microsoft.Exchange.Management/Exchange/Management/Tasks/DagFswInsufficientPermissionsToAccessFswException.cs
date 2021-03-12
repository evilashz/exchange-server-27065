using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200104D RID: 4173
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswInsufficientPermissionsToAccessFswException : LocalizedException
	{
		// Token: 0x0600B03E RID: 45118 RVA: 0x00295A50 File Offset: 0x00293C50
		public DagFswInsufficientPermissionsToAccessFswException(string fswMachine, Exception ex) : base(Strings.DagFswInsufficientPermissionsToAccessFswException(fswMachine, ex))
		{
			this.fswMachine = fswMachine;
			this.ex = ex;
		}

		// Token: 0x0600B03F RID: 45119 RVA: 0x00295A6D File Offset: 0x00293C6D
		public DagFswInsufficientPermissionsToAccessFswException(string fswMachine, Exception ex, Exception innerException) : base(Strings.DagFswInsufficientPermissionsToAccessFswException(fswMachine, ex), innerException)
		{
			this.fswMachine = fswMachine;
			this.ex = ex;
		}

		// Token: 0x0600B040 RID: 45120 RVA: 0x00295A8C File Offset: 0x00293C8C
		protected DagFswInsufficientPermissionsToAccessFswException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswMachine = (string)info.GetValue("fswMachine", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B041 RID: 45121 RVA: 0x00295AE1 File Offset: 0x00293CE1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswMachine", this.fswMachine);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003827 RID: 14375
		// (get) Token: 0x0600B042 RID: 45122 RVA: 0x00295B0D File Offset: 0x00293D0D
		public string FswMachine
		{
			get
			{
				return this.fswMachine;
			}
		}

		// Token: 0x17003828 RID: 14376
		// (get) Token: 0x0600B043 RID: 45123 RVA: 0x00295B15 File Offset: 0x00293D15
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x0400618D RID: 24973
		private readonly string fswMachine;

		// Token: 0x0400618E RID: 24974
		private readonly Exception ex;
	}
}
