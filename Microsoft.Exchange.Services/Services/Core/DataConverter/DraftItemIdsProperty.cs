using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000160 RID: 352
	internal sealed class DraftItemIdsProperty : ComplexPropertyBase, IToXmlForPropertyBagCommand, IToServiceObjectForPropertyBagCommand, IPropertyCommand
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x0002FD2C File Offset: 0x0002DF2C
		private DraftItemIdsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002FD35 File Offset: 0x0002DF35
		public static DraftItemIdsProperty CreateCommand(CommandContext commandContext)
		{
			return new DraftItemIdsProperty(commandContext);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0002FD40 File Offset: 0x0002DF40
		public static bool IsItemInDraftsFolder(StoreId storeId, StoreObjectId draftsFolderId)
		{
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
			StoreObjectId parentIdFromItemId = IdConverter.GetParentIdFromItemId(storeObjectId);
			return draftsFolderId != null && draftsFolderId.Equals(parentIdFromItemId);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002FD68 File Offset: 0x0002DF68
		public void ToServiceObjectForPropertyBag()
		{
			ToServiceObjectForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			ConversationType conversationType = commandSettings.ServiceObject as ConversationType;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			if (conversationType != null)
			{
				conversationType.DraftStoreIds = this.FindDraftItems(propertyBag, idAndSession, "DraftItemIdsProperty::ToServiceObjectForPropertyBag");
				return;
			}
			ExTraceGlobals.ItemDataTracer.TraceDebug((long)this.GetHashCode(), "[DraftItemIdsProperty::ToServiceObjectForPropertyBag] the service object passed in was not a conversation type.");
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002FDE4 File Offset: 0x0002DFE4
		private IEnumerable<StoreId> FindDraftItems(IDictionary<PropertyDefinition, object> propertyBag, IdAndSession idAndSession, string callingMethodName)
		{
			IEnumerable<StoreId> result = null;
			object obj;
			if (PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, ConversationItemSchema.ConversationGlobalItemIds, out obj))
			{
				IEnumerable<StoreId> enumerable = obj as IEnumerable<StoreId>;
				if (enumerable != null)
				{
					StoreObjectId draftsFolderId = idAndSession.Session.GetDefaultFolderId(DefaultFolderType.Drafts);
					result = from storeId in enumerable
					where DraftItemIdsProperty.IsItemInDraftsFolder(storeId, draftsFolderId)
					select StoreId.GetStoreObjectId(storeId);
				}
				else
				{
					ExTraceGlobals.ItemDataTracer.TraceDebug<string, object>((long)this.GetHashCode(), "[{0}] Unexpectedly got non-array property {1}", callingMethodName, obj);
					StoreId storeId2 = null;
					if (PropertyCommand.TryGetValueFromPropertyBag<StoreId>(propertyBag, ConversationItemSchema.ConversationGlobalItemIds, out storeId2))
					{
						StoreObjectId defaultFolderId = idAndSession.Session.GetDefaultFolderId(DefaultFolderType.Drafts);
						if (DraftItemIdsProperty.IsItemInDraftsFolder(storeId2, defaultFolderId))
						{
							result = new StoreId[]
							{
								StoreId.GetStoreObjectId(storeId2)
							};
						}
					}
					else
					{
						ExTraceGlobals.ItemDataTracer.TraceDebug<string>((long)this.GetHashCode(), "[{0}] Could not get ConversationGlobalItemIds from property bag as an array or a single store id.", callingMethodName);
					}
				}
			}
			else
			{
				ExTraceGlobals.ItemDataTracer.TraceDebug<string>((long)this.GetHashCode(), "[{0}] Could not get ConversationGlobalItemIds from property bag.", callingMethodName);
			}
			return result;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			XmlElement serviceItem = commandSettings.ServiceItem;
			IdAndSession idAndSession = commandSettings.IdAndSession;
			IEnumerable<StoreId> enumerable = this.FindDraftItems(propertyBag, idAndSession, "DraftItemIdsProperty::ToXmlForPropertyBag");
			if (enumerable != null)
			{
				XmlElement idParentElement = base.CreateXmlElement(serviceItem, this.xmlLocalName);
				foreach (StoreId storeId in enumerable)
				{
					IdConverter.CreateStoreIdXml(idParentElement, storeId, idAndSession, ((ArrayPropertyInformation)this.commandContext.PropertyInformation).ArrayItemLocalName);
				}
			}
		}
	}
}
