using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000148 RID: 328
	internal sealed class FolderArchiveTagProperty : RetentionTagPropertyBase
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0002C0FD File Offset: 0x0002A2FD
		private FolderArchiveTagProperty(CommandContext commandContext) : base(commandContext, Microsoft.Exchange.Data.Directory.SystemConfiguration.RetentionActionType.MoveToArchive)
		{
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002C107 File Offset: 0x0002A307
		public static FolderArchiveTagProperty CreateCommand(CommandContext commandContext)
		{
			return new FolderArchiveTagProperty(commandContext);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002C110 File Offset: 0x0002A310
		internal override Guid? GetRetentionTag(StoreObject storeObject, out bool isExplicit)
		{
			Folder folder = storeObject as Folder;
			if (folder == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			isExplicit = false;
			if (!PropertyCommand.StorePropertyExists(storeObject, StoreObjectSchema.ArchiveTag))
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[FolderArchiveTagProperty::GetRetentionTag] ArchiveTag property did not exist for {0}", storeObject.StoreObjectId);
				return null;
			}
			Guid? policyTagForArchiveFromFolder = PolicyTagHelper.GetPolicyTagForArchiveFromFolder(folder, out isExplicit);
			if (policyTagForArchiveFromFolder != null)
			{
				ExTraceGlobals.ELCTracer.TraceDebug<Guid?, StoreObjectId>((long)this.GetHashCode(), "[FolderArchiveTagProperty::GetRetentionTag] Archive tag {0} was found for {1}", policyTagForArchiveFromFolder, storeObject.StoreObjectId);
			}
			else
			{
				ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[FolderArchiveTagProperty::GetRetentionTag] Archive tag was not found for {1}", storeObject.StoreObjectId);
			}
			return policyTagForArchiveFromFolder;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002C1C0 File Offset: 0x0002A3C0
		internal override Guid? GetRetentionTagFromPropertyBag(IDictionary<PropertyDefinition, object> propertyBag, out bool isExplicit)
		{
			isExplicit = false;
			byte[] b;
			if (!PropertyCommand.TryGetValueFromPropertyBag<byte[]>(propertyBag, StoreObjectSchema.ArchiveTag, out b))
			{
				ExTraceGlobals.ELCTracer.TraceDebug((long)this.GetHashCode(), "[FolderArchiveTagProperty::GetRetentionTagFromPropertyBag] Archive tag was not found in property bag.");
				return null;
			}
			Guid value = new Guid(b);
			object obj;
			if (PropertyCommand.TryGetValueFromPropertyBag<object>(propertyBag, StoreObjectSchema.RetentionFlags, out obj) && obj != null && obj is int)
			{
				isExplicit = (((int)obj & 16) != 0);
			}
			return new Guid?(value);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002C238 File Offset: 0x0002A438
		internal override void SetRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			using (Folder folder = base.OpenFolderForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.ArchiveProperties))
			{
				PolicyTagHelper.SetPolicyTagForArchiveOnFolder(policyTag, folder);
				folder.Save();
			}
			ExTraceGlobals.ELCTracer.TraceDebug<Guid, StoreObjectId>((long)this.GetHashCode(), "[FolderArchiveTagProperty::SetRetentionTag] Archive tag {0} was set on {1}", policyTag.PolicyGuid, storeObject.StoreObjectId);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		internal override void NewRetentionTag(StoreObject storeObject, PolicyTag policyTag)
		{
			Folder folder = storeObject as Folder;
			if (folder == null)
			{
				throw new InvalidPropertyRequestException(this.commandContext.PropertyInformation.PropertyPath);
			}
			PolicyTagHelper.SetPolicyTagForArchiveOnNewFolder(policyTag, folder);
			ExTraceGlobals.ELCTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[FolderArchiveTagProperty::NewRetentionTag] Archive tag {0} was set on new folder", policyTag.PolicyGuid);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002C2FC File Offset: 0x0002A4FC
		internal override void DeleteRetentionTag(StoreObject storeObject)
		{
			using (Folder folder = base.OpenFolderForRetentionTag(storeObject.Session, storeObject.StoreObjectId, PolicyTagHelper.ArchiveProperties))
			{
				PolicyTagHelper.ClearPolicyTagForArchiveOnFolder(folder);
				folder.Save();
			}
			ExTraceGlobals.ELCTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "[FolderArchiveTagProperty::DeleteRetentionTag] ArchiveTag was cleared on {0}", storeObject.StoreObjectId);
		}
	}
}
