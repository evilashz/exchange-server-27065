using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000E3 RID: 227
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterWriteException : RegistryParameterException
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0001C8AD File Offset: 0x0001AAAD
		public RegistryParameterWriteException(string valueName, string errMsg) : base(Strings.RegistryParameterWriteException(valueName, errMsg))
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001C8CF File Offset: 0x0001AACF
		public RegistryParameterWriteException(string valueName, string errMsg, Exception innerException) : base(Strings.RegistryParameterWriteException(valueName, errMsg), innerException)
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		protected RegistryParameterWriteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.valueName = (string)info.GetValue("valueName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001C949 File Offset: 0x0001AB49
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("valueName", this.valueName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001C975 File Offset: 0x0001AB75
		public string ValueName
		{
			get
			{
				return this.valueName;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001C97D File Offset: 0x0001AB7D
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x0400073D RID: 1853
		private readonly string valueName;

		// Token: 0x0400073E RID: 1854
		private readonly string errMsg;
	}
}
