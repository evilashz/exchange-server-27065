using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.E4E;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.E4E
{
	// Token: 0x0200032B RID: 811
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EncryptionConfigurationDataProvider : IConfigDataProvider
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0007AAB2 File Offset: 0x00078CB2
		string IConfigDataProvider.Source
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0007AABA File Offset: 0x00078CBA
		public EncryptionConfigurationDataProvider(string organizationRawIdentity)
		{
			this.organizationRawIdentity = organizationRawIdentity;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0007AACC File Offset: 0x00078CCC
		public virtual IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			IConfigurable[] array = this.Find<T>(new FalseFilter(), identity, true, null);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0007AAF5 File Offset: 0x00078CF5
		public virtual IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return (IConfigurable[])this.FindPaged<T>(filter, rootId, deepSearch, sortBy, 0).ToArray<T>();
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0007AB10 File Offset: 0x00078D10
		public virtual IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			if (!typeof(ConfigurableObject).IsAssignableFrom(typeof(T)))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			return this.InternalFindPaged<T>(filter, rootId, deepSearch, sortBy, pageSize);
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0007AB64 File Offset: 0x00078D64
		public virtual void Save(IConfigurable instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			ConfigurableObject configurableObject = instance as ConfigurableObject;
			if (configurableObject == null)
			{
				throw new NotSupportedException("Save: " + instance.GetType().FullName);
			}
			ValidationError[] array = configurableObject.Validate();
			if (array.Length > 0)
			{
				throw new DataValidationException(array[0]);
			}
			this.InternalSave(configurableObject);
			configurableObject.ResetChangeTracking(true);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0007ACC4 File Offset: 0x00078EC4
		protected IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			EncryptionConfigurationTable.RequestData requestData;
			EncryptionConfigurationData encryptionConfigurationData = EncryptionConfigurationTable.GetEncryptionConfiguration(this.organizationRawIdentity, false, out requestData);
			yield return (T)((object)this.ConvertStoreObjectToPresentationObject(encryptionConfigurationData));
			yield break;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0007ACE4 File Offset: 0x00078EE4
		protected void InternalSave(ConfigurableObject instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			EncryptionConfiguration encryptionConfiguration = instance as EncryptionConfiguration;
			if (encryptionConfiguration == null)
			{
				throw new NotSupportedException("Save: " + instance.GetType().FullName);
			}
			EncryptionConfigurationTable.SetEncryptionConfiguration(this.organizationRawIdentity, encryptionConfiguration.ImageBase64, encryptionConfiguration.EmailText, encryptionConfiguration.PortalText, encryptionConfiguration.DisclaimerText, encryptionConfiguration.OTPEnabled);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0007AD4D File Offset: 0x00078F4D
		public void Delete(IConfigurable instance)
		{
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0007AD4F File Offset: 0x00078F4F
		private EncryptionConfiguration ConvertStoreObjectToPresentationObject(EncryptionConfigurationData encryptionConfigurationData)
		{
			return new EncryptionConfiguration(encryptionConfigurationData.ImageBase64, encryptionConfigurationData.EmailText, encryptionConfigurationData.PortalText, encryptionConfigurationData.DisclaimerText, encryptionConfigurationData.OTPEnabled);
		}

		// Token: 0x04000BE2 RID: 3042
		private readonly string organizationRawIdentity;
	}
}
