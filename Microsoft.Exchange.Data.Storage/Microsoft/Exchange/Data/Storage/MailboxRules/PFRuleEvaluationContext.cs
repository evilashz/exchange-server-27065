using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000BEF RID: 3055
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class PFRuleEvaluationContext : RuleEvaluationContextBase
	{
		// Token: 0x06006D02 RID: 27906 RVA: 0x001D1D58 File Offset: 0x001CFF58
		static PFRuleEvaluationContext()
		{
			string fqdn = LocalServer.GetServer().Fqdn;
			PFRuleEvaluationContext.localServerFqdn = fqdn;
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
				PFRuleEvaluationContext.localServerNetworkAddress = hostEntry.AddressList[0];
			}
			catch (SocketException ex)
			{
				ExTraceGlobals.SessionTracer.TraceError<string>(0L, "Start failed: {0}", ex.ToString());
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_PFRuleConfigGetLocalIPFailure, null, new object[]
				{
					ex
				});
				throw new InvalidRuleException(ex.Message, ex);
			}
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x001D1E3C File Offset: 0x001D003C
		protected PFRuleEvaluationContext(Folder folder, ICoreItem message, StoreSession session, ProxyAddress recipient, IADRecipientCache recipientCache, long mimeSize) : base(folder, null, session, recipient, recipientCache, mimeSize, PFRuleConfig.Instance, ExTraceGlobals.SessionTracer)
		{
			AcrPropertyBag acrPropertyBag = message.PropertyBag as AcrPropertyBag;
			if (acrPropertyBag != null)
			{
				this.messageContextStoreObject = acrPropertyBag.Context.StoreObject;
			}
			base.Message = new MessageItem(message, false);
			this.ruleHistory = base.Message.GetRuleHistory();
			base.LimitChecker = new LimitChecker(this);
			this.ruleConfig = PFRuleConfig.Instance;
		}

		// Token: 0x06006D04 RID: 27908 RVA: 0x001D1EB7 File Offset: 0x001D00B7
		protected PFRuleEvaluationContext(PFRuleEvaluationContext parentContext) : base(parentContext)
		{
		}

		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x06006D05 RID: 27909 RVA: 0x001D1EC0 File Offset: 0x001D00C0
		public override string DefaultDomainName
		{
			get
			{
				if (this.defaultDomainName == null)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 189, "DefaultDomainName", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\Rules\\MailboxRules\\PFRuleEvaluationContext.cs");
					this.defaultDomainName = tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain().DomainName.Domain;
				}
				return this.defaultDomainName;
			}
		}

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x06006D06 RID: 27910 RVA: 0x001D1F12 File Offset: 0x001D0112
		public override List<KeyValuePair<string, string>> ExtraTrackingEventData
		{
			get
			{
				if (this.extraTrackingEventData == null)
				{
					this.extraTrackingEventData = new List<KeyValuePair<string, string>>();
				}
				return this.extraTrackingEventData;
			}
		}

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x06006D07 RID: 27911 RVA: 0x001D1F2D File Offset: 0x001D012D
		public override IsMemberOfResolver<string> IsMemberOfResolver
		{
			get
			{
				return this.ruleConfig.IsMemberOfResolver;
			}
		}

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x06006D08 RID: 27912 RVA: 0x001D1F3A File Offset: 0x001D013A
		public override string LocalServerFqdn
		{
			get
			{
				return PFRuleEvaluationContext.localServerFqdn;
			}
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x06006D09 RID: 27913 RVA: 0x001D1F41 File Offset: 0x001D0141
		public override IPAddress LocalServerNetworkAddress
		{
			get
			{
				return PFRuleEvaluationContext.localServerNetworkAddress;
			}
		}

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x06006D0A RID: 27914 RVA: 0x001D1F48 File Offset: 0x001D0148
		public override ExEventLog.EventTuple OofHistoryCorruption
		{
			get
			{
				throw new InvalidOperationException("Access of OofHistoryCorruption property is invalid for public folder rules.");
			}
		}

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x06006D0B RID: 27915 RVA: 0x001D1F54 File Offset: 0x001D0154
		public override ExEventLog.EventTuple OofHistoryFolderMissing
		{
			get
			{
				throw new InvalidOperationException("Access of OofHistoryFolderMissing property is invalid for public folder rules.");
			}
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x001D1F60 File Offset: 0x001D0160
		public static PFRuleEvaluationContext Create(StoreObjectId folderId, ProxyAddress recipientProxyAddress, ICoreItem message, long mimeSize, PublicFolderSession session)
		{
			PFRuleEvaluationContext result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				Folder folder = Folder.Bind(session, folderId, RuleEvaluationContextBase.AdditionalFolderProperties);
				disposeGuard.Add<Folder>(folder);
				PFMessageContext pfmessageContext = new PFMessageContext(folder, message, session, recipientProxyAddress ?? ProxyAddress.Parse(ProxyAddressPrefix.Smtp.PrimaryPrefix, session.MailboxPrincipal.MailboxInfo.PrimarySmtpAddress.ToString()), new ADRecipientCache<ADRawEntry>(PFRuleEvaluationContext.RecipientProperties, 0, session.MailboxPrincipal.MailboxInfo.OrganizationId), mimeSize);
				disposeGuard.Add<PFMessageContext>(pfmessageContext);
				pfmessageContext.traceFormatter = new TraceFormatter(false);
				session.ProhibitFolderRuleEvaluation = true;
				disposeGuard.Success();
				result = pfmessageContext;
			}
			return result;
		}

		// Token: 0x06006D0D RID: 27917 RVA: 0x001D2030 File Offset: 0x001D0230
		public override MessageItem CreateMessageItem(PropertyDefinition[] prefetchProperties)
		{
			PublicFolderSession publicFolderSession = base.StoreSession as PublicFolderSession;
			MessageItem result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MessageItem messageItem = MessageItem.Create(publicFolderSession, publicFolderSession.GetInternalSubmissionFolderId());
				disposeGuard.Add<MessageItem>(messageItem);
				messageItem.Load(prefetchProperties);
				messageItem.SaveFlags |= PropertyBagSaveFlags.IgnoreAccessDeniedErrors;
				disposeGuard.Success();
				result = messageItem;
			}
			return result;
		}

		// Token: 0x06006D0E RID: 27918 RVA: 0x001D20A8 File Offset: 0x001D02A8
		public override ISubmissionItem GenerateSubmissionItem(MessageItem item, WorkItem workItem)
		{
			return new PFSubmissionItem(this, item);
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x001D20B1 File Offset: 0x001D02B1
		public override Folder GetDeletedItemsFolder()
		{
			throw new InvalidOperationException("Calling GetDeletedItemsFolder is invalid for public folder rules.");
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x001D20C0 File Offset: 0x001D02C0
		public override void SetMailboxOwnerAsSender(MessageItem message)
		{
			PublicFolderSession publicFolderSession = base.StoreSession as PublicFolderSession;
			if (base.CurrentFolder.PropertyBag.GetValueOrDefault<bool>(FolderSchema.MailEnabled))
			{
				Exception ex = null;
				try
				{
					byte[] valueOrDefault = base.CurrentFolder.PropertyBag.GetValueOrDefault<byte[]>(FolderSchema.ProxyGuid);
					if (valueOrDefault != null && valueOrDefault.Length == 16)
					{
						IRecipientSession adrecipientSession = publicFolderSession.GetADRecipientSession(true, ConsistencyMode.PartiallyConsistent);
						ADRawEntry adrawEntry = adrecipientSession.Read(new ADObjectId(valueOrDefault)) as ADPublicFolder;
						if (adrawEntry != null)
						{
							message.From = new Participant(adrawEntry);
							return;
						}
					}
					ex = new ObjectNotFoundException(ServerStrings.ExItemNotFound);
				}
				catch (ADTransientException ex2)
				{
					ex = ex2;
				}
				catch (ADExternalException ex3)
				{
					ex = ex3;
				}
				catch (ADOperationException ex4)
				{
					ex = ex4;
				}
				catch (DataValidationException ex5)
				{
					ex = ex5;
				}
				catch (ObjectNotFoundException ex6)
				{
					ex = ex6;
				}
				if (ex != null)
				{
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_PFRuleSettingFromAddressFailure, base.CurrentFolder.StoreObjectId.ToHexEntryId(), new object[]
					{
						ex
					});
				}
			}
			message.From = (publicFolderSession.ConnectAsParticipant ?? new Participant(publicFolderSession.MailboxPrincipal));
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x001D2204 File Offset: 0x001D0404
		public override void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			StorageGlobals.EventLogger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x001D2214 File Offset: 0x001D0414
		public override ExTimeZone DetermineRecipientTimeZone()
		{
			if (this.timeZoneRetrieved)
			{
				base.TraceDebug<ExTimeZone>("TimeZone retrieved before, returning it. TimeZone: {0}", this.timeZone);
				return this.timeZone;
			}
			this.timeZoneRetrieved = true;
			this.timeZone = ExTimeZone.CurrentTimeZone;
			base.TraceDebug<Type, ExTimeZone>("Session is not MailboxSession, using server time zone instead. SessionType: {0}, TimeZone: {1}", base.StoreSession.GetType(), this.timeZone);
			return this.timeZone;
		}

		// Token: 0x06006D13 RID: 27923 RVA: 0x001D2278 File Offset: 0x001D0478
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			((PublicFolderSession)base.StoreSession).ProhibitFolderRuleEvaluation = false;
			if (base.Message != null)
			{
				if (this.messageContextStoreObject != null)
				{
					base.Message.PropertyBag.Context.StoreObject = this.messageContextStoreObject;
				}
				base.Message.CoreObject = null;
				base.Message.Dispose();
			}
		}

		// Token: 0x04003E18 RID: 15896
		private static readonly ADPropertyDefinition[] RecipientProperties = new ADPropertyDefinition[]
		{
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.EmailAddresses,
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.PrimarySmtpAddress,
			ADRecipientSchema.ThrottlingPolicy,
			ADRecipientSchema.SCLJunkEnabled,
			ADRecipientSchema.SCLJunkThreshold,
			IADMailStorageSchema.ExchangeGuid,
			IADMailStorageSchema.RulesQuota
		};

		// Token: 0x04003E19 RID: 15897
		private static string localServerFqdn;

		// Token: 0x04003E1A RID: 15898
		private static IPAddress localServerNetworkAddress;

		// Token: 0x04003E1B RID: 15899
		private List<KeyValuePair<string, string>> extraTrackingEventData;

		// Token: 0x04003E1C RID: 15900
		private string defaultDomainName;

		// Token: 0x04003E1D RID: 15901
		private PFRuleConfig ruleConfig;

		// Token: 0x04003E1E RID: 15902
		private bool timeZoneRetrieved;

		// Token: 0x04003E1F RID: 15903
		private ExTimeZone timeZone;

		// Token: 0x04003E20 RID: 15904
		private StoreObject messageContextStoreObject;
	}
}
