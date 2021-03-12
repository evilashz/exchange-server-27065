using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E2 RID: 226
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterReadException : RegistryParameterException
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
		public RegistryParameterReadException(string valueName, string errMsg) : base(Strings.RegistryParameterReadException(valueName, errMsg))
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001C7F6 File Offset: 0x0001A9F6
		public RegistryParameterReadException(string valueName, string errMsg, Exception innerException) : base(Strings.RegistryParameterReadException(valueName, errMsg), innerException)
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001C81C File Offset: 0x0001AA1C
		protected RegistryParameterReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.valueName = (string)info.GetValue("valueName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001C871 File Offset: 0x0001AA71
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("valueName", this.valueName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0001C89D File Offset: 0x0001AA9D
		public string ValueName
		{
			get
			{
				return this.valueName;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001C8A5 File Offset: 0x0001AAA5
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x0400073B RID: 1851
		private readonly string valueName;

		// Token: 0x0400073C RID: 1852
		private readonly string errMsg;
	}
}
