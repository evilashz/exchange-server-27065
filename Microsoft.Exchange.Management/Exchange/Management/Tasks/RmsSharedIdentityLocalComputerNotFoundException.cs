using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E24 RID: 3620
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsSharedIdentityLocalComputerNotFoundException : LocalizedException
	{
		// Token: 0x0600A5BB RID: 42427 RVA: 0x002868B9 File Offset: 0x00284AB9
		public RmsSharedIdentityLocalComputerNotFoundException() : base(Strings.RmsSharedIdentityLocalComputerNotFound)
		{
		}

		// Token: 0x0600A5BC RID: 42428 RVA: 0x002868C6 File Offset: 0x00284AC6
		public RmsSharedIdentityLocalComputerNotFoundException(Exception innerException) : base(Strings.RmsSharedIdentityLocalComputerNotFound, innerException)
		{
		}

		// Token: 0x0600A5BD RID: 42429 RVA: 0x002868D4 File Offset: 0x00284AD4
		protected RmsSharedIdentityLocalComputerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A5BE RID: 42430 RVA: 0x002868DE File Offset: 0x00284ADE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
