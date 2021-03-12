using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200000C RID: 12
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedDeleteAggregationSubscriptionException : LocalizedException
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00004ADD File Offset: 0x00002CDD
		public FailedDeleteAggregationSubscriptionException(string name) : base(Strings.FailedDeleteAggregationSubscription(name))
		{
			this.name = name;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004AF2 File Offset: 0x00002CF2
		public FailedDeleteAggregationSubscriptionException(string name, Exception innerException) : base(Strings.FailedDeleteAggregationSubscription(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004B08 File Offset: 0x00002D08
		protected FailedDeleteAggregationSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004B32 File Offset: 0x00002D32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004B4D File Offset: 0x00002D4D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040000D0 RID: 208
		private readonly string name;
	}
}
