using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000009 RID: 9
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuditLogException : LocalizedException
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003560 File Offset: 0x00001760
		public AuditLogException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003569 File Offset: 0x00001769
		public AuditLogException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003573 File Offset: 0x00001773
		protected AuditLogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000357D File Offset: 0x0000177D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
