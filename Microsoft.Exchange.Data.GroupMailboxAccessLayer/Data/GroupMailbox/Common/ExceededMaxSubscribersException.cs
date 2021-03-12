using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.GroupMailbox.Common
{
	// Token: 0x02000063 RID: 99
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExceededMaxSubscribersException : DataSourceOperationException
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00011D8F File Offset: 0x0000FF8F
		public ExceededMaxSubscribersException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00011D98 File Offset: 0x0000FF98
		public ExceededMaxSubscribersException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00011DA2 File Offset: 0x0000FFA2
		protected ExceededMaxSubscribersException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00011DAC File Offset: 0x0000FFAC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
