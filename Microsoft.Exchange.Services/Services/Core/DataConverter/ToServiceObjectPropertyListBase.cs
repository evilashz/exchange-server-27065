using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B6 RID: 438
	internal abstract class ToServiceObjectPropertyListBase : PropertyList
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x0003C544 File Offset: 0x0003A744
		protected ToServiceObjectPropertyListBase()
		{
			this.commandContextsUnordered = new Dictionary<PropertyInformation, List<CommandContext>>();
			this.commandContextsOrdered = new List<CommandContext>();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003C562 File Offset: 0x0003A762
		public ToServiceObjectPropertyListBase(Shape shape, ResponseShape responseShape) : base(shape)
		{
			this.responseShape = responseShape;
			this.commandContextsUnordered = new Dictionary<PropertyInformation, List<CommandContext>>();
			this.commandContextsOrdered = new List<CommandContext>();
			this.SelectProperties();
			this.SequencePropertyList(shape);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0003C595 File Offset: 0x0003A795
		public ResponseShape ResponseShape
		{
			get
			{
				return this.responseShape;
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0003C5A0 File Offset: 0x0003A7A0
		private void SelectProperties()
		{
			this.AddBaseShapeProperties(this.responseShape.BaseShape);
			this.AddAdditionalProperties(this.responseShape.AdditionalProperties);
			ItemResponseShape itemResponseShape = this.responseShape as ItemResponseShape;
			if (itemResponseShape != null && itemResponseShape.IncludeMimeContent)
			{
				this.Add(ItemSchema.MimeContent, this.GetCommandSettings(), false);
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003C5F8 File Offset: 0x0003A7F8
		private void AddBaseShapeProperties(ShapeEnum shapeEnum)
		{
			if (shapeEnum == ShapeEnum.Default)
			{
				this.Add(this.shape.DefaultProperties);
				return;
			}
			this.AddFromSchemaPropertyList(this.shape, shapeEnum);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003C61D File Offset: 0x0003A81D
		private void AddFromSchemaPropertyList(Shape shapeToAddFrom, ShapeEnum shapeEnum)
		{
			if (shapeToAddFrom.InnerShape != null)
			{
				this.AddFromSchemaPropertyList(shapeToAddFrom.InnerShape, shapeEnum);
			}
			this.Add(shapeToAddFrom.Schema.GetPropertyInformationListByShapeEnum(shapeEnum));
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0003C648 File Offset: 0x0003A848
		private void AddAdditionalProperties(PropertyPath[] additionalProperties)
		{
			if (additionalProperties != null)
			{
				foreach (PropertyPath propertyPath in additionalProperties)
				{
					PropertyInformation propertyInformation = null;
					if (this.shape.TryGetPropertyInformation(propertyPath, out propertyInformation))
					{
						if (!ExchangeVersion.Current.Supports(propertyInformation.EffectiveVersion))
						{
							throw new InvalidPropertyRequestException(CoreResources.ErrorInvalidPropertyVersionRequest(propertyPath.ToString(), ExchangeVersion.Current.ToString()), propertyPath);
						}
						this.Add(propertyInformation, this.GetCommandSettings(propertyPath), false);
					}
					else if (this.IsPropertyRequiredInShape && !ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP1))
					{
						throw new InvalidPropertyRequestException(CoreResources.ErrorInvalidPropertyVersionRequest(propertyPath.ToString(), ExchangeVersion.Current.ToString()), propertyPath);
					}
				}
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0003C6FC File Offset: 0x0003A8FC
		private void Add(PropertyInformation propertyInformation, CommandSettings commandSettings, bool returnErrorForInvalidProperty)
		{
			List<CommandContext> list = null;
			if (this.ValidateProperty(propertyInformation, returnErrorForInvalidProperty))
			{
				if (!this.commandContextsUnordered.TryGetValue(propertyInformation, out list))
				{
					list = new List<CommandContext>();
					list.Add(new CommandContext(commandSettings, propertyInformation));
					this.commandContextsUnordered.Add(propertyInformation, list);
					return;
				}
				if (propertyInformation.SupportsMultipleInstancesForToXml)
				{
					list.Add(new CommandContext(commandSettings, propertyInformation));
				}
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003C75C File Offset: 0x0003A95C
		private void Add(IList<PropertyInformation> properties)
		{
			foreach (PropertyInformation propertyInformation in properties)
			{
				if (ExchangeVersion.Current.Supports(propertyInformation.EffectiveVersion))
				{
					this.Add(propertyInformation, this.GetCommandSettings(), this.IsErrorReturnedForInvalidBaseShapeProperty);
				}
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0003C7C4 File Offset: 0x0003A9C4
		private void SequencePropertyList(Shape shape)
		{
			if (shape.InnerShape != null && !this.IsInSequence)
			{
				this.SequencePropertyList(shape.InnerShape);
			}
			this.SequenceCommands(shape.Schema);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003C7F0 File Offset: 0x0003A9F0
		private void SequenceCommands(Schema schema)
		{
			IList<PropertyInformation> propertyInformationInXmlSchemaSequence = schema.PropertyInformationInXmlSchemaSequence;
			foreach (PropertyInformation key in propertyInformationInXmlSchemaSequence)
			{
				List<CommandContext> list = null;
				if (this.commandContextsUnordered.TryGetValue(key, out list))
				{
					foreach (CommandContext item in list)
					{
						this.commandContextsOrdered.Add(item);
					}
					this.commandContextsUnordered.Remove(key);
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
		private bool IsInSequence
		{
			get
			{
				return this.commandContextsUnordered.Count == 0 && this.commandContextsOrdered.Count >= 0;
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
		public PropertyDefinition[] GetPropertyDefinitions()
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			foreach (CommandContext commandContext in this.commandContextsOrdered)
			{
				PropertyDefinition[] propertyDefinitions = commandContext.GetPropertyDefinitions();
				if (propertyDefinitions != null && propertyDefinitions.Length > 0)
				{
					list.AddRange(propertyDefinitions);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0003C934 File Offset: 0x0003AB34
		public HashSet<PropertyPath> GetProperties()
		{
			HashSet<PropertyPath> hashSet = new HashSet<PropertyPath>();
			foreach (CommandContext commandContext in this.commandContextsOrdered)
			{
				hashSet.Add(commandContext.PropertyInformation.PropertyPath);
			}
			return hashSet;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0003C99C File Offset: 0x0003AB9C
		public void FilterProperties(List<PropertyPath> allowedProperties)
		{
			foreach (CommandContext commandContext in this.commandContextsOrdered.ToArray())
			{
				PropertyPath propertyPath = commandContext.PropertyInformation.PropertyPath;
				if (!allowedProperties.Contains(propertyPath))
				{
					this.commandContextsOrdered.Remove(commandContext);
				}
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		public void Remove(PropertyPath property)
		{
			this.commandContextsOrdered.RemoveAll((CommandContext commandContext) => commandContext.PropertyInformation.PropertyPath.Equals(property));
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0003CA3E File Offset: 0x0003AC3E
		protected virtual bool IsPropertyRequiredInShape
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0003CA41 File Offset: 0x0003AC41
		protected virtual bool IsErrorReturnedForInvalidBaseShapeProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BF1 RID: 3057
		protected abstract ToServiceObjectCommandSettingsBase GetCommandSettings();

		// Token: 0x06000BF2 RID: 3058
		protected abstract ToServiceObjectCommandSettingsBase GetCommandSettings(PropertyPath propertyPath);

		// Token: 0x06000BF3 RID: 3059
		protected abstract bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty);

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0003CA44 File Offset: 0x0003AC44
		public XmlElement CreateItemXmlElement(XmlDocument ownerDocument)
		{
			return this.shape.CreateItemXmlElement(ownerDocument);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0003CA52 File Offset: 0x0003AC52
		public XmlElement CreateItemXmlElement(XmlElement parentElement)
		{
			return this.shape.CreateItemXmlElement(parentElement);
		}

		// Token: 0x0400096D RID: 2413
		private Dictionary<PropertyInformation, List<CommandContext>> commandContextsUnordered;

		// Token: 0x0400096E RID: 2414
		protected List<CommandContext> commandContextsOrdered;

		// Token: 0x0400096F RID: 2415
		protected ResponseShape responseShape;
	}
}
