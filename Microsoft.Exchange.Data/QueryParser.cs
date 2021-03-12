using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Generated;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200024D RID: 589
	public class QueryParser
	{
		// Token: 0x06001416 RID: 5142 RVA: 0x0003F7E4 File Offset: 0x0003D9E4
		internal QueryParser(string query, ObjectSchema schema, QueryParser.Capabilities capabilities, QueryParser.EvaluateVariableDelegate evalDelegate, QueryParser.ConvertValueFromStringDelegate convertDelegate)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			Hashtable allFilterable = new Hashtable(schema.AllFilterableProperties.Count, StringComparer.OrdinalIgnoreCase);
			foreach (PropertyDefinition propertyDefinition in schema.AllFilterableProperties)
			{
				allFilterable.Add(propertyDefinition.Name, propertyDefinition);
			}
			this.parser = new Parser(query, capabilities, (string propName) => (PropertyDefinition)allFilterable[propName], null, evalDelegate, convertDelegate);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0003F898 File Offset: 0x0003DA98
		internal QueryParser(string query, QueryParser.Capabilities capabilities, QueryParser.LookupPropertyDelegate schemaLookupDelegate, QueryParser.ListKnownPropertiesDelegate listKnownPropertiesDelegate, QueryParser.EvaluateVariableDelegate evalDelegate, QueryParser.ConvertValueFromStringDelegate convertDelegate)
		{
			this.parser = new Parser(query, capabilities, schemaLookupDelegate, listKnownPropertiesDelegate, evalDelegate, convertDelegate);
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0003F8B4 File Offset: 0x0003DAB4
		public QueryFilter ParseTree
		{
			get
			{
				return this.parser.ParseTree;
			}
		}

		// Token: 0x04000BDF RID: 3039
		private Parser parser;

		// Token: 0x0200024E RID: 590
		// (Invoke) Token: 0x0600141A RID: 5146
		public delegate object EvaluateVariableDelegate(string varName);

		// Token: 0x0200024F RID: 591
		// (Invoke) Token: 0x0600141E RID: 5150
		public delegate object ConvertValueFromStringDelegate(object value, Type targetType);

		// Token: 0x02000250 RID: 592
		// (Invoke) Token: 0x06001422 RID: 5154
		internal delegate PropertyDefinition LookupPropertyDelegate(string propName);

		// Token: 0x02000251 RID: 593
		// (Invoke) Token: 0x06001426 RID: 5158
		internal delegate IEnumerable<PropertyDefinition> ListKnownPropertiesDelegate();

		// Token: 0x02000252 RID: 594
		[Flags]
		public enum Capabilities
		{
			// Token: 0x04000BE1 RID: 3041
			Or = 1,
			// Token: 0x04000BE2 RID: 3042
			Like = 2,
			// Token: 0x04000BE3 RID: 3043
			NotLike = 4,
			// Token: 0x04000BE4 RID: 3044
			All = 65535
		}
	}
}
