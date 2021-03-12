using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000098 RID: 152
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceClientTransientException : DxStoreClientTransientException
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x0001408D File Offset: 0x0001228D
		public DxStoreInstanceClientTransientException(string errMsg3) : base(Strings.DxStoreInstanceClientTransientException(errMsg3))
		{
			this.errMsg3 = errMsg3;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000140A7 File Offset: 0x000122A7
		public DxStoreInstanceClientTransientException(string errMsg3, Exception innerException) : base(Strings.DxStoreInstanceClientTransientException(errMsg3), innerException)
		{
			this.errMsg3 = errMsg3;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000140C2 File Offset: 0x000122C2
		protected DxStoreInstanceClientTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg3 = (string)info.GetValue("errMsg3", typeof(string));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000140EC File Offset: 0x000122EC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg3", this.errMsg3);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x00014107 File Offset: 0x00012307
		public string ErrMsg3
		{
			get
			{
				return this.errMsg3;
			}
		}

		// Token: 0x04000287 RID: 647
		private readonly string errMsg3;
	}
}
