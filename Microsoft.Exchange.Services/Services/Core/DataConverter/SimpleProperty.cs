using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000EF RID: 239
	internal class SimpleProperty : PropertyCommand, IToXmlCommand, IToXmlForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IToServiceObjectCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x000219B2 File Offset: 0x0001FBB2
		public SimpleProperty(CommandContext commandContext, BaseConverter converter) : base(commandContext)
		{
			this.Initialize(commandContext, converter);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000219CA File Offset: 0x0001FBCA
		public SimpleProperty(CommandContext commandContext) : base(commandContext)
		{
			this.Initialize(commandContext, BaseConverter.GetConverterForPropertyDefinition(commandContext.GetPropertyDefinitions()[0]));
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000219EE File Offset: 0x0001FBEE
		public SimpleProperty(CommandContext commandContext, bool returnEmptyXmlElementForNullStringValue) : this(commandContext)
		{
			this.returnEmptyXmlElementForNullStringValue = returnEmptyXmlElementForNullStringValue;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000219FE File Offset: 0x0001FBFE
		public static SimpleProperty CreateCommand(CommandContext commandContext)
		{
			return new SimpleProperty(commandContext);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00021A06 File Offset: 0x0001FC06
		public static SimpleProperty CreateCommandForDoNonReturnNonRepresentableProperty(CommandContext commandContext)
		{
			return new SimpleProperty(commandContext, false);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00021A10 File Offset: 0x0001FC10
		public static SimpleProperty CreateCommandForPropertyWithDefaultValue(CommandContext commandContext)
		{
			BaseConverter converterForPropertyDefinition = BaseConverter.GetConverterForPropertyDefinition(commandContext.GetPropertyDefinitions()[0]);
			return new SimpleProperty(commandContext, converterForPropertyDefinition)
			{
				returnDefaultValue = true
			};
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00021A3B File Offset: 0x0001FC3B
		public static SimpleProperty CreateIsReadReceiptRequestedCommand(CommandContext commandContext)
		{
			PropertyCommand.PreventSentMessageUpdate(commandContext);
			return SimpleProperty.CreateCommand(commandContext);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00021A49 File Offset: 0x0001FC49
		public static SimpleProperty CreateIsDeliveryReceiptRequestedCommand(CommandContext commandContext)
		{
			PropertyCommand.PreventSentMessageUpdate(commandContext);
			return SimpleProperty.CreateCommand(commandContext);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00021A58 File Offset: 0x0001FC58
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			this.SetProperty(setPropertyUpdate.ServiceObject, storeObject);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00021A7C File Offset: 0x0001FC7C
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			base.DeleteProperties(storeObject, updateCommandSettings.PropertyUpdate.PropertyPath, new PropertyDefinition[]
			{
				this.propertyDefinition
			});
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00021AB3 File Offset: 0x0001FCB3
		protected virtual object PreparePropertyBagValue(object propertyValue)
		{
			return propertyValue;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00021AB8 File Offset: 0x0001FCB8
		protected virtual void SetProperty(ServiceObject serviceObject, StoreObject storeObject)
		{
			object obj = serviceObject[this.commandContext.PropertyInformation];
			ArrayPropertyInformation arrayPropertyInformation = this.commandContext.PropertyInformation as ArrayPropertyInformation;
			if (arrayPropertyInformation != null)
			{
				string[] array = obj as string[];
				int num = array.Length;
				Array array2 = Array.CreateInstance(this.GetSimplePropertyType().GetElementType(), num);
				for (int i = 0; i < num; i++)
				{
					object value = this.Parse(array[i]);
					array2.SetValue(value, i);
				}
				base.SetPropertyValueOnStoreObject(storeObject, this.propertyDefinition, array2);
				return;
			}
			if (obj is string)
			{
				obj = this.Parse((string)obj);
			}
			base.SetPropertyValueOnStoreObject(storeObject, this.propertyDefinition, obj);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00021B63 File Offset: 0x0001FD63
		protected virtual Type GetSimplePropertyType()
		{
			return this.propertyDefinition.Type;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00021B70 File Offset: 0x0001FD70
		protected object GetDefaultValueForPropertyType()
		{
			Type simplePropertyType = this.GetSimplePropertyType();
			object result;
			if (SimpleProperty.defaultValueMap.TryGetValue(simplePropertyType, out result))
			{
				return result;
			}
			if (simplePropertyType.GetTypeInfo().IsValueType)
			{
				return Activator.CreateInstance(simplePropertyType);
			}
			return null;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00021BAA File Offset: 0x0001FDAA
		protected virtual object Parse(string propertyString)
		{
			return this.propertyConverter.ConvertToObject(propertyString);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00021BB8 File Offset: 0x0001FDB8
		protected virtual object GetPropertyValue(StoreObject storeObject)
		{
			return PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.propertyDefinition);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00021BC6 File Offset: 0x0001FDC6
		protected virtual bool StorePropertyExists(StoreObject storeObject)
		{
			return PropertyCommand.StorePropertyExists(storeObject, this.propertyDefinition);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00021BD4 File Offset: 0x0001FDD4
		protected virtual string ToString(object propertyValue)
		{
			return this.propertyConverter.ConvertToString(propertyValue);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		protected virtual void WriteServiceProperty(object propertyValue, ServiceObject serviceObject, PropertyInformation propInfo)
		{
			if (propertyValue == null || propertyValue is PropertyError)
			{
				return;
			}
			object servicePropertyValue = this.GetServicePropertyValue(propertyValue);
			serviceObject[propInfo] = servicePropertyValue;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00021C10 File Offset: 0x0001FE10
		private void Initialize(CommandContext commandContext, BaseConverter converter)
		{
			PropertyDefinition[] propertyDefinitions = commandContext.GetPropertyDefinitions();
			this.propertyDefinition = propertyDefinitions[0];
			this.propertyConverter = converter;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00021C34 File Offset: 0x0001FE34
		private object GetServicePropertyValue(object propertyValue)
		{
			IdConverterWithCommandSettings idConverterWithCommandSettings = new IdConverterWithCommandSettings(base.GetCommandSettings<ToServiceObjectCommandSettingsBase>(), CallContext.Current);
			object obj = this.propertyConverter.ConvertToServiceObjectValue(propertyValue, idConverterWithCommandSettings);
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
				string text = obj as string;
				string[] array = obj as string[];
				if (text != null)
				{
					obj = Util.EncodeForAntiXSS(text);
				}
				else if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							array[i] = Util.EncodeForAntiXSS(array[i]);
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public virtual void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			if (this.StorePropertyExists(storeObject))
			{
				this.WriteServiceProperty(this.GetPropertyValue(storeObject), serviceObject, propertyInformation);
				return;
			}
			if (this.returnDefaultValue)
			{
				this.WriteServiceProperty(this.GetDefaultValueForPropertyType(), serviceObject, propertyInformation);
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00021D34 File Offset: 0x0001FF34
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			object obj = null;
			if (!PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, this.propertyDefinition, out obj))
			{
				if (this.returnDefaultValue)
				{
					obj = this.GetDefaultValueForPropertyType();
					this.WriteServiceProperty(obj, serviceObject, propertyInformation);
				}
				return;
			}
			if (!(propertyInformation is ArrayPropertyInformation))
			{
				obj = this.PreparePropertyBagValue(obj);
				this.WriteServiceProperty(obj, serviceObject, propertyInformation);
				return;
			}
			object[] array = obj as object[];
			Array array2 = null;
			if (array != null && array.Length > 0)
			{
				object servicePropertyValue = this.GetServicePropertyValue(array[0]);
				array2 = Array.CreateInstance(servicePropertyValue.GetType(), array.Length);
				array2.SetValue(servicePropertyValue, 0);
				for (int i = 1; i < array.Length; i++)
				{
					array2.SetValue(this.GetServicePropertyValue(array[i]), i);
				}
			}
			else if (obj != null && !(obj is PropertyError))
			{
				object servicePropertyValue2 = this.GetServicePropertyValue(obj);
				array2 = Array.CreateInstance(servicePropertyValue2.GetType(), 1);
				array2.SetValue(servicePropertyValue2, 0);
			}
			serviceObject[propertyInformation] = array2;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00021E58 File Offset: 0x00020058
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			XmlElement serviceItem = commandSettings.ServiceItem;
			if (this.StorePropertyExists(storeObject))
			{
				this.WriteServiceProperty(this.GetPropertyValue(storeObject), serviceItem);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00021E94 File Offset: 0x00020094
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			XmlElement serviceItem = commandSettings.ServiceItem;
			object propertyValue = null;
			if (PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, this.propertyDefinition, out propertyValue))
			{
				propertyValue = this.PreparePropertyBagValue(propertyValue);
				this.WriteServiceProperty(propertyValue, serviceItem);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00021ED8 File Offset: 0x000200D8
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			XmlElement serviceProperty = commandSettings.ServiceProperty;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			if (serviceObject != null)
			{
				this.SetProperty(serviceObject, storeObject);
				return;
			}
			this.SetProperty(serviceProperty, storeObject);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00021F18 File Offset: 0x00020118
		protected virtual void SetProperty(XmlElement serviceProperty, StoreObject storeObject)
		{
			ArrayPropertyInformation arrayPropertyInformation = this.commandContext.PropertyInformation as ArrayPropertyInformation;
			if (arrayPropertyInformation != null)
			{
				Array array = Array.CreateInstance(this.GetSimplePropertyType().GetElementType(), serviceProperty.ChildNodes.Count);
				for (int i = 0; i < serviceProperty.ChildNodes.Count; i++)
				{
					XmlElement textNodeParent = (XmlElement)serviceProperty.ChildNodes[i];
					object value = this.Parse(ServiceXml.GetXmlTextNodeValue(textNodeParent));
					array.SetValue(value, i);
				}
				base.SetPropertyValueOnStoreObject(storeObject, this.propertyDefinition, array);
				return;
			}
			base.SetPropertyValueOnStoreObject(storeObject, this.propertyDefinition, this.Parse(ServiceXml.GetXmlTextNodeValue(serviceProperty)));
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00021FBC File Offset: 0x000201BC
		private void WriteServiceProperty(object propertyValue, XmlElement serviceItem)
		{
			if (!this.returnEmptyXmlElementForNullStringValue && propertyValue == null)
			{
				return;
			}
			ArrayPropertyInformation arrayPropertyInformation = this.commandContext.PropertyInformation as ArrayPropertyInformation;
			if (arrayPropertyInformation != null)
			{
				XmlElement parentElement = base.CreateXmlElement(serviceItem, this.xmlLocalName);
				Array array = propertyValue as Array;
				if (array != null)
				{
					using (IEnumerator enumerator = array.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object propertyValue2 = enumerator.Current;
							base.CreateXmlTextElement(parentElement, arrayPropertyInformation.ArrayItemLocalName, this.ToString(propertyValue2));
						}
						return;
					}
				}
				ExTraceGlobals.ItemDataTracer.TraceDebug<string>((long)this.GetHashCode(), "[SimpleProperty::WriteServiceProperty] Unexpectedly got non-array property {0}", this.ToString(propertyValue));
				base.CreateXmlTextElement(parentElement, arrayPropertyInformation.ArrayItemLocalName, this.ToString(propertyValue));
				return;
			}
			string text = this.ToString(propertyValue);
			if (text != null || this.returnEmptyXmlElementForNullStringValue)
			{
				base.CreateXmlTextElement(serviceItem, this.xmlLocalName, text);
			}
		}

		// Token: 0x040006BF RID: 1727
		private static Dictionary<Type, object> defaultValueMap = new Dictionary<Type, object>
		{
			{
				typeof(bool),
				false
			},
			{
				typeof(byte),
				0
			},
			{
				typeof(char),
				'\0'
			},
			{
				typeof(double),
				0.0
			},
			{
				typeof(float),
				0f
			},
			{
				typeof(int),
				0
			},
			{
				typeof(long),
				0L
			},
			{
				typeof(short),
				0
			}
		};

		// Token: 0x040006C0 RID: 1728
		protected PropertyDefinition propertyDefinition;

		// Token: 0x040006C1 RID: 1729
		protected BaseConverter propertyConverter;

		// Token: 0x040006C2 RID: 1730
		private bool returnEmptyXmlElementForNullStringValue = true;

		// Token: 0x040006C3 RID: 1731
		private bool returnDefaultValue;
	}
}
