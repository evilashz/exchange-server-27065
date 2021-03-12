using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001CF RID: 463
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMRPCIncompabibleVersionException : LocalizedException
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x0003614D File Offset: 0x0003434D
		public UMRPCIncompabibleVersionException() : base(Strings.UMRPCIncompatibleVersionException)
		{
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003615A File Offset: 0x0003435A
		public UMRPCIncompabibleVersionException(Exception innerException) : base(Strings.UMRPCIncompatibleVersionException, innerException)
		{
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00036168 File Offset: 0x00034368
		protected UMRPCIncompabibleVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00036172 File Offset: 0x00034372
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
