using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000247 RID: 583
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataProviderReturnErrorException : PermanentDALException
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x00047947 File Offset: 0x00045B47
		public DataProviderReturnErrorException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00047950 File Offset: 0x00045B50
		public DataProviderReturnErrorException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0004795A File Offset: 0x00045B5A
		protected DataProviderReturnErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00047964 File Offset: 0x00045B64
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
