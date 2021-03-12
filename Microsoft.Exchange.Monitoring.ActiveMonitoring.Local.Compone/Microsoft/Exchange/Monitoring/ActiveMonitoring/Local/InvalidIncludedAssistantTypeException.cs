using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005AD RID: 1453
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIncludedAssistantTypeException : LocalizedException
	{
		// Token: 0x060026FA RID: 9978 RVA: 0x000DE427 File Offset: 0x000DC627
		public InvalidIncludedAssistantTypeException() : base(Strings.InvalidIncludedAssistantType)
		{
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000DE434 File Offset: 0x000DC634
		public InvalidIncludedAssistantTypeException(Exception innerException) : base(Strings.InvalidIncludedAssistantType, innerException)
		{
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000DE442 File Offset: 0x000DC642
		protected InvalidIncludedAssistantTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000DE44C File Offset: 0x000DC64C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
