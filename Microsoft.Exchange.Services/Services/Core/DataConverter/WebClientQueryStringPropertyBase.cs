using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200013B RID: 315
	internal abstract class WebClientQueryStringPropertyBase : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectForPropertyBagCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x00029F68 File Offset: 0x00028168
		public WebClientQueryStringPropertyBase(CommandContext commandContext) : base(commandContext)
		{
			this.itemIdProperty = this.propertyDefinitions[0];
			this.itemClassProperty = this.propertyDefinitions[1];
			this.isAssociatedProperty = this.propertyDefinitions[2];
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00029F9B File Offset: 0x0002819B
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00029FA0 File Offset: 0x000281A0
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			string className;
			if (this.ShouldCreateForServiceObjectOrXML(commandSettings.StoreObject, out className))
			{
				string itemId = this.GetItemId(commandSettings.StoreObject.Id, commandSettings.IdAndSession);
				commandSettings.ServiceObject.PropertyBag[this.commandContext.PropertyInformation] = this.CreateQueryString(className, itemId);
			}
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002A000 File Offset: 0x00028200
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			string className;
			StoreId storeId;
			if (this.ShouldCreateForPropertyBag(commandSettings.PropertyBag, out className, out storeId))
			{
				string itemId = this.GetItemId(storeId, commandSettings.IdAndSession);
				commandSettings.ServiceObject.PropertyBag[this.commandContext.PropertyInformation] = this.CreateQueryString(className, itemId);
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002A058 File Offset: 0x00028258
		private string GetItemId(StoreId storeId, IdAndSession idAndSession)
		{
			return IdConverter.GetConcatenatedId(storeId, idAndSession, null).Id;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002A078 File Offset: 0x00028278
		private bool ShouldCreateForServiceObjectOrXML(StoreObject storeObject, out string className)
		{
			className = null;
			bool flag = (bool)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.isAssociatedProperty);
			if (!flag)
			{
				StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(storeObject.Id);
				className = storeObject.ClassName;
				bool isPublic = !IdConverter.IsMessageId(asStoreObjectId.ProviderLevelItemId) || IdConverter.IsFromPublicStore(asStoreObjectId);
				if (this.ValidateProperty(storeObject, className, isPublic))
				{
					return true;
				}
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[WebClientQueryStringPropertyBase::ShouldCreateForServiceObjectOrXML] Returning false (isAssociated: {0}} for storeObject.Id: {1}", flag.ToString(), storeObject.Id.ToString());
			return false;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002A100 File Offset: 0x00028300
		private bool ShouldCreateForPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, out string className, out StoreId storeId)
		{
			className = null;
			storeId = null;
			bool flag;
			if (PropertyCommand.TryGetValueFromPropertyBag<StoreId>(propertyBag, this.itemIdProperty, out storeId) && PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, this.itemClassProperty, out className) && PropertyCommand.TryGetValueFromPropertyBag<bool>(propertyBag, this.isAssociatedProperty, out flag))
			{
				if (!flag)
				{
					StoreObjectId asStoreObjectId = IdConverter.GetAsStoreObjectId(storeId);
					bool isPublic = IdConverter.IsFromPublicStore(asStoreObjectId);
					if (this.ValidatePropertyFromPropertyBag(propertyBag, className, isPublic))
					{
						return true;
					}
				}
				else
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[WebClientQueryStringPropertyBase::ShouldCreateForPropertyBag] Returning false (isAssociated: {0}} for storeObject.Id: {1}", flag.ToString(), (storeId != null) ? storeId.ToString() : "<null>");
				}
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "[WebClientQueryStringPropertyBase::ShouldCreateForPropertyBag] Returning false for storeObject.Id: {0}", (storeId != null) ? storeId.ToString() : "<null>");
			}
			return false;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002A1BB File Offset: 0x000283BB
		protected virtual bool ValidateProperty(StoreObject storeObject, string className, bool isPublic)
		{
			return true;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002A1BE File Offset: 0x000283BE
		protected virtual bool ValidatePropertyFromPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, string className, bool isPublic)
		{
			return true;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002A1C4 File Offset: 0x000283C4
		private StringBuilder CreateQueryString(string className, string id)
		{
			StringBuilder stringBuilder = new StringBuilder("?");
			stringBuilder.Append("ItemID=" + WebUtility.UrlEncode(id));
			stringBuilder.Append("&exvsurl=1");
			stringBuilder.Append("&viewmodel=" + this.GetOwaViewModel(className));
			if (CallContext.Current.AccessingPrincipal.MailboxInfo == null)
			{
				return stringBuilder;
			}
			return stringBuilder;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002A22C File Offset: 0x0002842C
		protected virtual string GetOwaViewModel(string className)
		{
			if (className != null)
			{
				if (className == "IPM.Appointment")
				{
					return WebClientQueryStringPropertyBase.ReadCalendarViewModel;
				}
				if (className == "IPM.Contact")
				{
					return WebClientQueryStringPropertyBase.ReadContactViewModel;
				}
				if (className == "IPM.Task")
				{
					return WebClientQueryStringPropertyBase.ReadTaskViewModel;
				}
			}
			return WebClientQueryStringPropertyBase.ReadItemViewModel;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002A280 File Offset: 0x00028480
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			string className;
			if (this.ShouldCreateForServiceObjectOrXML(commandSettings.StoreObject, out className))
			{
				string itemId = this.GetItemId(commandSettings.StoreObject.Id, commandSettings.IdAndSession);
				this.CreateQueryStringXmlElement(commandSettings.ServiceItem, className, itemId);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002A2CC File Offset: 0x000284CC
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			string className;
			StoreId storeId;
			if (this.ShouldCreateForPropertyBag(commandSettings.PropertyBag, out className, out storeId))
			{
				string itemId = this.GetItemId(storeId, commandSettings.IdAndSession);
				this.CreateQueryStringXmlElement(commandSettings.ServiceItem, className, itemId);
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0002A310 File Offset: 0x00028510
		protected void CreateQueryStringXmlElement(XmlElement serviceItem, string className, string id)
		{
			StringBuilder stringBuilder = this.CreateQueryString(className, id);
			base.CreateXmlTextElement(serviceItem, this.xmlLocalName, stringBuilder.ToString());
		}

		// Token: 0x04000753 RID: 1875
		private const int ItemIdPropertyIndex = 0;

		// Token: 0x04000754 RID: 1876
		private const int ItemClassPropertyIndex = 1;

		// Token: 0x04000755 RID: 1877
		private const int IsAssociatedPropertyIndex = 2;

		// Token: 0x04000756 RID: 1878
		private PropertyDefinition itemIdProperty;

		// Token: 0x04000757 RID: 1879
		private PropertyDefinition itemClassProperty;

		// Token: 0x04000758 RID: 1880
		private PropertyDefinition isAssociatedProperty;

		// Token: 0x04000759 RID: 1881
		protected static readonly string ReadContactViewModel = "PersonaCardViewModelFactory";

		// Token: 0x0400075A RID: 1882
		protected static readonly string ReadCalendarViewModel = "CalendarItemDetailsViewModelFactory";

		// Token: 0x0400075B RID: 1883
		protected static readonly string ReadTaskViewModel = "TaskReadingPaneViewModelPopOutFactory";

		// Token: 0x0400075C RID: 1884
		protected static readonly string ReadItemViewModel = "ReadMessageItem";
	}
}
