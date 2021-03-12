using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001039 RID: 4153
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasServerNotRpcEnabledException : LocalizedException
	{
		// Token: 0x0600AFD5 RID: 45013 RVA: 0x00294F35 File Offset: 0x00293135
		public CasServerNotRpcEnabledException(string server) : base(Strings.ErrorCasServerNotRpcEnabled(server))
		{
			this.server = server;
		}

		// Token: 0x0600AFD6 RID: 45014 RVA: 0x00294F4A File Offset: 0x0029314A
		public CasServerNotRpcEnabledException(string server, Exception innerException) : base(Strings.ErrorCasServerNotRpcEnabled(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600AFD7 RID: 45015 RVA: 0x00294F60 File Offset: 0x00293160
		protected CasServerNotRpcEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600AFD8 RID: 45016 RVA: 0x00294F8A File Offset: 0x0029318A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700380E RID: 14350
		// (get) Token: 0x0600AFD9 RID: 45017 RVA: 0x00294FA5 File Offset: 0x002931A5
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04006174 RID: 24948
		private readonly string server;
	}
}
