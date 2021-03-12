using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E6C RID: 3692
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDMemberNameUnavailableUsedForDLException : WLCDMemberException
	{
		// Token: 0x0600A6FE RID: 42750 RVA: 0x00287EE9 File Offset: 0x002860E9
		public WLCDMemberNameUnavailableUsedForDLException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6FF RID: 42751 RVA: 0x00287EF2 File Offset: 0x002860F2
		public WLCDMemberNameUnavailableUsedForDLException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A700 RID: 42752 RVA: 0x00287EFC File Offset: 0x002860FC
		protected WLCDMemberNameUnavailableUsedForDLException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A701 RID: 42753 RVA: 0x00287F06 File Offset: 0x00286106
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
