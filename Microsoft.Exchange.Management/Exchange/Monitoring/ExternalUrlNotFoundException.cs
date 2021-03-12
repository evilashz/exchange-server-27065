using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001100 RID: 4352
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExternalUrlNotFoundException : LocalizedException
	{
		// Token: 0x0600B3E8 RID: 46056 RVA: 0x0029BDFD File Offset: 0x00299FFD
		public ExternalUrlNotFoundException(ServerIdParameter serverId, Type type) : base(Strings.messageExternalUrlNotFoundException(serverId, type))
		{
			this.serverId = serverId;
			this.type = type;
		}

		// Token: 0x0600B3E9 RID: 46057 RVA: 0x0029BE1A File Offset: 0x0029A01A
		public ExternalUrlNotFoundException(ServerIdParameter serverId, Type type, Exception innerException) : base(Strings.messageExternalUrlNotFoundException(serverId, type), innerException)
		{
			this.serverId = serverId;
			this.type = type;
		}

		// Token: 0x0600B3EA RID: 46058 RVA: 0x0029BE38 File Offset: 0x0029A038
		protected ExternalUrlNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverId = (ServerIdParameter)info.GetValue("serverId", typeof(ServerIdParameter));
			this.type = (Type)info.GetValue("type", typeof(Type));
		}

		// Token: 0x0600B3EB RID: 46059 RVA: 0x0029BE8D File Offset: 0x0029A08D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverId", this.serverId);
			info.AddValue("type", this.type);
		}

		// Token: 0x17003905 RID: 14597
		// (get) Token: 0x0600B3EC RID: 46060 RVA: 0x0029BEB9 File Offset: 0x0029A0B9
		public ServerIdParameter ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x17003906 RID: 14598
		// (get) Token: 0x0600B3ED RID: 46061 RVA: 0x0029BEC1 File Offset: 0x0029A0C1
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0400626B RID: 25195
		private readonly ServerIdParameter serverId;

		// Token: 0x0400626C RID: 25196
		private readonly Type type;
	}
}
