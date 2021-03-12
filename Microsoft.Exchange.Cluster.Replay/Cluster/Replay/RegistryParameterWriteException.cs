using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200051A RID: 1306
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterWriteException : RegistryParameterException
	{
		// Token: 0x06002F98 RID: 12184 RVA: 0x000C5D65 File Offset: 0x000C3F65
		public RegistryParameterWriteException(string valueName, string errMsg) : base(ReplayStrings.RegistryParameterWriteException(valueName, errMsg))
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000C5D87 File Offset: 0x000C3F87
		public RegistryParameterWriteException(string valueName, string errMsg, Exception innerException) : base(ReplayStrings.RegistryParameterWriteException(valueName, errMsg), innerException)
		{
			this.valueName = valueName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000C5DAC File Offset: 0x000C3FAC
		protected RegistryParameterWriteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.valueName = (string)info.GetValue("valueName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000C5E01 File Offset: 0x000C4001
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("valueName", this.valueName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000C5E2D File Offset: 0x000C402D
		public string ValueName
		{
			get
			{
				return this.valueName;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000C5E35 File Offset: 0x000C4035
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040015D7 RID: 5591
		private readonly string valueName;

		// Token: 0x040015D8 RID: 5592
		private readonly string errMsg;
	}
}
