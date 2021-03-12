using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000095 RID: 149
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreAccessClientTransientException : DxStoreClientTransientException
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x00013F07 File Offset: 0x00012107
		public DxStoreAccessClientTransientException(string errMsg1) : base(Strings.DxStoreAccessClientTransientException(errMsg1))
		{
			this.errMsg1 = errMsg1;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00013F21 File Offset: 0x00012121
		public DxStoreAccessClientTransientException(string errMsg1, Exception innerException) : base(Strings.DxStoreAccessClientTransientException(errMsg1), innerException)
		{
			this.errMsg1 = errMsg1;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00013F3C File Offset: 0x0001213C
		protected DxStoreAccessClientTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg1 = (string)info.GetValue("errMsg1", typeof(string));
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00013F66 File Offset: 0x00012166
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg1", this.errMsg1);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00013F81 File Offset: 0x00012181
		public string ErrMsg1
		{
			get
			{
				return this.errMsg1;
			}
		}

		// Token: 0x04000284 RID: 644
		private readonly string errMsg1;
	}
}
