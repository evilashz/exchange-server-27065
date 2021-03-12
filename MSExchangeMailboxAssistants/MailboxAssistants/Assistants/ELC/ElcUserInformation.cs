using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000036 RID: 54
	internal class ElcUserInformation
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000B980 File Offset: 0x00009B80
		internal ElcUserInformation(MailboxSession session)
		{
			this.mailboxSession = session;
			this.utcNow = DateTime.UtcNow;
			this.now = (DateTime)ExDateTime.Now;
			IRecipientSession recipientSession;
			this.adUser = AdReader.GetADUser(this.mailboxSession, true, out recipientSession);
			if (this.adUser == null)
			{
				ElcUserInformation.Tracer.TraceError((long)this.GetHashCode(), "{0}: Failed to get the AD information for the user.", new object[]
				{
					TraceContext.Get()
				});
				throw new SkipException(Strings.descADUserLookupFailure(this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			if (this.adUser.RetentionHoldEnabled && (this.adUser.StartDateForRetentionHold == null || this.now >= this.adUser.StartDateForRetentionHold) && (this.adUser.EndDateForRetentionHold == null || this.now <= this.adUser.EndDateForRetentionHold))
			{
				this.suspendExpiration = true;
			}
			if (this.adUser.LitigationHoldEnabled)
			{
				this.litigationHold = true;
				this.litigationHoldDuration = this.adUser.LitigationHoldDuration;
				if (this.litigationHoldDuration == null && this.ApplyLitigationHoldDuration)
				{
					this.GetLegacyStateAndSetLitigationHoldDuration();
				}
			}
			this.auditEnabled = this.adUser.MailboxAuditEnabled;
			this.InPlaceHolds = this.adUser.InPlaceHolds;
			this.InPlaceHoldConfiguration = new List<InPlaceHoldConfiguration>();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000BB44 File Offset: 0x00009D44
		private void GetLegacyStateAndSetLitigationHoldDuration()
		{
			ElcMailboxHelper.ConfigState configState = ElcMailboxHelper.ConfigState.Unknown;
			IRecipientSession recipientSession;
			ADUser aduser = AdReader.GetADUser(this.mailboxSession, false, out recipientSession);
			if (aduser == null)
			{
				ElcUserInformation.Tracer.TraceError((long)this.GetHashCode(), "{0}: Failed to get the writable AD information for the user.", new object[]
				{
					TraceContext.Get()
				});
				throw new SkipException(Strings.descADUserLookupFailure(this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			Unlimited<EnhancedTimeSpan>? legacyLitigationHoldDuration = this.GetLegacyLitigationHoldDuration(out configState);
			aduser.LitigationHoldDuration = legacyLitigationHoldDuration;
			try
			{
				recipientSession.Save(aduser);
			}
			catch (DataValidationException ex)
			{
				ElcUserInformation.Tracer.TraceDebug<ElcUserInformation, string, DataValidationException>((long)this.GetHashCode(), "{0}: DataValidationException occurred when setting LitigationHoldDuration to ADUser. Duration value: {1}. Exception: {2}", this, (legacyLitigationHoldDuration != null) ? (legacyLitigationHoldDuration.Value.IsUnlimited ? Unlimited<EnhancedTimeSpan>.UnlimitedString : legacyLitigationHoldDuration.Value.Value.TotalDays.ToString()) : "No value", ex);
				throw new SkipException(new LocalizedString(string.Format("{0}: DataValidationException occurred when setting LitigationHoldDuration to ADUser. Duration value: {1}.", this, (legacyLitigationHoldDuration != null) ? (legacyLitigationHoldDuration.Value.IsUnlimited ? Unlimited<EnhancedTimeSpan>.UnlimitedString : legacyLitigationHoldDuration.Value.Value.TotalDays.ToString()) : "No value")), ex);
			}
			this.litigationHoldDuration = aduser.LitigationHoldDuration;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		private Unlimited<EnhancedTimeSpan>? GetLegacyLitigationHoldDuration(out ElcMailboxHelper.ConfigState state)
		{
			state = ElcMailboxHelper.ConfigState.Unknown;
			Unlimited<EnhancedTimeSpan> value;
			Exception ex;
			ElcMailboxHelper.TryGetExistingHoldDurationInStore(this.mailboxSession.MailboxOwner, this.mailboxSession.ClientInfoString, out value, out state, out ex);
			if (state == ElcMailboxHelper.ConfigState.Found)
			{
				return new Unlimited<EnhancedTimeSpan>?(value);
			}
			if (ex != null)
			{
				ElcUserInformation.Tracer.TraceDebug<IExchangePrincipal>((long)this.GetHashCode(), "{0}: Unable to retrieve litigation hold duration for this mailbox.", this.mailboxSession.MailboxOwner);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_FailedToReadLitigationHoldDurationFromPrimaryMailbox, null, new object[]
				{
					this.mailboxSession.MailboxOwner
				});
			}
			else
			{
				ElcUserInformation.Tracer.TraceDebug<IExchangePrincipal, string>((long)this.GetHashCode(), "{0}: Unable to retrieve litigation hold duration for this mailbox. ConfigState is {1}. No error encountered", this.mailboxSession.MailboxOwner, state.ToString());
			}
			return new Unlimited<EnhancedTimeSpan>?(Unlimited<EnhancedTimeSpan>.UnlimitedValue);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000BD80 File Offset: 0x00009F80
		private void HandleErrorLoadingProcessEhaMigrationMessageSetting(OrganizationId orgId, Exception exception)
		{
			ElcUserInformation.Tracer.TraceError<ElcUserInformation, OrganizationId, string>((long)this.GetHashCode(), "{0}: Failed to find Organization settings for IsProcessEhaMigratedMessages for organization {1}. {2}", this, orgId, (exception != null) ? ("Exception: " + exception.ToString()) : string.Empty);
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCFailedToLoadProcessEhaMigrationMessageSetting, null, new object[]
			{
				orgId,
				this.MailboxSession.DisplayName,
				(exception != null) ? exception.ToString() : string.Empty
			});
			throw new SkipException(Strings.descUnableToFetchOrganizationEhaMigrationSetting(this.MailboxSession.DisplayName, orgId.ToString()));
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000BE18 File Offset: 0x0000A018
		internal MailboxSession MailboxSession
		{
			get
			{
				return this.mailboxSession;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000BE20 File Offset: 0x0000A020
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000BE28 File Offset: 0x0000A028
		internal List<InPlaceHoldConfiguration> InPlaceHoldConfiguration { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000BE31 File Offset: 0x0000A031
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000BE39 File Offset: 0x0000A039
		internal IList<string> InPlaceHolds { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000BE42 File Offset: 0x0000A042
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000BE4A File Offset: 0x0000A04A
		internal OrganizationCache OrgCache { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000BE54 File Offset: 0x0000A054
		internal int MaxSearchQueriesLimit
		{
			get
			{
				if (this.maxSearchQueriesLimit == null)
				{
					object obj = Globals.ReadRegKey(ElcGlobals.ParameterRegistryKeyPath, "MaxSearchHoldQueryLengthLimit");
					int value;
					if (obj != null && int.TryParse(obj.ToString(), out value))
					{
						this.maxSearchQueriesLimit = new int?(value);
					}
					else
					{
						this.maxSearchQueriesLimit = new int?(10000);
					}
				}
				return this.maxSearchQueriesLimit.Value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		internal string MailboxSmtpAddress
		{
			get
			{
				return this.mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		internal ADUser ADUser
		{
			get
			{
				return this.adUser;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		internal bool SuspendExpiration
		{
			get
			{
				return this.suspendExpiration;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		internal bool LitigationHoldEnabled
		{
			get
			{
				return this.litigationHold;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000BF04 File Offset: 0x0000A104
		internal Unlimited<EnhancedTimeSpan>? LitigationHoldDuration
		{
			get
			{
				return this.litigationHoldDuration;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000BF0C File Offset: 0x0000A10C
		internal bool AuditEnabled
		{
			get
			{
				return this.auditEnabled;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000BF14 File Offset: 0x0000A114
		internal DateTime UtcNow
		{
			get
			{
				return this.utcNow;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000BF1C File Offset: 0x0000A11C
		internal DateTime Now
		{
			get
			{
				return this.now;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000BF24 File Offset: 0x0000A124
		internal bool ProcessEhaMigratedMessages
		{
			get
			{
				if (this.processEhaMigratedMessages == null)
				{
					if (!this.TryProcessEhaMigratedMessages)
					{
						this.processEhaMigratedMessages = new bool?(false);
					}
					else
					{
						OrganizationId organizationId = this.ADUser.OrganizationId;
						Exception ex = null;
						try
						{
							bool? flag = this.OrgCache.Get(organizationId);
							if (flag != null)
							{
								this.processEhaMigratedMessages = new bool?(flag.Value);
							}
							else
							{
								this.HandleErrorLoadingProcessEhaMigrationMessageSetting(organizationId, null);
							}
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
						if (ex != null)
						{
							this.HandleErrorLoadingProcessEhaMigrationMessageSetting(organizationId, ex);
						}
					}
				}
				return this.processEhaMigratedMessages.Value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		internal bool TryProcessEhaMigratedMessages
		{
			get
			{
				bool result = false;
				try
				{
					result = (VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.ElcAssistantTryProcessEhaMigratedMessages.Enabled || Datacenter.IsPartnerHostedOnly(true));
				}
				catch (CannotDetermineExchangeModeException)
				{
				}
				return result;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000C030 File Offset: 0x0000A230
		internal bool ApplyLitigationHoldDuration
		{
			get
			{
				bool result = false;
				try
				{
					result = VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.ElcAssistantApplyLitigationHoldDuration.Enabled;
				}
				catch (CannotDetermineExchangeModeException)
				{
				}
				return result;
			}
		}

		// Token: 0x0400016A RID: 362
		private const int DefaultMaxSearchQueryLengthLimit = 10000;

		// Token: 0x0400016B RID: 363
		private const string MaxSearchQueryLengthLimitRegistryName = "MaxSearchHoldQueryLengthLimit";

		// Token: 0x0400016C RID: 364
		protected static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x0400016D RID: 365
		protected static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400016E RID: 366
		private readonly bool auditEnabled;

		// Token: 0x0400016F RID: 367
		private MailboxSession mailboxSession;

		// Token: 0x04000170 RID: 368
		private ADUser adUser;

		// Token: 0x04000171 RID: 369
		private bool suspendExpiration;

		// Token: 0x04000172 RID: 370
		private bool litigationHold;

		// Token: 0x04000173 RID: 371
		private Unlimited<EnhancedTimeSpan>? litigationHoldDuration = null;

		// Token: 0x04000174 RID: 372
		private DateTime utcNow;

		// Token: 0x04000175 RID: 373
		private DateTime now;

		// Token: 0x04000176 RID: 374
		private bool? processEhaMigratedMessages;

		// Token: 0x04000177 RID: 375
		private int? maxSearchQueriesLimit;
	}
}
