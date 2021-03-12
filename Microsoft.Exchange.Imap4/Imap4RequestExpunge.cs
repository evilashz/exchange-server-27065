using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000032 RID: 50
	internal sealed class Imap4RequestExpunge : Imap4RequestWithMessageSetSupport
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000DA8F File Offset: 0x0000BC8F
		public Imap4RequestExpunge(Imap4ResponseFactory factory, string tag, string data) : this(factory, tag, data, false)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public Imap4RequestExpunge(Imap4ResponseFactory factory, string tag, string data, bool useUids) : base(factory, tag, data, useUids, useUids ? 1 : 0, useUids ? 1 : 0)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_EXPUNGE_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_EXPUNGE_Failures;
			base.AllowedStates = Imap4State.Selected;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000DAED File Offset: 0x0000BCED
		public override bool NeedsStoreConnection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		public override bool NeedToDelayStoreAction
		{
			get
			{
				return this.delayedActionsEnqueued;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public override ProtocolResponse Process()
		{
			if (base.Factory.SelectedMailbox.MailboxDoesNotExist)
			{
				base.Factory.SelectedMailbox = null;
				return new Imap4Response(this, Imap4Response.Type.no, "Mailbox has been deleted or moved.");
			}
			if (base.Factory.SelectedMailbox.ExamineMode)
			{
				return new Imap4Response(this, Imap4Response.Type.no, "Command received in Invalid state.");
			}
			List<ProtocolMessage> messagesToExpunge;
			if (base.UseUids)
			{
				List<ProtocolMessage> messages = base.GetMessages(base.ArrayOfArguments[0]);
				if (this.ParseResult == ParseResult.invalidMessageSet)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "[*] The specified message set is invalid.");
				}
				if (base.MessageSetIsInvalid)
				{
					return new Imap4Response(this, Imap4Response.Type.no, "[*] The specified message set is invalid.");
				}
				messagesToExpunge = base.Factory.SelectedMailbox.GetMessagesToExpunge(messages);
			}
			else
			{
				messagesToExpunge = base.Factory.SelectedMailbox.GetMessagesToExpunge();
			}
			this.delayedActionsEnqueued = true;
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed = new int?(messagesToExpunge.Count);
			}
			Imap4Response imap4Response = new Imap4Response(this, Imap4Response.Type.ok, "EXPUNGE completed.");
			Imap4RequestExpunge.ExpungeResponseItem responseitem = new Imap4RequestExpunge.ExpungeResponseItem(messagesToExpunge, imap4Response);
			if (!base.Factory.Session.SendToClient(responseitem))
			{
				return null;
			}
			return imap4Response;
		}

		// Token: 0x04000177 RID: 375
		internal const string EXPUNGEResponseCompleted = "EXPUNGE completed.";

		// Token: 0x04000178 RID: 376
		internal const string EXPUNGEResponseFailed = "EXPUNGE failed.";

		// Token: 0x04000179 RID: 377
		internal static readonly byte[] NotificationStar = Encoding.ASCII.GetBytes("* ");

		// Token: 0x0400017A RID: 378
		internal static readonly byte[] NotificationExpunge = Encoding.ASCII.GetBytes(" EXPUNGE\r\n");

		// Token: 0x0400017B RID: 379
		internal static readonly byte[] NotificationExists = Encoding.ASCII.GetBytes(" EXISTS\r\n");

		// Token: 0x0400017C RID: 380
		private bool delayedActionsEnqueued;

		// Token: 0x02000033 RID: 51
		private class ExpungeResponseItem : DisposeTrackableBase, IResponseItem
		{
			// Token: 0x06000201 RID: 513 RVA: 0x0000DC52 File Offset: 0x0000BE52
			public ExpungeResponseItem(List<ProtocolMessage> itemsToDelete, Imap4Response response)
			{
				this.itemsToDelete = itemsToDelete;
				this.response = response;
				this.currentMessageIndex = 0;
				this.bufferBuilder = new BufferBuilder();
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x06000202 RID: 514 RVA: 0x0000DC7A File Offset: 0x0000BE7A
			public BaseSession.SendCompleteDelegate SendCompleteDelegate
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000DC80 File Offset: 0x0000BE80
			public int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
			{
				buffer = null;
				offset = 0;
				ProtocolBaseServices.SessionTracer.TraceDebug(session.SessionId, "ExpungeResponseItem.GetNextChunk is called");
				if (this.bufferBuilder == null)
				{
					return 0;
				}
				ProtocolSession protocolSession = session as ProtocolSession;
				ResponseFactory responseFactory = ((ProtocolSession)session).ResponseFactory;
				Imap4Mailbox selectedMailbox = ((Imap4ResponseFactory)responseFactory).SelectedMailbox;
				bool flag = !responseFactory.IsStoreConnected;
				bool flag2 = false;
				IStandardBudget perCallBudget = null;
				int result;
				try
				{
					ActivityContext.SetThreadScope(protocolSession.ActivityScope);
					if (flag)
					{
						Monitor.Enter(protocolSession.ResponseFactory.Store);
						flag2 = true;
						responseFactory.Store.Connect();
					}
					try
					{
						perCallBudget = protocolSession.ResponseFactory.AcquirePerCommandBudget();
					}
					catch (OverBudgetException exception)
					{
						return this.ProcessException(responseFactory, this.response.Request, exception, selectedMailbox, out buffer);
					}
					catch (ADTransientException exception2)
					{
						return this.ProcessException(responseFactory, this.response.Request, exception2, selectedMailbox, out buffer);
					}
					if (this.messageStoreIds == null)
					{
						this.messageStoreIds = selectedMailbox.DataAccessView.GetStoreObjectIds(this.itemsToDelete);
						ProtocolBaseServices.SessionTracer.TraceDebug(session.SessionId, "StoreObjectIds are preloaded");
						this.itemsToDelete.Reverse();
						Array.Reverse(this.messageStoreIds);
					}
					int num = Math.Min(256, this.itemsToDelete.Count - this.currentMessageIndex);
					if (num == 0)
					{
						ProtocolBaseServices.SessionTracer.TraceDebug(session.SessionId, "All items expunged");
						if (this.itemsToDelete.Count > 0)
						{
							selectedMailbox.UpdateMessageCounters();
							selectedMailbox.ResetNotifications();
						}
						result = this.GetExistsNotification(selectedMailbox, out buffer);
					}
					else
					{
						StoreObjectId[] array = new StoreObjectId[num];
						Array.Copy(this.messageStoreIds, this.currentMessageIndex, array, 0, num);
						ProtocolBaseServices.SessionTracer.TraceDebug<int>(session.SessionId, "Processing {0} items", num);
						this.bufferBuilder.Reset();
						for (int i = 0; i < num; i++)
						{
							this.bufferBuilder.Append(Imap4RequestExpunge.NotificationStar);
							this.bufferBuilder.Append(this.itemsToDelete[this.currentMessageIndex + i].Index.ToString().Trim());
							this.bufferBuilder.Append(Imap4RequestExpunge.NotificationExpunge);
							int num2 = this.itemsToDelete[this.currentMessageIndex + i].Index - 1;
							if (num2 >= 0 && num2 < selectedMailbox.Messages.Count)
							{
								selectedMailbox.Messages.RemoveAt(num2);
							}
						}
						try
						{
							if (!responseFactory.DeleteMessages(array))
							{
								ProtocolBaseServices.SessionTracer.TraceError(session.SessionId, "responseFactory.DeleteMessages failed");
								this.response.ResponseType = Imap4Response.Type.no;
								this.response.MessageString = "EXPUNGE failed.";
								return this.GetExistsNotification(selectedMailbox, out buffer);
							}
						}
						catch (StoragePermanentException exception3)
						{
							return this.ProcessException(responseFactory, this.response.Request, exception3, selectedMailbox, out buffer);
						}
						catch (StorageTransientException exception4)
						{
							return this.ProcessException(responseFactory, this.response.Request, exception4, selectedMailbox, out buffer);
						}
						ProtocolBaseServices.SessionTracer.TraceDebug<int>(session.SessionId, "{0} items deleted", num);
						this.currentMessageIndex += num;
						buffer = this.bufferBuilder.GetBuffer();
						result = this.bufferBuilder.Length;
					}
				}
				catch (Exception exception5)
				{
					if (!protocolSession.CheckNonCriticalException(exception5))
					{
						throw;
					}
					result = this.ProcessException(responseFactory, this.response.Request, exception5, selectedMailbox, out buffer);
				}
				finally
				{
					if (flag)
					{
						responseFactory.Store.Disconnect();
					}
					protocolSession.EnforceMicroDelayAndDisposeCostHandles(perCallBudget);
					if (flag2)
					{
						Monitor.Exit(protocolSession.ResponseFactory.Store);
					}
				}
				return result;
			}

			// Token: 0x06000204 RID: 516 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
			private int ProcessException(ResponseFactory responseFactory, ProtocolRequest request, Exception exception, Imap4Mailbox selectedMailbox, out byte[] buffer)
			{
				using (Imap4Response imap4Response = (Imap4Response)responseFactory.ProcessException(this.response.Request, exception))
				{
					this.response.ResponseType = imap4Response.ResponseType;
					this.response.MessageString = imap4Response.MessageString;
				}
				return this.GetExistsNotification(selectedMailbox, out buffer);
			}

			// Token: 0x06000205 RID: 517 RVA: 0x0000E114 File Offset: 0x0000C314
			private int GetExistsNotification(Imap4Mailbox selectedMailbox, out byte[] buffer)
			{
				this.bufferBuilder.Reset();
				this.bufferBuilder.Append(Imap4RequestExpunge.NotificationStar);
				this.bufferBuilder.Append(selectedMailbox.MessagesTotal.ToString().Trim());
				this.bufferBuilder.Append(Imap4RequestExpunge.NotificationExists);
				buffer = this.bufferBuilder.GetBuffer();
				int length = this.bufferBuilder.Length;
				this.bufferBuilder = null;
				return length;
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000E18B File Offset: 0x0000C38B
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<Imap4RequestExpunge.ExpungeResponseItem>(this);
			}

			// Token: 0x06000207 RID: 519 RVA: 0x0000E193 File Offset: 0x0000C393
			protected override void InternalDispose(bool isDisposing)
			{
			}

			// Token: 0x0400017D RID: 381
			private List<ProtocolMessage> itemsToDelete;

			// Token: 0x0400017E RID: 382
			private Imap4Response response;

			// Token: 0x0400017F RID: 383
			private StoreObjectId[] messageStoreIds;

			// Token: 0x04000180 RID: 384
			private int currentMessageIndex;

			// Token: 0x04000181 RID: 385
			private BufferBuilder bufferBuilder;
		}
	}
}
