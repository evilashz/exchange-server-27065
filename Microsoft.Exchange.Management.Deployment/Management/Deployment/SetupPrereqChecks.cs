using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Threading.Tasks;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Analysis;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200002A RID: 42
	public class SetupPrereqChecks
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000038DA File Offset: 0x00001ADA
		public SetupPrereqChecks() : this(SetupMode.All, SetupRole.All, null)
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000038EA File Offset: 0x00001AEA
		public SetupPrereqChecks(SetupMode mode, SetupRole role, GlobalParameters globalParameters)
		{
			this.mode = mode;
			this.role = role;
			this.globalParameters = globalParameters;
			this.DataProviderFactory = new DataProviderFactory();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003912 File Offset: 0x00001B12
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000391A File Offset: 0x00001B1A
		internal IDataProviderFactory DataProviderFactory { get; set; }

		// Token: 0x060000A5 RID: 165 RVA: 0x00003954 File Offset: 0x00001B54
		public void DoCheckPrereqs(Action<int> writeProgress, Microsoft.Exchange.Configuration.Tasks.Task task)
		{
			PrereqAnalysis prereqAnalysis = new PrereqAnalysis(this.DataProviderFactory, this.mode, this.role, this.globalParameters);
			this.DoAnalysis(writeProgress, prereqAnalysis);
			try
			{
				SetupLogger.IsPrereqLogging = true;
				foreach (Result result in prereqAnalysis.Conclusions)
				{
					RuleResult ruleResult = (RuleResult)result;
					if (!ruleResult.HasException && ruleResult.Value)
					{
						Rule rule = (Rule)ruleResult.Source;
						string text = string.Empty;
						MessageFeature messageFeature = (MessageFeature)rule.Features.First((Feature x) => x.GetType() == typeof(MessageFeature));
						if (messageFeature != null)
						{
							text = messageFeature.Text(ruleResult);
						}
						RuleTypeFeature ruleTypeFeature = (RuleTypeFeature)rule.Features.FirstOrDefault((Feature x) => x.GetType() == typeof(RuleTypeFeature));
						if (ruleTypeFeature != null)
						{
							RuleType ruleType = ruleTypeFeature.RuleType;
							LocalizedString text2 = new LocalizedString(text);
							string helpUrl = ruleResult.GetHelpUrl();
							if (ruleType == RuleType.Error)
							{
								task.WriteError(new Exception(text), ErrorCategory.NotSpecified, rule.Name, false, helpUrl);
							}
							else if (ruleType == RuleType.Warning)
							{
								task.WriteWarning(text2, helpUrl);
							}
							else
							{
								task.WriteVerbose(text2);
							}
						}
					}
				}
			}
			finally
			{
				SetupLogger.IsPrereqLogging = false;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B50 File Offset: 0x00001D50
		internal void DoAnalysis(Action<int> writeProgress, Analysis analysis)
		{
			BlockingCollection<int> progressCollection = new BlockingCollection<int>();
			writeProgress(1);
			analysis.ProgressUpdated += delegate(object sender, ProgressUpdateEventArgs arg)
			{
				try
				{
					int num = Math.Max(1, arg.CompletedPercentage);
					progressCollection.Add(num);
					if (num == 100)
					{
						progressCollection.CompleteAdding();
					}
				}
				catch (InvalidOperationException)
				{
				}
			};
			System.Threading.Tasks.Task.Factory.StartNew(delegate()
			{
				analysis.StartAnalysis();
			});
			while (!progressCollection.IsCompleted)
			{
				try
				{
					writeProgress(progressCollection.Take());
				}
				catch (InvalidOperationException)
				{
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BE0 File Offset: 0x00001DE0
		internal static Process GetParentProcess()
		{
			int num = 0;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				num = currentProcess.Id;
			}
			Process result = null;
			string queryString = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", num);
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", queryString))
			{
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementBaseObject managementBaseObject = enumerator.Current;
						uint processId = (uint)managementBaseObject["ParentProcessId"];
						result = Process.GetProcessById((int)processId);
					}
				}
			}
			return result;
		}

		// Token: 0x04000093 RID: 147
		private const int ProgressStarted = 1;

		// Token: 0x04000094 RID: 148
		private const int ProgressCompleted = 100;

		// Token: 0x04000095 RID: 149
		private readonly SetupMode mode;

		// Token: 0x04000096 RID: 150
		private readonly SetupRole role;

		// Token: 0x04000097 RID: 151
		private GlobalParameters globalParameters;
	}
}
