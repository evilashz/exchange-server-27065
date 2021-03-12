using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000138 RID: 312
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProcessingException : LocalizedException
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x0006B94F File Offset: 0x00069B4F
		public ProcessingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0006B958 File Offset: 0x00069B58
		public ProcessingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0006B962 File Offset: 0x00069B62
		protected ProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0006B96C File Offset: 0x00069B6C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
