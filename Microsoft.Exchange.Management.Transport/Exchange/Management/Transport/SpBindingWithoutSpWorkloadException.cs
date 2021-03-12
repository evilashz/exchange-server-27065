using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000164 RID: 356
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SpBindingWithoutSpWorkloadException : InvalidCompliancePolicySpBindingException
	{
		// Token: 0x06000EF4 RID: 3828 RVA: 0x00035A3C File Offset: 0x00033C3C
		public SpBindingWithoutSpWorkloadException() : base(Strings.ErrorSpBindingWithoutSpWorkload)
		{
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00035A49 File Offset: 0x00033C49
		public SpBindingWithoutSpWorkloadException(Exception innerException) : base(Strings.ErrorSpBindingWithoutSpWorkload, innerException)
		{
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00035A57 File Offset: 0x00033C57
		protected SpBindingWithoutSpWorkloadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00035A61 File Offset: 0x00033C61
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
