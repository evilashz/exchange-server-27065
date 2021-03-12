using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002B0 RID: 688
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ProxyAddressExistsException : LocalizedException
	{
		// Token: 0x060018E4 RID: 6372 RVA: 0x0005C73B File Offset: 0x0005A93B
		public ProxyAddressExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0005C744 File Offset: 0x0005A944
		public ProxyAddressExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0005C74E File Offset: 0x0005A94E
		protected ProxyAddressExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0005C758 File Offset: 0x0005A958
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
