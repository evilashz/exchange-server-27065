using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000096 RID: 150
	public abstract class SetPermissionTaskBase<TIdentity, TPublicObject, TDataObject> : SetObjectWithIdentityTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : AcePresentationObject, new() where TDataObject : IConfigurable, new()
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002A2E1 File Offset: 0x000284E1
		internal ADObjectId DomainControllerDomainId
		{
			get
			{
				return this.domainControllerDomainId;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002A2E9 File Offset: 0x000284E9
		internal IRecipientSession ReadOnlyRecipientSession
		{
			get
			{
				return this.readOnlyRecipientSession;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002A2F1 File Offset: 0x000284F1
		internal IRecipientSession GlobalCatalogRecipientSession
		{
			get
			{
				return this.globalCatalogRecipientSession;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002A2F9 File Offset: 0x000284F9
		internal IConfigurationSession ReadOnlyConfigurationSession
		{
			get
			{
				return this.readOnlyConfigurationSession;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002A301 File Offset: 0x00028501
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002A309 File Offset: 0x00028509
		protected virtual int CurrentProcessedObject
		{
			get
			{
				return this.currentProcessedObject;
			}
			set
			{
				this.currentProcessedObject = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002A312 File Offset: 0x00028512
		protected List<List<ActiveDirectoryAccessRule>> ModifiedAcl
		{
			get
			{
				return this.modifiedAcl;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002A31A File Offset: 0x0002851A
		protected List<TDataObject> ModifiedObjects
		{
			get
			{
				return this.modifiedObjects;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002A322 File Offset: 0x00028522
		protected SecurityIdentifier SecurityPrincipalSid
		{
			get
			{
				if (!(this.securityPrincipal.MasterAccountSid != null) || this.securityPrincipal.MasterAccountSid.IsWellKnown(WellKnownSidType.SelfSid))
				{
					return this.securityPrincipal.Sid;
				}
				return this.securityPrincipal.MasterAccountSid;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002A362 File Offset: 0x00028562
		protected override IConfigDataProvider CreateSession()
		{
			return null;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002A368 File Offset: 0x00028568
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			this.readOnlyRecipientSession = PermissionTaskHelper.GetReadOnlyRecipientSession(this.DomainController);
			if (this.readOnlyRecipientSession.UseGlobalCatalog)
			{
				this.globalCatalogRecipientSession = this.readOnlyRecipientSession;
			}
			else
			{
				this.globalCatalogRecipientSession = PermissionTaskHelper.GetReadOnlyRecipientSession(null);
			}
			this.readOnlyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 207, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\permission\\SetPermissionTaskBase.cs");
			if (this.DomainController != null)
			{
				ADServer adserver = DirectoryUtilities.DomainControllerFromName(this.DomainController);
				if (adserver == null)
				{
					base.ThrowTerminatingError(new RecipientTaskException(Strings.DCWithGivenNameCouldNotBeFound(this.DomainController)), ErrorCategory.ObjectNotFound, null);
				}
				this.domainControllerDomainId = adserver.DomainId;
			}
			this.modifiedAcl.Clear();
			this.modifiedObjects.Clear();
			TaskLogger.LogExit();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002A444 File Offset: 0x00028644
		protected override void StampChangesOn(IConfigurable dataObject)
		{
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002A448 File Offset: 0x00028648
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0002A4D9 File Offset: 0x000286D9
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ParameterSetName = "AccessRights")]
		[Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true, ParameterSetName = "Owner")]
		[Parameter(Mandatory = false, Position = 0, ParameterSetName = "Instance")]
		public override TIdentity Identity
		{
			get
			{
				if (base.Fields[SetPermissionTaskBase<TIdentity, TPublicObject, TDataObject>.paramIdentity] != null)
				{
					return (TIdentity)((object)base.Fields[SetPermissionTaskBase<TIdentity, TPublicObject, TDataObject>.paramIdentity]);
				}
				if (this.Instance != null)
				{
					TIdentity result = (default(TIdentity) == null) ? Activator.CreateInstance<TIdentity>() : default(TIdentity);
					TPublicObject instance = this.Instance;
					result.Initialize((ADObjectId)instance.Identity);
					return result;
				}
				return (TIdentity)((object)null);
			}
			set
			{
				base.Fields[SetPermissionTaskBase<TIdentity, TPublicObject, TDataObject>.paramIdentity] = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002A4F1 File Offset: 0x000286F1
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0002A4F9 File Offset: 0x000286F9
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0002A502 File Offset: 0x00028702
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0002A50A File Offset: 0x0002870A
		[Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Instance")]
		public new TPublicObject Instance
		{
			get
			{
				return base.Instance;
			}
			set
			{
				base.Instance = value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002A514 File Offset: 0x00028714
		protected bool IsInherited
		{
			get
			{
				if (this.Instance != null)
				{
					TPublicObject instance = this.Instance;
					return instance.IsInherited;
				}
				return false;
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002A544 File Offset: 0x00028744
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.IsInherited)
			{
				TPublicObject instance = this.Instance;
				this.WriteWarning(Strings.ErrorWillNotPerformOnInheritedAccessRight(instance.Identity.ToString()));
				return;
			}
			if (base.ParameterSetName == "Owner")
			{
				return;
			}
			TPublicObject instance2 = this.Instance;
			if (instance2.User != null)
			{
				IRecipientSession session = this.GlobalCatalogRecipientSession;
				TPublicObject instance3 = this.Instance;
				this.securityPrincipal = SecurityPrincipalIdParameter.GetSecurityPrincipal(session, instance3.User, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002A5F4 File Offset: 0x000287F4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.IsInherited)
			{
				return;
			}
			this.PutEachEntryInList();
			TaskLogger.LogExit();
		}

		// Token: 0x06000A1A RID: 2586
		protected abstract void ApplyModification(TDataObject modifiedObject, ActiveDirectoryAccessRule[] modifiedAces);

		// Token: 0x06000A1B RID: 2587
		protected abstract void WriteAces(ADObjectId id, IEnumerable<ActiveDirectoryAccessRule> aces);

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002A610 File Offset: 0x00028810
		protected void WriteCurrentProgress(LocalizedString activityDesc, LocalizedString status, ProgressRecordType recordType, int percent)
		{
			base.WriteProgress(new ExProgressRecord(0, activityDesc, status)
			{
				RecordType = recordType,
				PercentComplete = percent
			});
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002A63C File Offset: 0x0002883C
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			if ("Owner" != base.ParameterSetName)
			{
				this.CurrentProcessedObject = 0;
				while (this.CurrentProcessedObject < this.ModifiedObjects.Count)
				{
					try
					{
						this.ApplyModification(this.ModifiedObjects[this.CurrentProcessedObject], this.ModifiedAcl[this.CurrentProcessedObject].ToArray());
					}
					catch (OverflowException exception)
					{
						this.WriteError(exception, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_180;
					}
					catch (DataValidationException exception2)
					{
						this.WriteError(exception2, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_180;
					}
					catch (DataSourceOperationException exception3)
					{
						this.WriteError(exception3, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_180;
					}
					catch (DataSourceTransientException exception4)
					{
						this.WriteError(exception4, ErrorCategory.WriteError, base.CurrentObjectIndex, false);
						goto IL_180;
					}
					goto IL_C1;
					IL_180:
					this.CurrentProcessedObject++;
					continue;
					IL_C1:
					if (this.CurrentProcessedObject + 1 < this.ModifiedObjects.Count)
					{
						this.WriteCurrentProgress(Strings.ProcessingAceActivity, Strings.ProcessingAceStatus(((ADRawEntry)((object)this.ModifiedObjects[this.CurrentProcessedObject])).Id.ToString()), ProgressRecordType.Processing, this.CurrentProcessedObject * 100 / this.ModifiedObjects.Count);
					}
					this.WriteAces(((ADRawEntry)((object)this.ModifiedObjects[this.CurrentProcessedObject])).Id, this.ModifiedAcl[this.CurrentProcessedObject]);
					if (this.CurrentProcessedObject + 1 == this.ModifiedObjects.Count)
					{
						this.WriteCurrentProgress(Strings.ProcessingAceActivity, Strings.CompletedAceActivity, ProgressRecordType.Completed, 100);
						goto IL_180;
					}
					goto IL_180;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002A828 File Offset: 0x00028A28
		internal virtual void WriteErrorPerObject(LocalizedException exception, ExchangeErrorCategory category, object target)
		{
			if (target == null)
			{
				this.WriteError(exception, category, this.CurrentProcessedObject, false);
				return;
			}
			base.WriteError(exception, category, target);
		}

		// Token: 0x06000A1F RID: 2591
		protected abstract void ConstructAcl(List<ActiveDirectoryAccessRule> modifiedAcl);

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A84C File Offset: 0x00028A4C
		protected void PutEachEntryInList()
		{
			List<ActiveDirectoryAccessRule> item = null;
			bool flag = false;
			for (int i = 0; i < this.ModifiedObjects.Count; i++)
			{
				if (this.IsEqualEntry(i))
				{
					item = this.ModifiedAcl[i];
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				item = new List<ActiveDirectoryAccessRule>();
			}
			this.ConstructAcl(item);
			if (!flag)
			{
				this.ModifiedObjects.Add(this.DataObject);
				this.ModifiedAcl.Add(item);
			}
		}

		// Token: 0x06000A21 RID: 2593
		protected abstract bool IsEqualEntry(int index);

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002A8BD File Offset: 0x00028ABD
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is SecurityDescriptorAccessDeniedException;
		}

		// Token: 0x04000206 RID: 518
		private IRecipientSession readOnlyRecipientSession;

		// Token: 0x04000207 RID: 519
		private IRecipientSession globalCatalogRecipientSession;

		// Token: 0x04000208 RID: 520
		private IConfigurationSession readOnlyConfigurationSession;

		// Token: 0x04000209 RID: 521
		private ADObjectId domainControllerDomainId;

		// Token: 0x0400020A RID: 522
		private IADSecurityPrincipal securityPrincipal;

		// Token: 0x0400020B RID: 523
		private static string paramIdentity = "Identity";

		// Token: 0x0400020C RID: 524
		private List<List<ActiveDirectoryAccessRule>> modifiedAcl = new List<List<ActiveDirectoryAccessRule>>();

		// Token: 0x0400020D RID: 525
		private List<TDataObject> modifiedObjects = new List<TDataObject>();

		// Token: 0x0400020E RID: 526
		private int currentProcessedObject;
	}
}
