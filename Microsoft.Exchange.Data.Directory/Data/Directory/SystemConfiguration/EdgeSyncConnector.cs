using System;
using System.Management.Automation;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200042F RID: 1071
	[Serializable]
	public class EdgeSyncConnector : ADConfigurationObject
	{
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06003033 RID: 12339 RVA: 0x000C2436 File Offset: 0x000C0636
		// (set) Token: 0x06003034 RID: 12340 RVA: 0x000C2448 File Offset: 0x000C0648
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[EdgeSyncConnectorSchema.Enabled];
			}
			set
			{
				this[EdgeSyncConnectorSchema.Enabled] = value;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003035 RID: 12341 RVA: 0x000C245B File Offset: 0x000C065B
		// (set) Token: 0x06003036 RID: 12342 RVA: 0x000C246D File Offset: 0x000C066D
		public string SynchronizationProvider
		{
			get
			{
				return (string)this[EdgeSyncConnectorSchema.SynchronizationProvider];
			}
			internal set
			{
				this[EdgeSyncConnectorSchema.SynchronizationProvider] = value;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06003037 RID: 12343 RVA: 0x000C247B File Offset: 0x000C067B
		// (set) Token: 0x06003038 RID: 12344 RVA: 0x000C248D File Offset: 0x000C068D
		public string AssemblyPath
		{
			get
			{
				return (string)this[EdgeSyncConnectorSchema.AssemblyPath];
			}
			internal set
			{
				this[EdgeSyncConnectorSchema.AssemblyPath] = value;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000C249B File Offset: 0x000C069B
		internal override ADObjectSchema Schema
		{
			get
			{
				return ObjectSchema.GetInstance<EdgeSyncConnector.AllEdgeSyncConnectorProperties>();
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x000C24A2 File Offset: 0x000C06A2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchEdgeSyncConnector";
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000C24AC File Offset: 0x000C06AC
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchEdgeSyncMservConnector"),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "msExchEdgeSyncEhfConnector")
				});
			}
		}

		// Token: 0x04002074 RID: 8308
		private const string MostDerivedClass = "msExchEdgeSyncConnector";

		// Token: 0x04002075 RID: 8309
		private static readonly EdgeSyncConnectorSchema schema = ObjectSchema.GetInstance<EdgeSyncConnectorSchema>();

		// Token: 0x02000430 RID: 1072
		private class AllEdgeSyncConnectorProperties : ADPropertyUnionSchema
		{
			// Token: 0x17000DCD RID: 3533
			// (get) Token: 0x0600303E RID: 12350 RVA: 0x000C2514 File Offset: 0x000C0714
			public override ReadOnlyCollection<ADObjectSchema> ObjectSchemas
			{
				get
				{
					return EdgeSyncConnector.AllEdgeSyncConnectorProperties.edgeSyncConnectorSchema;
				}
			}

			// Token: 0x04002076 RID: 8310
			private static ReadOnlyCollection<ADObjectSchema> edgeSyncConnectorSchema = new ReadOnlyCollection<ADObjectSchema>(new ADObjectSchema[]
			{
				ObjectSchema.GetInstance<EdgeSyncEhfConnector.EdgeSyncEhfConnectorSchema>(),
				ObjectSchema.GetInstance<EdgeSyncMservConnectorSchema>()
			});
		}
	}
}
