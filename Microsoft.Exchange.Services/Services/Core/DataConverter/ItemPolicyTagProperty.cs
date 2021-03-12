using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000161 RID: 353
	internal sealed class ItemPolicyTagProperty : RetentionTagPropertyBase
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x0002FF98 File Offset: 0x0002E198
		private ItemPolicyTagProperty(CommandContext commandContext) : base(commandContext, Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType.DeleteAndAllowRecovery)
		{
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002FFA2 File Offset: 0x0002E1A2
		public static ItemPolicyTagProperty CreateCommand(CommandContext commandContext)
		{
			return new ItemPolicyTagProperty(commandContext);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002FFAC File Offset: 0x0002E1AC
		internal override Guid? GetRetentionTag(StoreObject storeObject, out bool isExplicit)
		{
			Item item = storeObject as Item;
			if (item == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			isExplicit = false;
			if (!PropertyCommand.StorePropertyExists(storeObject, StoreObjectSchema.PolicyTag))
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[ItemPolicyTagProperty::GetRetentionTag] PolicyTag property did not exist for {0}", storeObject.StoreObjectId);
				return null;
			}
			DateTime? dateTime = null;
			Guid? policyTagForDeleteFromItem = PolicyTagHelper.GetPolicyTagForDeleteFromItem(item, out isExplicit, out dateTime);
			if (policyTagForDeleteFromItem != null)
			{
				ExTraceGlobals.ELCTracer.TraceDebug<Guid?, StoreObjectId>((long)this.GetHashCode(), "[ItemPolicyTagProperty::GetRetentionTag] Policy tag {0} was found for {1}", policyTagForDeleteFromItem, storeObject.StoreObjectId);
			}
			else
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[ItemPolicyTagProperty::GetRetentionTag] Policy tag was not found for {1}", storeObject.StoreObjectId);
			}
			return policyTagForDeleteFromItem;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00030068 File Offset: 0x0002E268
		internal override Guid? GetRetentionTagFromPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, out bool isExplicit)
		{
			isExplicit = false;
			byte[] b;
			if (!PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, StoreObjectSchema.PolicyTag, out b))
			{
				ExTraceGlobals.ELCTracer.TraceDebug((long)this.GetHashCode(), "[ItemPolicyTagProperty::GetRetentionTagFromPropertyBag] Policy tag was not found in property bag.");
				return null;
			}
			Guid value = new Guid(b);
			object obj;
			if (PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, StoreObjectSchema.RetentionPeriod, out obj))
			{
				isExplicit = (obj != null);
			}
			return new Guid?(value);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000300D0 File Offset: 0x0002E2D0
		internal override void SetRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			using (Item item = base.OpenItemForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.RetentionProperties))
			{
				PolicyTagHelper.SetPolicyTagForDeleteOnItem(policyTag, item);
				item.Save(SaveMode.NoConflictResolution);
			}
			ExTraceGlobals.ELCTracer.TraceDebug<Guid, StoreObjectId>((long)this.GetHashCode(), "[ItemPolicyTagProperty::SetRetentionTag] Policy tag {0} was set on {1}", policyTag.PolicyGuid, storeObject.StoreObjectId);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00030144 File Offset: 0x0002E344
		internal override void NewRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			Item item = storeObject as Item;
			if (item == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			PolicyTagHelper.SetPolicyTagForDeleteOnNewItem(policyTag, item);
			ExTraceGlobals.ELCTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[ItemPolicyTagProperty::NewRetentionTag] Policy tag {0} was set on new item", policyTag.PolicyGuid);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00030194 File Offset: 0x0002E394
		internal override void DeleteRetentionTag(StoreObject storeObject)
		{
			using (Item item = base.OpenItemForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.RetentionProperties))
			{
				PolicyTagHelper.ClearPolicyTagForDeleteOnItem(item);
				item.Save(SaveMode.NoConflictResolution);
			}
			ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[ItemPolicyTagProperty::DeleteRetentionTag] PolicyTag was cleared on {0}", storeObject.StoreObjectId);
		}
	}
}
