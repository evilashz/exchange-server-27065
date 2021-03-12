using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003BE RID: 958
	internal class DeliveryAgentConnection
	{
		// Token: 0x06002BE4 RID: 11236 RVA: 0x000AF811 File Offset: 0x000ADA11
		public DeliveryAgentConnection(NextHopConnection nextHopConnection, DeliveryAgentMExEvents mexEvents) : this(nextHopConnection, mexEvents, null, new DeliveryAgentConnection.Stats())
		{
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000AF824 File Offset: 0x000ADA24
		public DeliveryAgentConnection(NextHopConnection nextHopConnection, DeliveryAgentMExEvents mexEvents, DeliveryAgentConnector connector, DeliveryAgentConnection.Stats stats)
		{
			if (nextHopConnection == null)
			{
				throw new ArgumentNullException("nextHopConnection");
			}
			if (mexEvents == null)
			{
				throw new ArgumentNullException("mexEvents");
			}
			this.nextHopConnection = nextHopConnection;
			this.mexEvents = mexEvents;
			this.connector = connector;
			this.stats = stats;
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000AF87B File Offset: 0x000ADA7B
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.DeliveryAgentsTracer;
			}
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000AF884 File Offset: 0x000ADA84
		public IAsyncResult BeginConnection(object asyncState, AsyncCallback asyncCallback)
		{
			this.connectionAsyncResult = new DeliveryAgentConnection.AsyncResult(asyncState, asyncCallback);
			SmtpResponse smtpResponse;
			if (this.connector != null || this.TryGetDeliveryAgentConnector(out this.connector, out smtpResponse))
			{
				DeliveryAgentConnection.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Created a delivery agent connection for domain {0} using protocol {1}.", this.nextHopConnection.Key.NextHopDomain, this.connector.DeliveryProtocol);
				this.stats.Initialize(this.connector.DeliveryProtocol);
				this.ExecuteNextState(DeliveryAgentConnection.State.OnOpenConnection);
			}
			else
			{
				DeliveryAgentConnection.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Could not get the delivery agent connector for connector {0}.", this.nextHopConnection.Key.NextHopConnector);
				this.AckConnection(AckStatus.Resubmit, smtpResponse);
				this.CompleteAsyncOperation();
			}
			return this.connectionAsyncResult;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000AF946 File Offset: 0x000ADB46
		public void EndConnection(IAsyncResult ar)
		{
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000AF948 File Offset: 0x000ADB48
		public void Retire()
		{
			this.retire = true;
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000AF954 File Offset: 0x000ADB54
		private bool TryGetDeliveryAgentConnector(out DeliveryAgentConnector connector, out SmtpResponse failureResponse)
		{
			if (!Components.RoutingComponent.MailRouter.TryGetLocalSendConnector<DeliveryAgentConnector>(this.nextHopConnection.Key.NextHopConnector, out connector))
			{
				DeliveryAgentConnection.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Connector {0} could not be found", this.nextHopConnection.Key.NextHopConnector);
				failureResponse = AckReason.DeliveryAgentConnectorDeleted;
				return false;
			}
			failureResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000AF9C8 File Offset: 0x000ADBC8
		private void ExecuteNextState(DeliveryAgentConnection.State nextState)
		{
			while (nextState != DeliveryAgentConnection.State.Done)
			{
				switch (nextState)
				{
				case DeliveryAgentConnection.State.OnOpenConnection:
					nextState = this.RaiseOnOpenConnectionEvent();
					break;
				case DeliveryAgentConnection.State.OnDeliverMailItem:
					nextState = this.RaiseOnDeliverMailItemEvent();
					break;
				case DeliveryAgentConnection.State.OnCloseConnection:
					nextState = this.RaiseOnCloseConnectionEvent();
					break;
				default:
					throw new InvalidOperationException("Invalid next state: " + nextState);
				}
			}
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000AFA28 File Offset: 0x000ADC28
		private DeliveryAgentConnection.State RaiseOnOpenConnectionEvent()
		{
			DeliveryAgentConnection.State result = DeliveryAgentConnection.State.Done;
			if (this.GetCurrentMailItem() == null)
			{
				DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "No mail item available, returning without raising OnOpenConnection event.");
				this.CompleteAsyncOperation();
			}
			else
			{
				DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "Retrieved first mail item for this session.  Raising OnOpenConnection event.");
				this.CreateMexSession(false);
				RoutedMailItemWrapper deliverableMailItem = this.CreateDeliverableMailItem();
				InternalOpenConnectionEventSource internalOpenConnectionEventSource = new InternalOpenConnectionEventSource(this.mexSession, deliverableMailItem, this.sessionId, this.nextHopConnection, this.stats);
				InternalOpenConnectionEventArgs internalOpenConnectionEventArgs = new InternalOpenConnectionEventArgs(deliverableMailItem, this.nextHopConnection.Key.NextHopDomain);
				this.TrackBeginAgentInvocation(this.currentMailItem, LatencyComponent.DeliveryAgentOnOpenConnection);
				IAsyncResult asyncResult = this.mexEvents.RaiseEvent(this.mexSession, "OnOpenConnection", new AsyncCallback(this.OnOpenConnectionEventCompleted), internalOpenConnectionEventSource, new object[]
				{
					internalOpenConnectionEventSource,
					internalOpenConnectionEventArgs
				});
				if (asyncResult.CompletedSynchronously)
				{
					result = this.HandleOpenConnectionEventCompleted(asyncResult);
				}
			}
			return result;
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000AFB18 File Offset: 0x000ADD18
		private void OnOpenConnectionEventCompleted(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				DeliveryAgentConnection.State nextState = this.HandleOpenConnectionEventCompleted(ar);
				this.ExecuteNextState(nextState);
			}
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000AFB3C File Offset: 0x000ADD3C
		private DeliveryAgentConnection.State HandleOpenConnectionEventCompleted(IAsyncResult ar)
		{
			DeliveryAgentConnection.State result = DeliveryAgentConnection.State.Done;
			InternalDeliveryAgentEventSource internalEventSource = ((InternalOpenConnectionEventSource)ar.AsyncState).InternalEventSource;
			bool flag = this.EndEvent(ar, internalEventSource);
			if (flag && internalEventSource.ConnectionRegistered)
			{
				DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "The agent registered a connection.  Moving on to OnDeliverMailItem event.");
				this.remoteHost = internalEventSource.RemoteHost;
				result = DeliveryAgentConnection.State.OnDeliverMailItem;
				this.nextHopConnection.ConnectionAttemptSucceeded();
			}
			else
			{
				DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "No agent registered a connection.");
				if (!internalEventSource.MessageAcked)
				{
					internalEventSource.AckMailItemPending();
				}
				if (!internalEventSource.ConnectionUnregistered)
				{
					this.AckConnection(AckStatus.Retry, DeliveryAgentConnection.ConnectionNotHandledResponse);
				}
				this.CompleteAsyncOperation();
			}
			this.ReleaseMexSession();
			return result;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000AFBE8 File Offset: 0x000ADDE8
		private DeliveryAgentConnection.State RaiseOnDeliverMailItemEvent()
		{
			DeliveryAgentConnection.State result = DeliveryAgentConnection.State.Done;
			if (this.stats.NumMessagesDelivered == this.connector.MaxMessagesPerConnection)
			{
				DeliveryAgentConnection.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Already delivered the max messages allowed per connection, {0}.  Moving to OnCloseConnection.", this.connector.MaxMessagesPerConnection);
				result = DeliveryAgentConnection.State.OnCloseConnection;
			}
			else if (this.GetCurrentMailItem() == null)
			{
				DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "No more mail items available.  Moving to OnCloseConnection.");
				result = DeliveryAgentConnection.State.OnCloseConnection;
			}
			else
			{
				DeliveryAgentConnection.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Retrieved mail item with message id {0}.  Raising OnDeliverMailItem event.", this.currentMailItem.Message.MessageId);
				this.CreateMexSession(true);
				RoutedMailItemWrapper deliverableMailItem = this.CreateDeliverableMailItem();
				InternalDeliverMailItemEventSource internalDeliverMailItemEventSource = new InternalDeliverMailItemEventSource(this.mexSession, deliverableMailItem, this.sessionId, this.nextHopConnection, this.remoteHost, this.stats);
				InternalDeliverMailItemEventArgs internalDeliverMailItemEventArgs = new InternalDeliverMailItemEventArgs(deliverableMailItem);
				this.TrackBeginAgentInvocation(this.currentMailItem, LatencyComponent.DeliveryAgentOnDeliverMailItem);
				IAsyncResult asyncResult = this.mexEvents.RaiseEvent(this.mexSession, "OnDeliverMailItem", new AsyncCallback(this.OnDeliverMailItemEventCompleted), internalDeliverMailItemEventSource, new object[]
				{
					internalDeliverMailItemEventSource,
					internalDeliverMailItemEventArgs
				});
				if (asyncResult.CompletedSynchronously)
				{
					result = this.HandleDeliverMailItemEventCompleted(asyncResult);
				}
			}
			return result;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000AFD18 File Offset: 0x000ADF18
		private void OnDeliverMailItemEventCompleted(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				DeliveryAgentConnection.State nextState = this.HandleDeliverMailItemEventCompleted(ar);
				this.ExecuteNextState(nextState);
			}
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000AFD3C File Offset: 0x000ADF3C
		private DeliveryAgentConnection.State HandleDeliverMailItemEventCompleted(IAsyncResult ar)
		{
			DeliveryAgentConnection.State result = DeliveryAgentConnection.State.Done;
			InternalDeliveryAgentEventSource internalEventSource = ((InternalDeliverMailItemEventSource)ar.AsyncState).InternalEventSource;
			this.EndEvent(ar, internalEventSource);
			if (internalEventSource.ConnectionUnregistered)
			{
				DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "The agent unregistered its connection.");
				this.CompleteAsyncOperation();
			}
			else
			{
				if (internalEventSource.AnyRecipientsAcked)
				{
					internalEventSource.AckRemainingRecipientsAndFinalizeMailItem(AckStatus.Retry, DeliveryAgentConnection.RecipientNotHandledResponse);
				}
				if (!internalEventSource.MessageAcked)
				{
					internalEventSource.AckMailItemDefer(DeliveryAgentConnection.MessageNotHandledResponse);
				}
				result = DeliveryAgentConnection.State.OnDeliverMailItem;
			}
			this.ReleaseMexSession();
			this.ReleaseCurrentMailItem();
			return result;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000AFDC4 File Offset: 0x000ADFC4
		private DeliveryAgentConnection.State RaiseOnCloseConnectionEvent()
		{
			DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "Raising OnCloseConnection event.");
			DeliveryAgentConnection.State result = DeliveryAgentConnection.State.Done;
			this.CreateMexSession(false);
			InternalCloseConnectionEventSource internalCloseConnectionEventSource = new InternalCloseConnectionEventSource(this.mexSession, this.sessionId, this.remoteHost, this.nextHopConnection, this.stats);
			InternalCloseConnectionEventArgs internalCloseConnectionEventArgs = new InternalCloseConnectionEventArgs();
			this.TrackBeginAgentInvocation(null, LatencyComponent.None);
			IAsyncResult asyncResult = this.mexEvents.RaiseEvent(this.mexSession, "OnCloseConnection", new AsyncCallback(this.OnCloseConnectionEventCompleted), internalCloseConnectionEventSource, new object[]
			{
				internalCloseConnectionEventSource,
				internalCloseConnectionEventArgs
			});
			if (asyncResult.CompletedSynchronously)
			{
				result = this.HandleCloseConnectionEventCompleted(asyncResult);
			}
			return result;
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000AFE6C File Offset: 0x000AE06C
		private void OnCloseConnectionEventCompleted(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				DeliveryAgentConnection.State nextState = this.HandleCloseConnectionEventCompleted(ar);
				this.ExecuteNextState(nextState);
			}
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000AFE90 File Offset: 0x000AE090
		private DeliveryAgentConnection.State HandleCloseConnectionEventCompleted(IAsyncResult ar)
		{
			this.EndEvent(ar, null);
			InternalCloseConnectionEventSource internalCloseConnectionEventSource = (InternalCloseConnectionEventSource)ar.AsyncState;
			if (!internalCloseConnectionEventSource.ConnectionUnregistered)
			{
				internalCloseConnectionEventSource.UnregisterConnection(DeliveryAgentConnection.DefaultUnregisterConnectionResponse);
			}
			this.ReleaseMexSession();
			this.CompleteAsyncOperation();
			return DeliveryAgentConnection.State.Done;
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000AFED2 File Offset: 0x000AE0D2
		private void CompleteAsyncOperation()
		{
			this.ReleaseMexSession();
			this.mexEvents = null;
			this.connectionAsyncResult.Completed();
			this.connectionAsyncResult = null;
			this.ReleaseCurrentMailItem();
			this.nextHopConnection = null;
			this.connector = null;
			this.stats = null;
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000AFF10 File Offset: 0x000AE110
		private bool EndEvent(IAsyncResult ar, InternalDeliveryAgentEventSource eventSource)
		{
			DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "Event completed.");
			try
			{
				this.connectionAsyncResult.SetAsyncIfNecessary(ar);
				this.mexEvents.EndEvent(this.mexSession, ar);
			}
			catch (Exception ex)
			{
				DeliveryAgentConnection.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "The agent threw an exception: {0}", ex);
				bool flag = false;
				if (ExceptionHelper.HandleLeakedException)
				{
					if (eventSource == null || eventSource.MessageAcked)
					{
						flag = ExceptionHelper.IsHandleableException(ex);
					}
					else if (ExceptionHelper.IsHandleableTransientException(ex))
					{
						string text = string.Format(DeliveryAgentConnection.messageFailedWithTransientException, this.currentMailItem.Message.MessageId, ex.GetType().Name);
						SmtpResponse smtpResponse = new SmtpResponse("450", "4.5.1", new string[]
						{
							text
						});
						if (eventSource.AnyRecipientsAcked)
						{
							eventSource.AckRemainingRecipientsAndFinalizeMailItem(AckStatus.Retry, smtpResponse);
						}
						else
						{
							eventSource.AckMailItemDefer(smtpResponse);
						}
						flag = true;
					}
					else if (ExceptionHelper.IsHandleablePermanentException(ex))
					{
						string text2 = string.Format(DeliveryAgentConnection.messageFailedWithPermanentException, this.currentMailItem.Message.MessageId, ex.GetType().Name);
						SmtpResponse smtpResponse2 = new SmtpResponse("550", "5.6.0", new string[]
						{
							text2
						});
						if (eventSource.AnyRecipientsAcked)
						{
							eventSource.AckRemainingRecipientsAndFinalizeMailItem(AckStatus.Fail, smtpResponse2);
						}
						else
						{
							eventSource.AckMailItemFail(smtpResponse2);
						}
						flag = true;
					}
				}
				if (!flag)
				{
					throw;
				}
				DeliveryAgentConnection.Tracer.TraceDebug((long)this.GetHashCode(), "The exception was handled");
				return false;
			}
			finally
			{
				this.TrackEndAgentInvocation();
			}
			return true;
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000B00CC File Offset: 0x000AE2CC
		private RoutedMailItem GetCurrentMailItem()
		{
			if (!this.retire && this.currentMailItem == null)
			{
				this.GetNextMailItem();
			}
			return this.currentMailItem;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000B00EC File Offset: 0x000AE2EC
		private void GetNextMailItem()
		{
			if (this.mexSession != null)
			{
				throw new InvalidOperationException("GetNextMailItem() is called with an existing mexSession");
			}
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			for (;;)
			{
				this.currentMailItem = this.nextHopConnection.GetNextRoutedMailItem();
				if (this.currentMailItem == null)
				{
					break;
				}
				if (Components.Configuration.TryGetAcceptedDomainTable(this.currentMailItem.OrganizationId, out perTenantAcceptedDomainTable))
				{
					goto IL_99;
				}
				DeliveryAgentConnection.Tracer.TraceError<long>((long)this.GetHashCode(), "Unable to load accepted domains; deferring mail item {0}", this.currentMailItem.RecordId);
				this.nextHopConnection.AckMailItem(AckStatus.Retry, DeliveryAgentConnection.FailedToLoadAcceptedDomainsResponse, null, new TimeSpan?(InternalDeliveryAgentEventSource.DefaultMailItemRetryInterval), MessageTrackingSource.QUEUE, "DeliveryAgentDequeue", LatencyComponent.DeliveryAgent, true);
				this.currentMailItem = null;
			}
			return;
			IL_99:
			this.currentAcceptedDomains = perTenantAcceptedDomainTable.AcceptedDomainTable;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000B019E File Offset: 0x000AE39E
		private void ReleaseCurrentMailItem()
		{
			this.currentMailItem = null;
			this.currentAcceptedDomains = null;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000B01D8 File Offset: 0x000AE3D8
		private void CreateMexSession(bool mailItemSpecificEvent)
		{
			AcceptedDomainTable firstOrgAcceptedDomainTable;
			IReadOnlyMailItem readOnlyMailItem;
			if (mailItemSpecificEvent)
			{
				if (this.currentMailItem == null)
				{
					throw new InvalidOperationException("this.currentMailItem cannot be null for mail-item-specific event");
				}
				if (this.currentAcceptedDomains == null)
				{
					throw new InvalidOperationException("this.currentAcceptedDomains cannot be null for mail-item-specific event");
				}
				firstOrgAcceptedDomainTable = this.currentAcceptedDomains;
				readOnlyMailItem = this.currentMailItem;
			}
			else
			{
				firstOrgAcceptedDomainTable = Components.Configuration.FirstOrgAcceptedDomainTable;
				readOnlyMailItem = null;
			}
			this.mexSession = this.mexEvents.GetExecutionContext(this.connector.DeliveryProtocol, DeliveryAgentServer.GetInstance(readOnlyMailItem, firstOrgAcceptedDomainTable), delegate
			{
				this.TrackAsyncMessage(this.currentMailItem);
			}, delegate
			{
				this.TrackAsyncMessageCompleted(this.currentMailItem);
			}, () => this.SavePoisonContext(this.currentMailItem));
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000B0272 File Offset: 0x000AE472
		private void ReleaseMexSession()
		{
			if (this.mexSession != null)
			{
				this.mexEvents.FreeExecutionContext(this.mexSession);
				this.mexSession = null;
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000B0294 File Offset: 0x000AE494
		private RoutedMailItemWrapper CreateDeliverableMailItem()
		{
			return new RoutedMailItemWrapper(this.currentMailItem, this.nextHopConnection.ReadyRecipientsList);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000B02AC File Offset: 0x000AE4AC
		private void TrackBeginAgentInvocation(RoutedMailItem mailItem, LatencyComponent latencyComponent)
		{
			if (mailItem != null)
			{
				this.SavePoisonContext(mailItem);
				if (this.mexSession.AgentLatencyTracker != null)
				{
					this.mexSession.AgentLatencyTracker.BeginTrackLatency(latencyComponent, mailItem.LatencyTracker);
				}
			}
			this.stats.AgentInvokeStart();
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000B02E8 File Offset: 0x000AE4E8
		private void TrackAsyncMessage(RoutedMailItem mailItem)
		{
			TransportMailItem.TrackAsyncMessage(mailItem.InternetMessageId);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000B02F5 File Offset: 0x000AE4F5
		private void TrackAsyncMessageCompleted(RoutedMailItem mailItem)
		{
			TransportMailItem.TrackAsyncMessageCompleted(mailItem.InternetMessageId);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000B0302 File Offset: 0x000AE502
		private bool SavePoisonContext(RoutedMailItem mailItem)
		{
			return TransportMailItem.SetPoisonContext(mailItem.RecordId, mailItem.InternetMessageId, MessageProcessingSource.DeliveryAgent);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000B0317 File Offset: 0x000AE517
		private void TrackEndAgentInvocation()
		{
			if (this.mexSession.AgentLatencyTracker != null && this.currentMailItem != null)
			{
				this.mexSession.AgentLatencyTracker.EndTrackLatency();
			}
			this.stats.AgentInvokeEnd();
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000B0349 File Offset: 0x000AE549
		private void AckConnection(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.nextHopConnection.AckConnection(ackStatus, smtpResponse, null);
		}

		// Token: 0x0400160A RID: 5642
		private static readonly SmtpResponse ConnectionNotHandledResponse = new SmtpResponse("451", "4.4.0", new string[]
		{
			"No DeliveryAgent handled the queue"
		});

		// Token: 0x0400160B RID: 5643
		private static readonly SmtpResponse MessageNotHandledResponse = new SmtpResponse("451", "4.4.0", new string[]
		{
			"DeliveryAgent returned without processing message"
		});

		// Token: 0x0400160C RID: 5644
		private static readonly SmtpResponse RecipientNotHandledResponse = new SmtpResponse("451", "4.4.0", new string[]
		{
			"DeliveryAgent returned without processing recipient"
		});

		// Token: 0x0400160D RID: 5645
		private static readonly SmtpResponse FailedToLoadAcceptedDomainsResponse = new SmtpResponse("450", "4.5.1", new string[]
		{
			"Failed to load accepted domains"
		});

		// Token: 0x0400160E RID: 5646
		private static readonly SmtpResponse DefaultUnregisterConnectionResponse = new SmtpResponse("250", "2.0.0", new string[]
		{
			"DeliveryAgent successfully closed the connection"
		});

		// Token: 0x0400160F RID: 5647
		private static string messageFailedWithTransientException = "DeliveryAgent failed to process message with id = {0} with transient exception {1}";

		// Token: 0x04001610 RID: 5648
		private static string messageFailedWithPermanentException = "DeliveryAgent failed to process message with id = {0} with permanent exception {1}";

		// Token: 0x04001611 RID: 5649
		private ulong sessionId = SessionId.GetNextSessionId();

		// Token: 0x04001612 RID: 5650
		private DeliveryAgentMExEvents mexEvents;

		// Token: 0x04001613 RID: 5651
		private DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession;

		// Token: 0x04001614 RID: 5652
		private DeliveryAgentConnection.AsyncResult connectionAsyncResult;

		// Token: 0x04001615 RID: 5653
		private DeliveryAgentConnector connector;

		// Token: 0x04001616 RID: 5654
		private NextHopConnection nextHopConnection;

		// Token: 0x04001617 RID: 5655
		private string remoteHost;

		// Token: 0x04001618 RID: 5656
		private RoutedMailItem currentMailItem;

		// Token: 0x04001619 RID: 5657
		private AcceptedDomainTable currentAcceptedDomains;

		// Token: 0x0400161A RID: 5658
		private bool retire;

		// Token: 0x0400161B RID: 5659
		private DeliveryAgentConnection.Stats stats;

		// Token: 0x020003BF RID: 959
		private enum State
		{
			// Token: 0x0400161D RID: 5661
			OnOpenConnection,
			// Token: 0x0400161E RID: 5662
			OnDeliverMailItem,
			// Token: 0x0400161F RID: 5663
			OnCloseConnection,
			// Token: 0x04001620 RID: 5664
			Done
		}

		// Token: 0x020003C0 RID: 960
		public class Stats
		{
			// Token: 0x17000D5C RID: 3420
			// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000B043C File Offset: 0x000AE63C
			public virtual int NumMessagesDelivered
			{
				get
				{
					return this.numMessagesDelivered;
				}
			}

			// Token: 0x17000D5D RID: 3421
			// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000B0444 File Offset: 0x000AE644
			public virtual long NumBytesDelivered
			{
				get
				{
					return this.numBytesDelivered;
				}
			}

			// Token: 0x17000D5E RID: 3422
			// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000B044C File Offset: 0x000AE64C
			public virtual bool HasOpenConnection
			{
				get
				{
					return this.hasOpenConnection;
				}
			}

			// Token: 0x06002C0B RID: 11275 RVA: 0x000B0454 File Offset: 0x000AE654
			public virtual void Initialize(string deliveryProtocol)
			{
				this.perfCounters = DeliveryAgentPerfCounters.GetInstance(deliveryProtocol);
			}

			// Token: 0x06002C0C RID: 11276 RVA: 0x000B0464 File Offset: 0x000AE664
			public virtual void MessageDelivered(int numRecipients, long numBytes)
			{
				this.numMessagesDelivered++;
				this.numBytesDelivered += numBytes;
				this.perfCounters.MessagesDeliveredTotal.Increment();
				this.perfCounters.RecipientDeliveriesCompletedTotal.IncrementBy((long)numRecipients);
				this.perfCounters.MessageBytesSentTotal.IncrementBy(numBytes);
			}

			// Token: 0x06002C0D RID: 11277 RVA: 0x000B04C3 File Offset: 0x000AE6C3
			public virtual void ConnectionStarted()
			{
				this.perfCounters.CurrentConnectionCount.Increment();
				this.hasOpenConnection = true;
			}

			// Token: 0x06002C0E RID: 11278 RVA: 0x000B04DD File Offset: 0x000AE6DD
			public virtual void ConnectionStopped()
			{
				this.perfCounters.ConnectionsCompletedTotal.Increment();
				this.perfCounters.CurrentConnectionCount.Decrement();
				this.hasOpenConnection = false;
			}

			// Token: 0x06002C0F RID: 11279 RVA: 0x000B0508 File Offset: 0x000AE708
			public virtual void ConnectionFailed()
			{
				this.perfCounters.ConnectionsFailedTotal.Increment();
			}

			// Token: 0x06002C10 RID: 11280 RVA: 0x000B051B File Offset: 0x000AE71B
			public virtual void MessageFailed()
			{
				this.perfCounters.MessagesFailedTotal.Increment();
			}

			// Token: 0x06002C11 RID: 11281 RVA: 0x000B052E File Offset: 0x000AE72E
			public virtual void MessageDeferred()
			{
				this.perfCounters.MessagesDeferredTotal.Increment();
			}

			// Token: 0x06002C12 RID: 11282 RVA: 0x000B0541 File Offset: 0x000AE741
			public virtual void AgentInvokeStart()
			{
				this.agentInvocationTime = DateTime.UtcNow;
				this.perfCounters.InvocationTotal.Increment();
			}

			// Token: 0x06002C13 RID: 11283 RVA: 0x000B0560 File Offset: 0x000AE760
			public virtual void AgentInvokeEnd()
			{
				TimeSpan timeSpan = DateTime.UtcNow - this.agentInvocationTime;
				this.perfCounters.InvocationDurationTotal.IncrementBy((long)timeSpan.TotalSeconds);
			}

			// Token: 0x04001621 RID: 5665
			private DeliveryAgentPerfCountersInstance perfCounters;

			// Token: 0x04001622 RID: 5666
			private DateTime agentInvocationTime;

			// Token: 0x04001623 RID: 5667
			private int numMessagesDelivered;

			// Token: 0x04001624 RID: 5668
			private long numBytesDelivered;

			// Token: 0x04001625 RID: 5669
			private bool hasOpenConnection;
		}

		// Token: 0x020003C1 RID: 961
		private class AsyncResult : IAsyncResult
		{
			// Token: 0x06002C14 RID: 11284 RVA: 0x000B0597 File Offset: 0x000AE797
			public AsyncResult(object asyncState, AsyncCallback asyncCallback)
			{
				this.asyncState = asyncState;
				this.asyncCallback = asyncCallback;
			}

			// Token: 0x17000D5F RID: 3423
			// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000B05AD File Offset: 0x000AE7AD
			public object AsyncState
			{
				get
				{
					return this.asyncState;
				}
			}

			// Token: 0x17000D60 RID: 3424
			// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000B05B5 File Offset: 0x000AE7B5
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17000D61 RID: 3425
			// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000B05BC File Offset: 0x000AE7BC
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000D62 RID: 3426
			// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000B05BF File Offset: 0x000AE7BF
			public bool IsCompleted
			{
				get
				{
					return this.operationIsCompleted;
				}
			}

			// Token: 0x06002C19 RID: 11289 RVA: 0x000B05C7 File Offset: 0x000AE7C7
			public void SetAsyncIfNecessary(IAsyncResult ar)
			{
			}

			// Token: 0x06002C1A RID: 11290 RVA: 0x000B05C9 File Offset: 0x000AE7C9
			public void Completed()
			{
				this.operationIsCompleted = true;
				this.asyncCallback(this);
				this.asyncCallback = null;
			}

			// Token: 0x04001626 RID: 5670
			private object asyncState;

			// Token: 0x04001627 RID: 5671
			private AsyncCallback asyncCallback;

			// Token: 0x04001628 RID: 5672
			private bool operationIsCompleted;
		}
	}
}
