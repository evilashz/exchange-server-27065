using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Principal;
using System.Xml;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x0200045E RID: 1118
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class EwsStoreDataProvider : IConfigDataProvider
	{
		// Token: 0x060031B8 RID: 12728 RVA: 0x000CB0BA File Offset: 0x000C92BA
		protected EwsStoreDataProvider(LazilyInitialized<IExchangePrincipal> mailbox)
		{
			this.mailbox = mailbox;
			this.CanRetry = true;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000CB0D0 File Offset: 0x000C92D0
		protected EwsStoreDataProvider(LazilyInitialized<IExchangePrincipal> mailbox, SpecialLogonType logonType, OpenAsAdminOrSystemServiceBudgetTypeType budgetType) : this(mailbox)
		{
			this.logonType = new SpecialLogonType?(logonType);
			this.budgetType = budgetType;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000CB0EC File Offset: 0x000C92EC
		protected EwsStoreDataProvider(LazilyInitialized<IExchangePrincipal> mailbox, SecurityAccessToken securityAccessToken) : this(mailbox)
		{
			this.securityAccessToken = securityAccessToken;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000CB12C File Offset: 0x000C932C
		protected Item BindItem(ItemId itemId, PropertySet propertySet)
		{
			return this.InvokeServiceCall<Item>(() => Item.Bind(this.Service, itemId, propertySet ?? PropertySet.FirstClassProperties));
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000CB1B0 File Offset: 0x000C93B0
		protected virtual void ExpireCache()
		{
			SecurityIdentifier userSid = this.Mailbox.Sid;
			ADSessionSettings adSetting = this.Mailbox.MailboxInfo.OrganizationId.ToADSessionSettings();
			this.mailbox = new LazilyInitialized<IExchangePrincipal>(delegate()
			{
				IExchangePrincipal result;
				try
				{
					result = ExchangePrincipal.FromUserSid(adSetting, userSid);
				}
				catch (ObjectNotFoundException ex)
				{
					throw new DataSourceOperationException(ex.LocalizedString, ex);
				}
				return result;
			});
			this.service = null;
			this.cache = null;
			this.defaultFolder = null;
			this.requestedServerVersion = null;
			this.mailboxVersion = null;
			EwsStoreDataProvider.caches.Remove(this.CacheKey);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000CB23F File Offset: 0x000C943F
		protected virtual EwsStoreObject FilterObject(EwsStoreObject ewsStoreObject)
		{
			return ewsStoreObject;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000CB242 File Offset: 0x000C9442
		protected Folder GetOrCreateFolder(string folderName)
		{
			return this.GetOrCreateFolder(folderName, new FolderId(10));
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000CB25F File Offset: 0x000C945F
		protected Folder GetOrCreateFolder(string folderName, FolderId parentFolder)
		{
			return this.GetOrCreateFolderCore(folderName, parentFolder, () => new Folder(this.Service));
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000CB2E8 File Offset: 0x000C94E8
		private Folder GetOrCreateFolderCore(string folderName, FolderId parentFolder, Func<Folder> creator)
		{
			SearchFilter filter = new SearchFilter.IsEqualTo(FolderSchema.DisplayName, folderName);
			FolderView view = new FolderView(1);
			FindFoldersResults findFoldersResults = this.InvokeServiceCall<FindFoldersResults>(() => this.Service.FindFolders(parentFolder, filter, view));
			if (findFoldersResults.Folders.Count == 0)
			{
				Folder folder = creator();
				folder.DisplayName = folderName;
				try
				{
					this.InvokeServiceCall(delegate()
					{
						folder.Save(parentFolder);
					});
				}
				catch (DataSourceOperationException ex)
				{
					ServiceResponseException ex2 = ex.InnerException as ServiceResponseException;
					if (ex2 == null || ex2.ErrorCode != 107)
					{
						throw;
					}
				}
				findFoldersResults = this.InvokeServiceCall<FindFoldersResults>(() => this.Service.FindFolders(parentFolder, filter, view));
			}
			return findFoldersResults.Folders[0];
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000CB3F0 File Offset: 0x000C95F0
		public virtual T FindByAlternativeId<T>(string alternativeId) where T : IConfigurable, new()
		{
			if (string.IsNullOrEmpty(alternativeId))
			{
				throw new ArgumentNullException("alternativeId");
			}
			ItemId itemId = null;
			if (!this.Cache.TryGetItemId(alternativeId, out itemId))
			{
				SearchFilter filter = new SearchFilter.IsEqualTo(EwsStoreObjectSchema.AlternativeId.StorePropertyDefinition, alternativeId);
				IEnumerable<T> source = this.InternalFindPaged<T>(filter, null, false, null, 1, new ProviderPropertyDefinition[0]);
				T t = source.FirstOrDefault<T>();
				this.Cache.SetItemId(alternativeId, (t == null) ? null : ((EwsStoreObjectId)t.Identity).EwsObjectId);
				return t;
			}
			if (itemId != null)
			{
				return (T)((object)this.Read<T>(new EwsStoreObjectId(itemId)));
			}
			return default(T);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000CB4CC File Offset: 0x000C96CC
		protected Item FindItemByAlternativeId(string alternativeId)
		{
			if (string.IsNullOrEmpty(alternativeId))
			{
				throw new ArgumentNullException("alternativeId");
			}
			SearchFilter filter = new SearchFilter.IsEqualTo(EwsStoreObjectSchema.AlternativeId.StorePropertyDefinition, alternativeId);
			return this.InvokeServiceCall<FindItemsResults<Item>>(() => this.Service.FindItems(this.DefaultFolder, filter, new ItemView(1))).FirstOrDefault<Item>();
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000CB526 File Offset: 0x000C9726
		public virtual IEnumerable<T> FindInFolder<T>(SearchFilter filter, FolderId rootId) where T : IConfigurable, new()
		{
			return this.InternalFindPaged<T>(filter, rootId, false, null, 0, new ProviderPropertyDefinition[0]);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000CBC0C File Offset: 0x000C9E0C
		protected virtual IEnumerable<T> InternalFindPaged<T>(SearchFilter filter, FolderId rootId, bool deepSearch, SortBy[] sortBy, int pageSize, params ProviderPropertyDefinition[] properties) where T : IConfigurable, new()
		{
			Func<GetItemResponse, bool> func = null;
			Func<GetItemResponse, Item> func2 = null;
			EwsStoreDataProvider.<>c__DisplayClass1b<T> CS$<>8__locals1 = new EwsStoreDataProvider.<>c__DisplayClass1b<T>();
			CS$<>8__locals1.filter = filter;
			CS$<>8__locals1.rootId = rootId;
			CS$<>8__locals1.<>4__this = this;
			if (pageSize < 0 || pageSize > 1000)
			{
				throw new ArgumentOutOfRangeException("pageSize", pageSize, string.Format("pageSize should be between 1 and {0} or 0 to use the default page size: {1}", 1000, this.DefaultPageSize));
			}
			EwsStoreObject dummyObject = (EwsStoreObject)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			List<SearchFilter> filters = new List<SearchFilter>();
			if (CS$<>8__locals1.filter != null)
			{
				filters.Add(CS$<>8__locals1.filter);
			}
			SearchFilter versioningFilter = dummyObject.VersioningFilter;
			if (versioningFilter != null)
			{
				filters.Add(versioningFilter);
			}
			SearchFilter itemClassFilter = dummyObject.ItemClassFilter;
			if (itemClassFilter != null)
			{
				filters.Add(itemClassFilter);
			}
			if (filters.Count == 1)
			{
				CS$<>8__locals1.filter = filters[0];
			}
			else if (filters.Count > 1)
			{
				CS$<>8__locals1.filter = new SearchFilter.SearchFilterCollection(0, filters.ToArray());
			}
			CS$<>8__locals1.itemView = new ItemView((pageSize == 0) ? this.DefaultPageSize : pageSize);
			if (sortBy != null && sortBy.Length > 0)
			{
				foreach (SortBy sortBy2 in sortBy)
				{
					CS$<>8__locals1.itemView.OrderBy.Add(((EwsStoreObjectPropertyDefinition)sortBy2.ColumnDefinition).StorePropertyDefinition, (sortBy2.SortOrder == SortOrder.Ascending) ? 0 : 1);
				}
			}
			bool useBindItem = false;
			CS$<>8__locals1.propertySet = null;
			if (properties != null && properties.Length > 0)
			{
				CS$<>8__locals1.propertySet = this.CreatePropertySet(properties, out useBindItem);
			}
			else
			{
				CS$<>8__locals1.propertySet = this.CreatePropertySet<T>(out useBindItem);
			}
			if (useBindItem)
			{
				CS$<>8__locals1.itemView.PropertySet = new PropertySet(0);
			}
			else
			{
				CS$<>8__locals1.itemView.PropertySet = CS$<>8__locals1.propertySet;
			}
			for (;;)
			{
				EwsStoreDataProvider.<>c__DisplayClass1e<T> CS$<>8__locals2 = new EwsStoreDataProvider.<>c__DisplayClass1e<T>();
				CS$<>8__locals2.CS$<>8__locals1c = CS$<>8__locals1;
				FindItemsResults<Item> results = this.InvokeServiceCall<FindItemsResults<Item>>(() => CS$<>8__locals1.<>4__this.Service.FindItems(CS$<>8__locals1.rootId ?? CS$<>8__locals1.<>4__this.DefaultFolder, CS$<>8__locals1.filter, CS$<>8__locals1.itemView));
				CS$<>8__locals2.items = results.Items;
				if (useBindItem && results.Items.Count > 0)
				{
					ServiceResponseCollection<GetItemResponse> serviceResponseCollection = this.InvokeServiceCall<ServiceResponseCollection<GetItemResponse>>(() => CS$<>8__locals2.CS$<>8__locals1c.<>4__this.Service.BindToItems(from x in CS$<>8__locals2.items
					select x.Id, CS$<>8__locals2.CS$<>8__locals1c.propertySet));
					EwsStoreDataProvider.<>c__DisplayClass1e<T> CS$<>8__locals3 = CS$<>8__locals2;
					IEnumerable<GetItemResponse> source = serviceResponseCollection;
					if (func == null)
					{
						func = ((GetItemResponse x) => x.Item != null);
					}
					IEnumerable<GetItemResponse> source2 = source.Where(func);
					if (func2 == null)
					{
						func2 = ((GetItemResponse x) => x.Item);
					}
					CS$<>8__locals3.items = source2.Select(func2);
				}
				foreach (Item item in CS$<>8__locals2.items)
				{
					T instance = this.ObjectFromItem<T>(item);
					if (instance != null)
					{
						yield return instance;
					}
				}
				if (!results.MoreAvailable)
				{
					break;
				}
				CS$<>8__locals1.itemView.Offset = results.NextPageOffset.Value;
			}
			yield break;
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000CBC50 File Offset: 0x000C9E50
		private PropertySet CreatePropertySet<T>(out bool hasReturnOnBindProperty) where T : IConfigurable, new()
		{
			return this.CreatePropertySet(((EwsStoreObject)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)))).ObjectSchema.AllProperties, out hasReturnOnBindProperty);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000CBD10 File Offset: 0x000C9F10
		private PropertySet CreatePropertySet(IEnumerable<PropertyDefinition> properties, out bool hasReturnOnBindProperty)
		{
			List<PropertyDefinition> expandedList = new List<PropertyDefinition>(properties);
			for (int i = 0; i < expandedList.Count; i++)
			{
				ProviderPropertyDefinition providerPropertyDefinition = expandedList[i] as ProviderPropertyDefinition;
				if (providerPropertyDefinition.IsCalculated)
				{
					expandedList.AddRange(from x in providerPropertyDefinition.SupportingProperties
					where !expandedList.Contains(x)
					select x);
				}
			}
			properties = expandedList;
			hasReturnOnBindProperty = properties.Any((PropertyDefinition x) => x is EwsStoreObjectPropertyDefinition && ((EwsStoreObjectPropertyDefinition)x).ReturnOnBind);
			PropertySet propertySet = new PropertySet(0);
			propertySet.AddRange(from x in properties
			where x is EwsStoreObjectPropertyDefinition && x != EwsStoreObjectSchema.Identity && ((EwsStoreObjectPropertyDefinition)x).StorePropertyDefinition.Version <= this.RequestedServerVersion
			select ((EwsStoreObjectPropertyDefinition)x).StorePropertyDefinition);
			if (!properties.Any((PropertyDefinition x) => x == EwsStoreObjectSchema.ExchangeVersion))
			{
				propertySet.Add(EwsStoreObjectSchema.ExchangeVersion.StorePropertyDefinition);
			}
			return propertySet;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000CBE3C File Offset: 0x000CA03C
		private T ObjectFromItem<T>(Item item) where T : IConfigurable, new()
		{
			EwsStoreObject ewsStoreObject = (EwsStoreObject)((object)((default(T) == null) ? Activator.CreateInstance<T>() : default(T)));
			object originalValue = null;
			ExchangeObjectVersion exchangeVersion = (ExchangeObjectVersion)EwsStoreObjectSchema.ExchangeVersion.DefaultValue;
			if (item.TryGetProperty(EwsStoreObjectSchema.ExchangeVersion.StorePropertyDefinition, ref originalValue))
			{
				exchangeVersion = (ExchangeObjectVersion)ValueConvertor.ConvertValue(originalValue, typeof(ExchangeObjectVersion), null);
				ewsStoreObject.SetExchangeVersion(exchangeVersion);
				if (ewsStoreObject.ExchangeVersion.Major > ewsStoreObject.MaximumSupportedExchangeObjectVersion.Major)
				{
					ExTraceGlobals.StorageTracer.TraceWarning<ItemId, byte, byte>(0L, "{0} has major version {1} which is greater than current one ({2}) and will be ignored", item.Id, ewsStoreObject.ExchangeVersion.Major, ewsStoreObject.MaximumSupportedExchangeObjectVersion.Major);
					return default(T);
				}
			}
			if (!string.IsNullOrEmpty(ewsStoreObject.ItemClass) && !ewsStoreObject.ItemClass.Equals(item.ItemClass, StringComparison.OrdinalIgnoreCase))
			{
				return default(T);
			}
			ewsStoreObject.CopyFromItemObject(item, this.RequestedServerVersion);
			if (ewsStoreObject.MaximumSupportedExchangeObjectVersion.IsOlderThan(ewsStoreObject.ExchangeVersion))
			{
				ExTraceGlobals.StorageTracer.TraceWarning<ItemId, ExchangeObjectVersion, ExchangeObjectVersion>(0L, "{0} has version {1} which is greater than current one ({2}) and will be read-only", item.Id, ewsStoreObject.ExchangeVersion, ewsStoreObject.MaximumSupportedExchangeObjectVersion);
				ewsStoreObject.SetIsReadOnly(true);
			}
			ValidationError[] array = ewsStoreObject.ValidateRead();
			ewsStoreObject.ResetChangeTracking(true);
			if (array.Length > 0)
			{
				foreach (ValidationError validationError in array)
				{
					PropertyValidationError propertyValidationError = validationError as PropertyValidationError;
					ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "Object '{0}' read from '{1}' failed validation. Attribute: '{2}'. Invalid data: '{3}'. Error message: '{4}'.", new object[]
					{
						ewsStoreObject.Identity,
						this.Mailbox.ToString() + "\\" + this.DefaultFolder.ToString(),
						(propertyValidationError != null) ? propertyValidationError.PropertyDefinition.Name : "<null>",
						(propertyValidationError != null) ? (propertyValidationError.InvalidData ?? "<null>") : "<null>",
						validationError.Description
					});
				}
			}
			return (T)((object)this.FilterObject(ewsStoreObject));
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000CC080 File Offset: 0x000CA280
		protected void InvokeServiceCall(Action callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.InvokeServiceCall<object>(delegate()
			{
				callback();
				return null;
			});
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000CC0C0 File Offset: 0x000CA2C0
		protected T InvokeServiceCall<T>(Func<T> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int num = 2;
			while (num-- > 0)
			{
				Exception ex = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					T result = callback();
					this.Cache.FailedCount = 0;
					return result;
				}
				catch (ServiceResponseException ex2)
				{
					if (EwsStoreDataProvider.ServiceErrorsForWrongEws.Contains(ex2.ErrorCode))
					{
						flag2 = true;
					}
					else if (EwsStoreDataProvider.TransientServiceErrors.Contains(ex2.ErrorCode))
					{
						flag = true;
					}
					ex = ex2;
				}
				catch (ServiceVersionException ex3)
				{
					flag2 = true;
					ex = ex3;
				}
				catch (ServiceRemoteException ex4)
				{
					ex = ex4;
				}
				catch (ServiceLocalException ex5)
				{
					ex = ex5;
				}
				if (this.Cache.FailedCount >= 5 && (ExDateTime.Now - this.Cache.LastDiscoverTime).TotalMinutes >= 5.0)
				{
					this.ExpireCache();
				}
				else if (flag2 && ((ExDateTime.Now - this.Cache.LastDiscoverTime).Seconds >= 30 || this.Cache.FailedCount == 0))
				{
					this.ExpireCache();
				}
				this.Cache.FailedCount++;
				if ((!this.CanRetry && !flag2) || num <= 0 || !flag)
				{
					throw new DataSourceOperationException(new LocalizedString(ex.Message), ex);
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000CC244 File Offset: 0x000CA444
		private void OnSerializeCustomSoapHeaders(XmlWriter writer)
		{
			object obj = null;
			if (this.logonType != null)
			{
				obj = EwsHelper.CreateSpecialLogonAuthenticationHeader(this.Mailbox, this.logonType.Value, this.budgetType, this.RequiredServerVersion);
			}
			else if (this.securityAccessToken != null)
			{
				obj = EwsHelper.CreateSerializedSecurityContext(this.Mailbox, this.securityAccessToken);
			}
			if (obj != null)
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(obj.GetType());
				safeXmlSerializer.Serialize(writer, obj);
			}
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000CC2CC File Offset: 0x000CA4CC
		public void Delete(IConfigurable instance)
		{
			EwsStoreObject ewsStoreObject = (EwsStoreObject)instance;
			if (ewsStoreObject.IsReadOnly)
			{
				throw new InvalidOperationException("Can't delete read-only object.");
			}
			if (instance.ObjectState == ObjectState.Deleted)
			{
				throw new InvalidOperationException(ServerStrings.ExceptionObjectHasBeenDeleted);
			}
			Item item = this.BindItem(ewsStoreObject.Identity.EwsObjectId, PropertySet.IdOnly);
			if (item != null)
			{
				this.InvokeServiceCall(delegate()
				{
					item.Delete(0);
				});
			}
			this.Cache.ClearItemCache(ewsStoreObject);
			ewsStoreObject.ResetChangeTracking();
			ewsStoreObject.MarkAsDeleted();
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000CC367 File Offset: 0x000CA567
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.FindPaged<T>(filter, rootId, deepSearch, sortBy, 0).Cast<IConfigurable>().ToArray<IConfigurable>();
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000CC380 File Offset: 0x000CA580
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			if (filter != null)
			{
				throw new ArgumentException("filter isn't supported.");
			}
			FolderId rootId2 = null;
			if (rootId != null)
			{
				rootId2 = new FolderId(((EwsStoreObjectId)rootId).EwsObjectId.UniqueId);
			}
			return this.InternalFindPaged<T>(null, rootId2, deepSearch, (sortBy == null) ? null : new SortBy[]
			{
				sortBy
			}, pageSize, new ProviderPropertyDefinition[0]);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000CC3DC File Offset: 0x000CA5DC
		public virtual IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			bool flag;
			Item item = this.BindItem(((EwsStoreObjectId)identity).EwsObjectId, this.CreatePropertySet<T>(out flag));
			return (item == null) ? default(T) : this.ObjectFromItem<T>(item);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000CC454 File Offset: 0x000CA654
		public virtual void Save(IConfigurable instance)
		{
			if (instance.ObjectState == ObjectState.Unchanged)
			{
				return;
			}
			if (instance.ObjectState == ObjectState.Deleted)
			{
				throw new NotSupportedException("Can't save deleted object.");
			}
			EwsStoreObject ewsStoreObject = (EwsStoreObject)instance;
			if (ewsStoreObject.IsReadOnly)
			{
				throw new InvalidOperationException("Can't save read-only object.");
			}
			ValidationError[] array = ewsStoreObject.Validate();
			if (array.Length > 0)
			{
				throw new DataValidationException(array[0]);
			}
			if (ewsStoreObject.MaximumSupportedExchangeObjectVersion.IsOlderThan(ewsStoreObject.ExchangeVersion))
			{
				throw new DataValidationException(new PropertyValidationError(DataStrings.ErrorCannotSaveBecauseTooNew(ewsStoreObject.ExchangeVersion, ewsStoreObject.MaximumSupportedExchangeObjectVersion), ADObjectSchema.ExchangeVersion, ewsStoreObject.ExchangeVersion));
			}
			if (ewsStoreObject.ObjectState == ObjectState.New)
			{
				Item item = this.CreateItemObjectForNew();
				ewsStoreObject.CopyChangeToItemObject(item, this.RequestedServerVersion);
				this.InvokeServiceCall(delegate()
				{
					item.Save(this.DefaultFolder);
				});
				ewsStoreObject.CopyFromItemObject(item, this.RequestedServerVersion);
			}
			else
			{
				bool flag;
				Item item = this.BindItem(ewsStoreObject.Identity.EwsObjectId, this.CreatePropertySet(ewsStoreObject.GetChangedPropertyDefinitions(), out flag));
				if (item != null)
				{
					ewsStoreObject.CopyChangeToItemObject(item, this.RequestedServerVersion);
					this.InvokeServiceCall(delegate()
					{
						item.Update(2);
					});
				}
			}
			ewsStoreObject.ResetChangeTracking(true);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000CC5C8 File Offset: 0x000CA7C8
		public bool ApplyPolicyTag(Guid policyTagGuid, Folder folder, bool ignoreError = false)
		{
			if (this.RequestedServerVersion >= FolderSchema.PolicyTag.Version)
			{
				try
				{
					if (folder.PolicyTag == null || folder.PolicyTag.RetentionId != policyTagGuid)
					{
						folder.PolicyTag = new PolicyTag(true, policyTagGuid);
						this.InvokeServiceCall(delegate()
						{
							folder.Update();
						});
						return true;
					}
					return false;
				}
				catch (LocalizedException ex)
				{
					ExTraceGlobals.StorageTracer.TraceError<IExchangePrincipal, string>(0L, "EwsStoreDataProvider::failed to apply PolicyTag to mailbox '{0}', message: {1}", this.Mailbox, ex.Message);
					if (!ignoreError)
					{
						throw;
					}
					return false;
				}
			}
			ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, ServerVersion>(0L, "EwsStoreDataProvider::skip applying PolicyTag to mailbox '{0}' because the mailbox version '{1}' is too low.", this.Mailbox, this.MailboxVersion);
			return false;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000CC6B8 File Offset: 0x000CA8B8
		public void ApplyPolicyTag(Guid policyTagGuid, params Item[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				Item item = items[i];
				if (item.PolicyTag == null || item.PolicyTag.RetentionId != policyTagGuid)
				{
					item.PolicyTag = new PolicyTag(true, policyTagGuid);
					this.InvokeServiceCall(delegate()
					{
						item.Update(1);
					});
				}
			}
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000CC734 File Offset: 0x000CA934
		protected virtual Item CreateItemObjectForNew()
		{
			return new EmailMessage(this.Service);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000CC741 File Offset: 0x000CA941
		protected virtual EwsStoreDataProviderCacheEntry CreateCacheEntry()
		{
			return new EwsStoreDataProviderCacheEntry();
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000CC748 File Offset: 0x000CA948
		protected virtual FolderId GetDefaultFolder()
		{
			return new FolderId(10, new Mailbox(this.Mailbox.MailboxInfo.PrimarySmtpAddress.ToString()));
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000CC77F File Offset: 0x000CA97F
		string IConfigDataProvider.Source
		{
			get
			{
				return this.mailbox.ToString();
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000CC78C File Offset: 0x000CA98C
		public ExchangeService Service
		{
			get
			{
				if (this.service == null)
				{
					string text = this.Cache.EwsUrl;
					if (text == null && (ExDateTime.Now - this.Cache.LastDiscoverTime).TotalMinutes >= 1.0)
					{
						text = (this.Cache.EwsUrl = EwsHelper.DiscoverEwsUrl(this.Mailbox));
						this.Cache.LastDiscoverTime = ExDateTime.Now;
					}
					if (string.IsNullOrEmpty(text))
					{
						StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ErrorDiscoverEwsUrlForMailbox, this.Mailbox.MailboxInfo.PrimarySmtpAddress.ToString(), new object[]
						{
							this.Mailbox
						});
						throw new NoInternalEwsAvailableException(this.Mailbox.ToString());
					}
					ExTraceGlobals.StorageTracer.TraceDebug<IExchangePrincipal, string>(0L, "EwsStoreDataProvider: Connect to mailbox '{0}' with EWS at '{1}'.", this.Mailbox, text);
					this.service = new ExchangeService(this.RequestedServerVersion);
					this.service.HttpWebRequestFactory = new EwsHttpWebRequestFactoryEx
					{
						ServerCertificateValidationCallback = this.CertificateValidationCallback
					};
					this.service.Url = new Uri(text);
					this.service.UserAgent = WellKnownUserAgent.GetEwsNegoAuthUserAgent("EwsStoreDataProvider");
					this.service.HttpHeaders["X-AnchorMailbox"] = this.Mailbox.MailboxInfo.PrimarySmtpAddress.ToString();
					this.service.OnSerializeCustomSoapHeaders += new CustomXmlSerializationDelegate(this.OnSerializeCustomSoapHeaders);
					this.OnExchangeServiceCreated(this.service);
				}
				return this.service;
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000CC928 File Offset: 0x000CAB28
		protected virtual void OnExchangeServiceCreated(ExchangeService service)
		{
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000CC92A File Offset: 0x000CAB2A
		// (set) Token: 0x060031D9 RID: 12761 RVA: 0x000CC932 File Offset: 0x000CAB32
		public bool CanRetry { get; set; }

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x000CC93C File Offset: 0x000CAB3C
		public FolderId DefaultFolder
		{
			get
			{
				FolderId result;
				if ((result = this.defaultFolder) == null)
				{
					result = (this.defaultFolder = this.GetDefaultFolder());
				}
				return result;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000CC964 File Offset: 0x000CAB64
		public ExchangeVersion RequestedServerVersion
		{
			get
			{
				if (this.requestedServerVersion == null)
				{
					if (this.MailboxVersion.Major == (int)ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major)
					{
						this.requestedServerVersion = new ExchangeVersion?((this.MailboxVersion.Minor == 0) ? 1 : ((this.MailboxVersion.Minor == 1) ? 2 : ((this.MailboxVersion.Minor == 2) ? 3 : 2)));
					}
					else if (this.MailboxVersion.Major >= (int)ExchangeObjectVersion.Exchange2012.ExchangeBuild.Major)
					{
						this.requestedServerVersion = new ExchangeVersion?(4);
					}
					else
					{
						this.requestedServerVersion = new ExchangeVersion?(0);
					}
					if (this.requestedServerVersion.Value < this.RequiredServerVersion)
					{
						throw new MailboxVersionTooLowException(this.Mailbox.ToString(), this.RequiredServerVersion.ToString(), this.MailboxVersion.ToString());
					}
				}
				return this.requestedServerVersion.Value;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000CCA5D File Offset: 0x000CAC5D
		protected virtual ExchangeVersion RequiredServerVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x000CCA60 File Offset: 0x000CAC60
		protected virtual int DefaultPageSize
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000CCA64 File Offset: 0x000CAC64
		internal IExchangePrincipal Mailbox
		{
			get
			{
				return this.mailbox.Value;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000CCA71 File Offset: 0x000CAC71
		protected ServerVersion MailboxVersion
		{
			get
			{
				if (this.mailboxVersion == null)
				{
					this.mailboxVersion = new ServerVersion(this.Mailbox.MailboxInfo.Location.ServerVersion);
				}
				return this.mailboxVersion;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000CCAA7 File Offset: 0x000CACA7
		// (set) Token: 0x060031E1 RID: 12769 RVA: 0x000CCAAF File Offset: 0x000CACAF
		protected SpecialLogonType? LogonType
		{
			get
			{
				return this.logonType;
			}
			set
			{
				this.logonType = value;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000CCAB8 File Offset: 0x000CACB8
		// (set) Token: 0x060031E3 RID: 12771 RVA: 0x000CCAC0 File Offset: 0x000CACC0
		protected OpenAsAdminOrSystemServiceBudgetTypeType BudgetType
		{
			get
			{
				return this.budgetType;
			}
			set
			{
				this.budgetType = value;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000CCACC File Offset: 0x000CACCC
		protected EwsStoreDataProviderCacheEntry Cache
		{
			get
			{
				if (this.cache == null)
				{
					string cacheKey = this.CacheKey;
					EwsStoreDataProviderCacheEntry ewsStoreDataProviderCacheEntry;
					if (!EwsStoreDataProvider.caches.TryGetValue(cacheKey, out ewsStoreDataProviderCacheEntry))
					{
						this.cache = this.CreateCacheEntry();
						EwsStoreDataProvider.caches[cacheKey] = this.cache;
					}
					else
					{
						this.cache = ewsStoreDataProviderCacheEntry;
					}
				}
				return this.cache;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000CCB23 File Offset: 0x000CAD23
		protected virtual RemoteCertificateValidationCallback CertificateValidationCallback
		{
			get
			{
				return new RemoteCertificateValidationCallback(EwsHelper.CertificateErrorHandler);
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000CCB34 File Offset: 0x000CAD34
		private string CacheKey
		{
			get
			{
				return base.GetType().FullName + ":" + this.Mailbox.ObjectId.ObjectGuid.ToString();
			}
		}

		// Token: 0x04001AF0 RID: 6896
		public const int MaximumPageSize = 1000;

		// Token: 0x04001AF1 RID: 6897
		private static readonly MruDictionaryCache<string, EwsStoreDataProviderCacheEntry> caches = new MruDictionaryCache<string, EwsStoreDataProviderCacheEntry>(10, 100, 60);

		// Token: 0x04001AF2 RID: 6898
		private static readonly ServiceError[] TransientServiceErrors = new ServiceError[]
		{
			6,
			8,
			75,
			126,
			128,
			262,
			263,
			363,
			101
		};

		// Token: 0x04001AF3 RID: 6899
		private static readonly ServiceError[] ServiceErrorsForWrongEws = new ServiceError[]
		{
			83,
			414,
			222
		};

		// Token: 0x04001AF4 RID: 6900
		private SpecialLogonType? logonType;

		// Token: 0x04001AF5 RID: 6901
		private OpenAsAdminOrSystemServiceBudgetTypeType budgetType;

		// Token: 0x04001AF6 RID: 6902
		private LazilyInitialized<IExchangePrincipal> mailbox;

		// Token: 0x04001AF7 RID: 6903
		private ServerVersion mailboxVersion;

		// Token: 0x04001AF8 RID: 6904
		private ExchangeService service;

		// Token: 0x04001AF9 RID: 6905
		private SecurityAccessToken securityAccessToken;

		// Token: 0x04001AFA RID: 6906
		private FolderId defaultFolder;

		// Token: 0x04001AFB RID: 6907
		private ExchangeVersion? requestedServerVersion;

		// Token: 0x04001AFC RID: 6908
		private EwsStoreDataProviderCacheEntry cache;
	}
}
