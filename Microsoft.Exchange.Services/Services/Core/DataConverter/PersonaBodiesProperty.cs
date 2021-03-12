using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000114 RID: 276
	internal class PersonaBodiesProperty : BodyProperty, IToServiceObjectForPropertyBagCommand, IToXmlForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x00027762 File Offset: 0x00025962
		protected PersonaBodiesProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002776B File Offset: 0x0002596B
		public new static PersonaBodiesProperty CreateCommand(CommandContext commandContext)
		{
			return new PersonaBodiesProperty(commandContext);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00027773 File Offset: 0x00025973
		public void ToXmlForPropertyBag()
		{
			throw new InvalidOperationException("Do not call this. It's obsolete");
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00027780 File Offset: 0x00025980
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			PropertyDefinition propertyDefinition = this.commandContext.GetPropertyDefinitions()[0];
			object obj = null;
			if (propertyDefinition != null && PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, propertyDefinition, out obj))
			{
				ArrayPropertyInformation arrayPropertyInformation = propertyInformation as ArrayPropertyInformation;
				if (arrayPropertyInformation != null && obj != null)
				{
					object[] array = obj as object[];
					if (array != null && array.Length > 0)
					{
						object servicePropertyValue = this.GetServicePropertyValue(array[0]);
						if (servicePropertyValue != null)
						{
							Array array2 = Array.CreateInstance(servicePropertyValue.GetType(), array.Length);
							array2.SetValue(servicePropertyValue, 0);
							for (int i = 1; i < array.Length; i++)
							{
								array2.SetValue(this.GetServicePropertyValue(array[i]), i);
							}
							serviceObject[propertyInformation] = array2;
						}
					}
				}
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00027859 File Offset: 0x00025A59
		protected override BodyFormat ComputeBodyFormat(BodyResponseType bodyType, Item item)
		{
			return BodyFormat.TextPlain;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002785C File Offset: 0x00025A5C
		private object GetServicePropertyValue(object propertyValue)
		{
			object result = null;
			if (propertyValue != null)
			{
				AttributedValue<PersonNotes> attributedValue = (AttributedValue<PersonNotes>)propertyValue;
				if (attributedValue.Value != null)
				{
					BodyContentAttributedValue bodyContentAttributedValue = new BodyContentAttributedValue
					{
						Attributions = attributedValue.Attributions,
						Value = new BodyContentType
						{
							BodyType = BodyType.Text,
							Value = attributedValue.Value.NotesBody,
							IsTruncated = attributedValue.Value.IsTruncated
						}
					};
					bool encodeStringProperties;
					if (CallContext.Current == null)
					{
						encodeStringProperties = Global.EncodeStringProperties;
					}
					else
					{
						encodeStringProperties = CallContext.Current.EncodeStringProperties;
					}
					if (encodeStringProperties && ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2012))
					{
						string value = bodyContentAttributedValue.Value.Value;
						if (value != null)
						{
							bodyContentAttributedValue.Value.Value = Util.EncodeForAntiXSS(value);
						}
					}
					result = bodyContentAttributedValue;
				}
			}
			return result;
		}
	}
}
