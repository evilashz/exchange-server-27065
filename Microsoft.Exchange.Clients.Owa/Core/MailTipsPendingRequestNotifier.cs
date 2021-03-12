using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MailTips;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200015A RID: 346
	internal sealed class MailTipsPendingRequestNotifier : IPendingRequestNotifier
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00052958 File Offset: 0x00050B58
		public bool ShouldThrottle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000BE7 RID: 3047 RVA: 0x0005295C File Offset: 0x00050B5C
		// (remove) Token: 0x06000BE8 RID: 3048 RVA: 0x00052994 File Offset: 0x00050B94
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000529CC File Offset: 0x00050BCC
		public string ReadDataAndResetState()
		{
			string result;
			lock (this.payload)
			{
				if (0 < this.payload.Count)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug<int>((long)this.GetHashCode(), "MailTipsPendingRequestNotifier.ReadDataAndResetState. Processing {0} requests.", this.payload.Count);
					int capacity = 512 * this.payload.Count;
					StringBuilder stringBuilder = new StringBuilder(capacity);
					for (int i = 1; i <= this.payload.Count; i++)
					{
						MailTipsState mailTipsState = this.payload.Dequeue();
						ExTraceGlobals.CoreCallTracer.TraceDebug<int, string, ProxyAddress>((long)this.GetHashCode(), "MailTipsPendingRequestNotifier.ReadDataAndResetState. Request: {0}, Requester: {1}, Sending as: {2}.", i, mailTipsState.LogonUserPrimarySmtpAddress, mailTipsState.SendingAs);
						stringBuilder.Append("processMailTipsResponse({");
						bool flag2 = true;
						foreach (MailTips mailTips in mailTipsState.MailTipsResult)
						{
							if (!flag2)
							{
								stringBuilder.Append(",");
							}
							flag2 = false;
							MailTipsPendingRequestNotifier.SerializeMailTips(mailTips, mailTipsState, stringBuilder);
						}
						if (mailTipsState.Budget != null)
						{
							mailTipsState.Budget.Dispose();
						}
						stringBuilder.Append("}");
						stringBuilder.Append(",{");
						stringBuilder.Append("'fHideByDefault' : ");
						stringBuilder.Append(mailTipsState.ShouldHideByDefault ? 1 : 0);
						if (mailTipsState.DoesNeedConfig)
						{
							Organization configuration = mailTipsState.CachedOrganizationConfiguration.OrganizationConfiguration.Configuration;
							stringBuilder.Append(", 'fEnabled' : ");
							stringBuilder.Append(configuration.MailTipsAllTipsEnabled ? 1 : 0);
							stringBuilder.Append(", 'fMailboxEnabled' : ");
							stringBuilder.Append(configuration.MailTipsMailboxSourcedTipsEnabled ? 1 : 0);
							stringBuilder.Append(", 'fGroupMetricsEnabled' : ");
							stringBuilder.Append(configuration.MailTipsGroupMetricsEnabled ? 1 : 0);
							stringBuilder.Append(", 'fExternalEnabled' : ");
							stringBuilder.Append(configuration.MailTipsExternalRecipientsTipsEnabled ? 1 : 0);
							stringBuilder.Append(", 'iLargeAudienceThreshold' : ");
							stringBuilder.Append(configuration.MailTipsLargeAudienceThreshold);
						}
						stringBuilder.Append("}");
						stringBuilder.Append(");");
						if (mailTipsState.GetMailTipsQuery != null)
						{
							if (mailTipsState.RequestLogger == null)
							{
								mailTipsState.RequestLogger = new RequestLogger();
							}
						}
						else
						{
							ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "MailTipsPendingRequestNotifier.ReadDataAndResetState: GetMailTipsQuery was null.");
						}
						if (mailTipsState.PendingRequestManager.ChunkedHttpResponse != null)
						{
							mailTipsState.PendingRequestManager.ChunkedHttpResponse.Log(mailTipsState.RequestLogger);
						}
					}
					this.isPickupInProgress = false;
					result = stringBuilder.ToString();
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00052CA8 File Offset: 0x00050EA8
		public void AddToPayload(MailTipsState mailTipsState)
		{
			lock (this.payload)
			{
				if (256 == this.payload.Count)
				{
					this.payload.Dequeue();
				}
				this.payload.Enqueue(mailTipsState);
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00052D0C File Offset: 0x00050F0C
		public void PickupData()
		{
			bool flag = false;
			lock (this.payload)
			{
				if (this.isPickupInProgress)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "MailTipsPendingRequestNotifier.PickupData. No need to call DataAvailable method, data pickup is already in progress.");
				}
				else if (this.payload.Count == 0)
				{
					ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "MailTipsPendingRequestNotifier.PickupData. No data available to be picked up.");
				}
				else
				{
					this.isPickupInProgress = true;
					flag = true;
				}
			}
			if (flag)
			{
				this.DataAvailable(this, new EventArgs());
				ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "MailTipsPendingRequestNotifier.PickupData. Pickup is in progress.");
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00052DC0 File Offset: 0x00050FC0
		public void ConnectionAliveTimer()
		{
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00052DC4 File Offset: 0x00050FC4
		internal static void SerializeMailTips(MailTips mailTips, MailTipsState mailTipsState, StringBuilder stringBuilder)
		{
			stringBuilder.Append("'");
			stringBuilder.Append(Utilities.JavascriptEncode(mailTips.EmailAddress.Address));
			stringBuilder.Append("' : ");
			stringBuilder.Append("{");
			stringBuilder.Append("'iSize' : ");
			stringBuilder.Append(mailTips.TotalMemberCount);
			stringBuilder.Append(", ");
			stringBuilder.Append("'iExternalSize' : ");
			stringBuilder.Append(mailTips.ExternalMemberCount);
			if (!string.IsNullOrEmpty(mailTips.CustomMailTip))
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("'sCustomTip' : '");
				stringBuilder.Append(Utilities.JavascriptEncode(Utilities.RemoveHtmlComments(mailTips.CustomMailTip)));
				stringBuilder.Append("'");
			}
			if (!string.IsNullOrEmpty(mailTips.OutOfOfficeMessage))
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("'sAutoReplyMessage' : '");
				stringBuilder.Append(Utilities.JavascriptEncode(Utilities.RemoveHtmlComments(mailTips.OutOfOfficeMessage)));
				stringBuilder.Append("'");
			}
			if (mailTips.OutOfOfficeDuration != null && DateTime.MinValue != mailTips.OutOfOfficeDuration.EndTime)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("'sAutoReplyEndDate' : '");
				ExDateTime exDateTime = new ExDateTime(mailTipsState.LogonUserTimeZone, mailTips.OutOfOfficeDuration.EndTime);
				stringBuilder.Append(Utilities.JavascriptEncode(exDateTime.ToString(mailTipsState.WeekdayDateTimeFormat, mailTipsState.LogonUserCulture)));
				stringBuilder.Append("'");
			}
			if (mailTips.IsModerated)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("'fModerated' : 1");
			}
			if (mailTips.MailboxFull)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("'fFull' : 1");
			}
			if (null != mailTipsState.SendingAs && !string.IsNullOrEmpty(mailTipsState.SendingAs.AddressString))
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("'oRestricted' : {'");
				stringBuilder.Append(Utilities.JavascriptEncode(mailTipsState.SendingAs.AddressString));
				stringBuilder.Append("' : ");
				stringBuilder.Append(mailTips.DeliveryRestricted ? "1}" : "0}");
			}
			if (mailTips.Exception != null)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append("'fErrored' : 1");
			}
			stringBuilder.Append("}");
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00053036 File Offset: 0x00051236
		internal void RegisterWithPendingRequestManager(UserContext userContext)
		{
			if (userContext != null && userContext.PendingRequestManager != null)
			{
				userContext.PendingRequestManager.AddPendingRequestNotifier(this);
				return;
			}
			ExTraceGlobals.CoreCallTracer.TraceDebug((long)this.GetHashCode(), "MailTipsPendingRequestNotifier.RegisterWithPendingRequestManager. Cannot register because the pending request manager is null.");
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00053066 File Offset: 0x00051266
		internal MailTipsPendingRequestNotifier()
		{
			this.payload = new Queue<MailTipsState>(16);
		}

		// Token: 0x04000879 RID: 2169
		private const int DefaultQueueCapacity = 16;

		// Token: 0x0400087A RID: 2170
		private const int MaxQueueCapacity = 256;

		// Token: 0x0400087B RID: 2171
		private const int AverageMailTipsLength = 512;

		// Token: 0x0400087C RID: 2172
		private Queue<MailTipsState> payload;

		// Token: 0x0400087D RID: 2173
		private bool isPickupInProgress;
	}
}
