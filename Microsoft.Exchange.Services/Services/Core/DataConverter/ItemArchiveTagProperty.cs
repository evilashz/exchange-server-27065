using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200015E RID: 350
	internal sealed class ItemArchiveTagProperty : RetentionTagPropertyBase
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x0002FA27 File Offset: 0x0002DC27
		private ItemArchiveTagProperty(CommandContext commandContext) : base(commandContext, Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType.MoveToArchive)
		{
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002FA31 File Offset: 0x0002DC31
		public static ItemArchiveTagProperty CreateCommand(CommandContext commandContext)
		{
			return new ItemArchiveTagProperty(commandContext);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002FA3C File Offset: 0x0002DC3C
		internal override Guid? GetRetentionTag(StoreObject storeObject, out bool isExplicit)
		{
			Item item = storeObject as Item;
			if (item == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			isExplicit = false;
			if (!PropertyCommand.StorePropertyExists(storeObject, StoreObjectSchema.ArchiveTag))
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[ItemArchiveTagProperty::GetRetentionTag] ArchiveTag property did not exist for {0}", storeObject.StoreObjectId);
				return null;
			}
			bool flag = false;
			DateTime? dateTime = null;
			Guid? policyTagForArchiveFromItem = PolicyTagHelper.GetPolicyTagForArchiveFromItem(item, out isExplicit, out flag, out dateTime);
			if (policyTagForArchiveFromItem != null)
			{
				ExTraceGlobals.ELCTracer.TraceDebug<Guid?, StoreObjectId>((long)this.GetHashCode(), "[ItemArchiveTagProperty::GetRetentionTag] Archive tag {0} was found for {1}", policyTagForArchiveFromItem, storeObject.StoreObjectId);
			}
			else
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[ItemArchiveTagProperty::GetRetentionTag] Archive tag was not found for {1}", storeObject.StoreObjectId);
			}
			return policyTagForArchiveFromItem;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002FAFC File Offset: 0x0002DCFC
		internal override Guid? GetRetentionTagFromPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, out bool isExplicit)
		{
			isExplicit = false;
			byte[] b;
			if (!PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, StoreObjectSchema.ArchiveTag, out b))
			{
				ExTraceGlobals.ELCTracer.TraceDebug((long)this.GetHashCode(), "[ItemArchiveTagProperty::GetRetentionTagFromPropertyBag] Policy tag was not found in property bag.");
				return null;
			}
			Guid value = new Guid(b);
			object obj;
			if (PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, StoreObjectSchema.ArchivePeriod, out obj))
			{
				isExplicit = (obj != null);
			}
			return new Guid?(value);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002FB64 File Offset: 0x0002DD64
		internal override void SetRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			using (Item item = base.OpenItemForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.ArchiveProperties))
			{
				PolicyTagHelper.SetPolicyTagForArchiveOnItem(policyTag, item);
				item.Save(SaveMode.NoConflictResolution);
			}
			ExTraceGlobals.ELCTracer.TraceDebug<Guid, StoreObjectId>((long)this.GetHashCode(), "[ItemArchiveTagProperty::SetRetentionTag] Archive tag {0} was set on {1}", policyTag.PolicyGuid, storeObject.StoreObjectId);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
		internal override void NewRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			Item item = storeObject as Item;
			if (item == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			PolicyTagHelper.SetPolicyTagForArchiveOnNewItem(policyTag, item);
			ExTraceGlobals.ELCTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[ItemArchiveTagProperty::NewRetentionTag] Archive tag {0} was set on new item", policyTag.PolicyGuid);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002FC28 File Offset: 0x0002DE28
		internal override void DeleteRetentionTag(StoreObject storeObject)
		{
			using (Item item = base.OpenItemForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.ArchiveProperties))
			{
				PolicyTagHelper.ClearPolicyTagForArchiveOnItem(item);
				item.Save(SaveMode.NoConflictResolution);
			}
			ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[ItemArchiveTagProperty::DeleteRetentionTag] ArchiveTag was cleared on {0}", storeObject.StoreObjectId);
		}
	}
}
