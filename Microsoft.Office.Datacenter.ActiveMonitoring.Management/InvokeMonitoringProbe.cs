using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management
{
	// Token: 0x02000008 RID: 8
	[OutputType(new Type[]
	{
		typeof(MonitorHealthEntry)
	})]
	[Cmdlet("Invoke", "MonitoringProbe")]
	public sealed class InvokeMonitoringProbe : PSCmdlet
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000266C File Offset: 0x0000086C
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002674 File Offset: 0x00000874
		[ValidateNotNullOrEmpty]
		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public string Identity { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000267D File Offset: 0x0000087D
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002685 File Offset: 0x00000885
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string Server { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000268E File Offset: 0x0000088E
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002696 File Offset: 0x00000896
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string ItemTargetExtension { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000269F File Offset: 0x0000089F
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000026A7 File Offset: 0x000008A7
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Account { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000026B8 File Offset: 0x000008B8
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Password { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026C1 File Offset: 0x000008C1
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026C9 File Offset: 0x000008C9
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Endpoint { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026D2 File Offset: 0x000008D2
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000026DA File Offset: 0x000008DA
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string SecondaryAccount { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026E3 File Offset: 0x000008E3
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000026EB File Offset: 0x000008EB
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string SecondaryPassword { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026F4 File Offset: 0x000008F4
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000026FC File Offset: 0x000008FC
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string SecondaryEndpoint { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002705 File Offset: 0x00000905
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000270D File Offset: 0x0000090D
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string TimeOutSeconds { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002716 File Offset: 0x00000916
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000271E File Offset: 0x0000091E
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string PropertyOverride { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00002727 File Offset: 0x00000927
		protected override void BeginProcessing()
		{
			if (!MonitoringItemIdentity.MonitorIdentityId.IsValidFormat(this.Identity))
			{
				base.WriteError(new ErrorRecord(new ArgumentException(Strings.InvalidMonitorIdentity(this.Identity)), string.Empty, ErrorCategory.InvalidArgument, null));
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002760 File Offset: 0x00000960
		protected override void EndProcessing()
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
				reply = RpcInvokeMonitoringProbe.Invoke(this.Server, this.Identity, text, this.ItemTargetExtension, 300000);
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
				base.WriteWarning(ex.LocalizedString);
				return;
			}
			if (!string.IsNullOrEmpty(reply.ErrorMessage))
			{
				base.WriteWarning(reply.ErrorMessage);
				return;
			}
			MonitoringProbeResult sendToPipeline = new MonitoringProbeResult(this.Server, reply.ProbeResult);
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002858 File Offset: 0x00000A58
		private static void AddToPropertyBag(string value, string propName, Dictionary<string, string> propertyBag)
		{
			if (value != null && propertyBag != null)
			{
				propertyBag.Add(propName, value);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002868 File Offset: 0x00000A68
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

		// Token: 0x0600003E RID: 62 RVA: 0x00002950 File Offset: 0x00000B50
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
					base.WriteError(new ErrorRecord(new ArgumentException(Strings.InvalidPropertyOverrideValue(this.PropertyOverride)), string.Empty, (ErrorCategory)1000, null));
				}
				string[] array3 = text.Split(new char[]
				{
					'='
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array3.Length != 2 || string.IsNullOrWhiteSpace(array3[0]) || string.IsNullOrWhiteSpace(array3[1]))
				{
					base.WriteError(new ErrorRecord(new ArgumentException(Strings.InvalidPropertyOverrideValue(this.PropertyOverride)), string.Empty, (ErrorCategory)1000, null));
				}
				array3[0].Trim();
				array3[1].Trim();
				propertyBag[array3[0]] = array3[1].Replace("'", string.Empty).Replace("\"", string.Empty);
			}
		}
	}
}
