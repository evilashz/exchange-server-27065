using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A1 RID: 161
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreManagerServerTransientException : DxStoreServerTransientException
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001450B File Offset: 0x0001270B
		public DxStoreManagerServerTransientException(string errMsg5) : base(Strings.DxStoreManagerServerTransientException(errMsg5))
		{
			this.errMsg5 = errMsg5;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00014525 File Offset: 0x00012725
		public DxStoreManagerServerTransientException(string errMsg5, Exception innerException) : base(Strings.DxStoreManagerServerTransientException(errMsg5), innerException)
		{
			this.errMsg5 = errMsg5;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00014540 File Offset: 0x00012740
		protected DxStoreManagerServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg5 = (string)info.GetValue("errMsg5", typeof(string));
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001456A File Offset: 0x0001276A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg5", this.errMsg5);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00014585 File Offset: 0x00012785
		public string ErrMsg5
		{
			get
			{
				return this.errMsg5;
			}
		}

		// Token: 0x04000290 RID: 656
		private readonly string errMsg5;
	}
}
