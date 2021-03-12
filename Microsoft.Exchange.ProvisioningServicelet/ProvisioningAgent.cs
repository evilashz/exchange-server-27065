using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.ProvisioningServicelet;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Servicelets.Provisioning.Messages;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Servicelets.Provisioning
{
	// Token: 0x02000002 RID: 2
	internal abstract class ProvisioningAgent : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ProvisioningAgent(IProvisioningData data, ProvisioningAgentContext agentContext)
		{
			MigrationUtil.ThrowOnNullArgument(data, "data");
			MigrationUtil.ThrowOnNullArgument(agentContext, "agentContext");
			this.isUserInputError = false;
			this.data = data;
			this.agentContext = agentContext;
			this.timeStarted = ExDateTime.Now;
			this.timeFinished = null;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002125 File Offset: 0x00000325
		public Error Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000212D File Offset: 0x0000032D
		public ExDateTime TimeStarted
		{
			get
			{
				return this.timeStarted;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002135 File Offset: 0x00000335
		public ExDateTime? TimeFinished
		{
			get
			{
				return this.timeFinished;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000213D File Offset: 0x0000033D
		public IProvisioningData ProvisioningData
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002145 File Offset: 0x00000345
		public ProvisioningAgentContext AgentContext
		{
			get
			{
				return this.agentContext;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000214D File Offset: 0x0000034D
		public virtual IMailboxData MailboxData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002158 File Offset: 0x00000358
		public int GroupMemberProvisioned { get; protected set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002161 File Offset: 0x00000361
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002169 File Offset: 0x00000369
		public int GroupMemberSkipped { get; protected set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002172 File Offset: 0x00000372
		public bool IsUserInputError
		{
			get
			{
				return this.isUserInputError;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000217A File Offset: 0x0000037A
		public void Dispose()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022A0 File Offset: 0x000004A0
		public void Work()
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				Thread.CurrentThread.CurrentCulture = this.AgentContext.CultureInfo;
				Thread.CurrentThread.CurrentUICulture = this.AgentContext.CultureInfo;
				this.timeStarted = ExDateTime.Now;
				this.IncrementPerfCounterForAttempt();
				this.error = this.CreateRecipient();
				if (this.error == null)
				{
					this.IncrementPerfCounterForCompletion();
					this.timeFinished = new ExDateTime?(ExDateTime.Now);
					return;
				}
				if (this.error.IsUserInputError)
				{
					this.isUserInputError = true;
					ExTraceGlobals.WorkerTracer.TraceError<Error>(17732, (long)this.GetHashCode(), "non-fatal error {0}. Processing of the request will continue", this.error);
					this.IncrementPerfCounterForFailure();
					return;
				}
				ExTraceGlobals.WorkerTracer.TraceError<Error>(17736, (long)this.GetHashCode(), "fatal error {0}. Processing of the request will not continue", this.error);
				this.IncrementPerfCounterForTransientFailure();
				this.AgentContext.EventLog.LogEvent(MSExchangeProvisioningEventLogConstants.Tuple_NonFatalProcessingError, string.Empty, new object[]
				{
					this.AgentContext.TenantOrganization,
					this.ProvisioningData.ToString(),
					this.error
				});
			}, (object exception) => true, ReportOptions.None);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D4 File Offset: 0x000004D4
		protected static Exception FilterTaskException(Exception ex)
		{
			if (ex is CommandNotFoundException)
			{
				ex = new PermissionDeniedException(Strings.PermissionDenied);
			}
			else if (ex is ThrowTerminatingErrorException && (ex.InnerException is ManagementObjectNotFoundException || ex.InnerException is ManagementObjectAlreadyExistsException || ex.InnerException is ManagementObjectDuplicateException || ex.InnerException is ManagementObjectAmbiguousException))
			{
				ex = ex.InnerException;
			}
			return ex;
		}

		// Token: 0x06000010 RID: 16
		protected abstract Error CreateRecipient();

		// Token: 0x06000011 RID: 17 RVA: 0x0000233C File Offset: 0x0000053C
		protected T RunPSCommand<T>(PSCommand command, RunspaceProxy runspaceProxy, out Error errors)
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17800, (long)this.GetHashCode(), "RunPSCommand");
			PowerShellProxy powerShellProxy = new PowerShellProxy(runspaceProxy, command);
			Collection<T> source = powerShellProxy.Invoke<T>();
			if (powerShellProxy.Failed)
			{
				errors = new Error(powerShellProxy.Errors[0], command.Commands[0].CommandText);
				return default(T);
			}
			errors = null;
			return source.FirstOrDefault<T>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023B4 File Offset: 0x000005B4
		protected T SafeRunPSCommand<T>(PSCommand command, RunspaceProxy runspaceProxy, out Error errors, ProvisioningAgent.ErrorMessageOperation errorMessageOperation, uint? faultTraceId)
		{
			T result;
			try
			{
				if (faultTraceId != null)
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest(faultTraceId.Value);
				}
				result = this.RunPSCommand<T>(command, runspaceProxy, out errors);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3840290109U);
				errors = new Error(ProvisioningAgent.FilterTaskException(ex), null, command.Commands[0].CommandText);
				if (errorMessageOperation != null)
				{
					errors.Message = errorMessageOperation(ex.Message);
				}
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002450 File Offset: 0x00000650
		protected IEnumerable<T> RunPSCommandAndReturnAll<T>(PSCommand command, RunspaceProxy runspaceProxy, out Error errors)
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17800, (long)this.GetHashCode(), "RunPSCommandAndReturnAll");
			PowerShellProxy powerShellProxy = new PowerShellProxy(runspaceProxy, command);
			Collection<T> result = powerShellProxy.Invoke<T>();
			if (powerShellProxy.Failed)
			{
				errors = new Error(powerShellProxy.Errors[0], command.Commands[0].CommandText);
				return null;
			}
			errors = null;
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024BC File Offset: 0x000006BC
		protected bool PopulateParamsToPSCommand(PSCommand command, string[][] parameterMap, Dictionary<string, object> parameters)
		{
			ExTraceGlobals.WorkerTracer.TraceFunction(17808, (long)this.GetHashCode(), "PopulateParamsToPSCommand");
			bool result = false;
			if (parameterMap != null)
			{
				foreach (string[] array in parameterMap)
				{
					string key = array[0];
					object obj;
					if (parameters.TryGetValue(key, out obj) && obj != null)
					{
						result = true;
						string parameterName = string.IsNullOrEmpty(array[1]) ? array[0] : array[1];
						command.AddParameter(parameterName, obj);
					}
				}
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002538 File Offset: 0x00000738
		protected virtual void IncrementPerfCounterForCompletion()
		{
			BulkUserProvisioningCounters.NumberOfRecipientsCreated.Increment();
			BulkUserProvisioningCounters.RateOfRecipientsCreated.Increment();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002550 File Offset: 0x00000750
		protected virtual void IncrementPerfCounterForFailure()
		{
			BulkUserProvisioningCounters.NumberOfRecipientsFailed.Increment();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000255D File Offset: 0x0000075D
		protected virtual void IncrementPerfCounterForTransientFailure()
		{
			BulkUserProvisioningCounters.NumberOfRequestsWithTransientError.Increment();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000256C File Offset: 0x0000076C
		protected virtual void IncrementPerfCounterForAttempt()
		{
			BulkUserProvisioningCounters.NumberOfRecipientsAttempted.Increment();
			BulkUserProvisioningCounters.RateOfRecipientsAttempted.Increment();
			BulkUserProvisioningCounters.LastRecipientAttemptedTimestamp.RawValue = DateTime.UtcNow.Ticks;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025E4 File Offset: 0x000007E4
		protected virtual void UpdateProxyAddressesParameter(MailEnabledRecipient recipient)
		{
			if (this.ProvisioningData.Parameters.ContainsKey(ADRecipientSchema.EmailAddresses.Name))
			{
				string[] array = this.ProvisioningData.Parameters[ADRecipientSchema.EmailAddresses.Name] as string[];
				if (recipient.EmailAddresses != null)
				{
					string[] array2 = recipient.EmailAddresses.ToStringArray();
					if (array2 != null)
					{
						if (array == null)
						{
							array = array2;
						}
						else
						{
							string primaryProxyAddress = array.LastOrDefault((string address) => address.StartsWith("SMTP:", StringComparison.Ordinal));
							if (string.IsNullOrEmpty(primaryProxyAddress))
							{
								primaryProxyAddress = array2.LastOrDefault((string address) => address.StartsWith("SMTP:", StringComparison.Ordinal));
							}
							if (string.IsNullOrEmpty(primaryProxyAddress))
							{
								array = array.Union(array2, StringComparer.OrdinalIgnoreCase).ToArray<string>();
							}
							else
							{
								IEnumerable<string> enumerable = array.Union(array2, StringComparer.OrdinalIgnoreCase);
								enumerable = from address in enumerable
								where !address.Equals(primaryProxyAddress, StringComparison.OrdinalIgnoreCase)
								select address.ToLower();
								enumerable = enumerable.Union(new string[]
								{
									primaryProxyAddress
								}, StringComparer.Ordinal);
								array = enumerable.ToArray<string>();
							}
						}
					}
				}
				this.ProvisioningData.Parameters[ADRecipientSchema.EmailAddresses.Name] = array;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000279C File Offset: 0x0000099C
		protected void RemoveSmtpProxyAddressesWithAcceptedDomain()
		{
			if (this.ProvisioningData.Parameters.ContainsKey(ADRecipientSchema.EmailAddresses.Name))
			{
				IEnumerable<string> enumerable = this.ProvisioningData.Parameters[ADRecipientSchema.EmailAddresses.Name] as string[];
				if (enumerable != null)
				{
					PSCommand command = new PSCommand().AddCommand("Get-AcceptedDomain");
					this.PopulateParamsToPSCommand(command, ProvisioningAgent.getAcceptedDomainParameterMap, this.ProvisioningData.Parameters);
					Error error;
					IEnumerable<AcceptedDomain> enumerable2 = this.RunPSCommandAndReturnAll<AcceptedDomain>(command, this.AgentContext.Runspace, out error);
					if (error != null || enumerable2 == null)
					{
						return;
					}
					foreach (AcceptedDomain acceptedDomain in enumerable2)
					{
						if ((acceptedDomain.DomainType == AcceptedDomainType.Authoritative || acceptedDomain.DomainType == AcceptedDomainType.InternalRelay) && acceptedDomain.DomainName != null && acceptedDomain.DomainName.SmtpDomain != null)
						{
							string domainPart = "@" + acceptedDomain.DomainName.SmtpDomain.Domain;
							enumerable = from address in enumerable
							where !address.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase) || !address.EndsWith(domainPart, StringComparison.OrdinalIgnoreCase)
							select address;
						}
					}
					this.ProvisioningData.Parameters[ADRecipientSchema.EmailAddresses.Name] = enumerable.ToArray<string>();
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002920 File Offset: 0x00000B20
		protected void RemoveSmtpProxyAddressesWithExternalDomain()
		{
			if (this.ProvisioningData.Parameters.ContainsKey(ADRecipientSchema.EmailAddresses.Name))
			{
				IEnumerable<string> enumerable = this.ProvisioningData.Parameters[ADRecipientSchema.EmailAddresses.Name] as string[];
				if (enumerable != null)
				{
					PSCommand command = new PSCommand().AddCommand("Get-AcceptedDomain");
					this.PopulateParamsToPSCommand(command, ProvisioningAgent.getAcceptedDomainParameterMap, this.ProvisioningData.Parameters);
					Error error;
					IEnumerable<AcceptedDomain> enumerable2 = this.RunPSCommandAndReturnAll<AcceptedDomain>(command, this.AgentContext.Runspace, out error);
					if (error != null || enumerable2 == null)
					{
						return;
					}
					IEnumerable<string> enumerable3 = enumerable;
					foreach (AcceptedDomain acceptedDomain in enumerable2)
					{
						if ((acceptedDomain.DomainType == AcceptedDomainType.Authoritative || acceptedDomain.DomainType == AcceptedDomainType.InternalRelay) && acceptedDomain.DomainName != null && acceptedDomain.DomainName.SmtpDomain != null)
						{
							string domainPart = "@" + acceptedDomain.DomainName.SmtpDomain.Domain;
							enumerable3 = from address in enumerable3
							where address.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase) && !address.EndsWith(domainPart, StringComparison.OrdinalIgnoreCase)
							select address;
						}
					}
					enumerable = enumerable.Except(enumerable3, StringComparer.OrdinalIgnoreCase);
					this.ProvisioningData.Parameters[ADRecipientSchema.EmailAddresses.Name] = enumerable.ToArray<string>();
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly string[][] getAcceptedDomainParameterMap = new string[][]
		{
			new string[]
			{
				"Organization",
				string.Empty
			}
		};

		// Token: 0x04000002 RID: 2
		private ProvisioningAgentContext agentContext;

		// Token: 0x04000003 RID: 3
		private IProvisioningData data;

		// Token: 0x04000004 RID: 4
		private Error error;

		// Token: 0x04000005 RID: 5
		private ExDateTime timeStarted;

		// Token: 0x04000006 RID: 6
		private ExDateTime? timeFinished;

		// Token: 0x04000007 RID: 7
		private bool isUserInputError;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x06000023 RID: 35
		protected delegate LocalizedString ErrorMessageOperation(string message);
	}
}
