using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200012C RID: 300
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AssociationNotFoundException : DataSourceOperationException
	{
		// Token: 0x06001474 RID: 5236 RVA: 0x0006AECD File Offset: 0x000690CD
		public AssociationNotFoundException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0006AED6 File Offset: 0x000690D6
		public AssociationNotFoundException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0006AEE0 File Offset: 0x000690E0
		protected AssociationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0006AEEA File Offset: 0x000690EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
