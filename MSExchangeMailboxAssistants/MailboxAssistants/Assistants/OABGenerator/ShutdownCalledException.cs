using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x02000147 RID: 327
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ShutdownCalledException : LocalizedException
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x00052098 File Offset: 0x00050298
		public ShutdownCalledException() : base(Strings.ShutdownCalled)
		{
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000520A5 File Offset: 0x000502A5
		public ShutdownCalledException(Exception innerException) : base(Strings.ShutdownCalled, innerException)
		{
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000520B3 File Offset: 0x000502B3
		protected ShutdownCalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000520BD File Offset: 0x000502BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
