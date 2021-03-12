using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000518 RID: 1304
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RegistryParameterException : LocalizedException
	{
		// Token: 0x06002F8D RID: 12173 RVA: 0x000C5C16 File Offset: 0x000C3E16
		public RegistryParameterException(string errorMsg) : base(ReplayStrings.RegistryParameterException(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000C5C2B File Offset: 0x000C3E2B
		public RegistryParameterException(string errorMsg, Exception innerException) : base(ReplayStrings.RegistryParameterException(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000C5C41 File Offset: 0x000C3E41
		protected RegistryParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000C5C6B File Offset: 0x000C3E6B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x000C5C86 File Offset: 0x000C3E86
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x040015D4 RID: 5588
		private readonly string errorMsg;
	}
}
