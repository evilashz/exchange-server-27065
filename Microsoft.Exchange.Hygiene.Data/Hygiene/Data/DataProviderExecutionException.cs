using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000244 RID: 580
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataProviderExecutionException : PermanentDALException
	{
		// Token: 0x06001725 RID: 5925 RVA: 0x000478D2 File Offset: 0x00045AD2
		public DataProviderExecutionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000478DB File Offset: 0x00045ADB
		public DataProviderExecutionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000478E5 File Offset: 0x00045AE5
		protected DataProviderExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000478EF File Offset: 0x00045AEF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
