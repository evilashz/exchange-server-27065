using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A2 RID: 4258
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotSwitchLinkedInAccountException : LocalizedException
	{
		// Token: 0x0600B229 RID: 45609 RVA: 0x0029978D File Offset: 0x0029798D
		public CannotSwitchLinkedInAccountException() : base(Strings.CannotSwitchLinkedInAccount)
		{
		}

		// Token: 0x0600B22A RID: 45610 RVA: 0x0029979A File Offset: 0x0029799A
		public CannotSwitchLinkedInAccountException(Exception innerException) : base(Strings.CannotSwitchLinkedInAccount, innerException)
		{
		}

		// Token: 0x0600B22B RID: 45611 RVA: 0x002997A8 File Offset: 0x002979A8
		protected CannotSwitchLinkedInAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B22C RID: 45612 RVA: 0x002997B2 File Offset: 0x002979B2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
