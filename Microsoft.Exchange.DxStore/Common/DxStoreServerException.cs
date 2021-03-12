using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200009B RID: 155
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreServerException : LocalizedException
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x00014213 File Offset: 0x00012413
		public DxStoreServerException(string errMsg) : base(Strings.DxStoreServerException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00014228 File Offset: 0x00012428
		public DxStoreServerException(string errMsg, Exception innerException) : base(Strings.DxStoreServerException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001423E File Offset: 0x0001243E
		protected DxStoreServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00014268 File Offset: 0x00012468
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00014283 File Offset: 0x00012483
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x0400028A RID: 650
		private readonly string errMsg;
	}
}
