using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000519 RID: 1305
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterReadException : RegistryParameterException
	{
		// Token: 0x06002F92 RID: 12178 RVA: 0x000C5C8E File Offset: 0x000C3E8E
		public RegistryParameterReadException(string valueName, string errMsg) : base(ReplayStrings.RegistryParameterReadException(valueName, errMsg))
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000C5CB0 File Offset: 0x000C3EB0
		public RegistryParameterReadException(string valueName, string errMsg, Exception innerException) : base(ReplayStrings.RegistryParameterReadException(valueName, errMsg), innerException)
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000C5CD4 File Offset: 0x000C3ED4
		protected RegistryParameterReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.valueName = (string)info.GetValue("valueName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000C5D29 File Offset: 0x000C3F29
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("valueName", this.valueName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06002F96 RID: 12182 RVA: 0x000C5D55 File Offset: 0x000C3F55
		public string ValueName
		{
			get
			{
				return this.valueName;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x000C5D5D File Offset: 0x000C3F5D
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040015D5 RID: 5589
		private readonly string valueName;

		// Token: 0x040015D6 RID: 5590
		private readonly string errMsg;
	}
}
