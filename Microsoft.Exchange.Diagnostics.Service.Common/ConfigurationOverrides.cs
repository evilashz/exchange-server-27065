using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000007 RID: 7
	public class ConfigurationOverrides : IDisposable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public ConfigurationOverrides()
		{
			double configDouble = Configuration.GetConfigDouble("ConfigurationOverridesRefreshTimerInterval", 0.0, double.MaxValue, 3600000.0);
			this.refreshTimer = new Timer(configDouble);
			this.refreshTimer.Elapsed += this.RefreshEvent;
			this.refreshTimer.Start();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00003D60 File Offset: 0x00001F60
		public static bool Equals(Dictionary<string, string> left, Dictionary<string, string> right)
		{
			if (object.ReferenceEquals(left, right))
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			if (left.Count != right.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, string> keyValuePair in left)
			{
				string a;
				if (!right.TryGetValue(keyValuePair.Key, out a) || a != keyValuePair.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.refreshTimer != null)
			{
				this.refreshTimer.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003E20 File Offset: 0x00002020
		public void Refresh()
		{
			Logger.LogInformationMessage("Refreshing configuration overrides.", new object[0]);
			Dictionary<string, string> dictionary = this.Read();
			if (ConfigurationOverrides.Equals(dictionary, Configuration.Overrides))
			{
				Logger.LogInformationMessage("Overrides unchanged.", new object[0]);
				return;
			}
			Logger.LogInformationMessage("Overrides changed.", new object[0]);
			Configuration.Overrides = dictionary;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00003E78 File Offset: 0x00002078
		protected virtual Dictionary<string, string> Read()
		{
			return new Dictionary<string, string>();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00003E7F File Offset: 0x0000207F
		private void RefreshEvent(object sender, ElapsedEventArgs e)
		{
			this.Refresh();
		}

		// Token: 0x040002BC RID: 700
		private readonly Timer refreshTimer;

		// Token: 0x040002BD RID: 701
		private bool disposed;
	}
}
