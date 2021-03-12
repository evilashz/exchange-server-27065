using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AC0 RID: 2752
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADTopologyUnexpectedException : PermanentGlobalException
	{
		// Token: 0x0600807A RID: 32890 RVA: 0x001A5417 File Offset: 0x001A3617
		public ADTopologyUnexpectedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x001A5420 File Offset: 0x001A3620
		public ADTopologyUnexpectedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x001A542A File Offset: 0x001A362A
		protected ADTopologyUnexpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x001A5434 File Offset: 0x001A3634
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
