using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000093 RID: 147
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreClientException : LocalizedException
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x00013E17 File Offset: 0x00012017
		public DxStoreClientException(string errMsg) : base(Strings.DxStoreClientException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00013E2C File Offset: 0x0001202C
		public DxStoreClientException(string errMsg, Exception innerException) : base(Strings.DxStoreClientException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00013E42 File Offset: 0x00012042
		protected DxStoreClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00013E6C File Offset: 0x0001206C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00013E87 File Offset: 0x00012087
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04000282 RID: 642
		private readonly string errMsg;
	}
}
