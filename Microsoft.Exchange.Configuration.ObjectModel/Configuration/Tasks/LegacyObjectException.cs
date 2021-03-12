using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002C4 RID: 708
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LegacyObjectException : LocalizedException
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x0005CE49 File Offset: 0x0005B049
		public LegacyObjectException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0005CE52 File Offset: 0x0005B052
		public LegacyObjectException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0005CE5C File Offset: 0x0005B05C
		protected LegacyObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0005CE66 File Offset: 0x0005B066
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
