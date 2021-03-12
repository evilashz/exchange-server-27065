using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class TransportEvent : ConfigurableObject
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000474B File Offset: 0x0000294B
		internal TransportEvent(string eventTopic, IEnumerable<string> transportAgentIdentities) : base(new SimpleProviderPropertyBag())
		{
			this.Event = eventTopic;
			this.TransportAgents = transportAgentIdentities;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004766 File Offset: 0x00002966
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000477D File Offset: 0x0000297D
		public string Event
		{
			get
			{
				return (string)this.propertyBag[TransportEventSchema.EventTopic];
			}
			private set
			{
				this.propertyBag[TransportEventSchema.EventTopic] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004790 File Offset: 0x00002990
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000047A7 File Offset: 0x000029A7
		public IEnumerable<string> TransportAgents
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[TransportEventSchema.TransportAgentIdentities];
			}
			private set
			{
				this.propertyBag[TransportEventSchema.TransportAgentIdentities] = new MultiValuedProperty<string>(new List<string>(value));
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000047C4 File Offset: 0x000029C4
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TransportEvent.schema;
			}
		}

		// Token: 0x04000039 RID: 57
		private static ObjectSchema schema = ObjectSchema.GetInstance<TransportEventSchema>();
	}
}
