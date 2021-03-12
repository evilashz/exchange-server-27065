using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02001176 RID: 4470
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewPolicyTipConfigDuplicateException : LocalizedException
	{
		// Token: 0x0600B637 RID: 46647 RVA: 0x0029F6F7 File Offset: 0x0029D8F7
		public NewPolicyTipConfigDuplicateException() : base(Strings.NewPolicyTipConfigDuplicate)
		{
		}

		// Token: 0x0600B638 RID: 46648 RVA: 0x0029F704 File Offset: 0x0029D904
		public NewPolicyTipConfigDuplicateException(Exception innerException) : base(Strings.NewPolicyTipConfigDuplicate, innerException)
		{
		}

		// Token: 0x0600B639 RID: 46649 RVA: 0x0029F712 File Offset: 0x0029D912
		protected NewPolicyTipConfigDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B63A RID: 46650 RVA: 0x0029F71C File Offset: 0x0029D91C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
