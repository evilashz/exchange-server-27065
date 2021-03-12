using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000165 RID: 357
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MulipleExBindingObjectDetectedException : InvalidCompliancePolicyExBindingException
	{
		// Token: 0x06000EF8 RID: 3832 RVA: 0x00035A6B File Offset: 0x00033C6B
		public MulipleExBindingObjectDetectedException() : base(Strings.MulipleExBindingObjectDetected)
		{
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00035A78 File Offset: 0x00033C78
		public MulipleExBindingObjectDetectedException(Exception innerException) : base(Strings.MulipleExBindingObjectDetected, innerException)
		{
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00035A86 File Offset: 0x00033C86
		protected MulipleExBindingObjectDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00035A90 File Offset: 0x00033C90
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
