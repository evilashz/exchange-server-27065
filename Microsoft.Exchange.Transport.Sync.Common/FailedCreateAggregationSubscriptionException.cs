using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedCreateAggregationSubscriptionException : LocalizedException
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00004A65 File Offset: 0x00002C65
		public FailedCreateAggregationSubscriptionException(string name) : base(Strings.FailedCreateAggregationSubscription(name))
		{
			this.name = name;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004A7A File Offset: 0x00002C7A
		public FailedCreateAggregationSubscriptionException(string name, Exception innerException) : base(Strings.FailedCreateAggregationSubscription(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004A90 File Offset: 0x00002C90
		protected FailedCreateAggregationSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004ABA File Offset: 0x00002CBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004AD5 File Offset: 0x00002CD5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly string name;
	}
}
