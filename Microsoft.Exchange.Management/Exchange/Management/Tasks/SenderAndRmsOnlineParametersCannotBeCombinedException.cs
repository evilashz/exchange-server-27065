using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010EB RID: 4331
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SenderAndRmsOnlineParametersCannotBeCombinedException : LocalizedException
	{
		// Token: 0x0600B385 RID: 45957 RVA: 0x0029B5C1 File Offset: 0x002997C1
		public SenderAndRmsOnlineParametersCannotBeCombinedException() : base(Strings.SenderAndRmsOnlineParametersCannotBeCombined)
		{
		}

		// Token: 0x0600B386 RID: 45958 RVA: 0x0029B5CE File Offset: 0x002997CE
		public SenderAndRmsOnlineParametersCannotBeCombinedException(Exception innerException) : base(Strings.SenderAndRmsOnlineParametersCannotBeCombined, innerException)
		{
		}

		// Token: 0x0600B387 RID: 45959 RVA: 0x0029B5DC File Offset: 0x002997DC
		protected SenderAndRmsOnlineParametersCannotBeCombinedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B388 RID: 45960 RVA: 0x0029B5E6 File Offset: 0x002997E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
