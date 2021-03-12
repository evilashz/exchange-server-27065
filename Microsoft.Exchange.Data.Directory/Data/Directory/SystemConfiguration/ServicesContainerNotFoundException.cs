using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000AA0 RID: 2720
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServicesContainerNotFoundException : ADOperationException
	{
		// Token: 0x06007FED RID: 32749 RVA: 0x001A4AA8 File Offset: 0x001A2CA8
		public ServicesContainerNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x001A4AB1 File Offset: 0x001A2CB1
		public ServicesContainerNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x001A4ABB File Offset: 0x001A2CBB
		protected ServicesContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x001A4AC5 File Offset: 0x001A2CC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
