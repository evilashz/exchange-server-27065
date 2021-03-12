using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001031 RID: 4145
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class JournalRuleCorruptException : LocalizedException
	{
		// Token: 0x0600AFAD RID: 44973 RVA: 0x00294B6A File Offset: 0x00292D6A
		public JournalRuleCorruptException() : base(Strings.JournalRuleCorrupt)
		{
		}

		// Token: 0x0600AFAE RID: 44974 RVA: 0x00294B77 File Offset: 0x00292D77
		public JournalRuleCorruptException(Exception innerException) : base(Strings.JournalRuleCorrupt, innerException)
		{
		}

		// Token: 0x0600AFAF RID: 44975 RVA: 0x00294B85 File Offset: 0x00292D85
		protected JournalRuleCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AFB0 RID: 44976 RVA: 0x00294B8F File Offset: 0x00292D8F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
