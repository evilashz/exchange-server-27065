using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x02000790 RID: 1936
	[Cmdlet("Get", "CalendarDiagnosticLog", DefaultParameterSetName = "DoNotExportParameterSet")]
	public sealed class GetCalendarDiagnosticLog : GetRecipientObjectTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06004426 RID: 17446 RVA: 0x00117761 File Offset: 0x00115961
		// (set) Token: 0x06004427 RID: 17447 RVA: 0x00117778 File Offset: 0x00115978
		[Parameter(Mandatory = true, ParameterSetName = "ExportToMsgParameterSet", ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "DoNotExportParameterSet", ValueFromPipelineByPropertyName = true, Position = 0)]
		public new MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x0011778B File Offset: 0x0011598B
		// (set) Token: 0x06004429 RID: 17449 RVA: 0x001177AB File Offset: 0x001159AB
		[Parameter(Mandatory = false)]
		public string Subject
		{
			get
			{
				return (string)(base.Fields["Subject"] ?? string.Empty);
			}
			set
			{
				base.Fields["Subject"] = value;
			}
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x0600442A RID: 17450 RVA: 0x001177BE File Offset: 0x001159BE
		// (set) Token: 0x0600442B RID: 17451 RVA: 0x001177DE File Offset: 0x001159DE
		[Parameter(Mandatory = false)]
		public string MeetingID
		{
			get
			{
				return (string)(base.Fields["MeetingID"] ?? string.Empty);
			}
			set
			{
				base.Fields["MeetingID"] = value;
			}
		}

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x001177F4 File Offset: 0x001159F4
		// (set) Token: 0x0600442D RID: 17453 RVA: 0x0011786C File Offset: 0x00115A6C
		[Parameter(Mandatory = false)]
		public ExDateTime? StartDate
		{
			get
			{
				ExDateTime? exDateTime = (ExDateTime?)base.Fields["StartDate"];
				if (exDateTime == null)
				{
					exDateTime = new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, DateTime.MinValue.ToUniversalTime()));
				}
				else
				{
					exDateTime = new ExDateTime?(new ExDateTime(ExTimeZone.CurrentTimeZone, exDateTime.Value.UtcTicks));
				}
				return new ExDateTime?(exDateTime.Value);
			}
			set
			{
				base.Fields["StartDate"] = value;
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x0600442E RID: 17454 RVA: 0x00117884 File Offset: 0x00115A84
		// (set) Token: 0x0600442F RID: 17455 RVA: 0x001178FC File Offset: 0x00115AFC
		[Parameter(Mandatory = false)]
		public ExDateTime? EndDate
		{
			get
			{
				ExDateTime? exDateTime = (ExDateTime?)base.Fields["EndDate"];
				if (exDateTime == null)
				{
					exDateTime = new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, DateTime.MaxValue.ToUniversalTime()));
				}
				else
				{
					exDateTime = new ExDateTime?(new ExDateTime(ExTimeZone.CurrentTimeZone, exDateTime.Value.UtcTicks));
				}
				return new ExDateTime?(exDateTime.Value);
			}
			set
			{
				base.Fields["EndDate"] = value;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x00117914 File Offset: 0x00115B14
		// (set) Token: 0x06004431 RID: 17457 RVA: 0x0011793A File Offset: 0x00115B3A
		[Parameter(Mandatory = false)]
		public SwitchParameter Latest
		{
			get
			{
				return (SwitchParameter)(base.Fields["GetLatest"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["GetLatest"] = value;
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x00117954 File Offset: 0x00115B54
		// (set) Token: 0x06004433 RID: 17459 RVA: 0x00117A34 File Offset: 0x00115C34
		[Parameter(Mandatory = true, ParameterSetName = "ExportToMsgParameterSet")]
		public string LogLocation
		{
			get
			{
				string fileName = string.Empty;
				if (!this.IsLogLocationInitialized)
				{
					if (!base.Fields.Contains("LogLocation"))
					{
						using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CalendarLogs"))
						{
							if (registryKey != null)
							{
								string text = registryKey.GetValue("LogLocation", string.Empty) as string;
								if (!string.IsNullOrEmpty(text))
								{
									fileName = text;
									base.Fields["LogLocation"] = LocalLongFullPath.ConvertInvalidCharactersInPathName(fileName);
								}
							}
							goto IL_A4;
						}
					}
					fileName = (string)base.Fields["LogLocation"];
					base.Fields["LogLocation"] = LocalLongFullPath.ConvertInvalidCharactersInPathName(fileName);
					IL_A4:
					this.IsLogLocationInitialized = true;
				}
				return (string)base.Fields["LogLocation"];
			}
			set
			{
				base.Fields["LogLocation"] = value;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x00117A47 File Offset: 0x00115C47
		// (set) Token: 0x06004435 RID: 17461 RVA: 0x00117A5E File Offset: 0x00115C5E
		private MailboxIdParameter LogMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["LogMailbox"];
			}
			set
			{
				base.Fields["LogMailbox"] = value;
			}
		}

		// Token: 0x06004436 RID: 17462 RVA: 0x00117A74 File Offset: 0x00115C74
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			TaskLogger.LogEnter();
			try
			{
				if (this.LogLocation != null)
				{
					this.LogLocation = CalendarDiagnosticLogFileWriter.CheckAndCreateLogLocation(this.LogLocation);
				}
			}
			catch (SecurityException exception)
			{
				throw new ThrowTerminatingErrorException(new ErrorRecord(exception, string.Empty, ErrorCategory.PermissionDenied, this));
			}
			catch (ArgumentException exception2)
			{
				throw new ThrowTerminatingErrorException(new ErrorRecord(exception2, string.Empty, ErrorCategory.InvalidData, this));
			}
			catch (NotSupportedException exception3)
			{
				throw new ThrowTerminatingErrorException(new ErrorRecord(exception3, string.Empty, ErrorCategory.InvalidData, this));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x00117B14 File Offset: 0x00115D14
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.OutputLogs();
			TaskLogger.LogExit();
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x00117B28 File Offset: 0x00115D28
		protected override void InternalValidate()
		{
			this.itemId = null;
			this.objectId = null;
			LocalizedString? localizedString = null;
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!string.IsNullOrEmpty(this.MeetingID))
			{
				try
				{
					this.itemId = StoreObjectId.Deserialize(this.MeetingID);
				}
				catch (Exception)
				{
					if (!GlobalObjectId.TryParse(this.MeetingID, out this.objectId))
					{
						base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(MailboxIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, null);
					}
				}
			}
			this.logSourceUser = base.GetDataObjects(this.Identity, base.OptionalIdentityData, out localizedString).FirstOrDefault<ADUser>();
			if (this.logSourceUser == null || base.HasErrors)
			{
				base.WriteError(new ManagementObjectNotFoundException(localizedString ?? base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(MailboxIdParameter).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, null);
			}
			if (this.LogMailbox != null)
			{
				this.outputMailboxUser = base.GetDataObjects(this.LogMailbox, base.OptionalIdentityData, out localizedString).FirstOrDefault<ADUser>();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004439 RID: 17465 RVA: 0x00117CA8 File Offset: 0x00115EA8
		private void OutputLogs()
		{
			TaskLogger.LogEnter();
			ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(base.SessionSettings, this.logSourceUser, RemotingOptions.AllowCrossSite);
			using (MailboxSession mailboxSession = StoreTasksHelper.OpenMailboxSession(exchangePrincipal, "Get-CalendarDiagnosticLogs"))
			{
				Dictionary<string, List<VersionedId>> allCalendarLogItems = this.GetAllCalendarLogItems(mailboxSession);
				if (allCalendarLogItems.Keys.Count == 0)
				{
					this.WriteWarning(Strings.CalendarDiagnosticLogsNotFound(this.Subject, mailboxSession.MailboxOwner.MailboxInfo.DisplayName));
					return;
				}
				MailboxSession mailboxSession2 = null;
				try
				{
					if (this.outputMailboxUser != null)
					{
						ExchangePrincipal principal = exchangePrincipal;
						mailboxSession2 = StoreTasksHelper.OpenMailboxSession(principal, "Get-CalendarDiagnosticLogs");
					}
					else
					{
						mailboxSession2 = mailboxSession;
					}
					SmtpAddress address = new SmtpAddress(exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
					foreach (KeyValuePair<string, List<VersionedId>> keyValuePair in allCalendarLogItems)
					{
						if (!string.IsNullOrEmpty(this.LogLocation))
						{
							this.diagnosticLogWriter = new CalendarDiagnosticLogFileWriter(this.LogLocation, mailboxSession.MailboxOwner.MailboxInfo.DisplayName, address.Domain);
						}
						base.WriteProgress(Strings.GetCalendarDiagnosticLog(this.Identity.ToString()), Strings.SavingCalendarLogs, 0);
						List<VersionedId> value = keyValuePair.Value;
						int count = value.Count;
						foreach (VersionedId storeId in value)
						{
							using (Item item = Item.Bind(mailboxSession, storeId))
							{
								if (!(item.LastModifiedTime > this.EndDate) && !(item.LastModifiedTime < this.StartDate))
								{
									if (!string.IsNullOrEmpty(this.LogLocation))
									{
										string text = null;
										if (Directory.Exists(this.LogLocation))
										{
											FileInfo fileInfo = this.diagnosticLogWriter.LogItem(item, out text);
											if (fileInfo == null && !string.IsNullOrEmpty(text))
											{
												base.WriteWarning(text);
											}
											else
											{
												base.WriteResult(new CalendarLog(item, fileInfo, (string)address));
											}
										}
									}
									else
									{
										base.WriteResult(new CalendarLog(item, (string)address));
									}
								}
							}
						}
					}
				}
				finally
				{
					if (mailboxSession2 != null)
					{
						mailboxSession2.Dispose();
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600443A RID: 17466 RVA: 0x00117F98 File Offset: 0x00116198
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || exception is StorageTransientException || exception is StoragePermanentException || exception is ObjectNotFoundException || exception is IOException || exception is UnauthorizedAccessException;
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x00117FCE File Offset: 0x001161CE
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			TaskLogger.LogExit();
		}

		// Token: 0x0600443C RID: 17468 RVA: 0x00118064 File Offset: 0x00116264
		private Dictionary<string, List<VersionedId>> GetAllCalendarLogItems(MailboxSession mailboxSession)
		{
			CalendarVersionStoreGateway calendarVersionStoreGateway = new CalendarVersionStoreGateway(default(CalendarVersionStoreQueryPolicy), true);
			Dictionary<string, List<VersionedId>> resultTree = new Dictionary<string, List<VersionedId>>();
			try
			{
				if (this.itemId != null)
				{
					using (Item item = Item.Bind(mailboxSession, this.itemId, ItemBindOption.LoadRequiredPropertiesOnly, GetCalendarDiagnosticLog.requiredIdProperties))
					{
						this.Subject = item.GetValueOrDefault<string>(ItemSchema.Subject);
						byte[] valueOrDefault = item.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.CleanGlobalObjectId);
						if (valueOrDefault != null)
						{
							string cleanGoidKey = this.GetCleanGoidKey(valueOrDefault);
							calendarVersionStoreGateway.QueryByCleanGlobalObjectId(mailboxSession, new GlobalObjectId(valueOrDefault), "{735AA13D-42D0-4610-BDCB-3DF048D33957}", GetCalendarDiagnosticLog.requiredIdProperties, (PropertyBag propertyBag) => this.AddItemToResultTree(cleanGoidKey, resultTree, propertyBag), false, null, this.StartDate, this.EndDate);
						}
						else
						{
							base.WriteError(new InvalidOperationException(Strings.GetCalendarDiagnosticLogNoCleanGoidErrorMessage.ToString()), ErrorCategory.InvalidData, this);
						}
						goto IL_170;
					}
				}
				if (this.objectId != null)
				{
					calendarVersionStoreGateway.QueryByCleanGlobalObjectId(mailboxSession, this.objectId, "{735AA13D-42D0-4610-BDCB-3DF048D33957}", GetCalendarDiagnosticLog.requiredIdProperties, (PropertyBag propertyBag) => this.AddItemToResultTree(this.MeetingID, resultTree, propertyBag), false, null, this.StartDate, this.EndDate);
				}
				else
				{
					new HashSet<string>();
					calendarVersionStoreGateway.QueryBySubjectContains(mailboxSession, this.Subject, "{735AA13D-42D0-4610-BDCB-3DF048D33957}", GetCalendarDiagnosticLog.requiredIdProperties, delegate(PropertyBag propertyBag)
					{
						byte[] array;
						string cleanGoidKey = this.GetCleanGoidKey(propertyBag, out array);
						this.AddItemToResultTree(cleanGoidKey, resultTree, propertyBag);
					}, this.StartDate, this.EndDate);
				}
				IL_170:;
			}
			catch (CalendarVersionStoreNotPopulatedException exception)
			{
				base.WriteError(exception, ErrorCategory.OperationTimeout, this);
			}
			return resultTree;
		}

		// Token: 0x0600443D RID: 17469 RVA: 0x00118230 File Offset: 0x00116430
		private bool AddItemToResultTree(string cleanGoidKey, Dictionary<string, List<VersionedId>> resultTree, PropertyBag propertyBag)
		{
			List<VersionedId> list;
			if (!resultTree.TryGetValue(cleanGoidKey, out list))
			{
				list = new List<VersionedId>(1);
				resultTree.Add(cleanGoidKey, list);
				list.Add(propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id));
			}
			else if (!this.Latest.IsPresent)
			{
				list.Add(propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id));
			}
			return true;
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x0011828C File Offset: 0x0011648C
		private string GetCleanGoidKey(PropertyBag propertyBag, out byte[] cleanGoid)
		{
			cleanGoid = propertyBag.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.CleanGlobalObjectId);
			return this.GetCleanGoidKey(cleanGoid);
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x001182A3 File Offset: 0x001164A3
		private string GetCleanGoidKey(byte[] cleanGoid)
		{
			if (cleanGoid != null)
			{
				return GlobalObjectId.ByteArrayToHexString(cleanGoid);
			}
			return string.Empty;
		}

		// Token: 0x04002A4D RID: 10829
		private const string TaskName = "Get-CalendarDiagnosticLogs";

		// Token: 0x04002A4E RID: 10830
		private const string IdentityKey = "Identity";

		// Token: 0x04002A4F RID: 10831
		private const string SubjectKey = "Subject";

		// Token: 0x04002A50 RID: 10832
		private const string MeetingIDKey = "MeetingID";

		// Token: 0x04002A51 RID: 10833
		private const string LogLocationKey = "LogLocation";

		// Token: 0x04002A52 RID: 10834
		private const string LogMailboxKey = "LogMailbox";

		// Token: 0x04002A53 RID: 10835
		private const string StartDateKey = "StartDate";

		// Token: 0x04002A54 RID: 10836
		private const string EndDateKey = "EndDate";

		// Token: 0x04002A55 RID: 10837
		private const string GetLatestKey = "GetLatest";

		// Token: 0x04002A56 RID: 10838
		private const string DoNotExportKey = "DoNotExport";

		// Token: 0x04002A57 RID: 10839
		private const string DoNotExportParameterSet = "DoNotExportParameterSet";

		// Token: 0x04002A58 RID: 10840
		private const string ExportToMsgParameterSet = "ExportToMsgParameterSet";

		// Token: 0x04002A59 RID: 10841
		private const string LogRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CalendarLogs";

		// Token: 0x04002A5A RID: 10842
		private const string LogRegistryValue = "LogLocation";

		// Token: 0x04002A5B RID: 10843
		private const string RequiredPropertySetKey = "{735AA13D-42D0-4610-BDCB-3DF048D33957}";

		// Token: 0x04002A5C RID: 10844
		private ADUser logSourceUser;

		// Token: 0x04002A5D RID: 10845
		private ADUser outputMailboxUser;

		// Token: 0x04002A5E RID: 10846
		private bool IsLogLocationInitialized;

		// Token: 0x04002A5F RID: 10847
		private static StorePropertyDefinition[] requiredIdProperties = new StorePropertyDefinition[]
		{
			ItemSchema.Id,
			CalendarItemBaseSchema.CleanGlobalObjectId,
			ItemSchema.NormalizedSubject,
			CalendarItemBaseSchema.OriginalLastModifiedTime
		};

		// Token: 0x04002A60 RID: 10848
		private CalendarDiagnosticLogFileWriter diagnosticLogWriter;

		// Token: 0x04002A61 RID: 10849
		private StoreObjectId itemId;

		// Token: 0x04002A62 RID: 10850
		private GlobalObjectId objectId;
	}
}
