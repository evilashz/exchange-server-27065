using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E1 RID: 225
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterException : LocalizedException
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0001C75C File Offset: 0x0001A95C
		public RegistryParameterException(string errorMsg) : base(Strings.RegistryParameterException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001C771 File Offset: 0x0001A971
		public RegistryParameterException(string errorMsg, Exception innerException) : base(Strings.RegistryParameterException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001C787 File Offset: 0x0001A987
		protected RegistryParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001C7B1 File Offset: 0x0001A9B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x0400073A RID: 1850
		private readonly string errorMsg;
	}
}
