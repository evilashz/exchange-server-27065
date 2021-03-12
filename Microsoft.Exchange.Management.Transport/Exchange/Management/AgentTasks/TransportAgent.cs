using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.AgentTasks
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class TransportAgent : ConfigurableObject
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004430 File Offset: 0x00002630
		internal TransportAgent(string identity, bool enabled, int priority, string agentFactory, string assemblyPath) : base(new SimpleProviderPropertyBag())
		{
			this.propertyBag[SimpleProviderObjectSchema.Identity] = new TransportAgentObjectId(identity);
			this.Enabled = enabled;
			this.Priority = priority;
			this.TransportAgentFactory = agentFactory;
			this.AssemblyPath = LocalLongFullPath.Parse(assemblyPath);
			base.ResetChangeTracking();
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004487 File Offset: 0x00002687
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000449E File Offset: 0x0000269E
		[Parameter(Mandatory = true)]
		public bool Enabled
		{
			get
			{
				return (bool)this.propertyBag[TransportAgentSchema.Enabled];
			}
			private set
			{
				this.propertyBag[TransportAgentSchema.Enabled] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000044B6 File Offset: 0x000026B6
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000044CD File Offset: 0x000026CD
		[Parameter(Mandatory = true)]
		public int Priority
		{
			get
			{
				return (int)this.propertyBag[TransportAgentSchema.Priority];
			}
			private set
			{
				this.propertyBag[TransportAgentSchema.Priority] = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000044E5 File Offset: 0x000026E5
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000044FC File Offset: 0x000026FC
		[Parameter(Mandatory = true)]
		public string TransportAgentFactory
		{
			get
			{
				return (string)this.propertyBag[TransportAgentSchema.AgentFactory];
			}
			private set
			{
				this.propertyBag[TransportAgentSchema.AgentFactory] = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000450F File Offset: 0x0000270F
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004526 File Offset: 0x00002726
		[Parameter(Mandatory = true)]
		public LocalLongFullPath AssemblyPath
		{
			get
			{
				return (LocalLongFullPath)this.propertyBag[TransportAgentSchema.AssemblyPath];
			}
			private set
			{
				this.propertyBag[TransportAgentSchema.AssemblyPath] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004539 File Offset: 0x00002739
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TransportAgent.schema;
			}
		}

		// Token: 0x04000032 RID: 50
		private static ObjectSchema schema = ObjectSchema.GetInstance<TransportAgentSchema>();
	}
}
