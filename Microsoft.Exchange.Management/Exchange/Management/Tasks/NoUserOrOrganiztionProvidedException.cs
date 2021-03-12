using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020011A0 RID: 4512
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoUserOrOrganiztionProvidedException : LocalizedException
	{
		// Token: 0x0600B711 RID: 46865 RVA: 0x002A0DA6 File Offset: 0x0029EFA6
		public NoUserOrOrganiztionProvidedException() : base(Strings.NoUserOrOrganiztionProvidedException)
		{
		}

		// Token: 0x0600B712 RID: 46866 RVA: 0x002A0DB3 File Offset: 0x0029EFB3
		public NoUserOrOrganiztionProvidedException(Exception innerException) : base(Strings.NoUserOrOrganiztionProvidedException, innerException)
		{
		}

		// Token: 0x0600B713 RID: 46867 RVA: 0x002A0DC1 File Offset: 0x0029EFC1
		protected NoUserOrOrganiztionProvidedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B714 RID: 46868 RVA: 0x002A0DCB File Offset: 0x0029EFCB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
