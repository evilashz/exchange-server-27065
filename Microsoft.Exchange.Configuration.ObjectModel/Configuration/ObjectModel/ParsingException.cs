using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002A8 RID: 680
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ParsingException : LocalizedException
	{
		// Token: 0x060018BB RID: 6331 RVA: 0x0005C31B File Offset: 0x0005A51B
		public ParsingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0005C324 File Offset: 0x0005A524
		public ParsingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005C32E File Offset: 0x0005A52E
		protected ParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0005C338 File Offset: 0x0005A538
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
