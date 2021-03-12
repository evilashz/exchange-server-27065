using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000109 RID: 265
	internal sealed class ApnsChannelCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x00019EB8 File Offset: 0x000180B8
		internal ApnsChannelCountersInstance(string instanceName, ApnsChannelCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Push Notifications Apns Channel")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ApnsConnectionFailed = new ExPerformanceCounter(base.CategoryName, "Apns Connection Failed - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ApnsConnectionFailed);
				this.ApnsReadTaskEnded = new ExPerformanceCounter(base.CategoryName, "Apns Read Task Ended - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ApnsReadTaskEnded);
				this.ApnsChannelReset = new ExPerformanceCounter(base.CategoryName, "Apns Reset - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ApnsChannelReset);
				this.AverageApnsConnectionTime = new ExPerformanceCounter(base.CategoryName, "Apns Connection - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsConnectionTime);
				this.AverageApnsConnectionTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Connection - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsConnectionTimeBase);
				this.AverageApnsAuthTime = new ExPerformanceCounter(base.CategoryName, "Apns Auth - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsAuthTime);
				this.AverageApnsAuthTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Auth - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsAuthTimeBase);
				this.AverageApnsChannelSendTime = new ExPerformanceCounter(base.CategoryName, "Apns Channel Send - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelSendTime);
				this.AverageApnsChannelSendTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Channel Send - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelSendTimeBase);
				this.AverageApnsResponseReadTime = new ExPerformanceCounter(base.CategoryName, "Apns Response Read - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsResponseReadTime);
				this.AverageApnsResponseReadTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Response Read - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsResponseReadTimeBase);
				this.AverageApnsChannelOpenTime = new ExPerformanceCounter(base.CategoryName, "Apns Channel Connection Open - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelOpenTime);
				this.AverageApnsChannelOpenTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Channel Connection Open - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelOpenTimeBase);
				long num = this.ApnsConnectionFailed.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001A194 File Offset: 0x00018394
		internal ApnsChannelCountersInstance(string instanceName) : base(instanceName, "MSExchange Push Notifications Apns Channel")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.ApnsConnectionFailed = new ExPerformanceCounter(base.CategoryName, "Apns Connection Failed - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ApnsConnectionFailed);
				this.ApnsReadTaskEnded = new ExPerformanceCounter(base.CategoryName, "Apns Read Task Ended - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ApnsReadTaskEnded);
				this.ApnsChannelReset = new ExPerformanceCounter(base.CategoryName, "Apns Reset - Counter", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ApnsChannelReset);
				this.AverageApnsConnectionTime = new ExPerformanceCounter(base.CategoryName, "Apns Connection - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsConnectionTime);
				this.AverageApnsConnectionTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Connection - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsConnectionTimeBase);
				this.AverageApnsAuthTime = new ExPerformanceCounter(base.CategoryName, "Apns Auth - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsAuthTime);
				this.AverageApnsAuthTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Auth - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsAuthTimeBase);
				this.AverageApnsChannelSendTime = new ExPerformanceCounter(base.CategoryName, "Apns Channel Send - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelSendTime);
				this.AverageApnsChannelSendTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Channel Send - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelSendTimeBase);
				this.AverageApnsResponseReadTime = new ExPerformanceCounter(base.CategoryName, "Apns Response Read - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsResponseReadTime);
				this.AverageApnsResponseReadTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Response Read - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsResponseReadTimeBase);
				this.AverageApnsChannelOpenTime = new ExPerformanceCounter(base.CategoryName, "Apns Channel Connection Open - Average Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelOpenTime);
				this.AverageApnsChannelOpenTimeBase = new ExPerformanceCounter(base.CategoryName, "Apns Channel Connection Open - Average Time Base", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageApnsChannelOpenTimeBase);
				long num = this.ApnsConnectionFailed.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001A470 File Offset: 0x00018670
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x040004C5 RID: 1221
		public readonly ExPerformanceCounter ApnsConnectionFailed;

		// Token: 0x040004C6 RID: 1222
		public readonly ExPerformanceCounter ApnsReadTaskEnded;

		// Token: 0x040004C7 RID: 1223
		public readonly ExPerformanceCounter ApnsChannelReset;

		// Token: 0x040004C8 RID: 1224
		public readonly ExPerformanceCounter AverageApnsConnectionTime;

		// Token: 0x040004C9 RID: 1225
		public readonly ExPerformanceCounter AverageApnsConnectionTimeBase;

		// Token: 0x040004CA RID: 1226
		public readonly ExPerformanceCounter AverageApnsAuthTime;

		// Token: 0x040004CB RID: 1227
		public readonly ExPerformanceCounter AverageApnsAuthTimeBase;

		// Token: 0x040004CC RID: 1228
		public readonly ExPerformanceCounter AverageApnsChannelSendTime;

		// Token: 0x040004CD RID: 1229
		public readonly ExPerformanceCounter AverageApnsChannelSendTimeBase;

		// Token: 0x040004CE RID: 1230
		public readonly ExPerformanceCounter AverageApnsResponseReadTime;

		// Token: 0x040004CF RID: 1231
		public readonly ExPerformanceCounter AverageApnsResponseReadTimeBase;

		// Token: 0x040004D0 RID: 1232
		public readonly ExPerformanceCounter AverageApnsChannelOpenTime;

		// Token: 0x040004D1 RID: 1233
		public readonly ExPerformanceCounter AverageApnsChannelOpenTimeBase;
	}
}
