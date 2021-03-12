using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200009D RID: 157
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreAccessServerTransientException : DxStoreServerTransientException
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x00014303 File Offset: 0x00012503
		public DxStoreAccessServerTransientException(string errMsg1) : base(Strings.DxStoreAccessServerTransientException(errMsg1))
		{
			this.errMsg1 = errMsg1;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001431D File Offset: 0x0001251D
		public DxStoreAccessServerTransientException(string errMsg1, Exception innerException) : base(Strings.DxStoreAccessServerTransientException(errMsg1), innerException)
		{
			this.errMsg1 = errMsg1;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00014338 File Offset: 0x00012538
		protected DxStoreAccessServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg1 = (string)info.GetValue("errMsg1", typeof(string));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00014362 File Offset: 0x00012562
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg1", this.errMsg1);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0001437D File Offset: 0x0001257D
		public string ErrMsg1
		{
			get
			{
				return this.errMsg1;
			}
		}

		// Token: 0x0400028C RID: 652
		private readonly string errMsg1;
	}
}
