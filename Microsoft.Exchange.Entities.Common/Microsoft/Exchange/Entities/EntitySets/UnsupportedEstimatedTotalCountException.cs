using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x0200000A RID: 10
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedEstimatedTotalCountException : NotSupportedException
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000026A5 File Offset: 0x000008A5
		public UnsupportedEstimatedTotalCountException() : base(Strings.UnsupportedEstimatedTotalCount)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026B7 File Offset: 0x000008B7
		public UnsupportedEstimatedTotalCountException(Exception innerException) : base(Strings.UnsupportedEstimatedTotalCount, innerException)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026CA File Offset: 0x000008CA
		protected UnsupportedEstimatedTotalCountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026D4 File Offset: 0x000008D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
