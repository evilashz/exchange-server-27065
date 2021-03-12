using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000A9 RID: 169
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RuleCollectionNotInAdException : ExchangeConfigurationException
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00018092 File Offset: 0x00016292
		public RuleCollectionNotInAdException(string name) : base(TransportRulesStrings.RuleCollectionNotInAd(name))
		{
			this.name = name;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000180A7 File Offset: 0x000162A7
		public RuleCollectionNotInAdException(string name, Exception innerException) : base(TransportRulesStrings.RuleCollectionNotInAd(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000180BD File Offset: 0x000162BD
		protected RuleCollectionNotInAdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000180E7 File Offset: 0x000162E7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00018102 File Offset: 0x00016302
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040002E2 RID: 738
		private readonly string name;
	}
}
