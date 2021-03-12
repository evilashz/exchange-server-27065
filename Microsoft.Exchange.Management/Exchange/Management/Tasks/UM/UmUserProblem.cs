using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D6 RID: 4566
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UmUserProblem : LocalizedException
	{
		// Token: 0x0600B90C RID: 47372 RVA: 0x002A5599 File Offset: 0x002A3799
		public UmUserProblem() : base(Strings.UserProblem)
		{
		}

		// Token: 0x0600B90D RID: 47373 RVA: 0x002A55A6 File Offset: 0x002A37A6
		public UmUserProblem(Exception innerException) : base(Strings.UserProblem, innerException)
		{
		}

		// Token: 0x0600B90E RID: 47374 RVA: 0x002A55B4 File Offset: 0x002A37B4
		protected UmUserProblem(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B90F RID: 47375 RVA: 0x002A55BE File Offset: 0x002A37BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
