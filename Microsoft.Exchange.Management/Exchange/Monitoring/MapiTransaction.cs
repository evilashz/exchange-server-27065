using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000571 RID: 1393
	internal class MapiTransaction : IComparable
	{
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000C7D27 File Offset: 0x000C5F27
		internal Server TargetServer
		{
			get
			{
				return this.targetServer;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000C7D2F File Offset: 0x000C5F2F
		internal Database Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000C7D37 File Offset: 0x000C5F37
		internal ADRecipient ADRecipient
		{
			get
			{
				return this.adRecipient;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000C7D3F File Offset: 0x000C5F3F
		internal string DiagnosticContext
		{
			get
			{
				return this.diagnosticContext;
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000C7D48 File Offset: 0x000C5F48
		internal MapiTransaction(Server targetServer, Database database, ADRecipient adRecipient, bool isArchiveMailbox, bool isDatabaseCopyActive)
		{
			if (targetServer == null)
			{
				throw new ArgumentNullException("targetServer");
			}
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			this.targetServer = targetServer;
			this.database = database;
			this.adRecipient = adRecipient;
			this.isArchiveMailbox = isArchiveMailbox;
			this.isDatabaseCopyActive = isDatabaseCopyActive;
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000C7DA8 File Offset: 0x000C5FA8
		private string ShortErrorMsgFromException(Exception exception)
		{
			string result = string.Empty;
			if (exception.InnerException != null)
			{
				result = Strings.MapiTransactionShortErrorMsgFromExceptionWithInnerException(exception.GetType().ToString(), exception.Message, exception.InnerException.GetType().ToString(), exception.InnerException.Message);
			}
			else
			{
				result = Strings.MapiTransactionShortErrorMsgFromException(exception.GetType().ToString(), exception.Message);
			}
			return result;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000C7E1C File Offset: 0x000C601C
		private string GetErrorStringBasedOnDatabaseCopyState()
		{
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=StoreActiveMonitoring", this.targetServer.Name, null, null, null))
			{
				MdbStatus[] array = exRpcAdmin.ListMdbStatus(new Guid[]
				{
					this.database.Guid
				});
				if (array.Length != 0)
				{
					if (this.isDatabaseCopyActive)
					{
						if ((array[0].Status & MdbStatusFlags.Online) != MdbStatusFlags.Online)
						{
							return Strings.MapiTransactionDiagnosticTargetDatabaseDismounted;
						}
					}
					else if ((array[0].Status & MdbStatusFlags.AttachedReadOnly) != MdbStatusFlags.AttachedReadOnly)
					{
						return Strings.MapiTransactionDiagnosticTargetDatabaseNotAttached;
					}
				}
			}
			return null;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000C7ED0 File Offset: 0x000C60D0
		private string GetDiagnosticContext(Exception ex)
		{
			if (ex == null)
			{
				throw new ArgumentNullException("ex");
			}
			string result = string.Empty;
			Exception innerException = ex.InnerException;
			MapiPermanentException ex2 = innerException as MapiPermanentException;
			MapiRetryableException ex3 = innerException as MapiRetryableException;
			if (ex2 != null)
			{
				result = ex2.DiagCtx.ToCompactString();
			}
			else if (ex3 != null)
			{
				result = ex3.DiagCtx.ToCompactString();
			}
			return result;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000C7F28 File Offset: 0x000C6128
		private string DiagnoseMapiOperationException(LocalizedException exception, out MapiTransactionResultEnum result)
		{
			result = MapiTransactionResultEnum.Failure;
			bool flag;
			bool flag2;
			string result2 = this.DiagnoseMapiOperationException(exception, out flag, out flag2);
			if (flag)
			{
				result = MapiTransactionResultEnum.MdbMoved;
			}
			else if (flag2)
			{
				result = MapiTransactionResultEnum.StoreNotRunning;
			}
			this.diagnosticContext = this.GetDiagnosticContext(exception);
			return result2;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000C7F60 File Offset: 0x000C6160
		internal string DiagnoseMapiOperationException(LocalizedException exception, out bool targetDatabaseMoved, out bool storeNotRunning)
		{
			targetDatabaseMoved = false;
			storeNotRunning = false;
			try
			{
				if (this.isDatabaseCopyActive)
				{
					DatabaseLocationInfo serverForDatabase = RecipientTaskHelper.GetActiveManagerInstance().GetServerForDatabase(this.database.Guid);
					if (string.Compare(serverForDatabase.ServerFqdn, this.targetServer.Fqdn, StringComparison.OrdinalIgnoreCase) != 0)
					{
						targetDatabaseMoved = true;
						return Strings.MapiTransactionDiagnosticTargetDatabaseNotOnTargetServer(serverForDatabase.ServerFqdn, this.targetServer.Fqdn);
					}
				}
			}
			catch (ObjectNotFoundException exception2)
			{
				return Strings.MapiTransactionDiagnosticFailedToGetMdbLocation(this.ShortErrorMsgFromException(exception2));
			}
			using (ServiceController serviceController = new ServiceController("MSExchangeIS", this.targetServer.Fqdn))
			{
				try
				{
					if (serviceController.Status != ServiceControllerStatus.Running)
					{
						storeNotRunning = true;
						return Strings.MapiTransactionDiagnosticStoreServiceIsNotRunning;
					}
				}
				catch (InvalidOperationException exception3)
				{
					return Strings.MapiTransactionDiagnosticStoreServiceCheckFailure(this.ShortErrorMsgFromException(exception3));
				}
			}
			try
			{
				string errorStringBasedOnDatabaseCopyState = this.GetErrorStringBasedOnDatabaseCopyState();
				if (!string.IsNullOrWhiteSpace(errorStringBasedOnDatabaseCopyState))
				{
					return errorStringBasedOnDatabaseCopyState;
				}
			}
			catch (MapiPermanentException exception4)
			{
				return Strings.MapiTransactionDiagnosticStoreStateCheckFailure(this.ShortErrorMsgFromException(exception4));
			}
			catch (MapiRetryableException exception5)
			{
				return Strings.MapiTransactionDiagnosticStoreStateCheckFailure(this.ShortErrorMsgFromException(exception5));
			}
			return this.ShortErrorMsgFromException(exception);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000C80D4 File Offset: 0x000C62D4
		private void Execute(object transactionOutcome)
		{
			this.Execute((MapiTransactionOutcome)transactionOutcome);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000C80E4 File Offset: 0x000C62E4
		private void Execute(MapiTransactionOutcome transactionOutcome)
		{
			ExchangePrincipal mailboxOwner = null;
			MapiTransactionResultEnum resultEnum = MapiTransactionResultEnum.Failure;
			string error = string.Empty;
			TimeSpan latency = TimeSpan.Zero;
			Guid? mailboxGuid = null;
			MailboxMiscFlags? mailboxMiscFlags = null;
			try
			{
				if (this.adRecipient == null)
				{
					try
					{
						string errorStringBasedOnDatabaseCopyState = this.GetErrorStringBasedOnDatabaseCopyState();
						if (!string.IsNullOrWhiteSpace(errorStringBasedOnDatabaseCopyState))
						{
							error = errorStringBasedOnDatabaseCopyState;
						}
						else
						{
							error = Strings.MapiTransactionErrorMsgNoMailbox;
						}
					}
					catch (MapiPermanentException ex)
					{
						error = Strings.MapiTransactionDiagnosticStoreStateCheckFailure(this.ShortErrorMsgFromException(ex));
						this.diagnosticContext = ex.DiagCtx.ToCompactString();
					}
					catch (MapiRetryableException ex2)
					{
						error = Strings.MapiTransactionDiagnosticStoreStateCheckFailure(this.ShortErrorMsgFromException(ex2));
						this.diagnosticContext = ex2.DiagCtx.ToCompactString();
					}
					transactionOutcome.Update(MapiTransactionResultEnum.Failure, TimeSpan.Zero, error, mailboxGuid, mailboxMiscFlags, this.isDatabaseCopyActive);
				}
				else
				{
					try
					{
						if (this.adRecipient is ADSystemMailbox)
						{
							mailboxOwner = ExchangePrincipal.FromADSystemMailbox(ADSessionSettings.FromRootOrgScopeSet(), (ADSystemMailbox)this.adRecipient, this.targetServer);
						}
						else
						{
							ADSessionSettings adSessionSettings = this.adRecipient.OrganizationId.ToADSessionSettings();
							mailboxOwner = ExchangePrincipal.FromMailboxData(adSessionSettings, this.adRecipient.DisplayName, this.targetServer.Fqdn, this.targetServer.ExchangeLegacyDN, this.adRecipient.LegacyExchangeDN, this.isArchiveMailbox ? ((ADUser)this.adRecipient).ArchiveGuid : ((ADUser)this.adRecipient).ExchangeGuid, this.database.Guid, this.adRecipient.PrimarySmtpAddress.ToString(), this.adRecipient.Id, new List<CultureInfo>(), Array<Guid>.Empty, RecipientType.Invalid, RemotingOptions.AllowCrossSite);
						}
					}
					catch (ObjectNotFoundException ex3)
					{
						transactionOutcome.Update(MapiTransactionResultEnum.Failure, TimeSpan.Zero, this.ShortErrorMsgFromException(ex3), mailboxGuid, mailboxMiscFlags, this.isDatabaseCopyActive);
						this.diagnosticContext = this.GetDiagnosticContext(ex3);
						return;
					}
					MailboxSession mailboxSession = null;
					Stopwatch stopwatch = Stopwatch.StartNew();
					try
					{
						if (!this.transactionTimeouted)
						{
							try
							{
								mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=StoreActiveMonitoring;Action=Test-MapiConnectivity", false, false, !this.isDatabaseCopyActive);
							}
							catch (StorageTransientException exception)
							{
								error = this.DiagnoseMapiOperationException(exception, out resultEnum);
								return;
							}
							catch (StoragePermanentException exception2)
							{
								error = this.DiagnoseMapiOperationException(exception2, out resultEnum);
								return;
							}
							if (!this.transactionTimeouted)
							{
								using (Folder.Bind(mailboxSession, DefaultFolderType.Inbox, new PropertyDefinition[]
								{
									FolderSchema.ItemCount
								}))
								{
									resultEnum = MapiTransactionResultEnum.Success;
									error = string.Empty;
								}
								mailboxSession.Mailbox.Load(new PropertyDefinition[]
								{
									MailboxSchema.MailboxGuid,
									MailboxSchema.MailboxMiscFlags
								});
								byte[] array = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.MailboxGuid) as byte[];
								object obj = mailboxSession.Mailbox.TryGetProperty(MailboxSchema.MailboxMiscFlags);
								if (array != null && array.Length == 16)
								{
									mailboxGuid = new Guid?(new Guid(array));
								}
								if (obj is int)
								{
									mailboxMiscFlags = new MailboxMiscFlags?((MailboxMiscFlags)obj);
								}
								latency = stopwatch.Elapsed;
							}
						}
					}
					finally
					{
						if (mailboxSession != null)
						{
							mailboxSession.Dispose();
						}
					}
				}
			}
			catch (Exception exception3)
			{
				error = this.ShortErrorMsgFromException(exception3);
			}
			finally
			{
				lock (this.timeoutOperationLock)
				{
					if (!this.transactionTimeouted)
					{
						transactionOutcome.Update(resultEnum, latency, error, mailboxGuid, mailboxMiscFlags, this.isDatabaseCopyActive);
					}
				}
			}
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000C8534 File Offset: 0x000C6734
		internal MapiTransactionOutcome TimedExecute(int timeOutMilliseconds)
		{
			Thread thread = new Thread(new ParameterizedThreadStart(this.Execute));
			MapiTransactionOutcome mapiTransactionOutcome = new MapiTransactionOutcome(this.targetServer, this.database, this.adRecipient);
			this.transactionTimeouted = false;
			try
			{
				thread.Start(mapiTransactionOutcome);
				if (!thread.Join(timeOutMilliseconds))
				{
					lock (this.timeoutOperationLock)
					{
						this.transactionTimeouted = true;
					}
					if (!thread.Join(250))
					{
						thread.Abort();
					}
				}
				if (mapiTransactionOutcome.Latency.TotalMilliseconds > (double)timeOutMilliseconds || this.transactionTimeouted)
				{
					mapiTransactionOutcome.Update(MapiTransactionResultEnum.Failure, TimeSpan.Zero, Strings.MapiTransactionErrorMsgTimeout((double)timeOutMilliseconds / 1000.0), null, null, this.isDatabaseCopyActive);
				}
			}
			catch (ThreadAbortException)
			{
				mapiTransactionOutcome.Update(MapiTransactionResultEnum.Failure, TimeSpan.Zero, Strings.MapiTransactionAbortedMsg, null, null, this.isDatabaseCopyActive);
			}
			return mapiTransactionOutcome;
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x000C8668 File Offset: 0x000C6868
		public int CompareTo(object obj)
		{
			MapiTransaction mapiTransaction = obj as MapiTransaction;
			if (mapiTransaction == null)
			{
				throw new ArgumentException();
			}
			int num = string.Compare(this.database.ServerName, mapiTransaction.database.ServerName, true, CultureInfo.CurrentCulture);
			if (num != 0)
			{
				return num;
			}
			return string.Compare(this.database.Name, mapiTransaction.database.Name, true, CultureInfo.CurrentCulture);
		}

		// Token: 0x040022CB RID: 8907
		private const string MapiClientIdAndAction = "Client=StoreActiveMonitoring;Action=Test-MapiConnectivity";

		// Token: 0x040022CC RID: 8908
		private Server targetServer;

		// Token: 0x040022CD RID: 8909
		private Database database;

		// Token: 0x040022CE RID: 8910
		private ADRecipient adRecipient;

		// Token: 0x040022CF RID: 8911
		private string diagnosticContext;

		// Token: 0x040022D0 RID: 8912
		private readonly bool isArchiveMailbox;

		// Token: 0x040022D1 RID: 8913
		private readonly bool isDatabaseCopyActive;

		// Token: 0x040022D2 RID: 8914
		private volatile bool transactionTimeouted;

		// Token: 0x040022D3 RID: 8915
		private object timeoutOperationLock = new object();
	}
}
