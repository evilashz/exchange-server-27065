﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000200 RID: 512
	internal abstract class NonIndexableItemProvider
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003BA94 File Offset: 0x00039C94
		public NonIndexableItemProvider(IRecipientSession recipientSession, ExTimeZone timeZone, CallerInfo callerInfo, OrganizationId orgId, string[] mailboxes, bool searchArchiveOnly)
		{
			Util.ThrowOnNull(recipientSession, "recipientSession");
			Util.ThrowOnNull(timeZone, "timeZone");
			Util.ThrowOnNull(callerInfo, "callerInfo");
			Util.ThrowOnNull(orgId, "orgId");
			Util.ThrowOnNull(mailboxes, "mailboxes");
			this.recipientSession = recipientSession;
			this.timeZone = timeZone;
			this.callerInfo = callerInfo;
			this.orgId = orgId;
			this.mailboxes = mailboxes;
			this.searchArchiveOnly = searchArchiveOnly;
			this.failedMailboxes = new Dictionary<string, string>(1);
			this.alreadyProxy = false;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003BB20 File Offset: 0x00039D20
		internal Dictionary<string, string> FailedMailboxes
		{
			get
			{
				return this.failedMailboxes;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003BB28 File Offset: 0x00039D28
		internal void ExecuteSearch()
		{
			this.InternalExecuteSearch();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003BB30 File Offset: 0x00039D30
		internal static List<ADRawEntry> FindMailboxesInAD(IRecipientSession recipientSession, string[] legacyExchangeDNs, List<string> notFoundMailboxes)
		{
			List<ADRawEntry> list = new List<ADRawEntry>(legacyExchangeDNs.Length);
			Result<ADRawEntry>[] array = recipientSession.FindByLegacyExchangeDNs(legacyExchangeDNs, MailboxInfo.PropertyDefinitionCollection);
			if (array != null && array.Length > 0)
			{
				Dictionary<string, ADRawEntry> dictionary = new Dictionary<string, ADRawEntry>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					ADRawEntry data = array[i].Data;
					if (data != null)
					{
						string key = (string)data[ADRecipientSchema.LegacyExchangeDN];
						if (!dictionary.ContainsKey(key))
						{
							dictionary.Add(key, data);
						}
						foreach (string text in legacyExchangeDNs)
						{
							if (!dictionary.ContainsKey(text) && Util.CheckLegacyDnExistInProxyAddresses(text, data))
							{
								dictionary.Add(text, data);
								break;
							}
						}
					}
				}
				foreach (string text2 in legacyExchangeDNs)
				{
					if (dictionary.ContainsKey(text2))
					{
						list.Add(dictionary[text2]);
					}
					else
					{
						notFoundMailboxes.Add(text2);
					}
				}
			}
			else
			{
				notFoundMailboxes.AddRange(legacyExchangeDNs);
			}
			return list;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003BC3C File Offset: 0x00039E3C
		protected void AddFailedMailbox(string legacyDn, string error)
		{
			if (this.failedMailboxes.ContainsKey(legacyDn))
			{
				this.failedMailboxes[legacyDn] = error;
				return;
			}
			this.failedMailboxes.Add(legacyDn, error);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003BD0C File Offset: 0x00039F0C
		protected void ExecuteSearchWebService()
		{
			this.ewsClient = Factory.Current.CreateNonIndexableDiscoveryEwsClient(this.groupId, new MailboxInfo[]
			{
				this.mailboxInfo
			}, this.timeZone, this.callerInfo);
			this.alreadyProxy = true;
			string legacyExchangeDN = this.mailboxInfo.LegacyExchangeDN;
			Exception exception = null;
			Util.HandleExceptions(delegate
			{
				int num = 3;
				while (num-- > 0)
				{
					try
					{
						ServiceRemoteException exception = null;
						this.InternalExecuteSearchWebService();
						break;
					}
					catch (ServiceResponseException ex)
					{
						ServiceRemoteException exception = ex;
						if (!WebServiceMailboxSearchGroup.TransientServiceErrors.Contains(ex.ErrorCode))
						{
							break;
						}
					}
					catch (ServiceRemoteException exception)
					{
						ServiceRemoteException exception = exception;
					}
					catch (WebServiceProxyInvalidResponseException exception2)
					{
						ServiceRemoteException exception = exception2;
						break;
					}
				}
			}, delegate(GrayException ge)
			{
				exception = new WebServiceProxyInvalidResponseException(Strings.UnexpectedError, ge);
			});
			if (exception != null)
			{
				this.AddFailedMailbox(legacyExchangeDN, exception.Message);
				this.HandleExecuteSearchWebServiceFailed(legacyExchangeDN);
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003BDB4 File Offset: 0x00039FB4
		protected void PerformMailboxDiscovery(ADRawEntry adRawEntry, MailboxType mailboxType, out GroupId groupId, out MailboxInfo mailboxInfo)
		{
			List<MailboxInfo> list = new List<MailboxInfo>(1);
			list.Add(new MailboxInfo(adRawEntry, mailboxType));
			IEwsEndpointDiscovery ewsEndpointDiscovery = Factory.Current.GetEwsEndpointDiscovery(list, this.orgId, this.callerInfo);
			long num = 0L;
			long num2 = 0L;
			Dictionary<GroupId, List<MailboxInfo>> source = ewsEndpointDiscovery.FindEwsEndpoints(out num, out num2);
			KeyValuePair<GroupId, List<MailboxInfo>> keyValuePair = source.First<KeyValuePair<GroupId, List<MailboxInfo>>>();
			groupId = keyValuePair.Key;
			mailboxInfo = keyValuePair.Value[0];
		}

		// Token: 0x06000DAE RID: 3502
		protected abstract void InternalExecuteSearch();

		// Token: 0x06000DAF RID: 3503
		protected abstract void InternalExecuteSearchWebService();

		// Token: 0x06000DB0 RID: 3504
		protected abstract void HandleExecuteSearchWebServiceFailed(string legacyDn);

		// Token: 0x0400096F RID: 2415
		protected readonly IRecipientSession recipientSession;

		// Token: 0x04000970 RID: 2416
		protected readonly ExTimeZone timeZone;

		// Token: 0x04000971 RID: 2417
		protected readonly CallerInfo callerInfo;

		// Token: 0x04000972 RID: 2418
		protected readonly OrganizationId orgId;

		// Token: 0x04000973 RID: 2419
		protected readonly string[] mailboxes;

		// Token: 0x04000974 RID: 2420
		protected readonly bool searchArchiveOnly;

		// Token: 0x04000975 RID: 2421
		protected readonly Dictionary<string, string> failedMailboxes;

		// Token: 0x04000976 RID: 2422
		protected GroupId groupId;

		// Token: 0x04000977 RID: 2423
		protected MailboxInfo mailboxInfo;

		// Token: 0x04000978 RID: 2424
		protected INonIndexableDiscoveryEwsClient ewsClient;

		// Token: 0x04000979 RID: 2425
		protected bool alreadyProxy;
	}
}
