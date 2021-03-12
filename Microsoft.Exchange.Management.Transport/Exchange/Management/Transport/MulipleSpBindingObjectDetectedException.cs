using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000166 RID: 358
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MulipleSpBindingObjectDetectedException : InvalidCompliancePolicySpBindingException
	{
		// Token: 0x06000EFC RID: 3836 RVA: 0x00035A9A File Offset: 0x00033C9A
		public MulipleSpBindingObjectDetectedException() : base(Strings.MulipleSpBindingObjectDetected)
		{
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00035AA7 File Offset: 0x00033CA7
		public MulipleSpBindingObjectDetectedException(Exception innerException) : base(Strings.MulipleSpBindingObjectDetected, innerException)
		{
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00035AB5 File Offset: 0x00033CB5
		protected MulipleSpBindingObjectDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00035ABF File Offset: 0x00033CBF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
