using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedSetAggregationSubscriptionException : LocalizedException
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004BD2 File Offset: 0x00002DD2
		public FailedSetAggregationSubscriptionException(string name) : base(Strings.FailedSetAggregationSubscription(name))
		{
			this.name = name;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public FailedSetAggregationSubscriptionException(string name, Exception innerException) : base(Strings.FailedSetAggregationSubscription(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004BFD File Offset: 0x00002DFD
		protected FailedSetAggregationSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004C27 File Offset: 0x00002E27
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004C42 File Offset: 0x00002E42
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040000D2 RID: 210
		private readonly string name;
	}
}
