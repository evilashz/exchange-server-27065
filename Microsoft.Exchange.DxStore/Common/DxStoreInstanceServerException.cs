using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200009E RID: 158
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceServerException : DxStoreServerException
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00014385 File Offset: 0x00012585
		public DxStoreInstanceServerException(string errMsg2) : base(Strings.DxStoreInstanceServerException(errMsg2))
		{
			this.errMsg2 = errMsg2;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001439F File Offset: 0x0001259F
		public DxStoreInstanceServerException(string errMsg2, Exception innerException) : base(Strings.DxStoreInstanceServerException(errMsg2), innerException)
		{
			this.errMsg2 = errMsg2;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000143BA File Offset: 0x000125BA
		protected DxStoreInstanceServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg2 = (string)info.GetValue("errMsg2", typeof(string));
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000143E4 File Offset: 0x000125E4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg2", this.errMsg2);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000143FF File Offset: 0x000125FF
		public string ErrMsg2
		{
			get
			{
				return this.errMsg2;
			}
		}

		// Token: 0x0400028D RID: 653
		private readonly string errMsg2;
	}
}
