using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020000F0 RID: 240
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class GetUserSettingsException : LocalizedException
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00016255 File Offset: 0x00014455
		public GetUserSettingsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001625E File Offset: 0x0001445E
		public GetUserSettingsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00016268 File Offset: 0x00014468
		protected GetUserSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00016272 File Offset: 0x00014472
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
