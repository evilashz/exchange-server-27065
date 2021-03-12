using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E58 RID: 3672
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDMemberException : WLCDPartnerException
	{
		// Token: 0x0600A6AE RID: 42670 RVA: 0x00287BDD File Offset: 0x00285DDD
		public WLCDMemberException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6AF RID: 42671 RVA: 0x00287BE6 File Offset: 0x00285DE6
		public WLCDMemberException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6B0 RID: 42672 RVA: 0x00287BF0 File Offset: 0x00285DF0
		protected WLCDMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6B1 RID: 42673 RVA: 0x00287BFA File Offset: 0x00285DFA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
