using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200059A RID: 1434
	[Cmdlet("Invoke", "MonitoringProbe")]
	public sealed class InvokeMonitoringProbe : Task
	{
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x000CE41C File Offset: 0x000CC61C
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x000CE433 File Offset: 0x000CC633
		[ValidateNotNullOrEmpty]
		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string Identity
		{
			get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x000CE446 File Offset: 0x000CC646
		// (set) Token: 0x06003274 RID: 12916 RVA: 0x000CE44E File Offset: 0x000CC64E
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public ServerIdParameter Server
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000CE457 File Offset: 0x000CC657
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x000CE46E File Offset: 0x000CC66E
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string ItemTargetExtension
		{
			get
			{
				return (string)base.Fields["ItemTargetExtension"];
			}
			set
			{
				base.Fields["ItemTargetExtension"] = value;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000CE481 File Offset: 0x000CC681
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x000CE498 File Offset: 0x000CC698
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string Account
		{
			get
			{
				return (string)base.Fields["Account"];
			}
			set
			{
				base.Fields["Account"] = value;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000CE4AB File Offset: 0x000CC6AB
		// (set) Token: 0x0600327A RID: 12922 RVA: 0x000CE4C2 File Offset: 0x000CC6C2
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Password
		{
			get
			{
				return (string)base.Fields["Password"];
			}
			set
			{
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000CE4D5 File Offset: 0x000CC6D5
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x000CE4EC File Offset: 0x000CC6EC
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Endpoint
		{
			get
			{
				return (string)base.Fields["Endpoint"];
			}
			set
			{
				base.Fields["Endpoint"] = value;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000CE4FF File Offset: 0x000CC6FF
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000CE516 File Offset: 0x000CC716
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string SecondaryAccount
		{
			get
			{
				return (string)base.Fields["SecondaryAccount"];
			}
			set
			{
				base.Fields["SecondaryAccount"] = value;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000CE529 File Offset: 0x000CC729
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000CE540 File Offset: 0x000CC740
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string SecondaryPassword
		{
			get
			{
				return (string)base.Fields["SecondaryPassword"];
			}
			set
			{
				base.Fields["SecondaryPassword"] = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000CE553 File Offset: 0x000CC753
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x000CE56A File Offset: 0x000CC76A
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string SecondaryEndpoint
		{
			get
			{
				return (string)base.Fields["SecondaryEndpoint"];
			}
			set
			{
				base.Fields["SecondaryEndpoint"] = value;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000CE57D File Offset: 0x000CC77D
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000CE594 File Offset: 0x000CC794
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string TimeOutSeconds
		{
			get
			{
				return (string)base.Fields["TimeOutSeconds"];
			}
			set
			{
				base.Fields["TimeOutSeconds"] = value;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000CE5A7 File Offset: 0x000CC7A7
		// (set) Token: 0x06003286 RID: 12934 RVA: 0x000CE5BE File Offset: 0x000CC7BE
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string PropertyOverride
		{
			get
			{
				return (string)base.Fields["PropertyOverride"];
			}
			set
			{
				base.Fields["PropertyOverride"] = value;
			}
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000CE5D4 File Offset: 0x000CC7D4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (!MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(this.Identity))
				{
					base.WriteError(new ArgumentException(Strings.InvalidMonitorIdentity(this.Identity)), (ErrorCategory)1000, null);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000CE62C File Offset: 0x000CC82C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				RpcInvokeMonitoringProbe.Reply reply = null;
				LocalizedException ex = null;
				try
				{
					Dictionary<string, string> dictionary = this.CreatePropertyBag();
					string text = string.Empty;
					if (dictionary != null && dictionary.Count != 0)
					{
						text = CrimsonHelper.ConvertDictionaryToXml(dictionary);
					}
					this.ItemTargetExtension = (string.IsNullOrWhiteSpace(this.ItemTargetExtension) ? string.Empty : this.ItemTargetExtension);
					text = (string.IsNullOrWhiteSpace(text) ? string.Empty : text);
					reply = RpcInvokeMonitoringProbe.Invoke(this.Server.Fqdn, this.Identity, text, this.ItemTargetExtension, 300000);
				}
				catch (ActiveMonitoringServerException ex2)
				{
					ex = ex2;
				}
				catch (ActiveMonitoringServerTransientException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					this.WriteWarning(ex.LocalizedString);
				}
				else if (!string.IsNullOrEmpty(reply.ErrorMessage))
				{
					base.WriteWarning(reply.ErrorMessage);
				}
				else
				{
					MonitoringProbeResult sendToPipeline = new MonitoringProbeResult(this.Server.Fqdn, reply.ProbeResult);
					base.WriteObject(sendToPipeline);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000CE744 File Offset: 0x000CC944
		protected override bool IsKnownException(Exception exception)
		{
			return exception is InvalidVersionException || exception is InvalidIdentityException || exception is InvalidDurationException || base.IsKnownException(exception);
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000CE768 File Offset: 0x000CC968
		private Dictionary<string, string> CreatePropertyBag()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			InvokeMonitoringProbe.AddToPropertyBag(this.Account, "Account", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(this.Password, "Password", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(this.Endpoint, "Endpoint", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(this.SecondaryAccount, "SecondaryAccount", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(this.SecondaryPassword, "SecondaryPassword", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(this.SecondaryEndpoint, "SecondaryEndpoint", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(this.TimeOutSeconds, "TimeoutSeconds", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(MonitoringItemIdentity.MonitorIdentityId.GetHealthSet(this.Identity), "ServiceName", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(MonitoringItemIdentity.MonitorIdentityId.GetMonitor(this.Identity), "Name", dictionary);
			InvokeMonitoringProbe.AddToPropertyBag(MonitoringItemIdentity.MonitorIdentityId.GetTargetResource(this.Identity), "TargetResource", dictionary);
			if (!string.IsNullOrWhiteSpace(this.PropertyOverride))
			{
				this.ParseAndAddPropertyOverrides(dictionary);
			}
			return dictionary;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000CE84E File Offset: 0x000CCA4E
		private static void AddToPropertyBag(string value, string propName, Dictionary<string, string> propertyBag)
		{
			if (value != null && propertyBag != null)
			{
				propertyBag.Add(propName, value);
			}
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000CE860 File Offset: 0x000CCA60
		private void ParseAndAddPropertyOverrides(Dictionary<string, string> propertyBag)
		{
			string[] array = this.PropertyOverride.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				if (!text.Contains("="))
				{
					base.WriteError(new ArgumentException(Strings.InvalidPropertyOverrideValue(this.PropertyOverride)), (ErrorCategory)1000, null);
				}
				string[] array3 = text.Split(new char[]
				{
					'='
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array3.Length != 2 || string.IsNullOrWhiteSpace(array3[0]) || string.IsNullOrWhiteSpace(array3[1]))
				{
					base.WriteError(new ArgumentException(Strings.InvalidPropertyOverrideValue(this.PropertyOverride)), (ErrorCategory)1000, null);
				}
				array3[0].Trim();
				array3[1].Trim();
				propertyBag[array3[0]] = array3[1].Replace("'", "").Replace("\"", "");
			}
		}

		// Token: 0x0400235F RID: 9055
		private ServerIdParameter serverId;
	}
}
