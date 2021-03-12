using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MailTips;
using Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000159 RID: 345
	internal sealed class MailTipsNotificationHandler
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x0005220C File Offset: 0x0005040C
		public MailTipsNotificationHandler(UserContext userContext)
		{
			this.userContext = userContext;
			this.mailTipsNotifier = new MailTipsPendingRequestNotifier();
			this.mailTipsNotifier.RegisterWithPendingRequestManager(userContext);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00052234 File Offset: 0x00050434
		internal IAsyncResult BeginGetMailTipsInBatches(RecipientInfo[] recipientsInfo, RecipientInfo senderInfo, bool doesNeedConfig, AsyncCallback asyncCallback, object asyncCallbackData)
		{
			this.primarySmtpAddress = this.userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			ClientSecurityContext clientSecurityContext = this.userContext.LogonIdentity.ClientSecurityContext.Clone();
			string weekdayDateTimeFormat = this.userContext.UserOptions.GetWeekdayDateTimeFormat(true);
			MailTipsState mailTipsState = new MailTipsState(recipientsInfo, senderInfo, doesNeedConfig, this.userContext.ExchangePrincipal.LegacyDn, this.primarySmtpAddress, clientSecurityContext, this.userContext.TimeZone, this.userContext.UserCulture, this.userContext.ExchangePrincipal.MailboxInfo.OrganizationId, this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, this.userContext.UserOptions.HideMailTipsByDefault, this.userContext.PendingRequestManager, Query<IEnumerable<MailTips>>.GetCurrentHttpRequestServerName(), weekdayDateTimeFormat);
			OwaAsyncResult owaAsyncResult = new OwaAsyncResult(asyncCallback, asyncCallbackData);
			Interlocked.Increment(ref this.concurrentRequestCount);
			if (3 >= this.concurrentRequestCount)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<int, string>((long)this.GetHashCode(), "MailTipsNotificationHandler.BeginGetMailTipsInBatches, serving concurrent request {0} for {1}", this.concurrentRequestCount, this.primarySmtpAddress);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.GetMailTipsWorker), mailTipsState);
				return owaAsyncResult;
			}
			IAsyncResult result;
			try
			{
				ExTraceGlobals.CoreCallTracer.TraceError<int>((long)this.GetHashCode(), "MailTipsNotificationHandler.BeginGetMailTipsInBatches, maximum concurrent request limit {0} has been reached", 3);
				MailTipsNotificationHandler.PopulateException(mailTipsState, new OwaMaxConcurrentRequestsExceededException("Maximum MailTips concurrent requests exceeded"), this.GetHashCode());
				this.mailTipsNotifier.AddToPayload(mailTipsState);
				this.mailTipsNotifier.PickupData();
				result = owaAsyncResult;
			}
			finally
			{
				Interlocked.Decrement(ref this.concurrentRequestCount);
			}
			return result;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000523D4 File Offset: 0x000505D4
		internal void EndGetMailTipsInBatches(IAsyncResult asyncResult)
		{
			OwaAsyncResult owaAsyncResult = (OwaAsyncResult)asyncResult;
			if (owaAsyncResult.Exception != null)
			{
				ExTraceGlobals.CoreCallTracer.TraceError<Exception>((long)this.GetHashCode(), "MailTipsNotificationHandler.EndGetMailTipsInBatches, exception {0}", owaAsyncResult.Exception);
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0005240C File Offset: 0x0005060C
		private static ProxyAddress[] GetNextBatch(RecipientInfo[] recipientsInfo, ref int index)
		{
			int num = recipientsInfo.Length - index;
			if (50 < num)
			{
				num = 50;
			}
			ProxyAddress[] array = new ProxyAddress[num];
			int i = 0;
			while (i < num)
			{
				array[i] = recipientsInfo[index].ToProxyAddress();
				i++;
				index++;
			}
			return array;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00052450 File Offset: 0x00050650
		private static void PopulateException(MailTipsState mailTipsState, Exception exception, int hashCode)
		{
			ExTraceGlobals.CoreCallTracer.TraceError<Exception>((long)hashCode, "MailTipsNotificationHandler.PopulateException: {0}", exception);
			List<MailTips> mailTipsResult = mailTipsState.MailTipsResult;
			RecipientInfo[] recipientsInfo = mailTipsState.RecipientsInfo;
			for (int i = mailTipsResult.Count; i < recipientsInfo.Length; i++)
			{
				mailTipsResult.Add(new MailTips(new EmailAddress(recipientsInfo[i].DisplayName, recipientsInfo[i].RoutingAddress, recipientsInfo[i].RoutingType), exception));
			}
			if (mailTipsState.RequestLogger == null)
			{
				mailTipsState.RequestLogger = new RequestLogger();
			}
			mailTipsState.RequestLogger.AppendToLog<Type>("MailTipsException", exception.GetType());
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000524E4 File Offset: 0x000506E4
		private void GetMailTipsWorker(object state)
		{
			MailTipsState mailTipsState = null;
			try
			{
				mailTipsState = (MailTipsState)state;
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "MailTipsNotificationHandler.GetMailTipsWorker");
				this.GetMailTipsInBatches(mailTipsState);
				this.mailTipsNotifier.AddToPayload(mailTipsState);
				this.mailTipsNotifier.PickupData();
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<Exception>((long)this.GetHashCode(), "Generic exception caught during GetMailTipsWorker call: {0}", ex);
				if (Globals.SendWatsonReports)
				{
					ExTraceGlobals.CoreTracer.TraceError((long)this.GetHashCode(), "Sending watson report.");
					ExWatson.AddExtraData(this.GetExtraWatsonData(mailTipsState));
					ExWatson.SendReport(ex, ReportOptions.None, null);
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.concurrentRequestCount);
				ExTraceGlobals.CoreCallTracer.TraceError<string, int>((long)this.GetHashCode(), "MailTipsNotificationHandler.GetMailTipsWorker, {0} concurrent requests count decremented to {1}", this.primarySmtpAddress, this.concurrentRequestCount);
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000525CC File Offset: 0x000507CC
		private ProxyAddress GetSendingAsProxyAddress(MailTipsState mailTipsState)
		{
			ProxyAddress proxyAddress;
			if (mailTipsState.SenderInfo == null)
			{
				if (string.IsNullOrEmpty(mailTipsState.LogonUserLegDn))
				{
					proxyAddress = ProxyAddress.Parse(ProxyAddressPrefix.Smtp.PrimaryPrefix, mailTipsState.LogonUserPrimarySmtpAddress ?? string.Empty);
				}
				else
				{
					proxyAddress = ProxyAddress.Parse(ProxyAddressPrefix.LegacyDN.PrimaryPrefix, mailTipsState.LogonUserLegDn);
				}
			}
			else
			{
				proxyAddress = mailTipsState.SenderInfo.ToProxyAddress();
			}
			mailTipsState.SendingAs = proxyAddress;
			return proxyAddress;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0005263C File Offset: 0x0005083C
		private IEnumerable<MailTips> GetMailTipsInBatches(MailTipsState mailTipsState)
		{
			Exception ex = null;
			try
			{
				using (IStandardBudget standardBudget = StandardBudget.Acquire(mailTipsState.ClientSecurityContext.UserSid, BudgetType.Owa, ADSessionSettings.FromRootOrgScopeSet()))
				{
					string callerInfo = "MailTipsNotificationHandler.GetMailTipsInBatches";
					standardBudget.CheckOverBudget();
					standardBudget.StartConnection(callerInfo);
					standardBudget.StartLocal(callerInfo, default(TimeSpan));
					mailTipsState.Budget = standardBudget;
					ClientContext clientContext = ClientContext.Create(mailTipsState.ClientSecurityContext, mailTipsState.Budget, mailTipsState.LogonUserTimeZone, mailTipsState.LogonUserCulture);
					((InternalClientContext)clientContext).QueryBaseDN = mailTipsState.QueryBaseDn;
					ExTraceGlobals.CoreCallTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "QueryBaseDN set to {0}", mailTipsState.QueryBaseDn);
					ProxyAddress sendingAsProxyAddress = this.GetSendingAsProxyAddress(mailTipsState);
					mailTipsState.CachedOrganizationConfiguration = CachedOrganizationConfiguration.GetInstance(mailTipsState.LogonUserOrgId, CachedOrganizationConfiguration.ConfigurationTypes.All);
					ExTraceGlobals.CoreCallTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Organization ID = {0}", mailTipsState.LogonUserOrgId);
					try
					{
						int num = 0;
						do
						{
							ProxyAddress[] nextBatch = MailTipsNotificationHandler.GetNextBatch(mailTipsState.RecipientsInfo, ref num);
							mailTipsState.GetMailTipsQuery = new GetMailTipsQuery(this.GetHashCode(), clientContext, sendingAsProxyAddress, mailTipsState.CachedOrganizationConfiguration, nextBatch, MailTipTypes.OutOfOfficeMessage | MailTipTypes.MailboxFullStatus | MailTipTypes.CustomMailTip | MailTipTypes.ExternalMemberCount | MailTipTypes.TotalMemberCount | MailTipTypes.DeliveryRestriction | MailTipTypes.ModerationStatus, mailTipsState.LogonUserCulture.LCID, mailTipsState.Budget, null);
							mailTipsState.GetMailTipsQuery.ServerName = mailTipsState.ServerName;
							mailTipsState.RequestLogger = mailTipsState.GetMailTipsQuery.RequestLogger;
							IEnumerable<MailTips> collection = mailTipsState.GetMailTipsQuery.Execute();
							mailTipsState.MailTipsResult.AddRange(collection);
						}
						while (num < mailTipsState.RecipientsInfo.Length);
					}
					catch (UserWithoutFederatedProxyAddressException ex2)
					{
						ex = ex2;
					}
					catch (InvalidFederatedOrganizationIdException ex3)
					{
						ex = ex3;
					}
				}
			}
			catch (OverBudgetException ex4)
			{
				ex = ex4;
			}
			catch (ObjectDisposedException ex5)
			{
				ex = ex5;
			}
			catch (OwaInvalidOperationException ex6)
			{
				ex = ex6;
			}
			finally
			{
				if (ex != null)
				{
					MailTipsNotificationHandler.PopulateException(mailTipsState, ex, this.GetHashCode());
				}
			}
			return mailTipsState.MailTipsResult;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0005289C File Offset: 0x00050A9C
		private string GetExtraWatsonData(MailTipsState mailTipsState)
		{
			int num = 0;
			if (this.userContext.Breadcrumbs != null)
			{
				num = this.userContext.Breadcrumbs.Count * 128;
			}
			int num2 = 0;
			if (mailTipsState != null)
			{
				num2 = mailTipsState.GetEstimatedStringLength();
			}
			StringBuilder stringBuilder = new StringBuilder(40 + num + num2);
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "OWAVersion: {0}, ", new object[]
			{
				Globals.ApplicationVersion
			});
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "MailTipsState: {0}, ", new object[]
			{
				mailTipsState
			});
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "BreadCrumbs: {0}", new object[]
			{
				this.userContext.DumpBreadcrumbs()
			});
			return stringBuilder.ToString();
		}

		// Token: 0x04000873 RID: 2163
		private const int MaxConcurrentRequests = 3;

		// Token: 0x04000874 RID: 2164
		private const MailTipTypes MailTipsTypesRequested = MailTipTypes.OutOfOfficeMessage | MailTipTypes.MailboxFullStatus | MailTipTypes.CustomMailTip | MailTipTypes.ExternalMemberCount | MailTipTypes.TotalMemberCount | MailTipTypes.DeliveryRestriction | MailTipTypes.ModerationStatus;

		// Token: 0x04000875 RID: 2165
		private UserContext userContext;

		// Token: 0x04000876 RID: 2166
		private MailTipsPendingRequestNotifier mailTipsNotifier;

		// Token: 0x04000877 RID: 2167
		private int concurrentRequestCount;

		// Token: 0x04000878 RID: 2168
		private string primarySmtpAddress;
	}
}
