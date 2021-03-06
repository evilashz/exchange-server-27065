using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Configuration.TenantMonitoring;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.ProvisioningMonitoring;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CFA RID: 3322
	[Cmdlet("Test", "TenantMonitoringPerfCounters", SupportsShouldProcess = true)]
	public sealed class TestTenantMonitoringPerfCounters : Task
	{
		// Token: 0x170027A4 RID: 10148
		// (get) Token: 0x06007FC0 RID: 32704 RVA: 0x00209EBE File Offset: 0x002080BE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestTenantMonitoringPerfCounters;
			}
		}

		// Token: 0x170027A5 RID: 10149
		// (get) Token: 0x06007FC1 RID: 32705 RVA: 0x00209EC5 File Offset: 0x002080C5
		// (set) Token: 0x06007FC2 RID: 32706 RVA: 0x00209ECD File Offset: 0x002080CD
		[Parameter(Mandatory = true)]
		public string CounterNameForAttempts { get; set; }

		// Token: 0x170027A6 RID: 10150
		// (get) Token: 0x06007FC3 RID: 32707 RVA: 0x00209ED6 File Offset: 0x002080D6
		// (set) Token: 0x06007FC4 RID: 32708 RVA: 0x00209EDE File Offset: 0x002080DE
		[Parameter(Mandatory = true)]
		public string CounterNameForSuccesses { get; set; }

		// Token: 0x170027A7 RID: 10151
		// (get) Token: 0x06007FC5 RID: 32709 RVA: 0x00209EE7 File Offset: 0x002080E7
		// (set) Token: 0x06007FC6 RID: 32710 RVA: 0x00209EEF File Offset: 0x002080EF
		[Parameter(Mandatory = true)]
		public uint MinimumCountForAlert { get; set; }

		// Token: 0x170027A8 RID: 10152
		// (get) Token: 0x06007FC7 RID: 32711 RVA: 0x00209EF8 File Offset: 0x002080F8
		// (set) Token: 0x06007FC8 RID: 32712 RVA: 0x00209F00 File Offset: 0x00208100
		[Parameter(Mandatory = true)]
		public uint SuccessThresholdPercentage { get; set; }

		// Token: 0x170027A9 RID: 10153
		// (get) Token: 0x06007FC9 RID: 32713 RVA: 0x00209F09 File Offset: 0x00208109
		// (set) Token: 0x06007FCA RID: 32714 RVA: 0x00209F11 File Offset: 0x00208111
		[Parameter(Mandatory = true)]
		public uint EventId { get; set; }

		// Token: 0x06007FCB RID: 32715 RVA: 0x00209F1C File Offset: 0x0020811C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			List<string> list = new List<string>();
			StringBuilder stringBuilder = null;
			string[] instanceNames = MSExchangeTenantMonitoring.GetInstanceNames();
			if (instanceNames != null)
			{
				foreach (string text in instanceNames)
				{
					if (!string.Equals("_total", text, StringComparison.OrdinalIgnoreCase) && this.CheckInstance(text))
					{
						MSExchangeTenantMonitoringInstance instance = MSExchangeTenantMonitoring.GetInstance(text);
						double valueForCounter = this.GetValueForCounter(instance, this.CounterNameForAttempts);
						double valueForCounter2 = this.GetValueForCounter(instance, this.CounterNameForSuccesses);
						if (valueForCounter != this.GetValueForCounter(instance, this.CounterNameForAttempts) || valueForCounter2 != this.GetValueForCounter(instance, this.CounterNameForSuccesses))
						{
							ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_IgnoringInstanceData, new string[]
							{
								text
							});
						}
						else if (valueForCounter > 0.0 && valueForCounter >= this.MinimumCountForAlert && valueForCounter2 / valueForCounter * 100.0 < this.SuccessThresholdPercentage)
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(text);
							}
							else if (list.Count < 10)
							{
								stringBuilder.AppendFormat(", {0}", text);
							}
							list.Add(text);
							ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_InstanceAboveThreshold, new string[]
							{
								text,
								valueForCounter.ToString(),
								valueForCounter2.ToString()
							});
						}
					}
				}
				if (list.Count > 0)
				{
					stringBuilder.AppendFormat(", TotalFailingInstances={0}", list.Count);
				}
			}
			string text2 = (stringBuilder != null) ? stringBuilder.ToString() : string.Empty;
			if (!string.IsNullOrEmpty(text2))
			{
				if (TestTenantMonitoringPerfCounters.eventIdDictionary.ContainsKey(this.EventId))
				{
					ExManagementApplicationLogger.LogEvent(TestTenantMonitoringPerfCounters.eventIdDictionary[this.EventId], new string[]
					{
						text2,
						this.CounterNameForAttempts,
						this.CounterNameForSuccesses,
						this.MinimumCountForAlert.ToString(),
						this.SuccessThresholdPercentage.ToString(),
						this.GetFailureDetails(list, this.CounterNameForAttempts)
					});
				}
			}
			else
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_TenantMonitoringSuccess, new string[]
				{
					this.CounterNameForAttempts,
					this.CounterNameForSuccesses
				});
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06007FCC RID: 32716 RVA: 0x0020A170 File Offset: 0x00208370
		private bool CheckInstance(string instanceName)
		{
			int processId = 0;
			if (!ProvisioningMonitoringConfig.TryGetPidFromInstanceName(instanceName, ref processId))
			{
				return false;
			}
			bool result;
			try
			{
				using (Process.GetProcessById(processId))
				{
					result = true;
				}
			}
			catch (ArgumentException)
			{
				MSExchangeTenantMonitoring.RemoveInstance(instanceName);
				result = false;
			}
			return result;
		}

		// Token: 0x06007FCD RID: 32717 RVA: 0x0020A1C8 File Offset: 0x002083C8
		private double GetValueForCounter(MSExchangeTenantMonitoringInstance instance, string counterName)
		{
			double result = 0.0;
			switch (counterName)
			{
			case "MSExchangeCmdletIterationSuccesses":
				result = (double)instance.MSExchangeCmdletIterationSuccesses.RawValue;
				break;
			case "MSExchangeCmdletIterationAttempts":
				result = (double)instance.MSExchangeCmdletIterationAttempts.RawValue;
				break;
			case "MSExchangeCmdletSuccesses":
				result = (double)instance.MSExchangeCmdletSuccesses.RawValue;
				break;
			case "MSExchangeCmdletAttempts":
				result = (double)instance.MSExchangeCmdletAttempts.RawValue;
				break;
			case "MSExchangeGetManagementEndpointIterationSuccesses":
				result = (double)instance.MSExchangeGetManagementEndpointIterationSuccesses.RawValue;
				break;
			case "MSExchangeGetManagementEndpointIterationAttempts":
				result = (double)instance.MSExchangeGetManagementEndpointIterationAttempts.RawValue;
				break;
			case "MSExchangeGetManagementEndpointSuccesses":
				result = (double)instance.MSExchangeGetManagementEndpointSuccesses.RawValue;
				break;
			case "MSExchangeGetManagementEndpointAttempts":
				result = (double)instance.MSExchangeGetManagementEndpointAttempts.RawValue;
				break;
			case "MSExchangeRemoveSecondaryDomainIterationSuccesses":
				result = (double)instance.MSExchangeRemoveSecondaryDomainIterationSuccesses.RawValue;
				break;
			case "MSExchangeRemoveSecondaryDomainIterationAttempts":
				result = (double)instance.MSExchangeRemoveSecondaryDomainIterationAttempts.RawValue;
				break;
			case "MSExchangeRemoveSecondaryDomainSuccesses":
				result = (double)instance.MSExchangeRemoveSecondaryDomainSuccesses.RawValue;
				break;
			case "MSExchangeRemoveSecondaryDomainAttempts":
				result = (double)instance.MSExchangeRemoveSecondaryDomainAttempts.RawValue;
				break;
			case "MSExchangeAddSecondaryDomainIterationSuccesses":
				result = (double)instance.MSExchangeAddSecondaryDomainIterationSuccesses.RawValue;
				break;
			case "MSExchangeAddSecondaryDomainIterationAttempts":
				result = (double)instance.MSExchangeAddSecondaryDomainIterationAttempts.RawValue;
				break;
			case "MSExchangeAddSecondaryDomainSuccesses":
				result = (double)instance.MSExchangeAddSecondaryDomainSuccesses.RawValue;
				break;
			case "MSExchangeAddSecondaryDomainAttempts":
				result = (double)instance.MSExchangeAddSecondaryDomainAttempts.RawValue;
				break;
			case "MSExchangeRemoveOrganizationIterationSuccesses":
				result = (double)instance.MSExchangeRemoveOrganizationIterationSuccesses.RawValue;
				break;
			case "MSExchangeRemoveOrganizationIterationAttempts":
				result = (double)instance.MSExchangeRemoveOrganizationIterationAttempts.RawValue;
				break;
			case "MSExchangeRemoveOrganizationSuccesses":
				result = (double)instance.MSExchangeRemoveOrganizationSuccesses.RawValue;
				break;
			case "MSExchangeRemoveOrganizationAttempts":
				result = (double)instance.MSExchangeRemoveOrganizationAttempts.RawValue;
				break;
			case "MSExchangeNewOrganizationIterationSuccesses":
				result = (double)instance.MSExchangeNewOrganizationIterationSuccesses.RawValue;
				break;
			case "MSExchangeNewOrganizationIterationAttempts":
				result = (double)instance.MSExchangeNewOrganizationIterationAttempts.RawValue;
				break;
			case "MSExchangeNewOrganizationSuccesses":
				result = (double)instance.MSExchangeNewOrganizationSuccesses.RawValue;
				break;
			case "MSExchangeNewOrganizationAttempts":
				result = (double)instance.MSExchangeNewOrganizationAttempts.RawValue;
				break;
			case "MSExchangeNewMailboxIterationSuccesses":
				result = (double)instance.MSExchangeNewMailboxIterationSuccesses.RawValue;
				break;
			case "MSExchangeNewMailboxIterationAttempts":
				result = (double)instance.MSExchangeNewMailboxIterationAttempts.RawValue;
				break;
			case "MSExchangeNewMailboxSuccesses":
				result = (double)instance.MSExchangeNewMailboxSuccesses.RawValue;
				break;
			case "MSExchangeNewMailboxAttempts":
				result = (double)instance.MSExchangeNewMailboxAttempts.RawValue;
				break;
			}
			return result;
		}

		// Token: 0x06007FCE RID: 32718 RVA: 0x0020A5D0 File Offset: 0x002087D0
		private List<string> GetCmdletNamesFromAttemptsCounterName(string counterNameForAttempts)
		{
			List<string> list = new List<string>();
			switch (counterNameForAttempts)
			{
			case "MSExchangeCmdletIterationAttempts":
			case "MSExchangeCmdletAttempts":
				list.AddRange(new string[]
				{
					"get-managementendpoint",
					"remove-secondarydomain",
					"add-secondarydomain",
					"remove-organization",
					"new-organization",
					"new-mailbox",
					"new-syncmailbox"
				});
				break;
			case "MSExchangeGetManagementEndpointIterationAttempts":
			case "MSExchangeGetManagementEndpointAttempts":
				list.Add("get-managementendpoint");
				break;
			case "MSExchangeRemoveSecondaryDomainIterationAttempts":
			case "MSExchangeRemoveSecondaryDomainAttempts":
				list.Add("remove-secondarydomain");
				break;
			case "MSExchangeAddSecondaryDomainIterationAttempts":
			case "MSExchangeAddSecondaryDomainAttempts":
				list.Add("add-secondarydomain");
				break;
			case "MSExchangeRemoveOrganizationIterationAttempts":
			case "MSExchangeRemoveOrganizationAttempts":
				list.Add("remove-organization");
				break;
			case "MSExchangeNewOrganizationIterationAttempts":
			case "MSExchangeNewOrganizationAttempts":
				list.Add("new-organization");
				break;
			case "MSExchangeNewMailboxIterationAttempts":
			case "MSExchangeNewMailboxAttempts":
				list.AddRange(new string[]
				{
					"new-mailbox",
					"new-syncmailbox"
				});
				break;
			}
			return list;
		}

		// Token: 0x06007FCF RID: 32719 RVA: 0x0020A7B8 File Offset: 0x002089B8
		private string GetFailureDetails(List<string> instances, string counterNameForAttempts)
		{
			List<string> cmdletNamesFromAttemptsCounterName = this.GetCmdletNamesFromAttemptsCounterName(counterNameForAttempts);
			if (cmdletNamesFromAttemptsCounterName.Count == 0 || instances == null)
			{
				return string.Empty;
			}
			string result;
			try
			{
				List<string> list = new List<string>();
				foreach (string instanceName in instances)
				{
					string item = null;
					if (ProvisioningMonitoringConfig.TryGetOrganizationNameFromInstanceName(instanceName, ref item))
					{
						list.Add(item);
					}
				}
				if (list.Count == 0)
				{
					result = string.Empty;
				}
				else
				{
					TimeSpan timeSpan = IntervalCounterInstanceCache.UpdateInterval + IntervalCounterInstanceCache.UpdateInterval;
					DateTime t = this.startTime.Add(timeSpan.Negate());
					StringBuilder stringBuilder = new StringBuilder();
					using (EventLog eventLog = new EventLog())
					{
						eventLog.Log = "MSExchange Management";
						EventLogEntryCollection entries = eventLog.Entries;
						int count = entries.Count;
						for (int i = count - 1; i >= 0; i--)
						{
							EventLogEntry eventLogEntry = entries[i];
							if (eventLogEntry.TimeGenerated < t)
							{
								break;
							}
							if (eventLogEntry.ReplacementStrings != null && eventLogEntry.ReplacementStrings.Length >= 14 && eventLogEntry.ReplacementStrings[0] != null && eventLogEntry.EntryType == EventLogEntryType.Error && cmdletNamesFromAttemptsCounterName.Contains(eventLogEntry.ReplacementStrings[0].ToLowerInvariant()))
							{
								foreach (string text in list.ToArray())
								{
									if ((eventLogEntry.ReplacementStrings[1] != null && eventLogEntry.ReplacementStrings[1].IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1) || (eventLogEntry.ReplacementStrings[7] != null && eventLogEntry.ReplacementStrings[7].IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1))
									{
										stringBuilder.AppendLine("Invocation: ");
										foreach (string value in eventLogEntry.ReplacementStrings)
										{
											stringBuilder.AppendLine(value);
										}
										list.Remove(text);
										break;
									}
								}
								if (list.Count == 0 || stringBuilder.Length > 10000)
								{
									break;
								}
							}
						}
					}
					if (stringBuilder.Length > 10000)
					{
						stringBuilder.Remove(10000, stringBuilder.Length - 10000);
						stringBuilder.Append("...");
					}
					result = stringBuilder.ToString();
				}
			}
			catch (Exception ex)
			{
				if (instances.Count > 10)
				{
					instances = instances.GetRange(0, 10);
				}
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_FailToRetrieveErrorDetails, new string[]
				{
					counterNameForAttempts,
					string.Join(", ", instances.ToArray()),
					ex.ToString()
				});
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x04003EA4 RID: 16036
		private const string instanceNameForTotal = "_total";

		// Token: 0x04003EA5 RID: 16037
		private const int maximumInstancesForEventLog = 10;

		// Token: 0x04003EA6 RID: 16038
		private const int maximumFailureDetailsLength = 10000;

		// Token: 0x04003EA7 RID: 16039
		private readonly DateTime startTime = (DateTime)ExDateTime.Now;

		// Token: 0x04003EA8 RID: 16040
		private static Dictionary<uint, ExEventLog.EventTuple> eventIdDictionary = new Dictionary<uint, ExEventLog.EventTuple>
		{
			{
				10000U,
				ManagementEventLogConstants.Tuple_NewMailboxAttempts
			},
			{
				10001U,
				ManagementEventLogConstants.Tuple_NewMailboxIterationAttempts
			},
			{
				10002U,
				ManagementEventLogConstants.Tuple_NewOrganizationAttempts
			},
			{
				10003U,
				ManagementEventLogConstants.Tuple_NewOrganizationIterationAttempts
			},
			{
				10004U,
				ManagementEventLogConstants.Tuple_RemoveOrganizationAttempts
			},
			{
				10005U,
				ManagementEventLogConstants.Tuple_RemoveOrganizationIterationAttempts
			},
			{
				10006U,
				ManagementEventLogConstants.Tuple_AddSecondaryDomainAttempts
			},
			{
				10007U,
				ManagementEventLogConstants.Tuple_AddSecondaryDomainIterationAttempts
			},
			{
				10008U,
				ManagementEventLogConstants.Tuple_RemoveSecondaryDomainAttempts
			},
			{
				10009U,
				ManagementEventLogConstants.Tuple_RemoveSecondaryDomainIterationAttempts
			},
			{
				10010U,
				ManagementEventLogConstants.Tuple_GetManagementEndpointAttempts
			},
			{
				10011U,
				ManagementEventLogConstants.Tuple_GetManagementEndpointIterationAttempts
			},
			{
				10012U,
				ManagementEventLogConstants.Tuple_CmdletAttempts
			},
			{
				10013U,
				ManagementEventLogConstants.Tuple_CmdletIterationAttempts
			}
		};
	}
}
