using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x020010A3 RID: 4259
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoProvisionFailedException : LocalizedException
	{
		// Token: 0x0600B22D RID: 45613 RVA: 0x002997BC File Offset: 0x002979BC
		public AutoProvisionFailedException() : base(Strings.AutoProvisionFailedException)
		{
		}

		// Token: 0x0600B22E RID: 45614 RVA: 0x002997C9 File Offset: 0x002979C9
		public AutoProvisionFailedException(Exception innerException) : base(Strings.AutoProvisionFailedException, innerException)
		{
		}

		// Token: 0x0600B22F RID: 45615 RVA: 0x002997D7 File Offset: 0x002979D7
		protected AutoProvisionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B230 RID: 45616 RVA: 0x002997E1 File Offset: 0x002979E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
