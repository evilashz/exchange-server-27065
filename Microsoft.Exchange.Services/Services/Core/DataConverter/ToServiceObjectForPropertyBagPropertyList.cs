using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001B7 RID: 439
	internal sealed class ToServiceObjectForPropertyBagPropertyList : ToServiceObjectPropertyListBase
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x0003CA60 File Offset: 0x0003AC60
		public ToServiceObjectForPropertyBagPropertyList(Shape shape, ResponseShape responseShape) : base(shape, responseShape)
		{
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0003CA6A File Offset: 0x0003AC6A
		protected override ToServiceObjectCommandSettingsBase GetCommandSettings()
		{
			return new ToServiceObjectForPropertyBagCommandSettings();
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0003CA71 File Offset: 0x0003AC71
		protected override ToServiceObjectCommandSettingsBase GetCommandSettings(PropertyPath propertyPath)
		{
			return new ToServiceObjectForPropertyBagCommandSettings(propertyPath);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0003CA79 File Offset: 0x0003AC79
		protected override bool IsPropertyRequiredInShape
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0003CA7C File Offset: 0x0003AC7C
		protected override bool IsErrorReturnedForInvalidBaseShapeProperty
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0003CA80 File Offset: 0x0003AC80
		protected override bool ValidateProperty(PropertyInformation propertyInformation, bool returnErrorForInvalidProperty)
		{
			bool implementsToServiceObjectForPropertyBagCommand = propertyInformation.ImplementsToServiceObjectForPropertyBagCommand;
			if (!implementsToServiceObjectForPropertyBagCommand && returnErrorForInvalidProperty)
			{
				throw new InvalidPropertyForOperationException(propertyInformation.PropertyPath);
			}
			return implementsToServiceObjectForPropertyBagCommand;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0003CAA8 File Offset: 0x0003ACA8
		public IList<IToServiceObjectForPropertyBagCommand> CreatePropertyCommands(IDictionary<PropertyDefinition, object> propertyBag, ServiceObject serviceObject, IdAndSession idAndSession, CommandOptions commandOptions)
		{
			List<IToServiceObjectForPropertyBagCommand> list = new List<IToServiceObjectForPropertyBagCommand>();
			foreach (CommandContext commandContext in this.commandContextsOrdered)
			{
				ToServiceObjectForPropertyBagCommandSettings toServiceObjectForPropertyBagCommandSettings = (ToServiceObjectForPropertyBagCommandSettings)commandContext.CommandSettings;
				toServiceObjectForPropertyBagCommandSettings.PropertyBag = propertyBag;
				toServiceObjectForPropertyBagCommandSettings.ServiceObject = serviceObject;
				toServiceObjectForPropertyBagCommandSettings.IdAndSession = idAndSession;
				toServiceObjectForPropertyBagCommandSettings.CommandOptions = commandOptions;
				list.Add((IToServiceObjectForPropertyBagCommand)commandContext.PropertyInformation.CreatePropertyCommand(commandContext));
			}
			return list;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0003CB40 File Offset: 0x0003AD40
		public ServiceObject ConvertPropertiesToServiceObject(ServiceObject serviceObject, IDictionary<PropertyDefinition, object> propertyBag, IdAndSession idAndSession)
		{
			return this.ConvertPropertiesToServiceObject(serviceObject, propertyBag, idAndSession, CommandOptions.None);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0003CB4C File Offset: 0x0003AD4C
		public ServiceObject ConvertPropertiesToServiceObject(ServiceObject serviceObject, IDictionary<PropertyDefinition, object> propertyBag, IdAndSession idAndSession, CommandOptions commandOptions)
		{
			IList<IToServiceObjectForPropertyBagCommand> list = this.CreatePropertyCommands(propertyBag, serviceObject, idAndSession, commandOptions);
			foreach (IToServiceObjectForPropertyBagCommand toServiceObjectForPropertyBagCommand in list)
			{
				try
				{
					toServiceObjectForPropertyBagCommand.ToServiceObjectForPropertyBag();
				}
				catch (InvalidValueForPropertyException)
				{
					ExTraceGlobals.FindCommandBaseCallTracer.TraceError<string>((long)this.GetHashCode(), "[ToServiceObjectForPropertyBagPropertyList::ConvertPropertiesToServiceObject] Failed to render property to service object.  Property: {0}", toServiceObjectForPropertyBagCommand.ToString());
				}
			}
			return serviceObject;
		}
	}
}
