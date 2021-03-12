using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000099 RID: 153
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreManagerClientException : DxStoreClientException
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0001410F File Offset: 0x0001230F
		public DxStoreManagerClientException(string errMsg4) : base(Strings.DxStoreManagerClientException(errMsg4))
		{
			this.errMsg4 = errMsg4;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00014129 File Offset: 0x00012329
		public DxStoreManagerClientException(string errMsg4, Exception innerException) : base(Strings.DxStoreManagerClientException(errMsg4), innerException)
		{
			this.errMsg4 = errMsg4;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00014144 File Offset: 0x00012344
		protected DxStoreManagerClientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg4 = (string)info.GetValue("errMsg4", typeof(string));
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001416E File Offset: 0x0001236E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg4", this.errMsg4);
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00014189 File Offset: 0x00012389
		public string ErrMsg4
		{
			get
			{
				return this.errMsg4;
			}
		}

		// Token: 0x04000288 RID: 648
		private readonly string errMsg4;
	}
}
