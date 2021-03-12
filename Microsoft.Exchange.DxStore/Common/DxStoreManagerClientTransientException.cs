using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200009A RID: 154
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreManagerClientTransientException : DxStoreClientTransientException
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00014191 File Offset: 0x00012391
		public DxStoreManagerClientTransientException(string errMsg5) : base(Strings.DxStoreManagerClientTransientException(errMsg5))
		{
			this.errMsg5 = errMsg5;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000141AB File Offset: 0x000123AB
		public DxStoreManagerClientTransientException(string errMsg5, Exception innerException) : base(Strings.DxStoreManagerClientTransientException(errMsg5), innerException)
		{
			this.errMsg5 = errMsg5;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000141C6 File Offset: 0x000123C6
		protected DxStoreManagerClientTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg5 = (string)info.GetValue("errMsg5", typeof(string));
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000141F0 File Offset: 0x000123F0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg5", this.errMsg5);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001420B File Offset: 0x0001240B
		public string ErrMsg5
		{
			get
			{
				return this.errMsg5;
			}
		}

		// Token: 0x04000289 RID: 649
		private readonly string errMsg5;
	}
}
