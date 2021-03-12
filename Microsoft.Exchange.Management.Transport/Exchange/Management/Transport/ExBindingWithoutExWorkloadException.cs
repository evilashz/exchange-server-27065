using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000161 RID: 353
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExBindingWithoutExWorkloadException : InvalidCompliancePolicyExBindingException
	{
		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003596E File Offset: 0x00033B6E
		public ExBindingWithoutExWorkloadException() : base(Strings.ErrorExBindingWithoutExWorkload)
		{
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003597B File Offset: 0x00033B7B
		public ExBindingWithoutExWorkloadException(Exception innerException) : base(Strings.ErrorExBindingWithoutExWorkload, innerException)
		{
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00035989 File Offset: 0x00033B89
		protected ExBindingWithoutExWorkloadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00035993 File Offset: 0x00033B93
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
