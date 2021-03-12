using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200014B RID: 331
	internal sealed class FolderPolicyTagProperty : RetentionTagPropertyBase
	{
		// Token: 0x06000910 RID: 2320 RVA: 0x0002C3A0 File Offset: 0x0002A5A0
		private FolderPolicyTagProperty(CommandContext commandContext) : base(commandContext, Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType.DeleteAndAllowRecovery)
		{
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002C3AA File Offset: 0x0002A5AA
		public static FolderPolicyTagProperty CreateCommand(CommandContext commandContext)
		{
			return new FolderPolicyTagProperty(commandContext);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		internal override Guid? GetRetentionTag(StoreObject storeObject, out bool isExplicit)
		{
			Folder folder = storeObject as Folder;
			if (folder == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			isExplicit = false;
			if (!PropertyCommand.StorePropertyExists(storeObject, StoreObjectSchema.PolicyTag))
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[FolderPolicyTagProperty::GetRetentionTag] PolicyTag property did not exist for {0}", storeObject.StoreObjectId);
				return null;
			}
			Guid? policyTagForDeleteFromFolder = PolicyTagHelper.GetPolicyTagForDeleteFromFolder(folder, out isExplicit);
			if (policyTagForDeleteFromFolder != null)
			{
				ExTraceGlobals.ELCTracer.TraceDebug<Guid?, StoreObjectId>((long)this.GetHashCode(), "[FolderPolicyTagProperty::GetRetentionTag] Policy tag {0} was found for {1}", policyTagForDeleteFromFolder, storeObject.StoreObjectId);
			}
			else
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[FolderPolicyTagProperty::GetRetentionTag] Policy tag was not found for {1}", storeObject.StoreObjectId);
			}
			return policyTagForDeleteFromFolder;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002C464 File Offset: 0x0002A664
		internal override Guid? GetRetentionTagFromPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, out bool isExplicit)
		{
			isExplicit = false;
			byte[] b;
			if (!PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, StoreObjectSchema.PolicyTag, out b))
			{
				ExTraceGlobals.ELCTracer.TraceDebug((long)this.GetHashCode(), "[FolderPolicyTagProperty::GetRetentionTagFromPropertyBag] Policy tag was not found in property bag.");
				return null;
			}
			Guid value = new Guid(b);
			object obj;
			if (PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, StoreObjectSchema.RetentionFlags, out obj) && obj != null && obj is int)
			{
				isExplicit = (((int)obj & 1) != 0);
			}
			return new Guid?(value);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		internal override void SetRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			using (Folder folder = base.OpenFolderForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.RetentionProperties))
			{
				PolicyTagHelper.SetPolicyTagForDeleteOnFolder(policyTag, folder);
				folder.Save();
			}
			ExTraceGlobals.ELCTracer.TraceDebug<Guid, StoreObjectId>((long)this.GetHashCode(), "[FolderPolicyTagProperty::SetRetentionTag] Policy tag {0} was set on {1}", policyTag.PolicyGuid, storeObject.StoreObjectId);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002C550 File Offset: 0x0002A750
		internal override void NewRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			Folder folder = storeObject as Folder;
			if (folder == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			PolicyTagHelper.SetPolicyTagForDeleteOnNewFolder(policyTag, folder);
			ExTraceGlobals.ELCTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[FolderPolicyTagProperty::NewRetentionTag] Policy tag {0} was set on new folder", policyTag.PolicyGuid);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		internal override void DeleteRetentionTag(StoreObject storeObject)
		{
			using (Folder folder = base.OpenFolderForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.RetentionProperties))
			{
				PolicyTagHelper.ClearPolicyTagForDeleteOnFolder(folder);
				folder.Save();
			}
			ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[FolderPolicyTagProperty::DeleteRetentionTag] PolicyTag was cleared on {0}", storeObject.StoreObjectId);
		}
	}
}
