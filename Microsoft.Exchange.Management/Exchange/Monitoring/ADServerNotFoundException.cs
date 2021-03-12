using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001103 RID: 4355
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADServerNotFoundException : LocalizedException
	{
		// Token: 0x0600B3FA RID: 46074 RVA: 0x0029C061 File Offset: 0x0029A261
		public ADServerNotFoundException(string serverId) : base(Strings.messageADServerNotFoundException(serverId))
		{
			this.serverId = serverId;
		}

		// Token: 0x0600B3FB RID: 46075 RVA: 0x0029C076 File Offset: 0x0029A276
		public ADServerNotFoundException(string serverId, Exception innerException) : base(Strings.messageADServerNotFoundException(serverId), innerException)
		{
			this.serverId = serverId;
		}

		// Token: 0x0600B3FC RID: 46076 RVA: 0x0029C08C File Offset: 0x0029A28C
		protected ADServerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverId = (string)info.GetValue("serverId", typeof(string));
		}

		// Token: 0x0600B3FD RID: 46077 RVA: 0x0029C0B6 File Offset: 0x0029A2B6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverId", this.serverId);
		}

		// Token: 0x1700390B RID: 14603
		// (get) Token: 0x0600B3FE RID: 46078 RVA: 0x0029C0D1 File Offset: 0x0029A2D1
		public string ServerId
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x04006271 RID: 25201
		private readonly string serverId;
	}
}
