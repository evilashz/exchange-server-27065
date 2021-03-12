using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ProxyException : LocalizedException
	{
		// Token: 0x06001843 RID: 6211 RVA: 0x0004B30C File Offset: 0x0004950C
		public ProxyException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0004B315 File Offset: 0x00049515
		public ProxyException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0004B31F File Offset: 0x0004951F
		protected ProxyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x0004B329 File Offset: 0x00049529
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
