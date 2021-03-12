using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C6 RID: 1222
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ManagementApiException : LocalizedException
	{
		// Token: 0x06002DBC RID: 11708 RVA: 0x000C212D File Offset: 0x000C032D
		public ManagementApiException(string api) : base(ReplayStrings.ManagementApiError(api))
		{
			this.api = api;
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000C2142 File Offset: 0x000C0342
		public ManagementApiException(string api, Exception innerException) : base(ReplayStrings.ManagementApiError(api), innerException)
		{
			this.api = api;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000C2158 File Offset: 0x000C0358
		protected ManagementApiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.api = (string)info.GetValue("api", typeof(string));
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000C2182 File Offset: 0x000C0382
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("api", this.api);
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x000C219D File Offset: 0x000C039D
		public string Api
		{
			get
			{
				return this.api;
			}
		}

		// Token: 0x0400154B RID: 5451
		private readonly string api;
	}
}
