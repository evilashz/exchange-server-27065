using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A4 RID: 164
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceStaleStoreException : DxStoreInstanceServerException
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00014691 File Offset: 0x00012891
		public DxStoreInstanceStaleStoreException() : base(Strings.DxStoreInstanceStaleStore)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000146A3 File Offset: 0x000128A3
		public DxStoreInstanceStaleStoreException(Exception innerException) : base(Strings.DxStoreInstanceStaleStore, innerException)
		{
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000146B6 File Offset: 0x000128B6
		protected DxStoreInstanceStaleStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000146C0 File Offset: 0x000128C0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
