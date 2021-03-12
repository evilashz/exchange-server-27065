using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000413 RID: 1043
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToFindServerException : TransientException
	{
		// Token: 0x060029CE RID: 10702 RVA: 0x000BA449 File Offset: 0x000B8649
		public FailedToFindServerException(string serverName) : base(ReplayStrings.FailedToFindLocalServerException(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000BA45E File Offset: 0x000B865E
		public FailedToFindServerException(string serverName, Exception innerException) : base(ReplayStrings.FailedToFindLocalServerException(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000BA474 File Offset: 0x000B8674
		protected FailedToFindServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000BA49E File Offset: 0x000B869E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000BA4B9 File Offset: 0x000B86B9
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04001429 RID: 5161
		private readonly string serverName;
	}
}
