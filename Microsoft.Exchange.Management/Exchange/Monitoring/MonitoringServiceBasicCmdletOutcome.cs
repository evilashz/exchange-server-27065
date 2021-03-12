using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000578 RID: 1400
	[Serializable]
	public class MonitoringServiceBasicCmdletOutcome : ConfigurableObject
	{
		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000C8C9D File Offset: 0x000C6E9D
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MonitoringServiceBasicCmdletOutcome.schema;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000C8CA4 File Offset: 0x000C6EA4
		// (set) Token: 0x06003152 RID: 12626 RVA: 0x000C8CB6 File Offset: 0x000C6EB6
		public string Server
		{
			get
			{
				return (string)this[MonitoringServiceBasicCmdletOutcomeSchema.Server];
			}
			internal set
			{
				this[MonitoringServiceBasicCmdletOutcomeSchema.Server] = value;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000C8CC4 File Offset: 0x000C6EC4
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x000C8CD6 File Offset: 0x000C6ED6
		public MonitoringServiceBasicCmdletResult Result
		{
			get
			{
				return (MonitoringServiceBasicCmdletResult)this[MonitoringServiceBasicCmdletOutcomeSchema.Result];
			}
			internal set
			{
				this[MonitoringServiceBasicCmdletOutcomeSchema.Result] = value;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000C8CE4 File Offset: 0x000C6EE4
		// (set) Token: 0x06003156 RID: 12630 RVA: 0x000C8CFB File Offset: 0x000C6EFB
		public string Error
		{
			get
			{
				return (string)this.propertyBag[MonitoringServiceBasicCmdletOutcomeSchema.Error];
			}
			internal set
			{
				this.propertyBag[MonitoringServiceBasicCmdletOutcomeSchema.Error] = value;
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000C8D0E File Offset: 0x000C6F0E
		public MonitoringServiceBasicCmdletOutcome(string server) : base(new SimpleProviderPropertyBag())
		{
			this.Server = server;
			this.Result = new MonitoringServiceBasicCmdletResult(MonitoringServiceBasicCmdletResultEnum.Undefined);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000C8D3C File Offset: 0x000C6F3C
		internal void Update(MonitoringServiceBasicCmdletResultEnum resultEnum, string error)
		{
			lock (this.thisLock)
			{
				this.Result = new MonitoringServiceBasicCmdletResult(resultEnum);
				this.Error = (error ?? string.Empty);
			}
		}

		// Token: 0x040022EE RID: 8942
		[NonSerialized]
		private object thisLock = new object();

		// Token: 0x040022EF RID: 8943
		private static MonitoringServiceBasicCmdletOutcomeSchema schema = ObjectSchema.GetInstance<MonitoringServiceBasicCmdletOutcomeSchema>();
	}
}
