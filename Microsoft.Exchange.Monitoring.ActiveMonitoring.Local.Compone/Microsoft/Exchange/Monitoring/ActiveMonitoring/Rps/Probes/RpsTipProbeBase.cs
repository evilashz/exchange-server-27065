using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Extensions;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Rps.Probes
{
	// Token: 0x0200042F RID: 1071
	public class RpsTipProbeBase : RPSLogonProbe
	{
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0009B638 File Offset: 0x00099838
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x0009B640 File Offset: 0x00099840
		protected DateTime StartTime { get; set; }

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0009B64C File Offset: 0x0009984C
		protected string ExecutionId
		{
			get
			{
				if (string.IsNullOrEmpty(this.executionId))
				{
					if (base.Definition.Attributes.ContainsKey("ExecutionId"))
					{
						this.executionId = base.Definition.Attributes["ExecutionId"];
					}
					if (string.IsNullOrEmpty(this.executionId))
					{
						this.executionId = DateTime.UtcNow.ToString("yyMMddhhmm");
					}
				}
				return this.executionId;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0009B6C4 File Offset: 0x000998C4
		protected string ServicePlan
		{
			get
			{
				if (string.IsNullOrEmpty(this.servicePlan))
				{
					if (base.Definition.Attributes.ContainsKey("ServicePlan"))
					{
						this.servicePlan = base.Definition.Attributes["ServicePlan"];
					}
					else
					{
						this.servicePlan = "BPOS";
					}
				}
				return this.servicePlan;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0009B723 File Offset: 0x00099923
		protected string LiveIDParameterName
		{
			get
			{
				if (this.ServicePlan.Equals("BPOS", StringComparison.OrdinalIgnoreCase))
				{
					return "MicrosoftOnlineServicesID";
				}
				return "WindowsLiveID";
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x0009B743 File Offset: 0x00099943
		protected string DomainName
		{
			get
			{
				return base.Definition.Account.Substring(base.Definition.Account.IndexOf("@") + 1);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0009B76C File Offset: 0x0009996C
		protected string HostName
		{
			get
			{
				if (string.IsNullOrEmpty(this.hostName))
				{
					Uri uri = new Uri(base.Definition.Endpoint);
					this.hostName = uri.Host;
				}
				return this.hostName;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x0009B7AC File Offset: 0x000999AC
		protected string AccountUserName
		{
			get
			{
				return base.Definition.Account.Split(new char[]
				{
					'@'
				})[0];
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0009B7D8 File Offset: 0x000999D8
		public RpsTipProbeBase()
		{
			this.psInvocationSetting = new PSInvocationSettings();
			this.psInvocationSetting.Host = new RunspaceHost();
			this.psInvocationSetting.RemoteStreamOptions = RemoteStreamOptions.AddInvocationInfo;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0009B808 File Offset: 0x00099A08
		protected override Runspace InvokeCmdlet()
		{
			try
			{
				using (PowerShell powerShell = PowerShell.Create())
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RPSTracer, base.TraceContext, "Begin to execute TIP scenarioes", null, "InvokeCmdlet", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\TipProbes\\RpsTipProbeBase.cs", 177);
					this.ExecuteTipScenarioes(powerShell);
					WTFDiagnostics.TraceInformation(ExTraceGlobals.RPSTracer, base.TraceContext, "End to execute TIP scenarioes", null, "InvokeCmdlet", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\RPS\\TipProbes\\RpsTipProbeBase.cs", 179);
				}
			}
			catch (Exception innerException)
			{
				base.ThrowFailureException("Execute cmdlet failed.", DateTime.UtcNow - this.StartTime, innerException);
			}
			finally
			{
				try
				{
					base.Result.StateAttribute11 = base.Result.StateAttribute11 + ((WSManConnectionInfo)base.Runspace.ConnectionInfo).ConnectionUri.ToString();
				}
				catch (Exception ex)
				{
					base.Result.StateAttribute11 = base.Result.StateAttribute11 + "Exception:" + ex.ToString();
				}
			}
			return base.Runspace;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0009B938 File Offset: 0x00099B38
		protected virtual void ExecuteTipScenarioes(PowerShell powershell)
		{
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0009B93C File Offset: 0x00099B3C
		protected Collection<PSObject> ExecuteCmdlet(PowerShell powerShell, Command command)
		{
			this.StartTime = DateTime.UtcNow;
			powerShell.Commands.Clear();
			powerShell.Commands.AddCommand(command);
			powerShell.Runspace = base.Runspace;
			Collection<PSObject> result = powerShell.Invoke(null, this.psInvocationSetting);
			TimeSpan timeSpan = DateTime.UtcNow - this.StartTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.GetCommandDescription(command));
			stringBuilder.AppendLine(string.Format(" ({0} seconds)", timeSpan.TotalSeconds));
			stringBuilder.AppendLine();
			if (powerShell.Streams.Error != null && powerShell.Streams.Error.Count > 0)
			{
				stringBuilder.AppendLine("Error Information:");
				foreach (ErrorRecord errorRecord in powerShell.Streams.Error)
				{
					stringBuilder.AppendLine(errorRecord.Exception.ToString());
				}
				throw new ApplicationException(stringBuilder.ToString());
			}
			if (powerShell.Streams.Warning != null && powerShell.Streams.Warning.Count > 0)
			{
				stringBuilder.AppendLine("Warning message: ");
				foreach (WarningRecord warningRecord in powerShell.Streams.Warning)
				{
					stringBuilder.AppendLine(warningRecord.Message);
				}
				powerShell.Streams.Warning.Clear();
			}
			if (string.IsNullOrEmpty(base.Result.StateAttribute5))
			{
				base.Result.StateAttribute5 = stringBuilder.ToString();
			}
			else
			{
				ProbeResult result2 = base.Result;
				result2.StateAttribute5 += stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0009BB28 File Offset: 0x00099D28
		protected string GetUniqueName(string baseName)
		{
			return string.Format("{0}_{1}_{2}", baseName, this.ExecutionId, (DateTime.UtcNow.Ticks % 600000000L).ToString());
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0009BB64 File Offset: 0x00099D64
		protected PSObject CreateMailbox(PowerShell powershell, string baseName)
		{
			string uniqueName = this.GetUniqueName(baseName);
			Collection<PSObject> collection = this.ExecuteCmdlet(powershell, new Command("New-Mailbox")
			{
				Parameters = 
				{
					{
						"Name",
						uniqueName
					},
					{
						this.LiveIDParameterName,
						uniqueName + "@" + this.DomainName
					},
					{
						"Password",
						base.Definition.AccountPassword.ConvertToSecureString()
					},
					{
						"ResetPasswordOnNextLogon",
						false
					}
				}
			});
			if (collection.Count <= 0)
			{
				throw new ApplicationException("New-Mailbox return no result");
			}
			return collection[0];
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0009BC14 File Offset: 0x00099E14
		protected void RemoveObject(PowerShell powershell, string objectType, string identity)
		{
			if (string.IsNullOrEmpty(objectType) || string.IsNullOrEmpty(identity))
			{
				return;
			}
			this.ExecuteCmdlet(powershell, new Command("Remove-" + objectType)
			{
				Parameters = 
				{
					{
						"Identity",
						identity
					}
				}
			});
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0009BC60 File Offset: 0x00099E60
		private string GetCommandDescription(Command command)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(command.CommandText);
			foreach (CommandParameter commandParameter in command.Parameters)
			{
				stringBuilder.AppendFormat(" -{0}:{1}", commandParameter.Name, commandParameter.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0009BCD8 File Offset: 0x00099ED8
		protected PSObject NewDistributionGroup(PowerShell powershell, string baseName)
		{
			return this.NewDistributionGroup(powershell, baseName, this.AccountUserName);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0009BCE8 File Offset: 0x00099EE8
		protected PSObject NewDistributionGroup(PowerShell powershell, string baseName, string managedBy)
		{
			string uniqueName = this.GetUniqueName(baseName);
			Collection<PSObject> collection = this.ExecuteCmdlet(powershell, new Command("New-DistributionGroup")
			{
				Parameters = 
				{
					{
						"Name",
						uniqueName
					},
					{
						"ManagedBy",
						managedBy
					}
				}
			});
			if (collection.Count <= 0)
			{
				throw new ApplicationException("New-DistributionGroup didn't return any result");
			}
			return collection[0];
		}

		// Token: 0x040012D8 RID: 4824
		private string executionId;

		// Token: 0x040012D9 RID: 4825
		private string servicePlan;

		// Token: 0x040012DA RID: 4826
		private string hostName;

		// Token: 0x040012DB RID: 4827
		private PSInvocationSettings psInvocationSetting;
	}
}
