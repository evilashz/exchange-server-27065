using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200009F RID: 159
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreInstanceServerTransientException : DxStoreServerTransientException
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x00014407 File Offset: 0x00012607
		public DxStoreInstanceServerTransientException(string errMsg3) : base(Strings.DxStoreInstanceServerTransientException(errMsg3))
		{
			this.errMsg3 = errMsg3;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00014421 File Offset: 0x00012621
		public DxStoreInstanceServerTransientException(string errMsg3, Exception innerException) : base(Strings.DxStoreInstanceServerTransientException(errMsg3), innerException)
		{
			this.errMsg3 = errMsg3;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001443C File Offset: 0x0001263C
		protected DxStoreInstanceServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg3 = (string)info.GetValue("errMsg3", typeof(string));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00014466 File Offset: 0x00012666
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg3", this.errMsg3);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00014481 File Offset: 0x00012681
		public string ErrMsg3
		{
			get
			{
				return this.errMsg3;
			}
		}

		// Token: 0x0400028E RID: 654
		private readonly string errMsg3;
	}
}
