using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x0200020A RID: 522
	internal class PerformanceCounterRestartResponderCheckers : RestartResponderChecker
	{
		// Token: 0x06000EC2 RID: 3778 RVA: 0x00061AF8 File Offset: 0x0005FCF8
		internal PerformanceCounterRestartResponderCheckers(ResponderDefinition definition) : base(definition)
		{
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00061B01 File Offset: 0x0005FD01
		internal override string SkipReasonOrException
		{
			get
			{
				return this.skipReasonOrException;
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00061B0C File Offset: 0x0005FD0C
		protected override bool IsWithinThreshold()
		{
			this.skipReasonOrException = null;
			try
			{
				if (this.performanceCounterRestartResponderCheckers != null)
				{
					foreach (PerformanceCounterRestartResponderChecker performanceCounterRestartResponderChecker in this.performanceCounterRestartResponderCheckers)
					{
						if (performanceCounterRestartResponderChecker != null && !performanceCounterRestartResponderChecker.IsRestartAllowed)
						{
							this.skipReasonOrException += performanceCounterRestartResponderChecker.SkipReasonOrException;
							return false;
						}
						if (performanceCounterRestartResponderChecker != null)
						{
							this.skipReasonOrException += performanceCounterRestartResponderChecker.SkipReasonOrException;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.skipReasonOrException = ex.ToString();
			}
			return true;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00061BAC File Offset: 0x0005FDAC
		internal override string KeyOfEnabled
		{
			get
			{
				return "MoMTPerformanceCounterSettingEnabled";
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00061BB3 File Offset: 0x0005FDB3
		internal override string KeyOfSetting
		{
			get
			{
				return "MoMTPerformanceCounterSettings";
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x00061BBC File Offset: 0x0005FDBC
		internal override string DefaultSetting
		{
			get
			{
				if (PerformanceCounterRestartResponderCheckers.defaultSettings == null)
				{
					using (Stream manifestResourceStream = typeof(PerformanceCounterCheckSetting).Assembly.GetManifestResourceStream("MapiMTPerformanceCounterResponderCheckConfig.xml"))
					{
						if (manifestResourceStream != null)
						{
							using (StreamReader streamReader = new StreamReader(manifestResourceStream))
							{
								PerformanceCounterRestartResponderCheckers.defaultSettings = streamReader.ReadToEnd();
							}
						}
					}
				}
				return PerformanceCounterRestartResponderCheckers.defaultSettings;
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00061C38 File Offset: 0x0005FE38
		protected override bool OnSettingChange(string newSetting)
		{
			try
			{
				this.performanceCounterCheckSettings = null;
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(PerformanceCounterCheckSetting[]));
				using (TextReader textReader = new StringReader(newSetting))
				{
					this.performanceCounterCheckSettings = (PerformanceCounterCheckSetting[])xmlSerializer.Deserialize(textReader);
					if (this.performanceCounterCheckSettings != null && this.performanceCounterCheckSettings.Length > 0)
					{
						this.performanceCounterRestartResponderCheckers = new PerformanceCounterRestartResponderChecker[this.performanceCounterCheckSettings.Length];
						int num = 0;
						foreach (PerformanceCounterCheckSetting performanceCounterCheckSetting in this.performanceCounterCheckSettings)
						{
							this.performanceCounterRestartResponderCheckers[num++] = new PerformanceCounterRestartResponderChecker(null, performanceCounterCheckSetting.CategoryName, performanceCounterCheckSetting.CounterName, performanceCounterCheckSetting.InstanceName, performanceCounterCheckSetting.MinThreshold, performanceCounterCheckSetting.MaxThreshold, performanceCounterCheckSetting.ReasonToSkip);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.skipReasonOrException = ex.ToString();
				return false;
			}
			return true;
		}

		// Token: 0x04000AF6 RID: 2806
		private const string MoMTPerformanceCounterSettingEnabled = "MoMTPerformanceCounterSettingEnabled";

		// Token: 0x04000AF7 RID: 2807
		private const string MapiMTPerformanceCounterResponderCheckConfig = "MapiMTPerformanceCounterResponderCheckConfig.xml";

		// Token: 0x04000AF8 RID: 2808
		private const string MoMTPerformanceCounterSettings = "MoMTPerformanceCounterSettings";

		// Token: 0x04000AF9 RID: 2809
		private string skipReasonOrException;

		// Token: 0x04000AFA RID: 2810
		private PerformanceCounterCheckSetting[] performanceCounterCheckSettings;

		// Token: 0x04000AFB RID: 2811
		private PerformanceCounterRestartResponderChecker[] performanceCounterRestartResponderCheckers;

		// Token: 0x04000AFC RID: 2812
		private static string defaultSettings;
	}
}
