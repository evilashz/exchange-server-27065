using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000138 RID: 312
	internal abstract class RetentionTagPropertyBase : PropertyCommand, IToXmlCommand, IToServiceObjectCommand, IToXmlForPropertyBagCommand, IToServiceObjectForPropertyBagCommand, ISetCommand, ISetUpdateCommand, IDeleteUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00029A4D File Offset: 0x00027C4D
		public RetentionTagPropertyBase(CommandContext commandContext, Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType retentionAction) : base(commandContext)
		{
			this.retentionAction = retentionAction;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00029A60 File Offset: 0x00027C60
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			bool flag = false;
			Guid? retentionTag = this.GetRetentionTag(storeObject, out flag);
			if (retentionTag != null)
			{
				serviceObject.PropertyBag[propertyInformation] = new RetentionTagType
				{
					IsExplicit = flag,
					Value = retentionTag.ToString()
				};
				ExTraceGlobals.ELCTracer.TraceDebug<bool, Guid?>((long)this.GetHashCode(), "[RetentionTagPropertyBase::ToServiceObject] IsExplicit = {0}, Value = {1}", flag, retentionTag);
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00029AF4 File Offset: 0x00027CF4
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			bool flag = false;
			Guid? retentionTagFromPropertyBag = this.GetRetentionTagFromPropertyBag(commandSettings.PropertyBag, out flag);
			if (retentionTagFromPropertyBag != null)
			{
				serviceObject.PropertyBag[propertyInformation] = new RetentionTagType
				{
					IsExplicit = flag,
					Value = retentionTagFromPropertyBag.ToString()
				};
				ExTraceGlobals.ELCTracer.TraceDebug<bool, Guid?>((long)this.GetHashCode(), "[RetentionTagPropertyBase::ToServiceObjectForPropertyBag] IsExplicit = {0}, Value = {1}", flag, retentionTagFromPropertyBag);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00029B80 File Offset: 0x00027D80
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			StoreObject storeObject = updateCommandSettings.StoreObject;
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			this.SetProperty(storeObject, serviceObject, false);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00029BA4 File Offset: 0x00027DA4
		public void Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			this.SetProperty(storeObject, serviceObject, true);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00029BCF File Offset: 0x00027DCF
		public override void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			this.DeleteRetentionTag(updateCommandSettings.StoreObject);
		}

		// Token: 0x06000880 RID: 2176
		internal abstract Guid? GetRetentionTag(StoreObject storeObject, out bool isExplicit);

		// Token: 0x06000881 RID: 2177
		internal abstract Guid? GetRetentionTagFromPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, out bool isExplicit);

		// Token: 0x06000882 RID: 2178
		internal abstract void SetRetentionTag(StoreObject storeObject, PolicyTag policyTag);

		// Token: 0x06000883 RID: 2179
		internal abstract void NewRetentionTag(StoreObject storeObject, PolicyTag policyTag);

		// Token: 0x06000884 RID: 2180
		internal abstract void DeleteRetentionTag(StoreObject storeObject);

		// Token: 0x06000885 RID: 2181 RVA: 0x00029BE0 File Offset: 0x00027DE0
		internal Item OpenItemForRetentionTag(StoreSession storeSession, StoreObjectId itemId, PropertyDefinition[] properties)
		{
			Item item = Item.Bind(storeSession, itemId, properties);
			item.OpenAsReadWrite();
			return item;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00029BFD File Offset: 0x00027DFD
		internal Folder OpenFolderForRetentionTag(StoreSession storeSession, StoreObjectId folderId, PropertyDefinition[] properties)
		{
			return Folder.Bind(storeSession, folderId, properties);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00029C08 File Offset: 0x00027E08
		internal void SetProperty(StoreObject storeObject, ServiceObject serviceObject, bool isNewObject)
		{
			RetentionTagType valueOrDefault = serviceObject.GetValueOrDefault<RetentionTagType>(this.commandContext.PropertyInformation);
			MailboxSession mailboxSession = (MailboxSession)storeObject.Session;
			if (!valueOrDefault.IsExplicit)
			{
				ExTraceGlobals.ELCTracer.TraceError<string, StoreObjectId>((long)this.GetHashCode(), "[RetentionTagPropertyBase::SetProperty] Tag {0} is not allowed to have IsExplicit set to false for {1}", valueOrDefault.Value, storeObject.StoreObjectId);
				throw new InvalidRetentionTagException((CoreResources.IDs)3769371271U);
			}
			Guid guid;
			if (!Guid.TryParse(valueOrDefault.Value, out guid) || !(guid != Guid.Empty))
			{
				return;
			}
			if (mailboxSession == null)
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidRetentionTagNone);
			}
			PolicyTagList policyTagList = mailboxSession.GetPolicyTagList(this.retentionAction);
			if (policyTagList == null)
			{
				throw new InvalidRetentionTagException(CoreResources.IDs.ErrorInvalidRetentionTagNone);
			}
			PolicyTag policyTag;
			if (!policyTagList.TryGetValue(guid, out policyTag))
			{
				ExTraceGlobals.ELCTracer.TraceError<Guid, StoreObjectId>((long)this.GetHashCode(), "[RetentionTagPropertyBase::SetProperty] Tag {0} has incorrect intended action type for {1}", guid, storeObject.StoreObjectId);
				throw new InvalidRetentionTagException(CoreResources.IDs.ErrorInvalidRetentionTagTypeMismatch);
			}
			if (!policyTag.IsVisible)
			{
				ExTraceGlobals.ELCTracer.TraceError<Guid, StoreObjectId>((long)this.GetHashCode(), "[RetentionTagPropertyBase::SetProperty] Tag {0} is an invisible tag for {1}", guid, storeObject.StoreObjectId);
				throw new InvalidRetentionTagException((CoreResources.IDs)4105318492U);
			}
			if (isNewObject)
			{
				this.NewRetentionTag(storeObject, policyTag);
				return;
			}
			this.SetRetentionTag(storeObject, policyTag);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00029D4C File Offset: 0x00027F4C
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			XmlElement serviceItem = commandSettings.ServiceItem;
			bool flag = false;
			Guid? retentionTag = this.GetRetentionTag(storeObject, out flag);
			if (retentionTag != null)
			{
				XmlElement xmlElement = base.CreateXmlTextElement(serviceItem, this.xmlLocalName, retentionTag.ToString());
				PropertyCommand.CreateXmlAttribute(xmlElement, "IsExplicit", flag.ToString());
				ExTraceGlobals.ELCTracer.TraceDebug<XmlElement>((long)this.GetHashCode(), "[RetentionTagPropertyBase::ToXml] {0}", xmlElement);
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00029DCC File Offset: 0x00027FCC
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			bool flag = false;
			Guid? retentionTagFromPropertyBag = this.GetRetentionTagFromPropertyBag(commandSettings.PropertyBag, out flag);
			if (retentionTagFromPropertyBag != null)
			{
				XmlElement xmlElement = base.CreateXmlTextElement(commandSettings.ServiceItem, this.xmlLocalName, retentionTagFromPropertyBag.ToString());
				PropertyCommand.CreateXmlAttribute(xmlElement, "IsExplicit", flag.ToString());
				ExTraceGlobals.ELCTracer.TraceDebug<XmlElement>((long)this.GetHashCode(), "[RetentionTagPropertyBase::ToXmlForPropertyBag] {0}", xmlElement);
			}
		}

		// Token: 0x04000752 RID: 1874
		private Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType retentionAction;
	}
}
