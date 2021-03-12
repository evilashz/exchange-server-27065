using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000097 RID: 151
	public abstract class SetADPermissionTaskBase : SetPermissionTaskBase<ADRawEntryIdParameter, ADAcePresentationObject, ADRawEntry>
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x0002A908 File Offset: 0x00028B08
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new SetADPermissionTaskModuleFactory();
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002A910 File Offset: 0x00028B10
		internal IConfigurationSession GetWritableSession(ADObjectId identity)
		{
			this.writableConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(identity), 106, "GetWritableSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\permission\\SetADPermissionTaskBase.cs");
			if (identity.IsDescendantOf(this.writableConfigurationSession.ConfigurationNamingContext))
			{
				return this.writableConfigurationSession;
			}
			if (base.DomainControllerDomainId == null || base.DomainControllerDomainId.Equals(identity.DomainId))
			{
				return this.writableSessionOnSpcecifiedDC;
			}
			return this.writableSession;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002A990 File Offset: 0x00028B90
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.readOnlySession = PermissionTaskHelper.GetReadOnlySession(base.DomainController);
			if (this.readOnlySession.UseGlobalCatalog)
			{
				this.globalCatalogSession = this.readOnlySession;
			}
			else
			{
				this.globalCatalogSession = PermissionTaskHelper.GetReadOnlySession(null);
			}
			this.writableSessionOnSpcecifiedDC = PermissionTaskHelper.GetWritableSession(base.DomainController);
			this.writableSession = PermissionTaskHelper.GetWritableSession(null);
			TaskLogger.LogExit();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002AA04 File Offset: 0x00028C04
		protected override IConfigurable ResolveDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable result = PermissionTaskHelper.ResolveDataObject(this.readOnlySession, base.ReadOnlyConfigurationSession, this.globalCatalogSession, this.Identity, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRawEntry>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002AA54 File Offset: 0x00028C54
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.Clear();
			base.InternalValidate();
			this.CheckRbac();
			if (base.IsInherited)
			{
				return;
			}
			if (base.ParameterSetName == "Owner")
			{
				return;
			}
			if (base.Instance.AccessRights == null && base.Instance.ExtendedRights == null)
			{
				base.WriteError(new MustSpecifyEitherAccessOrExtendedRightsException(), ErrorCategory.InvalidData, null);
			}
			if (base.ParameterSetName == "Instance")
			{
				if (base.Instance.User == null)
				{
					base.WriteError(new ArgumentException(Strings.ErrorUserNull, "User"), ErrorCategory.InvalidArgument, null);
				}
				if (base.Instance.AccessRights == null || base.Instance.AccessRights.Length == 0)
				{
					base.WriteError(new ArgumentException(Strings.ErrorAccessRightsEmpty, "AccessRights"), ErrorCategory.InvalidArgument, null);
				}
				this.genericAll = this.IsGenericAllAccessRight(base.Instance);
			}
			else
			{
				bool flag = false;
				bool flag2 = false;
				if (base.Instance.AccessRights != null)
				{
					foreach (ActiveDirectoryRights activeDirectoryRights in base.Instance.AccessRights)
					{
						if (activeDirectoryRights == ActiveDirectoryRights.GenericAll)
						{
							this.genericAll = true;
							break;
						}
						if (activeDirectoryRights == ActiveDirectoryRights.CreateChild || activeDirectoryRights == ActiveDirectoryRights.DeleteChild)
						{
							flag2 = true;
						}
						else if (activeDirectoryRights == ActiveDirectoryRights.ReadProperty || activeDirectoryRights == ActiveDirectoryRights.WriteProperty || activeDirectoryRights == ActiveDirectoryRights.Self)
						{
							flag = true;
						}
					}
					if (this.genericAll && base.Instance.AccessRights.Length > 1)
					{
						base.WriteError(new ArgumentException(Strings.ErrorGenericAllCannotbeUsedWithOtherAccessRights, "AccessRights"), ErrorCategory.InvalidArgument, null);
					}
				}
				if (!this.genericAll && base.Instance.ChildObjectTypes != null && !flag2 && !flag)
				{
					base.WriteError(new ArgumentException(Strings.ErrorChildObjectTypeParameter, "ChildObjectTypes"), ErrorCategory.InvalidArgument, null);
				}
				if (!this.genericAll && base.Instance.Properties != null && !flag)
				{
					base.WriteError(new ArgumentException(Strings.ErrorPropertyParameter, "Properties"), ErrorCategory.InvalidArgument, null);
				}
			}
			if (base.Instance.ExtendedRights != null)
			{
				for (int j = 0; j < base.Instance.ExtendedRights.Length; j++)
				{
					if (string.Compare(base.Instance.ExtendedRights[j].RawIdentity, "all", StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						this.allExtendedRightsSpecified = true;
						break;
					}
					this.extendedRight.Add((ExtendedRight)base.GetDataObject<ExtendedRight>(base.Instance.ExtendedRights[j], base.ReadOnlyConfigurationSession, null, new LocalizedString?(Strings.ErrorExtendedRightNotFound(base.Instance.ExtendedRights[j].ToString())), new LocalizedString?(LocalizedString.Empty)));
				}
			}
			if (base.Instance.ChildObjectTypes != null)
			{
				for (int k = 0; k < base.Instance.ChildObjectTypes.Length; k++)
				{
					this.childObjectType.Add((ADSchemaClassObject)base.GetDataObject<ADSchemaClassObject>(base.Instance.ChildObjectTypes[k], base.ReadOnlyConfigurationSession, null, new LocalizedString?(Strings.ErrorChildObjectTypeNotFound(base.Instance.ChildObjectTypes[k].ToString())), new LocalizedString?(Strings.ErrorChildObjectTypeNotUnique(base.Instance.ChildObjectTypes[k].ToString()))));
				}
			}
			if (base.Instance.Properties != null)
			{
				for (int l = 0; l < base.Instance.Properties.Length; l++)
				{
					IEnumerable<ADSchemaAttributeObject> objects = base.Instance.Properties[l].GetObjects<ADSchemaAttributeObject>(null, base.ReadOnlyConfigurationSession);
					using (IEnumerator<ADSchemaAttributeObject> enumerator = objects.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ADSchemaAttributeObject item = enumerator.Current;
							if (enumerator.MoveNext())
							{
								base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorPropertyTypeNotUnique(base.Instance.Properties[l].ToString())), ErrorCategory.InvalidData, null);
							}
							else
							{
								this.propertyType.Add(item);
							}
						}
						else
						{
							ExtendedRightIdParameter extendedRightIdParameter = ExtendedRightIdParameter.Parse(base.Instance.Properties[l].RawIdentity);
							this.propertyType.Add((ExtendedRight)base.GetDataObject<ExtendedRight>(extendedRightIdParameter, base.ReadOnlyConfigurationSession, null, new LocalizedString?(Strings.ErrorPropertyTypeNotFound(extendedRightIdParameter.ToString())), new LocalizedString?(Strings.ErrorPropertyTypeNotUnique(extendedRightIdParameter.ToString()))));
						}
					}
				}
			}
			if (base.Instance.InheritedObjectType != null)
			{
				this.inheritedObjectType = (ADSchemaClassObject)base.GetDataObject<ADSchemaClassObject>(base.Instance.InheritedObjectType, base.ReadOnlyConfigurationSession, null, new LocalizedString?(Strings.ErrorInheritedObjectTypeNotFound(base.Instance.InheritedObjectType.ToString())), new LocalizedString?(Strings.ErrorInheritedObjectTypeNotUnique(base.Instance.InheritedObjectType.ToString())));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002AF0C File Offset: 0x0002910C
		private void Clear()
		{
			this.extendedRight.Clear();
			this.propertyType.Clear();
			this.childObjectType.Clear();
			this.allExtendedRightsSpecified = false;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002AF36 File Offset: 0x00029136
		protected override bool IsEqualEntry(int Index)
		{
			return base.ModifiedObjects[Index].Id.Equals(this.DataObject.Id);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002AF5C File Offset: 0x0002915C
		protected override void ConstructAcl(List<ActiveDirectoryAccessRule> modifiedAcl)
		{
			TaskLogger.LogEnter();
			Guid inheritedObjectTypeGuid = (base.Instance.InheritedObjectType != null) ? this.inheritedObjectType.SchemaIDGuid : Guid.Empty;
			AccessControlType accessControlType = base.Instance.Deny ? AccessControlType.Deny : AccessControlType.Allow;
			if (this.allExtendedRightsSpecified)
			{
				modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, ActiveDirectoryRights.ExtendedRight, accessControlType, Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
			}
			else if (this.extendedRight != null)
			{
				foreach (ExtendedRight extendedRight in this.extendedRight)
				{
					modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, ActiveDirectoryRights.ExtendedRight, accessControlType, (extendedRight != null) ? extendedRight.RightsGuid : Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
				}
			}
			if (base.Instance.AccessRights != null)
			{
				if (this.genericAll)
				{
					if (this.childObjectType.Count > 0)
					{
						this.ConstructChildObjectTypesAcl(modifiedAcl, ActiveDirectoryRights.GenericAll, accessControlType, inheritedObjectTypeGuid);
					}
					else if (this.propertyType.Count > 0)
					{
						this.ConstructPropertiesAcl(modifiedAcl, ActiveDirectoryRights.GenericAll, accessControlType, inheritedObjectTypeGuid);
					}
					else
					{
						modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, ActiveDirectoryRights.GenericAll, accessControlType, Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
					}
				}
				else
				{
					foreach (ActiveDirectoryRights activeDirectoryRights in base.Instance.AccessRights)
					{
						if ((activeDirectoryRights & ActiveDirectoryRights.ExtendedRight) == ActiveDirectoryRights.ExtendedRight && !this.allExtendedRightsSpecified && (this.extendedRight == null || this.extendedRight.Count == 0))
						{
							modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, ActiveDirectoryRights.ExtendedRight, accessControlType, Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
						}
						if ((activeDirectoryRights & ActiveDirectoryRights.CreateChild) == ActiveDirectoryRights.CreateChild || (activeDirectoryRights & ActiveDirectoryRights.DeleteChild) == ActiveDirectoryRights.DeleteChild)
						{
							ActiveDirectoryRights right = activeDirectoryRights & (ActiveDirectoryRights.CreateChild | ActiveDirectoryRights.DeleteChild);
							this.ConstructChildObjectTypesAcl(modifiedAcl, right, accessControlType, inheritedObjectTypeGuid);
						}
						if ((activeDirectoryRights & ActiveDirectoryRights.ReadProperty) == ActiveDirectoryRights.ReadProperty || (activeDirectoryRights & ActiveDirectoryRights.WriteProperty) == ActiveDirectoryRights.WriteProperty || (activeDirectoryRights & ActiveDirectoryRights.Self) == ActiveDirectoryRights.Self)
						{
							if (this.childObjectType.Count > 0)
							{
								this.ConstructChildObjectTypesAcl(modifiedAcl, activeDirectoryRights, accessControlType, inheritedObjectTypeGuid);
								if (this.propertyType.Count > 0)
								{
									ActiveDirectoryRights right2 = activeDirectoryRights & (ActiveDirectoryRights.Self | ActiveDirectoryRights.ReadProperty | ActiveDirectoryRights.WriteProperty);
									this.ConstructPropertiesAcl(modifiedAcl, right2, accessControlType, inheritedObjectTypeGuid);
								}
							}
							else
							{
								ActiveDirectoryRights right3 = activeDirectoryRights & (ActiveDirectoryRights.Self | ActiveDirectoryRights.ReadProperty | ActiveDirectoryRights.WriteProperty);
								this.ConstructPropertiesAcl(modifiedAcl, right3, accessControlType, inheritedObjectTypeGuid);
							}
						}
						ActiveDirectoryRights activeDirectoryRights2 = activeDirectoryRights & ~ActiveDirectoryRights.ExtendedRight & ~ActiveDirectoryRights.CreateChild & ~ActiveDirectoryRights.DeleteChild & ~ActiveDirectoryRights.ReadProperty & ~ActiveDirectoryRights.WriteProperty & ~ActiveDirectoryRights.Self;
						if (activeDirectoryRights2 != (ActiveDirectoryRights)0)
						{
							modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, activeDirectoryRights2, accessControlType, Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002B21C File Offset: 0x0002941C
		protected void ConstructPropertiesAcl(List<ActiveDirectoryAccessRule> modifiedAcl, ActiveDirectoryRights right, AccessControlType allowOrDeny, Guid inheritedObjectTypeGuid)
		{
			if (this.propertyType.Count == 0)
			{
				modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, right, allowOrDeny, Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
				return;
			}
			Guid objectType = Guid.Empty;
			foreach (ADObject adobject in this.propertyType)
			{
				if (adobject is ExtendedRight)
				{
					objectType = ((ExtendedRight)adobject).RightsGuid;
				}
				else
				{
					objectType = ((ADSchemaAttributeObject)adobject).SchemaIDGuid;
				}
				modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, right, allowOrDeny, objectType, base.Instance.InheritanceType, inheritedObjectTypeGuid));
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002B2E4 File Offset: 0x000294E4
		protected void ConstructChildObjectTypesAcl(List<ActiveDirectoryAccessRule> modifiedAcl, ActiveDirectoryRights right, AccessControlType allowOrDeny, Guid inheritedObjectTypeGuid)
		{
			if (this.childObjectType.Count == 0)
			{
				modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, right, allowOrDeny, Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
				return;
			}
			foreach (ADSchemaClassObject adschemaClassObject in this.childObjectType)
			{
				modifiedAcl.Add(new ActiveDirectoryAccessRule(base.SecurityPrincipalSid, right, allowOrDeny, (adschemaClassObject != null) ? adschemaClassObject.SchemaIDGuid : Guid.Empty, base.Instance.InheritanceType, inheritedObjectTypeGuid));
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002B394 File Offset: 0x00029594
		private void CheckRbac()
		{
			ADScopeException ex = null;
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)this.DataObject[ADObjectSchema.ObjectClass];
			if (multiValuedProperty.Contains("person") || multiValuedProperty.Contains("msExchDynamicDistributionList") || multiValuedProperty.Contains("group") || multiValuedProperty.Contains("publicFolder") || multiValuedProperty.Contains("msExchPublicMDB") || multiValuedProperty.Contains("msExchSystemMailbox") || multiValuedProperty.Contains(ADMicrosoftExchangeRecipient.MostDerivedClass) || multiValuedProperty.Contains("exchangeAdminService") || multiValuedProperty.Contains("computer"))
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, this.DataObject.Id, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 581, "CheckRbac", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\permission\\SetADPermissionTaskBase.cs");
				ADRecipient adrecipient = tenantOrRootOrgRecipientSession.Read(this.DataObject.Id);
				if (adrecipient == null)
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorObjectNotFound(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
				}
				if (!tenantOrRootOrgRecipientSession.TryVerifyIsWithinScopes(adrecipient, true, out ex))
				{
					base.WriteError(new TaskInvalidOperationException(Strings.ErrorCannotChangeObjectOutOfWriteScope(adrecipient.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ExchangeErrorCategory.Client, adrecipient.Identity);
					return;
				}
			}
			else
			{
				ADObject adobject = null;
				bool flag = false;
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromCustomScopeSet(base.ScopeSet, this.DataObject.Id, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true), 620, "CheckRbac", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\permission\\SetADPermissionTaskBase.cs");
				if (multiValuedProperty.Contains("msExchOabVirtualDirectory"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<ADOabVirtualDirectory>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("ServiceConnectionPoint"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<ADServiceConnectionPoint>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("RpcClientAccess"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<ExchangeRpcClientAccess>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchProtocolCfgHTTPContainer"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<HttpContainer>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchProtocolCfgIMAPContainer"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<Imap4Container>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchInformationStore"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<InformationStore>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("mTA"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<MicrosoftMTAConfiguration>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchProtocolCfgPOPContainer"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<Pop3Container>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("protocolCfgSharedServer"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<ProtocolsContainer>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchSmtpReceiveConnector"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<ReceiveConnector>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchExchangeServer"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<Server>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchProtocolCfgSMTPContainer"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<SmtpContainer>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("protocolCfgSMTPServer"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<SmtpVirtualServerConfiguration>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchMDB"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<Database>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("msExchPrivateMDB"))
				{
					adobject = tenantOrTopologyConfigurationSession.Read<MailboxDatabase>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains(ADOrganizationalUnit.MostDerivedClass))
				{
					tenantOrTopologyConfigurationSession.UseConfigNC = false;
					adobject = tenantOrTopologyConfigurationSession.Read<ADOrganizationalUnit>(this.DataObject.Id);
					flag = true;
				}
				else if (multiValuedProperty.Contains("domain"))
				{
					tenantOrTopologyConfigurationSession.UseConfigNC = false;
					adobject = tenantOrTopologyConfigurationSession.Read<ADDomain>(this.DataObject.Id);
					flag = true;
				}
				if (flag)
				{
					if (adobject == null)
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorObjectNotFound(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
					}
					if (!tenantOrTopologyConfigurationSession.TryVerifyIsWithinScopes(adobject, true, out ex))
					{
						base.WriteError(new TaskInvalidOperationException(Strings.ErrorCannotChangeObjectOutOfWriteScope(adobject.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ExchangeErrorCategory.Client, adobject.Identity);
					}
				}
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002B874 File Offset: 0x00029A74
		private bool IsGenericAllAccessRight(ADAcePresentationObject ace)
		{
			foreach (ActiveDirectoryRights activeDirectoryRights in ace.AccessRights)
			{
				if (activeDirectoryRights == ActiveDirectoryRights.GenericAll)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400020F RID: 527
		private IConfigurationSession readOnlySession;

		// Token: 0x04000210 RID: 528
		private IConfigurationSession globalCatalogSession;

		// Token: 0x04000211 RID: 529
		private IConfigurationSession writableSessionOnSpcecifiedDC;

		// Token: 0x04000212 RID: 530
		private IConfigurationSession writableConfigurationSession;

		// Token: 0x04000213 RID: 531
		private IConfigurationSession writableSession;

		// Token: 0x04000214 RID: 532
		private List<ExtendedRight> extendedRight = new List<ExtendedRight>();

		// Token: 0x04000215 RID: 533
		private List<ADObject> propertyType = new List<ADObject>();

		// Token: 0x04000216 RID: 534
		private List<ADSchemaClassObject> childObjectType = new List<ADSchemaClassObject>();

		// Token: 0x04000217 RID: 535
		private ADSchemaClassObject inheritedObjectType;

		// Token: 0x04000218 RID: 536
		private bool allExtendedRightsSpecified;

		// Token: 0x04000219 RID: 537
		private bool genericAll;
	}
}
