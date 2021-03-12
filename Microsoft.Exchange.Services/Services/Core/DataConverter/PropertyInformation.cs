using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000205 RID: 517
	internal class PropertyInformation : XmlElementInformation
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x00043780 File Offset: 0x00041980
		static PropertyInformation()
		{
			PropertyInformation.commandInterfaceAttributes.Add(typeof(IAppendUpdateCommand), PropertyInformationAttributes.ImplementsAppendUpdateCommand);
			PropertyInformation.commandInterfaceAttributes.Add(typeof(IDeleteUpdateCommand), PropertyInformationAttributes.ImplementsDeleteUpdateCommand);
			PropertyInformation.commandInterfaceAttributes.Add(typeof(ISetCommand), PropertyInformationAttributes.ImplementsSetCommand);
			PropertyInformation.commandInterfaceAttributes.Add(typeof(ISetUpdateCommand), PropertyInformationAttributes.ImplementsSetUpdateCommand);
			PropertyInformation.commandInterfaceAttributes.Add(typeof(IToXmlCommand), PropertyInformationAttributes.ImplementsToXmlCommand);
			PropertyInformation.commandInterfaceAttributes.Add(typeof(IToXmlForPropertyBagCommand), PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand);
			PropertyInformation.commandInterfaceAttributes.Add(typeof(IToServiceObjectCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);
			PropertyInformation.commandInterfaceAttributes.Add(typeof(IToServiceObjectForPropertyBagCommand), PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0004384C File Offset: 0x00041A4C
		public PropertyInformation(string localName, string xmlPath, string namespaceUri, ExchangeVersion effectiveVersion, PropertyDefinition[] propertyDefinitions, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createPropertyCommand, bool supportsMultipleInstancesForToXml, PropertyInformationAttributes propertyInformationAttributes) : base(localName, xmlPath, namespaceUri, effectiveVersion)
		{
			if (propertyDefinitions == null)
			{
				this.propertyDefinitions = new PropertyDefinition[0];
			}
			else
			{
				this.propertyDefinitions = propertyDefinitions;
			}
			this.supportsMultipleInstancesForToXml = supportsMultipleInstancesForToXml;
			this.propertyPath = propertyPath;
			this.createPropertyCommand = createPropertyCommand;
			this.propertyInformationAttributes = this.InternalGetPropertyInformationAttributes(propertyInformationAttributes);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000438A4 File Offset: 0x00041AA4
		private static PropertyInformationAttributes GetAttributesFromCommandInterfaces(Type propertyCommandType)
		{
			PropertyInformationAttributes propertyInformationAttributes = PropertyInformationAttributes.None;
			foreach (Type key in propertyCommandType.GetTypeInfo().ImplementedInterfaces)
			{
				PropertyInformationAttributes propertyInformationAttributes2;
				if (PropertyInformation.commandInterfaceAttributes.TryGetValue(key, out propertyInformationAttributes2))
				{
					propertyInformationAttributes |= propertyInformationAttributes2;
				}
			}
			return propertyInformationAttributes;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00043908 File Offset: 0x00041B08
		protected PropertyInformationAttributes PreparePropertyInformationAttributes(Type propertyCommandType, PropertyInformationAttributes propertyInformationAttributesToSupport)
		{
			PropertyInformationAttributes attributesFromCommandInterfaces = PropertyInformation.GetAttributesFromCommandInterfaces(propertyCommandType);
			PropertyInformationAttributes result;
			if (propertyInformationAttributesToSupport == PropertyInformationAttributes.None)
			{
				result = attributesFromCommandInterfaces;
			}
			else
			{
				result = propertyInformationAttributesToSupport;
			}
			if (this.propertyDefinitions.Length == 1)
			{
				result = this.ModifyAttributesForStoreReadOnlyFlag(result);
			}
			return result;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0004393C File Offset: 0x00041B3C
		private PropertyInformationAttributes ModifyAttributesForStoreReadOnlyFlag(PropertyInformationAttributes propertyInformationAttributes)
		{
			StorePropertyDefinition storePropertyDefinition = this.propertyDefinitions[0] as StorePropertyDefinition;
			if (storePropertyDefinition != null && (storePropertyDefinition.PropertyFlags & PropertyFlags.ReadOnly) == PropertyFlags.ReadOnly)
			{
				propertyInformationAttributes &= ~(PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsAppendUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand);
			}
			return propertyInformationAttributes;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0004396C File Offset: 0x00041B6C
		public PropertyInformation(string localName, string xmlPath, string namespaceUri, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createPropertyCommand, bool supportsMultipleInstancesForToXml) : this(localName, xmlPath, namespaceUri, effectiveVersion, (propertyDefinition == null) ? null : new PropertyDefinition[]
		{
			propertyDefinition
		}, propertyPath, createPropertyCommand, supportsMultipleInstancesForToXml, PropertyInformationAttributes.None)
		{
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000439A0 File Offset: 0x00041BA0
		public PropertyInformation(string localName, string xmlPath, string namespaceUri, ExchangeVersion effectiveVersion, PropertyDefinition[] propertyDefinitions, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createPropertyCommand) : this(localName, xmlPath, namespaceUri, effectiveVersion, propertyDefinitions, propertyPath, createPropertyCommand, false, PropertyInformationAttributes.None)
		{
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000439C0 File Offset: 0x00041BC0
		public PropertyInformation(string localName, string xmlPath, string namespaceUri, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createPropertyCommand) : this(localName, xmlPath, namespaceUri, effectiveVersion, propertyDefinition, propertyPath, createPropertyCommand, false)
		{
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000439DF File Offset: 0x00041BDF
		public PropertyInformation(string localName, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createPropertyCommand) : this(localName, ServiceXml.GetFullyQualifiedName(localName.ToString()), ServiceXml.DefaultNamespaceUri, effectiveVersion, propertyDefinition, propertyPath, createPropertyCommand)
		{
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x000439FE File Offset: 0x00041BFE
		public PropertyInformation(PropertyUriEnum propertyUriEnum, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition, PropertyCommand.CreatePropertyCommand createPropertyCommand) : this(propertyUriEnum.ToString(), effectiveVersion, propertyDefinition, new PropertyUri(propertyUriEnum), createPropertyCommand)
		{
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00043A1C File Offset: 0x00041C1C
		public PropertyInformation(string localName, string xmlPath, string namespaceUri, ExchangeVersion effectiveVersion, PropertyDefinition[] propertyDefinitions, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createPropertyCommand, PropertyInformationAttributes propertyInformationAttributes) : this(localName, xmlPath, namespaceUri, effectiveVersion, propertyDefinitions, propertyPath, createPropertyCommand, false, propertyInformationAttributes)
		{
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00043A40 File Offset: 0x00041C40
		public PropertyInformation(string localName, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition, PropertyPath propertyPath, PropertyCommand.CreatePropertyCommand createPropertyCommand, PropertyInformationAttributes propertyInformationAttributes) : this(localName, ServiceXml.GetFullyQualifiedName(localName.ToString()), ServiceXml.DefaultNamespaceUri, effectiveVersion, (propertyDefinition == null) ? null : new PropertyDefinition[]
		{
			propertyDefinition
		}, propertyPath, createPropertyCommand, propertyInformationAttributes)
		{
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00043A7D File Offset: 0x00041C7D
		public PropertyInformation(PropertyUriEnum propertyUriEnum, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition, PropertyCommand.CreatePropertyCommand createPropertyCommand, PropertyInformationAttributes propertyInformationAttributes) : this(propertyUriEnum.ToString(), effectiveVersion, propertyDefinition, new PropertyUri(propertyUriEnum), createPropertyCommand, propertyInformationAttributes)
		{
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00043A9C File Offset: 0x00041C9C
		public virtual PropertyDefinition[] GetPropertyDefinitions(CommandSettings commandSettings)
		{
			return this.propertyDefinitions;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00043AA4 File Offset: 0x00041CA4
		public PropertyPath PropertyPath
		{
			get
			{
				return this.propertyPath;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00043AAC File Offset: 0x00041CAC
		public virtual PropertyCommand.CreatePropertyCommand CreatePropertyCommand
		{
			get
			{
				return this.createPropertyCommand;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00043AB4 File Offset: 0x00041CB4
		public virtual PropertyInformationAttributes PropertyInformationAttributes
		{
			get
			{
				return this.propertyInformationAttributes;
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00043ABC File Offset: 0x00041CBC
		private bool IsAttributeSet(PropertyInformationAttributes checkPropertyInformationAttributes)
		{
			return (this.PropertyInformationAttributes & checkPropertyInformationAttributes) == checkPropertyInformationAttributes;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00043AC9 File Offset: 0x00041CC9
		public bool SupportsMultipleInstancesForToXml
		{
			get
			{
				return this.supportsMultipleInstancesForToXml;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00043AD1 File Offset: 0x00041CD1
		public bool ImplementsSetCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsSetCommand);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00043ADA File Offset: 0x00041CDA
		public bool ImplementsToXmlCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsToXmlCommand);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00043AE3 File Offset: 0x00041CE3
		public bool ImplementsAppendUpdateCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsAppendUpdateCommand);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x00043AEC File Offset: 0x00041CEC
		public bool ImplementsDeleteUpdateCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsDeleteUpdateCommand);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00043AF6 File Offset: 0x00041CF6
		public bool ImplementsSetUpdateCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsSetUpdateCommand);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00043B00 File Offset: 0x00041D00
		public bool ImplementsToXmlForPropertyBagCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00043B0A File Offset: 0x00041D0A
		public bool ImplementsToServiceObjectCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsToServiceObjectCommand);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00043B17 File Offset: 0x00041D17
		public bool ImplementsToServiceObjectForPropertyBagCommand
		{
			get
			{
				return this.IsAttributeSet(PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00043B24 File Offset: 0x00041D24
		public static PropertyInformation CreateMessageFlagsPropertyInformation(string localName, ExchangeVersion effectiveVersion, PropertyUriEnum propertyUriEnum, PropertyCommand.CreatePropertyCommand creationDelegate)
		{
			return new PropertyInformation(localName, effectiveVersion, MessageItemSchema.Flags, new PropertyUri(propertyUriEnum), creationDelegate, PropertyInformationAttributes.ImplementsReadOnlyCommands);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00043B3E File Offset: 0x00041D3E
		private PropertyInformationAttributes InternalGetPropertyInformationAttributes(PropertyInformationAttributes propertyInformationAttributes)
		{
			return this.PreparePropertyInformationAttributes(this.createPropertyCommand.Method.ReturnType, propertyInformationAttributes);
		}

		// Token: 0x04000A99 RID: 2713
		private const bool SupportsMultipleInstancesForToXmlDefault = false;

		// Token: 0x04000A9A RID: 2714
		private readonly PropertyDefinition[] propertyDefinitions;

		// Token: 0x04000A9B RID: 2715
		private readonly PropertyPath propertyPath;

		// Token: 0x04000A9C RID: 2716
		private readonly PropertyCommand.CreatePropertyCommand createPropertyCommand;

		// Token: 0x04000A9D RID: 2717
		private readonly bool supportsMultipleInstancesForToXml;

		// Token: 0x04000A9E RID: 2718
		private readonly PropertyInformationAttributes propertyInformationAttributes;

		// Token: 0x04000A9F RID: 2719
		private static Dictionary<Type, PropertyInformationAttributes> commandInterfaceAttributes = new Dictionary<Type, PropertyInformationAttributes>();
	}
}
