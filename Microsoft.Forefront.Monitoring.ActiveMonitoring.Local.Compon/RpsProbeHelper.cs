using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Threading;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001FF RID: 511
	public sealed class RpsProbeHelper
	{
		// Token: 0x06000F8B RID: 3979 RVA: 0x00028C2C File Offset: 0x00026E2C
		public RpsProbeHelper(ProbeWorkItem probe, bool delegated)
		{
			if (probe == null)
			{
				string text = "The ProbeWorkItem is null.";
				this.TraceError(text);
				throw new ArgumentNullException(text);
			}
			this.probeWorkItem = probe;
			this.delegated = delegated;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00028C74 File Offset: 0x00026E74
		public void DoWork(CancellationToken cancellationToken)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.TraceDebug(string.Format("RemotePowershellProbe started: {0}", DateTime.UtcNow));
			try
			{
				this.GetExtensionAttributes();
				foreach (RpsProbeHelper.PSCmdlet pscmdlet in this.extensionAttributes.WorkContext.Cmdlets)
				{
					DateTime dateTime = DateTime.UtcNow;
					TimeSpan timeout = new TimeSpan(0L);
					if (!string.IsNullOrWhiteSpace(pscmdlet.RetryTimeInSeconds))
					{
						dateTime = dateTime.AddSeconds(Convert.ToDouble(pscmdlet.RetryTimeInSeconds));
					}
					if (!string.IsNullOrWhiteSpace(pscmdlet.SleepTimeInSeconds))
					{
						timeout = new TimeSpan(0, 0, Convert.ToInt32(pscmdlet.SleepTimeInSeconds));
					}
					IEnumerable<string> enumerable = new List<string>();
					do
					{
						cancellationToken.ThrowIfCancellationRequested();
						IEnumerable<PSObject> enumerable2 = this.ExecutePSCmdlet(pscmdlet, dateTime);
						if (enumerable2 != null)
						{
							enumerable = this.VerifyPSResult(enumerable2, pscmdlet);
							if (enumerable.Count<string>() == 0)
							{
								break;
							}
						}
						Thread.Sleep(timeout);
					}
					while (dateTime > DateTime.UtcNow);
					if (enumerable.Count<string>() != 0)
					{
						throw new ApplicationException(string.Format("PS cmdlet {0} failed - {1}", pscmdlet.Name, string.Join(",", enumerable)));
					}
					this.TraceDebug(string.Format("Remote PowerShell for cmdlet {0} passed.", pscmdlet.Name));
					if (pscmdlet.Logout != null && pscmdlet.Logout.Equals("true", StringComparison.InvariantCultureIgnoreCase) && string.IsNullOrWhiteSpace(pscmdlet.EndpointUri))
					{
						this.powerShellLookup[Tuple.Create<string, string>(pscmdlet.EndpointUri, pscmdlet.UserName)].Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				this.TraceError(ex.ToString());
				throw;
			}
			finally
			{
				this.CloseRemotePowerShell();
				stopwatch.Stop();
				this.TraceDebug(string.Format("RemotePowershellProbe finished in: {0}", stopwatch.Elapsed));
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00028E68 File Offset: 0x00027068
		private void CloseRemotePowerShell()
		{
			foreach (RemotePowershellWrapper remotePowershellWrapper in this.powerShellLookup.Values)
			{
				remotePowershellWrapper.Dispose();
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00028EC0 File Offset: 0x000270C0
		private IEnumerable<PSObject> ExecutePSCmdlet(RpsProbeHelper.PSCmdlet cmdlet, DateTime endTime)
		{
			IEnumerable<PSObject> enumerable = null;
			RemotePowershellWrapper remotePowershellWrapper = this.defaultRps;
			try
			{
				if (!string.IsNullOrWhiteSpace(cmdlet.EndpointUri))
				{
					remotePowershellWrapper = this.OpenRemotePowerShell(cmdlet.EndpointUri, cmdlet.UserName, cmdlet.Password, null);
				}
				enumerable = remotePowershellWrapper.Execute(this.CreatePSCommand(cmdlet));
			}
			catch (Exception ex)
			{
				this.TraceDebug(string.Format("Remote PowerShell for cmdlet {0} failed. {1}", cmdlet.Name, ex.ToString()));
				if (this.IsExpectedStatusPass(cmdlet))
				{
					if (endTime >= DateTime.UtcNow)
					{
						return null;
					}
					if (ex.InnerException != null)
					{
						throw ex.InnerException;
					}
					throw;
				}
			}
			if (this.IsExpectedStatusPass(cmdlet))
			{
				if (enumerable == null)
				{
					throw new ApplicationException(string.Format("Remote PowerShell returned no results for {0} user with cmdlet {1}.", this.userName, cmdlet.Name));
				}
			}
			else if (enumerable != null)
			{
				throw new ApplicationException(string.Format("Remote PowerShell returned results for {0} user with cmdlet {1} when it shoud have failed.", this.userName, cmdlet.Name));
			}
			return enumerable;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00028FB4 File Offset: 0x000271B4
		private RemotePowershellWrapper OpenRemotePowerShell(string endpointUri, string userName, string password, string delegatedTenant)
		{
			Tuple<string, string> key = Tuple.Create<string, string>(endpointUri, userName);
			if (!this.powerShellLookup.ContainsKey(key))
			{
				this.powerShellLookup.Add(key, new RemotePowershellWrapper(endpointUri, userName, this.ConvertStringToSecure(password), delegatedTenant));
			}
			return this.powerShellLookup[key];
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00028FFF File Offset: 0x000271FF
		private bool IsExpectedStatusPass(RpsProbeHelper.PSCmdlet cmdlet)
		{
			return cmdlet.Status == null || cmdlet.Status.Equals("pass", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0002901C File Offset: 0x0002721C
		private void GetExtensionAttributes()
		{
			this.extensionAttributes = this.LoadContextFromXml(this.probeWorkItem.Definition.ExtensionAttributes);
			if (this.delegated)
			{
				this.userName = this.VerifyXmlIsNotNullOrEmpty(RpsProbeHelper.DelegatedAdminName, this.extensionAttributes.WorkContext.DelegatedAdmin);
				this.delegatedTenant = this.VerifyXmlIsNotNullOrEmpty(RpsProbeHelper.DelegatedTenantName, this.extensionAttributes.WorkContext.DelegatedTenant);
			}
			else
			{
				this.userName = this.VerifyXmlIsNotNullOrEmpty(RpsProbeHelper.TenantAdminName, this.extensionAttributes.WorkContext.TenantAdmin);
				this.delegatedTenant = null;
			}
			this.password = this.VerifyXmlIsNotNullOrEmpty(RpsProbeHelper.PasswordName, this.extensionAttributes.WorkContext.Password);
			this.endPointUri = this.VerifyXmlIsNotNullOrEmpty(RpsProbeHelper.EndPointUriName, this.extensionAttributes.WorkContext.EndpointUri);
			this.defaultRps = this.OpenRemotePowerShell(this.endPointUri, this.userName, this.password, this.delegatedTenant);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00029120 File Offset: 0x00027320
		private RpsProbeHelper.ExtensionAttributes LoadContextFromXml(string xml)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(RpsProbeHelper.ExtensionAttributes));
			return (RpsProbeHelper.ExtensionAttributes)xmlSerializer.Deserialize(new StringReader(xml));
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00029150 File Offset: 0x00027350
		private IEnumerable<string> VerifyPSResult(IEnumerable<PSObject> results, RpsProbeHelper.PSCmdlet cmdlet)
		{
			List<string> list = new List<string>();
			if (cmdlet.ExpectedResults != null && results != null)
			{
				foreach (RpsProbeHelper.CmdletExpectedResult cmdletExpectedResult in cmdlet.ExpectedResults)
				{
					foreach (PSObject psobject in results)
					{
						if (psobject.Properties[cmdletExpectedResult.Property] != null && string.Compare(psobject.Properties[cmdletExpectedResult.Property].Value.ToString(), cmdletExpectedResult.Value, true) != 0)
						{
							list.Add(string.Format("The property '{0}' did  not have the expected value. Expected:{1}, Actual:{2} ", cmdletExpectedResult.Property, cmdletExpectedResult.Value, psobject.Properties[cmdletExpectedResult.Property].Value.ToString()));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00029248 File Offset: 0x00027448
		private SecureString ConvertStringToSecure(string password)
		{
			char[] array = password.ToCharArray();
			SecureString secureString = new SecureString();
			foreach (char c in array)
			{
				secureString.AppendChar(c);
			}
			return secureString;
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00029284 File Offset: 0x00027484
		private PSCommand CreatePSCommand(RpsProbeHelper.PSCmdlet cmdlet)
		{
			PSCommand pscommand = new PSCommand();
			pscommand.AddCommand(cmdlet.Name);
			if (cmdlet.Parameters != null)
			{
				foreach (RpsProbeHelper.CmdletParameter cmdletParameter in cmdlet.Parameters)
				{
					if (string.IsNullOrEmpty(cmdletParameter.Value))
					{
						pscommand.AddParameter(cmdletParameter.Name);
					}
					else
					{
						pscommand.AddParameter(cmdletParameter.Name, cmdletParameter.Value);
					}
				}
			}
			return pscommand;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000292F8 File Offset: 0x000274F8
		private string VerifyXmlIsNotNullOrEmpty(string name, string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				string text = string.Format("The {0} is not set in the ExtensionAttributes: {1}", name, this.probeWorkItem.Definition.ExtensionAttributes);
				this.TraceError(text);
				throw new ArgumentException(text);
			}
			return value;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00029338 File Offset: 0x00027538
		private void TraceDebug(string debugMsg)
		{
			ProbeResult result = this.probeWorkItem.Result;
			result.ExecutionContext = result.ExecutionContext + debugMsg + " ";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.RPSTracer, this.probeWorkItem.TraceContext, debugMsg, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\RPS\\RpsProbeHelper.cs", 442);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002938C File Offset: 0x0002758C
		private void TraceError(string errorMsg)
		{
			ProbeResult result = this.probeWorkItem.Result;
			result.ExecutionContext = result.ExecutionContext + errorMsg + " ";
			WTFDiagnostics.TraceError(ExTraceGlobals.RPSTracer, this.probeWorkItem.TraceContext, errorMsg, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\RPS\\RpsProbeHelper.cs", 452);
		}

		// Token: 0x0400076A RID: 1898
		internal static readonly string TenantAdminName = "TenantAdmin";

		// Token: 0x0400076B RID: 1899
		internal static readonly string PasswordName = "Password";

		// Token: 0x0400076C RID: 1900
		internal static readonly string DelegatedAdminName = "DelegatedAdmin";

		// Token: 0x0400076D RID: 1901
		internal static readonly string DelegatedTenantName = "DelegatedTenant";

		// Token: 0x0400076E RID: 1902
		internal static readonly string EndPointUriName = "EndpointUri";

		// Token: 0x0400076F RID: 1903
		private readonly bool delegated;

		// Token: 0x04000770 RID: 1904
		private readonly ProbeWorkItem probeWorkItem;

		// Token: 0x04000771 RID: 1905
		private string userName;

		// Token: 0x04000772 RID: 1906
		private string delegatedTenant;

		// Token: 0x04000773 RID: 1907
		private string endPointUri;

		// Token: 0x04000774 RID: 1908
		private RpsProbeHelper.ExtensionAttributes extensionAttributes;

		// Token: 0x04000775 RID: 1909
		private string password;

		// Token: 0x04000776 RID: 1910
		private Dictionary<Tuple<string, string>, RemotePowershellWrapper> powerShellLookup = new Dictionary<Tuple<string, string>, RemotePowershellWrapper>();

		// Token: 0x04000777 RID: 1911
		private RemotePowershellWrapper defaultRps;

		// Token: 0x02000200 RID: 512
		[XmlType(AnonymousType = true)]
		public class ExtensionAttributes
		{
			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00029414 File Offset: 0x00027614
			// (set) Token: 0x06000F9B RID: 3995 RVA: 0x0002941C File Offset: 0x0002761C
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public RpsProbeHelper.WorkContext WorkContext { get; set; }
		}

		// Token: 0x02000201 RID: 513
		[XmlType(AnonymousType = true)]
		public class WorkContext
		{
			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0002942D File Offset: 0x0002762D
			// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00029435 File Offset: 0x00027635
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string DelegatedTenant { get; set; }

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0002943E File Offset: 0x0002763E
			// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00029446 File Offset: 0x00027646
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string DelegatedAdmin { get; set; }

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0002944F File Offset: 0x0002764F
			// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00029457 File Offset: 0x00027657
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string TenantAdmin { get; set; }

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00029460 File Offset: 0x00027660
			// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00029468 File Offset: 0x00027668
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string Password { get; set; }

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00029471 File Offset: 0x00027671
			// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00029479 File Offset: 0x00027679
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string EndpointUri { get; set; }

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00029482 File Offset: 0x00027682
			// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x0002948A File Offset: 0x0002768A
			[XmlArrayItem("Cmdlet", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
			public RpsProbeHelper.PSCmdlet[] Cmdlets { get; set; }
		}

		// Token: 0x02000202 RID: 514
		[XmlType(AnonymousType = true)]
		public class PSCmdlet
		{
			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0002949B File Offset: 0x0002769B
			// (set) Token: 0x06000FAB RID: 4011 RVA: 0x000294A3 File Offset: 0x000276A3
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string Logout { get; set; }

			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000294AC File Offset: 0x000276AC
			// (set) Token: 0x06000FAD RID: 4013 RVA: 0x000294B4 File Offset: 0x000276B4
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string UserName { get; set; }

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x06000FAE RID: 4014 RVA: 0x000294BD File Offset: 0x000276BD
			// (set) Token: 0x06000FAF RID: 4015 RVA: 0x000294C5 File Offset: 0x000276C5
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string Password { get; set; }

			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000294CE File Offset: 0x000276CE
			// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x000294D6 File Offset: 0x000276D6
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string EndpointUri { get; set; }

			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000294DF File Offset: 0x000276DF
			// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x000294E7 File Offset: 0x000276E7
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string RetryTimeInSeconds { get; set; }

			// Token: 0x170004DA RID: 1242
			// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x000294F0 File Offset: 0x000276F0
			// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x000294F8 File Offset: 0x000276F8
			[XmlElement(Form = XmlSchemaForm.Unqualified)]
			public string SleepTimeInSeconds { get; set; }

			// Token: 0x170004DB RID: 1243
			// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00029501 File Offset: 0x00027701
			// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x00029509 File Offset: 0x00027709
			[XmlArrayItem("Parameter", Form = XmlSchemaForm.Unqualified, IsNullable = true)]
			public RpsProbeHelper.CmdletParameter[] Parameters { get; set; }

			// Token: 0x170004DC RID: 1244
			// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00029512 File Offset: 0x00027712
			// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x0002951A File Offset: 0x0002771A
			[XmlArrayItem("ExpectedResult", Form = XmlSchemaForm.Unqualified, IsNullable = true)]
			public RpsProbeHelper.CmdletExpectedResult[] ExpectedResults { get; set; }

			// Token: 0x170004DD RID: 1245
			// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00029523 File Offset: 0x00027723
			// (set) Token: 0x06000FBB RID: 4027 RVA: 0x0002952B File Offset: 0x0002772B
			[XmlAttribute]
			public string Name { get; set; }

			// Token: 0x170004DE RID: 1246
			// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00029534 File Offset: 0x00027734
			// (set) Token: 0x06000FBD RID: 4029 RVA: 0x0002953C File Offset: 0x0002773C
			[XmlAttribute]
			public string Status { get; set; }
		}

		// Token: 0x02000203 RID: 515
		[XmlType(AnonymousType = true)]
		public class CmdletParameter
		{
			// Token: 0x170004DF RID: 1247
			// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0002954D File Offset: 0x0002774D
			// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00029555 File Offset: 0x00027755
			[XmlAttribute]
			public string Name { get; set; }

			// Token: 0x170004E0 RID: 1248
			// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0002955E File Offset: 0x0002775E
			// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00029566 File Offset: 0x00027766
			[XmlAttribute]
			public string Value { get; set; }
		}

		// Token: 0x02000204 RID: 516
		[XmlType(AnonymousType = true)]
		public class CmdletExpectedResult
		{
			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00029577 File Offset: 0x00027777
			// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x0002957F File Offset: 0x0002777F
			[XmlAttribute]
			public string Property { get; set; }

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00029588 File Offset: 0x00027788
			// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x00029590 File Offset: 0x00027790
			[XmlAttribute]
			public string Value { get; set; }

			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00029599 File Offset: 0x00027799
			// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x000295A1 File Offset: 0x000277A1
			[XmlAttribute]
			public string Type { get; set; }
		}
	}
}
