using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000246 RID: 582
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataProviderMappingException : PermanentDALException
	{
		// Token: 0x0600172D RID: 5933 RVA: 0x00047920 File Offset: 0x00045B20
		public DataProviderMappingException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00047929 File Offset: 0x00045B29
		public DataProviderMappingException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00047933 File Offset: 0x00045B33
		protected DataProviderMappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0004793D File Offset: 0x00045B3D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
