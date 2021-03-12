using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E99 RID: 3737
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExBindingStoreObjectProvider : TenantStoreDataProvider
	{
		// Token: 0x060081F2 RID: 33266 RVA: 0x002380D8 File Offset: 0x002362D8
		public ExBindingStoreObjectProvider(ExPolicyConfigProvider policyConfigProvider) : base(policyConfigProvider.GetOrganizationId())
		{
			ArgumentValidator.ThrowIfNull("policyConfigProvider", policyConfigProvider);
			this.policyConfigProvider = policyConfigProvider;
		}

		// Token: 0x060081F3 RID: 33267 RVA: 0x002380F8 File Offset: 0x002362F8
		protected override FolderId GetDefaultFolder()
		{
			if (this.containerFolderId == null)
			{
				this.containerFolderId = base.GetOrCreateFolder("UnifiedPolicyBindings", new FolderId(10, new Mailbox(base.Mailbox.MailboxInfo.PrimarySmtpAddress.ToString()))).Id;
			}
			return this.containerFolderId;
		}

		// Token: 0x060081F4 RID: 33268 RVA: 0x00238154 File Offset: 0x00236354
		public BindingStorage FindBindingStorageByPolicyId(Guid policyId)
		{
			BindingStorage result = null;
			ExBindingStoreObject exBindingStoreObject = this.FindByAlternativeId<ExBindingStoreObject>(policyId.ToString());
			if (exBindingStoreObject != null)
			{
				result = exBindingStoreObject.ToBindingStorage(this.policyConfigProvider);
			}
			return result;
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x00238188 File Offset: 0x00236388
		public BindingStorage FindBindingStorageById(string identity)
		{
			BindingStorage result = null;
			SearchFilter filter = new SearchFilter.IsEqualTo(ExBindingStoreObjectSchema.MasterIdentity.StorePropertyDefinition, identity);
			ExBindingStoreObject exBindingStoreObject = this.InternalFindPaged<ExBindingStoreObject>(filter, this.GetDefaultFolder(), false, null, 1, new ProviderPropertyDefinition[0]).FirstOrDefault<ExBindingStoreObject>();
			if (exBindingStoreObject != null)
			{
				result = exBindingStoreObject.ToBindingStorage(this.policyConfigProvider);
			}
			return result;
		}

		// Token: 0x060081F6 RID: 33270 RVA: 0x002381D8 File Offset: 0x002363D8
		public void SaveBindingStorage(BindingStorage bindingStorage)
		{
			ArgumentValidator.ThrowIfNull("bindingStorage", bindingStorage);
			if (bindingStorage.ObjectState == ObjectState.Unchanged)
			{
				return;
			}
			ExBindingStoreObject exBindingStoreObject = bindingStorage.RawObject as ExBindingStoreObject;
			if (bindingStorage.ObjectState == ObjectState.New)
			{
				exBindingStoreObject = new ExBindingStoreObject();
			}
			if (exBindingStoreObject == null)
			{
				throw new InvalidOperationException("BindingStorage has no associated ExBindingStoreObject to save.");
			}
			exBindingStoreObject.FromBindingStorage(bindingStorage, this.policyConfigProvider);
			this.Save(exBindingStoreObject);
		}

		// Token: 0x060081F7 RID: 33271 RVA: 0x00238238 File Offset: 0x00236438
		public void DeleteBindingStorage(BindingStorage bindingStorage)
		{
			ArgumentValidator.ThrowIfNull("bindingStorage", bindingStorage);
			ExBindingStoreObject exBindingStoreObject = bindingStorage.RawObject as ExBindingStoreObject;
			if (exBindingStoreObject == null)
			{
				throw new InvalidOperationException("BindingStorage has no associated ExBindingStoreObject, cannot be deleted.");
			}
			base.Delete(exBindingStoreObject);
		}

		// Token: 0x04005732 RID: 22322
		public const string ContainerFolderName = "UnifiedPolicyBindings";

		// Token: 0x04005733 RID: 22323
		private FolderId containerFolderId;

		// Token: 0x04005734 RID: 22324
		private ExPolicyConfigProvider policyConfigProvider;
	}
}
