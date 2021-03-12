using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000416 RID: 1046
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedingChannelIsClosedException : LocalizedException
	{
		// Token: 0x060029DE RID: 10718 RVA: 0x000BA611 File Offset: 0x000B8811
		public SeedingChannelIsClosedException(Guid g) : base(ReplayStrings.SeedingChannelIsClosedException(g))
		{
			this.g = g;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000BA626 File Offset: 0x000B8826
		public SeedingChannelIsClosedException(Guid g, Exception innerException) : base(ReplayStrings.SeedingChannelIsClosedException(g), innerException)
		{
			this.g = g;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000BA63C File Offset: 0x000B883C
		protected SeedingChannelIsClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.g = (Guid)info.GetValue("g", typeof(Guid));
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000BA666 File Offset: 0x000B8866
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("g", this.g);
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x000BA686 File Offset: 0x000B8886
		public Guid G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x0400142D RID: 5165
		private readonly Guid g;
	}
}
