using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001A1 RID: 417
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMServerNotFoundDialPlanException : UMServerNotFoundException
	{
		// Token: 0x06000E55 RID: 3669 RVA: 0x00034EE2 File Offset: 0x000330E2
		public UMServerNotFoundDialPlanException(string dialPlan) : base(Strings.UMServerNotFoundDialPlanException(dialPlan))
		{
			this.dialPlan = dialPlan;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00034EF7 File Offset: 0x000330F7
		public UMServerNotFoundDialPlanException(string dialPlan, Exception innerException) : base(Strings.UMServerNotFoundDialPlanException(dialPlan), innerException)
		{
			this.dialPlan = dialPlan;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00034F0D File Offset: 0x0003310D
		protected UMServerNotFoundDialPlanException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dialPlan = (string)info.GetValue("dialPlan", typeof(string));
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00034F37 File Offset: 0x00033137
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dialPlan", this.dialPlan);
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00034F52 File Offset: 0x00033152
		public string DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x04000781 RID: 1921
		private readonly string dialPlan;
	}
}
