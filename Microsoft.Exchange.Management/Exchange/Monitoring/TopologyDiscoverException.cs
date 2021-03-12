using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001108 RID: 4360
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TopologyDiscoverException : LocalizedException
	{
		// Token: 0x0600B419 RID: 46105 RVA: 0x0029C4B9 File Offset: 0x0029A6B9
		public TopologyDiscoverException(string topologyDiscoverMode) : base(Strings.messageTopologyDiscoverException(topologyDiscoverMode))
		{
			this.topologyDiscoverMode = topologyDiscoverMode;
		}

		// Token: 0x0600B41A RID: 46106 RVA: 0x0029C4CE File Offset: 0x0029A6CE
		public TopologyDiscoverException(string topologyDiscoverMode, Exception innerException) : base(Strings.messageTopologyDiscoverException(topologyDiscoverMode), innerException)
		{
			this.topologyDiscoverMode = topologyDiscoverMode;
		}

		// Token: 0x0600B41B RID: 46107 RVA: 0x0029C4E4 File Offset: 0x0029A6E4
		protected TopologyDiscoverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.topologyDiscoverMode = (string)info.GetValue("topologyDiscoverMode", typeof(string));
		}

		// Token: 0x0600B41C RID: 46108 RVA: 0x0029C50E File Offset: 0x0029A70E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("topologyDiscoverMode", this.topologyDiscoverMode);
		}

		// Token: 0x17003916 RID: 14614
		// (get) Token: 0x0600B41D RID: 46109 RVA: 0x0029C529 File Offset: 0x0029A729
		public string TopologyDiscoverMode
		{
			get
			{
				return this.topologyDiscoverMode;
			}
		}

		// Token: 0x0400627C RID: 25212
		private readonly string topologyDiscoverMode;
	}
}
