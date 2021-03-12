using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x02000599 RID: 1433
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EndpointManagerEndpointUninitializedException : LocalizedException
	{
		// Token: 0x0600269B RID: 9883 RVA: 0x000DDC03 File Offset: 0x000DBE03
		public EndpointManagerEndpointUninitializedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000DDC0C File Offset: 0x000DBE0C
		public EndpointManagerEndpointUninitializedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000DDC16 File Offset: 0x000DBE16
		protected EndpointManagerEndpointUninitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000DDC20 File Offset: 0x000DBE20
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
