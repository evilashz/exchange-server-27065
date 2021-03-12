using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001101 RID: 4353
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClientAccessArrayNotFoundException : LocalizedException
	{
		// Token: 0x0600B3EE RID: 46062 RVA: 0x0029BEC9 File Offset: 0x0029A0C9
		public ClientAccessArrayNotFoundException(string site, ServerIdParameter serverId) : base(Strings.messageClientAccessArrayNotFoundException(site, serverId))
		{
			this.site = site;
			this.serverId = serverId;
		}

		// Token: 0x0600B3EF RID: 46063 RVA: 0x0029BEE6 File Offset: 0x0029A0E6
		public ClientAccessArrayNotFoundException(string site, ServerIdParameter serverId, Exception innerException) : base(Strings.messageClientAccessArrayNotFoundException(site, serverId), innerException)
		{
			this.site = site;
			this.serverId = serverId;
		}

		// Token: 0x0600B3F0 RID: 46064 RVA: 0x0029BF04 File Offset: 0x0029A104
		protected ClientAccessArrayNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.site = (string)info.GetValue("site", typeof(string));
			this.serverId = (ServerIdParameter)info.GetValue("serverId", typeof(ServerIdParameter));
		}

		// Token: 0x0600B3F1 RID: 46065 RVA: 0x0029BF59 File Offset: 0x0029A159
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("site", this.site);
			info.AddValue("serverId", this.serverId);
		}

		// Token: 0x17003907 RID: 14599
		// (get) Token: 0x0600B3F2 RID: 46066 RVA: 0x0029BF85 File Offset: 0x0029A185
		public string Site
		{
			get
			{
				return this.site;
			}
		}

		// Token: 0x17003908 RID: 14600
		// (get) Token: 0x0600B3F3 RID: 46067 RVA: 0x0029BF8D File Offset: 0x0029A18D
		public ServerIdParameter ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x0400626D RID: 25197
		private readonly string site;

		// Token: 0x0400626E RID: 25198
		private readonly ServerIdParameter serverId;
	}
}
