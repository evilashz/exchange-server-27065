using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000249 RID: 585
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataProviderIsTooBusyException : TransientDALException
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x00047995 File Offset: 0x00045B95
		public DataProviderIsTooBusyException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0004799E File Offset: 0x00045B9E
		public DataProviderIsTooBusyException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000479A8 File Offset: 0x00045BA8
		protected DataProviderIsTooBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x000479B2 File Offset: 0x00045BB2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
