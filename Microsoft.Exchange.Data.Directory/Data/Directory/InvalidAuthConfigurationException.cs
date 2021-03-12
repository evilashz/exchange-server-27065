using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE8 RID: 2792
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidAuthConfigurationException : DataSourceOperationException
	{
		// Token: 0x06008139 RID: 33081 RVA: 0x001A6474 File Offset: 0x001A4674
		public InvalidAuthConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600813A RID: 33082 RVA: 0x001A647D File Offset: 0x001A467D
		public InvalidAuthConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600813B RID: 33083 RVA: 0x001A6487 File Offset: 0x001A4687
		protected InvalidAuthConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600813C RID: 33084 RVA: 0x001A6491 File Offset: 0x001A4691
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
