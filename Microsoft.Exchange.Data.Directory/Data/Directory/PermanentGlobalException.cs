using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ABE RID: 2750
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PermanentGlobalException : DataSourceOperationException
	{
		// Token: 0x06008072 RID: 32882 RVA: 0x001A53C9 File Offset: 0x001A35C9
		public PermanentGlobalException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x001A53D2 File Offset: 0x001A35D2
		public PermanentGlobalException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008074 RID: 32884 RVA: 0x001A53DC File Offset: 0x001A35DC
		protected PermanentGlobalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008075 RID: 32885 RVA: 0x001A53E6 File Offset: 0x001A35E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
