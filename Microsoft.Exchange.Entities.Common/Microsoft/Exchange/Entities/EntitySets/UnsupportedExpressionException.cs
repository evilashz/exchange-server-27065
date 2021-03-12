using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Entities.EntitySets
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnsupportedExpressionException : NotSupportedException
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000026DE File Offset: 0x000008DE
		public UnsupportedExpressionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026EC File Offset: 0x000008EC
		public UnsupportedExpressionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026FB File Offset: 0x000008FB
		protected UnsupportedExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002705 File Offset: 0x00000905
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
