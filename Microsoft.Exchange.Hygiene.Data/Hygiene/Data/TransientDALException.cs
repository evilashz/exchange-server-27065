using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000242 RID: 578
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TransientDALException : DataSourceTransientException
	{
		// Token: 0x0600171D RID: 5917 RVA: 0x00047884 File Offset: 0x00045A84
		public TransientDALException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0004788D File Offset: 0x00045A8D
		public TransientDALException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00047897 File Offset: 0x00045A97
		protected TransientDALException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000478A1 File Offset: 0x00045AA1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
