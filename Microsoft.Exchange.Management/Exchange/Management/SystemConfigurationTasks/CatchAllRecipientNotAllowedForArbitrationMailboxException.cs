using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F69 RID: 3945
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CatchAllRecipientNotAllowedForArbitrationMailboxException : LocalizedException
	{
		// Token: 0x0600ABFD RID: 44029 RVA: 0x0028FC9A File Offset: 0x0028DE9A
		public CatchAllRecipientNotAllowedForArbitrationMailboxException() : base(Strings.CatchAllRecipientNotAllowedForArbitrationMailbox)
		{
		}

		// Token: 0x0600ABFE RID: 44030 RVA: 0x0028FCA7 File Offset: 0x0028DEA7
		public CatchAllRecipientNotAllowedForArbitrationMailboxException(Exception innerException) : base(Strings.CatchAllRecipientNotAllowedForArbitrationMailbox, innerException)
		{
		}

		// Token: 0x0600ABFF RID: 44031 RVA: 0x0028FCB5 File Offset: 0x0028DEB5
		protected CatchAllRecipientNotAllowedForArbitrationMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AC00 RID: 44032 RVA: 0x0028FCBF File Offset: 0x0028DEBF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
