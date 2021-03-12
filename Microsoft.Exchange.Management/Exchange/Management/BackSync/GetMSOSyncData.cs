using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;
using Microsoft.Exchange.Management.BackSync.Configuration;
using Microsoft.Exchange.Management.BackSync.Processors;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.BackSync
{
	// Token: 0x020000A5 RID: 165
	[Cmdlet("Get", "MSOSyncData", DefaultParameterSetName = "IncrementalSyncParameterSet")]
	public sealed class GetMSOSyncData : GetTaskBase<ADRawEntry>
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00016403 File Offset: 0x00014603
		private IDataProcessor DataProcessor
		{
			get
			{
				if (this.dataProcessor == null)
				{
					this.dataProcessor = this.CreateDataProcessor();
				}
				return this.dataProcessor;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001641F File Offset: 0x0001461F
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00016436 File Offset: 0x00014636
		[Parameter(Mandatory = true, ParameterSetName = "ObjectFullSyncInitialCallParameterSet")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "TenantFullSyncInitialCallParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "IncrementalSyncParameterSet")]
		public byte[] Cookie
		{
			get
			{
				return (byte[])base.Fields["Cookie"];
			}
			set
			{
				base.Fields["Cookie"] = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00016449 File Offset: 0x00014649
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00016460 File Offset: 0x00014660
		[Parameter(Mandatory = true, ParameterSetName = "ObjectFullSyncInitialCallFromMergeSyncParameterSet")]
		[Parameter(Mandatory = true, ParameterSetName = "ObjectFullSyncInitialCallParameterSet")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "ObjectFullSyncInitialCallFromTenantFullSyncParameterSet")]
		public SyncObjectId[] ObjectIds
		{
			get
			{
				return (SyncObjectId[])base.Fields["ObjectIds"];
			}
			set
			{
				base.Fields["ObjectIds"] = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00016473 File Offset: 0x00014673
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0001648A File Offset: 0x0001468A
		[Parameter(Mandatory = true, ParameterSetName = "TenantFullSyncInitialCallParameterSet")]
		[ValidateNotNullOrEmpty]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001649D File Offset: 0x0001469D
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x000164BE File Offset: 0x000146BE
		[Parameter(Mandatory = false, ParameterSetName = "ObjectFullSyncInitialCallParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "ObjectFullSyncInitialCallFromMergeSyncParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "ObjectFullSyncInitialCallFromTenantFullSyncParameterSet")]
		public BackSyncOptions SyncOptions
		{
			get
			{
				return (BackSyncOptions)(base.Fields["SyncOptions"] ?? BackSyncOptions.None);
			}
			set
			{
				base.Fields["SyncOptions"] = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x000164D6 File Offset: 0x000146D6
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x000164ED File Offset: 0x000146ED
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "ObjectFullSyncSubsequentCallParameterSet")]
		public byte[] ObjectFullSyncPageToken
		{
			get
			{
				return (byte[])base.Fields["ObjectFullSyncPageToken"];
			}
			set
			{
				base.Fields["ObjectFullSyncPageToken"] = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00016500 File Offset: 0x00014700
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00016517 File Offset: 0x00014717
		[Parameter(Mandatory = true, ParameterSetName = "TenantFullSyncSubsequentCallParameterSet")]
		[ValidateNotNullOrEmpty]
		public byte[] TenantFullSyncPageToken
		{
			get
			{
				return (byte[])base.Fields["TenantFullSyncPageToken"];
			}
			set
			{
				base.Fields["TenantFullSyncPageToken"] = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001652A File Offset: 0x0001472A
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00016541 File Offset: 0x00014741
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "ObjectFullSyncInitialCallFromTenantFullSyncParameterSet")]
		public byte[] TenantFullSyncPageTokenContext
		{
			get
			{
				return (byte[])base.Fields["TenantFullSyncPageTokenContext"];
			}
			set
			{
				base.Fields["TenantFullSyncPageTokenContext"] = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00016554 File Offset: 0x00014754
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001656B File Offset: 0x0001476B
		[Parameter(Mandatory = true, ParameterSetName = "MergeInitialCallParameterSet")]
		[ValidateNotNullOrEmpty]
		public byte[] MergeTenantFullSyncPageToken
		{
			get
			{
				return (byte[])base.Fields["MergeTenantFullSyncPageToken"];
			}
			set
			{
				base.Fields["MergeTenantFullSyncPageToken"] = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001657E File Offset: 0x0001477E
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00016595 File Offset: 0x00014795
		[Parameter(Mandatory = true, ParameterSetName = "MergeInitialCallParameterSet")]
		[ValidateNotNullOrEmpty]
		public byte[] MergeIncrementalSyncCookie
		{
			get
			{
				return (byte[])base.Fields["MergeIncrementalSyncCookie"];
			}
			set
			{
				base.Fields["MergeIncrementalSyncCookie"] = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000165A8 File Offset: 0x000147A8
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x000165BF File Offset: 0x000147BF
		[Parameter(Mandatory = true, ParameterSetName = "MergeSubsequentCallParameterSet")]
		[ValidateNotNullOrEmpty]
		public byte[] MergePageToken
		{
			get
			{
				return (byte[])base.Fields["MergePageToken"];
			}
			set
			{
				base.Fields["MergePageToken"] = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000165D2 File Offset: 0x000147D2
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000165E9 File Offset: 0x000147E9
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "ObjectFullSyncInitialCallFromMergeSyncParameterSet")]
		public byte[] MergePageTokenContext
		{
			get
			{
				return (byte[])base.Fields["MergePageTokenContext"];
			}
			set
			{
				base.Fields["MergePageTokenContext"] = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000165FC File Offset: 0x000147FC
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00016613 File Offset: 0x00014813
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public ServiceInstanceId ServiceInstance
		{
			get
			{
				return (ServiceInstanceId)base.Fields["ServiceInstance"];
			}
			set
			{
				base.Fields["ServiceInstance"] = value;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00016628 File Offset: 0x00014828
		static GetMSOSyncData()
		{
			if (SyncConfiguration.EnableCloudPublicDelegatesRecipientFiltering())
			{
				GetMSOSyncData.PropertyFilterMap.Add(SyncUserSchema.CloudPublicDelegates, RecipientTypeDetails.UserMailbox);
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000166AD File Offset: 0x000148AD
		private ADObjectId GetRootOrgId()
		{
			return ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000166B4 File Offset: 0x000148B4
		private void ResolveOrganization(bool resolveRetiredTenant = false)
		{
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAllTenantsPartitionId(this.currentPartitionId), 338, "ResolveOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
			tenantConfigurationSession.SessionSettings.TenantConsistencyMode = (resolveRetiredTenant ? TenantConsistencyMode.IncludeRetiredTenants : TenantConsistencyMode.IgnoreRetiredTenants);
			string text = null;
			if (this.Organization != null)
			{
				OrganizationIdParameter organization = this.Organization;
				text = organization.ToString();
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Get organization from cmdlet Organization parameter. orgId {0}.", organization.RawIdentity);
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "orgIdString {0}", text);
				this.tenantFullSyncOrganizationCU = (ExchangeConfigurationUnit)base.GetDataObject<ExchangeConfigurationUnit>(organization, tenantConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(text)), new LocalizedString?(Strings.ErrorOrganizationNotUnique(text)));
			}
			else if (this.tenantFullSyncPageToken != null)
			{
				text = this.tenantFullSyncPageToken.TenantExternalDirectoryId.ToString();
				this.tenantFullSyncOrganizationCU = this.GetCUForExternalDirectoryOrganizationId(tenantConfigurationSession, text);
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Get organization from tenant token. orgId {0}.", text);
			}
			else if (this.mergePageToken != null)
			{
				text = this.mergePageToken.TenantExternalDirectoryId.ToString();
				this.tenantFullSyncOrganizationCU = this.GetCUForExternalDirectoryOrganizationId(tenantConfigurationSession, text);
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Get organization from merge page token. orgId {0}.", text);
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "tenantFullSyncOrganizationCU {0}", this.tenantFullSyncOrganizationCU.DistinguishedName);
			if (GetMSOSyncData.msOnlineScope == null)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Find MSO scope {0} ...", "MSOnlinePartnerScope");
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 382, "ResolveOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
				ManagementScope managementScope = topologyConfigurationSession.ReadRootOrgManagementScopeByName("MSOnlinePartnerScope");
				if (managementScope == null)
				{
					ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Unable to find MSO scope {0}", "MSOnlinePartnerScope");
					base.WriteError(new ADExternalException(Strings.ErrorScopeNotFound("MSOnlinePartnerScope")), ErrorCategory.ObjectNotFound, "MSOnlinePartnerScope");
				}
				if (!ExchangeRunspaceConfiguration.TryStampQueryFilterOnManagementScope(managementScope))
				{
					ExTraceGlobals.BackSyncTracer.TraceError((long)SyncConfiguration.TraceId, "TryStampQueryFilterOnManagementScope failed");
					base.WriteError(new DataValidationException(new PropertyValidationError(Strings.ErrorAcceptedDomainExists(managementScope.Filter), ManagementScopeSchema.Filter, managementScope.Filter)), ErrorCategory.InvalidData, managementScope.Filter);
				}
				GetMSOSyncData.msOnlineScope = managementScope;
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "ResolveOrganization msOnlineScope {0}", GetMSOSyncData.msOnlineScope.DistinguishedName);
			}
			TenantOrganizationPresentationObject obj = new TenantOrganizationPresentationObject(this.tenantFullSyncOrganizationCU);
			if (!OpathFilterEvaluator.FilterMatches(GetMSOSyncData.msOnlineScope.QueryFilter, obj))
			{
				ADObjectId adobjectId;
				string text2 = base.TryGetExecutingUserId(out adobjectId) ? adobjectId.ToCanonicalName() : base.ExecutingUserIdentityName;
				ExTraceGlobals.BackSyncTracer.TraceError<string, string>((long)SyncConfiguration.TraceId, "User {0} has no access to orgId {1}", text2, text);
				base.WriteError(new ADScopeException(Strings.ErrorOrgOutOfPartnerScope(text2, text)), (ErrorCategory)1004, this.tenantFullSyncOrganizationCU);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00016994 File Offset: 0x00014B94
		protected override void InternalBeginProcessing()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "GetMSOSyncData.InternalBeginProcessing entering");
			this.dirSyncBasedTenantFullSyncThreshold = SyncConfiguration.DirSyncBasedTenantFullSyncThreshold();
			base.InternalBeginProcessing();
			this.performanceCounterSession = this.CreatePerformanceCounterSession();
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Created performance counter session {0}", this.performanceCounterSession.GetType().Name);
			this.performanceCounterSession.Initialize();
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Initialized performance counter session");
			try
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Process input cookie based on parameter set {0}", base.ParameterSetName.ToString());
				this.currentPartitionId = GetMSOSyncData.GetPartitionIdFromServiceInstance(this.ServiceInstance);
				if (null == this.currentPartitionId)
				{
					base.WriteError(new CannotResolvePartitionFromInstanceIdException(this.ServiceInstance.ToString()), ErrorCategory.InvalidArgument, null);
				}
				string parameterSetName;
				if ((parameterSetName = base.ParameterSetName) != null)
				{
					if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6000546-1 == null)
					{
						<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6000546-1 = new Dictionary<string, int>(9)
						{
							{
								"IncrementalSyncParameterSet",
								0
							},
							{
								"ObjectFullSyncInitialCallParameterSet",
								1
							},
							{
								"ObjectFullSyncInitialCallFromTenantFullSyncParameterSet",
								2
							},
							{
								"ObjectFullSyncInitialCallFromMergeSyncParameterSet",
								3
							},
							{
								"ObjectFullSyncSubsequentCallParameterSet",
								4
							},
							{
								"TenantFullSyncInitialCallParameterSet",
								5
							},
							{
								"TenantFullSyncSubsequentCallParameterSet",
								6
							},
							{
								"MergeInitialCallParameterSet",
								7
							},
							{
								"MergeSubsequentCallParameterSet",
								8
							}
						};
					}
					int num;
					if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6000546-1.TryGetValue(parameterSetName, out num))
					{
						switch (num)
						{
						case 0:
							this.syncCookie = ((this.Cookie == null) ? new BackSyncCookie(this.ServiceInstance) : BackSyncCookie.Parse(this.Cookie));
							this.ValidateServiceInstance(this.syncCookie.ServiceInstanceId);
							this.invocationId = this.syncCookie.InvocationId;
							break;
						case 1:
						{
							BackSyncCookie backSyncCookie = BackSyncCookie.Parse(this.Cookie);
							this.ValidateServiceInstance(backSyncCookie.ServiceInstanceId);
							this.objectFullSyncPageToken = new ObjectFullSyncPageToken(backSyncCookie.InvocationId, this.ObjectIds, this.SyncOptions, backSyncCookie.ServiceInstanceId);
							this.invocationId = this.objectFullSyncPageToken.InvocationId;
							break;
						}
						case 2:
						{
							TenantFullSyncPageToken tenantFullSyncPageToken = Microsoft.Exchange.Data.Directory.Sync.TenantFullSyncPageToken.Parse(this.TenantFullSyncPageTokenContext);
							this.ValidateServiceInstance(tenantFullSyncPageToken.ServiceInstanceId);
							this.objectFullSyncPageToken = new ObjectFullSyncPageToken(tenantFullSyncPageToken.InvocationId, this.ObjectIds, this.SyncOptions, tenantFullSyncPageToken.ServiceInstanceId);
							this.invocationId = this.objectFullSyncPageToken.InvocationId;
							break;
						}
						case 3:
						{
							MergePageToken mergePageToken = Microsoft.Exchange.Data.Directory.Sync.MergePageToken.Parse(this.MergePageTokenContext);
							this.ValidateServiceInstance(mergePageToken.ServiceInstanceId);
							this.objectFullSyncPageToken = new ObjectFullSyncPageToken(mergePageToken.InvocationId, this.ObjectIds, this.SyncOptions, mergePageToken.ServiceInstanceId);
							this.invocationId = this.objectFullSyncPageToken.InvocationId;
							break;
						}
						case 4:
							this.objectFullSyncPageToken = Microsoft.Exchange.Data.Directory.Sync.ObjectFullSyncPageToken.Parse(this.ObjectFullSyncPageToken);
							this.ValidateServiceInstance(this.objectFullSyncPageToken.ServiceInstanceId);
							this.invocationId = this.objectFullSyncPageToken.InvocationId;
							break;
						case 5:
						{
							this.ResolveOrganization(false);
							BackSyncCookie backSyncCookie = BackSyncCookie.Parse(this.Cookie);
							this.ValidateServiceInstance(backSyncCookie.ServiceInstanceId);
							this.tenantFullSyncPageToken = new TenantFullSyncPageToken(SyncConfiguration.EnableIgnoreCookieDCDuringTenantFaultin() ? Guid.Empty : backSyncCookie.InvocationId, new Guid(this.tenantFullSyncOrganizationCU.ExternalDirectoryOrganizationId), this.tenantFullSyncOrganizationCU.OrganizationalUnitLink, backSyncCookie.ServiceInstanceId, this.ShouldUseDirSyncBasedTenantFullSync());
							this.invocationId = this.tenantFullSyncPageToken.InvocationId;
							if (!this.ShouldUseDirSyncBasedTenantFullSync() && this.invocationId == Guid.Empty)
							{
								this.invocationId = this.tenantFullSyncPageToken.SelectDomainController(this.currentPartitionId);
							}
							break;
						}
						case 6:
							this.tenantFullSyncPageToken = Microsoft.Exchange.Data.Directory.Sync.TenantFullSyncPageToken.Parse(this.TenantFullSyncPageToken);
							this.ValidateServiceInstance(this.tenantFullSyncPageToken.ServiceInstanceId);
							this.ResolveOrganization(false);
							this.invocationId = this.tenantFullSyncPageToken.InvocationId;
							if (this.invocationId == Guid.Empty)
							{
								this.invocationId = this.tenantFullSyncPageToken.SelectDomainController(this.currentPartitionId);
							}
							break;
						case 7:
							this.mergePageToken = new MergePageToken(this.MergeTenantFullSyncPageToken, this.MergeIncrementalSyncCookie);
							this.ValidateServiceInstance(this.mergePageToken.ServiceInstanceId);
							this.ResolveOrganization(true);
							this.invocationId = this.mergePageToken.InvocationId;
							break;
						case 8:
							this.mergePageToken = Microsoft.Exchange.Data.Directory.Sync.MergePageToken.Parse(this.MergePageToken);
							this.ValidateServiceInstance(this.mergePageToken.ServiceInstanceId);
							this.ResolveOrganization(true);
							this.invocationId = this.mergePageToken.InvocationId;
							if (this.invocationId == Guid.Empty)
							{
								this.invocationId = this.mergePageToken.SelectDomainController(this.currentPartitionId);
							}
							break;
						default:
							goto IL_4EB;
						}
						ExTraceGlobals.BackSyncTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "this.invocationId = {0}", this.invocationId);
						return;
					}
				}
				IL_4EB:
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Not supported parameter set {0}", base.ParameterSetName);
				throw new NotSupportedException("not supported parameter set " + base.ParameterSetName);
			}
			catch
			{
				ExTraceGlobals.BackSyncTracer.TraceError((long)SyncConfiguration.TraceId, "GetMSOSyncData.InternalBeginProcessing encountered exception");
				this.performanceCounterSession.IncrementUserError();
				throw;
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00016F1C File Offset: 0x0001511C
		protected override void InternalStateReset()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "GetMSOSyncData.InternalStateReset entering");
			this.syncConfiguration = this.CreateSyncConfiguration();
			try
			{
				base.InternalStateReset();
			}
			catch (Exception ex)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "GetMSOSyncData.InternalStateReset exception {0}", ex.ToString());
				this.performanceCounterSession.IncrementSystemError();
				Exception ex2 = this.HandleException(ex);
				if (ex2 == ex)
				{
					throw;
				}
				throw ex2;
			}
			finally
			{
				this.ProcessPerformanceCounters();
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00016FB0 File Offset: 0x000151B0
		protected override void InternalProcessRecord()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "GetMSOSyncData.InternalProcessRecord entering");
			try
			{
				base.InternalProcessRecord();
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "GetMSOSyncData.InternalProcessRecord flush ...");
				this.DataProcessor.Flush(new Func<byte[]>(this.syncConfiguration.GetResultCookie), this.syncConfiguration.MoreData);
			}
			catch (Exception ex)
			{
				this.performanceCounterSession.IncrementSystemError();
				Exception ex2 = this.HandleException(ex);
				if (ex2 == ex)
				{
					throw;
				}
				throw ex2;
			}
			finally
			{
				this.ProcessPerformanceCounters();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00017058 File Offset: 0x00015258
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is InvalidCookieException || exception is InvalidCookieServiceInstanceIdException || exception is BackSyncDataSourceTransientException || exception is BackSyncDataSourceUnavailableException || exception is BackSyncDataSourceReplicationException || (this.syncConfiguration != null && this.syncConfiguration.IsKnownException(exception));
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000170B0 File Offset: 0x000152B0
		protected override IConfigDataProvider CreateSession()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Create IConfigurationSession and IRecipientSession ...");
			string text = string.Empty;
			if (this.invocationId != Guid.Empty)
			{
				text = SyncConfiguration.FindDomainControllerByInvocationId(this.invocationId, this.currentPartitionId);
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "CreateSession domainController {0}", text);
			ITopologyConfigurationSession rootOrgConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(text, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.currentPartitionId), 653, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
			ADSessionSettings adsessionSettings = ADSessionSettings.SessionSettingsFactory.Default.FromAllTenantsPartitionId(this.currentPartitionId);
			adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			ITenantConfigurationSession tenantSystemConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(text, true, ConsistencyMode.PartiallyConsistent, null, adsessionSettings, 663, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
			ADSessionSettings adsessionSettings2 = (this.tenantFullSyncOrganizationCU == null) ? ADSessionSettings.FromAllTenantsPartitionId(this.currentPartitionId) : ADSessionSettings.FromOrganizationIdWithoutRbacScopes(this.GetRootOrgId(), this.tenantFullSyncOrganizationCU.OrganizationId, base.ExecutingUserOrganizationId, false, false);
			adsessionSettings2.IncludeSoftDeletedObjects = true;
			adsessionSettings2.ServerSettings.RecipientViewRoot = null;
			adsessionSettings2.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
			ITenantRecipientSession tenantRecipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(false, ConsistencyMode.PartiallyConsistent, adsessionSettings2, 687, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
			tenantRecipientSession.UseGlobalCatalog = false;
			tenantRecipientSession.DomainController = text;
			tenantRecipientSession.LogSizeLimitExceededEvent = false;
			this.syncConfiguration.SetConfiguration(rootOrgConfigurationSession, tenantSystemConfigurationSession, tenantRecipientSession);
			return tenantRecipientSession;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001720C File Offset: 0x0001540C
		protected override IEnumerable<ADRawEntry> GetPagedData()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Get {0} data page", this.syncConfiguration.GetType().Name);
			return this.syncConfiguration.GetDataPage();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017240 File Offset: 0x00015440
		protected override void WriteResult(IConfigurable dataObject)
		{
			ADRawEntry adrawEntry = (ADRawEntry)dataObject;
			ExTraceGlobals.BackSyncTracer.TraceDebug<ADObjectId>((long)SyncConfiguration.TraceId, "GetMSOSyncData:: - Start processing object {0}.", adrawEntry.Id);
			PropertyBag propertyBag = adrawEntry.propertyBag;
			ProcessorHelper.TracePropertBag("GetMSOSyncData::WriteResult", propertyBag);
			this.DataProcessor.Process(propertyBag);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00017290 File Offset: 0x00015490
		private ExchangeConfigurationUnit GetCUForExternalDirectoryOrganizationId(ITenantConfigurationSession tenantConfigSession, string externalDirectoryOrganizationId)
		{
			ExchangeConfigurationUnit exchangeConfigurationUnitByExternalId = tenantConfigSession.GetExchangeConfigurationUnitByExternalId(externalDirectoryOrganizationId);
			if (exchangeConfigurationUnitByExternalId == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorOrganizationNotFound(externalDirectoryOrganizationId));
			}
			return exchangeConfigurationUnitByExternalId;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000172B8 File Offset: 0x000154B8
		private Exception HandleException(Exception exception)
		{
			Exception ex = this.syncConfiguration.HandleException(exception);
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_BackSyncExceptionCaught, new string[]
			{
				this.syncConfigurationMode,
				(ex == null) ? string.Empty : ex.ToString()
			});
			return ex;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017304 File Offset: 0x00015504
		private SyncConfiguration CreateSyncConfiguration()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "GetMSOSyncData.CreateSyncConfiguration entering");
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetMSOSyncData.CreateSyncConfiguration ParameterSetName {0}", base.ParameterSetName);
			string parameterSetName;
			switch (parameterSetName = base.ParameterSetName)
			{
			case "IncrementalSyncParameterSet":
				this.syncConfigurationMode = "IncrementalSync";
				return new IncrementalSyncConfiguration(this.syncCookie, this.invocationId, new OutputResultDelegate(this.WriteCookieAndResponse), new GetMSOSyncData.SyncEventLogger(), new ExcludedObjectReporter());
			case "ObjectFullSyncInitialCallParameterSet":
			case "ObjectFullSyncInitialCallFromTenantFullSyncParameterSet":
			case "ObjectFullSyncInitialCallFromMergeSyncParameterSet":
			case "ObjectFullSyncSubsequentCallParameterSet":
				this.syncConfigurationMode = "ObjectFullSync";
				return new ObjectFullSyncConfiguration(this.objectFullSyncPageToken, this.invocationId, new OutputResultDelegate(this.WriteCookieAndResponse), new GetMSOSyncData.SyncEventLogger(), new FullSyncObjectErrorReporter(this.performanceCounterSession));
			case "TenantFullSyncInitialCallParameterSet":
			case "TenantFullSyncSubsequentCallParameterSet":
				this.syncConfigurationMode = "TenantFullSync";
				if ((base.ParameterSetName == "TenantFullSyncInitialCallParameterSet" && this.ShouldUseDirSyncBasedTenantFullSync()) || (base.ParameterSetName == "TenantFullSyncSubsequentCallParameterSet" && this.tenantFullSyncPageToken.TenantScopedBackSyncCookie != null))
				{
					return new DirSyncBasedTenantFullSyncConfiguration(this.tenantFullSyncPageToken, this.tenantFullSyncOrganizationCU, this.invocationId, new OutputResultDelegate(this.WriteCookieAndResponse), new GetMSOSyncData.SyncEventLogger(), new VerboseObjectErrorReporter(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)));
				}
				return new TenantFullSyncConfiguration(this.tenantFullSyncPageToken, this.invocationId, new OutputResultDelegate(this.WriteCookieAndResponse), new GetMSOSyncData.SyncEventLogger(), new VerboseObjectErrorReporter(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)));
			case "MergeInitialCallParameterSet":
			case "MergeSubsequentCallParameterSet":
				this.syncConfigurationMode = "Merge";
				if (this.mergePageToken.TenantScopedBackSyncCookie != null)
				{
					return new DirSyncBasedMergeConfiguration(this.mergePageToken, this.tenantFullSyncOrganizationCU, this.invocationId, new OutputResultDelegate(this.WriteCookieAndResponse), new GetMSOSyncData.SyncEventLogger(), new VerboseObjectErrorReporter(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)), this.currentPartitionId);
				}
				return new MergeConfiguration(this.mergePageToken, this.invocationId, new OutputResultDelegate(this.WriteCookieAndResponse), new GetMSOSyncData.SyncEventLogger(), new VerboseObjectErrorReporter(new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)), this.currentPartitionId);
			}
			throw new NotSupportedException("not supported parameter set " + base.ParameterSetName);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000175DC File Offset: 0x000157DC
		private IDataProcessor CreateDataProcessor()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "GetMSOSyncData.CreateDataProcessor entering");
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetMSOSyncData.CreateDataProcessor ParameterSetName {0}", base.ParameterSetName);
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6000550-1 == null)
				{
					<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6000550-1 = new Dictionary<string, int>(9)
					{
						{
							"IncrementalSyncParameterSet",
							0
						},
						{
							"ObjectFullSyncInitialCallParameterSet",
							1
						},
						{
							"ObjectFullSyncInitialCallFromTenantFullSyncParameterSet",
							2
						},
						{
							"ObjectFullSyncInitialCallFromMergeSyncParameterSet",
							3
						},
						{
							"ObjectFullSyncSubsequentCallParameterSet",
							4
						},
						{
							"TenantFullSyncInitialCallParameterSet",
							5
						},
						{
							"TenantFullSyncSubsequentCallParameterSet",
							6
						},
						{
							"MergeInitialCallParameterSet",
							7
						},
						{
							"MergeSubsequentCallParameterSet",
							8
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{53462315-70A3-48A4-9A87-A128789A2C41}.$$method0x6000550-1.TryGetValue(parameterSetName, out num))
				{
					IDataProcessor dataProcessor;
					switch (num)
					{
					case 0:
						dataProcessor = this.CreateIncrementalSyncDataProcessor(false);
						break;
					case 1:
					case 2:
					case 3:
					case 4:
						dataProcessor = this.CreateObjectFullSyncDataProcessor();
						break;
					case 5:
						dataProcessor = (this.ShouldUseDirSyncBasedTenantFullSync() ? this.CreateIncrementalSyncDataProcessor(true) : this.CreateFullSyncDataProcessor(null));
						break;
					case 6:
						dataProcessor = ((this.tenantFullSyncPageToken.TenantScopedBackSyncCookie != null) ? this.CreateIncrementalSyncDataProcessor(true) : this.CreateFullSyncDataProcessor(null));
						break;
					case 7:
					case 8:
						dataProcessor = ((this.mergePageToken.TenantScopedBackSyncCookie != null) ? this.CreateIncrementalSyncDataProcessor(true) : this.CreateFullSyncDataProcessor(null));
						break;
					default:
						goto IL_170;
					}
					ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetMSOSyncData: Data processor has been created {0}", dataProcessor.GetType().Name);
					return dataProcessor;
				}
			}
			IL_170:
			throw new NotSupportedException("not supported parameter set " + base.ParameterSetName);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00017790 File Offset: 0x00015990
		private PerformanceCounterSession CreatePerformanceCounterSession()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Create performance counter session for parameter set {0}", base.ParameterSetName);
			string parameterSetName;
			switch (parameterSetName = base.ParameterSetName)
			{
			case "IncrementalSyncParameterSet":
				return new IncrementalSyncPerformanceCounterSession(GetMSOSyncData.enablePerformanceCounters);
			case "ObjectFullSyncInitialCallParameterSet":
			case "ObjectFullSyncSubsequentCallParameterSet":
			case "ObjectFullSyncInitialCallFromTenantFullSyncParameterSet":
			case "ObjectFullSyncInitialCallFromMergeSyncParameterSet":
				return new ObjectFullSyncPerformanceCounterSession(GetMSOSyncData.enablePerformanceCounters);
			case "TenantFullSyncInitialCallParameterSet":
			case "TenantFullSyncSubsequentCallParameterSet":
			case "MergeInitialCallParameterSet":
			case "MergeSubsequentCallParameterSet":
				return new TenantFullSyncPerformanceCounterSession(GetMSOSyncData.enablePerformanceCounters);
			}
			throw new NotSupportedException("not supported parameter set " + base.ParameterSetName);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000178BC File Offset: 0x00015ABC
		private void ProcessPerformanceCounters()
		{
			if (base.ParameterSetName == "IncrementalSyncParameterSet")
			{
				bool sameCookie = false;
				object obj;
				if (base.SessionState.Variables.TryGetValue("BackSyncLastCookie", out obj))
				{
					sameCookie = this.IsCookieParameterSameAs(obj as byte[]);
				}
				base.SessionState.Variables["BackSyncLastCookie"] = this.Cookie;
				this.performanceCounterSession.ReportSameCookie(sameCookie);
			}
			this.performanceCounterSession.Finish();
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00017938 File Offset: 0x00015B38
		private bool IsCookieParameterSameAs(byte[] cookie)
		{
			if (this.Cookie == null || this.Cookie.Length == 0 || cookie == null)
			{
				return false;
			}
			if (this.Cookie.Length != cookie.Length)
			{
				return false;
			}
			for (int i = 0; i < this.Cookie.Length; i++)
			{
				if (this.Cookie[i] != cookie[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001798E File Offset: 0x00015B8E
		private void WriteCookieAndResponse(byte[] serializedCookie, object response)
		{
			this.WriteResultWithAuditLog(new SyncData(serializedCookie, response));
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000179A0 File Offset: 0x00015BA0
		private void WriteResultWithAuditLog(IConfigurable dataObject)
		{
			base.WriteResult(dataObject);
			if (BackSyncAuditLog.IsEnabled)
			{
				BackSyncAuditLog.Instance.Append(base.ExecutingUserIdentityName, this.Cookie, this.GetParametersExceptCookie(), ((SyncData)dataObject).Response, this.syncConfiguration.ErrorSyncObjects);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000179F0 File Offset: 0x00015BF0
		private NameValueCollection GetParametersExceptCookie()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (object obj in base.Fields.Keys)
			{
				string text = (string)obj;
				if (text != "Cookie")
				{
					object obj2 = base.Fields[text];
					string a;
					if ((a = text) != null)
					{
						if (!(a == "SyncOptions"))
						{
							if (a == "ObjectIds")
							{
								SyncObjectId[] array = obj2 as SyncObjectId[];
								if (array != null)
								{
									foreach (SyncObjectId syncObjectId in array)
									{
										nameValueCollection.Add("ObjectIds", syncObjectId.ToString());
									}
									continue;
								}
								continue;
							}
						}
						else
						{
							BackSyncOptions backSyncOptions = (BackSyncOptions)obj2;
							if (backSyncOptions != BackSyncOptions.None)
							{
								nameValueCollection.Add("SyncOptions", backSyncOptions.ToString());
								continue;
							}
							continue;
						}
					}
					if (obj2 is byte[])
					{
						nameValueCollection.Add(text, Convert.ToBase64String((byte[])obj2));
					}
					else
					{
						nameValueCollection.Add(text, obj2.ToString());
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00017B48 File Offset: 0x00015D48
		private IDataProcessor CreateFullSyncDataProcessor(OrganizationLookup organizationLookup = null)
		{
			FullSyncConfiguration config = (FullSyncConfiguration)this.syncConfiguration;
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "CreateFullSyncDataProcessor");
			ExcludedObjectReporter reporter = (ExcludedObjectReporter)config.ExcludedObjectReporter;
			LinkTargetPropertyLookup linkTargetPropertyLookup = new LinkTargetPropertyLookup(new Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]>(config.GetProperties));
			PagedOutputResultWriter pagedOutputResultWriter = new PagedOutputResultWriter(new WriteResultDelegate(this.WriteResultWithAuditLog), (IEnumerable<SyncObject> objects, bool moreData, byte[] cookie, ServiceInstanceId serviceInstance) => SyncObject.CreateGetDirectoryObjectsResponse(objects, moreData, cookie, config.GetReportedErrors(), serviceInstance), null, new AddErrorSyncObjectDelegate(config.AddErrorSyncObject), this.ServiceInstance);
			IDataProcessor next = pagedOutputResultWriter;
			if (GetMSOSyncData.IsTenantRelocationSupportEnabled())
			{
				if (organizationLookup == null)
				{
					QueryFilter managementScopeFilter = GetMSOSyncData.GetManagementScopeFilter();
					organizationLookup = new OrganizationLookup(new Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]>(config.GetOrganizationProperties), managementScopeFilter);
				}
				next = new TenantRelocationProcessor(next, organizationLookup, reporter, new GetTenantRelocationStateDelegate(GetMSOSyncData.GetTenantRelocationState), config.InvocationId, false);
			}
			NeverSetAttributesFilter next2 = new NeverSetAttributesFilter(next);
			PropertyReferenceTargetMissingPropertyResolver next3 = new PropertyReferenceTargetMissingPropertyResolver(next2, linkTargetPropertyLookup);
			LinkTargetMissingPropertyResolver next4 = new LinkTargetMissingPropertyResolver(next3, linkTargetPropertyLookup);
			BatchLookup next5 = new BatchLookup(next4, linkTargetPropertyLookup);
			RecipientTypeSpecificPropertyFilter recipientTypeSpecificPropertyFilter = new RecipientTypeSpecificPropertyFilter(next5, GetMSOSyncData.PropertyFilterMap);
			next = recipientTypeSpecificPropertyFilter;
			if (SyncConfiguration.InlcudeLinks(config.FullSyncPageToken.SyncOptions))
			{
				next = new Metadata2LinkTranslator(recipientTypeSpecificPropertyFilter);
			}
			RecipientTypeDetails acceptedRecipientTypes = RecipientTaskHelper.GetAcceptedRecipientTypes();
			return new RecipientTypeFilter(next, acceptedRecipientTypes, reporter);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00017CA0 File Offset: 0x00015EA0
		private static TenantRelocationState GetTenantRelocationState(ADObjectId tenantOUId, out bool isSourceTenant, bool readThrough)
		{
			TenantRelocationState tenantRelocationState = TenantRelocationStateCache.GetTenantRelocationState(tenantOUId.Name, tenantOUId.GetPartitionId(), out isSourceTenant, readThrough);
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "GetTenantRelocationState:: - {0} relocation state for tenant {1} is: isSource={2}, sourceForest={3}, sourceState={4}, targetForest={5}, targetState={6}.", new object[]
			{
				readThrough ? "Latest" : "Cached",
				tenantOUId,
				isSourceTenant,
				tenantRelocationState.SourceForestFQDN,
				tenantRelocationState.SourceForestState,
				tenantRelocationState.TargetForestFQDN,
				tenantRelocationState.TargetForestState
			});
			return tenantRelocationState;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00017D30 File Offset: 0x00015F30
		private IDataProcessor CreateObjectFullSyncDataProcessor()
		{
			ObjectFullSyncConfiguration objectFullSyncConfiguration = (ObjectFullSyncConfiguration)this.syncConfiguration;
			QueryFilter managementScopeFilter = GetMSOSyncData.GetManagementScopeFilter();
			OrganizationLookup organizationLookup = new OrganizationLookup(new Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]>(objectFullSyncConfiguration.GetOrganizationProperties), managementScopeFilter);
			RecipientTypeDetails acceptedRecipientTypes = RecipientTaskHelper.GetAcceptedRecipientTypes();
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "CreateObjectFullSyncDataProcessor acceptedRecipientTypes = {0}", acceptedRecipientTypes.ToString());
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "CreateObjectFullSyncDataProcessor orgQueryFilter = {0}", (managementScopeFilter != null) ? managementScopeFilter.ToString() : "NULL");
			IDataProcessor dataProcessor = this.CreateFullSyncDataProcessor(organizationLookup);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "ObjectFullSyncConfiguration.CreateDataProcessor baseProcessor {0}", dataProcessor.GetType().Name);
			ExcludedObjectReporter reporter = (ExcludedObjectReporter)objectFullSyncConfiguration.ExcludedObjectReporter;
			RecipientDeletedDuringOrganizationDeletionFilter next = new RecipientDeletedDuringOrganizationDeletionFilter(dataProcessor, organizationLookup, reporter);
			OrganizationFilter next2 = new OrganizationFilter(next, organizationLookup, reporter, false);
			return new BatchLookup(next2, organizationLookup);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00017E1C File Offset: 0x0001601C
		private IDataProcessor CreateIncrementalSyncDataProcessor(bool isDirSyncBasedTenantFullSync = false)
		{
			IncrementalSyncConfiguration incrementalSyncConfiguration = (IncrementalSyncConfiguration)this.syncConfiguration;
			QueryFilter managementScopeFilter = GetMSOSyncData.GetManagementScopeFilter();
			RecipientTypeDetails acceptedRecipientTypes = RecipientTaskHelper.GetAcceptedRecipientTypes();
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "CreateIncrementalSyncDataProcessor acceptedRecipientTypes = {0}", acceptedRecipientTypes.ToString());
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "CreateIncrementalSyncDataProcessor orgQueryFilter = {0}", (managementScopeFilter != null) ? managementScopeFilter.ToString() : "NULL");
			Dictionary<ADObjectId, ADRawEntry> propertyCache = new Dictionary<ADObjectId, ADRawEntry>();
			ObjectPropertyLookup objectPropertyLookup = new ObjectPropertyLookup(new Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]>(incrementalSyncConfiguration.GetProperties), propertyCache);
			LinkTargetPropertyLookup linkTargetPropertyLookup = new LinkTargetPropertyLookup(new Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]>(incrementalSyncConfiguration.GetProperties), propertyCache);
			OrganizationLookup organizationLookup = new OrganizationLookup(new Func<ADObjectId[], PropertyDefinition[], Result<ADRawEntry>[]>(incrementalSyncConfiguration.GetOrganizationProperties), managementScopeFilter);
			ExcludedObjectReporter reporter = (ExcludedObjectReporter)incrementalSyncConfiguration.ExcludedObjectReporter;
			PagedOutputResultWriter pagedOutputResultWriter;
			if (isDirSyncBasedTenantFullSync)
			{
				pagedOutputResultWriter = new PagedOutputResultWriter(new WriteResultDelegate(this.WriteResultWithAuditLog), (IEnumerable<SyncObject> objects, bool moreData, byte[] cookie, ServiceInstanceId serviceInstance) => SyncObject.CreateGetDirectoryObjectsResponse(objects, moreData, cookie, new DirectoryObjectError[0], serviceInstance), new Action<int>(this.performanceCounterSession.ReportChangeCount), new AddErrorSyncObjectDelegate(incrementalSyncConfiguration.AddErrorSyncObject), this.ServiceInstance);
			}
			else
			{
				pagedOutputResultWriter = new PagedOutputResultWriter(new WriteResultDelegate(this.WriteResultWithAuditLog), new Func<IEnumerable<SyncObject>, bool, byte[], ServiceInstanceId, object>(SyncObject.CreateGetChangesResponse), new Action<int>(this.performanceCounterSession.ReportChangeCount), new AddErrorSyncObjectDelegate(incrementalSyncConfiguration.AddErrorSyncObject), this.ServiceInstance);
			}
			IDataProcessor next = pagedOutputResultWriter;
			bool flag = GetMSOSyncData.IsTenantRelocationSupportEnabled();
			if (flag)
			{
				next = new TenantRelocationProcessor(next, organizationLookup, reporter, new GetTenantRelocationStateDelegate(GetMSOSyncData.GetTenantRelocationState), incrementalSyncConfiguration.InvocationId, true);
			}
			PropertyReferenceTargetMissingPropertyResolver next2 = new PropertyReferenceTargetMissingPropertyResolver(next, linkTargetPropertyLookup);
			LinkTargetMissingPropertyResolver next3 = new LinkTargetMissingPropertyResolver(next2, linkTargetPropertyLookup);
			BatchLookup next4 = new BatchLookup(next3, linkTargetPropertyLookup);
			RecipientTypeSpecificPropertyFilter next5 = new RecipientTypeSpecificPropertyFilter(next4, GetMSOSyncData.PropertyFilterMap);
			RecipientTypeFilter next6 = new RecipientTypeFilter(next5, acceptedRecipientTypes, reporter);
			RecipientDeletedDuringOrganizationDeletionFilter next7 = new RecipientDeletedDuringOrganizationDeletionFilter(next6, organizationLookup, reporter);
			MissingPropertyResolver missingPropertyResolver = new MissingPropertyResolver(next7, objectPropertyLookup);
			incrementalSyncConfiguration.MissingPropertyResolver = missingPropertyResolver;
			BatchLookup next8 = new BatchLookup(missingPropertyResolver, objectPropertyLookup);
			OrganizationFilter next9 = new OrganizationFilter(next8, organizationLookup, reporter, flag);
			BatchLookup next10 = new BatchLookup(next9, organizationLookup);
			return new IncludedBackIntoBacksyncDetector(next10, this.ServiceInstance);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00018034 File Offset: 0x00016234
		private static QueryFilter GetManagementScopeFilter()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1211, "GetManagementScopeFilter", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
			string text = "MSOnlinePartnerScope";
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetManagementScopeFilter partnerScopeName {0}", text);
			QueryFilter queryFilter = null;
			ManagementScope managementScope = topologyConfigurationSession.ReadRootOrgManagementScopeByName(text);
			if (managementScope == null)
			{
				ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Scope not found for name {0}", text);
				throw new ADExternalException(Strings.ErrorScopeNotFound(text));
			}
			ScopeRestrictionType scopeRestrictionType = managementScope.ScopeRestrictionType;
			string arg;
			RBACHelper.TryConvertPowershellFilterIntoQueryFilter(managementScope.Filter, scopeRestrictionType, null, out queryFilter, out arg);
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetManagementScopeFilter orgQueryFilter {0}", (queryFilter != null) ? queryFilter.ToString() : "NULL");
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "GetManagementScopeFilter errorString {0}", arg);
			return queryFilter;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00018108 File Offset: 0x00016308
		private void ValidateServiceInstance(ServiceInstanceId cookieServiceInstanceId)
		{
			if (!this.ServiceInstance.Equals(cookieServiceInstanceId))
			{
				ExTraceGlobals.BackSyncTracer.TraceError((long)SyncConfiguration.TraceId, "Cookie ServiceInstanceId and parameter ServiceInstanceId are different");
				base.WriteError(new CookieAndParameterServiceInstanceIdMismatchException(cookieServiceInstanceId.ToString(), this.ServiceInstance.ToString()), ErrorCategory.InvalidArgument, cookieServiceInstanceId);
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00018158 File Offset: 0x00016358
		private bool ShouldUseDirSyncBasedTenantFullSync()
		{
			if (base.ParameterSetName != "TenantFullSyncInitialCallParameterSet")
			{
				ExTraceGlobals.BackSyncTracer.TraceError((long)SyncConfiguration.TraceId, "this.ParameterSetName != TenantFullSyncInitialCallParameterSet");
				throw new InvalidOperationException("ParameterSetName");
			}
			if (this.dirSyncBasedTenantFullSyncThreshold < 0L)
			{
				return false;
			}
			if (this.currentTenantSize < 0L)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, this.tenantFullSyncOrganizationCU.OrganizationId, base.ExecutingUserOrganizationId, false);
				IConfigurationSession configSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 1282, "ShouldUseDirSyncBasedTenantFullSync", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
				this.currentTenantSize = GetMSOSyncData.GetTenantSize(configSession, this.tenantFullSyncOrganizationCU.OrganizationId);
				ExTraceGlobals.BackSyncTracer.TraceDebug<OrganizationId, long>((long)SyncConfiguration.TraceId, "ShouldUseDirSyncBasedTenantFullSync: Organization: {0} Size: {1}.", this.tenantFullSyncOrganizationCU.OrganizationId, this.currentTenantSize);
			}
			return this.currentTenantSize > this.dirSyncBasedTenantFullSyncThreshold;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00018234 File Offset: 0x00016434
		private static PartitionId GetPartitionIdFromServiceInstance(ServiceInstanceId serviceInstance)
		{
			PartitionId partitionId = null;
			Exception ex;
			if (PartitionId.TryParse(ServiceInstanceId.GetShortName(serviceInstance.InstanceId), out partitionId, out ex))
			{
				partitionId = (ADAccountPartitionLocator.IsKnownPartition(partitionId) ? partitionId : null);
			}
			if (null == partitionId)
			{
				string serviceInstanceId = ServiceInstanceId.GetServiceInstanceId(TopologyProvider.LocalForestFqdn);
				if (serviceInstanceId.Equals(serviceInstance.InstanceId, StringComparison.InvariantCultureIgnoreCase))
				{
					partitionId = PartitionId.LocalForest;
					partitionId = (ADAccountPartitionLocator.IsKnownPartition(partitionId) ? partitionId : null);
				}
			}
			if (null == partitionId)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1334, "GetPartitionIdFromServiceInstance", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
				topologyConfigurationSession.UseConfigNC = false;
				ITopologyConfigurationSession topologyConfigurationSession2 = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 1338, "GetPartitionIdFromServiceInstance", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\BackSync\\GetMSOSyncData.cs");
				ServiceInstanceIdParameter serviceInstanceIdParameter = new ServiceInstanceIdParameter(serviceInstance);
				IEnumerable<SyncServiceInstance> objects = serviceInstanceIdParameter.GetObjects<SyncServiceInstance>(SyncServiceInstance.GetMsoSyncRootContainer(), topologyConfigurationSession);
				using (IEnumerator<SyncServiceInstance> enumerator = objects.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						SyncServiceInstance syncServiceInstance = enumerator.Current;
						if (enumerator.MoveNext())
						{
							throw new ManagementObjectAmbiguousException(Strings.ErrorManagementObjectAmbiguous(serviceInstanceIdParameter.ToString()));
						}
						if (syncServiceInstance != null && !ADObjectId.IsNullOrEmpty(syncServiceInstance.AccountPartition))
						{
							ADObjectId accountPartition = syncServiceInstance.AccountPartition;
							AccountPartition accountPartition2 = topologyConfigurationSession2.Read<AccountPartition>(accountPartition);
							if (accountPartition2 != null)
							{
								partitionId = accountPartition2.PartitionId;
								partitionId = (ADAccountPartitionLocator.IsKnownPartition(partitionId) ? partitionId : null);
							}
						}
					}
				}
			}
			return partitionId;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001839C File Offset: 0x0001659C
		private static long GetTenantSize(IConfigurationSession configSession, OrganizationId organizationId)
		{
			long num = 0L;
			num += (long)SystemAddressListMemberCount.GetCount(configSession, organizationId, "All Mailboxes(VLV)", false);
			num += (long)SystemAddressListMemberCount.GetCount(configSession, organizationId, "All Mail Users(VLV)", false);
			num += (long)SystemAddressListMemberCount.GetCount(configSession, organizationId, "All Contacts(VLV)", false);
			return num + (long)SystemAddressListMemberCount.GetCount(configSession, organizationId, "All Groups(VLV)", false);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000183F1 File Offset: 0x000165F1
		private static bool IsTenantRelocationSupportEnabled()
		{
			return SyncConfiguration.GetConfigurationValue<int>("TenantRelocationEnabled", 1) == 1;
		}

		// Token: 0x040002A0 RID: 672
		private const string LastCookie = "BackSyncLastCookie";

		// Token: 0x040002A1 RID: 673
		private const string MSOnlinePartnerScope = "MSOnlinePartnerScope";

		// Token: 0x040002A2 RID: 674
		private const string IncrementalSyncParameterSet = "IncrementalSyncParameterSet";

		// Token: 0x040002A3 RID: 675
		private const string ObjectFullSyncInitialCallParameterSet = "ObjectFullSyncInitialCallParameterSet";

		// Token: 0x040002A4 RID: 676
		private const string ObjectFullSyncInitialCallFromTenantFullSyncParameterSet = "ObjectFullSyncInitialCallFromTenantFullSyncParameterSet";

		// Token: 0x040002A5 RID: 677
		private const string ObjectFullSyncInitialCallFromMergeSyncParameterSet = "ObjectFullSyncInitialCallFromMergeSyncParameterSet";

		// Token: 0x040002A6 RID: 678
		private const string ObjectFullSyncSubsequentCallParameterSet = "ObjectFullSyncSubsequentCallParameterSet";

		// Token: 0x040002A7 RID: 679
		private const string TenantFullSyncInitialCallParameterSet = "TenantFullSyncInitialCallParameterSet";

		// Token: 0x040002A8 RID: 680
		private const string TenantFullSyncSubsequentCallParameterSet = "TenantFullSyncSubsequentCallParameterSet";

		// Token: 0x040002A9 RID: 681
		private const string MergeInitialCallParameterSet = "MergeInitialCallParameterSet";

		// Token: 0x040002AA RID: 682
		private const string MergeSubsequentCallParameterSet = "MergeSubsequentCallParameterSet";

		// Token: 0x040002AB RID: 683
		private const string CookieParamName = "Cookie";

		// Token: 0x040002AC RID: 684
		private const string ObjectIdsName = "ObjectIds";

		// Token: 0x040002AD RID: 685
		private const string OrganizationName = "Organization";

		// Token: 0x040002AE RID: 686
		private const string SyncOptionsName = "SyncOptions";

		// Token: 0x040002AF RID: 687
		private const string ObjectFullSyncPageTokenName = "ObjectFullSyncPageToken";

		// Token: 0x040002B0 RID: 688
		private const string TenantFullSyncPageTokenName = "TenantFullSyncPageToken";

		// Token: 0x040002B1 RID: 689
		private const string TenantFullSyncPageTokenContextName = "TenantFullSyncPageTokenContext";

		// Token: 0x040002B2 RID: 690
		private const string MergePageTokenName = "MergePageToken";

		// Token: 0x040002B3 RID: 691
		private const string MergePageTokenContextName = "MergePageTokenContext";

		// Token: 0x040002B4 RID: 692
		private const string MergeTenantFullSyncPageTokenName = "MergeTenantFullSyncPageToken";

		// Token: 0x040002B5 RID: 693
		private const string MergeIncrementalSyncCookieName = "MergeIncrementalSyncCookie";

		// Token: 0x040002B6 RID: 694
		private const string ServiceInstanceParamName = "ServiceInstance";

		// Token: 0x040002B7 RID: 695
		private static readonly IDictionary<ADPropertyDefinition, RecipientTypeDetails> PropertyFilterMap = new Dictionary<ADPropertyDefinition, RecipientTypeDetails>
		{
			{
				SyncUserSchema.CloudMsExchBlockedSendersHash,
				RecipientTypeDetails.UserMailbox
			},
			{
				SyncUserSchema.CloudMsExchSafeRecipientsHash,
				RecipientTypeDetails.UserMailbox
			},
			{
				SyncUserSchema.CloudMsExchSafeSendersHash,
				RecipientTypeDetails.UserMailbox
			},
			{
				SyncUserSchema.CloudMsExchUCVoiceMailSettings,
				RecipientTypeDetails.UserMailbox | RecipientTypeDetails.MailUser
			},
			{
				SyncUserSchema.ServiceOriginatedResource,
				RecipientTypeDetails.UserMailbox | RecipientTypeDetails.MailUser
			}
		};

		// Token: 0x040002B8 RID: 696
		private static ManagementScope msOnlineScope;

		// Token: 0x040002B9 RID: 697
		private static bool enablePerformanceCounters = true;

		// Token: 0x040002BA RID: 698
		private string syncConfigurationMode;

		// Token: 0x040002BB RID: 699
		private SyncConfiguration syncConfiguration;

		// Token: 0x040002BC RID: 700
		private IDataProcessor dataProcessor;

		// Token: 0x040002BD RID: 701
		private BackSyncCookie syncCookie;

		// Token: 0x040002BE RID: 702
		private ObjectFullSyncPageToken objectFullSyncPageToken;

		// Token: 0x040002BF RID: 703
		private TenantFullSyncPageToken tenantFullSyncPageToken;

		// Token: 0x040002C0 RID: 704
		private MergePageToken mergePageToken;

		// Token: 0x040002C1 RID: 705
		private Guid invocationId;

		// Token: 0x040002C2 RID: 706
		private PartitionId currentPartitionId;

		// Token: 0x040002C3 RID: 707
		private PerformanceCounterSession performanceCounterSession;

		// Token: 0x040002C4 RID: 708
		private ExchangeConfigurationUnit tenantFullSyncOrganizationCU;

		// Token: 0x040002C5 RID: 709
		private long dirSyncBasedTenantFullSyncThreshold;

		// Token: 0x040002C6 RID: 710
		private long currentTenantSize = -1L;

		// Token: 0x020000A6 RID: 166
		internal class SyncEventLogger : ISyncEventLogger
		{
			// Token: 0x0600059A RID: 1434 RVA: 0x00018414 File Offset: 0x00016614
			public void LogSerializationFailedEvent(string objectId, int errorCount)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_BackSyncExcludeFromBackSync, new string[]
				{
					objectId,
					errorCount.ToString()
				});
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x00018444 File Offset: 0x00016644
			public void LogTooManyObjectReadRestartsEvent(string objectId, int pagedLinkReadRestartsLimit)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_BackSyncTooManyObjectReadRestarts, new string[]
				{
					objectId,
					pagedLinkReadRestartsLimit.ToString()
				});
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x00018474 File Offset: 0x00016674
			public void LogFullSyncFallbackDetectedEvent(BackSyncCookie previousCookie, BackSyncCookie currentCookie)
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_BackSyncFullSyncFailbackDetected, new string[]
				{
					previousCookie.LastWhenChanged.ToString(),
					currentCookie.LastWhenChanged.ToString(),
					previousCookie.InvocationId.ToString(),
					currentCookie.InvocationId.ToString(),
					Convert.ToBase64String(previousCookie.ToByteArray()),
					Convert.ToBase64String(currentCookie.ToByteArray())
				});
			}
		}
	}
}
