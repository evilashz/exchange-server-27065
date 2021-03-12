using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Web;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EA5 RID: 3749
	internal abstract class QueryAdapter
	{
		// Token: 0x060061A1 RID: 24993 RVA: 0x00130BD7 File Offset: 0x0012EDD7
		protected QueryAdapter()
		{
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x00130BDF File Offset: 0x0012EDDF
		public QueryAdapter(EntitySchema entitySchema, ODataQueryOptions odataQueryOptions)
		{
			ArgumentValidator.ThrowIfNull("entitySchema", entitySchema);
			ArgumentValidator.ThrowIfNull("odataQueryOptions", odataQueryOptions);
			this.EntitySchema = entitySchema;
			this.ODataQueryOptions = odataQueryOptions;
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x060061A3 RID: 24995 RVA: 0x00130C0B File Offset: 0x0012EE0B
		// (set) Token: 0x060061A4 RID: 24996 RVA: 0x00130C13 File Offset: 0x0012EE13
		public EntitySchema EntitySchema { get; private set; }

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x060061A5 RID: 24997 RVA: 0x00130C1C File Offset: 0x0012EE1C
		// (set) Token: 0x060061A6 RID: 24998 RVA: 0x00130C24 File Offset: 0x0012EE24
		public ODataQueryOptions ODataQueryOptions { get; private set; }

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x060061A7 RID: 24999 RVA: 0x00130C2D File Offset: 0x0012EE2D
		public IList<PropertyDefinition> RequestedProperties
		{
			get
			{
				if (this.requestedProperties == null)
				{
					this.PopulateRequestedProperties();
				}
				return this.requestedProperties;
			}
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x00130C43 File Offset: 0x0012EE43
		public int GetPageSize()
		{
			return QueryAdapter.GetPageSize(this.ODataQueryOptions.Top);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x00130C58 File Offset: 0x0012EE58
		public static int GetPageSize(int? topParameter)
		{
			int result = 50;
			if (topParameter != null)
			{
				result = Math.Min(topParameter.Value, 500);
			}
			return result;
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x00130C84 File Offset: 0x0012EE84
		private void PopulateRequestedProperties()
		{
			this.requestedProperties = this.EntitySchema.DefaultProperties;
			if (this.ODataQueryOptions.Select != null)
			{
				this.requestedProperties = new List<PropertyDefinition>
				{
					EntitySchema.Id
				};
				foreach (string text in this.ODataQueryOptions.Select)
				{
					if (string.Equals(text, "*", StringComparison.OrdinalIgnoreCase))
					{
						this.requestedProperties = new List<PropertyDefinition>();
						using (IEnumerator<PropertyDefinition> enumerator = this.EntitySchema.AllProperties.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								PropertyDefinition propertyDefinition = enumerator.Current;
								if (propertyDefinition.Flags != PropertyDefinitionFlags.Navigation)
								{
									this.requestedProperties.Add(propertyDefinition);
								}
							}
							break;
						}
					}
					PropertyDefinition propertyDefinition2 = this.EntitySchema.ResolveProperty(text);
					if (!propertyDefinition2.IsNavigation)
					{
						this.requestedProperties.Add(propertyDefinition2);
					}
				}
			}
		}

		// Token: 0x040034CF RID: 13519
		private IList<PropertyDefinition> requestedProperties;
	}
}
