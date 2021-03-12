using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000375 RID: 885
	internal sealed class SetHoldOnMailboxes : SingleStepServiceCommand<SetHoldOnMailboxesRequest, MailboxHoldResult>, IDisposeTrackable, IDisposable
	{
		// Token: 0x060018CA RID: 6346 RVA: 0x00088A60 File Offset: 0x00086C60
		public SetHoldOnMailboxes(CallContext callContext, SetHoldOnMailboxesRequest request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.SaveRequestData(request);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00088A7D File Offset: 0x00086C7D
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SetHoldOnMailboxes>(this);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00088A85 File Offset: 0x00086C85
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00088A9A File Offset: 0x00086C9A
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00088ABC File Offset: 0x00086CBC
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new SetHoldOnMailboxesResponse(base.Result.Code, base.Result.Error, base.Result.Value);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00088AE4 File Offset: 0x00086CE4
		internal override ServiceResult<MailboxHoldResult> Execute()
		{
			MailboxSearchHelper.PerformCommonAuthorization(base.CallContext.IsExternalUser, out this.runspaceConfig, out this.recipientSession);
			return this.ProcessRequest();
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00088B08 File Offset: 0x00086D08
		private void Dispose(bool fromDispose)
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00088B2C File Offset: 0x00086D2C
		private void SaveRequestData(SetHoldOnMailboxesRequest request)
		{
			this.actionType = request.ActionType;
			this.holdId = request.HoldId;
			this.query = request.Query;
			this.mailboxes = request.Mailboxes;
			this.language = request.Language;
			this.inPlaceHoldIdentity = request.InPlaceHoldIdentity;
			this.itemHoldPeriod = request.ItemHoldPeriod;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00088B90 File Offset: 0x00086D90
		private List<string> GetAndValidateMailboxObjectIds(List<string> nonExistMailboxes, List<string> nonAllowedMailboxes)
		{
			List<string> list = new List<string>();
			this.dictADRawEntries = MailboxSearchHelper.FindADEntriesByLegacyExchangeDNs(this.recipientSession, this.mailboxes, MailboxSearchHelper.AdditionalProperties);
			foreach (string text in this.mailboxes)
			{
				ADRawEntry recipient = null;
				if (this.dictADRawEntries.TryGetValue(text, out recipient))
				{
					if (MailboxSearchHelper.IsMembershipGroup(recipient))
					{
						throw new ServiceArgumentException(CoreResources.IDs.ErrorCannotApplyHoldOperationOnDG);
					}
					if (!MailboxSearchHelper.CanEnableHoldOnMailbox(this.runspaceConfig, recipient))
					{
						nonAllowedMailboxes.Add(text);
					}
					else
					{
						list.Add(text);
					}
				}
				else
				{
					nonExistMailboxes.Add(text);
				}
			}
			return list;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00088C30 File Offset: 0x00086E30
		private ServiceResult<MailboxHoldResult> ProcessRequest()
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<string> list3 = (this.mailboxes == null) ? null : this.GetAndValidateMailboxObjectIds(list, list2);
			bool flag = list3 != null && list3.Count > 0;
			if (string.IsNullOrEmpty(this.inPlaceHoldIdentity) && list2.Count == 0 && !flag)
			{
				ExTraceGlobals.SearchTracer.TraceError((long)this.GetHashCode(), "No mailbox to be placed hold after validating the mailbox list.");
				throw new ServiceInvalidOperationException((CoreResources.IDs)3819492078U);
			}
			Unlimited<EnhancedTimeSpan> unlimitedValue = Unlimited<EnhancedTimeSpan>.UnlimitedValue;
			if (!string.IsNullOrEmpty(this.itemHoldPeriod) && !Unlimited<EnhancedTimeSpan>.TryParse(this.itemHoldPeriod, out unlimitedValue))
			{
				throw new ValueOutOfRangeException(new ArgumentOutOfRangeException("ItemHoldPeriod"));
			}
			DiscoverySearchDataProvider discoverySearchDataProvider = new DiscoverySearchDataProvider(this.recipientSession.SessionSettings.CurrentOrganizationId);
			MailboxDiscoverySearch mailboxDiscoverySearch = null;
			mailboxDiscoverySearch = discoverySearchDataProvider.FindByAlternativeId<MailboxDiscoverySearch>(this.holdId);
			bool flag2 = false;
			if (this.actionType == HoldAction.Update)
			{
				if (mailboxDiscoverySearch == null)
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorHoldIsNotFound);
				}
				flag2 = true;
				mailboxDiscoverySearch.Query = this.query;
				mailboxDiscoverySearch.ItemHoldPeriod = unlimitedValue;
				if (!string.IsNullOrEmpty(this.language))
				{
					try
					{
						new CultureInfo(this.language);
						mailboxDiscoverySearch.Language = this.language;
					}
					catch (CultureNotFoundException)
					{
						ExTraceGlobals.SearchTracer.TraceError<string>((long)this.GetHashCode(), "Culture info: \"{0}\" returns CultureNotFoundException", this.language);
						throw new ServiceArgumentException(CoreResources.IDs.ErrorQueryLanguageNotValid);
					}
				}
				discoverySearchDataProvider.Save(mailboxDiscoverySearch);
			}
			else if (flag || !string.IsNullOrEmpty(this.inPlaceHoldIdentity))
			{
				if (this.actionType == HoldAction.Create)
				{
					if (mailboxDiscoverySearch == null)
					{
						mailboxDiscoverySearch = new MailboxDiscoverySearch();
						mailboxDiscoverySearch.Name = this.holdId;
						mailboxDiscoverySearch.InPlaceHoldEnabled = true;
						mailboxDiscoverySearch.InPlaceHoldIdentity = this.inPlaceHoldIdentity;
						if (!string.IsNullOrEmpty(this.inPlaceHoldIdentity))
						{
							mailboxDiscoverySearch.Description = CoreResources.GetLocalizedString((CoreResources.IDs)2560374358U);
						}
					}
					mailboxDiscoverySearch.Query = this.query;
					mailboxDiscoverySearch.ItemHoldPeriod = unlimitedValue;
					if (!string.IsNullOrEmpty(this.language))
					{
						mailboxDiscoverySearch.Language = this.language;
					}
					if (list3 == null)
					{
						goto IL_2D0;
					}
					using (List<string>.Enumerator enumerator = list3.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string item = enumerator.Current;
							if (mailboxDiscoverySearch.Sources == null)
							{
								mailboxDiscoverySearch.Sources = new MultiValuedProperty<string>();
							}
							if (!mailboxDiscoverySearch.Sources.Contains(item))
							{
								mailboxDiscoverySearch.Sources.Add(item);
							}
						}
						goto IL_2D0;
					}
				}
				if (this.actionType == HoldAction.Remove)
				{
					if (mailboxDiscoverySearch == null)
					{
						throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorHoldIsNotFound);
					}
					if (mailboxDiscoverySearch.Sources != null && list3 != null)
					{
						foreach (string item2 in list3)
						{
							mailboxDiscoverySearch.Sources.Remove(item2);
						}
					}
				}
				IL_2D0:
				if (string.IsNullOrEmpty(this.inPlaceHoldIdentity))
				{
					mailboxDiscoverySearch.ManagedByOrganization = this.recipientSession.SessionSettings.CurrentOrganizationId.ToString();
					discoverySearchDataProvider.Save(mailboxDiscoverySearch);
				}
				else if (this.actionType == HoldAction.Remove)
				{
					discoverySearchDataProvider.Delete(mailboxDiscoverySearch);
				}
				else
				{
					mailboxDiscoverySearch.ManagedByOrganization = "b5d6efcd-1aee-42b9-b168-6fef285fe613";
					discoverySearchDataProvider.Save(mailboxDiscoverySearch);
				}
			}
			List<MailboxHoldStatus> list4 = new List<MailboxHoldStatus>();
			if (string.IsNullOrEmpty(this.inPlaceHoldIdentity))
			{
				if (flag2)
				{
					MailboxSearchHelper.GetMailboxHoldStatuses(mailboxDiscoverySearch, this.recipientSession, list4);
				}
				else
				{
					HoldStatus status = HoldStatus.Failed;
					string additionalInfo = string.Empty;
					if (flag)
					{
						QueryBasedHoldTask queryBasedHoldTask = new QueryBasedHoldTask(CallContext.Current, mailboxDiscoverySearch, discoverySearchDataProvider, this.actionType, this.recipientSession);
						queryBasedHoldTask.Description = string.Format("Task {0} query based hold for id: {1}", (this.actionType == HoldAction.Create) ? "placing" : "removing", this.holdId);
						using (ActivityContext.SuppressThreadScope())
						{
							if (CallContext.Current.WorkloadManager.TrySubmitNewTask(queryBasedHoldTask))
							{
								status = HoldStatus.Pending;
							}
							else
							{
								additionalInfo = CoreResources.GetLocalizedString((CoreResources.IDs)2479091638U);
							}
						}
					}
					for (int i = 0; i < this.mailboxes.Length; i++)
					{
						if (list.Contains(this.mailboxes[i]))
						{
							list4.Add(new MailboxHoldStatus(this.mailboxes[i], HoldStatus.Failed, CoreResources.GetLocalizedString((CoreResources.IDs)2489326695U)));
						}
						else if (list2.Contains(this.mailboxes[i]))
						{
							list4.Add(new MailboxHoldStatus(this.mailboxes[i], HoldStatus.Failed, CoreResources.GetLocalizedString((CoreResources.IDs)4045771774U)));
						}
						else
						{
							list4.Add(new MailboxHoldStatus(this.mailboxes[i], status, additionalInfo));
						}
					}
				}
			}
			return new ServiceResult<MailboxHoldResult>(new MailboxHoldResult(this.holdId, this.query, list4.ToArray()));
		}

		// Token: 0x04001099 RID: 4249
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400109A RID: 4250
		private bool disposed;

		// Token: 0x0400109B RID: 4251
		private HoldAction actionType;

		// Token: 0x0400109C RID: 4252
		private string holdId;

		// Token: 0x0400109D RID: 4253
		private string query;

		// Token: 0x0400109E RID: 4254
		private string[] mailboxes;

		// Token: 0x0400109F RID: 4255
		private string language;

		// Token: 0x040010A0 RID: 4256
		private string inPlaceHoldIdentity;

		// Token: 0x040010A1 RID: 4257
		private string itemHoldPeriod;

		// Token: 0x040010A2 RID: 4258
		private ExchangeRunspaceConfiguration runspaceConfig;

		// Token: 0x040010A3 RID: 4259
		private IRecipientSession recipientSession;

		// Token: 0x040010A4 RID: 4260
		private Dictionary<string, ADRawEntry> dictADRawEntries;
	}
}
