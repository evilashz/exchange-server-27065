﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.AuditLogSearchServicelet;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Servicelets.AuditLogSearch.Messages;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x0200000C RID: 12
	public class TenantWorker
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003942 File Offset: 0x00001B42
		private static ExchangePrincipal GetExchangePrincipalFromADUser(ADUser user)
		{
			return ExchangePrincipal.FromADUser(user.OrganizationId.ToADSessionSettings(), user, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003956 File Offset: 0x00001B56
		internal TenantWorker(WaitHandle stopEvent, int retryIteration)
		{
			this.stopEvent = stopEvent;
			this.retryIteration = retryIteration;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A98 File Offset: 0x00001C98
		internal bool RunSearches(ADUser tenant)
		{
			bool allRequestsComplete = true;
			bool bypassAlert = false;
			Exception exception = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						allRequestsComplete = this.RunSearchesInternal(tenant);
					}
					catch (CannotResolveTenantNameException exception2)
					{
						allRequestsComplete = true;
						exception = exception2;
						bypassAlert = true;
					}
					catch (TenantAccessBlockedException exception3)
					{
						allRequestsComplete = true;
						exception = exception3;
						bypassAlert = true;
					}
					catch (StorageTransientException exception4)
					{
						allRequestsComplete = false;
						exception = exception4;
					}
					catch (StoragePermanentException ex)
					{
						if (ex.InnerException != null && ex.InnerException is CannotResolveTenantNameException)
						{
							allRequestsComplete = true;
							bypassAlert = true;
						}
						else
						{
							allRequestsComplete = false;
						}
						exception = ex;
					}
					catch (DataSourceOperationException ex2)
					{
						ServiceResponseException ex3 = ex2.InnerException as ServiceResponseException;
						if (ex3 != null && ex3.ErrorCode == 263)
						{
							allRequestsComplete = false;
						}
						else
						{
							allRequestsComplete = true;
						}
						exception = ex2;
					}
				});
			}
			catch (GrayException exception)
			{
				allRequestsComplete = true;
				GrayException exception5;
				exception = exception5;
			}
			if (exception != null)
			{
				ExTraceGlobals.WorkerTracer.TraceError(11881L, exception.ToString());
				AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_WorkerException, string.Empty, new object[]
				{
					exception.ToString()
				});
				if (!bypassAlert)
				{
					this.PublishNotification("AuditLogSearchCompletedWithErrors", exception.ToString(), ResultSeverityLevel.Error);
				}
				AuditLogSearchHealthHandler.GetInstance().AuditLogSearchHealth.AddException(exception);
			}
			return allRequestsComplete;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003FF8 File Offset: 0x000021F8
		private bool RunSearchesInternal(ADUser tenant)
		{
			ExTraceGlobals.WorkerTracer.TraceInformation<ADUser>(54361, (long)this.GetHashCode(), "Starting processing all requestss for tenant {0}.", tenant);
			bool flag = true;
			using (IEnumerator<TenantWorker.SearchWorkerBase> enumerator = this.GetSearchWorker(tenant).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TenantWorker.<>c__DisplayClass8 CS$<>8__locals2 = new TenantWorker.<>c__DisplayClass8();
					CS$<>8__locals2.worker = enumerator.Current;
					TenantWorker.<>c__DisplayClassa CS$<>8__locals3 = new TenantWorker.<>c__DisplayClassa();
					DiagnosticContext.Reset();
					ExTraceGlobals.WorkerTracer.TraceInformation<TenantWorker.SearchWorkerBase, ADUser>(94597, (long)this.GetHashCode(), "Starting processing '{0}' for tenant {1}.", CS$<>8__locals2.worker, tenant);
					if (this.stopEvent.WaitOne(0, false))
					{
						ExTraceGlobals.WorkerTracer.TraceInformation<TenantWorker.SearchWorkerBase>(29459, (long)this.GetHashCode(), "Service shutting down, quit return processing {0}", CS$<>8__locals2.worker);
						DiagnosticContext.TraceLocation((LID)58044U);
						return true;
					}
					ExTraceGlobals.FaultInjectionTracer.TraceTest(1354854863U);
					CS$<>8__locals3.requestComplete = false;
					CS$<>8__locals3.errorMessage = null;
					CS$<>8__locals3.lastException = null;
					CS$<>8__locals3.searchHealth = new Search
					{
						UserPrincipalName = ((tenant != null) ? tenant.UserPrincipalName : "Unknown Tenant"),
						Identity = CS$<>8__locals2.worker.Identity,
						RetryAttempt = this.retryIteration
					};
					AuditLogSearchHealthHandler.GetInstance().AuditLogSearchHealth.AddSearch(CS$<>8__locals3.searchHealth);
					using (AuditLogOpticsLogData searchStatistics = new AuditLogOpticsLogData())
					{
						searchStatistics.IsAsynchronous = true;
						searchStatistics.CallResult = false;
						searchStatistics.Retry = this.GetFailureCount(CS$<>8__locals2.worker.Identity);
						try
						{
							GrayException.MapAndReportGrayExceptions(delegate()
							{
								try
								{
									ExTraceGlobals.WorkerTracer.TraceInformation<TenantWorker.SearchWorkerBase>(28705, (long)this.GetHashCode(), "Start getting data for {0}", CS$<>8__locals2.worker);
									if (!CS$<>8__locals2.worker.GetLogEvents(this.stopEvent, searchStatistics, CS$<>8__locals3.searchHealth))
									{
										ExTraceGlobals.WorkerTracer.TraceInformation<TenantWorker.SearchWorkerBase>(29459, (long)this.GetHashCode(), "Service shutting down, return from processing {0}", CS$<>8__locals2.worker);
										DiagnosticContext.TraceLocation((LID)33468U);
									}
									else
									{
										ExTraceGlobals.WorkerTracer.TraceInformation<TenantWorker.SearchWorkerBase>(47332, (long)this.GetHashCode(), "Start sending result email for {0}", CS$<>8__locals2.worker);
										ExTraceGlobals.FaultInjectionTracer.TraceTest(1574464375U);
										CS$<>8__locals2.worker.SendAuditLogSearchResultEmail(tenant, ExTraceGlobals.WorkerTracer, string.Empty);
										AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_AuditLogSearchCompletedSuccessfully, string.Empty, new object[]
										{
											CS$<>8__locals2.worker
										});
										CS$<>8__locals3.requestComplete = true;
									}
								}
								catch (MessageSubmissionExceededException ex2)
								{
									DiagnosticContext.TraceLocation((LID)49852U);
									ExTraceGlobals.WorkerTracer.TraceInformation(96633, (long)this.GetHashCode(), ex2.ToString());
									CS$<>8__locals3.lastException = ex2;
									CS$<>8__locals3.errorMessage = Strings.ErrorAlsSearchResultLargeAttachmentSize;
									CS$<>8__locals3.requestComplete = true;
								}
								catch (TooManyResultsException ex3)
								{
									DiagnosticContext.TraceLocation((LID)48460U);
									ExTraceGlobals.WorkerTracer.TraceInformation(96633, (long)this.GetHashCode(), ex3.ToString());
									CS$<>8__locals3.lastException = ex3;
									CS$<>8__locals3.errorMessage = ex3.Message;
									CS$<>8__locals3.requestComplete = true;
								}
								catch (AdminAuditLogSearchException ex4)
								{
									DiagnosticContext.TraceLocation((LID)64844U);
									ExTraceGlobals.WorkerTracer.TraceError(11881L, ex4.ToString());
									CS$<>8__locals3.lastException = ex4;
								}
								catch (MailboxAuditLogSearchException ex5)
								{
									DiagnosticContext.TraceLocation((LID)40268U);
									ExTraceGlobals.WorkerTracer.TraceError(11881L, ex5.ToString());
									CS$<>8__locals3.lastException = ex5;
								}
								catch (StoragePermanentException ex6)
								{
									DiagnosticContext.TraceLocation((LID)56652U);
									ExTraceGlobals.WorkerTracer.TraceError(11881L, ex6.ToString());
									CS$<>8__locals3.lastException = ex6;
								}
								catch (ADTransientException ex7)
								{
									DiagnosticContext.TraceLocation((LID)44364U);
									ExTraceGlobals.WorkerTracer.TraceError(11881L, ex7.ToString());
									CS$<>8__locals3.lastException = ex7;
								}
								catch (StorageTransientException ex8)
								{
									DiagnosticContext.TraceLocation((LID)44364U);
									ExTraceGlobals.WorkerTracer.TraceError(11881L, ex8.ToString());
									CS$<>8__locals3.lastException = ex8;
									ExTraceGlobals.FaultInjectionTracer.TraceTest(3497182627U);
								}
								catch (AuditLogException ex9)
								{
									DiagnosticContext.TraceLocation((LID)36172U);
									ExTraceGlobals.WorkerTracer.TraceError(11881L, ex9.ToString());
									CS$<>8__locals3.lastException = ex9;
								}
							});
						}
						catch (GrayException ex)
						{
							DiagnosticContext.TraceLocation((LID)52556U);
							ExTraceGlobals.WorkerTracer.TraceError(11881L, ex.ToString());
							CS$<>8__locals3.lastException = ex;
							CS$<>8__locals3.errorMessage = Strings.ErrorAlsEncounteredUnexpectedException;
							CS$<>8__locals3.requestComplete = true;
						}
						finally
						{
							try
							{
								CS$<>8__locals3.searchHealth.Result = (CS$<>8__locals3.lastException == null);
								CS$<>8__locals3.searchHealth.ExceptionDetails = ExceptionDetails.Create(CS$<>8__locals3.lastException);
								if (CS$<>8__locals3.lastException != null)
								{
									DiagnosticContext.TraceLocation((LID)46412U);
									AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_WorkerException, string.Empty, new object[]
									{
										CS$<>8__locals3.lastException.ToString()
									});
									searchStatistics.ErrorCount++;
									searchStatistics.ErrorType = CS$<>8__locals3.lastException;
								}
								if (!CS$<>8__locals3.requestComplete)
								{
									CS$<>8__locals3.requestComplete = this.GiveupAsNecessary(CS$<>8__locals2.worker);
									if (CS$<>8__locals3.requestComplete)
									{
										DiagnosticContext.TraceLocation((LID)62796U);
										CS$<>8__locals3.errorMessage = Strings.ErrorAlsFailedToProcessRequest;
									}
								}
								bool flag2 = CS$<>8__locals3.requestComplete && !string.IsNullOrEmpty(CS$<>8__locals3.errorMessage);
								if (flag2)
								{
									DiagnosticContext.TraceLocation((LID)38220U);
									AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_AuditLogSearchCompletedWithErrors, string.Empty, new object[]
									{
										CS$<>8__locals2.worker,
										CS$<>8__locals3.errorMessage
									});
									CS$<>8__locals2.worker.SendAuditLogSearchResultEmail(tenant, ExTraceGlobals.WorkerTracer, CS$<>8__locals3.errorMessage);
								}
								else
								{
									DiagnosticContext.TraceLocation((LID)54604U);
									bool callResult = CS$<>8__locals3.requestComplete && string.IsNullOrEmpty(CS$<>8__locals3.errorMessage);
									searchStatistics.CallResult = callResult;
								}
								if (CS$<>8__locals3.requestComplete)
								{
									DiagnosticContext.TraceLocation((LID)42316U);
									searchStatistics.RequestDeleted = true;
									CS$<>8__locals2.worker.Cleanup(tenant);
								}
								flag &= CS$<>8__locals3.requestComplete;
							}
							finally
							{
								if (DiagnosticContext.HasData)
								{
									CS$<>8__locals3.searchHealth.DiagnosticContext = AuditingOpticsLogger.GetDiagnosticContextFromThread();
								}
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000044FC File Offset: 0x000026FC
		private void PublishNotification(string notification, string message, ResultSeverityLevel level)
		{
			new EventNotificationItem(ExchangeComponent.Compliance.Name, ExchangeComponent.Compliance.Name, notification, level)
			{
				Message = message
			}.Publish(false);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004534 File Offset: 0x00002734
		private bool GiveupAsNecessary(TenantWorker.SearchWorkerBase worker)
		{
			Guid identity = worker.Identity;
			DiagnosticContext.TraceGuid((LID)58700U, identity);
			int num;
			if (TenantWorker.failedSearches.TryGetValue(identity, out num))
			{
				if (num >= TenantWorker.RetryLimit)
				{
					TenantWorker.failedSearches.Remove(identity);
					DiagnosticContext.TraceDword((LID)34124U, (uint)num);
					DiagnosticContext.TraceDword((LID)50508U, (uint)TenantWorker.RetryLimit);
					return true;
				}
				num++;
			}
			else
			{
				num = 1;
			}
			DiagnosticContext.TraceDword((LID)47436U, (uint)num);
			TenantWorker.failedSearches[identity] = num;
			return false;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000045C8 File Offset: 0x000027C8
		private int GetFailureCount(Guid searchIdentity)
		{
			int result;
			if (TenantWorker.failedSearches.TryGetValue(searchIdentity, out result))
			{
				return result;
			}
			return 0;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000045E8 File Offset: 0x000027E8
		private IList<TenantWorker.SearchWorkerBase> GetSearchWorker(ADUser arbMbx)
		{
			List<TenantWorker.SearchWorkerBase> list = new List<TenantWorker.SearchWorkerBase>();
			ExchangePrincipal exchangePrincipalFromADUser = TenantWorker.GetExchangePrincipalFromADUser(arbMbx);
			if (TenantWorker.directoryAccessor.IsTenantAccessBlocked(arbMbx.OrganizationId))
			{
				ExTraceGlobals.WorkerTracer.TraceInformation(96633, (long)this.GetHashCode(), string.Format("{0} is blocked.", arbMbx.UserPrincipalName));
			}
			else
			{
				AuditLogSearchEwsDataProvider auditLogSearchEwsDataProvider = new AuditLogSearchEwsDataProvider(exchangePrincipalFromADUser);
				foreach (Microsoft.Exchange.Data.Storage.Management.AuditLogSearchBase auditLogSearchBase in auditLogSearchEwsDataProvider.FindIds<Microsoft.Exchange.Data.Storage.Management.AuditLogSearchBase>(null, true, 1000, null))
				{
					if (auditLogSearchBase is Microsoft.Exchange.Data.Storage.Management.AdminAuditLogSearch)
					{
						list.Add(new TenantWorker.AdminWorker(arbMbx.OrganizationId, (Microsoft.Exchange.Data.Storage.Management.AdminAuditLogSearch)auditLogSearchBase));
					}
					else
					{
						list.Add(new TenantWorker.MailboxWorker(arbMbx.OrganizationId, (Microsoft.Exchange.Data.Storage.Management.MailboxAuditLogSearch)auditLogSearchBase));
					}
				}
			}
			return list;
		}

		// Token: 0x0400003B RID: 59
		private const int DaysToRetry = 3;

		// Token: 0x0400003C RID: 60
		private static readonly IDirectoryAccessor directoryAccessor = new DirectoryAccessor();

		// Token: 0x0400003D RID: 61
		private static readonly int RetryLimit = 3 * (AuditLogSearchRetryPolicy.RetryLimit + 1) - 1;

		// Token: 0x0400003E RID: 62
		private static readonly IDictionary<Guid, int> failedSearches = new ConcurrentDictionary<Guid, int>();

		// Token: 0x0400003F RID: 63
		private readonly WaitHandle stopEvent;

		// Token: 0x04000040 RID: 64
		private readonly int retryIteration;

		// Token: 0x0200000D RID: 13
		internal abstract class SearchWorkerBase
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600007A RID: 122 RVA: 0x000046EA File Offset: 0x000028EA
			internal Guid Identity
			{
				get
				{
					return ((AuditLogSearchId)this.searchBase.Identity).Guid;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x0600007B RID: 123 RVA: 0x00004701 File Offset: 0x00002901
			protected List<Participant> StatusEmailRecipients
			{
				get
				{
					if (this.statusEmailRecipients == null)
					{
						this.PrepareRecipients();
					}
					return this.statusEmailRecipients;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x0600007C RID: 124 RVA: 0x00004717 File Offset: 0x00002917
			protected ADRecipient Requester
			{
				get
				{
					if (this.requester == null)
					{
						this.PrepareRecipients();
					}
					return this.requester;
				}
			}

			// Token: 0x0600007D RID: 125
			public abstract bool GetLogEvents(WaitHandle stopEvent, AuditLogOpticsLogData searchStatistics, Search searchHealth);

			// Token: 0x0600007E RID: 126 RVA: 0x00004730 File Offset: 0x00002930
			private EmailAddressType CreateEWSEmailAddressType(Participant participant)
			{
				return new EmailAddressType
				{
					EmailAddress = participant.EmailAddress,
					RoutingType = participant.RoutingType,
					OriginalDisplayName = participant.OriginalDisplayName
				};
			}

			// Token: 0x0600007F RID: 127 RVA: 0x00004774 File Offset: 0x00002974
			private MessageType CreateEWSMessageType(string errorMessage)
			{
				bool flag = string.IsNullOrEmpty(errorMessage);
				MessageType messageType = new MessageType();
				messageType.From = new SingleRecipientType
				{
					Item = this.CreateEWSEmailAddressType(new Participant(AuditLogSearchContext.Sender))
				};
				List<EmailAddressType> list = new List<EmailAddressType>();
				if (!flag && this.Requester != null)
				{
					list.Add(this.CreateEWSEmailAddressType(new Participant(this.Requester)));
				}
				list.AddRange(from participant in this.StatusEmailRecipients
				select this.CreateEWSEmailAddressType(participant));
				messageType.ToRecipients = list.ToArray();
				if (flag)
				{
					this.PrepareAttachment(messageType);
				}
				this.PrepareSubjectAndBody(messageType, errorMessage);
				return messageType;
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00004818 File Offset: 0x00002A18
			public void SendAuditLogSearchResultEmail(ADUser tenant, Trace tracer, string errorMessage)
			{
				EwsAuditClient ewsAuditClient = new EwsAuditClient(new EwsConnectionManager(TenantWorker.GetExchangePrincipalFromADUser(tenant), OpenAsAdminOrSystemServiceBudgetTypeType.Default, tracer), TimeSpan.FromSeconds(120.0), tracer);
				CreateItemType item = new CreateItemType
				{
					MessageDisposition = MessageDispositionType.SendOnly,
					MessageDispositionSpecified = true,
					Items = new NonEmptyArrayOfAllItemsType
					{
						Items = new ItemType[]
						{
							this.CreateEWSMessageType(errorMessage)
						}
					}
				};
				DiagnosticContext.TraceLocation((LID)63820U);
				ewsAuditClient.CreateItem(item);
			}

			// Token: 0x06000081 RID: 129 RVA: 0x0000489C File Offset: 0x00002A9C
			internal void Cleanup(ADUser arbMbx)
			{
				string text = (this.ewsSearchRequest.Identity != null) ? this.ewsSearchRequest.Identity.ToString() : string.Empty;
				try
				{
					AuditQueueType queueType = (this.ewsSearchRequest is Microsoft.Exchange.Data.Storage.Management.AdminAuditLogSearch) ? AuditQueueType.AsyncAdminSearch : AuditQueueType.AsyncMailboxSearch;
					AuditLogSearchEwsDataProvider auditLogSearchEwsDataProvider = new AuditLogSearchEwsDataProvider(TenantWorker.GetExchangePrincipalFromADUser(arbMbx));
					auditLogSearchEwsDataProvider.DeleteAuditLogSearch(this.ewsSearchRequest);
					AuditQueuesOpticsLogData auditQueuesOpticsLogData = new AuditQueuesOpticsLogData
					{
						QueueType = queueType,
						EventType = QueueEventType.Remove,
						CorrelationId = text,
						OrganizationId = this.searchBase.OrganizationId
					};
					auditQueuesOpticsLogData.Log();
				}
				catch (InvalidOperationException ex)
				{
					DiagnosticContext.TraceLocation((LID)39244U);
					ExTraceGlobals.WorkerTracer.TraceInformation<string, string>(96633, (long)this.GetHashCode(), "The deletion corresponding to Guid '{0}' failed: ", text, ex.Message);
				}
			}

			// Token: 0x06000082 RID: 130
			protected abstract void PrepareSubjectAndBody(MessageType item, string errorMessage);

			// Token: 0x06000083 RID: 131
			protected abstract XElement PrepareXElement();

			// Token: 0x06000084 RID: 132 RVA: 0x0000497C File Offset: 0x00002B7C
			private void PrepareRecipients()
			{
				if (this.searchBase.CreatedByEx != null && !string.IsNullOrEmpty(this.searchBase.CreatedByEx.ToString()))
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(this.searchBase.CreatedByEx);
					IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 760, "PrepareRecipients", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\AuditLogSearch\\Program\\Worker.cs");
					this.requester = tenantOrRootOrgRecipientSession.Read(this.searchBase.CreatedByEx);
				}
				this.statusEmailRecipients = new List<Participant>();
				foreach (SmtpAddress smtpAddress in this.searchBase.StatusMailRecipients)
				{
					Participant participant;
					if (Participant.TryParse(smtpAddress.ToString(), out participant) && participant != null)
					{
						this.statusEmailRecipients.Add(participant);
					}
				}
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00004A70 File Offset: 0x00002C70
			private void PrepareAttachment(MessageType messageItem)
			{
				XElement xelement = this.PrepareXElement();
				if (xelement != null)
				{
					using (StringWriter stringWriter = new StringWriter())
					{
						xelement.Save(stringWriter);
						stringWriter.Flush();
						messageItem.Attachments = new AttachmentType[]
						{
							new FileAttachmentType
							{
								Name = "SearchResult.xml",
								Content = Encoding.Unicode.GetBytes(stringWriter.ToString())
							}
						};
					}
				}
			}

			// Token: 0x04000041 RID: 65
			public const string RootName = "SearchResults";

			// Token: 0x04000042 RID: 66
			public const string ElementName = "Event";

			// Token: 0x04000043 RID: 67
			private const string AttachmentName = "SearchResult.xml";

			// Token: 0x04000044 RID: 68
			protected Microsoft.Exchange.Management.SystemConfigurationTasks.AuditLogSearchBase searchBase;

			// Token: 0x04000045 RID: 69
			protected Microsoft.Exchange.Data.Storage.Management.AuditLogSearchBase ewsSearchRequest;

			// Token: 0x04000046 RID: 70
			private ADRecipient requester;

			// Token: 0x04000047 RID: 71
			private List<Participant> statusEmailRecipients;
		}

		// Token: 0x0200000E RID: 14
		internal sealed class AdminWorker : TenantWorker.SearchWorkerBase
		{
			// Token: 0x06000088 RID: 136 RVA: 0x00004AF8 File Offset: 0x00002CF8
			public AdminWorker(OrganizationId organizationId, Microsoft.Exchange.Data.Storage.Management.AdminAuditLogSearch item)
			{
				this.ewsSearchRequest = item;
				this.searchBase = (this.searchCriteria = new Microsoft.Exchange.Management.SystemConfigurationTasks.AdminAuditLogSearch());
				this.searchCriteria.OrganizationId = organizationId;
				this.searchCriteria.Initialize(item);
				AuditLogSearchId auditLogSearchId = (this.searchBase.Identity != null) ? (this.searchBase.Identity as AuditLogSearchId) : null;
				AuditQueuesOpticsLogData auditQueuesOpticsLogData = new AuditQueuesOpticsLogData
				{
					QueueType = AuditQueueType.AsyncAdminSearch,
					EventType = QueueEventType.Peek,
					CorrelationId = ((auditLogSearchId != null) ? auditLogSearchId.Guid.ToString() : string.Empty),
					OrganizationId = organizationId
				};
				auditQueuesOpticsLogData.Log();
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00004BA8 File Offset: 0x00002DA8
			public override bool GetLogEvents(WaitHandle stopEvent, AuditLogOpticsLogData searchStatistics, Search searchHealth)
			{
				searchStatistics.SearchType = "Admin";
				searchStatistics.OrganizationId = this.searchCriteria.OrganizationId;
				searchStatistics.QueryComplexity = this.searchCriteria.QueryComplexity;
				searchStatistics.ShowDetails = true;
				searchStatistics.MailboxCount = 1;
				searchHealth.Kind = AuditSearchKind.Admin;
				AdminAuditLogSearchWorker adminAuditLogSearchWorker = new AdminAuditLogSearchWorker(7200, this.searchCriteria, searchStatistics);
				this.results = adminAuditLogSearchWorker.Search();
				return true;
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00004C18 File Offset: 0x00002E18
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}({1}, AdminAuditLogSearch)", new object[]
				{
					this.searchCriteria.Identity,
					this.searchCriteria.Name
				});
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00004C58 File Offset: 0x00002E58
			protected override void PrepareSubjectAndBody(MessageType item, string errorMessage)
			{
				bool flag = string.IsNullOrEmpty(errorMessage);
				if (flag)
				{
					DiagnosticContext.TraceLocation((LID)55628U);
					string requester = (base.Requester == null) ? this.searchBase.CreatedBy : base.Requester.DisplayName;
					item.Subject = Strings.AlsSubjectAdmin(this.searchCriteria.Name, requester);
				}
				else
				{
					DiagnosticContext.TraceLocation((LID)43340U);
					item.Subject = Strings.AlsSubjectAdminFailure(this.searchCriteria.Name);
				}
				using (StringWriter stringWriter = new StringWriter())
				{
					if (!flag)
					{
						stringWriter.WriteLine(errorMessage);
						stringWriter.WriteLine();
					}
					stringWriter.WriteLine(Strings.AlsEmailBodyCriteria);
					stringWriter.WriteLine();
					stringWriter.WriteLine(Strings.AlsEmailBodyStartDate(this.searchCriteria.StartDateUtc.ToString()));
					stringWriter.WriteLine(Strings.AlsEmailBodyEndDate(this.searchCriteria.EndDateUtc.ToString()));
					stringWriter.WriteLine(Strings.AlsEmailBodyAdminCmdlets(string.Join(", ", this.searchCriteria.Cmdlets.ToArray())));
					stringWriter.WriteLine(Strings.AlsEmailBodyAdminParamters(string.Join(", ", this.searchCriteria.Parameters.ToArray())));
					stringWriter.WriteLine(Strings.AlsEmailBodyAdminObjectIds(string.Join(", ", this.searchCriteria.ObjectIds.ToArray())));
					stringWriter.WriteLine(Strings.AlsEmailBodyAdminUserIds(string.Join(", ", this.searchCriteria.UserIds.ToArray())));
					stringWriter.WriteLine(Strings.AlsEmailBodyExternalAccess(this.searchCriteria.ExternalAccess));
					stringWriter.WriteLine();
					stringWriter.WriteLine();
					stringWriter.WriteLine(Strings.AlsEmailBodySubmitted(this.searchCriteria.CreationTime.ToString()));
					stringWriter.WriteLine(Strings.AlsEmailFooter);
					stringWriter.Flush();
					item.Body = new BodyType
					{
						BodyType1 = BodyTypeType.Text,
						Value = stringWriter.ToString()
					};
				}
			}

			// Token: 0x0600008C RID: 140 RVA: 0x00004ED8 File Offset: 0x000030D8
			protected override XElement PrepareXElement()
			{
				if (this.results == null)
				{
					this.results = new AdminAuditLogEvent[0];
				}
				return new XElement("SearchResults", from x in this.results
				select new XElement("Event", this.SkipNullOrEmptyValues(x)));
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00004FD4 File Offset: 0x000031D4
			private object[] SkipNullOrEmptyValues(AdminAuditLogEvent log)
			{
				List<object> list = new List<object>();
				list.AddRange(new object[]
				{
					new XAttribute("Caller", log.Caller),
					new XAttribute("Cmdlet", log.CmdletName)
				});
				if (log.RunDate != null)
				{
					list.Add(new XAttribute("RunDate", log.RunDate));
				}
				if (log.Succeeded != null)
				{
					list.Add(new XAttribute("Succeeded", log.Succeeded));
				}
				if (!string.IsNullOrEmpty(log.ObjectModified))
				{
					list.Add(new XAttribute("ObjectModified", log.ObjectModified));
				}
				if (!string.IsNullOrEmpty(log.Error))
				{
					list.Add(new XAttribute("Error", log.Error));
				}
				if (log.ExternalAccess != null)
				{
					list.Add(new XAttribute("ExternalAccess", log.ExternalAccess));
				}
				if (!string.IsNullOrEmpty(log.OriginatingServer))
				{
					list.Add(new XAttribute("OriginatingServer", log.OriginatingServer));
				}
				if (log.CmdletParameters != null && log.CmdletParameters.Count > 0)
				{
					list.Add(new XElement("CmdletParameters", from y in log.CmdletParameters
					select new XElement("Parameter", new object[]
					{
						new XAttribute("Name", y.Name),
						new XAttribute("Value", y.Value)
					})));
				}
				if (log.ModifiedProperties != null && log.ModifiedProperties.Count > 0)
				{
					list.Add(new XElement("ModifiedProperties", from y in log.ModifiedProperties
					select new XElement("Property", new object[]
					{
						new XAttribute("Name", y.Name),
						new XAttribute("OldValue", y.OldValue),
						new XAttribute("NewValue", y.NewValue)
					})));
				}
				return list.ToArray();
			}

			// Token: 0x04000048 RID: 72
			private const int SearchWorkerTimeoutInSeconds = 7200;

			// Token: 0x04000049 RID: 73
			private readonly Microsoft.Exchange.Management.SystemConfigurationTasks.AdminAuditLogSearch searchCriteria;

			// Token: 0x0400004A RID: 74
			private AdminAuditLogEvent[] results;
		}

		// Token: 0x0200000F RID: 15
		internal sealed class MailboxWorker : TenantWorker.SearchWorkerBase
		{
			// Token: 0x06000091 RID: 145 RVA: 0x000051DC File Offset: 0x000033DC
			public MailboxWorker(OrganizationId organizationId, Microsoft.Exchange.Data.Storage.Management.MailboxAuditLogSearch item)
			{
				this.ewsSearchRequest = item;
				ADSessionSettings sessionSettings = organizationId.ToADSessionSettings();
				this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.FullyConsistent, sessionSettings, 1067, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\AuditLogSearch\\Program\\Worker.cs");
				this.searchBase = (this.searchCriteria = new Microsoft.Exchange.Management.SystemConfigurationTasks.MailboxAuditLogSearch());
				this.searchCriteria.OrganizationId = organizationId;
				this.searchCriteria.Initialize(item);
				AuditLogSearchId auditLogSearchId = (this.searchBase.Identity != null) ? (this.searchBase.Identity as AuditLogSearchId) : null;
				AuditQueuesOpticsLogData auditQueuesOpticsLogData = new AuditQueuesOpticsLogData
				{
					QueueType = AuditQueueType.AsyncMailboxSearch,
					EventType = QueueEventType.Peek,
					CorrelationId = ((auditLogSearchId != null) ? auditLogSearchId.Guid.ToString() : string.Empty),
					OrganizationId = organizationId
				};
				auditQueuesOpticsLogData.Log();
			}

			// Token: 0x06000092 RID: 146 RVA: 0x000052B8 File Offset: 0x000034B8
			public override bool GetLogEvents(WaitHandle stopEvent, AuditLogOpticsLogData searchStatistics, Search searchHealth)
			{
				searchHealth.Kind = AuditSearchKind.Mailbox;
				searchStatistics.SearchType = "Mailbox";
				searchStatistics.OrganizationId = this.searchCriteria.OrganizationId;
				searchStatistics.QueryComplexity = this.searchCriteria.QueryComplexity;
				searchStatistics.ShowDetails = this.searchCriteria.ShowDetails;
				MailboxAuditLogSearchWorker mailboxAuditLogSearchWorker = new MailboxAuditLogSearchWorker(600, this.searchCriteria, Unlimited<int>.UnlimitedValue, searchStatistics);
				IEnumerable<ADRecipient> applicableMailboxes = this.GetApplicableMailboxes();
				this.results = new List<MailboxAuditLogRecord>();
				foreach (ADUser aduser in applicableMailboxes.OfType<ADUser>())
				{
					searchStatistics.MailboxCount++;
					DiagnosticContext.TraceGuid((LID)59724U, aduser.ExchangeGuid);
					DiagnosticContext.TraceDword((LID)35148U, (uint)searchStatistics.MailboxCount);
					string lastProcessedMailbox = aduser.ExchangeGuid.ToString();
					searchHealth.LastProcessedMailbox = lastProcessedMailbox;
					searchHealth.MailboxCount++;
					searchStatistics.LastProcessedMailbox = lastProcessedMailbox;
					foreach (MailboxAuditLogRecord item in mailboxAuditLogSearchWorker.SearchMailboxAudits(aduser))
					{
						this.results.Add(item);
						searchHealth.ResultCount++;
					}
					DiagnosticContext.TraceDword((LID)51532U, (uint)this.results.Count);
					if (this.results.Count >= 50000)
					{
						DiagnosticContext.TraceDword((LID)45388U, 50000U);
						throw new TooManyResultsException(this.results.Count);
					}
					if (stopEvent.WaitOne(0, false))
					{
						DiagnosticContext.TraceLocation((LID)61772U);
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000093 RID: 147 RVA: 0x000054C4 File Offset: 0x000036C4
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}({1}, MailboxAuditLogSearch)", new object[]
				{
					this.searchCriteria.Identity,
					this.searchCriteria.Name
				});
			}

			// Token: 0x06000094 RID: 148 RVA: 0x0000550C File Offset: 0x0000370C
			protected override void PrepareSubjectAndBody(MessageType item, string errorMessage)
			{
				bool flag = string.IsNullOrEmpty(errorMessage);
				if (flag)
				{
					string requester = (base.Requester == null) ? this.searchBase.CreatedBy : base.Requester.DisplayName;
					item.Subject = Strings.AlsSubjectMailbox(this.searchCriteria.Name, requester);
				}
				else
				{
					item.Subject = Strings.AlsSubjectMailboxFailure(this.searchCriteria.Name);
				}
				using (StringWriter stringWriter = new StringWriter())
				{
					if (!flag)
					{
						stringWriter.WriteLine(errorMessage);
						stringWriter.WriteLine();
					}
					stringWriter.WriteLine(Strings.AlsEmailBodyCriteria);
					stringWriter.WriteLine();
					stringWriter.WriteLine(Strings.AlsEmailBodyStartDate(this.searchCriteria.StartDateUtc.ToString()));
					stringWriter.WriteLine(Strings.AlsEmailBodyEndDate(this.searchCriteria.EndDateUtc.ToString()));
					string mailbox = string.Empty;
					if (this.searchCriteria.Mailboxes != null && this.searchCriteria.Mailboxes.Count > 0)
					{
						mailbox = string.Join(", ", (from id in this.searchCriteria.Mailboxes
						select id.ToString()).ToArray<string>());
					}
					stringWriter.WriteLine(Strings.AlsEmailBodyMailboxIds(mailbox));
					stringWriter.WriteLine(Strings.AlsEmailBodyMailboxLogonTypes(string.Join(", ", this.searchCriteria.LogonTypes.ToArray())));
					stringWriter.WriteLine(Strings.AlsEmailBodyMailboxOperations(string.Join(", ", this.searchCriteria.Operations.ToArray())));
					stringWriter.WriteLine(Strings.AlsEmailBodyMailboxShowDetails(this.searchCriteria.ShowDetails));
					stringWriter.WriteLine(Strings.AlsEmailBodyExternalAccess(this.searchCriteria.ExternalAccess));
					stringWriter.WriteLine();
					stringWriter.WriteLine();
					stringWriter.WriteLine(Strings.AlsEmailBodySubmitted(this.searchCriteria.CreationTime.ToString()));
					stringWriter.WriteLine(Strings.AlsEmailFooter);
					stringWriter.Flush();
					item.Body = new BodyType
					{
						BodyType1 = BodyTypeType.Text,
						Value = stringWriter.ToString()
					};
				}
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00005964 File Offset: 0x00003B64
			protected override XElement PrepareXElement()
			{
				if (this.results.Count == 0 || this.results[0] is MailboxAuditLogEvent)
				{
					return new XElement("SearchResults", from x in this.results
					let y = (MailboxAuditLogEvent)x
					select new XElement("Event", this.SkipNullOrEmptyValues(y)));
				}
				Func<MailboxAuditLogRecord, XElement> createElement = delegate(MailboxAuditLogRecord x)
				{
					XElement xelement = new XElement("Event", new object[]
					{
						new XAttribute("MailboxGuid", x.MailboxGuid),
						new XAttribute("Owner", x.MailboxResolvedOwnerName)
					});
					xelement.SetAttributeValue("LastAccessed", x.LastAccessed);
					return xelement;
				};
				return new XElement("SearchResults", from x in this.results
				select createElement(x));
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00005A30 File Offset: 0x00003C30
			private IEnumerable<ADRecipient> GetApplicableMailboxes()
			{
				if (this.searchCriteria.Mailboxes != null && this.searchCriteria.Mailboxes.Count > 0)
				{
					return this.GetRecipientsFromMailboxIds();
				}
				return this.recipientSession.FindPaged(null, QueryScope.SubTree, this.searchCriteria.GetRecipientFilter(), null, 0);
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00005CC4 File Offset: 0x00003EC4
			private IEnumerable<ADRecipient> GetRecipientsFromMailboxIds()
			{
				foreach (ADObjectId id in this.searchCriteria.Mailboxes)
				{
					MailboxIdParameter mailboxId = new MailboxIdParameter(id);
					IEnumerable<ADRecipient> recipientList = mailboxId.GetObjects<ADRecipient>(null, this.recipientSession);
					foreach (ADRecipient recipient in recipientList)
					{
						yield return recipient;
					}
				}
				yield break;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00005D14 File Offset: 0x00003F14
			private object[] SkipNullOrEmptyValues(MailboxAuditLogEvent log)
			{
				List<object> list = new List<object>();
				list.AddRange(new object[]
				{
					new XAttribute("MailboxGuid", log.MailboxGuid),
					new XAttribute("Owner", log.MailboxResolvedOwnerName)
				});
				if (log.LastAccessed != null)
				{
					list.Add(new XAttribute("LastAccessed", log.LastAccessed));
				}
				if (!string.IsNullOrEmpty(log.Operation))
				{
					list.Add(new XAttribute("Operation", log.Operation));
				}
				if (!string.IsNullOrEmpty(log.ItemId))
				{
					list.Add(new XAttribute("ItemId", log.ItemId));
				}
				if (!string.IsNullOrEmpty(log.ItemSubject))
				{
					list.Add(new XAttribute("ItemSubject", log.ItemSubject));
				}
				if (!string.IsNullOrEmpty(log.OperationResult))
				{
					list.Add(new XAttribute("OperationResult", log.OperationResult));
				}
				if (!string.IsNullOrEmpty(log.LogonType))
				{
					list.Add(new XAttribute("LogonType", log.LogonType));
				}
				if (!string.IsNullOrEmpty(log.DestFolderId))
				{
					list.Add(new XAttribute("DestFolderId", log.DestFolderId));
				}
				if (!string.IsNullOrEmpty(log.DestFolderPathName))
				{
					list.Add(new XAttribute("DestFolderPathName", log.DestFolderPathName));
				}
				if (!string.IsNullOrEmpty(log.FolderId))
				{
					list.Add(new XAttribute("FolderId", log.FolderId));
				}
				if (!string.IsNullOrEmpty(log.FolderPathName))
				{
					list.Add(new XAttribute("FolderPathName", log.FolderPathName));
				}
				if (!string.IsNullOrEmpty(log.ClientInfoString))
				{
					list.Add(new XAttribute("ClientInfoString", log.ClientInfoString));
				}
				if (!string.IsNullOrEmpty(log.ClientIPAddress))
				{
					list.Add(new XAttribute("ClientIPAddress", log.ClientIPAddress));
				}
				if (!string.IsNullOrEmpty(log.ClientMachineName))
				{
					list.Add(new XAttribute("ClientMachineName", log.ClientMachineName));
				}
				if (!string.IsNullOrEmpty(log.ClientProcessName))
				{
					list.Add(new XAttribute("ClientProcessName", log.ClientProcessName));
				}
				if (!string.IsNullOrEmpty(log.ClientVersion))
				{
					list.Add(new XAttribute("ClientVersion", log.ClientVersion));
				}
				if (!string.IsNullOrEmpty(log.InternalLogonType))
				{
					list.Add(new XAttribute("InternalLogonType", log.InternalLogonType));
				}
				if (!string.IsNullOrEmpty(log.MailboxOwnerUPN))
				{
					list.Add(new XAttribute("MailboxOwnerUPN", log.MailboxOwnerUPN));
				}
				if (!string.IsNullOrEmpty(log.MailboxOwnerSid))
				{
					list.Add(new XAttribute("MailboxOwnerSid", log.MailboxOwnerSid));
				}
				if (!string.IsNullOrEmpty(log.DestMailboxOwnerUPN))
				{
					list.Add(new XAttribute("DestMailboxOwnerUPN", log.DestMailboxOwnerUPN));
				}
				if (!string.IsNullOrEmpty(log.DestMailboxOwnerSid))
				{
					list.Add(new XAttribute("DestMailboxOwnerSid", log.DestMailboxOwnerSid));
				}
				if (!string.IsNullOrEmpty(log.DestMailboxGuid))
				{
					list.Add(new XAttribute("DestMailboxGuid", log.DestMailboxGuid));
				}
				if (log.CrossMailboxOperation != null)
				{
					list.Add(new XAttribute("CrossMailboxOperation", log.CrossMailboxOperation));
				}
				if (!string.IsNullOrEmpty(log.LogonUserDisplayName))
				{
					list.Add(new XAttribute("LogonUserDisplayName", log.LogonUserDisplayName));
				}
				if (!string.IsNullOrEmpty(log.LogonUserSid))
				{
					list.Add(new XAttribute("LogonUserSid", log.LogonUserSid));
				}
				if (!string.IsNullOrEmpty(log.DirtyProperties))
				{
					list.Add(new XAttribute("DirtyProperties", log.DirtyProperties));
				}
				if (log.SourceItems != null && log.SourceItems.Count > 0)
				{
					list.Add(new XElement("SourceItems", from si in log.SourceItems
					select new XElement("Item", this.GetItemAttributes(si))));
				}
				if (log.SourceFolders != null && log.SourceFolders.Count > 0)
				{
					list.Add(new XElement("SourceFolders", from sf in log.SourceFolders
					select new XElement("Folder", this.GetFolderAttributes(sf))));
				}
				if (!string.IsNullOrEmpty(log.OriginatingServer))
				{
					list.Add(new XAttribute("OriginatingServer", log.OriginatingServer));
				}
				return list.ToArray();
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00006220 File Offset: 0x00004420
			private object[] GetFolderAttributes(MailboxAuditLogSourceFolder sf)
			{
				List<XAttribute> list = new List<XAttribute>();
				if (!string.IsNullOrEmpty(sf.SourceFolderId))
				{
					list.Add(new XAttribute("Id", sf.SourceFolderId));
				}
				if (!string.IsNullOrEmpty(sf.SourceFolderPathName))
				{
					list.Add(new XAttribute("PathName", sf.SourceFolderPathName));
				}
				return list.ToArray();
			}

			// Token: 0x0600009A RID: 154 RVA: 0x0000628C File Offset: 0x0000448C
			private object[] GetItemAttributes(MailboxAuditLogSourceItem si)
			{
				List<XAttribute> list = new List<XAttribute>();
				if (!string.IsNullOrEmpty(si.SourceItemId))
				{
					list.Add(new XAttribute("Id", si.SourceItemId));
				}
				if (!string.IsNullOrEmpty(si.SourceItemSubject))
				{
					list.Add(new XAttribute("Subject", si.SourceItemSubject));
				}
				if (!string.IsNullOrEmpty(si.SourceItemFolderPathName))
				{
					list.Add(new XAttribute("FolderPathName", si.SourceItemFolderPathName));
				}
				return list.ToArray();
			}

			// Token: 0x0400004D RID: 77
			private readonly IRecipientSession recipientSession;

			// Token: 0x0400004E RID: 78
			private readonly Microsoft.Exchange.Management.SystemConfigurationTasks.MailboxAuditLogSearch searchCriteria;

			// Token: 0x0400004F RID: 79
			private List<MailboxAuditLogRecord> results;
		}
	}
}
