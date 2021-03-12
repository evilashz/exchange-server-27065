using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006ED RID: 1773
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SharepointPropertyDefinition : DocumentLibraryPropertyDefinition
	{
		// Token: 0x06004665 RID: 18021 RVA: 0x0012BFC8 File Offset: 0x0012A1C8
		internal static SharepointPropertyDefinition PropertyDefinitionToSharepointPropertyDefinition(Schema schema, PropertyDefinition propDef)
		{
			SharepointPropertyDefinition sharepointPropertyDefinition = propDef as SharepointPropertyDefinition;
			DocumentLibraryPropertyDefinition documentLibraryPropertyDefinition = propDef as DocumentLibraryPropertyDefinition;
			if (documentLibraryPropertyDefinition == null)
			{
				throw new ArgumentException("propDefs");
			}
			if (sharepointPropertyDefinition == null && schema.IdToPropertyMap.ContainsKey(documentLibraryPropertyDefinition.PropertyId))
			{
				sharepointPropertyDefinition = (schema.IdToPropertyMap[documentLibraryPropertyDefinition.PropertyId] as SharepointPropertyDefinition);
			}
			return sharepointPropertyDefinition;
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x0012C020 File Offset: 0x0012A220
		internal static SharepointPropertyDefinition[] PropertyDefinitionsToSharepointPropertyDefinitions(Schema schema, ICollection<PropertyDefinition> propDefs)
		{
			List<SharepointPropertyDefinition> list = new List<SharepointPropertyDefinition>(propDefs.Count);
			foreach (PropertyDefinition propDef in propDefs)
			{
				list.Add(SharepointPropertyDefinition.PropertyDefinitionToSharepointPropertyDefinition(schema, propDef));
			}
			return list.ToArray();
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x0012C080 File Offset: 0x0012A280
		internal SharepointPropertyDefinition(string displayName, Type propType, DocumentLibraryPropertyId propertyId, string name, SharepointFieldType fieldType, SharepointPropertyDefinition.MarshalTypeToSharepoint clrToSharepoint, SharepointPropertyDefinition.MarshalTypeFromSharepoint sharepointToClr, object defaultValue) : base(displayName, propType, defaultValue, propertyId)
		{
			this.name = name;
			this.fieldType = fieldType;
			this.clrToSharepoint = clrToSharepoint;
			this.sharepointToClr = sharepointToClr;
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06004668 RID: 18024 RVA: 0x0012C0AD File Offset: 0x0012A2AD
		internal string SharepointName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06004669 RID: 18025 RVA: 0x0012C0B5 File Offset: 0x0012A2B5
		internal string FieldTypeAsString
		{
			get
			{
				return this.fieldType.ToString();
			}
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x0012C0C7 File Offset: 0x0012A2C7
		internal virtual string BuildFieldRef()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x0012C0CE File Offset: 0x0012A2CE
		internal SharepointPropertyDefinition.MarshalTypeToSharepoint ToSharepoint
		{
			get
			{
				return this.clrToSharepoint;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x0600466C RID: 18028 RVA: 0x0012C0D6 File Offset: 0x0012A2D6
		internal SharepointPropertyDefinition.MarshalTypeFromSharepoint FromSharepoint
		{
			get
			{
				return this.sharepointToClr;
			}
		}

		// Token: 0x04002693 RID: 9875
		private readonly string name;

		// Token: 0x04002694 RID: 9876
		private readonly SharepointFieldType fieldType;

		// Token: 0x04002695 RID: 9877
		private readonly SharepointPropertyDefinition.MarshalTypeToSharepoint clrToSharepoint;

		// Token: 0x04002696 RID: 9878
		private readonly SharepointPropertyDefinition.MarshalTypeFromSharepoint sharepointToClr;

		// Token: 0x020006EE RID: 1774
		// (Invoke) Token: 0x0600466E RID: 18030
		internal delegate string MarshalTypeToSharepoint(object obj, CultureInfo cultureInfo);

		// Token: 0x020006EF RID: 1775
		// (Invoke) Token: 0x06004672 RID: 18034
		internal delegate object MarshalTypeFromSharepoint(string obj, CultureInfo cultureInfo);
	}
}
