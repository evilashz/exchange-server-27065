using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F50 RID: 3920
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyParseSchema
	{
		// Token: 0x06008680 RID: 34432 RVA: 0x0024E988 File Offset: 0x0024CB88
		public PropertyParseSchema(string label, SimpleProviderPropertyDefinition propertyDefinition, Func<string, object> propertyParser)
		{
			this.Label = label;
			this.Property = propertyDefinition;
			if (propertyParser == null)
			{
				this.PropertyParser = delegate(string line)
				{
					if (!string.IsNullOrEmpty(line))
					{
						return line;
					}
					return null;
				};
				return;
			}
			this.PropertyParser = propertyParser;
		}

		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x06008681 RID: 34433 RVA: 0x0024E9D7 File Offset: 0x0024CBD7
		// (set) Token: 0x06008682 RID: 34434 RVA: 0x0024E9DF File Offset: 0x0024CBDF
		public string Label { get; private set; }

		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x06008683 RID: 34435 RVA: 0x0024E9E8 File Offset: 0x0024CBE8
		// (set) Token: 0x06008684 RID: 34436 RVA: 0x0024E9F0 File Offset: 0x0024CBF0
		public SimpleProviderPropertyDefinition Property { get; private set; }

		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x06008685 RID: 34437 RVA: 0x0024E9F9 File Offset: 0x0024CBF9
		// (set) Token: 0x06008686 RID: 34438 RVA: 0x0024EA01 File Offset: 0x0024CC01
		public Func<string, object> PropertyParser { get; private set; }
	}
}
