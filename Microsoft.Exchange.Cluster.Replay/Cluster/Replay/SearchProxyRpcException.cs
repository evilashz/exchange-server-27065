using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000453 RID: 1107
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SearchProxyRpcException : LocalizedException
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x000BD171 File Offset: 0x000BB371
		public SearchProxyRpcException(string msg) : base(ReplayStrings.SearchProxyRpcException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000BD186 File Offset: 0x000BB386
		public SearchProxyRpcException(string msg, Exception innerException) : base(ReplayStrings.SearchProxyRpcException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000BD19C File Offset: 0x000BB39C
		protected SearchProxyRpcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000BD1C6 File Offset: 0x000BB3C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000BD1E1 File Offset: 0x000BB3E1
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04001492 RID: 5266
		private readonly string msg;
	}
}
