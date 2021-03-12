using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.SoapWebClient
{
	// Token: 0x020000F1 RID: 241
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class GetFederationInformationException : LocalizedException
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0001627C File Offset: 0x0001447C
		public GetFederationInformationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00016285 File Offset: 0x00014485
		public GetFederationInformationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001628F File Offset: 0x0001448F
		protected GetFederationInformationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00016299 File Offset: 0x00014499
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
