using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000414 RID: 1044
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatacenterInvalidRegistryException : LocalizedException
	{
		// Token: 0x0600193E RID: 6462 RVA: 0x0005F75E File Offset: 0x0005D95E
		public DatacenterInvalidRegistryException() : base(DiagnosticsResources.DatacenterInvalidRegistryException)
		{
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x0005F76B File Offset: 0x0005D96B
		public DatacenterInvalidRegistryException(Exception innerException) : base(DiagnosticsResources.DatacenterInvalidRegistryException, innerException)
		{
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0005F779 File Offset: 0x0005D979
		protected DatacenterInvalidRegistryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0005F783 File Offset: 0x0005D983
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
