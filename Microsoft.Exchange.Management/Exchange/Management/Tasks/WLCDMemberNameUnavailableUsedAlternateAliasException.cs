using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E6B RID: 3691
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDMemberNameUnavailableUsedAlternateAliasException : WLCDMemberException
	{
		// Token: 0x0600A6FA RID: 42746 RVA: 0x00287EC2 File Offset: 0x002860C2
		public WLCDMemberNameUnavailableUsedAlternateAliasException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6FB RID: 42747 RVA: 0x00287ECB File Offset: 0x002860CB
		public WLCDMemberNameUnavailableUsedAlternateAliasException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6FC RID: 42748 RVA: 0x00287ED5 File Offset: 0x002860D5
		protected WLCDMemberNameUnavailableUsedAlternateAliasException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6FD RID: 42749 RVA: 0x00287EDF File Offset: 0x002860DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
