using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000097 RID: 151
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceClientException : DxStoreClientException
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001400B File Offset: 0x0001220B
		public DxStoreInstanceClientException(string errMsg2) : base(Strings.DxStoreInstanceClientException(errMsg2))
		{
			this.errMsg2 = errMsg2;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00014025 File Offset: 0x00012225
		public DxStoreInstanceClientException(string errMsg2, Exception innerException) : base(Strings.DxStoreInstanceClientException(errMsg2), innerException)
		{
			this.errMsg2 = errMsg2;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00014040 File Offset: 0x00012240
		protected DxStoreInstanceClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg2 = (string)info.GetValue("errMsg2", typeof(string));
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001406A File Offset: 0x0001226A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg2", this.errMsg2);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00014085 File Offset: 0x00012285
		public string ErrMsg2
		{
			get
			{
				return this.errMsg2;
			}
		}

		// Token: 0x04000286 RID: 646
		private readonly string errMsg2;
	}
}
