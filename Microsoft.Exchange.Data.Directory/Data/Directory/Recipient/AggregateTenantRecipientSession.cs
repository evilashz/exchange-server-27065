using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200020F RID: 527
	internal class AggregateTenantRecipientSession : DirectorySessionBase, ITenantRecipientSession, IRecipientSession, IDirectorySession, IConfigDataProvider, IAggregateSession
	{
		// Token: 0x06001BAF RID: 7087 RVA: 0x0007424C File Offset: 0x0007244C
		internal AggregateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings) : base(false, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.DomainController = domainController;
			base.SearchRoot = searchRoot;
			base.Lcid = lcid;
			base.UseGlobalCatalog = base.ReadOnly;
			if (AggregationHelper.IsMailboxRole)
			{
				this.directoryBackendType = (DirectoryBackendType.MServ | DirectoryBackendType.Mbx);
				((IAggregateSession)this).MbxReadMode = MbxReadMode.OnlyIfLocatorDataAvailable;
				if (!readOnly)
				{
					this.backendWriteMode = BackendWriteMode.WriteToMServ;
				}
			}
			else
			{
				this.directoryBackendType = DirectoryBackendType.MServ;
				((IAggregateSession)this).MbxReadMode = MbxReadMode.NoMbxRead;
				this.backendWriteMode = BackendWriteMode.NoWrites;
			}
			if (!DatacenterRegistry.IsForefrontForOffice() || DatacenterRegistry.IsForefrontForOfficeDeployment())
			{
				this.directoryBackendType |= DirectoryBackendType.AD;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x000742E5 File Offset: 0x000724E5
		internal AggregateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope) : this(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings)
		{
			base.ConfigScope = configScope;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x00074300 File Offset: 0x00072500
		private ApiNotSupportedException HandleUnsupportedApi(string toBeAddressedIn = null, [CallerMemberName] string memberName = "<unknown>")
		{
			LocalizedString localizedString = DirectoryStrings.ApiNotSupportedError(AggregateTenantRecipientSession.className, memberName);
			string stackTraceLine = base.GetStackTraceLine(4);
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_ApiNotSupported, stackTraceLine, new object[]
			{
				localizedString,
				stackTraceLine
			});
			return new ApiNotSupportedException(localizedString);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00074348 File Offset: 0x00072548
		private ApiInputNotSupportedException HandleUnsupportedInput(object input = null, [CallerMemberName] string memberName = "<unknown>")
		{
			LocalizedString localizedString = DirectoryStrings.ApiDoesNotSupportInputFormatError(AggregateTenantRecipientSession.className, memberName, (input != null) ? input.ToString() : "<null>");
			string stackTraceLine = base.GetStackTraceLine(4);
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_ApiInputNotSupported, stackTraceLine, new object[]
			{
				localizedString,
				stackTraceLine
			});
			return new ApiInputNotSupportedException(localizedString);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x000743A0 File Offset: 0x000725A0
		private ADRawEntry LoadADRawEntryByPuid(ulong puid, IEnumerable<PropertyDefinition> properties)
		{
			return this.LoadADRawEntryByPuidOrMemberName(puid, SmtpAddress.Empty, properties);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x000743AF File Offset: 0x000725AF
		private TResult LoadGenericRecipientByPuid<TResult>(ulong puid, IEnumerable<PropertyDefinition> properties = null) where TResult : ADObject, new()
		{
			return this.LoadGenericRecipientObjectByPuidOrMemberName<TResult>(puid, SmtpAddress.Empty, properties);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000743BE File Offset: 0x000725BE
		private ADRawEntry LoadADRawEntryByMemberName(SmtpAddress address, IEnumerable<PropertyDefinition> properties)
		{
			return this.LoadADRawEntryByPuidOrMemberName(0UL, address, properties);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x000743CA File Offset: 0x000725CA
		private TResult LoadGenericRecipientByMemberName<TResult>(SmtpAddress address, IEnumerable<PropertyDefinition> properties = null) where TResult : ADObject, new()
		{
			return this.LoadGenericRecipientObjectByPuidOrMemberName<TResult>(0UL, address, properties);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000743D6 File Offset: 0x000725D6
		private TResult LoadGenericRecipientObjectByPuidOrMemberName<TResult>(ulong puid, SmtpAddress address, IEnumerable<PropertyDefinition> properties) where TResult : ADObject, new()
		{
			return this.LoadEntryByPuidOrMemberName<TResult>(puid, address, properties, Activator.CreateInstance<TResult>());
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000743EB File Offset: 0x000725EB
		private ADRawEntry LoadADRawEntryByPuidOrMemberName(ulong puid, SmtpAddress address, IEnumerable<PropertyDefinition> properties)
		{
			return this.LoadEntryByPuidOrMemberName<ADRawEntry>(puid, address, properties, null);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000743F8 File Offset: 0x000725F8
		private TResult LoadEntryByPuidOrMemberName<TResult>(ulong puid, SmtpAddress address, IEnumerable<PropertyDefinition> props, ADObject dummyInstance) where TResult : ADRawEntry, new()
		{
			List<PropertyDefinition> list = (props != null) ? new List<PropertyDefinition>(props) : new List<PropertyDefinition>();
			if (dummyInstance != null)
			{
				list.AddRange(dummyInstance.Schema.AllProperties);
			}
			bool flag = dummyInstance == null;
			bool flag2 = dummyInstance is MiniObject;
			bool readOnly = this.readOnly || flag || flag2;
			List<ADPropertyDefinition> list2;
			List<MServPropertyDefinition> list3;
			List<MbxPropertyDefinition> list4;
			AggregationHelper.FilterPropertyDefinitionsByBackendSource(list, ((IAggregateSession)this).MbxReadMode, out list2, out list3, out list4);
			DirectoryBackendType directoryBackendType = DirectoryBackendType.None;
			if (list4.Count > 0 || puid == 0UL)
			{
				list3.Add(MServRecipientSchema.Database);
			}
			ADRawEntry adrawEntry = null;
			if (list3.Count > 0)
			{
				directoryBackendType |= DirectoryBackendType.MServ;
				if (!base.IsDirectoryBackendMServ)
				{
					throw new ArgumentException("Wrong lookup type - this directory session does not support MServ backend");
				}
				if (address != SmtpAddress.Empty)
				{
					adrawEntry = AggregationHelper.PerformMservLookupByMemberName(address, this.readOnly, list3);
				}
				else
				{
					if (puid == 0UL)
					{
						return default(TResult);
					}
					adrawEntry = AggregationHelper.PerformMservLookupByPuid(puid, this.readOnly, list3);
				}
				if (adrawEntry == null)
				{
					return default(TResult);
				}
			}
			ADObjectId adobjectId = (adrawEntry != null) ? adrawEntry.Id : ConsumerIdentityHelper.GetADObjectIdFromPuid(puid);
			Guid mdbGuid = Guid.Empty;
			ADObjectId adobjectId2 = (ADObjectId)adrawEntry[MServRecipientSchema.Database];
			bool flag3 = list4.Count > 0 && adobjectId2 != null && adobjectId2.PartitionFQDN != null && ((IAggregateSession)this).MbxReadMode != MbxReadMode.NoMbxRead;
			if (list4.Count > 0 && (adobjectId2 == null || adobjectId2.PartitionFQDN == null) && ((IAggregateSession)this).MbxReadMode == MbxReadMode.Always)
			{
				throw new NoLocatorInformationInMServException();
			}
			if (flag3)
			{
				if (new PartitionId(adobjectId2.PartitionFQDN) != PartitionId.LocalForest)
				{
					throw new NotLocalMaiboxException();
				}
				mdbGuid = adobjectId2.ObjectGuid;
			}
			ADRawEntry adResult = null;
			if (list2.Count > 0)
			{
				directoryBackendType |= DirectoryBackendType.AD;
				if (!base.IsDirectoryBackendAD)
				{
					throw new ArgumentException("Wrong lookup type - this directory session does not support AD backend");
				}
				adResult = AggregationHelper.PerformADLookup(adobjectId, list2);
			}
			ADRawEntry adrawEntry2 = null;
			if (flag3)
			{
				if (!base.IsDirectoryBackendMbx)
				{
					throw new ArgumentException("Wrong lookup type - this directory session does not support Mbx backend, is it running on FE role?");
				}
				try
				{
					adrawEntry2 = AggregationHelper.PerformMbxLookupByPuid(adobjectId, mdbGuid, this.readOnly, list4);
					directoryBackendType |= DirectoryBackendType.Mbx;
				}
				catch (ADDriverStoreAccessPermanentException ex)
				{
					if (this.mbxReadMode != MbxReadMode.OnlyIfLocatorDataAvailable || ex.InnerException == null || !(ex.InnerException is MapiExceptionUserInformationNotFound))
					{
						throw;
					}
				}
			}
			ADPropertyBag adpropertyBag = new ADPropertyBag(readOnly, list.Count<PropertyDefinition>());
			adpropertyBag.SetField(ADObjectSchema.Id, adrawEntry[ADObjectSchema.Id]);
			foreach (PropertyDefinition propertyDefinition in list)
			{
				this.CopyPropertyToResultingPropertyBag((ADPropertyDefinition)propertyDefinition, adpropertyBag, adrawEntry, adrawEntry2, adResult);
			}
			TResult result;
			if (flag)
			{
				result = (TResult)((object)base.CreateAndInitializeADRawEntry(adpropertyBag));
			}
			else
			{
				result = (TResult)((object)this.CreateAndInitializeObject<TResult>(adpropertyBag, dummyInstance));
			}
			if (!this.readOnly)
			{
				result.MservPropertyBag = ((adrawEntry != null) ? ((ADPropertyBag)adrawEntry.propertyBag) : new ADPropertyBag());
				result.MbxPropertyBag = ((adrawEntry2 != null) ? ((ADPropertyBag)adrawEntry2.propertyBag) : new ADPropertyBag());
			}
			result.SetId(adrawEntry.Id);
			result.OriginatingServer = TemplateTenantConfiguration.GetLocalTemplateTenant().OriginatingServer;
			result.DirectoryBackendType = directoryBackendType;
			result.WhenReadUTC = new DateTime?(DateTime.UtcNow);
			ValidationError[] array = result.ValidateRead();
			if (array.Length > 0)
			{
				foreach (ValidationError validationError in array)
				{
					PropertyValidationError propertyValidationError = validationError as PropertyValidationError;
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_VALIDATION_FAILED_ATTRIBUTE, puid.ToString(), new object[]
					{
						puid.ToString(),
						"<AggregateTenantRecipientSession>",
						(propertyValidationError != null) ? propertyValidationError.PropertyDefinition.Name : string.Empty,
						validationError.Description,
						(propertyValidationError != null) ? (propertyValidationError.InvalidData ?? string.Empty) : string.Empty
					});
				}
				if (base.ConsistencyMode == ConsistencyMode.IgnoreInvalid)
				{
					return default(TResult);
				}
			}
			result.ResetChangeTracking(true);
			return result;
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x00074858 File Offset: 0x00072A58
		protected override ADObject CreateAndInitializeObject<TResult>(ADPropertyBag propertyBag, ADRawEntry dummyInstance)
		{
			return ADObjectFactory.CreateAndInitializeRecipientObject<TResult>(propertyBag, dummyInstance, this);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00074864 File Offset: 0x00072A64
		private void CopyPropertyToResultingPropertyBag(ADPropertyDefinition adProp, ADPropertyBag resultingPropertyBag, ADRawEntry mservResult, ADRawEntry mbxResult, ADRawEntry adResult)
		{
			if (adProp.MbxPropertyDefinition != null && mbxResult != null && mbxResult.propertyBag.Contains(adProp.MbxPropertyDefinition))
			{
				resultingPropertyBag.SetField(adProp, mbxResult[adProp.MbxPropertyDefinition]);
			}
			else if (adProp.MServPropertyDefinition != null)
			{
				resultingPropertyBag.SetField(adProp, mservResult[adProp.MServPropertyDefinition]);
			}
			else if (!adProp.IsCalculated)
			{
				resultingPropertyBag.SetField(adProp, adResult[adProp]);
			}
			else
			{
				foreach (ProviderPropertyDefinition providerPropertyDefinition in adProp.SupportingProperties)
				{
					ADPropertyDefinition adProp2 = (ADPropertyDefinition)providerPropertyDefinition;
					this.CopyPropertyToResultingPropertyBag(adProp2, resultingPropertyBag, mservResult, mbxResult, adResult);
				}
			}
			if (adProp == ADRecipientSchema.EmailAddresses)
			{
				ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)resultingPropertyBag[adProp];
				proxyAddressCollection.CopyChangesOnly = true;
			}
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00074950 File Offset: 0x00072B50
		private ulong GetPuidByLegacyExchangeDN(string legacyExchangeDN, bool throwIfError = true)
		{
			ulong result;
			if (!ConsumerIdentityHelper.TryGetPuidFromLegacyExchangeDN(legacyExchangeDN, out result) && throwIfError)
			{
				Exception ex = this.HandleUnsupportedInput(legacyExchangeDN, "GetPuidByLegacyExchangeDN");
				if (!ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("TolerateInvalidInputInAggregateSession"))
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00074988 File Offset: 0x00072B88
		private ulong GetPuidByADObjectId(ADObjectId adObjectId, bool throwIfError = true)
		{
			ulong result;
			if (!ConsumerIdentityHelper.TryGetPuidFromADObjectId(adObjectId, out result) && throwIfError)
			{
				Exception ex = this.HandleUnsupportedInput(adObjectId, "GetPuidByADObjectId");
				if (!ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("TolerateInvalidInputInAggregateSession"))
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000749C0 File Offset: 0x00072BC0
		private ulong GetPuidByGuid(Guid exchangeGuid, bool throwIfError = true)
		{
			ulong result;
			if (!ConsumerIdentityHelper.TryGetPuidFromGuid(exchangeGuid, out result) && throwIfError)
			{
				Exception ex = this.HandleUnsupportedInput(exchangeGuid, "GetPuidByGuid");
				if (!ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("TolerateInvalidInputInAggregateSession"))
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000749FC File Offset: 0x00072BFC
		private ulong GetPuidBySecurityIdentifier(SecurityIdentifier sid, bool throwIfError = true)
		{
			ulong result;
			if (!ConsumerIdentityHelper.TryGetPuidFromSecurityIdentifier(sid, out result) && throwIfError)
			{
				Exception ex = this.HandleUnsupportedInput(sid, "GetPuidBySecurityIdentifier");
				if (!ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("TolerateInvalidInputInAggregateSession"))
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00074A34 File Offset: 0x00072C34
		private ulong GetPuidByExternalDirectoryObjectId(string externalDirectoryObjectId, bool throwIfError = true)
		{
			ulong result;
			if (!ConsumerIdentityHelper.TryGetPuidByExternalDirectoryObjectId(externalDirectoryObjectId, out result) && throwIfError)
			{
				Exception ex = this.HandleUnsupportedInput(externalDirectoryObjectId, "GetPuidByExternalDirectoryObjectId");
				if (!ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("TolerateInvalidInputInAggregateSession"))
				{
					throw ex;
				}
			}
			return result;
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00074A6A File Offset: 0x00072C6A
		private SmtpAddress GetSmtpAddressByProxyAddress(ProxyAddress proxyAddress, bool throwIfError = true)
		{
			return new SmtpAddress(proxyAddress.ValueString);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00074A90 File Offset: 0x00072C90
		private Result<TData>[] ReadMultiple<TKey, TIdentity, TData>(TKey[] keys, AggregateTenantRecipientSession.KeyConverter<TKey, TIdentity> keyConverter, AggregateTenantRecipientSession.RawDataRetriever<TIdentity, TData> dataRetriever, IEnumerable<PropertyDefinition> properties = null) where TData : class
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Rank > 1)
			{
				throw new ArgumentOutOfRangeException("keys", DirectoryStrings.ExArgumentOutOfRangeException("keys.Rank", keys.Rank));
			}
			if (keys.Length == 0)
			{
				return new Result<TData>[0];
			}
			if (keyConverter == null)
			{
				throw new ArgumentNullException("keyConverter");
			}
			if (dataRetriever == null)
			{
				throw new ArgumentException("dataRetriever cannot be null");
			}
			TIdentity[] array = (from k in keys
			select keyConverter(k, true)).ToArray<TIdentity>();
			Result<TData>[] array2 = new Result<TData>[keys.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				TData tdata = dataRetriever(array[i], properties);
				array2[i] = ((tdata != null) ? new Result<TData>(tdata, null) : new Result<TData>(default(TData), ProviderError.NotFound));
			}
			return array2;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00074B88 File Offset: 0x00072D88
		private void CreateOrUpdateEntry(ADRecipient instanceToSave)
		{
			IList<PropertyDefinition> allProperties = ADRecipientProperties.Instance.AllProperties;
			bool flag = false;
			bool flag2 = false;
			ExTraceGlobals.ADSaveTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "AggregateTenantRecipientSession::CreateOrUpdateEntry - updating or creating entry {0}", instanceToSave.Id);
			foreach (PropertyDefinition propertyDefinition in allProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (!adpropertyDefinition.IsCalculated && (instanceToSave.propertyBag.IsChanged(adpropertyDefinition) || adpropertyDefinition.IsMultivalued) && (instanceToSave.ObjectState != ObjectState.New || !adpropertyDefinition.PersistDefaultValue || !adpropertyDefinition.DefaultValue.Equals(instanceToSave[adpropertyDefinition])))
				{
					object obj = null;
					instanceToSave.propertyBag.TryGetField(adpropertyDefinition, ref obj);
					if (!adpropertyDefinition.IsMultivalued || (obj != null && ((MultiValuedPropertyBase)obj).Changed))
					{
						ExTraceGlobals.ADSaveTracer.TraceDebug<string>((long)this.GetHashCode(), "AggregateTenantRecipientSession::Save - updating {0}", adpropertyDefinition.Name);
						if (adpropertyDefinition.MServPropertyDefinition != null)
						{
							if (this.BackendWriteMode == BackendWriteMode.WriteToMServ)
							{
								flag = true;
								if (flag2)
								{
									throw new ADOperationException(DirectoryStrings.MservAndMbxExclusive);
								}
								this.ApplyPropertyChangeToMserv(instanceToSave, adpropertyDefinition);
							}
							else if (adpropertyDefinition.MbxPropertyDefinition == null)
							{
								throw new ADOperationException(DirectoryStrings.NotInWriteToMServMode(adpropertyDefinition.Name));
							}
						}
						if (adpropertyDefinition.MbxPropertyDefinition != null)
						{
							if (this.BackendWriteMode == BackendWriteMode.WriteToMbx)
							{
								flag2 = true;
								if (flag)
								{
									throw new ADOperationException(DirectoryStrings.MservAndMbxExclusive);
								}
								this.ApplyPropertyChangeToMbx(instanceToSave, adpropertyDefinition);
							}
							else if (adpropertyDefinition.MServPropertyDefinition == null)
							{
								throw new ADOperationException(DirectoryStrings.NotInWriteToMbxMode(adpropertyDefinition.Name));
							}
						}
						if (adpropertyDefinition.MServPropertyDefinition == null && adpropertyDefinition.MbxPropertyDefinition == null && ((adpropertyDefinition.DefaultValue == null && instanceToSave[adpropertyDefinition] != null) || !adpropertyDefinition.DefaultValue.Equals(instanceToSave[adpropertyDefinition])) && adpropertyDefinition != ADRecipientSchema.UMDtmfMap)
						{
							throw new ADOperationException(DirectoryStrings.AggregatedSessionCannotMakeADChanges(adpropertyDefinition.Name));
						}
					}
				}
			}
			if (!flag)
			{
				if (flag2)
				{
					if (this.BackendWriteMode != BackendWriteMode.WriteToMbx)
					{
						throw new InvalidOperationException("AggregateTenantRecipientSession instance with BackendWriteMode != WriteToMbx cannot be used to change Mbx-backed properties");
					}
					ADObjectId adobjectId = (ADObjectId)instanceToSave[ADMailboxRecipientSchema.Database];
					Guid guid = (adobjectId != null) ? adobjectId.ObjectGuid : Guid.Empty;
					Guid guid2 = (instanceToSave[ADMailboxRecipientSchema.ExchangeGuid] != null) ? ((Guid)instanceToSave[ADMailboxRecipientSchema.ExchangeGuid]) : Guid.Empty;
					if (guid == Guid.Empty || guid2 == Guid.Empty)
					{
						throw new ADOperationException(DirectoryStrings.AggregatedSessionCannotMakeMbxChanges);
					}
					AggregationHelper.PerformMbxModification(guid, guid2, instanceToSave.MbxPropertyBag, !instanceToSave.DirectoryBackendType.HasFlag(DirectoryBackendType.Mbx));
				}
				return;
			}
			if (this.BackendWriteMode != BackendWriteMode.WriteToMServ)
			{
				throw new InvalidOperationException("AggregateTenantRecipientSession instance with BackendWriteMode != WriteToMServ cannot be used to change MServ-backed properties");
			}
			AggregationHelper.PerformMservModification(instanceToSave.MservPropertyBag);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00074E58 File Offset: 0x00073058
		private void ApplyPropertyChangeToMserv(ADRawEntry instanceToSave, ADPropertyDefinition property)
		{
			if (instanceToSave.MservPropertyBag == null)
			{
				instanceToSave.MservPropertyBag = new ADPropertyBag(false, 4);
				instanceToSave.MservPropertyBag[MServRecipientSchema.Id] = instanceToSave.Id;
			}
			if (!property.IsMultivalued)
			{
				instanceToSave.MservPropertyBag[property.MServPropertyDefinition] = instanceToSave.propertyBag[property];
				return;
			}
			MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)instanceToSave.propertyBag[property];
			if (multiValuedPropertyBase == null || multiValuedPropertyBase.WasCleared)
			{
				throw new MservOperationException(DirectoryStrings.NoResetOrAssignedMvp);
			}
			MultiValuedPropertyBase multiValuedPropertyBase2 = (MultiValuedPropertyBase)instanceToSave.MservPropertyBag[property.MServPropertyDefinition];
			foreach (object item in multiValuedPropertyBase.Removed)
			{
				multiValuedPropertyBase2.Remove(item);
			}
			foreach (object item2 in multiValuedPropertyBase.Added)
			{
				multiValuedPropertyBase2.Add(item2);
			}
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00074F48 File Offset: 0x00073148
		private void ApplyPropertyChangeToMbx(ADRawEntry instanceToSave, ADPropertyDefinition property)
		{
			if (instanceToSave.MbxPropertyBag == null)
			{
				instanceToSave.MbxPropertyBag = new ADPropertyBag(false, 4);
			}
			if (!property.IsMultivalued)
			{
				instanceToSave.MbxPropertyBag[property.MbxPropertyDefinition] = instanceToSave.propertyBag[property];
				return;
			}
			MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)instanceToSave.propertyBag[property];
			MultiValuedPropertyBase multiValuedPropertyBase2 = (MultiValuedPropertyBase)instanceToSave.MbxPropertyBag[property.MbxPropertyDefinition];
			multiValuedPropertyBase2.Clear();
			foreach (object item in ((IEnumerable)multiValuedPropertyBase))
			{
				multiValuedPropertyBase2.Add(item);
			}
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00075004 File Offset: 0x00073204
		private void DeleteEntry(ADRecipient instanceToDelete)
		{
			ExTraceGlobals.ADSaveTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "AggregateTenantRecipientSession::DeleteEntry - updating or creating entry {0}", instanceToDelete.Id);
			throw new NotImplementedException("Delete");
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x0007502C File Offset: 0x0007322C
		// (set) Token: 0x06001BC8 RID: 7112 RVA: 0x00075034 File Offset: 0x00073234
		MbxReadMode IAggregateSession.MbxReadMode
		{
			get
			{
				return this.mbxReadMode;
			}
			set
			{
				if (value == MbxReadMode.Always && !base.IsDirectoryBackendMbx)
				{
					throw new ArgumentException("Cannot enable Mbx reads if server role is not BE");
				}
				this.mbxReadMode = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x00075054 File Offset: 0x00073254
		// (set) Token: 0x06001BCA RID: 7114 RVA: 0x0007505C File Offset: 0x0007325C
		public BackendWriteMode BackendWriteMode
		{
			get
			{
				return this.backendWriteMode;
			}
			set
			{
				if (value == BackendWriteMode.WriteToMbx && !base.IsDirectoryBackendMbx)
				{
					throw new ArgumentException("Cannot enable Mbx writes if server role is not BE");
				}
				this.backendWriteMode = value;
			}
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0007507C File Offset: 0x0007327C
		ADRawEntry IDirectorySession.ReadADRawEntry(ADObjectId adObjectId, IEnumerable<PropertyDefinition> properties)
		{
			ulong puidByADObjectId = this.GetPuidByADObjectId(adObjectId, true);
			return this.LoadADRawEntryByPuid(puidByADObjectId, properties);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0007509A File Offset: 0x0007329A
		Result<ADRawEntry>[] IDirectorySession.FindByADObjectIds(ADObjectId[] ids, params PropertyDefinition[] properties)
		{
			return this.ReadMultiple<ADObjectId, ulong, ADRawEntry>(ids, new AggregateTenantRecipientSession.KeyConverter<ADObjectId, ulong>(this.GetPuidByADObjectId), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADRawEntry>(this.LoadADRawEntryByPuid), properties);
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000750BC File Offset: 0x000732BC
		Result<TData>[] IDirectorySession.FindByADObjectIds<TData>(ADObjectId[] ids)
		{
			return this.ReadMultiple<ADObjectId, ulong, TData>(ids, new AggregateTenantRecipientSession.KeyConverter<ADObjectId, ulong>(this.GetPuidByADObjectId), new AggregateTenantRecipientSession.RawDataRetriever<ulong, TData>(this.LoadGenericRecipientByPuid<TData>), null);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x000750DE File Offset: 0x000732DE
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			((IRecipientSession)this).Delete((ADRecipient)instance);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000750EC File Offset: 0x000732EC
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			if (!typeof(ADRecipient).IsAssignableFrom(typeof(T)))
			{
				throw new ArgumentException(DirectoryStrings.ErrorWrongTypeParameter);
			}
			ADRecipient adrecipient = ((IRecipientSession)this).Read((ADObjectId)identity);
			if (!(adrecipient is T))
			{
				return null;
			}
			return adrecipient;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0007513C File Offset: 0x0007333C
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			((IRecipientSession)this).Save((ADRecipient)instance);
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x0007514A File Offset: 0x0007334A
		ADObjectId IRecipientSession.SearchRoot
		{
			get
			{
				return this.searchRoot;
			}
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00075152 File Offset: 0x00073352
		void IRecipientSession.Delete(ADRecipient instanceToDelete)
		{
			throw this.HandleUnsupportedApi("OM:1298014", "Delete");
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00075164 File Offset: 0x00073364
		ADRawEntry IRecipientSession.FindADRawEntryBySid(SecurityIdentifier sid, IEnumerable<PropertyDefinition> properties)
		{
			ulong puidBySecurityIdentifier = this.GetPuidBySecurityIdentifier(sid, true);
			return this.LoadADRawEntryByPuid(puidBySecurityIdentifier, properties);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00075182 File Offset: 0x00073382
		Result<ADRecipient>[] IRecipientSession.FindADRecipientsByLegacyExchangeDNs(string[] legacyExchangeDNs)
		{
			return this.ReadMultiple<string, ulong, ADRecipient>(legacyExchangeDNs, new AggregateTenantRecipientSession.KeyConverter<string, ulong>(this.GetPuidByLegacyExchangeDN), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADRecipient>(this.LoadGenericRecipientByPuid<ADRecipient>), null);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x000751A4 File Offset: 0x000733A4
		ADUser IRecipientSession.FindADUserByObjectId(ADObjectId adObjectId)
		{
			ulong puidByADObjectId = this.GetPuidByADObjectId(adObjectId, true);
			return this.LoadGenericRecipientByPuid<ADUser>(puidByADObjectId, null);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x000751C4 File Offset: 0x000733C4
		ADUser IRecipientSession.FindADUserByExternalDirectoryObjectId(string externalDirectoryObjectId)
		{
			ulong puidByExternalDirectoryObjectId = this.GetPuidByExternalDirectoryObjectId(externalDirectoryObjectId, true);
			return this.LoadGenericRecipientByPuid<ADUser>(puidByExternalDirectoryObjectId, null);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x000751E4 File Offset: 0x000733E4
		ADRawEntry IRecipientSession.FindByExchangeGuid(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			ulong puidByGuid = this.GetPuidByGuid(exchangeGuid, true);
			return this.LoadADRawEntryByPuid(puidByGuid, properties);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00075204 File Offset: 0x00073404
		TData IRecipientSession.FindByExchangeGuid<TData>(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			ulong puidByGuid = this.GetPuidByGuid(exchangeGuid, true);
			return this.LoadGenericRecipientByPuid<TData>(puidByGuid, properties);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00075224 File Offset: 0x00073424
		ADRecipient IRecipientSession.FindByExchangeGuid(Guid exchangeGuid)
		{
			ulong puidByGuid = this.GetPuidByGuid(exchangeGuid, true);
			return this.LoadGenericRecipientByPuid<ADRecipient>(puidByGuid, null);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00075242 File Offset: 0x00073442
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid)
		{
			return ((IRecipientSession)this).FindByExchangeGuid(exchangeGuid);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0007524B File Offset: 0x0007344B
		ADRawEntry IRecipientSession.FindByExchangeGuidIncludingAlternate(Guid exchangeGuid, IEnumerable<PropertyDefinition> properties)
		{
			return ((IRecipientSession)this).FindByExchangeGuid(exchangeGuid, properties);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00075258 File Offset: 0x00073458
		TData IRecipientSession.FindByExchangeGuidIncludingAlternate<TData>(Guid exchangeGuid)
		{
			ulong puidByGuid = this.GetPuidByGuid(exchangeGuid, true);
			return this.LoadGenericRecipientByPuid<TData>(puidByGuid, null);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00075276 File Offset: 0x00073476
		ADRecipient IRecipientSession.FindByExchangeGuidIncludingArchive(Guid exchangeGuid)
		{
			return ((IRecipientSession)this).FindByExchangeGuid(exchangeGuid);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0007527F File Offset: 0x0007347F
		Result<ADRecipient>[] IRecipientSession.FindByExchangeGuidsIncludingArchive(Guid[] keys)
		{
			return this.ReadMultiple<Guid, ulong, ADRecipient>(keys, new AggregateTenantRecipientSession.KeyConverter<Guid, ulong>(this.GetPuidByGuid), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADRecipient>(this.LoadGenericRecipientByPuid<ADRecipient>), null);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x000752A1 File Offset: 0x000734A1
		ADRecipient IRecipientSession.FindByExchangeObjectId(Guid exchangeObjectId)
		{
			return ((IRecipientSession)this).FindByExchangeGuid(exchangeObjectId);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000752AC File Offset: 0x000734AC
		ADRecipient IRecipientSession.FindByLegacyExchangeDN(string legacyExchangeDN)
		{
			ulong puidByLegacyExchangeDN = this.GetPuidByLegacyExchangeDN(legacyExchangeDN, true);
			return this.LoadGenericRecipientByPuid<ADRecipient>(puidByLegacyExchangeDN, null);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x000752CA File Offset: 0x000734CA
		Result<ADRawEntry>[] IRecipientSession.FindByLegacyExchangeDNs(string[] legacyExchangeDNs, params PropertyDefinition[] properties)
		{
			return this.ReadMultiple<string, ulong, ADRawEntry>(legacyExchangeDNs, new AggregateTenantRecipientSession.KeyConverter<string, ulong>(this.GetPuidByLegacyExchangeDN), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADRawEntry>(this.LoadADRawEntryByPuid), properties);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x000752EC File Offset: 0x000734EC
		ADRecipient IRecipientSession.FindByObjectGuid(Guid guid)
		{
			return ((IRecipientSession)this).FindByExchangeGuid(guid);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000752F5 File Offset: 0x000734F5
		ADRecipient IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress)
		{
			return this.LoadGenericRecipientByMemberName<ADRecipient>(new SmtpAddress(proxyAddress.ValueString), null);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x00075309 File Offset: 0x00073509
		ADRawEntry IRecipientSession.FindByProxyAddress(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return this.LoadADRawEntryByMemberName(this.GetSmtpAddressByProxyAddress(proxyAddress, true), properties);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0007531A File Offset: 0x0007351A
		TData IRecipientSession.FindByProxyAddress<TData>(ProxyAddress proxyAddress)
		{
			return this.LoadGenericRecipientByMemberName<TData>(this.GetSmtpAddressByProxyAddress(proxyAddress, true), null);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0007532B File Offset: 0x0007352B
		Result<ADRawEntry>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses, params PropertyDefinition[] properties)
		{
			return this.ReadMultiple<ProxyAddress, SmtpAddress, ADRawEntry>(proxyAddresses, new AggregateTenantRecipientSession.KeyConverter<ProxyAddress, SmtpAddress>(this.GetSmtpAddressByProxyAddress), new AggregateTenantRecipientSession.RawDataRetriever<SmtpAddress, ADRawEntry>(this.LoadADRawEntryByMemberName), properties);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0007534D File Offset: 0x0007354D
		Result<TData>[] IRecipientSession.FindByProxyAddresses<TData>(ProxyAddress[] proxyAddresses)
		{
			return this.ReadMultiple<ProxyAddress, SmtpAddress, TData>(proxyAddresses, new AggregateTenantRecipientSession.KeyConverter<ProxyAddress, SmtpAddress>(this.GetSmtpAddressByProxyAddress), new AggregateTenantRecipientSession.RawDataRetriever<SmtpAddress, TData>(this.LoadGenericRecipientByMemberName<TData>), null);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0007536F File Offset: 0x0007356F
		Result<ADRecipient>[] IRecipientSession.FindByProxyAddresses(ProxyAddress[] proxyAddresses)
		{
			return this.ReadMultiple<ProxyAddress, SmtpAddress, ADRecipient>(proxyAddresses, new AggregateTenantRecipientSession.KeyConverter<ProxyAddress, SmtpAddress>(this.GetSmtpAddressByProxyAddress), new AggregateTenantRecipientSession.RawDataRetriever<SmtpAddress, ADRecipient>(this.LoadGenericRecipientByMemberName<ADRecipient>), null);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00075394 File Offset: 0x00073594
		ADRecipient IRecipientSession.FindBySid(SecurityIdentifier sid)
		{
			ulong puidBySecurityIdentifier = this.GetPuidBySecurityIdentifier(sid, true);
			return this.LoadGenericRecipientByPuid<ADRecipient>(puidBySecurityIdentifier, null);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000753B2 File Offset: 0x000735B2
		TResult IRecipientSession.FindMiniRecipientByProxyAddress<TResult>(ProxyAddress proxyAddress, IEnumerable<PropertyDefinition> properties)
		{
			return this.LoadGenericRecipientByMemberName<TResult>(this.GetSmtpAddressByProxyAddress(proxyAddress, true), properties);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000753C4 File Offset: 0x000735C4
		TResult IRecipientSession.FindMiniRecipientBySid<TResult>(SecurityIdentifier sid, IEnumerable<PropertyDefinition> properties)
		{
			ulong puidBySecurityIdentifier = this.GetPuidBySecurityIdentifier(sid, true);
			return this.LoadGenericRecipientByPuid<TResult>(puidBySecurityIdentifier, properties);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000753E2 File Offset: 0x000735E2
		MiniRecipientWithTokenGroups IRecipientSession.ReadTokenGroupsGlobalAndUniversal(ADObjectId id)
		{
			throw this.HandleUnsupportedApi("OM:1054958", "ReadTokenGroupsGlobalAndUniversal");
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x000753F4 File Offset: 0x000735F4
		List<string> IRecipientSession.GetTokenSids(ADRawEntry user, AssignmentMethod assignmentMethodFlags)
		{
			return ((IRecipientSession)this).GetTokenSids(user.Id, assignmentMethodFlags);
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00075403 File Offset: 0x00073603
		List<string> IRecipientSession.GetTokenSids(ADObjectId userId, AssignmentMethod assignmentMethodFlags)
		{
			return AggregateTenantRecipientSession.defaultTokenSids;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0007540C File Offset: 0x0007360C
		ADRecipient IRecipientSession.Read(ADObjectId adObjectId)
		{
			ulong puidByADObjectId = this.GetPuidByADObjectId(adObjectId, true);
			return this.LoadGenericRecipientByPuid<ADRecipient>(puidByADObjectId, null);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0007542C File Offset: 0x0007362C
		MiniRecipient IRecipientSession.ReadMiniRecipient(ADObjectId adObjectId, IEnumerable<PropertyDefinition> properties)
		{
			ulong puidByADObjectId = this.GetPuidByADObjectId(adObjectId, true);
			return this.LoadGenericRecipientByPuid<MiniRecipient>(puidByADObjectId, properties);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0007544C File Offset: 0x0007364C
		TMiniRecipient IRecipientSession.ReadMiniRecipient<TMiniRecipient>(ADObjectId adObjectId, IEnumerable<PropertyDefinition> properties)
		{
			ulong puidByADObjectId = this.GetPuidByADObjectId(adObjectId, true);
			return this.LoadGenericRecipientByPuid<TMiniRecipient>(puidByADObjectId, properties);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0007546C File Offset: 0x0007366C
		ADRawEntry IRecipientSession.FindUserBySid(SecurityIdentifier sid, IList<PropertyDefinition> properties)
		{
			ulong puidBySecurityIdentifier = this.GetPuidBySecurityIdentifier(sid, true);
			return this.LoadADRawEntryByPuid(puidBySecurityIdentifier, properties);
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0007548A File Offset: 0x0007368A
		Result<ADRecipient>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds)
		{
			return this.ReadMultiple<ADObjectId, ulong, ADRecipient>(entryIds, new AggregateTenantRecipientSession.KeyConverter<ADObjectId, ulong>(this.GetPuidByADObjectId), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADRecipient>(this.LoadGenericRecipientByPuid<ADRecipient>), null);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000754AC File Offset: 0x000736AC
		Result<ADRawEntry>[] IRecipientSession.ReadMultiple(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return this.ReadMultiple<ADObjectId, ulong, ADRawEntry>(entryIds, new AggregateTenantRecipientSession.KeyConverter<ADObjectId, ulong>(this.GetPuidByADObjectId), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADRawEntry>(this.LoadADRawEntryByPuid), properties);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000754CE File Offset: 0x000736CE
		Result<ADUser>[] IRecipientSession.ReadMultipleADUsers(ADObjectId[] userIds)
		{
			return this.ReadMultiple<ADObjectId, ulong, ADUser>(userIds, new AggregateTenantRecipientSession.KeyConverter<ADObjectId, ulong>(this.GetPuidByADObjectId), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADUser>(this.LoadGenericRecipientByPuid<ADUser>), null);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000754F0 File Offset: 0x000736F0
		Result<ADRawEntry>[] IRecipientSession.ReadMultipleWithDeletedObjects(ADObjectId[] entryIds, params PropertyDefinition[] properties)
		{
			return ((IRecipientSession)this).ReadMultiple(entryIds, properties);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00075514 File Offset: 0x00073714
		ADObjectId[] IRecipientSession.ResolveSidsToADObjectIds(string[] sids)
		{
			ulong[] source = (from k in sids
			select this.GetPuidBySecurityIdentifier(new SecurityIdentifier(k), true)).ToArray<ulong>();
			return (from k in source
			select ConsumerIdentityHelper.GetADObjectIdFromPuid(k)).ToArray<ADObjectId>();
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00075563 File Offset: 0x00073763
		void IRecipientSession.Save(ADRecipient instanceToSave)
		{
			((IRecipientSession)this).Save(instanceToSave, false);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00075570 File Offset: 0x00073770
		void IRecipientSession.Save(ADRecipient instanceToSave, bool bypassValidation)
		{
			ExTraceGlobals.ADSaveTracer.TraceDebug((long)this.GetHashCode(), "AggregateTenantRecipientSession::Save - saving object of type {0} and ID {1}.", new object[]
			{
				(instanceToSave == null) ? "<null>" : instanceToSave.GetType().Name,
				(instanceToSave == null) ? "<null>" : instanceToSave.Id
			});
			if (instanceToSave == null)
			{
				throw new ArgumentNullException("instanceToSave");
			}
			if (instanceToSave.Id == null)
			{
				throw new ArgumentNullException("instanceToSave.Id");
			}
			if (this.readOnly)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionInvalidOperationOnReadOnlySession("Save()"));
			}
			if (instanceToSave.IsReadOnly)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionInvalidOperationOnReadOnlyObject("Save()"));
			}
			if (!bypassValidation)
			{
				ValidationError[] array = instanceToSave.Validate();
				if (array.Length > 0)
				{
					throw new DataValidationException(array[0]);
				}
			}
			if (this.BackendWriteMode == BackendWriteMode.NoWrites)
			{
				throw new ArgumentException("Cannot complete Save operation - BackendWriteMode is set to NoWrites");
			}
			if (instanceToSave.ObjectState == ObjectState.Deleted)
			{
				throw new InvalidOperationException(DirectoryStrings.ExceptionObjectHasBeenDeleted);
			}
			if (instanceToSave.ObjectState == ObjectState.Unchanged && (instanceToSave.MbxPropertyBag == null || !instanceToSave.MbxPropertyBag.Changed))
			{
				return;
			}
			if (instanceToSave.ObjectState == ObjectState.New || instanceToSave.ObjectState == ObjectState.Changed)
			{
				this.CreateOrUpdateEntry(instanceToSave);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0007569D File Offset: 0x0007389D
		public override DirectoryBackendType DirectoryBackendType
		{
			get
			{
				return this.directoryBackendType;
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000756A5 File Offset: 0x000738A5
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, params PropertyDefinition[] properties)
		{
			return ((ITenantRecipientSession)this).FindByExternalDirectoryObjectIds(externalDirectoryObjectIds, false, properties);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000756B0 File Offset: 0x000738B0
		Result<ADRawEntry>[] ITenantRecipientSession.FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, bool includeDeletedObjects, params PropertyDefinition[] properties)
		{
			return this.ReadMultiple<string, ulong, ADRawEntry>(externalDirectoryObjectIds, new AggregateTenantRecipientSession.KeyConverter<string, ulong>(this.GetPuidByExternalDirectoryObjectId), new AggregateTenantRecipientSession.RawDataRetriever<ulong, ADRawEntry>(this.LoadADRawEntryByPuid), properties);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000756D2 File Offset: 0x000738D2
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			return ((ITenantRecipientSession)this).FindByNetID(netID, properties);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000756DC File Offset: 0x000738DC
		ADRawEntry[] ITenantRecipientSession.FindByNetID(string netID, params PropertyDefinition[] properties)
		{
			return new ADRawEntry[]
			{
				((ITenantRecipientSession)this).FindUniqueEntryByNetID(netID, properties)
			};
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000756FC File Offset: 0x000738FC
		MiniRecipient ITenantRecipientSession.FindRecipientByNetID(NetID netId)
		{
			ulong puid = netId.ToUInt64();
			return this.LoadGenericRecipientByPuid<MiniRecipient>(puid, null);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00075718 File Offset: 0x00073918
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, string organizationContext, params PropertyDefinition[] properties)
		{
			return ((ITenantRecipientSession)this).FindUniqueEntryByNetID(netID, properties);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00075722 File Offset: 0x00073922
		ADRawEntry ITenantRecipientSession.FindUniqueEntryByNetID(string netID, params PropertyDefinition[] properties)
		{
			return this.LoadADRawEntryByPuid(ConsumerIdentityHelper.ConvertPuidStringToPuidNumber(netID), properties);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00075731 File Offset: 0x00073931
		ADRawEntry ITenantRecipientSession.FindByLiveIdMemberName(string liveIdMemberName, params PropertyDefinition[] properties)
		{
			return this.LoadADRawEntryByMemberName(new SmtpAddress(liveIdMemberName), properties);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00075740 File Offset: 0x00073940
		void IDirectorySession.AnalyzeDirectoryError(PooledLdapConnection connection, DirectoryRequest request, DirectoryException de, int totalRetries, int retriesOnServer)
		{
			throw this.HandleUnsupportedApi(null, "AnalyzeDirectoryError");
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0007574E File Offset: 0x0007394E
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADObjectId rootId, ADObject dummyInstance, bool applyImplicitFilter)
		{
			throw this.HandleUnsupportedApi(null, "ApplyDefaultFilters");
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0007575C File Offset: 0x0007395C
		QueryFilter IDirectorySession.ApplyDefaultFilters(QueryFilter filter, ADScope scope, ADObject dummyInstance, bool applyImplicitFilter)
		{
			throw this.HandleUnsupportedApi(null, "ApplyDefaultFilters");
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x0007576A File Offset: 0x0007396A
		void IDirectorySession.CheckFilterForUnsafeIdentity(QueryFilter filter)
		{
			throw this.HandleUnsupportedApi(null, "CheckFilterForUnsafeIdentity");
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00075778 File Offset: 0x00073978
		void IDirectorySession.UnsafeExecuteModificationRequest(DirectoryRequest request, ADObjectId rootId)
		{
			throw this.HandleUnsupportedApi(null, "UnsafeExecuteModificationRequest");
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00075786 File Offset: 0x00073986
		ADRawEntry[] IDirectorySession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "Find");
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00075794 File Offset: 0x00073994
		TResult[] IDirectorySession.Find<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			throw this.HandleUnsupportedApi(null, "Find");
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000757A2 File Offset: 0x000739A2
		ADRawEntry[] IDirectorySession.FindAllADRawEntriesByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, bool useAtomicFilter, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindAllADRawEntriesByUsnRange");
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000757B0 File Offset: 0x000739B0
		Result<ADRawEntry>[] IDirectorySession.FindByCorrelationIds(Guid[] correlationIds, ADObjectId configUnit, params PropertyDefinition[] properties)
		{
			throw this.HandleUnsupportedApi(null, "FindByCorrelationIds");
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000757BE File Offset: 0x000739BE
		Result<ADRawEntry>[] IDirectorySession.FindByExchangeLegacyDNs(string[] exchangeLegacyDNs, params PropertyDefinition[] properties)
		{
			throw this.HandleUnsupportedApi(null, "FindByExchangeLegacyDNs");
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x000757CC File Offset: 0x000739CC
		Result<ADRawEntry>[] IDirectorySession.FindByObjectGuids(Guid[] objectGuids, params PropertyDefinition[] properties)
		{
			throw this.HandleUnsupportedApi(null, "FindByObjectGuids");
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x000757DA File Offset: 0x000739DA
		ADRawEntry[] IDirectorySession.FindDeletedTenantSyncObjectByUsnRange(ADObjectId tenantOuRoot, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindDeletedTenantSyncObjectByUsnRange");
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000757E8 File Offset: 0x000739E8
		ADPagedReader<TResult> IDirectorySession.FindPaged<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindPaged");
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000757F6 File Offset: 0x000739F6
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindPagedADRawEntry");
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00075804 File Offset: 0x00073A04
		ADPagedReader<ADRawEntry> IDirectorySession.FindPagedADRawEntryWithDefaultFilters<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindPagedADRawEntryWithDefaultFilters");
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00075812 File Offset: 0x00073A12
		ADPagedReader<TResult> IDirectorySession.FindPagedDeletedObject<TResult>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			throw this.HandleUnsupportedApi(null, "FindPagedDeletedObject");
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00075820 File Offset: 0x00073A20
		ADObjectId IDirectorySession.GetConfigurationNamingContext()
		{
			return TemplateTenantConfiguration.GetTempateTenantRecipientSession().GetConfigurationNamingContext();
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0007582C File Offset: 0x00073A2C
		ADObjectId IDirectorySession.GetConfigurationUnitsRoot()
		{
			throw this.HandleUnsupportedApi(null, "GetConfigurationUnitsRoot");
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0007583A File Offset: 0x00073A3A
		ADObjectId IDirectorySession.GetDomainNamingContext()
		{
			return TemplateTenantConfiguration.GetTempateTenantRecipientSession().GetDomainNamingContext();
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00075846 File Offset: 0x00073A46
		ADObjectId IDirectorySession.GetHostedOrganizationsRoot()
		{
			throw this.HandleUnsupportedApi(null, "GetHostedOrganizationsRoot");
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00075854 File Offset: 0x00073A54
		ADObjectId IDirectorySession.GetRootDomainNamingContext()
		{
			return TemplateTenantConfiguration.GetTempateTenantRecipientSession().GetRootDomainNamingContext();
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00075860 File Offset: 0x00073A60
		ADObjectId IDirectorySession.GetSchemaNamingContext()
		{
			return TemplateTenantConfiguration.GetTempateTenantRecipientSession().GetSchemaNamingContext();
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0007586C File Offset: 0x00073A6C
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, ref ADObjectId rootId)
		{
			throw this.HandleUnsupportedApi(null, "GetReadConnection");
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x0007587A File Offset: 0x00073A7A
		PooledLdapConnection IDirectorySession.GetReadConnection(string preferredServer, string optionalBaseDN, ref ADObjectId rootId, ADRawEntry scopeDeteriminingObject)
		{
			throw this.HandleUnsupportedApi(null, "GetReadConnection");
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00075888 File Offset: 0x00073A88
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject)
		{
			throw this.HandleUnsupportedApi(null, "GetReadScope");
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00075896 File Offset: 0x00073A96
		ADScope IDirectorySession.GetReadScope(ADObjectId rootId, ADRawEntry scopeDeterminingObject, bool isWellKnownGuidSearch, out ConfigScopes applicableScope)
		{
			throw this.HandleUnsupportedApi(null, "GetReadScope");
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000758A4 File Offset: 0x00073AA4
		bool IDirectorySession.GetSchemaAndApplyFilter(ADRawEntry adRawEntry, ADScope scope, out ADObject dummyInstance, out string[] ldapAttributes, ref QueryFilter filter, ref IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "GetSchemaAndApplyFilter");
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000758B2 File Offset: 0x00073AB2
		bool IDirectorySession.IsReadConnectionAvailable()
		{
			return true;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000758B5 File Offset: 0x00073AB5
		bool IDirectorySession.IsRootIdWithinScope<TData>(ADObjectId rootId)
		{
			return true;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000758B8 File Offset: 0x00073AB8
		bool IDirectorySession.IsTenantIdentity(ADObjectId id)
		{
			throw this.HandleUnsupportedApi(null, "IsTenantIdentity");
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000758C6 File Offset: 0x00073AC6
		public override SecurityDescriptor ReadSecurityDescriptorBlob(ADObjectId id)
		{
			return TemplateTenantConfiguration.GetTemplateUserSecurityDescriptorBlob();
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000758CD File Offset: 0x00073ACD
		string[] IDirectorySession.ReplicateSingleObject(ADObject instanceToReplicate, ADObjectId[] sites)
		{
			throw this.HandleUnsupportedApi(null, "ReplicateSingleObject");
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000758DB File Offset: 0x00073ADB
		bool IDirectorySession.ReplicateSingleObjectToTargetDC(ADObject instanceToReplicate, string targetServerName)
		{
			throw this.HandleUnsupportedApi(null, "ReplicateSingleObjectToTargetDC");
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000758E9 File Offset: 0x00073AE9
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, ADObjectId containerId)
		{
			throw this.HandleUnsupportedApi(null, "ResolveWellKnownGuid");
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000758F7 File Offset: 0x00073AF7
		TResult IDirectorySession.ResolveWellKnownGuid<TResult>(Guid wellKnownGuid, string containerDN)
		{
			throw this.HandleUnsupportedApi(null, "ResolveWellKnownGuid");
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00075905 File Offset: 0x00073B05
		TenantRelocationSyncObject IDirectorySession.RetrieveTenantRelocationSyncObject(ADObjectId adObjectId, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "RetrieveTenantRelocationSyncObject");
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00075913 File Offset: 0x00073B13
		ADOperationResultWithData<TResult>[] IDirectorySession.RunAgainstAllDCsInSite<TResult>(ADObjectId siteId, Func<TResult> methodToCall)
		{
			throw this.HandleUnsupportedApi(null, "RunAgainstAllDCsInSite");
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00075921 File Offset: 0x00073B21
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd)
		{
			throw this.HandleUnsupportedApi(null, "SaveSecurityDescriptor");
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0007592F File Offset: 0x00073B2F
		void IDirectorySession.SaveSecurityDescriptor(ADObjectId id, RawSecurityDescriptor sd, bool modifyOwner)
		{
			throw this.HandleUnsupportedApi(null, "SaveSecurityDescriptor");
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0007593D File Offset: 0x00073B3D
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd)
		{
			throw this.HandleUnsupportedApi(null, "SaveSecurityDescriptor");
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0007594B File Offset: 0x00073B4B
		void IDirectorySession.SaveSecurityDescriptor(ADObject obj, RawSecurityDescriptor sd, bool modifyOwner)
		{
			throw this.HandleUnsupportedApi(null, "SaveSecurityDescriptor");
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00075959 File Offset: 0x00073B59
		bool IDirectorySession.TryVerifyIsWithinScopes(ADObject entry, bool isModification, out ADScopeException exception)
		{
			exception = null;
			return true;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0007595F File Offset: 0x00073B5F
		void IDirectorySession.UpdateServerSettings(PooledLdapConnection connection)
		{
			throw this.HandleUnsupportedApi(null, "UpdateServerSettings");
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0007596D File Offset: 0x00073B6D
		void IDirectorySession.VerifyIsWithinScopes(ADObject entry, bool isModification)
		{
			throw this.HandleUnsupportedApi(null, "VerifyIsWithinScopes");
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0007597B File Offset: 0x00073B7B
		TResult[] IDirectorySession.ObjectsFromEntries<TResult>(SearchResultEntryCollection entries, string originatingServerName, IEnumerable<PropertyDefinition> properties, ADRawEntry dummyInstance)
		{
			throw this.HandleUnsupportedApi(null, "ObjectsFromEntries");
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00075989 File Offset: 0x00073B89
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			throw this.HandleUnsupportedApi(null, "Find");
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00075997 File Offset: 0x00073B97
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			throw this.HandleUnsupportedApi(null, "FindPaged");
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x000759A5 File Offset: 0x00073BA5
		string IConfigDataProvider.Source
		{
			get
			{
				return DirectoryBackendType.MServ.ToString();
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000759B2 File Offset: 0x00073BB2
		ITableView IRecipientSession.Browse(ADObjectId addressListId, int rowCountSuggestion, params PropertyDefinition[] properties)
		{
			throw this.HandleUnsupportedApi(null, "Browse");
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000759C0 File Offset: 0x00073BC0
		ADRecipient[] IRecipientSession.Find(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			throw this.HandleUnsupportedApi(null, "Find");
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000759CE File Offset: 0x00073BCE
		ADRawEntry[] IRecipientSession.FindADRawEntryByUsnRange(ADObjectId root, long startUsn, long endUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryScope scope, QueryFilter additionalFilter)
		{
			throw this.HandleUnsupportedApi(null, "FindADRawEntryByUsnRange");
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000759DC File Offset: 0x00073BDC
		ADUser[] IRecipientSession.FindADUser(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults)
		{
			throw this.HandleUnsupportedApi(null, "FindADUser");
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000759EA File Offset: 0x00073BEA
		ADObject IRecipientSession.FindByAccountName<T>(string domainName, string accountName)
		{
			throw this.HandleUnsupportedApi(null, "FindByAccountName");
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000759F8 File Offset: 0x00073BF8
		IEnumerable<T> IRecipientSession.FindByAccountName<T>(string domain, string account, ADObjectId rootId, QueryFilter searchFilter)
		{
			throw this.HandleUnsupportedApi(null, "FindByAccountName");
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00075A06 File Offset: 0x00073C06
		ADRecipient[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy)
		{
			throw this.HandleUnsupportedApi(null, "FindByANR");
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00075A14 File Offset: 0x00073C14
		ADRawEntry[] IRecipientSession.FindByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindByANR");
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00075A22 File Offset: 0x00073C22
		ADRecipient IRecipientSession.FindByCertificate(X509Identifier identifier)
		{
			throw this.HandleUnsupportedApi(null, "FindByCertificate");
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00075A30 File Offset: 0x00073C30
		ADRawEntry[] IRecipientSession.FindByCertificate(X509Identifier identifier, params PropertyDefinition[] properties)
		{
			throw this.HandleUnsupportedApi(null, "FindByCertificate");
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00075A3E File Offset: 0x00073C3E
		ADRawEntry[] IRecipientSession.FindDeletedADRawEntryByUsnRange(ADObjectId lastKnownParentId, long startUsn, int sizeLimit, IEnumerable<PropertyDefinition> properties, QueryFilter additionalFilter)
		{
			throw this.HandleUnsupportedApi(null, "FindDeletedADRawEntryByUsnRange");
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x00075A4C File Offset: 0x00073C4C
		MiniRecipient[] IRecipientSession.FindMiniRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindMiniRecipient");
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00075A5A File Offset: 0x00073C5A
		MiniRecipient[] IRecipientSession.FindMiniRecipientByANR(string anrMatch, int maxResults, SortBy sortBy, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindMiniRecipientByANR");
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00075A68 File Offset: 0x00073C68
		ADRecipient[] IRecipientSession.FindNames(IDictionary<PropertyDefinition, object> dictionary, int limit)
		{
			throw this.HandleUnsupportedApi(null, "FindNames");
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00075A76 File Offset: 0x00073C76
		object[][] IRecipientSession.FindNamesView(IDictionary<PropertyDefinition, object> dictionary, int limit, params PropertyDefinition[] properties)
		{
			throw this.HandleUnsupportedApi(null, "FindNamesView");
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00075A84 File Offset: 0x00073C84
		Result<OWAMiniRecipient>[] IRecipientSession.FindOWAMiniRecipientByUserPrincipalName(string[] userPrincipalNames)
		{
			throw this.HandleUnsupportedApi(null, "FindOWAMiniRecipientByUserPrincipalName");
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00075A92 File Offset: 0x00073C92
		ADPagedReader<ADRecipient> IRecipientSession.FindPaged(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize)
		{
			throw this.HandleUnsupportedApi(null, "FindPaged");
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x00075AA0 File Offset: 0x00073CA0
		ADPagedReader<TData> IRecipientSession.FindPagedMiniRecipient<TData>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindPagedMiniRecipient");
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00075AAE File Offset: 0x00073CAE
		ADRawEntry[] IRecipientSession.FindRecipient(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults, IEnumerable<PropertyDefinition> properties)
		{
			throw this.HandleUnsupportedApi(null, "FindRecipient");
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00075ABC File Offset: 0x00073CBC
		IEnumerable<ADGroup> IRecipientSession.FindRoleGroupsByForeignGroupSid(ADObjectId root, SecurityIdentifier sid)
		{
			throw this.HandleUnsupportedApi(null, "FindRoleGroupsByForeignGroupSid");
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00075ACA File Offset: 0x00073CCA
		SecurityIdentifier IRecipientSession.GetWellKnownExchangeGroupSid(Guid wkguid)
		{
			throw this.HandleUnsupportedApi(null, "GetWellKnownExchangeGroupSid");
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00075AD8 File Offset: 0x00073CD8
		bool IRecipientSession.IsLegacyDNInUse(string legacyDN)
		{
			throw this.HandleUnsupportedApi(null, "IsLegacyDNInUse");
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x00075AE6 File Offset: 0x00073CE6
		bool IRecipientSession.IsMemberOfGroupByWellKnownGuid(Guid wellKnownGuid, string containerDN, ADObjectId id)
		{
			throw this.HandleUnsupportedApi(null, "IsMemberOfGroupByWellKnownGuid");
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00075AF4 File Offset: 0x00073CF4
		bool IRecipientSession.IsRecipientInOrg(ProxyAddress proxyAddress)
		{
			throw this.HandleUnsupportedApi(null, "IsRecipientInOrg");
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00075B02 File Offset: 0x00073D02
		bool IRecipientSession.IsReducedRecipientSession()
		{
			return false;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00075B05 File Offset: 0x00073D05
		bool IRecipientSession.IsThrottlingPolicyInUse(ADObjectId throttlingPolicyId)
		{
			throw this.HandleUnsupportedApi(null, "IsThrottlingPolicyInUse");
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00075B13 File Offset: 0x00073D13
		Result<ADGroup>[] IRecipientSession.ReadMultipleADGroups(ADObjectId[] entryIds)
		{
			throw this.HandleUnsupportedApi(null, "ReadMultipleADGroups");
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00075B21 File Offset: 0x00073D21
		void IRecipientSession.SetPassword(ADObject obj, SecureString password)
		{
			throw this.HandleUnsupportedApi(null, "SetPassword");
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00075B2F File Offset: 0x00073D2F
		void IRecipientSession.SetPassword(ADObjectId id, SecureString password)
		{
			throw this.HandleUnsupportedApi(null, "SetPassword");
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00075B3D File Offset: 0x00073D3D
		ADRawEntry ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADRawEntry[] entries)
		{
			throw this.HandleUnsupportedApi(null, "ChooseBetweenAmbiguousUsers");
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00075B4B File Offset: 0x00073D4B
		ADObjectId ITenantRecipientSession.ChooseBetweenAmbiguousUsers(ADObjectId user1Id, ADObjectId user2Id)
		{
			throw this.HandleUnsupportedApi(null, "ChooseBetweenAmbiguousUsers");
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00075B59 File Offset: 0x00073D59
		Result<ADRawEntry>[] ITenantRecipientSession.ReadMultipleByLinkedPartnerId(LinkedPartnerGroupInformation[] entryIds, params PropertyDefinition[] properties)
		{
			throw this.HandleUnsupportedApi(null, "ReadMultipleByLinkedPartnerId");
		}

		// Token: 0x04000BDB RID: 3035
		private static string className = typeof(AggregateTenantRecipientSession).FullName;

		// Token: 0x04000BDC RID: 3036
		private static List<string> defaultTokenSids = new List<string>
		{
			"S-1-1-0",
			"S-1-5-11"
		};

		// Token: 0x04000BDD RID: 3037
		private MbxReadMode mbxReadMode;

		// Token: 0x04000BDE RID: 3038
		private BackendWriteMode backendWriteMode;

		// Token: 0x04000BDF RID: 3039
		private readonly DirectoryBackendType directoryBackendType;

		// Token: 0x02000210 RID: 528
		// (Invoke) Token: 0x06001C57 RID: 7255
		private delegate TIdentity KeyConverter<TKey, TIdentity>(TKey key, bool throwOnError);

		// Token: 0x02000211 RID: 529
		// (Invoke) Token: 0x06001C5B RID: 7259
		private delegate TData DataRetriever<TIdentity, TData>(TIdentity id);

		// Token: 0x02000212 RID: 530
		// (Invoke) Token: 0x06001C5F RID: 7263
		private delegate TData RawDataRetriever<TIdentity, TData>(TIdentity id, IEnumerable<PropertyDefinition> properties);
	}
}
