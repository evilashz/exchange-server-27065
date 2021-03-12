using System;
using System.Globalization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000014 RID: 20
	internal class PoisonHandler<TPoisonHandlerContext> : ITransportComponent
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public PoisonHandler(string registrySuffix, TimeSpan poisonEntryExpiryWindow, int maxPoisonEntries)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("registrySuffix", registrySuffix);
			ArgumentValidator.ThrowIfZeroOrNegative("maxPoisonEntries", maxPoisonEntries);
			this.storeDriverPoisonInfo = new SynchronizedDictionary<string, CrashProperties>();
			this.storeDriverPoisonMsgLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\PoisonMessage\\StoreDriver\\" + registrySuffix;
			this.poisonEntryExpiryWindow = poisonEntryExpiryWindow;
			this.maxPoisonEntries = maxPoisonEntries;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003D1B File Offset: 0x00001F1B
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003D22 File Offset: 0x00001F22
		public static TPoisonHandlerContext Context
		{
			internal get
			{
				return PoisonHandler<TPoisonHandlerContext>.context;
			}
			set
			{
				PoisonMessage.Context = null;
				PoisonHandler<TPoisonHandlerContext>.context = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003D30 File Offset: 0x00001F30
		public virtual int PoisonThreshold
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.PoisonThreshold;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003D46 File Offset: 0x00001F46
		private bool Enabled
		{
			get
			{
				return this.loaded && Components.Configuration.LocalServer.TransportServer.PoisonMessageDetectionEnabled;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D68 File Offset: 0x00001F68
		public virtual void SavePoisonContext()
		{
			if (!this.Enabled)
			{
				return;
			}
			if (PoisonHandler<TPoisonHandlerContext>.Context == null)
			{
				TraceHelper.GeneralTracer.TracePass(TraceHelper.MessageProbeActivityId, 0L, "No poison context information stored on the crashing thread. Exiting...");
				return;
			}
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(this.storeDriverPoisonMsgLocation))
			{
				TPoisonHandlerContext tpoisonHandlerContext = PoisonHandler<TPoisonHandlerContext>.Context;
				string text = tpoisonHandlerContext.ToString();
				int num = 1;
				if (registryKey.GetValue(text) != null)
				{
					if (registryKey.GetValueKind(text) != RegistryValueKind.MultiString)
					{
						registryKey.DeleteValue(text, false);
					}
					else
					{
						string[] array = (string[])registryKey.GetValue(text);
						if (array == null || array.Length != 2)
						{
							registryKey.DeleteValue(text, false);
						}
						else if (!int.TryParse(array[0], out num))
						{
							registryKey.DeleteValue(text, false);
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					this.DeleteOldestPoisonEntryIfNecessary(registryKey);
				}
				CrashProperties crashProperties = new CrashProperties((double)num, DateTime.UtcNow);
				registryKey.SetValue(text, new string[]
				{
					Convert.ToString(crashProperties.CrashCount),
					crashProperties.LastCrashTime.ToString("u")
				}, RegistryValueKind.MultiString);
				this.storeDriverPoisonInfo[text] = crashProperties;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003E9C File Offset: 0x0000209C
		public bool VerifyPoisonMessage(string poisonId, out int crashCount)
		{
			if (string.IsNullOrEmpty(poisonId))
			{
				throw new ArgumentNullException("poisonId");
			}
			if (!this.Enabled || this.storeDriverPoisonInfo.Count == 0)
			{
				crashCount = 0;
				return false;
			}
			CrashProperties crashProperties = null;
			if (this.storeDriverPoisonInfo.TryGetValue(poisonId, out crashProperties))
			{
				if (this.IsExpired(crashProperties))
				{
					this.MarkPoisonMessageHandled(poisonId);
				}
				else if (this.IsMessagePoison(crashProperties))
				{
					crashCount = (int)crashProperties.CrashCount;
					return true;
				}
			}
			crashCount = 0;
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003F12 File Offset: 0x00002112
		public void MarkPoisonMessageHandledIfExists(string poisonId)
		{
			if (this.storeDriverPoisonInfo.ContainsKey(poisonId))
			{
				this.MarkPoisonMessageHandled(poisonId);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003F2C File Offset: 0x0000212C
		public virtual void MarkPoisonMessageHandled(string poisonId)
		{
			if (string.IsNullOrEmpty(poisonId))
			{
				throw new ArgumentNullException("poisonId");
			}
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(this.storeDriverPoisonMsgLocation, true))
				{
					if (registryKey != null)
					{
						registryKey.DeleteValue(poisonId, false);
					}
				}
			}
			finally
			{
				this.storeDriverPoisonInfo.Remove(poisonId);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003FA4 File Offset: 0x000021A4
		public virtual void Load()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(this.storeDriverPoisonMsgLocation))
			{
				foreach (string text in registryKey.GetValueNames())
				{
					bool flag = registryKey.GetValueKind(text) != RegistryValueKind.MultiString;
					string[] array = (string[])registryKey.GetValue(text);
					if (array.Length != 2)
					{
						flag = true;
					}
					int value;
					DateTime lastCrashTime;
					if (flag)
					{
						TraceHelper.GeneralTracer.TraceFail<string, RegistryKey>(TraceHelper.MessageProbeActivityId, 0L, "Invalid value {0} in {1} registry key. Deleting it.", text, registryKey);
						registryKey.DeleteValue(text, false);
					}
					else if (!int.TryParse(array[0], out value) || !DateTime.TryParseExact(array[1], "u", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.RoundtripKind, out lastCrashTime))
					{
						TraceHelper.GeneralTracer.TraceFail<string, RegistryKey>(TraceHelper.MessageProbeActivityId, 0L, "Invalid value {0} in {1} registry key. Deleting it.", text, registryKey);
						registryKey.DeleteValue(text, false);
					}
					else
					{
						CrashProperties crashProperties = new CrashProperties(Convert.ToDouble(value), lastCrashTime);
						if (!this.IsExpired(crashProperties))
						{
							this.storeDriverPoisonInfo[text] = crashProperties;
						}
						else
						{
							registryKey.DeleteValue(text, false);
						}
					}
				}
			}
			this.loaded = true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000040D0 File Offset: 0x000022D0
		public void Unload()
		{
			this.storeDriverPoisonInfo.Clear();
			this.loaded = false;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000040E4 File Offset: 0x000022E4
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000040E7 File Offset: 0x000022E7
		protected virtual bool IsMessagePoison(CrashProperties crashProperties)
		{
			return crashProperties.CrashCount >= (double)this.PoisonThreshold;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000040FC File Offset: 0x000022FC
		protected virtual bool IsExpired(CrashProperties crashProperties)
		{
			if (crashProperties == null)
			{
				return false;
			}
			try
			{
				if (DateTime.UtcNow > crashProperties.LastCrashTime + this.poisonEntryExpiryWindow)
				{
					return true;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000414C File Offset: 0x0000234C
		private void DeleteOldestPoisonEntryIfNecessary(RegistryKey poisonRegistryEntryKey)
		{
			if (poisonRegistryEntryKey.ValueCount < this.maxPoisonEntries)
			{
				return;
			}
			string oldestPoisonEntryValueName = StoreDriverUtils.GetOldestPoisonEntryValueName(this.storeDriverPoisonInfo);
			if (!string.IsNullOrEmpty(oldestPoisonEntryValueName))
			{
				this.storeDriverPoisonInfo.Remove(oldestPoisonEntryValueName);
				poisonRegistryEntryKey.DeleteValue(oldestPoisonEntryValueName, false);
			}
		}

		// Token: 0x04000034 RID: 52
		protected const string LastCrashTimeFormat = "u";

		// Token: 0x04000035 RID: 53
		private const string PoisonMsgLocationBase = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport\\PoisonMessage\\StoreDriver\\";

		// Token: 0x04000036 RID: 54
		[ThreadStatic]
		private static TPoisonHandlerContext context;

		// Token: 0x04000037 RID: 55
		private readonly string storeDriverPoisonMsgLocation;

		// Token: 0x04000038 RID: 56
		private readonly TimeSpan poisonEntryExpiryWindow;

		// Token: 0x04000039 RID: 57
		private readonly int maxPoisonEntries;

		// Token: 0x0400003A RID: 58
		private bool loaded;

		// Token: 0x0400003B RID: 59
		private SynchronizedDictionary<string, CrashProperties> storeDriverPoisonInfo;
	}
}
