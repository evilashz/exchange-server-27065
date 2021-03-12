using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001BB RID: 443
	internal abstract class ToXmlPropertyListBase : PropertyList
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x0003CFAB File Offset: 0x0003B1AB
		protected ToXmlPropertyListBase()
		{
			this.commandContextsUnordered = new Dictionary<PropertyInformation, List<CommandContext>>();
			this.commandContextsOrdered = new List<CommandContext>();
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0003CFC9 File Offset: 0x0003B1C9
		public ToXmlPropertyListBase(Shape shape, ResponseShape responseShape) : base(shape)
		{
			this.responseShape = responseShape;
			this.commandContextsUnordered = new Dictionary<PropertyInformation, List<CommandContext>>();
			this.commandContextsOrdered = new List<CommandContext>();
			this.SelectProperties();
			this.SequencePropertyList(shape);
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0003CFFC File Offset: 0x0003B1FC
		public ResponseShape ResponseShape
		{
			get
			{
				return this.responseShape;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0003D004 File Offset: 0x0003B204
		private void SelectProperties()
		{
			this.AddBaseShapeProperties(this.responseShape.BaseShape);
			this.AddAdditionalProperties(this.responseShape.AdditionalProperties);
			ItemResponseShape itemResponseShape = this.responseShape as ItemResponseShape;
			if (itemResponseShape != null && itemResponseShape.IncludeMimeContent)
			{
				this.Add(ItemSchema.MimeContent, this.GetCommandSettings(), true);
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003D05C File Offset: 0x0003B25C
		private void AddBaseShapeProperties(ShapeEnum shapeEnum)
		{
			if (shapeEnum == ShapeEnum.Default)
			{
				this.Add(this.shape.DefaultProperties);
				return;
			}
			this.AddFromSchemaPropertyList(this.shape, shapeEnum);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0003D081 File Offset: 0x0003B281
		private void AddFromSchemaPropertyList(Shape shapeToAddFrom, ShapeEnum shapeEnum)
		{
			if (shapeToAddFrom.InnerShape != null)
			{
				this.AddFromSchemaPropertyList(shapeToAddFrom.InnerShape, shapeEnum);
			}
			this.Add(shapeToAddFrom.Schema.GetPropertyInformationListByShapeEnum(shapeEnum));
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0003D0AC File Offset: 0x0003B2AC
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
							throw new InvalidPropertyRequestException(propertyPath);
						}
						this.Add(propertyInformation, this.GetCommandSettings(propertyPath), true);
					}
					else if (this.IsPropertyRequiredInShape && !ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP1))
					{
						throw new InvalidPropertyRequestException(propertyPath);
					}
				}
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003D12C File Offset: 0x0003B32C
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

		// Token: 0x06000C21 RID: 3105 RVA: 0x0003D18C File Offset: 0x0003B38C
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

		// Token: 0x06000C22 RID: 3106 RVA: 0x0003D1F4 File Offset: 0x0003B3F4
		private void SequencePropertyList(Shape shape)
		{
			if (shape.InnerShape != null && !this.IsInSequence)
			{
				this.SequencePropertyList(shape.InnerShape);
			}
			this.SequenceCommands(shape.Schema);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003D220 File Offset: 0x0003B420
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

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		private bool IsInSequence
		{
			get
			{
				return this.commandContextsUnordered.Count == 0 && this.commandContextsOrdered.Count >= 0;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003D2F4 File Offset: 0x0003B4F4
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

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003D364 File Offset: 0x0003B564
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

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0003D3B1 File Offset: 0x0003B5B1
		protected virtual bool IsPropertyRequiredInShape
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		protected virtual bool IsErrorReturnedForInvalidBaseShapeProperty
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000C29 RID: 3113
		protected abstract ToXmlCommandSettingsBase GetCommandSettings();

		// Token: 0x06000C2A RID: 3114
		protected abstract ToXmlCommandSettingsBase GetCommandSettings(PropertyPath propertyPath);

		// Token: 0x06000C2B RID: 3115
		protected abstract bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty);

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003D3B7 File Offset: 0x0003B5B7
		public XmlElement CreateItemXmlElement(XmlDocument ownerDocument)
		{
			return this.shape.CreateItemXmlElement(ownerDocument);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0003D3C5 File Offset: 0x0003B5C5
		public XmlElement CreateItemXmlElement(XmlElement parentElement)
		{
			return this.shape.CreateItemXmlElement(parentElement);
		}

		// Token: 0x04000975 RID: 2421
		private Dictionary<PropertyInformation, List<CommandContext>> commandContextsUnordered;

		// Token: 0x04000976 RID: 2422
		protected List<CommandContext> commandContextsOrdered;

		// Token: 0x04000977 RID: 2423
		protected ResponseShape responseShape;
	}
}
