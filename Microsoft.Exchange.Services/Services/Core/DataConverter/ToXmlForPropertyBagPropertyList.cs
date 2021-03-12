using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001BC RID: 444
	internal sealed class ToXmlForPropertyBagPropertyList : ToXmlPropertyListBase
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x0003D3D3 File Offset: 0x0003B5D3
		public ToXmlForPropertyBagPropertyList(Shape shape, ResponseShape responseShape) : base(shape, responseShape)
		{
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003D3DD File Offset: 0x0003B5DD
		protected override ToXmlCommandSettingsBase GetCommandSettings()
		{
			return new ToXmlForPropertyBagCommandSettings();
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0003D3E4 File Offset: 0x0003B5E4
		protected override ToXmlCommandSettingsBase GetCommandSettings(PropertyPath propertyPath)
		{
			return new ToXmlForPropertyBagCommandSettings(propertyPath);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0003D3EC File Offset: 0x0003B5EC
		protected override bool IsPropertyRequiredInShape
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0003D3EF File Offset: 0x0003B5EF
		protected override bool IsErrorReturnedForInvalidBaseShapeProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
		protected override bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty)
		{
			bool implementsToXmlForPropertyBagCommand = propertyInformation.ImplementsToXmlForPropertyBagCommand;
			if (!implementsToXmlForPropertyBagCommand && returnErrorForInvalidProperty)
			{
				throw new InvalidPropertyForOperationException(propertyInformation.PropertyPath);
			}
			return implementsToXmlForPropertyBagCommand;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0003D420 File Offset: 0x0003B620
		public IList<IToXmlForPropertyBagCommand> CreatePropertyCommands(IDictionary<PropertyDefinition, object> propertyBag, XmlElement serviceItem, IdAndSession idAndSession)
		{
			List<IToXmlForPropertyBagCommand> list = new List<IToXmlForPropertyBagCommand>();
			foreach (CommandContext commandContext in this.commandContextsOrdered)
			{
				ToXmlForPropertyBagCommandSettings toXmlForPropertyBagCommandSettings = (ToXmlForPropertyBagCommandSettings)commandContext.CommandSettings;
				toXmlForPropertyBagCommandSettings.PropertyBag = propertyBag;
				toXmlForPropertyBagCommandSettings.ServiceItem = serviceItem;
				toXmlForPropertyBagCommandSettings.IdAndSession = idAndSession;
				list.Add((IToXmlForPropertyBagCommand)commandContext.PropertyInformation.CreatePropertyCommand(commandContext));
			}
			return list;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0003D4B0 File Offset: 0x0003B6B0
		public XmlElement ConvertPropertiesToXml(XmlDocument ownerDocument, IDictionary<PropertyDefinition, object> propertyBag, IdAndSession idAndSession)
		{
			XmlElement xmlElement = base.CreateItemXmlElement(ownerDocument);
			IList<IToXmlForPropertyBagCommand> list = this.CreatePropertyCommands(propertyBag, xmlElement, idAndSession);
			foreach (IToXmlForPropertyBagCommand toXmlForPropertyBagCommand in list)
			{
				try
				{
					toXmlForPropertyBagCommand.ToXmlForPropertyBag();
				}
				catch (InvalidValueForPropertyException)
				{
					ExTraceGlobals.FindCommandBaseCallTracer.TraceError<string>((long)this.GetHashCode(), "[ToXmlForPropertyBagPropertyList::ConvertPropertiesToXml] Failed to render property as xml.  Property: {0}", toXmlForPropertyBagCommand.ToString());
				}
			}
			return xmlElement;
		}
	}
}
