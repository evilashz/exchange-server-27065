using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000094 RID: 148
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreClientTransientException : TransientException
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00013E8F File Offset: 0x0001208F
		public DxStoreClientTransientException(string errMsg) : base(Strings.DxStoreClientTransientException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00013EA4 File Offset: 0x000120A4
		public DxStoreClientTransientException(string errMsg, Exception innerException) : base(Strings.DxStoreClientTransientException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00013EBA File Offset: 0x000120BA
		protected DxStoreClientTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00013EE4 File Offset: 0x000120E4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00013EFF File Offset: 0x000120FF
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04000283 RID: 643
		private readonly string errMsg;
	}
}
