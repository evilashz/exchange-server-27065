using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D7 RID: 215
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataSourceOperationException : LocalizedException
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0001A690 File Offset: 0x00018890
		public DataSourceOperationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001A699 File Offset: 0x00018899
		public DataSourceOperationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001A6A3 File Offset: 0x000188A3
		protected DataSourceOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001A6AD File Offset: 0x000188AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
