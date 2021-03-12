using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x0200001A RID: 26
	public abstract class GetSubscriptionBase<TSubscription> : GetTenantADObjectWithIdentityTaskBase<AggregationSubscriptionIdParameter, TSubscription> where TSubscription : IConfigurable, new()
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006ED2 File Offset: 0x000050D2
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006EE9 File Offset: 0x000050E9
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006EFC File Offset: 0x000050FC
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006F22 File Offset: 0x00005122
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter IncludeReport
		{
			get
			{
				return (SwitchParameter)(base.Fields[GetSubscriptionBase<TSubscription>.ParameterIncludeReport] ?? false);
			}
			set
			{
				base.Fields[GetSubscriptionBase<TSubscription>.ParameterIncludeReport] = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006F3A File Offset: 0x0000513A
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006F42 File Offset: 0x00005142
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006F4B File Offset: 0x0000514B
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00006F62 File Offset: 0x00005162
		[Parameter(Mandatory = false)]
		public AggregationType AggregationType
		{
			get
			{
				return (AggregationType)base.Fields["AggregationType"];
			}
			set
			{
				base.Fields["AggregationType"] = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00006F7A File Offset: 0x0000517A
		protected override Unlimited<uint> DefaultResultSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FC RID: 252
		protected abstract AggregationSubscriptionType IdentityType { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006F81 File Offset: 0x00005181
		protected virtual AggregationType AggregationTypeValue
		{
			get
			{
				if (!base.Fields.IsModified("AggregationType"))
				{
					return AggregationType.Aggregation;
				}
				return this.AggregationType;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006FA0 File Offset: 0x000051A0
		protected override void WriteResult(IConfigurable dataObject)
		{
			PimSubscriptionProxy pimSubscriptionProxy = dataObject as PimSubscriptionProxy;
			if (pimSubscriptionProxy != null)
			{
				pimSubscriptionProxy.NeedSuppressingPiiData = base.NeedSuppressingPiiData;
			}
			base.WriteResult(dataObject);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006FCC File Offset: 0x000051CC
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 130, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Mobility\\Aggregation\\GetSubscriptionBase.cs");
			if (this.Mailbox == null)
			{
				if (this.Identity != null && this.Identity.MailboxIdParameter != null)
				{
					this.Mailbox = this.Identity.MailboxIdParameter;
				}
				else
				{
					ADObjectId adObjectId;
					if (!base.TryGetExecutingUserId(out adObjectId))
					{
						throw new ExecutingUserPropertyNotFoundException("executingUserid");
					}
					this.Mailbox = new MailboxIdParameter(adObjectId);
				}
			}
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.Mailbox, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorUserNotFound(this.Mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(this.Mailbox.ToString())));
			IRecipientSession session = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(tenantOrRootOrgRecipientSession, aduser.OrganizationId, true);
			AggregationSubscriptionDataProvider aggregationSubscriptionDataProvider = null;
			try
			{
				aggregationSubscriptionDataProvider = SubscriptionConfigDataProviderFactory.Instance.CreateSubscriptionDataProvider(this.AggregationTypeValue, AggregationTaskType.Get, session, aduser);
				aggregationSubscriptionDataProvider.LoadReport = this.IncludeReport;
			}
			catch (MailboxFailureException exception)
			{
				this.WriteDebugInfoAndError(exception, ErrorCategory.InvalidArgument, this.Mailbox);
			}
			return aggregationSubscriptionDataProvider;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000070FC File Offset: 0x000052FC
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			base.InternalStateReset();
			this.identityDefined = (this.Identity != null);
			if (!this.identityDefined)
			{
				this.Identity = new AggregationSubscriptionIdParameter();
			}
			this.Identity.SubscriptionType = new AggregationSubscriptionType?(this.IdentityType);
			this.Identity.AggregationType = new AggregationType?(this.AggregationTypeValue);
			if (base.Fields.IsModified("SubscriptionType") && !base.Fields.IsModified("AggregationType"))
			{
				this.Identity.AggregationType = new AggregationType?(AggregationType.All);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000071B4 File Offset: 0x000053B4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			try
			{
				if (this.identityDefined && this.Identity.IsUniqueIdentity)
				{
					TSubscription tsubscription = (TSubscription)((object)base.GetDataObject(this.Identity));
					PimSubscriptionProxy pimSubscriptionProxy = tsubscription as PimSubscriptionProxy;
					if (pimSubscriptionProxy != null)
					{
						pimSubscriptionProxy.SetDebug(base.IsDebugOn || base.IsVerboseOn);
					}
					this.WriteResult(tsubscription);
				}
				else
				{
					LocalizedString? localizedString;
					IEnumerable<TSubscription> dataObjects = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString);
					foreach (TSubscription tsubscription2 in dataObjects)
					{
						PimSubscriptionProxy pimSubscriptionProxy2 = tsubscription2 as PimSubscriptionProxy;
						if (pimSubscriptionProxy2 != null)
						{
							pimSubscriptionProxy2.SetDebug(base.IsDebugOn || base.IsVerboseOn);
						}
					}
					this.WriteResult<TSubscription>(dataObjects);
					if (!base.HasErrors && base.WriteObjectCount == 0U && localizedString != null)
					{
						this.WriteDebugInfoAndError(new ManagementObjectNotFoundException(localizedString.Value), ErrorCategory.InvalidData, null);
					}
				}
			}
			finally
			{
				this.WriteDebugInfo();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007300 File Offset: 0x00005500
		protected void WriteDebugInfoAndError(Exception exception, ErrorCategory category, object target)
		{
			this.WriteDebugInfo();
			base.WriteError(exception, category, target);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007311 File Offset: 0x00005511
		protected void WriteDebugInfo()
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(CommonLoggingHelper.SyncLogSession.GetBlackBoxText());
			}
			CommonLoggingHelper.SyncLogSession.ClearBlackBox();
		}

		// Token: 0x04000071 RID: 113
		public static readonly string ParameterIncludeReport = "IncludeReport";

		// Token: 0x04000072 RID: 114
		private bool identityDefined;
	}
}
