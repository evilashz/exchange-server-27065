using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000418 RID: 1048
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedingAnotherServerException : TaskServerException
	{
		// Token: 0x060029E9 RID: 10729 RVA: 0x000BA762 File Offset: 0x000B8962
		public SeedingAnotherServerException(string seedingServerName, string requestServerName) : base(ReplayStrings.SeedingAnotherServerException(seedingServerName, requestServerName))
		{
			this.seedingServerName = seedingServerName;
			this.requestServerName = requestServerName;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000BA784 File Offset: 0x000B8984
		public SeedingAnotherServerException(string seedingServerName, string requestServerName, Exception innerException) : base(ReplayStrings.SeedingAnotherServerException(seedingServerName, requestServerName), innerException)
		{
			this.seedingServerName = seedingServerName;
			this.requestServerName = requestServerName;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000BA7A8 File Offset: 0x000B89A8
		protected SeedingAnotherServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.seedingServerName = (string)info.GetValue("seedingServerName", typeof(string));
			this.requestServerName = (string)info.GetValue("requestServerName", typeof(string));
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000BA7FD File Offset: 0x000B89FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("seedingServerName", this.seedingServerName);
			info.AddValue("requestServerName", this.requestServerName);
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x000BA829 File Offset: 0x000B8A29
		public string SeedingServerName
		{
			get
			{
				return this.seedingServerName;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000BA831 File Offset: 0x000B8A31
		public string RequestServerName
		{
			get
			{
				return this.requestServerName;
			}
		}

		// Token: 0x04001430 RID: 5168
		private readonly string seedingServerName;

		// Token: 0x04001431 RID: 5169
		private readonly string requestServerName;
	}
}
