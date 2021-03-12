using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A0 RID: 160
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreManagerServerException : DxStoreServerException
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00014489 File Offset: 0x00012689
		public DxStoreManagerServerException(string errMsg4) : base(Strings.DxStoreManagerServerException(errMsg4))
		{
			this.errMsg4 = errMsg4;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000144A3 File Offset: 0x000126A3
		public DxStoreManagerServerException(string errMsg4, Exception innerException) : base(Strings.DxStoreManagerServerException(errMsg4), innerException)
		{
			this.errMsg4 = errMsg4;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000144BE File Offset: 0x000126BE
		protected DxStoreManagerServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg4 = (string)info.GetValue("errMsg4", typeof(string));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000144E8 File Offset: 0x000126E8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg4", this.errMsg4);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00014503 File Offset: 0x00012703
		public string ErrMsg4
		{
			get
			{
				return this.errMsg4;
			}
		}

		// Token: 0x0400028F RID: 655
		private readonly string errMsg4;
	}
}
