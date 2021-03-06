using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Provisioning.Probes
{
	// Token: 0x020002AA RID: 682
	public sealed class ForwardSyncCookieProbe : ProbeWorkItem
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x00086EF0 File Offset: 0x000850F0
		public static ProbeDefinition CreateDefinition(string name, string targetResource, int recurrenceInterval)
		{
			return new ProbeDefinition
			{
				AssemblyPath = ForwardSyncCookieProbe.AssemblyPath,
				TypeName = ForwardSyncCookieProbe.TypeName,
				Name = name,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = recurrenceInterval / 2,
				TargetResource = targetResource
			};
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00086F38 File Offset: 0x00085138
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: DoWork(): starting", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 128);
			string serviceInstancename = ForwardSyncEventlogUtil.GetServiceInstancename();
			if (string.IsNullOrEmpty(serviceInstancename))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: DoWork(): This server is Passive", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 138);
				return;
			}
			int freshCookieThresholdMinutes = 60;
			if (!int.TryParse(base.Definition.TargetExtension, out freshCookieThresholdMinutes))
			{
				freshCookieThresholdMinutes = 60;
			}
			if (ExEnvironment.IsTest)
			{
				freshCookieThresholdMinutes = 1;
			}
			int maxCookieHistoryCount = 5;
			TimeSpan cookieHistoryInterval = new TimeSpan(1, 0, 0);
			DateTime utcNow = DateTime.UtcNow;
			try
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: DoWork(): Get ForwardSync arbitration events", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 166);
				this.enableSTXlogging = Convert.ToBoolean(base.Definition.ExtensionAttributes);
				this.companyCookieTimeStamp = null;
				this.hostName = Dns.GetHostName();
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: DoWork(): This server is Active for service instance {0}", serviceInstancename, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 176);
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: DoWork(): Get company cookie manager for service instance: {0}", serviceInstancename, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 182);
				CookieManager cookieManager = CookieManagerFactory.Default.GetCookieManager(ForwardSyncCookieType.CompanyIncremental, serviceInstancename, maxCookieHistoryCount, cookieHistoryInterval);
				try
				{
					this.CheckForwardSyncCookieTimestamp(cookieManager as DeltaSyncCookieManager, "CompanyCookie", freshCookieThresholdMinutes);
				}
				catch (Exception ex)
				{
					if (this.enableSTXlogging)
					{
						StxLoggerBase.GetLoggerInstance(StxLogType.TestForwardSyncCookie).BeginAppend(this.hostName, false, new TimeSpan(0L), 1, ex.Message, null, null, this.companyCookieTimeStamp.ToString(), this.companyCookieLatency.ToString(), new List<string>
						{
							serviceInstancename,
							this.companyCookieStructureType,
							this.companyCookieSize.ToString(),
							string.Empty,
							string.Empty
						});
					}
					throw;
				}
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: DoWork(): Get recipient cookie manager for service instance: {0}", serviceInstancename, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 225);
				CookieManager cookieManager2 = CookieManagerFactory.Default.GetCookieManager(ForwardSyncCookieType.RecipientIncremental, serviceInstancename, maxCookieHistoryCount, cookieHistoryInterval);
				try
				{
					this.CheckForwardSyncCookieTimestamp(cookieManager2 as DeltaSyncCookieManager, "RecipientCookie", freshCookieThresholdMinutes);
				}
				catch (Exception ex2)
				{
					if (this.enableSTXlogging)
					{
						StxLoggerBase.GetLoggerInstance(StxLogType.TestForwardSyncCookie).BeginAppend(this.hostName, false, new TimeSpan(0L), 1, ex2.Message, this.recipientCookieTimeStamp.ToString(), this.recipientCookieLatency.ToString(), this.companyCookieTimeStamp.ToString(), this.companyCookieLatency.ToString(), new List<string>
						{
							serviceInstancename,
							this.companyCookieStructureType,
							this.companyCookieSize.ToString(),
							this.recipientCookieStructureType,
							this.recipientCookieSize.ToString()
						});
					}
					throw;
				}
				if (this.enableSTXlogging)
				{
					StxLoggerBase.GetLoggerInstance(StxLogType.TestForwardSyncCookie).BeginAppend(this.hostName, true, new TimeSpan(0L), 0, null, this.recipientCookieTimeStamp.ToString(), this.recipientCookieLatency.ToString(), this.companyCookieTimeStamp.ToString(), this.companyCookieLatency.ToString(), new List<string>
					{
						serviceInstancename,
						this.companyCookieStructureType,
						this.companyCookieSize.ToString(),
						this.recipientCookieStructureType,
						this.recipientCookieSize.ToString()
					});
				}
			}
			catch (Exception ex3)
			{
				if (this.enableSTXlogging && !ex3.Message.Equals(this.errorMessage))
				{
					StxLoggerBase.GetLoggerInstance(StxLogType.TestForwardSyncCookie).BeginAppend(this.hostName, false, new TimeSpan(0L), 1, ex3.Message, serviceInstancename, null, null, null);
				}
				throw;
			}
			finally
			{
				base.Result.SampleValue = (double)((int)(DateTime.UtcNow - utcNow).TotalMilliseconds);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: DoWork(): ending", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 315);
			}
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000873F8 File Offset: 0x000855F8
		private void CheckForwardSyncCookieTimestamp(DeltaSyncCookieManager cookieManager, string cookieType, int freshCookieThresholdMinutes)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: CheckForwardSyncCookieTimestamp(): starting", null, "CheckForwardSyncCookieTimestamp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 330);
			string serviceInstanceName = cookieManager.ServiceInstanceName;
			DateTime? dateTime = null;
			string text = string.Empty;
			if (cookieManager is MsoMultiObjectCookieManager)
			{
				text = "MsoMultiObject";
				MsoMultiObjectCookieManager msoMultiObjectCookieManager = cookieManager as MsoMultiObjectCookieManager;
				ForwardSyncCookie forwardSyncCookie = msoMultiObjectCookieManager.ReadMostRecentCookie();
				if (forwardSyncCookie != null)
				{
					dateTime = this.GetTimestampFromMetaData(forwardSyncCookie.DistinguishedName, forwardSyncCookie.OriginatingServer);
				}
			}
			else if (cookieManager is MsoMainStreamCookieManager)
			{
				text = "MsoMultiValue";
			}
			else
			{
				text = "Default";
			}
			int num = (cookieManager.ReadCookie() == null) ? -1 : cookieManager.ReadCookie().Length;
			dateTime = ((dateTime == null || dateTime == null) ? cookieManager.GetMostRecentCookieTimestamp() : dateTime);
			TimeSpan timeSpan = DateTime.UtcNow - ((dateTime != null) ? dateTime.Value : DateTime.MinValue);
			if (cookieType.Equals("CompanyCookie"))
			{
				this.companyCookieTimeStamp = new DateTime?((dateTime != null) ? dateTime.Value : DateTime.MinValue);
				this.companyCookieLatency = timeSpan;
				this.companyCookieStructureType = text;
				this.companyCookieSize = num;
			}
			else if (cookieType.Equals("RecipientCookie"))
			{
				this.recipientCookieTimeStamp = new DateTime?((dateTime != null) ? dateTime.Value : DateTime.MinValue);
				this.recipientCookieLatency = timeSpan;
				this.recipientCookieStructureType = text;
				this.recipientCookieSize = num;
			}
			WTFDiagnostics.TraceInformation<string, string, double, int>(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: CheckForwardSyncCookieTimestamp(): CookieType: {0}, ServiceInstance: {1}, TimeSpanMins: {2}, ThresholdMins: {3}", cookieType, serviceInstanceName, timeSpan.TotalMinutes, freshCookieThresholdMinutes, null, "CheckForwardSyncCookieTimestamp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 381);
			if (timeSpan.TotalMinutes > (double)freshCookieThresholdMinutes)
			{
				this.errorMessage = string.Format("{0} for {1} with timestamp {2} is {3} minutes old while threshold is {4}", new object[]
				{
					cookieType,
					serviceInstanceName,
					dateTime,
					timeSpan.TotalMinutes,
					freshCookieThresholdMinutes
				});
				throw new Exception(this.errorMessage);
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, base.TraceContext, "ForwardSyncCookieProbe:: CheckForwardSyncCookieTimestamp(): ending", null, "CheckForwardSyncCookieTimestamp", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Provisioning\\ForwardSyncCookieProbe.cs", 403);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00087624 File Offset: 0x00085824
		private DateTime? GetTimestampFromMetaData(string dn, string originatingServer)
		{
			DateTime? result = null;
			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters = new Dictionary<string, string>
			{
				{
					"Identity",
					dn
				},
				{
					"Server",
					originatingServer
				},
				{
					"Properties",
					"msDS-ReplAttributeMetaData"
				}
			};
			try
			{
				using (LocalPowerShellProvider localPowerShellProvider = new LocalPowerShellProvider())
				{
					Collection<PSObject> collection = localPowerShellProvider.RunExchangeCmdlet<string>("Get-ADObject", parameters, base.TraceContext, true);
					MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>(collection[0].Properties["msDS-ReplAttributeMetaData"].Value);
					this.AppendToLog(true, 0, "ForwardCookieProbe. Get Metadata", "GetADObject");
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text in multiValuedProperty)
					{
						if (text.Contains("msExchMSOForwardSyncCookieTimestamp"))
						{
							stringBuilder.Append(text);
							this.AppendToLog(true, 0, text, "GetMeta msExchMSOForwardSyncCookieTimestamp");
							break;
						}
						if (text.Contains("msExchSyncCookie"))
						{
							stringBuilder.Append(text);
							this.AppendToLog(true, 0, text, "GetMeta msExchSyncCookie");
							break;
						}
					}
					string text2 = stringBuilder.ToString();
					if (string.IsNullOrEmpty(text2))
					{
						base.Result.StateAttribute1 = "Didn't get replication meta data. return";
						this.AppendToLog(true, 0, base.Result.StateAttribute1, "GetEmpty");
						return result;
					}
					XmlDocument xmlDocument = new XmlDocument();
					text2 = "<root>" + text2 + "</root>";
					text2 = text2.Replace('\0', ' ');
					xmlDocument.LoadXml(text2);
					XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("ftimeLastOriginatingChange");
					if (!string.IsNullOrEmpty(elementsByTagName[0].InnerText))
					{
						result = new DateTime?(DateTime.Parse(elementsByTagName[0].InnerText).ToUniversalTime());
					}
				}
			}
			catch (Exception ex)
			{
				this.AppendToLog(false, 1, "GetCookie time from metadata failed with " + ex.Message, "LogError");
				result = null;
			}
			return result;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00087884 File Offset: 0x00085A84
		private void AppendToLog(bool isProbeSucceed, int statusCode, string message, string action)
		{
			string target = Dns.GetHostName();
			StxLoggerBase.GetLoggerInstance(StxLogType.TestForwardSyncCookie).BeginAppend(target, isProbeSucceed, new TimeSpan(0L), statusCode, message, null, "escalate", action, null);
		}

		// Token: 0x04000E90 RID: 3728
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000E91 RID: 3729
		private static readonly string TypeName = typeof(ForwardSyncCookieProbe).FullName;

		// Token: 0x04000E92 RID: 3730
		private bool enableSTXlogging;

		// Token: 0x04000E93 RID: 3731
		private DateTime? companyCookieTimeStamp;

		// Token: 0x04000E94 RID: 3732
		private TimeSpan companyCookieLatency;

		// Token: 0x04000E95 RID: 3733
		private DateTime? recipientCookieTimeStamp;

		// Token: 0x04000E96 RID: 3734
		private TimeSpan recipientCookieLatency;

		// Token: 0x04000E97 RID: 3735
		private string hostName;

		// Token: 0x04000E98 RID: 3736
		private string errorMessage;

		// Token: 0x04000E99 RID: 3737
		private string companyCookieStructureType;

		// Token: 0x04000E9A RID: 3738
		private string recipientCookieStructureType;

		// Token: 0x04000E9B RID: 3739
		private int companyCookieSize;

		// Token: 0x04000E9C RID: 3740
		private int recipientCookieSize;
	}
}
