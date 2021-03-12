using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A07 RID: 2567
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class XsoStoreDataProviderBase : DisposeTrackableBase, IConfigDataProvider
	{
		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x06005E32 RID: 24114 RVA: 0x0018DC9A File Offset: 0x0018BE9A
		// (set) Token: 0x06005E33 RID: 24115 RVA: 0x0018DCA2 File Offset: 0x0018BEA2
		private bool IsStoreSessionFromOuter { get; set; }

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06005E34 RID: 24116 RVA: 0x0018DCAB File Offset: 0x0018BEAB
		// (set) Token: 0x06005E35 RID: 24117 RVA: 0x0018DCB3 File Offset: 0x0018BEB3
		public StoreSession StoreSession { get; protected set; }

		// Token: 0x06005E36 RID: 24118 RVA: 0x0018DCBC File Offset: 0x0018BEBC
		public static ExchangePrincipal GetExchangePrincipalWithAdSessionSettingsForOrg(OrganizationId organizationId, ADUser user)
		{
			ADSessionSettings adSettings = organizationId.ToADSessionSettings();
			return ExchangePrincipal.FromADUser(adSettings, user, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x0018DCD8 File Offset: 0x0018BED8
		public IConfigurationSession GetSystemConfigurationSession(OrganizationId organizationId)
		{
			ADSessionSettings sessionSettings = organizationId.ToADSessionSettings();
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 80, "GetSystemConfigurationSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Management\\XsoDriver\\XsoStoreDataProviderBase.cs");
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x0018DD08 File Offset: 0x0018BF08
		public XsoStoreDataProviderBase(StoreSession session)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (session == null)
				{
					throw new ArgumentNullException("session");
				}
				this.IsStoreSessionFromOuter = true;
				this.StoreSession = session;
				disposeGuard.Success();
			}
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x0018DD68 File Offset: 0x0018BF68
		public XsoStoreDataProviderBase()
		{
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x06005E3A RID: 24122 RVA: 0x0018DD70 File Offset: 0x0018BF70
		string IConfigDataProvider.Source
		{
			get
			{
				if (this.source == null)
				{
					this.source = this.StoreSession.ToString();
				}
				return this.source;
			}
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x0018DD94 File Offset: 0x0018BF94
		public virtual IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			base.CheckDisposed();
			IConfigurable[] array = this.Find<T>(new FalseFilter(), identity, true, null);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x0018DDC3 File Offset: 0x0018BFC3
		public virtual IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			base.CheckDisposed();
			return (IConfigurable[])this.FindPaged<T>(filter, rootId, deepSearch, sortBy, 0).ToArray<T>();
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x0018E0F8 File Offset: 0x0018C2F8
		public virtual IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			base.CheckDisposed();
			if (!typeof(ConfigurableObject).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				throw new NotSupportedException("FindPaged: " + typeof(T).FullName);
			}
			foreach (T item in this.InternalFindPaged<T>(filter, rootId, deepSearch, sortBy, pageSize))
			{
				ConfigurableObject userConfigurationObject = (ConfigurableObject)((object)item);
				foreach (PropertyDefinition propertyDefinition in userConfigurationObject.ObjectSchema.AllProperties)
				{
					ProviderPropertyDefinition providerPropertyDefinition = propertyDefinition as ProviderPropertyDefinition;
					if (providerPropertyDefinition != null && !providerPropertyDefinition.IsCalculated)
					{
						object obj = null;
						userConfigurationObject.propertyBag.TryGetField(providerPropertyDefinition, ref obj);
						userConfigurationObject.InstantiationErrors.AddRange(providerPropertyDefinition.ValidateProperty(obj ?? providerPropertyDefinition.DefaultValue, userConfigurationObject.propertyBag, true));
					}
				}
				userConfigurationObject.ResetChangeTracking(true);
				yield return item;
			}
			yield break;
		}

		// Token: 0x06005E3E RID: 24126
		protected abstract IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new();

		// Token: 0x06005E3F RID: 24127 RVA: 0x0018E13C File Offset: 0x0018C33C
		public virtual void Save(IConfigurable instance)
		{
			base.CheckDisposed();
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

		// Token: 0x06005E40 RID: 24128
		protected abstract void InternalSave(ConfigurableObject instance);

		// Token: 0x06005E41 RID: 24129 RVA: 0x0018E1A8 File Offset: 0x0018C3A8
		public void Delete(IConfigurable instance)
		{
			base.CheckDisposed();
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			ConfigurableObject configurableObject = instance as ConfigurableObject;
			if (configurableObject == null)
			{
				throw new NotSupportedException("Delete: " + instance.GetType().FullName);
			}
			if (configurableObject.ObjectState == ObjectState.Deleted)
			{
				throw new InvalidOperationException(ServerStrings.ExceptionObjectHasBeenDeleted);
			}
			this.InternalDelete(configurableObject);
			configurableObject.ResetChangeTracking();
			configurableObject.MarkAsDeleted();
		}

		// Token: 0x06005E42 RID: 24130 RVA: 0x0018E21A File Offset: 0x0018C41A
		protected virtual void InternalDelete(ConfigurableObject instance)
		{
			throw new NotImplementedException("InternalDelete");
		}

		// Token: 0x06005E43 RID: 24131 RVA: 0x0018E226 File Offset: 0x0018C426
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (!this.IsStoreSessionFromOuter && this.StoreSession != null)
				{
					this.StoreSession.Dispose();
				}
				this.StoreSession = null;
			}
		}

		// Token: 0x0400348C RID: 13452
		private string source;
	}
}
