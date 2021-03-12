using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001EF RID: 495
	internal abstract class DatabaseValidatorBase
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0004F168 File Offset: 0x0004D368
		protected static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004F170 File Offset: 0x0004D370
		protected DatabaseValidatorBase(IADDatabase database, int numHealthyCopiesMinimum, ICopyStatusClientLookup statusLookup, IMonitoringADConfig adConfig, PropertyUpdateTracker propertyUpdateTracker = null, bool isCopyRemoval = false, bool ignoreActivationDisfavored = true, bool ignoreMaintenanceChecks = true, bool ignoreTooManyActivesCheck = true, bool shouldSkipEvents = true) : this(database, numHealthyCopiesMinimum, 0, statusLookup, adConfig, propertyUpdateTracker, isCopyRemoval, ignoreActivationDisfavored, ignoreMaintenanceChecks, ignoreTooManyActivesCheck, shouldSkipEvents)
		{
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004F198 File Offset: 0x0004D398
		protected DatabaseValidatorBase(IADDatabase database, int numHealthyCopiesMinimum, int numHealthyPassiveCopiesMinimum, ICopyStatusClientLookup statusLookup, IMonitoringADConfig adConfig, PropertyUpdateTracker propertyUpdateTracker = null, bool isCopyRemoval = false, bool ignoreActivationDisfavored = true, bool ignoreMaintenanceChecks = true, bool ignoreTooManyActivesCheck = true, bool shouldSkipEvents = true)
		{
			this.m_database = database;
			this.m_statusLookup = statusLookup;
			this.m_adConfig = adConfig;
			this.m_propertyUpdateTracker = propertyUpdateTracker;
			if (this.m_propertyUpdateTracker == null)
			{
				this.m_propertyUpdateTracker = new PropertyUpdateTracker();
			}
			this.m_isCopyRemoval = isCopyRemoval;
			this.m_ignoreActivationDisfavored = ignoreActivationDisfavored;
			this.m_ignoreMaintenanceChecks = ignoreMaintenanceChecks;
			this.m_ignoreTooManyActivesCheck = ignoreTooManyActivesCheck;
			this.m_skipEvents = shouldSkipEvents;
			this.m_replayStateRegKey = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\State\\{0}", database.Guid.ToString());
			this.m_result = new DatabaseValidationResult(database.Name, database.Guid, adConfig.TargetServerName, numHealthyCopiesMinimum, numHealthyPassiveCopiesMinimum);
			this.m_errors = new Dictionary<AmServerName, string>(5);
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0004F255 File Offset: 0x0004D455
		public IHealthValidationResult Result
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0004F25D File Offset: 0x0004D45D
		protected IADDatabase Database
		{
			get
			{
				return this.m_database;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0004F265 File Offset: 0x0004D465
		protected IEnumerable<CopyStatusClientCachedEntry> CopyStatuses
		{
			get
			{
				return this.m_statuses;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0004F26D File Offset: 0x0004D46D
		protected ICopyStatusClientLookup StatusLookup
		{
			get
			{
				return this.m_statusLookup;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0004F275 File Offset: 0x0004D475
		protected PropertyUpdateTracker PropertyUpdateTracker
		{
			get
			{
				return this.m_propertyUpdateTracker;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001392 RID: 5010
		protected abstract DatabaseValidationMultiChecks ActiveCopyChecks { get; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001393 RID: 5011
		protected abstract DatabaseValidationMultiChecks PassiveCopyChecks { get; }

		// Token: 0x06001394 RID: 5012
		protected abstract string GetValidationRollupErrorMessage(int healthyCopiesCount, int expectedHealthyCopiesCount, int totalPassiveCopiesCount, int healthyPassiveCopiesCount, string rollupMessage);

		// Token: 0x06001395 RID: 5013
		protected abstract Exception UpdateReplayStateProperties(RegistryStateAccess regState, bool validationChecksPassed);

		// Token: 0x06001396 RID: 5014 RVA: 0x0004F27D File Offset: 0x0004D47D
		protected void RecordError(AmServerName server, string error)
		{
			DatabaseValidatorBase.Tracer.TraceError<AmServerName, string>((long)this.GetHashCode(), "DatabaseValidatorBase: RecordError() for server '{0}'. Error: {1}", server, error);
			this.m_errors[server] = error;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0004F2A4 File Offset: 0x0004D4A4
		protected Exception UpdateReplayStatePropertiesCommon(RegistryStateAccess regState, bool validationChecksPassed, string lastChecksPassedTimeRegkeyName, string isLastChecksPassedRegkeyName)
		{
			DateTime utcNow = DateTime.UtcNow;
			Exception ex;
			if (validationChecksPassed)
			{
				ex = regState.WriteString(lastChecksPassedTimeRegkeyName, utcNow.ToFileTime().ToString());
				if (ex != null)
				{
					DatabaseValidatorBase.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "UpdateReplayStateProperties(): Failed to write '{0}' value. Exception: {1}", lastChecksPassedTimeRegkeyName, ex);
				}
			}
			ex = regState.WriteString(isLastChecksPassedRegkeyName, validationChecksPassed.ToString());
			if (ex != null)
			{
				DatabaseValidatorBase.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "UpdateReplayStateProperties(): Failed to write '{0}' value. Exception: {1}", isLastChecksPassedRegkeyName, ex);
			}
			return ex;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004F33C File Offset: 0x0004D53C
		public IHealthValidationResult Run()
		{
			Exception ex = null;
			try
			{
				if (this.m_isCopyRemoval)
				{
					this.LookupCachedCopyStatuses(this.Database, this.m_adConfig.TargetServerName);
				}
				else
				{
					this.LookupCachedCopyStatuses(this.Database, null);
				}
				this.m_result.ActiveCopyStatus = (from status in this.CopyStatuses
				where status.IsActive
				select status).FirstOrDefault<CopyStatusClientCachedEntry>();
				this.m_result.TargetCopyStatus = this.CopyStatuses.FirstOrDefault((CopyStatusClientCachedEntry status) => status.ServerContacted.Equals(this.m_adConfig.TargetServerName));
				if (this.m_isCopyRemoval && this.m_result.ActiveCopyStatus != null)
				{
					this.m_ignoreTooManyActivesCheck = true;
				}
				foreach (CopyStatusClientCachedEntry status2 in this.CopyStatuses)
				{
					if (this.IsCopyHealthy(status2, this.m_result.ActiveCopyStatus, this.StatusLookup))
					{
						this.OnCopyIsHealthy(status2);
					}
					else
					{
						this.OnCopyFailed(status2);
					}
					if (this.m_isAnyCachedCopyStatusStale)
					{
						this.m_result.IsAnyCachedCopyStatusStale = true;
					}
				}
			}
			catch (DatabaseValidationException ex2)
			{
				ex = ex2;
			}
			catch (AmCommonTransientException ex3)
			{
				ex = ex3;
			}
			catch (AmServerException ex4)
			{
				ex = ex4;
			}
			catch (AmServerTransientException ex5)
			{
				ex = ex5;
			}
			catch (ADOperationException ex6)
			{
				ex = ex6;
			}
			catch (ADTransientException ex7)
			{
				ex = ex7;
			}
			finally
			{
				if (this.m_result.IsValidationSuccessful)
				{
					DatabaseValidatorBase.Tracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "DatabaseValidatorBase: Run() for database '{0}' has {1} copies that passed validation, with minimum of {2}. Returning true.", this.Database.Name, this.m_result.HealthyCopiesCount, this.m_result.MinimumNumHealthyCopies);
				}
				else
				{
					DatabaseValidatorBase.Tracer.TraceError<string, int, int>((long)this.GetHashCode(), "DatabaseValidatorBase: Run() for database '{0}' has {1} copies that passed validation, when minimum is {2}. Returning false.", this.Database.Name, this.m_result.HealthyCopiesCount, this.m_result.MinimumNumHealthyCopies);
				}
			}
			if (ex != null)
			{
				DatabaseValidatorBase.Tracer.TraceError<string, Exception>((long)this.GetHashCode(), "DatabaseValidatorBase: Run() for database '{0}' failed with exception: {1}", this.Database.Name, ex);
				this.RecordError(this.m_adConfig.TargetServerName, ex.Message);
			}
			else
			{
				int num = this.CopyStatuses.Count<CopyStatusClientCachedEntry>();
				if (num < this.m_result.MinimumNumHealthyCopies && !this.m_errors.ContainsKey(this.m_adConfig.TargetServerName))
				{
					this.RecordError(AmServerName.LocalComputerName, ReplayStrings.DbValidationNotEnoughCopies(this.Database.Name));
				}
			}
			this.m_result.ErrorMessage = this.GetErrorMessage(false);
			this.m_result.ErrorMessageWithoutFullStatus = this.GetErrorMessage(true);
			return this.m_result;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004F690 File Offset: 0x0004D890
		private string GetErrorMessage(bool skipFullCopyStatusDump)
		{
			if (this.m_errors.Count == 0 && this.m_result.IsValidationSuccessful && skipFullCopyStatusDump)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(2048);
			stringBuilder.AppendLine();
			foreach (KeyValuePair<AmServerName, string> keyValuePair in this.m_errors)
			{
				stringBuilder.AppendFormat("\r\n\r\n        {0}:\r\n        {1}\r\n        ", keyValuePair.Key.NetbiosName, keyValuePair.Value);
			}
			if (!skipFullCopyStatusDump && this.CopyStatuses != null)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("================");
				stringBuilder.AppendLine(ReplayStrings.DbValidationFullCopyStatusResultsLabel);
				stringBuilder.AppendLine("================");
				stringBuilder.AppendLine();
				foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in this.CopyStatuses)
				{
					stringBuilder.AppendLine("----------------");
					stringBuilder.AppendFormat("{0} : {1}\\{2}", ReplayStrings.DbValidationCopyStatusNameLabel, this.Database.Name, copyStatusClientCachedEntry.ServerContacted.NetbiosName);
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("----------------");
					stringBuilder.AppendLine(CopyStatusHelper.GetCopyStatusMonitoringDisplayString(copyStatusClientCachedEntry.CopyStatus));
					stringBuilder.AppendLine();
				}
			}
			if (this.m_errors.Count == 0 && this.m_result.IsValidationSuccessful)
			{
				return stringBuilder.ToString();
			}
			return this.GetValidationRollupErrorMessage(this.m_result.HealthyCopiesCount, this.m_result.MinimumNumHealthyCopies, this.m_result.TotalPassiveCopiesCount, this.m_result.HealthyPassiveCopiesCount, stringBuilder.ToString());
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004F878 File Offset: 0x0004DA78
		private bool IsCopyHealthy(CopyStatusClientCachedEntry status, CopyStatusClientCachedEntry activeStatus, ICopyStatusClientLookup statusLookup)
		{
			DatabaseValidationCheck.Arguments arguments = new DatabaseValidationCheck.Arguments(status.ServerContacted, status.ActiveServer, this.Database, status, activeStatus, statusLookup, this.m_adConfig, this.m_propertyUpdateTracker, this.m_ignoreActivationDisfavored, this.m_isCopyRemoval, this.m_ignoreMaintenanceChecks, this.m_ignoreTooManyActivesCheck);
			DatabaseValidationMultiChecks databaseValidationMultiChecks = status.IsActive ? this.ActiveCopyChecks : this.PassiveCopyChecks;
			foreach (DatabaseValidationCheck databaseValidationCheck in databaseValidationMultiChecks)
			{
				LocalizedString empty = LocalizedString.Empty;
				switch (databaseValidationCheck.Validate(arguments, ref empty))
				{
				case DatabaseValidationCheck.Result.Warning:
					if (!this.m_skipEvents)
					{
						ReplayCrimsonEvents.DatabaseCopyValidationCheckWarning.LogPeriodic<string, string, string>(arguments.DatabaseCopyName.GetHashCode() ^ databaseValidationCheck.CheckName.GetHashCode(), DiagCore.DefaultEventSuppressionInterval, arguments.DatabaseCopyName, databaseValidationCheck.CheckName, EventUtil.TruncateStringInput(empty, 32766));
					}
					break;
				case DatabaseValidationCheck.Result.Failed:
					this.RecordError(status.ServerContacted, empty);
					if (databaseValidationCheck.CheckId == DatabaseValidationCheck.ID.DatabaseCheckCopyStatusNotStale)
					{
						this.m_isAnyCachedCopyStatusStale = true;
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004F9C0 File Offset: 0x0004DBC0
		private void UpdateReplayStateWithError(CopyStatusClientCachedEntry status, bool validationChecksPassed)
		{
			Exception ex = null;
			try
			{
				using (RegistryStateAccess registryStateAccess = new RegistryStateAccess(this.m_replayStateRegKey))
				{
					ex = this.UpdateReplayStateProperties(registryStateAccess, validationChecksPassed);
				}
			}
			catch (RegistryParameterException ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				DatabaseValidatorBase.Tracer.TraceError<Guid, string, Exception>((long)this.GetHashCode(), "UpdateReplayStateWithError() for db copy '{0}\\{1}' failed to write to registry state: Error: {2}", status.DbGuid, status.ServerContacted.NetbiosName, ex);
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004FA7C File Offset: 0x0004DC7C
		private void LookupCachedCopyStatuses(IADDatabase database, AmServerName serverNameToExclude)
		{
			IADDatabaseCopy[] databaseCopies = database.DatabaseCopies;
			if (databaseCopies == null || databaseCopies.Length == 0)
			{
				throw new DatabaseValidationNoCopiesException(database.Name);
			}
			IEnumerable<AmServerName> servers = from dbCopy in databaseCopies
			where serverNameToExclude == null || !dbCopy.HostServerName.Equals(serverNameToExclude.NetbiosName, StringComparison.CurrentCultureIgnoreCase)
			select new AmServerName(dbCopy.HostServerName, false);
			this.m_statuses = this.m_statusLookup.GetCopyStatusesByDatabase(database.Guid, servers, CopyStatusClientLookupFlags.None);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004FB00 File Offset: 0x0004DD00
		private void OnCopyIsHealthy(CopyStatusClientCachedEntry status)
		{
			string siteName = this.GetSiteName(status);
			this.m_result.ReportHealthyCopy(status.ServerContacted, siteName);
			if (status.IsLocalCopy)
			{
				this.UpdateReplayStateWithError(status, true);
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004FB38 File Offset: 0x0004DD38
		private void OnCopyFailed(CopyStatusClientCachedEntry status)
		{
			string siteName = this.GetSiteName(status);
			this.m_result.ReportFailedCopy(status.ServerContacted, siteName);
			if (status.IsLocalCopy)
			{
				this.UpdateReplayStateWithError(status, false);
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004FB70 File Offset: 0x0004DD70
		private string GetSiteName(CopyStatusClientCachedEntry status)
		{
			IADServer iadserver = this.m_adConfig.LookupMiniServerByName(status.ServerContacted);
			string text = null;
			if (iadserver != null && iadserver.ServerSite != null)
			{
				text = iadserver.ServerSite.Name;
			}
			DatabaseValidatorBase.Tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "DatabaseValidatorBase: GetSiteName() for copy '{0}\\{1}' : AD Site name is '{2}'.", this.Database.Name, status.ServerContacted.NetbiosName, text ?? "<null>");
			return text;
		}

		// Token: 0x04000785 RID: 1925
		private readonly IADDatabase m_database;

		// Token: 0x04000786 RID: 1926
		private readonly Dictionary<AmServerName, string> m_errors;

		// Token: 0x04000787 RID: 1927
		private readonly ICopyStatusClientLookup m_statusLookup;

		// Token: 0x04000788 RID: 1928
		private readonly IMonitoringADConfig m_adConfig;

		// Token: 0x04000789 RID: 1929
		private readonly DatabaseValidationResult m_result;

		// Token: 0x0400078A RID: 1930
		private readonly bool m_isCopyRemoval;

		// Token: 0x0400078B RID: 1931
		private readonly bool m_ignoreActivationDisfavored;

		// Token: 0x0400078C RID: 1932
		private readonly bool m_ignoreMaintenanceChecks;

		// Token: 0x0400078D RID: 1933
		private readonly bool m_skipEvents;

		// Token: 0x0400078E RID: 1934
		private PropertyUpdateTracker m_propertyUpdateTracker;

		// Token: 0x0400078F RID: 1935
		private bool m_ignoreTooManyActivesCheck;

		// Token: 0x04000790 RID: 1936
		private bool m_isAnyCachedCopyStatusStale;

		// Token: 0x04000791 RID: 1937
		private readonly string m_replayStateRegKey;

		// Token: 0x04000792 RID: 1938
		private IEnumerable<CopyStatusClientCachedEntry> m_statuses;
	}
}
