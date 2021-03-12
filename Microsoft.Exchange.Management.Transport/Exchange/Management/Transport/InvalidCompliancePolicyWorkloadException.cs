using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200015D RID: 349
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidCompliancePolicyWorkloadException : LocalizedException
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x00035879 File Offset: 0x00033A79
		public InvalidCompliancePolicyWorkloadException() : base(Strings.InvalidCompliancePolicyWorkload)
		{
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00035886 File Offset: 0x00033A86
		public InvalidCompliancePolicyWorkloadException(Exception innerException) : base(Strings.InvalidCompliancePolicyWorkload, innerException)
		{
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00035894 File Offset: 0x00033A94
		protected InvalidCompliancePolicyWorkloadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003589E File Offset: 0x00033A9E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
