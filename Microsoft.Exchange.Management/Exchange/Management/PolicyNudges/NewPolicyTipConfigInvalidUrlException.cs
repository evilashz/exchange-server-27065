using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02001179 RID: 4473
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NewPolicyTipConfigInvalidUrlException : LocalizedException
	{
		// Token: 0x0600B646 RID: 46662 RVA: 0x0029F86D File Offset: 0x0029DA6D
		public NewPolicyTipConfigInvalidUrlException() : base(Strings.NewPolicyTipConfigInvalidUrl)
		{
		}

		// Token: 0x0600B647 RID: 46663 RVA: 0x0029F87A File Offset: 0x0029DA7A
		public NewPolicyTipConfigInvalidUrlException(Exception innerException) : base(Strings.NewPolicyTipConfigInvalidUrl, innerException)
		{
		}

		// Token: 0x0600B648 RID: 46664 RVA: 0x0029F888 File Offset: 0x0029DA88
		protected NewPolicyTipConfigInvalidUrlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B649 RID: 46665 RVA: 0x0029F892 File Offset: 0x0029DA92
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
