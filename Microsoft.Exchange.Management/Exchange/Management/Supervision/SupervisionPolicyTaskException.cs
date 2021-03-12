using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Supervision
{
	// Token: 0x02000E7D RID: 3709
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SupervisionPolicyTaskException : LocalizedException
	{
		// Token: 0x0600A745 RID: 42821 RVA: 0x0028827E File Offset: 0x0028647E
		public SupervisionPolicyTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A746 RID: 42822 RVA: 0x00288287 File Offset: 0x00286487
		public SupervisionPolicyTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A747 RID: 42823 RVA: 0x00288291 File Offset: 0x00286491
		protected SupervisionPolicyTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A748 RID: 42824 RVA: 0x0028829B File Offset: 0x0028649B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
