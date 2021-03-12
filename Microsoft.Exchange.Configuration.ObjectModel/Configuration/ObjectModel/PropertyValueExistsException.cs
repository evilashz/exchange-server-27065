using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002AE RID: 686
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PropertyValueExistsException : LocalizedException
	{
		// Token: 0x060018DC RID: 6364 RVA: 0x0005C6ED File Offset: 0x0005A8ED
		public PropertyValueExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005C6F6 File Offset: 0x0005A8F6
		public PropertyValueExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005C700 File Offset: 0x0005A900
		protected PropertyValueExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005C70A File Offset: 0x0005A90A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
