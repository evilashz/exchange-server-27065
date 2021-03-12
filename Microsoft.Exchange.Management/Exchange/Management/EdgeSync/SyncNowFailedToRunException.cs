using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000FD3 RID: 4051
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SyncNowFailedToRunException : LocalizedException
	{
		// Token: 0x0600ADF1 RID: 44529 RVA: 0x00292674 File Offset: 0x00290874
		public SyncNowFailedToRunException() : base(Strings.SyncNowFailedToRunException)
		{
		}

		// Token: 0x0600ADF2 RID: 44530 RVA: 0x00292681 File Offset: 0x00290881
		public SyncNowFailedToRunException(Exception innerException) : base(Strings.SyncNowFailedToRunException, innerException)
		{
		}

		// Token: 0x0600ADF3 RID: 44531 RVA: 0x0029268F File Offset: 0x0029088F
		protected SyncNowFailedToRunException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADF4 RID: 44532 RVA: 0x00292699 File Offset: 0x00290899
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
