using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.DelegatedAuthentication.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.DelegatedAuthentication
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DelegatedAccessDeniedException : LocalizedException
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000045A9 File Offset: 0x000027A9
		public DelegatedAccessDeniedException() : base(Strings.DelegatedAccessDeniedException)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000045B6 File Offset: 0x000027B6
		public DelegatedAccessDeniedException(Exception innerException) : base(Strings.DelegatedAccessDeniedException, innerException)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000045C4 File Offset: 0x000027C4
		protected DelegatedAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000045CE File Offset: 0x000027CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
