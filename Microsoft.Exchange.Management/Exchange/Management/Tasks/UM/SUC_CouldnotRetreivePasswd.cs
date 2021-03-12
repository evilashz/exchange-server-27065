using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D1 RID: 4561
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SUC_CouldnotRetreivePasswd : LocalizedException
	{
		// Token: 0x0600B8F5 RID: 47349 RVA: 0x002A53C7 File Offset: 0x002A35C7
		public SUC_CouldnotRetreivePasswd() : base(Strings.CouldnotRetreivePasswd)
		{
		}

		// Token: 0x0600B8F6 RID: 47350 RVA: 0x002A53D4 File Offset: 0x002A35D4
		public SUC_CouldnotRetreivePasswd(Exception innerException) : base(Strings.CouldnotRetreivePasswd, innerException)
		{
		}

		// Token: 0x0600B8F7 RID: 47351 RVA: 0x002A53E2 File Offset: 0x002A35E2
		protected SUC_CouldnotRetreivePasswd(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8F8 RID: 47352 RVA: 0x002A53EC File Offset: 0x002A35EC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
