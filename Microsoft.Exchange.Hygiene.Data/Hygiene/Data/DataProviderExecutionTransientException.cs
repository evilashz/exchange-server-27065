using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000245 RID: 581
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataProviderExecutionTransientException : TransientDALException
	{
		// Token: 0x06001729 RID: 5929 RVA: 0x000478F9 File Offset: 0x00045AF9
		public DataProviderExecutionTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00047902 File Offset: 0x00045B02
		public DataProviderExecutionTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0004790C File Offset: 0x00045B0C
		protected DataProviderExecutionTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00047916 File Offset: 0x00045B16
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
