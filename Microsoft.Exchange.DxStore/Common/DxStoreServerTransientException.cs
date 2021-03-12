using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200009C RID: 156
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreServerTransientException : TransientException
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x0001428B File Offset: 0x0001248B
		public DxStoreServerTransientException(string errMsg) : base(Strings.DxStoreServerTransientException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000142A0 File Offset: 0x000124A0
		public DxStoreServerTransientException(string errMsg, Exception innerException) : base(Strings.DxStoreServerTransientException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000142B6 File Offset: 0x000124B6
		protected DxStoreServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000142E0 File Offset: 0x000124E0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000142FB File Offset: 0x000124FB
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x0400028B RID: 651
		private readonly string errMsg;
	}
}
