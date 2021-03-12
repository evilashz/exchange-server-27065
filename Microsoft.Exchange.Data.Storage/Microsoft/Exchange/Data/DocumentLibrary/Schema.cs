using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x02000699 RID: 1689
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Schema
	{
		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x00125CF5 File Offset: 0x00123EF5
		public static Schema Instance
		{
			get
			{
				if (Schema.instance == null)
				{
					Schema.instance = new Schema();
				}
				return Schema.instance;
			}
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x00125D10 File Offset: 0x00123F10
		protected internal Schema()
		{
			this.allProperties = new Dictionary<PropertyDefinition, DocumentLibraryPropertyDefinition>();
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			FieldInfo[] fields = base.GetType().GetFields(bindingAttr);
			foreach (FieldInfo fieldInfo in fields)
			{
				object value = fieldInfo.GetValue(null);
				DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = value as DocumentLibraryPropertyDefinition;
				if (documentLibraryPropertyDefinition != null)
				{
					if (documentLibraryPropertyDefinition.PropertyId != DocumentLibraryPropertyId.None)
					{
						this.idToPropertyMap.Add(documentLibraryPropertyDefinition.PropertyId, documentLibraryPropertyDefinition);
					}
					this.allProperties.Add(documentLibraryPropertyDefinition, documentLibraryPropertyDefinition);
				}
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x00125DB0 File Offset: 0x00123FB0
		internal Dictionary<PropertyDefinition, DocumentLibraryPropertyDefinition> AllProperties
		{
			get
			{
				return this.allProperties;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x00125DB8 File Offset: 0x00123FB8
		internal Dictionary<DocumentLibraryPropertyId, DocumentLibraryPropertyDefinition> IdToPropertyMap
		{
			get
			{
				return this.idToPropertyMap;
			}
		}

		// Token: 0x0400258B RID: 9611
		private static Schema instance;

		// Token: 0x0400258C RID: 9612
		private Dictionary<PropertyDefinition, DocumentLibraryPropertyDefinition> allProperties = new Dictionary<PropertyDefinition, DocumentLibraryPropertyDefinition>();

		// Token: 0x0400258D RID: 9613
		private Dictionary<DocumentLibraryPropertyId, DocumentLibraryPropertyDefinition> idToPropertyMap = new Dictionary<DocumentLibraryPropertyId, DocumentLibraryPropertyDefinition>();
	}
}
