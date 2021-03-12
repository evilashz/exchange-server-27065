using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020000EF RID: 239
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AutodiscoverClientException : LocalizedException
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x0001622E File Offset: 0x0001442E
		public AutodiscoverClientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00016237 File Offset: 0x00014437
		public AutodiscoverClientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00016241 File Offset: 0x00014441
		protected AutodiscoverClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001624B File Offset: 0x0001444B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
