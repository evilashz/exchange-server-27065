using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000096 RID: 150
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreAccessClientException : DxStoreClientException
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x00013F89 File Offset: 0x00012189
		public DxStoreAccessClientException(string errMsg2) : base(Strings.DxStoreAccessClientException(errMsg2))
		{
			this.errMsg2 = errMsg2;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00013FA3 File Offset: 0x000121A3
		public DxStoreAccessClientException(string errMsg2, Exception innerException) : base(Strings.DxStoreAccessClientException(errMsg2), innerException)
		{
			this.errMsg2 = errMsg2;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00013FBE File Offset: 0x000121BE
		protected DxStoreAccessClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg2 = (string)info.GetValue("errMsg2", typeof(string));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00013FE8 File Offset: 0x000121E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg2", this.errMsg2);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00014003 File Offset: 0x00012203
		public string ErrMsg2
		{
			get
			{
				return this.errMsg2;
			}
		}

		// Token: 0x04000285 RID: 645
		private readonly string errMsg2;
	}
}
