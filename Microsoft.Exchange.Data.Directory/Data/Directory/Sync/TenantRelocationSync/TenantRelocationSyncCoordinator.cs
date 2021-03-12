using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x0200080E RID: 2062
	internal class TenantRelocationSyncCoordinator : DisposeTrackableBase
	{
		// Token: 0x17002402 RID: 9218
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x00164E25 File Offset: 0x00163025
		// (set) Token: 0x060065B8 RID: 26040 RVA: 0x00164E2C File Offset: 0x0016302C
		internal static int LinksPerPageLimit { get; private set; }

		// Token: 0x17002403 RID: 9219
		// (get) Token: 0x060065B9 RID: 26041 RVA: 0x00164E34 File Offset: 0x00163034
		// (set) Token: 0x060065BA RID: 26042 RVA: 0x00164E3B File Offset: 0x0016303B
		internal static int LinksOverldapSize { get; private set; }

		// Token: 0x17002404 RID: 9220
		// (get) Token: 0x060065BB RID: 26043 RVA: 0x00164E43 File Offset: 0x00163043
		// (set) Token: 0x060065BC RID: 26044 RVA: 0x00164E4B File Offset: 0x0016304B
		internal TenantRelocationSyncData SyncData { get; private set; }

		// Token: 0x060065BD RID: 26045 RVA: 0x00164E54 File Offset: 0x00163054
		static TenantRelocationSyncCoordinator()
		{
			TenantRelocationSyncCoordinator.InitializeConfigurableSettings();
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x00164F90 File Offset: 0x00163190
		public TenantRelocationSyncCoordinator(TenantRelocationSyncData syncConfigData)
		{
			this.SyncData = syncConfigData;
			this.throttlingManager = new TenantRelocationThrottlingManager(this.SyncData.Target.PartitionId.ForestFQDN);
			this.perfLogger = new TenantRelocationSyncPerfLogger(syncConfigData);
			this.adDriverValidatorEnabled = new Lazy<bool>(() => TenantRelocationConfigImpl.GetConfig<bool>("ADDriverValidatorEnabled"));
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x00164FFE File Offset: 0x001631FE
		public TenantRelocationSyncCoordinator(TenantRelocationSyncData syncConfigData, WaitHandle[] breakEvents) : this(syncConfigData)
		{
			this.breakEvents = breakEvents;
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x0016500E File Offset: 0x0016320E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TenantRelocationSyncCoordinator>(this);
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x00165016 File Offset: 0x00163216
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.throttlingManager.Dispose();
			}
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x00165026 File Offset: 0x00163226
		private bool FindObjectInTargetByDn(ADObjectId dn, out ADRawEntry obj)
		{
			return this.FindObjectInTargetByDn(dn, TenantRelocationSyncCoordinator.identityProperties, out obj);
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x00165038 File Offset: 0x00163238
		private bool FindObjectInTargetByDn(ADObjectId dn, PropertyDefinition[] props, out ADRawEntry obj)
		{
			bool useConfigNC = this.targetPartitionSession.UseConfigNC;
			if (dn.IsDescendantOf(this.SyncData.Target.PartitionConfigNcRoot))
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "FindObjectInTargetByDn: object {0} is in target configuration NC.", dn);
				this.targetPartitionSession.UseConfigNC = true;
			}
			else
			{
				this.targetPartitionSession.UseConfigNC = false;
			}
			try
			{
				obj = this.targetPartitionSession.ReadADRawEntry(dn, props);
			}
			finally
			{
				this.targetPartitionSession.UseConfigNC = useConfigNC;
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "FindObjectInTargetByDn: object {0}: found:{1}", dn.DistinguishedName, obj != null);
			return obj != null;
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x001650F4 File Offset: 0x001632F4
		private bool FindTargetObjectByCorrelationId(Guid cId, out ADRawEntry obj)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)this.GetHashCode(), "FindTargetObjectByCorrelationId: check if a correlation ID exists in target:{0}", cId);
			obj = this.Translator.FindByCorrelationIdWrapper(cId, TenantRelocationSyncCoordinator.identityProperties);
			if (obj == null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)this.GetHashCode(), "FindObjectInTargetByDn: correlation ID {0} not found", cId);
				return false;
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "FindObjectInTargetByDn: correlation ID {0} found, DN:{1}", cId, obj.Id.DistinguishedName);
			return true;
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x0016516C File Offset: 0x0016336C
		private void CreateObject(TenantRelocationSyncObject obj)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "CreateObject: Create object in target for source object {0}", obj.Id.DistinguishedName);
			this.CreatePlaceHolder(obj);
			this.UpdateObject(obj, RequestType.ModifyFullObject);
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x001651A0 File Offset: 0x001633A0
		private bool CompareObjectClasses(ADRawEntry obj1, ADRawEntry obj2)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)obj1[ADObjectSchema.ObjectClass];
			MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)obj2[ADObjectSchema.ObjectClass];
			return multiValuedProperty.Count == multiValuedProperty2.Count && multiValuedProperty[0].Equals(multiValuedProperty2[0]) && multiValuedProperty[multiValuedProperty.Count - 1].Equals(multiValuedProperty2[multiValuedProperty2.Count - 1]);
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x00165218 File Offset: 0x00163418
		private void ForceSyncObject(ADRawEntry targetObject)
		{
			Guid guid = (Guid)targetObject[ADObjectSchema.CorrelationIdRaw];
			ADObjectId entryId = new ADObjectId(guid);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ForceSyncObject: force sync object with correlation Id: {0}", guid);
			TenantRelocationSyncObject tenantRelocationSyncObject = this.sourcePartitionSession.RetrieveTenantRelocationSyncObject(entryId, TenantRelocationSyncCoordinator.placeholderProperties);
			if (tenantRelocationSyncObject == null)
			{
				throw new InvalidOperationException("ForceSyncObject: correlation Id not found");
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ForceSyncObject: found object with correlation Id: {0} in the source forest, DN: {1}", guid, tenantRelocationSyncObject.Id.DistinguishedName);
			if (tenantRelocationSyncObject.IsDeleted)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ForceSyncObject: source object {0} is a deleted object. Delete target obejct {1}", tenantRelocationSyncObject.Id.DistinguishedName, targetObject.Id.DistinguishedName);
				this.DeleteObjectWithLDAP(targetObject.Id);
				return;
			}
			this.UpdateObject(tenantRelocationSyncObject, RequestType.ModifyIncremental);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ForceSyncObject: Updated object in the target for source object: {0}", tenantRelocationSyncObject.Id.DistinguishedName);
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x00165300 File Offset: 0x00163500
		private void CreatePlaceHolder(TenantRelocationSyncObject obj)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "CreatePlaceHolder: entered with source DN:{0}", obj.Id.DistinguishedName);
			if (!this.SyncData.Source.IsUnderTenantScope(obj.Id) || this.SyncData.Source.IsTenantRootObject(obj.Id))
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "CreatePlaceHolder: object:{0} is not in the tenant scope or is root object, skipped", obj.Id.DistinguishedName);
				return;
			}
			ADObjectId adobjectId = this.Translator.SyntacticallyMapDistinguishedName(obj.Id);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "CreatePlaceHolder: syntactically map source DN:{0} to target DN:{1}", obj.Id.DistinguishedName, adobjectId.DistinguishedName);
			ADRawEntry adrawEntry;
			if (this.FindObjectInTargetByDn(adobjectId, out adrawEntry))
			{
				Guid arg = (Guid)adrawEntry[ADObjectSchema.CorrelationIdRaw];
				if (arg.Equals(Guid.Empty))
				{
					throw new InvalidOperationException("null correlation ID on target object, not expected");
				}
				if (arg.Equals(obj.Guid) && this.CompareObjectClasses(adrawEntry, obj))
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "CreatePlaceHolder: the object {0} exists in target forest with the same objectclass, correlation Id:{1}", adobjectId.DistinguishedName, arg);
					return;
				}
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "CreatePlaceHolder: the object {0} exists in target forest with the same objectclass or different correlation Id, force a sync on the object, so the object will be renamed", adobjectId.DistinguishedName);
				this.ForceSyncObject(adrawEntry);
				if (this.FindObjectInTargetByDn(adobjectId, out adrawEntry))
				{
					throw new InvalidOperationException("conflicting object exists");
				}
			}
			this.FixAncestry(obj.Id.Parent, null);
			this.CreateObjectWithLDAP(obj, RequestType.AddPlaceHolder);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "CreatePlaceHolder: placeholder for source object:{0} was created in target forest", obj.Id.DistinguishedName);
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x0016549C File Offset: 0x0016369C
		private void ReadAndCreatePlaceHolder(ADObjectId sourceDn)
		{
			TenantRelocationSyncObject obj = this.RetrieveTenantRelocationSyncObjectWrapper(sourceDn);
			this.CreatePlaceHolder(obj);
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x001654B8 File Offset: 0x001636B8
		private TenantRelocationSyncObject RetrieveTenantRelocationSyncObjectWrapper(ADObjectId sourceDn)
		{
			TenantRelocationSyncObject tenantRelocationSyncObject = this.sourcePartitionSession.RetrieveTenantRelocationSyncObject(sourceDn, TenantRelocationSyncCoordinator.placeholderProperties);
			if (tenantRelocationSyncObject == null)
			{
				if (this.SyncData.Source.IsConfigurationUnitUnderConfigNC)
				{
					bool useConfigNC = this.sourcePartitionSession.UseConfigNC;
					this.sourcePartitionSession.UseConfigNC = true;
					try
					{
						tenantRelocationSyncObject = this.sourcePartitionSession.RetrieveTenantRelocationSyncObject(sourceDn, TenantRelocationSyncCoordinator.placeholderProperties);
					}
					finally
					{
						this.sourcePartitionSession.UseConfigNC = useConfigNC;
					}
				}
				if (tenantRelocationSyncObject == null)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "RetrieveTenantRelocationSyncObjectWrapper: source object not found:{0}", sourceDn.ToDNString());
					throw new ADNoSuchObjectException(LocalizedString.Empty);
				}
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "RetrieveTenantRelocationSyncObjectWrapper: retrieve object {0} from source forest", tenantRelocationSyncObject.Id.DistinguishedName);
			return tenantRelocationSyncObject;
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x00165584 File Offset: 0x00163784
		private void FixAncestry(ADObjectId currentSourceObjectDn, ADObjectId mappedTargetDn)
		{
			if (this.SyncData.Source.IsTenantRootObject(currentSourceObjectDn) || !this.SyncData.Source.IsUnderTenantScope(currentSourceObjectDn))
			{
				return;
			}
			ADObjectId adobjectId = this.Translator.SyntacticallyMapDistinguishedName(currentSourceObjectDn);
			if (mappedTargetDn == null)
			{
				mappedTargetDn = this.Translator.ResolveSourceDistinguishedName(currentSourceObjectDn, true);
			}
			if (mappedTargetDn == null)
			{
				this.ReadAndCreatePlaceHolder(currentSourceObjectDn);
				return;
			}
			if (mappedTargetDn.Equals(adobjectId))
			{
				return;
			}
			this.FixAncestry(currentSourceObjectDn.Parent, null);
			mappedTargetDn = this.Translator.ResolveSourceDistinguishedName(currentSourceObjectDn, true);
			if (mappedTargetDn != null && !mappedTargetDn.Equals(adobjectId))
			{
				ADRawEntry adrawEntry;
				if (this.FindObjectInTargetByDn(adobjectId, out adrawEntry) && !adrawEntry.Id.ObjectGuid.Equals(mappedTargetDn.ObjectGuid))
				{
					this.ForceSyncObject(adrawEntry);
				}
				this.RenameObjectWithLDAP(mappedTargetDn, adobjectId);
			}
			DistinguishedNameMapItem distinguishedNameMapItem = this.Translator.Mappings.LookupBySourceADObjectId(currentSourceObjectDn);
			if (distinguishedNameMapItem != null)
			{
				this.Translator.Mappings.Remove(distinguishedNameMapItem);
				this.Translator.Mappings.Insert(currentSourceObjectDn, adobjectId, currentSourceObjectDn.ObjectGuid);
			}
		}

		// Token: 0x060065CC RID: 26060 RVA: 0x00165688 File Offset: 0x00163888
		private void RenameObjectWithLDAP(ADObjectId currentObjId, ADObjectId newObjId)
		{
			ModifyDNRequest modifyDNRequest = new ModifyDNRequest();
			modifyDNRequest.DistinguishedName = currentObjId.DistinguishedName;
			modifyDNRequest.NewName = newObjId.Rdn.ToString();
			modifyDNRequest.NewParentDistinguishedName = newObjId.Parent.DistinguishedName;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "RenameObject: DistinguishedName: {0}, NewName: {1}, NewParentDistinguishedName: {2}", modifyDNRequest.DistinguishedName, modifyDNRequest.NewName, modifyDNRequest.NewParentDistinguishedName);
			this.ExecuteModificationRequest(currentObjId, modifyDNRequest);
			TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Object Renamed", null, string.Format("DistinguishedName: {0}, NewName: {1}, NewParentDistinguishedName: {2}", modifyDNRequest.DistinguishedName, modifyDNRequest.NewName, modifyDNRequest.NewParentDistinguishedName), null);
			this.perfLogger.IncrementRenameCount();
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x00165738 File Offset: 0x00163938
		private void CreateObjectWithLDAP(TenantRelocationSyncObject obj, RequestType requestType)
		{
			this.CnfMangledNameCheck(obj);
			ADObjectId rootId;
			AddRequest addRequest = this.Translator.GenerateAddRequest(obj, requestType, out rootId);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, RequestType>((long)this.GetHashCode(), "CreateObjectWithLDAP: AddRequest generated for target object: {0}, requestType:{1}", addRequest.DistinguishedName, requestType);
			this.ExecuteModificationRequest(rootId, addRequest);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "CreateObjectWithLDAP: Executed AddRequest for target object: {0}", addRequest.DistinguishedName);
			this.perfLogger.IncrementPlaceHolderCount();
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x001657A8 File Offset: 0x001639A8
		private long GetUsnFromCookie(TenantRelocationSyncObject obj)
		{
			long result = 0L;
			if (obj.Id.IsDescendantOf(this.SyncData.Source.PartitionConfigNcRoot))
			{
				if (this.cookie.ConfigNcWatermarks != null)
				{
					this.cookie.ConfigNcWatermarks.TryGetValue(this.cookie.InvocationId, out result);
				}
			}
			else if (this.cookie.Watermarks != null)
			{
				this.cookie.Watermarks.TryGetValue(this.cookie.InvocationId, out result);
			}
			return result;
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x00165830 File Offset: 0x00163A30
		private bool FindDistinguishedNameInMVP(string dn, MultiValuedProperty<ADObjectId> list)
		{
			foreach (ADObjectId adobjectId in list)
			{
				if (adobjectId.DistinguishedName != null && adobjectId.DistinguishedName.Equals(dn, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x00165898 File Offset: 0x00163A98
		private ModifyRequest PrefilterSamAttributes(ModifyRequest modRequest)
		{
			bool flag = false;
			foreach (object obj in modRequest.Modifications)
			{
				DirectoryAttributeModification directoryAttributeModification = (DirectoryAttributeModification)obj;
				if (directoryAttributeModification.Name.Equals(ADGroupSchema.Members.LdapDisplayName, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return modRequest;
			}
			ADRawEntry adrawEntry;
			this.FindObjectInTargetByDn(new ADObjectId(modRequest.DistinguishedName), TenantRelocationSyncCoordinator.samAttributes, out adrawEntry);
			if (adrawEntry == null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "PrefilterSamAttributes: target object does not exist: {0}", modRequest.DistinguishedName);
				return modRequest;
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)adrawEntry[ADGroupSchema.Members];
			if (multiValuedProperty == null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "PrefilterSamAttributes: target object does not have values on member: {0}", modRequest.DistinguishedName);
				return modRequest;
			}
			List<DirectoryAttributeModification> list = new List<DirectoryAttributeModification>();
			foreach (object obj2 in modRequest.Modifications)
			{
				DirectoryAttributeModification directoryAttributeModification2 = (DirectoryAttributeModification)obj2;
				if (!directoryAttributeModification2.Name.Equals(ADGroupSchema.Members.LdapDisplayName, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(directoryAttributeModification2);
				}
				else
				{
					object[] values = directoryAttributeModification2.GetValues(typeof(string));
					if (values.Length != 1)
					{
						throw new InvalidOperationException("program inconsistency");
					}
					string text = (string)values[0];
					if (directoryAttributeModification2.Operation == DirectoryAttributeOperation.Add)
					{
						if (!this.FindDistinguishedNameInMVP(text, multiValuedProperty))
						{
							list.Add(directoryAttributeModification2);
						}
						else
						{
							ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "PrefilterSamAttributes: value to add exists on target, skipped: {0}", text);
						}
					}
					else if (directoryAttributeModification2.Operation == DirectoryAttributeOperation.Delete)
					{
						list.Add(directoryAttributeModification2);
					}
				}
			}
			return new ModifyRequest(modRequest.DistinguishedName, list.ToArray());
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x00165A88 File Offset: 0x00163C88
		private void UpdateObjectWithLDAP(TenantRelocationSyncObject obj, RequestType requestType)
		{
			this.perfLogger.IncrementUpdateCount();
			ADObjectId adobjectId;
			UpdateData updateData;
			ModifyRequest modifyRequest = this.Translator.GenerateModifyRequest(obj, requestType, this.GetUsnFromCookie(obj), out adobjectId, out updateData);
			if (modifyRequest == null && !updateData.HasUpdates)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "UpdateObjectWithLDAP: modRequest is null, request skipped: {0}", obj.Id.DistinguishedName);
				return;
			}
			this.ProcessObjectUpdate(requestType, adobjectId, modifyRequest);
			this.ProcessSecurityDescriptorUpdate(obj.Id, updateData, adobjectId);
			this.ProcessShadowPropertyMetadataUpdate(adobjectId, updateData);
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x00165B05 File Offset: 0x00163D05
		private void ProcessSecurityDescriptorUpdate(ADObjectId sourceId, UpdateData mData, ADObjectId targetId)
		{
			if (mData.IsNtSecurityDescriptorChanged)
			{
				this.securityDescriptorHandler.ProcessSecurityDescriptor(sourceId, targetId, mData.RequestType == RequestType.ModifyIncremental);
			}
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x00165B28 File Offset: 0x00163D28
		private void ProcessObjectUpdate(RequestType requestType, ADObjectId rootId, ModifyRequest modRequest)
		{
			if (modRequest != null)
			{
				modRequest = this.PrefilterSamAttributes(modRequest);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, RequestType>((long)this.GetHashCode(), "ProcessObjectUpdate: ModifyRequest generated for target object: {0}, requestType:{1}", modRequest.DistinguishedName, requestType);
				if (modRequest != null && modRequest.Modifications.Count > 0)
				{
					this.ExecuteModificationRequest(rootId, modRequest);
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessObjectUpdate: Executed ModifyRequest for target object: {0}", modRequest.DistinguishedName);
				}
			}
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x00165B94 File Offset: 0x00163D94
		private void ExecuteModificationRequest(ADObjectId rootId, DirectoryRequest request)
		{
			request.Controls.Add(new PermissiveModifyControl());
			this.throttlingManager.Throttle();
			try
			{
				this.targetPartitionSession.UnsafeExecuteModificationRequest(request, rootId);
			}
			catch (Exception ex)
			{
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Exception", null, string.Format("Fail to update object: {0}; Exception: {1}", rootId.ToDNString(), ex.ToString()), null);
				throw;
			}
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x00165C30 File Offset: 0x00163E30
		private void ProcessShadowPropertyMetadataUpdate(ADObjectId objId, UpdateData mData)
		{
			DirectoryAttributeModification directoryAttributeModification = new DirectoryAttributeModification();
			directoryAttributeModification.Operation = DirectoryAttributeOperation.Replace;
			directoryAttributeModification.Name = ADRecipientSchema.ConfigurationXMLRaw.LdapDisplayName;
			if (!objId.IsDescendantOf(this.SyncData.Target.TenantConfigurationUnitRoot) && !objId.IsDescendantOf(this.SyncData.Target.TenantOrganizationUnit))
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessShadowPropertyMetadataUpdate: object out of tenant scope, skipped: {0}", objId.DistinguishedName);
				return;
			}
			bool useConfigNC = this.targetPartitionSession.UseConfigNC;
			this.targetPartitionSession.UseConfigNC = objId.IsDescendantOf(this.SyncData.Target.PartitionConfigNcRoot);
			ADRawEntry adrawEntry;
			try
			{
				adrawEntry = this.targetPartitionSession.ReadADRawEntry(objId, TenantRelocationSyncCoordinator.recipientAttributes);
			}
			finally
			{
				this.targetPartitionSession.UseConfigNC = useConfigNC;
			}
			if (adrawEntry == null)
			{
				throw new ArgumentException("dn");
			}
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)adrawEntry[ADObjectSchema.ObjectClass];
			if (multiValuedProperty.Contains("user"))
			{
				string text = (string)adrawEntry[ADRecipientSchema.ConfigurationXMLRaw];
				UserConfigXML userConfigXML = null;
				UserConfigXML userConfigXML2 = null;
				DateTime relocationLastWriteTime = new DateTime(0L);
				if (!string.IsNullOrEmpty(text))
				{
					userConfigXML = XMLSerializableBase.Deserialize<UserConfigXML>(text, ADRecipientSchema.ConfigurationXML);
				}
				if (!string.IsNullOrEmpty(mData.SourceUserConfigXML))
				{
					userConfigXML2 = XMLSerializableBase.Deserialize<UserConfigXML>(mData.SourceUserConfigXML, ADRecipientSchema.ConfigurationXML);
					DateTime relocationLastWriteTime2 = userConfigXML2.RelocationLastWriteTime;
					relocationLastWriteTime = userConfigXML2.RelocationLastWriteTime;
				}
				UserConfigXML userConfigXML3;
				if (userConfigXML2 != null)
				{
					userConfigXML3 = userConfigXML2;
				}
				else
				{
					userConfigXML3 = new UserConfigXML();
				}
				List<PropertyMetaData> list;
				if (userConfigXML == null || userConfigXML.RelocationShadowPropMetaData == null)
				{
					if (userConfigXML2 != null && userConfigXML2.RelocationShadowPropMetaData != null)
					{
						list = new List<PropertyMetaData>(userConfigXML2.RelocationShadowPropMetaData);
					}
					else
					{
						list = new List<PropertyMetaData>();
					}
				}
				else
				{
					list = new List<PropertyMetaData>(userConfigXML.RelocationShadowPropMetaData);
				}
				userConfigXML3.RelocationLastWriteTime = DateTime.UtcNow;
				using (List<PropertyMetaData>.Enumerator enumerator = mData.ShadowMetadata.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PropertyMetaData pm = enumerator.Current;
						if (!(pm.LastWriteTime <= relocationLastWriteTime))
						{
							PropertyMetaData propertyMetaData = (from x in list
							where x.AttributeName == pm.AttributeName
							select x).FirstOrDefault<PropertyMetaData>();
							if (propertyMetaData != null)
							{
								list.Remove(propertyMetaData);
							}
							list.Add(pm);
						}
					}
				}
				userConfigXML3.RelocationShadowPropMetaData = list.ToArray();
				directoryAttributeModification.Add(userConfigXML3.ToString());
			}
			else
			{
				switch (mData.SourceUserConfigXMLStatus)
				{
				case UpdateData.SourceStatus.None:
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessShadowPropertyMetadataUpdate: nothing to do, returned: {0}", objId.DistinguishedName);
					return;
				case UpdateData.SourceStatus.Updated:
					directoryAttributeModification.Add(mData.SourceUserConfigXML);
					break;
				}
			}
			ModifyRequest modifyRequest = new ModifyRequest();
			modifyRequest.DistinguishedName = objId.DistinguishedName;
			modifyRequest.Modifications.Add(directoryAttributeModification);
			this.ExecuteModificationRequest(adrawEntry.Id, modifyRequest);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessShadowPropertyMetadataUpdate: Executed ModifyRequest for target object: {0}", modifyRequest.DistinguishedName);
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x00165F4C File Offset: 0x0016414C
		private void FixReferences(TenantRelocationSyncObject obj, RequestType requestType)
		{
			ADObjectId[] allDnReferences = this.Translator.GetAllDnReferences(obj, requestType, 0L);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, int>((long)this.GetHashCode(), "FixReferences: get all references for source object {0}, count:{1}", obj.Id.DistinguishedName, allDnReferences.Length);
			this.perfLogger.AddLinkCount(allDnReferences.Length);
			DistinguishedNameMapItem[] array;
			ADObjectId[] array2;
			ADObjectId[] array3;
			this.Translator.ResolveSourceDistinguishedNames(allDnReferences, out array, out array2, out array3);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "FixReferences: resolved references for source object {0}, count:{1}, mapped#:{2}, unmapped#:{3}, nonTenantSpecific#:{4}", new object[]
			{
				obj.Id.DistinguishedName,
				allDnReferences.Length,
				array.Length,
				array2.Length,
				array3.Length
			});
			foreach (ADObjectId adobjectId in array2)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "FixReferences: create placeholder for source object: {0}", adobjectId.DistinguishedName);
				try
				{
					this.ReadAndCreatePlaceHolder(adobjectId);
				}
				catch (ADNoSuchObjectException)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "Object DN cannot be resolved in source forest: {0}, skipped", adobjectId.ToDNString());
					TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Info", "0", string.Format("Object DN cannot be resolved in source forest: {0}, skipped", adobjectId.ToDNString()), null);
				}
			}
			this.Translator.ResolveSourceDistinguishedNames(allDnReferences, out array, out array2, out array3);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "FixReferences exits.");
		}

		// Token: 0x060065D7 RID: 26071 RVA: 0x001660DC File Offset: 0x001642DC
		private void UpdateObject(TenantRelocationSyncObject obj, RequestType requestType)
		{
			ADObjectId adobjectId = this.Translator.SyntacticallyMapDistinguishedName(obj.Id);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UpdateObject: syntactically mapped source object {0} to target object {1}", obj.Id.DistinguishedName, adobjectId.DistinguishedName);
			if (this.SyncData.Source.IsUnderTenantScope(obj.Id))
			{
				ADRawEntry adrawEntry;
				if (!this.FindTargetObjectByCorrelationId(obj.Guid, out adrawEntry))
				{
					throw new InvalidOperationException("cannot find correlation Id");
				}
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "UpdateObject: mapped source object {0} to target object {1}, with correlation Id", obj.Id.DistinguishedName, adrawEntry.Id.DistinguishedName);
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)obj[IADSecurityPrincipalSchema.Sid];
				if (null != securityIdentifier && !this.SyncData.Source.PartitionId.Equals(this.SyncData.Target.PartitionId))
				{
					MultiValuedProperty<SecurityIdentifier> multiValuedProperty = (MultiValuedProperty<SecurityIdentifier>)adrawEntry[IADSecurityPrincipalSchema.SidHistory];
					bool flag = multiValuedProperty != null && multiValuedProperty.Count > 0;
					bool flag2 = flag && multiValuedProperty.Contains(securityIdentifier);
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "UpdateObject: Check Sid History: Source DN: {0}, Target DN: {1}, Target Has SidHistory: {2}, SidHistory Contains Source Sid: {3}", new object[]
					{
						obj.Id.DistinguishedName,
						adrawEntry.Id.DistinguishedName,
						flag,
						flag2
					});
					if (!flag2)
					{
						this.AddSidHistory(obj, adrawEntry);
					}
				}
				this.FixAncestry(obj.Id, adrawEntry.Id);
			}
			this.FixReferences(obj, requestType);
			this.UpdateObjectWithLDAP(obj, requestType);
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x00166287 File Offset: 0x00164487
		public void InitialSync()
		{
			this.PerformSync(true, true);
			if (this.IsEventRaised())
			{
				return;
			}
			this.PerformSync(false, false);
			if (this.IsEventRaised())
			{
				return;
			}
			this.CaptureCompletionTargetVector();
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x001662B1 File Offset: 0x001644B1
		public void DeltaSync()
		{
			this.PerformSync(false, true);
			this.CaptureCompletionTargetVector();
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x001662C1 File Offset: 0x001644C1
		private bool IsEventRaised()
		{
			return this.breakEvents != null && WaitHandle.WaitAny(this.breakEvents, 0) != 258;
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x001662EC File Offset: 0x001644EC
		private void PerformSync(bool initialSync, bool useNewSession = true)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<bool, ADObjectId>((long)this.GetHashCode(), "PerformSync started, initialSync={0}, Source CU={1}", initialSync, this.SyncData.Source.TenantConfigurationUnit);
			if (useNewSession)
			{
				this.PrepareSessions();
			}
			ADSchemaDataProvider.Instance.LoadAllSchemaAttributeObjects();
			this.ProcessCookie(initialSync);
			TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Sync Started", null, null, null);
			this.ProcessDCAffinity();
			if (useNewSession && !this.ReplicationLatencyCheck(this.SyncData.Target.PartitionId, this.targetPartitionSession.DomainController, false))
			{
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Exception", null, "Replication latency in target partition exceeds 5 minutes, will not sync", null);
				throw new TenantRelocationException(RelocationError.HighADReplicationLatency, "TenantRelocationSync detects high replication latency in target forest, abort the relocation request");
			}
			this.securityDescriptorHandler = new TenantRelocationSecurityDescriptorHandler(this.SyncData, this.sourcePartitionSession, this.targetPartitionSession);
			this.Translator = new TenantRelocationSyncTranslator(this.SyncData, this.sourcePartitionSession, this.targetPartitionSession);
			this.PrepopulateRootContainers();
			while (this.cookie.MoreData)
			{
				if (this.IsEventRaised())
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "PerformSync: the break signal has been received, leaving.");
					TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Sync Finished", null, "Caller signaled a break event", null);
					return;
				}
				try
				{
					this.RetryFunc(delegate(int x)
					{
						this.ProcessOnePage();
					}, new TenantRelocationSyncCoordinator.ExceptionHandler(TenantRelocationSyncCoordinator.HandleMostExceptions), 5, 5);
				}
				catch (ADInvalidHandleCookieException)
				{
					if (this.cookie.IsPreSyncPhase)
					{
						this.cookie.ResetPresyncCookie();
						this.PersistSyncCookie(this.cookie);
						TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Exception", null, "ADInvalidHandleCookieException: PresyncCookie reset.", null);
					}
					throw;
				}
			}
			this.perfLogger.Flush();
			TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Sync Finished", "0", "Success", null);
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x001664E0 File Offset: 0x001646E0
		private void PrepopulateRootContainers()
		{
			if (this.cookie.IsPreSyncPhase && this.cookie.PreSyncLdapPagingCookie == null)
			{
				this.StampOrgIds(this.SyncData.Target.TenantConfigurationUnit);
				this.StampOrgIds(this.SyncData.Target.TenantOrganizationUnit);
			}
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x00166534 File Offset: 0x00164734
		private void StampOrgIds(ADObjectId targetDn)
		{
			ModifyRequest modifyRequest = new ModifyRequest();
			modifyRequest.DistinguishedName = targetDn.DistinguishedName;
			this.Translator.AppendOrgIdsToModifyRequest(modifyRequest);
			this.ExecuteModificationRequest(targetDn, modifyRequest);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "StampOrgIds: Executed ModifyRequest for target object: {0}", modifyRequest.DistinguishedName);
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x00166584 File Offset: 0x00164784
		private void RetryFunc(TenantRelocationSyncCoordinator.ProcessAction action, TenantRelocationSyncCoordinator.ExceptionHandler HandleException, int maxNumRetries, int retrySleepInSec)
		{
			int num = 0;
			try
			{
				IL_02:
				action(num);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "RetryFunc, function succeeded, return");
			}
			catch (Exception ex)
			{
				if (!HandleException(ex, num))
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("RetryFunc, unhandled exception, rethrow {0}", ex.ToString()));
					throw;
				}
				if (num >= maxNumRetries || TenantRelocationSyncCoordinator.WaitForBreakEventsOrTimeout(retrySleepInSec, this.breakEvents))
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "RetryFunc, gives up");
					TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Retry limit reach or break event signaled, give up", "0", "Exception", null);
					throw;
				}
				this.Translator.Mappings.ClearCacheIfNecessary(true);
				this.ProcessCookie(true);
				num++;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("RetryFunc, retry, number of retr:{0}, exception: {1}", num, ex.ToString()));
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, string.Format("Retry the page, Exception:{0}", ex.ToString()), "0", "Exception", null);
				goto IL_02;
			}
		}

		// Token: 0x060065DF RID: 26079 RVA: 0x001666B4 File Offset: 0x001648B4
		private void ProcessOnePage()
		{
			bool isSmallTenant = !this.SyncData.LargeTenantModeEnabled;
			TenantRelocationSyncConfiguration tenantRelocationSyncConfiguration = new TenantRelocationSyncConfiguration(this.cookie, Guid.Empty, isSmallTenant, null, null);
			tenantRelocationSyncConfiguration.SetConfiguration(this.sourceRootOrgConfigurationSession, this.sourceSystemConfigurationSession, this.sourceRecipientSession);
			IEnumerable<ADRawEntry> dataPage = tenantRelocationSyncConfiguration.GetDataPage();
			foreach (ADRawEntry adrawEntry in dataPage)
			{
				if (this.IsEventRaised())
				{
					return;
				}
				this.ProcessOneObject((TenantRelocationSyncObject)adrawEntry);
			}
			this.cookie.LastReadFailureStartTime = DateTime.MinValue;
			this.PersistSyncCookie(this.cookie);
			this.Translator.Mappings.ClearCacheIfNecessary(false);
			this.perfLogger.IncrementPageCount();
			TenantRelocationSyncLogger.Instance.Log(this.SyncData, "One page commited, cookie perserved", "0", "Success", null);
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x001667AC File Offset: 0x001649AC
		private static bool HandleMostExceptions(Exception ex, int retryNum)
		{
			if (ex is ADInvalidHandleCookieException)
			{
				return false;
			}
			if (ex is DataSourceTransientException || ex is ADExternalException || ex is ADInvalidPasswordException || ex is ADObjectAlreadyExistsException)
			{
				return true;
			}
			TenantRelocationException ex2 = ex as TenantRelocationException;
			if (ex2 == null)
			{
				return false;
			}
			RelocationError relocationErrorCode = ex2.RelocationErrorCode;
			return relocationErrorCode != RelocationError.SyncFailureDueToSourceObjectBeingModified && relocationErrorCode != RelocationError.RPCPermanentError && relocationErrorCode != RelocationError.ObjectsWithCnfNameFoundInSource;
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x00166810 File Offset: 0x00164A10
		private static bool HandleTenantRelocationLinkValuesShiftedException(Exception ex, int retryNum)
		{
			TenantRelocationException ex2 = ex as TenantRelocationException;
			return ex2 != null && ex2.RelocationErrorCode == RelocationError.SyncFailureDueToSourceObjectBeingModified;
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x00166834 File Offset: 0x00164A34
		internal void ProcessOneObject(TenantRelocationSyncObject syncObject)
		{
			try
			{
				this.ProcessObject(syncObject);
				this.ProcessAdditionalLinksForObject(syncObject);
				if (this.adDriverValidatorEnabled.Value)
				{
					this.ValidateTargetObject(this.Translator.SyntacticallyMapDistinguishedName(syncObject.Id));
				}
			}
			catch (Exception ex)
			{
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Exception", syncObject.Id.DistinguishedName + ":" + ex.GetType().ToString(), ex.Message, null);
				this.perfLogger.Flush();
				throw;
			}
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x001668D0 File Offset: 0x00164AD0
		private static DirectoryAttribute GetRangedPropertyValue(ADPropertyDefinition propertyDefinition, DirectoryAttribute[] attributes, out IntRange returnedRange)
		{
			DirectoryAttribute directoryAttribute = null;
			DirectoryAttribute directoryAttribute2 = null;
			returnedRange = new IntRange(0, int.MaxValue);
			string text = null;
			string value = string.Format(CultureInfo.InvariantCulture, "{0};range=", new object[]
			{
				propertyDefinition.LdapDisplayName
			});
			foreach (DirectoryAttribute directoryAttribute3 in attributes)
			{
				if (directoryAttribute3.Name.Equals(propertyDefinition.LdapDisplayName, StringComparison.InvariantCultureIgnoreCase))
				{
					directoryAttribute2 = directoryAttribute3;
					if (directoryAttribute3.Count > 0)
					{
						break;
					}
				}
				else if (directoryAttribute3.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase))
				{
					directoryAttribute = directoryAttribute3;
					returnedRange = RangedPropertyHelper.GetPropertyRangeFromLdapName(directoryAttribute3.Name, out text);
					break;
				}
			}
			if (directoryAttribute == null)
			{
				directoryAttribute = directoryAttribute2;
			}
			return directoryAttribute;
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x0016699C File Offset: 0x00164B9C
		internal void ProcessAdditionalLinksForObject(TenantRelocationSyncObject obj)
		{
			this.RetryFunc(delegate(int x)
			{
				this.ProcessAdditionalLinksForOneObjectWorker(obj, x);
			}, new TenantRelocationSyncCoordinator.ExceptionHandler(TenantRelocationSyncCoordinator.HandleTenantRelocationLinkValuesShiftedException), 10, 0);
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x001669E0 File Offset: 0x00164BE0
		internal void ProcessAdditionalLinksForOneObjectWorker(TenantRelocationSyncObject obj, int retryNum)
		{
			PropertyDefinition propertyDefinition = ADRecipientSchema.LinkMetadata;
			bool flag = retryNum != 0;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("ProcessAdditionalLinksForOneObjectWorker entered: object {0}, restartFromZero: {1}", obj.Id.DistinguishedName, flag));
			PropertyDefinition[] array = new PropertyDefinition[]
			{
				propertyDefinition,
				ADObjectSchema.ObjectClass,
				ADObjectSchema.CorrelationIdRaw,
				ADObjectSchema.CorrelationId,
				ADRecipientSchema.UsnChanged,
				TenantRelocationSyncSchema.ObjectId,
				IADSecurityPrincipalSchema.Sid,
				IADSecurityPrincipalSchema.SamAccountName,
				TenantRelocationSyncSchema.Deleted,
				ADRecipientSchema.ConfigurationXMLRaw
			};
			IntRange intRange;
			DirectoryAttribute rangedPropertyValue = TenantRelocationSyncCoordinator.GetRangedPropertyValue(ADRecipientSchema.LinkMetadata, obj.RawLdapSearchResult, out intRange);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("ProcessAdditionalLinksForOneObjectWorker: GetRangedPropertyValue returns range [{0},{1}]", intRange.LowerBound, intRange.UpperBound));
			MultiValuedProperty<LinkMetadata> multiValuedProperty = (MultiValuedProperty<LinkMetadata>)obj[ADRecipientSchema.LinkMetadata];
			while (rangedPropertyValue != null && intRange.UpperBound != 2147483647)
			{
				IntRange nextIntRange = this.GetNextIntRange(intRange, flag);
				flag = false;
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("ProcessAdditionalLinksForOneObjectWorker: nextRange: [{0},{1}]", nextIntRange.LowerBound, nextIntRange.UpperBound));
				propertyDefinition = RangedPropertyHelper.CreateRangedProperty(ADRecipientSchema.LinkMetadata, nextIntRange);
				array[0] = propertyDefinition;
				TenantRelocationSyncObject tenantRelocationSyncObject = this.sourcePartitionSession.RetrieveTenantRelocationSyncObject(obj.Id, array);
				if (tenantRelocationSyncObject == null)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "ProcessAdditionalLinksForOneObjectWorker: fail to retrieve the source object");
					throw new InvalidOperationException("source object not found");
				}
				MultiValuedProperty<LinkMetadata> multiValuedProperty2 = (MultiValuedProperty<LinkMetadata>)tenantRelocationSyncObject[propertyDefinition];
				tenantRelocationSyncObject.propertyBag.SetField(ADRecipientSchema.LinkMetadata, multiValuedProperty2);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("ProcessAdditionalLinksForOneObjectWorker: previous value {0}, current value: {1}", (multiValuedProperty == null) ? -1 : multiValuedProperty.Count, (multiValuedProperty2 == null) ? -1 : multiValuedProperty2.Count));
				if (!this.DoLinkMetadataMVPsOverlap(multiValuedProperty, multiValuedProperty2, nextIntRange))
				{
					throw new TenantRelocationException(RelocationError.SyncFailureDueToSourceObjectBeingModified, "Ranged value retrieval from source object failed due to active dupdates");
				}
				multiValuedProperty = multiValuedProperty2;
				this.ProcessObject(tenantRelocationSyncObject);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("ProcessAdditionalLinksForOneObjectWorker: object processed: {0}", obj.Id.DistinguishedName));
				rangedPropertyValue = TenantRelocationSyncCoordinator.GetRangedPropertyValue(ADRecipientSchema.LinkMetadata, tenantRelocationSyncObject.RawLdapSearchResult, out intRange);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("ProcessAdditionalLinksForOneObjectWorker: GetRangedPropertyValue returns range [{0},{1}]", intRange.LowerBound, intRange.UpperBound));
			}
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x00166C7C File Offset: 0x00164E7C
		private void ValidateTargetObject(ADObjectId targetObjectId)
		{
			ValidationError[] array = null;
			if (targetObjectId.Equals(this.SyncData.Target.TenantConfigurationUnit))
			{
				bool useConfigNC = this.targetPartitionSession.UseConfigNC;
				this.targetPartitionSession.UseConfigNC = targetObjectId.IsDescendantOf(this.SyncData.Target.PartitionConfigNcRoot);
				try
				{
					ExchangeConfigurationUnit exchangeConfigurationUnit = this.targetPartitionSession.Read<ExchangeConfigurationUnit>(targetObjectId);
					if (exchangeConfigurationUnit == null)
					{
						throw new InvalidOperationException("Failed to read target object, unexpectedly");
					}
					array = exchangeConfigurationUnit.Validate();
					goto IL_BD;
				}
				finally
				{
					this.targetPartitionSession.UseConfigNC = useConfigNC;
				}
			}
			if (targetObjectId == this.SyncData.Target.TenantOrganizationUnit || !targetObjectId.IsDescendantOf(this.SyncData.Target.TenantOrganizationUnit))
			{
				return;
			}
			TransportMiniRecipient transportMiniRecipient = this.targetRecipientSession.ReadMiniRecipient<TransportMiniRecipient>(targetObjectId, null);
			if (transportMiniRecipient == null)
			{
				return;
			}
			array = transportMiniRecipient.Validate();
			IL_BD:
			if (array.Length == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ValidationError validationError in array)
			{
				string name = validationError.GetType().Name;
				PropertyValidationError propertyValidationError = validationError as PropertyValidationError;
				if (propertyValidationError != null)
				{
					stringBuilder.AppendFormat("\t{0}: property {1} contains invalid data '{2}'\r\n", name, propertyValidationError.PropertyName, propertyValidationError.InvalidData.ToString());
				}
				else
				{
					stringBuilder.AppendFormat("\t{0}: object is invalid\r\n", name);
				}
			}
			throw new TenantRelocationException(RelocationError.ADDriverValidationFailed, string.Format("AD Driver Validation failed for {0}:\r\n{1}", targetObjectId.DistinguishedName, stringBuilder.ToString()));
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x00166DF0 File Offset: 0x00164FF0
		private IntRange GetNextIntRange(IntRange range, bool restartFromZero)
		{
			if (restartFromZero)
			{
				return new IntRange(0, TenantRelocationSyncCoordinator.LinksPerPageLimit);
			}
			int num = range.UpperBound + 1 - TenantRelocationSyncCoordinator.LinksOverldapSize;
			int upperBound = num + TenantRelocationSyncCoordinator.LinksPerPageLimit - 1;
			return new IntRange(num, upperBound);
		}

		// Token: 0x060065E8 RID: 26088 RVA: 0x00166E2C File Offset: 0x0016502C
		private bool DoLinkMetadataMVPsOverlap(MultiValuedProperty<LinkMetadata> previousMetadata, MultiValuedProperty<LinkMetadata> currentMetadata, IntRange currentRange)
		{
			if (currentRange.LowerBound == 0)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "DoLinkMetadataMVPsOverlap: nextRange starts from 0, return true");
				return true;
			}
			for (int i = 0; i < Math.Min(TenantRelocationSyncCoordinator.LinksOverldapSize, previousMetadata.Count); i++)
			{
				LinkMetadata value = previousMetadata[i];
				for (int j = Math.Max(currentMetadata.Count - TenantRelocationSyncCoordinator.LinksOverldapSize, 0); j < currentMetadata.Count; j++)
				{
					LinkMetadata linkMetadata = currentMetadata[j];
					if (this.AreMetadataForSameLinkValue(value, linkMetadata))
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("DoLinkMetadataMVPsOverlap: overldap found, return true:{0}, iPrevious={1}, jCurrent={2}", linkMetadata.TargetDistinguishedName, i, j));
						return true;
					}
				}
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "DoLinkMetadataMVPsOverlap: overldap not found, return false");
			return false;
		}

		// Token: 0x060065E9 RID: 26089 RVA: 0x00166EFC File Offset: 0x001650FC
		private bool AreMetadataForSameLinkValue(LinkMetadata value1, LinkMetadata value2)
		{
			bool flag = string.Equals(value1.AttributeName, value2.AttributeName);
			bool flag2 = string.Equals(value1.TargetDistinguishedName, value2.TargetDistinguishedName);
			bool flag3 = value1.Data == value2.Data || (value1.Data != null && value2.Data != null && value1.Data.SequenceEqual(value2.Data));
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), string.Format("AreMetadataForSameLinkValue: isSameAttribute:{0}, isSameTarget:{1}, isSameData:{2}", flag, flag2, flag3));
			return flag && flag2 && flag3;
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x00166F9C File Offset: 0x0016519C
		private void ProcessCookie(bool initialSync)
		{
			this.cookie = this.RetrieveSyncCookie();
			if (this.cookie == null)
			{
				if (!initialSync)
				{
					throw new InvalidOperationException("sync cookie is null");
				}
				this.cookie = new TenantRelocationSyncPageToken(Guid.Empty, this.SyncData.Source.TenantOrganizationUnit, this.SyncData.Source.TenantConfigurationUnit, this.SyncData.SourceTenantPartitionHint, false);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "PerformSync: A new cookie is initialized");
			}
			else
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ProcessCookie: cookie found, source DC:{0}, target DC:{1}.", this.cookie.AffinityDcFqdn ?? "null", this.cookie.AffinityTargetDcFqdn ?? "null");
			}
			if (!string.IsNullOrEmpty(this.cookie.AffinityTargetDcFqdn))
			{
				try
				{
					this.SetTargetAffinityDC(this.cookie.AffinityTargetDcFqdn);
					this.cookie = this.RetrieveSyncCookie();
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "ProcessCookie: cookie found on DC {0}, source DC:{1}, target DC:{2}.", this.sourceRootOrgConfigurationSession.DomainController, this.cookie.AffinityDcFqdn ?? "null", this.cookie.AffinityTargetDcFqdn ?? "null");
				}
				catch (Exception ex)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessCookie: Exception:{0}", ex.ToString());
					this.cookie.SetErrorState(ex);
					this.PersistSyncCookie(this.cookie);
					throw;
				}
			}
			if (!this.cookie.MoreData)
			{
				this.cookie.Reset();
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "ProcessCookie: the cookie has been reset to start a new sync cycle.");
			}
		}

		// Token: 0x060065EB RID: 26091 RVA: 0x00167150 File Offset: 0x00165350
		private TenantRelocationSyncPageToken RetrieveSyncCookie()
		{
			TenantRelocationRequest tenantRelocationRequest = this.ReadTenantRelocationRequest();
			if (tenantRelocationRequest == null || tenantRelocationRequest.TenantSyncCookie == null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "RetrieveSyncCookie: cannot find cookie on {0}", this.SyncData.Target.TenantConfigurationUnit.DistinguishedName);
				return null;
			}
			return new TenantRelocationSyncPageToken(tenantRelocationRequest.TenantSyncCookie);
		}

		// Token: 0x060065EC RID: 26092 RVA: 0x001671A8 File Offset: 0x001653A8
		private void PersistSyncCookie(TenantRelocationSyncPageToken cookie)
		{
			bool flag = false;
			string domainController = this.targetPartitionSession.DomainController;
			do
			{
				try
				{
					TenantRelocationRequest tenantRelocationRequest = this.ReadTenantRelocationRequest();
					tenantRelocationRequest.TenantSyncCookie = cookie.ToByteArray();
					this.SaveTenantRelocationRequest(tenantRelocationRequest);
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "PersistSyncCookie: cookie saved on {0}", this.SyncData.Target.TenantConfigurationUnit.DistinguishedName);
					flag = false;
				}
				catch (Exception ex)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "PersistSyncCookie: DC:{0}; Exception: {1}", this.targetPartitionSession.DomainController, ex.ToString());
					if (string.IsNullOrEmpty(this.targetPartitionSession.DomainController))
					{
						TenantRelocationSyncLogger.Instance.Log(this.SyncData, "cookie save failure", ex.GetType().ToString(), ex.Message, cookie.ToByteArray());
						throw;
					}
					this.targetPartitionSession.DomainController = null;
					flag = true;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<bool>((long)this.GetHashCode(), "PersistSyncCookie: retry: {0}", flag);
				}
			}
			while (flag);
			this.targetPartitionSession.DomainController = domainController;
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x001672C4 File Offset: 0x001654C4
		private TenantRelocationRequest ReadTenantRelocationRequest()
		{
			bool useConfigNC = this.targetPartitionSession.UseConfigNC;
			this.targetPartitionSession.UseConfigNC = this.SyncData.Target.IsConfigurationUnitUnderConfigNC;
			TenantRelocationRequest result;
			try
			{
				result = this.targetPartitionSession.Read<TenantRelocationRequest>(this.SyncData.Target.TenantConfigurationUnit);
			}
			finally
			{
				this.targetPartitionSession.UseConfigNC = useConfigNC;
			}
			return result;
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x00167334 File Offset: 0x00165534
		private void SaveTenantRelocationRequest(TenantRelocationRequest request)
		{
			bool useConfigNC = this.targetPartitionSession.UseConfigNC;
			this.targetPartitionSession.UseConfigNC = this.SyncData.Target.IsConfigurationUnitUnderConfigNC;
			try
			{
				this.targetPartitionSession.Save(request);
			}
			finally
			{
				this.targetPartitionSession.UseConfigNC = useConfigNC;
			}
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x00167394 File Offset: 0x00165594
		internal void ResetSyncCookie()
		{
			TenantRelocationRequest tenantRelocationRequest = this.ReadTenantRelocationRequest();
			tenantRelocationRequest.TenantSyncCookie = null;
			this.SaveTenantRelocationRequest(tenantRelocationRequest);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ResetSyncCookie: cookie reset on {0}", this.SyncData.Target.TenantConfigurationUnit.DistinguishedName);
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x001673E4 File Offset: 0x001655E4
		private void PersistCompletionTargetVector(WatermarkMap watermark)
		{
			bool flag = false;
			string domainController = this.targetPartitionSession.DomainController;
			do
			{
				try
				{
					TenantRelocationRequest tenantRelocationRequest = this.ReadTenantRelocationRequest();
					tenantRelocationRequest.TenantRelocationCompletionTargetVector = watermark.SerializeToBytes();
					this.SaveTenantRelocationRequest(tenantRelocationRequest);
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "PersistCompletionTargetVector: vector saved on {0}", this.SyncData.Target.TenantConfigurationUnit.DistinguishedName);
					flag = false;
				}
				catch (Exception ex)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "PersistCompletionTargetVector: DC:{0}; Exception: {1}", this.targetPartitionSession.DomainController, ex.ToString());
					if (string.IsNullOrEmpty(this.targetPartitionSession.DomainController))
					{
						throw;
					}
					this.targetPartitionSession.DomainController = null;
					flag = true;
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<bool>((long)this.GetHashCode(), "PersistSyncCookie: retry: {0}", flag);
				}
			}
			while (flag);
			this.targetPartitionSession.DomainController = domainController;
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x001674D0 File Offset: 0x001656D0
		private void CnfMangledNameCheck(TenantRelocationSyncObject obj)
		{
			bool flag = obj.Id.Rdn.UnescapedName.Contains("\nCNF:");
			if (ExEnvironment.IsTest && !flag)
			{
				flag = obj.Id.Rdn.UnescapedName.Contains("_CNF_");
			}
			if (flag)
			{
				string text = string.Format("An object with CNF mangled name is encountered - need to manually clean up these objects, object DN:{0}", obj.Id.DistinguishedName);
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, text, "0", "Exception", null);
				throw new TenantRelocationException(RelocationError.ObjectsWithCnfNameFoundInSource, text);
			}
		}

		// Token: 0x060065F2 RID: 26098 RVA: 0x00167560 File Offset: 0x00165760
		private void DeleteObjectWithLDAP(ADObjectId targetDn)
		{
			DeleteRequest deleteRequest = new DeleteRequest();
			deleteRequest.DistinguishedName = targetDn.DistinguishedName;
			deleteRequest.Controls.Add(new TreeDeleteControl());
			this.throttlingManager.Throttle();
			this.targetPartitionSession.UnsafeExecuteModificationRequest(deleteRequest, targetDn);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "DeleteObjectWithLDAP: Executed DelRequest for target object: {0}", deleteRequest.DistinguishedName);
			this.perfLogger.IncrementDeleteCount();
		}

		// Token: 0x060065F3 RID: 26099 RVA: 0x001675D0 File Offset: 0x001657D0
		internal void ProcessObject(TenantRelocationSyncObject obj)
		{
			ADRawEntry adrawEntry;
			bool flag = this.FindTargetObjectByCorrelationId(obj.Guid, out adrawEntry);
			if (obj.IsDeleted)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "ProcessObject: found a deleted object:{0} in source forest", obj.Id.DistinguishedName);
				ADObjectId id;
				if (flag)
				{
					if ((bool)adrawEntry[TenantRelocationSyncSchema.Deleted])
					{
						ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "ProcessObject: to skip deleted object:{0}, target exists: {1}", obj.Id.DistinguishedName, flag);
						return;
					}
					id = adrawEntry.Id;
				}
				else if (!this.CreateDummyObjectIfNecessary(obj, out id))
				{
					return;
				}
				this.DeleteObjectWithLDAP(id);
				return;
			}
			if (this.SyncData.Source.IsUnderTenantScope(obj.Id) && !flag)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "ProcessObject: to create object for source object {0}, target exists:{1}", obj.Id.DistinguishedName, flag);
				this.CreateObject(obj);
				return;
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "ProcessObject: to update object for source object {0}, target exists: {1}", obj.Id.DistinguishedName, flag);
			this.UpdateObject(obj, RequestType.ModifyIncremental);
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x001676DC File Offset: 0x001658DC
		private bool CreateDummyObjectIfNecessary(TenantRelocationSyncObject obj, out ADObjectId id)
		{
			DirectoryAttribute directoryAttribute = new DirectoryAttribute();
			directoryAttribute.Name = ADObjectSchema.ObjectClass.LdapDisplayName;
			if (((MultiValuedProperty<string>)obj[ADObjectSchema.ObjectClass]).Contains("inetOrgPerson"))
			{
				directoryAttribute.Add("inetOrgPerson");
			}
			else if (((MultiValuedProperty<string>)obj[ADObjectSchema.ObjectClass]).Contains("user"))
			{
				directoryAttribute.Add("user");
			}
			else if (((MultiValuedProperty<string>)obj[ADObjectSchema.ObjectClass]).Contains("group"))
			{
				directoryAttribute.Add("group");
			}
			else
			{
				if (!((MultiValuedProperty<string>)obj[ADObjectSchema.ObjectClass]).Contains("contact"))
				{
					id = null;
					return false;
				}
				directoryAttribute.Add("contact");
			}
			AddRequest addRequest = new AddRequest();
			id = this.SyncData.Target.TenantOrganizationUnit.GetChildId(obj.CorrelationId.ToString());
			addRequest.DistinguishedName = id.DistinguishedName;
			addRequest.Attributes.Add(directoryAttribute);
			DirectoryAttribute directoryAttribute2 = new DirectoryAttribute();
			directoryAttribute2.Name = ADObjectSchema.ConfigurationUnit.LdapDisplayName;
			directoryAttribute2.Add(this.SyncData.Target.TenantConfigurationUnit.DistinguishedName);
			addRequest.Attributes.Add(directoryAttribute2);
			DirectoryAttribute directoryAttribute3 = new DirectoryAttribute();
			directoryAttribute3.Name = ADObjectSchema.OrganizationalUnitRoot.LdapDisplayName;
			directoryAttribute3.Add(this.SyncData.Target.TenantOrganizationUnit.DistinguishedName);
			addRequest.Attributes.Add(directoryAttribute3);
			if (!string.IsNullOrEmpty(obj.ExternalDirectoryObjectId))
			{
				DirectoryAttribute directoryAttribute4 = new DirectoryAttribute();
				directoryAttribute4.Name = ADRecipientSchema.ExternalDirectoryObjectId.LdapDisplayName;
				directoryAttribute4.Add(obj.ExternalDirectoryObjectId);
				addRequest.Attributes.Add(directoryAttribute4);
			}
			DirectoryAttribute directoryAttribute5 = new DirectoryAttribute();
			directoryAttribute5.Name = ADObjectSchema.CorrelationIdRaw.LdapDisplayName;
			directoryAttribute5.Add(obj.CorrelationId.ToByteArray());
			addRequest.Attributes.Add(directoryAttribute5);
			this.throttlingManager.Throttle();
			this.targetPartitionSession.UnsafeExecuteModificationRequest(addRequest, id);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "CreateDummyObjectIfNecessary: Executed AddRequest for target object: {0}", addRequest.DistinguishedName);
			this.perfLogger.IncrementPlaceHolderCount();
			return true;
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x0016793C File Offset: 0x00165B3C
		private string PickDCForTenant(string tenantName, PartitionId partitionId)
		{
			ADRunspaceServerSettingsProvider instance = ADRunspaceServerSettingsProvider.GetInstance();
			bool flag;
			string fqdn = instance.GetGcFromToken(partitionId.ForestFQDN, tenantName.ToLowerInvariant(), out flag, false).Fqdn;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "PickDCForTenant: found DC: {0}", fqdn);
			return fqdn;
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x00167984 File Offset: 0x00165B84
		private void ProcessDCAffinity()
		{
			if (this.cookie == null)
			{
				throw new ArgumentException("this.cookie cannot be null");
			}
			try
			{
				if (string.IsNullOrEmpty(this.cookie.AffinityTargetDcFqdn))
				{
					string text = this.PickDCForTenant(this.SyncData.Target.TenantOrganizationUnit.Rdn.UnescapedName, this.SyncData.Target.PartitionId);
					this.cookie.AffinityTargetDcFqdn = text;
					TenantRelocationSyncLogger.Instance.Log(this.SyncData, "DC selection", null, string.Format("New target DC is selected: {0}", text), null);
				}
				this.SetTargetAffinityDC(this.cookie.AffinityTargetDcFqdn);
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, "DC selection", null, string.Format("target DC is set to {0}", this.cookie.AffinityTargetDcFqdn), null);
				if (string.IsNullOrEmpty(this.cookie.AffinityDcFqdn))
				{
					string text2 = this.PickDCForTenant(this.SyncData.Source.TenantOrganizationUnit.Rdn.UnescapedName, this.SyncData.Source.PartitionId);
					this.cookie.SetInvocationId(this.sourceRootOrgConfigurationSession.GetInvocationIdByFqdn(text2), text2);
					TenantRelocationSyncLogger.Instance.Log(this.SyncData, "DC selection", null, string.Format("New source DC is selected: {0}", text2), null);
				}
				this.SetSourceAffinityDC(this.cookie.AffinityDcFqdn);
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, "DC selection", null, string.Format("source DC is set to {0}", this.cookie.AffinityDcFqdn), null);
			}
			catch (Exception ex)
			{
				TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Exception", null, string.Format("Exception when setting affinity DC: {0}", ex.ToString()), null);
				this.PersistSyncCookie(this.cookie);
				throw;
			}
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x00167B60 File Offset: 0x00165D60
		private void SetSourceAffinityDC(string dcFqdn)
		{
			try
			{
				string text;
				SuitabilityVerifier.CheckIsServerSuitable(dcFqdn, false, null, out text);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "SetSourceAffinityDC: validate DC is suitable: {0}", dcFqdn);
				this.sourceRootOrgConfigurationSession.DomainController = dcFqdn;
				this.sourceSystemConfigurationSession.DomainController = dcFqdn;
				this.sourcePartitionSession.DomainController = dcFqdn;
				this.sourceRecipientSession.DomainController = dcFqdn;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "SetSourceAffinityDC: Exception: {0}", ex.ToString());
				if (this.cookie != null)
				{
					this.cookie.SetErrorState(ex);
				}
				throw;
			}
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x00167C04 File Offset: 0x00165E04
		private void SetTargetAffinityDC(string dcFqdn)
		{
			try
			{
				string text;
				SuitabilityVerifier.CheckIsServerSuitable(dcFqdn, false, null, out text);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "SetTargetAffinityDC: validate DC is suitable: {0}", dcFqdn);
				this.targetPartitionSession.DomainController = dcFqdn;
				this.targetRecipientSession.DomainController = dcFqdn;
			}
			catch (Exception ex)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "SetTargetAffinityDC: Exception: {0}", ex.ToString());
				if (this.cookie != null)
				{
					this.cookie.SetErrorState(ex);
				}
				throw;
			}
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x00167C90 File Offset: 0x00165E90
		private void PrepareSessions()
		{
			string domainController = this.PickDCForTenant(this.SyncData.Source.TenantOrganizationUnit.Rdn.UnescapedName, this.SyncData.Source.PartitionId);
			this.sourceRootOrgConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.SyncData.Source.PartitionId), 2553, "PrepareSessions", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			ADSessionSettings adsessionSettings = ADSessionSettings.FromTenantPartitionHint(this.SyncData.SourceTenantPartitionHint);
			adsessionSettings.IncludeSoftDeletedObjects = true;
			adsessionSettings.IncludeSoftDeletedObjectLinks = true;
			this.sourceSystemConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, adsessionSettings, 2567, "PrepareSessions", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			this.sourceSystemConfigurationSession.LogSizeLimitExceededEvent = false;
			ADSessionSettings adsessionSettings2 = ADSessionSettings.FromAccountPartitionWideScopeSet(new PartitionId(this.SyncData.Source.PartitionRoot));
			adsessionSettings2.IncludeSoftDeletedObjectLinks = true;
			adsessionSettings2.IncludeSoftDeletedObjects = true;
			this.sourcePartitionSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, adsessionSettings2, 2579, "PrepareSessions", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			this.sourcePartitionSession.UseConfigNC = false;
			this.sourceRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.PartiallyConsistent, adsessionSettings, 2590, "PrepareSessions", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			this.sourceRecipientSession.UseGlobalCatalog = false;
			this.sourceRecipientSession.DomainController = domainController;
			this.sourceRecipientSession.LogSizeLimitExceededEvent = false;
			string domainController2 = this.PickDCForTenant(this.SyncData.Target.TenantOrganizationUnit.Rdn.UnescapedName, this.SyncData.Target.PartitionId);
			ADSessionSettings adsessionSettings3 = ADSessionSettings.FromAccountPartitionWideScopeSet(this.SyncData.Target.PartitionId);
			adsessionSettings3.IncludeSoftDeletedObjects = true;
			adsessionSettings3.IncludeSoftDeletedObjectLinks = true;
			adsessionSettings3.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			this.targetPartitionSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController2, false, ConsistencyMode.PartiallyConsistent, null, adsessionSettings3, 2608, "PrepareSessions", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			this.targetPartitionSession.LogSizeLimitExceededEvent = false;
			this.targetPartitionSession.UseConfigNC = false;
			this.targetRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(domainController2, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAllTenantsPartitionId(this.SyncData.Target.PartitionId), 2618, "PrepareSessions", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			this.targetRecipientSession.UseGlobalCatalog = false;
			this.targetRecipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
			this.targetRecipientSession.SessionSettings.IncludeSoftDeletedObjectLinks = true;
			this.targetRecipientSession.SessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x00167F14 File Offset: 0x00166114
		internal void CaptureCompletionTargetVector()
		{
			WatermarkMap replicationWatermarkMapFromAllDCs = this.GetReplicationWatermarkMapFromAllDCs(this.targetPartitionSession.DomainController);
			this.PersistCompletionTargetVector(replicationWatermarkMapFromAllDCs);
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x00167F3C File Offset: 0x0016613C
		private bool CheckReplicationStatus(ITopologyConfigurationSession session, WatermarkMap finishWaterMark, bool useConfigNC)
		{
			WatermarkMap replicationCursors = SyncConfiguration.GetReplicationCursors(session, useConfigNC, false);
			return replicationCursors.ContainsAllChanges(finishWaterMark);
		}

		// Token: 0x060065FC RID: 26108 RVA: 0x00167F60 File Offset: 0x00166160
		internal bool PreLockdownReplicationCheck()
		{
			this.PrepareSessions();
			this.ProcessCookie(true);
			this.ProcessDCAffinity();
			return this.ReplicationLatencyCheck(this.SyncData.Source.PartitionId, this.sourceRootOrgConfigurationSession.DomainController, false) && this.ReplicationLatencyCheck(this.SyncData.Target.PartitionId, this.targetPartitionSession.DomainController, false) && (!this.SyncData.Source.IsConfigurationUnitUnderConfigNC || this.ReplicationLatencyCheck(this.SyncData.Source.PartitionId, this.sourceRootOrgConfigurationSession.DomainController, true));
		}

		// Token: 0x060065FD RID: 26109 RVA: 0x00168004 File Offset: 0x00166204
		private bool ReplicationLatencyCheck(PartitionId partitionId, string domainController, bool useConfigNC)
		{
			TimeSpan timeSpan = TimeSpan.FromMinutes((double)TenantRelocationConfigImpl.GetConfig<int>("MaxAllowedReplicationLatencyInMinutes"));
			Guid guid;
			MultiValuedProperty<ReplicationCursor> multiValuedProperty = this.ReadUtdVector(partitionId, domainController, useConfigNC, out guid);
			ReadOnlyCollection<ADServer> readOnlyCollection = ADForest.GetForest(partitionId).FindRootDomain().FindAllDomainControllers();
			int num = 0;
			foreach (ADServer adserver in readOnlyCollection)
			{
				string text;
				LocalizedString localizedString;
				if (!SuitabilityVerifier.IsServerSuitableIgnoreExceptions(adserver.DnsHostName, false, null, out text, out localizedString))
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ReplicationLatencyCheck: DC in MM:{0}, skipped, message: {1}", adserver.DnsHostName, localizedString.ToString());
				}
				else
				{
					Guid guid2;
					MultiValuedProperty<ReplicationCursor> multiValuedProperty2 = this.ReadUtdVector(partitionId, adserver.DnsHostName, useConfigNC, out guid2);
					if (!this.CompareCursors(multiValuedProperty, guid, multiValuedProperty2, guid2, timeSpan) || !this.CompareCursors(multiValuedProperty2, guid2, multiValuedProperty, guid, timeSpan))
					{
						TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Failure", "0", string.Format("Replication between {0} to {1} is slow, check fail", domainController, adserver.DnsHostName), null);
						return false;
					}
					num++;
				}
			}
			TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Info", "0", string.Format("Passed replication latency check on DC: {0}, number of DCs checked:{1}, maxLatency:{2}", domainController, num, timeSpan), null);
			return true;
		}

		// Token: 0x060065FE RID: 26110 RVA: 0x00168164 File Offset: 0x00166364
		private bool CompareCursors(MultiValuedProperty<ReplicationCursor> source, Guid sourceInvocationId, MultiValuedProperty<ReplicationCursor> target, Guid targetInvocationId, TimeSpan maxLatency)
		{
			ReplicationCursor replicationCursor = null;
			ReplicationCursor replicationCursor2 = null;
			foreach (ReplicationCursor replicationCursor3 in source)
			{
				if (replicationCursor3.SourceInvocationId == targetInvocationId)
				{
					replicationCursor = replicationCursor3;
					break;
				}
			}
			foreach (ReplicationCursor replicationCursor4 in target)
			{
				if (replicationCursor4.SourceInvocationId == targetInvocationId)
				{
					replicationCursor2 = replicationCursor4;
					break;
				}
			}
			if (replicationCursor != null && replicationCursor2 != null)
			{
				if (DateTime.UtcNow - replicationCursor.LastSuccessfulSyncTime < maxLatency)
				{
					return true;
				}
				if (replicationCursor2.UpToDatenessUsn < replicationCursor.UpToDatenessUsn + 1000L)
				{
					return true;
				}
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "CompareCursors: failed replication check, source Id: {0}, target Id:{1}, from source: LastSuccessfulSyncTime:{2}, usn:{3}; from target: usn:{4}", new object[]
			{
				sourceInvocationId,
				targetInvocationId,
				(replicationCursor == null) ? "<null>" : replicationCursor.LastSuccessfulSyncTime.ToString(),
				(replicationCursor == null) ? "<null>" : replicationCursor.UpToDatenessUsn.ToString(),
				(replicationCursor2 == null) ? "<null>" : replicationCursor2.UpToDatenessUsn.ToString()
			});
			return false;
		}

		// Token: 0x060065FF RID: 26111 RVA: 0x001682D8 File Offset: 0x001664D8
		internal bool WaitForLastChangeToReplicate(string domainController, TimeSpan timeout)
		{
			this.PrepareSessions();
			ADSchemaDataProvider.Instance.LoadAllSchemaAttributeObjects();
			this.ProcessCookie(false);
			this.ProcessDCAffinity();
			WatermarkMap watermarkMap;
			return this.WaitForReplicationDiffusion(this.SyncData.Source.PartitionId, domainController, timeout, out watermarkMap, this.SyncData.Source.IsConfigurationUnitUnderConfigNC);
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x0016832C File Offset: 0x0016652C
		internal bool WaitForTheLastChangeInSource(TimeSpan timeout)
		{
			this.PrepareSessions();
			ADSchemaDataProvider.Instance.LoadAllSchemaAttributeObjects();
			this.ProcessCookie(false);
			this.ProcessDCAffinity();
			return this.WaitForReplicationConvergence(this.sourceRootOrgConfigurationSession.DomainController, timeout);
		}

		// Token: 0x06006601 RID: 26113 RVA: 0x00168360 File Offset: 0x00166560
		internal bool WaitForReplicationConvergence(string domainController, TimeSpan timeout)
		{
			WatermarkMap watermarkMap = new WatermarkMap();
			WatermarkMap watermarkMap2 = new WatermarkMap();
			ReadOnlyCollection<ADServer> readOnlyCollection = ADForest.GetForest(this.SyncData.Source.PartitionId).FindRootDomain().FindAllDomainControllers();
			DateTime utcNow = DateTime.UtcNow;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "WaitForReplicationConvergence: start at {0}", utcNow);
			foreach (ADServer adserver in readOnlyCollection)
			{
				string text;
				LocalizedString localizedString;
				if (!SuitabilityVerifier.IsServerSuitableIgnoreExceptions(adserver.DnsHostName, false, null, out text, out localizedString))
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "WaitForReplicationConvergence: DC in MM:{0}, skipped, message: {1}", adserver.DnsHostName, localizedString.ToString());
				}
				else
				{
					Guid key;
					WatermarkMap watermarkMap3;
					long value = this.ReadDcHighestUSN(this.SyncData.Source.PartitionId, adserver.DnsHostName, false, out key, out watermarkMap3);
					watermarkMap[key] = value;
					if (this.SyncData.Source.IsConfigurationUnitUnderConfigNC)
					{
						value = this.ReadDcHighestUSN(this.SyncData.Source.PartitionId, adserver.DnsHostName, true, out key, out watermarkMap3);
						watermarkMap2[key] = value;
					}
				}
			}
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.SyncData.Source.PartitionId), 2914, "WaitForReplicationConvergence", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			bool flag = false;
			bool flag2 = !this.SyncData.Source.IsConfigurationUnitUnderConfigNC;
			while (!flag || !flag2)
			{
				if (!flag)
				{
					flag = this.CheckReplicationStatus(session, watermarkMap, false);
				}
				if (!flag2)
				{
					flag2 = this.CheckReplicationStatus(session, watermarkMap2, true);
				}
				if ((flag && flag2) || utcNow + timeout < DateTime.UtcNow || TenantRelocationSyncCoordinator.WaitForBreakEventsOrTimeout(5, this.breakEvents))
				{
					break;
				}
			}
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<bool, bool, DateTime>((long)this.GetHashCode(), "WaitForReplicationConvergence exits. domain finished:{0}, config finished:{1}, end time:{2}", flag, flag2, DateTime.UtcNow);
			return flag && flag2;
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x0016856C File Offset: 0x0016676C
		private MultiValuedProperty<ReplicationCursor> ReadUtdVector(PartitionId partitionId, string domainController, bool useConfigNC, out Guid invocationId)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 2974, "ReadUtdVector", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			invocationId = topologyConfigurationSession.GetInvocationIdByFqdn(domainController);
			topologyConfigurationSession.UseConfigNC = useConfigNC;
			return topologyConfigurationSession.ReadReplicationCursors(useConfigNC ? topologyConfigurationSession.GetConfigurationNamingContext() : topologyConfigurationSession.GetDomainNamingContext());
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x001685CC File Offset: 0x001667CC
		private long ReadDcHighestUSN(PartitionId partitionId, string domainController, bool useConfigNC, out Guid invocationId, out WatermarkMap watermarks)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(domainController, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 2993, "ReadDcHighestUSN", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\TenantRelocationSync\\TenantRelocationSyncCoordinator.cs");
			invocationId = topologyConfigurationSession.GetInvocationIdByFqdn(domainController);
			topologyConfigurationSession.UseConfigNC = useConfigNC;
			watermarks = SyncConfiguration.GetReplicationCursors(topologyConfigurationSession, useConfigNC, false);
			return watermarks[invocationId];
		}

		// Token: 0x06006604 RID: 26116 RVA: 0x0016862E File Offset: 0x0016682E
		internal static bool WaitForBreakEventsOrTimeout(int seconds, WaitHandle[] breakEvents)
		{
			if (breakEvents != null)
			{
				if (WaitHandle.WaitAny(breakEvents, seconds * 1000) != 258)
				{
					return true;
				}
			}
			else if (seconds > 0)
			{
				Thread.Sleep(seconds * 1000);
			}
			return false;
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x0016865C File Offset: 0x0016685C
		internal WatermarkMap GetReplicationWatermarkMapFromAllDCs(string domainController)
		{
			WatermarkMap result;
			this.WaitForReplicationDiffusion(this.SyncData.Target.PartitionId, domainController, new TimeSpan(0, 5, 0), out result, this.SyncData.Target.IsConfigurationUnitUnderConfigNC);
			return result;
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x0016869C File Offset: 0x0016689C
		internal bool WaitForReplicationDiffusion(PartitionId partitionId, string domainController, TimeSpan timeout, out WatermarkMap watermarks, bool useConfigNC)
		{
			Guid guid;
			long num = this.ReadDcHighestUSN(partitionId, domainController, useConfigNC, out guid, out watermarks);
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, Guid, long>((long)this.GetHashCode(), "WaitForReplicationDiffusion: Local DC:{0}, invocationId:{1}, usn={2}", partitionId.ForestFQDN, guid, num);
			ReadOnlyCollection<ADServer> readOnlyCollection = ADForest.GetForest(partitionId).FindRootDomain().FindAllDomainControllers();
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)this.GetHashCode(), "WaitForReplicationDiffusion: Number of DCs found:{0}", readOnlyCollection.Count);
			int num2 = 0;
			int num3 = 0;
			DateTime utcNow = DateTime.UtcNow;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "WaitForReplicationDiffusion: start at {0}", utcNow);
			List<ADServer> list = new List<ADServer>();
			Dictionary<ADServer, int> dictionary = new Dictionary<ADServer, int>();
			bool flag = false;
			while (!flag)
			{
				foreach (ADServer adserver in readOnlyCollection)
				{
					if (TenantRelocationSyncCoordinator.WaitForBreakEventsOrTimeout(0, this.breakEvents))
					{
						break;
					}
					if (!list.Contains(adserver))
					{
						string text;
						LocalizedString localizedString;
						if (adserver.DnsHostName.Equals(domainController, StringComparison.OrdinalIgnoreCase))
						{
							list.Add(adserver);
							num2++;
						}
						else if (!SuitabilityVerifier.IsServerSuitableIgnoreExceptions(adserver.DnsHostName, false, null, out text, out localizedString))
						{
							ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "WaitForReplicationDiffusion: DC in MM:{0}, skipped", adserver.DnsHostName);
							list.Add(adserver);
							num3++;
						}
						else
						{
							Guid guid2;
							WatermarkMap watermarkMap;
							try
							{
								this.ReadDcHighestUSN(partitionId, adserver.DnsHostName, useConfigNC, out guid2, out watermarkMap);
							}
							catch (Exception ex)
							{
								ExTraceGlobals.TenantRelocationTracer.TraceDebug<string>((long)this.GetHashCode(), "WaitForReplicationDiffusion: AD exception:{0}", ex.ToString());
								if (ex is DataSourceTransientException)
								{
									if (dictionary.ContainsKey(adserver))
									{
										Dictionary<ADServer, int> dictionary2;
										ADServer key;
										(dictionary2 = dictionary)[key = adserver] = dictionary2[key] + 1;
										if (dictionary[adserver] >= 3)
										{
											list.Add(adserver);
											num3++;
											ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, int>((long)this.GetHashCode(), "WaitForReplicationDiffusion: server: {0}, #failures:{1}, skipped", adserver.DnsHostName, dictionary[adserver]);
										}
									}
									else
									{
										dictionary[adserver] = 1;
									}
									continue;
								}
								throw;
							}
							ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "WaitForReplicationDiffusion: DC name:{0}; Invocation Id:{1}; target USNs:{2}, {3}; local USNs: {4}, {5}", new object[]
							{
								adserver.DnsHostName,
								guid2,
								watermarkMap[guid],
								watermarkMap[guid2],
								num,
								watermarks[guid2]
							});
							watermarks[guid2] = watermarkMap[guid2];
							if (watermarkMap[guid] >= num)
							{
								list.Add(adserver);
								num2++;
								ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "WaitForReplicationDiffusion: DC name:{0}; Invocation Id:{1} completed}", adserver.DnsHostName, guid2);
							}
						}
					}
				}
				flag = (utcNow + timeout < DateTime.UtcNow || list.Count >= readOnlyCollection.Count);
				ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "WaitForReplicationDiffusion: One round is done, isFinished:{0}, #completed:{1}, #success:{2}, #failed:{3}, #total:{4}, time={5}", new object[]
				{
					flag,
					list.Count,
					num2,
					num3,
					readOnlyCollection.Count,
					DateTime.UtcNow
				});
				if (!flag)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "WaitForReplicationDiffusion: Sleep for 15 seconds");
					if (TenantRelocationSyncCoordinator.WaitForBreakEventsOrTimeout(15, this.breakEvents))
					{
						break;
					}
				}
			}
			return list.Count >= readOnlyCollection.Count;
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x00168A80 File Offset: 0x00166C80
		private void AddSidHistory(TenantRelocationSyncObject sourceObject, ADRawEntry targetObject)
		{
			string forestFQDN = this.SyncData.Source.PartitionId.ForestFQDN;
			string forestFQDN2 = this.SyncData.Target.PartitionId.ForestFQDN;
			string text = (string)sourceObject[IADSecurityPrincipalSchema.SamAccountName];
			string destinationPricipalName = (string)targetObject[IADSecurityPrincipalSchema.SamAccountName];
			string lastUsedDc = this.targetPartitionSession.LastUsedDc;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					ADRawEntry[] array = this.sourcePartitionSession.Find(this.SyncData.Source.PartitionRoot, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, IADSecurityPrincipalSchema.SamAccountName, text), null, 2, TenantRelocationSyncCoordinator.identityProperties);
					if (array.Length != 1)
					{
						TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Info", null, string.Format("Duplicate samAccountName found in source partition: {0}.", text), null);
						throw new TenantRelocationException(RelocationError.DuplicateSamAccountNameInSource, string.Format("Duplicate samAccountName found in source partition: {0}", text));
					}
					this.AddSidHistory(forestFQDN, text, lastUsedDc, forestFQDN2, destinationPricipalName);
					break;
				}
				catch (ADExternalException ex)
				{
					if (ex.InnerException != null && ex.InnerException is Win32Exception)
					{
						int nativeErrorCode = ((Win32Exception)ex.InnerException).NativeErrorCode;
						if (nativeErrorCode == 1727 || nativeErrorCode == 8537 || 1332 == nativeErrorCode || 1354 == nativeErrorCode || 5 == nativeErrorCode)
						{
							string text2 = null;
							int num = nativeErrorCode;
							if (num <= 1332)
							{
								if (num != 5)
								{
									if (num == 1332)
									{
										text2 = "ERROR_NONE_MAPPED";
									}
								}
								else
								{
									text2 = "ERROR_ACCESS_DENIED";
								}
							}
							else if (num != 1354)
							{
								if (num != 1727)
								{
									if (num == 8537)
									{
										text2 = "ERROR_DS_CANT_FIND_DC_FOR_SRC_DOMAIN";
									}
								}
								else
								{
									text2 = "RPC_S_CALL_FAILED_DNE";
								}
							}
							else
							{
								text2 = "ERROR_INVALID_DOMAIN_ROLE";
							}
							if (i < 9)
							{
								ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "CreatePlaceHolder: retrying on AddSidHistory() call for {0} after getting {1}.", sourceObject.Id.DistinguishedName, text2);
								if (1332 == nativeErrorCode)
								{
									Thread.Sleep(6000);
								}
								else
								{
									Thread.Sleep(500);
								}
								goto IL_34E;
							}
							if (1332 == nativeErrorCode)
							{
								ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "CreatePlaceHolder: give up retry on AddSidHistory() call for {0} after getting {1}. Skip it and continue.", sourceObject.Id.DistinguishedName, text2);
								TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Info", nativeErrorCode.ToString(), string.Format("DsAddSidHistory error 0x534 from DC {0}, source principal name {1}. Skipping DsAddSidHistory().", lastUsedDc, text), null);
								goto IL_34E;
							}
							if (1354 == nativeErrorCode)
							{
								ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>((long)this.GetHashCode(), "CreatePlaceHolder: give up retry on AddSidHistory() call for {0} after getting {1}. Skip it and continue.", sourceObject.Id.DistinguishedName, text2);
								TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Info", nativeErrorCode.ToString(), string.Format("DsAddSidHistory error 0x54A from DC {0}, source principal name {1}. Give up.", lastUsedDc, text), null);
								throw new TenantRelocationException(RelocationError.AddSidHistorySourcePdcTransferred, string.Format("TenantRelocationSync.AddSidHistory() returned {0} after {1} retries", text2, i), ex);
							}
							throw new TenantRelocationException(RelocationError.RPCPermanentError, string.Format("TenantRelocationSync.AddSidHistory() returned {0} after {1} retries", text2, i), ex);
						}
						else
						{
							if (nativeErrorCode == 1378)
							{
								throw new TenantRelocationException(RelocationError.MemberInAliasPermanentError, string.Format("TenantRelocationSync.AddSidHistory() returned ERROR_MEMBER_IN_ALIAS.", new object[0]), ex);
							}
							if (nativeErrorCode == 8536)
							{
								throw new TenantRelocationException(RelocationError.DestinationAuditingDisabled, string.Format("TenantRelocationSync.AddSidHistory() returned ERROR_DS_DESTINATION_AUDITING_NOT_ENABLED.", new object[0]), ex);
							}
						}
					}
					throw new TenantRelocationException(RelocationError.RPCPermanentError, string.Format("TenantRelocationSync.AddSidHistory() returned Win32Exception with unrecognized NativeErrorCode.", new object[0]), ex);
				}
				IL_34E:;
			}
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x00168E08 File Offset: 0x00167008
		private void AddSidHistory(string sourceDomainFqdn, string sourcePrincipalName, string destinationDomainControllerFqdn, string destinationDomainFqdn, string destinationPricipalName)
		{
			SafeDsBindHandle safeDsBindHandle = null;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "AddSidHistory: Source Domain FQDN: {0}, Source SamAccountName : {1}, Target DC FQDN: {2}, Target Domain FQDN: {3}, Target SamAccountName: {4}", new object[]
			{
				sourceDomainFqdn,
				sourcePrincipalName,
				destinationDomainControllerFqdn,
				destinationDomainFqdn,
				destinationPricipalName
			});
			try
			{
				uint num = NativeMethods.DsBindWithSpnEx(destinationDomainControllerFqdn, destinationDomainFqdn, IntPtr.Zero, null, 1U, out safeDsBindHandle);
				if (num != 0U)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string, uint>((long)this.GetHashCode(), "AddSidHistory: failed to bind to domain controller {0} in destination domain {1} with error code {2}", destinationDomainControllerFqdn, destinationDomainFqdn, num);
					TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Exception", num.ToString(), string.Format("DsBindWithSpnEx error from DC {0}, source principal name {1}", destinationDomainControllerFqdn, sourcePrincipalName), null);
					throw new ADExternalException(DirectoryStrings.ExceptionCannotBindToDomain(destinationDomainControllerFqdn, destinationDomainFqdn, num.ToString("X")), new Win32Exception((int)num));
				}
				num = NativeMethods.DsAddSidHistory(safeDsBindHandle, 0U, sourceDomainFqdn, sourcePrincipalName, null, IntPtr.Zero, destinationDomainFqdn, destinationPricipalName);
				if (num != 0U)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug((long)this.GetHashCode(), "AddSidHistory: failed to copy SIDs from user {0} in domain {1} to user {2} in domain {3}. The error code returned is {4}", new object[]
					{
						sourcePrincipalName,
						sourceDomainFqdn,
						destinationPricipalName,
						destinationDomainFqdn,
						num
					});
					TenantRelocationSyncLogger.Instance.Log(this.SyncData, "Exception", num.ToString(), string.Format("DsAddSidHistory error from DC {0}, source principal name {1}", destinationDomainControllerFqdn, sourcePrincipalName), null);
					throw new ADExternalException(DirectoryStrings.ExceptionCannotAddSidHistory(sourcePrincipalName, sourceDomainFqdn, destinationPricipalName, destinationDomainFqdn, num.ToString("X")), new Win32Exception((int)num));
				}
			}
			finally
			{
				if (safeDsBindHandle != null)
				{
					safeDsBindHandle.Dispose();
				}
			}
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x00168F8C File Offset: 0x0016718C
		internal static uint GetInt32ValueFromRegistryValueOrDefault(string valueName, uint defaultValue)
		{
			uint num;
			if (!TenantRelocationSyncCoordinator.GetInt32ValueFromRegistryValue(valueName, out num) || num == 0U)
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x00168FAC File Offset: 0x001671AC
		internal static bool GetInt32ValueFromRegistryValue(string valueName, out uint retValue)
		{
			retValue = 0U;
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>(0L, "TenantRelocationSyncCoordinator::GetInt32ValueFromRegistryValue(): reading registry value {0}/{1}.", "SOFTWARE\\Microsoft\\ExchangeLabs", valueName);
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
			{
				object obj = null;
				if (registryKey != null)
				{
					obj = registryKey.GetValue(valueName, null);
				}
				if (obj == null)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>(0L, "TenantRelocationSyncCoordinator::GetInt32ValueFromRegistryValue(): registry value {0}/{1} is not found.", "SOFTWARE\\Microsoft\\ExchangeLabs", valueName);
					result = false;
				}
				else if (obj is int)
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string, int>(0L, "TenantRelocationSyncCoordinator::GetInt32ValueFromRegistryValue(): registry value {0}/{1} is {2}.", "SOFTWARE\\Microsoft\\ExchangeLabs", valueName, (int)obj);
					retValue = (uint)((int)obj);
					result = true;
				}
				else
				{
					ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>(0L, "TenantRelocationSyncCoordinator::GetInt32ValueFromRegistryValue(): registry value {0}/{1} is not numeric type.", "SOFTWARE\\Microsoft\\ExchangeLabs", valueName);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x00169078 File Offset: 0x00167278
		internal static void InitializeConfigurableSettings()
		{
			TenantRelocationSyncCoordinator.LinksPerPageLimit = (int)TenantRelocationConfigImpl.GetConfig<uint>("DataSyncLinksPerPageLimit");
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.InitializeConfigurableSettings Global Config: TenantRelocationSyncCoordinator.LinksPerPageLimit = {0}", TenantRelocationSyncCoordinator.LinksPerPageLimit);
			TenantRelocationSyncCoordinator.LinksOverldapSize = (int)TenantRelocationConfigImpl.GetConfig<uint>("DataSyncLinksOverldapSize");
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "TenantRelocationSyncConfiguration.InitializeConfigurableSettings Global Config: TenantRelocationSyncCoordinator.LinksOverldapSize = {0}", TenantRelocationSyncCoordinator.LinksOverldapSize);
		}

		// Token: 0x04004373 RID: 17267
		internal static readonly PropertyDefinition[] identityProperties = new PropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			ADRecipientSchema.DisplayName,
			ADObjectSchema.ConfigurationUnit,
			ADObjectSchema.CorrelationIdRaw,
			ADObjectSchema.CorrelationId,
			TenantRelocationSyncSchema.ObjectId,
			IADSecurityPrincipalSchema.SamAccountName,
			IADSecurityPrincipalSchema.Sid,
			IADSecurityPrincipalSchema.SidHistory,
			TenantRelocationSyncSchema.Deleted,
			ADUserSchema.UserAccountControl
		};

		// Token: 0x04004374 RID: 17268
		private static readonly PropertyDefinition[] placeholderProperties = new PropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			ADRecipientSchema.DisplayName,
			ADObjectSchema.CorrelationIdRaw,
			ADObjectSchema.CorrelationId,
			ADRecipientSchema.UsnChanged,
			TenantRelocationSyncSchema.ObjectId,
			TenantRelocationSyncSchema.LastKnownParent,
			ADRecipientSchema.AttributeMetadata,
			IADSecurityPrincipalSchema.SamAccountName,
			IADSecurityPrincipalSchema.Sid,
			TenantRelocationSyncSchema.Deleted,
			TenantRelocationSyncSchema.AllAttributes,
			ADUserSchema.UserAccountControl
		};

		// Token: 0x04004375 RID: 17269
		private static readonly PropertyDefinition[] samAttributes = new PropertyDefinition[]
		{
			ADGroupSchema.Members
		};

		// Token: 0x04004376 RID: 17270
		private static readonly PropertyDefinition[] recipientAttributes = new PropertyDefinition[]
		{
			ADObjectSchema.ObjectClass,
			TenantRelocationSyncSchema.ObjectId,
			ADRecipientSchema.ConfigurationXMLRaw
		};

		// Token: 0x04004377 RID: 17271
		private ITenantConfigurationSession sourceSystemConfigurationSession;

		// Token: 0x04004378 RID: 17272
		private ITenantRecipientSession sourceRecipientSession;

		// Token: 0x04004379 RID: 17273
		private ITenantRecipientSession targetRecipientSession;

		// Token: 0x0400437A RID: 17274
		private ITopologyConfigurationSession sourceRootOrgConfigurationSession;

		// Token: 0x0400437B RID: 17275
		private ITopologyConfigurationSession sourcePartitionSession;

		// Token: 0x0400437C RID: 17276
		private ITopologyConfigurationSession targetPartitionSession;

		// Token: 0x0400437D RID: 17277
		private TenantRelocationSecurityDescriptorHandler securityDescriptorHandler;

		// Token: 0x0400437E RID: 17278
		public TenantRelocationSyncTranslator Translator;

		// Token: 0x0400437F RID: 17279
		private Lazy<bool> adDriverValidatorEnabled;

		// Token: 0x04004380 RID: 17280
		private TenantRelocationSyncPageToken cookie;

		// Token: 0x04004381 RID: 17281
		private WaitHandle[] breakEvents;

		// Token: 0x04004382 RID: 17282
		private TenantRelocationSyncPerfLogger perfLogger;

		// Token: 0x04004383 RID: 17283
		private TenantRelocationThrottlingManager throttlingManager;

		// Token: 0x0200080F RID: 2063
		// (Invoke) Token: 0x0600660F RID: 26127
		private delegate bool ExceptionHandler(Exception ex, int retryNum);

		// Token: 0x02000810 RID: 2064
		// (Invoke) Token: 0x06006613 RID: 26131
		private delegate void ProcessAction(int retryNum);
	}
}
