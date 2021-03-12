using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000012 RID: 18
	internal abstract class WinRMDataExchanger : IDisposable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004030 File Offset: 0x00002230
		public static string PipeName
		{
			get
			{
				if (WinRMDataExchanger.pipeName == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						WinRMDataExchanger.pipeName = "M.E.C.Core.WinRMDataCommunicator.NamedPipe." + currentProcess.Id;
					}
				}
				return WinRMDataExchanger.pipeName;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004088 File Offset: 0x00002288
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00004095 File Offset: 0x00002295
		public string Identity
		{
			get
			{
				return this[WinRMDataExchanger.ItemKeyIdentity];
			}
			protected set
			{
				this[WinRMDataExchanger.ItemKeyIdentity] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000040A3 File Offset: 0x000022A3
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000040B0 File Offset: 0x000022B0
		public string SessionId
		{
			get
			{
				return this[WinRMDataExchanger.ItemKeySessionId];
			}
			set
			{
				this[WinRMDataExchanger.ItemKeySessionId] = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000040BE File Offset: 0x000022BE
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000040CB File Offset: 0x000022CB
		public string RequestId
		{
			get
			{
				return this[WinRMDataExchanger.ItemKeyRequestId];
			}
			set
			{
				this[WinRMDataExchanger.ItemKeyRequestId] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000040DC File Offset: 0x000022DC
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00004100 File Offset: 0x00002300
		public UserToken UserToken
		{
			get
			{
				string text = this["X-EX-UserToken"];
				if (text == null)
				{
					return null;
				}
				return UserToken.Deserialize(text);
			}
			set
			{
				this["X-EX-UserToken"] = ((value == null) ? null : value.Serialize());
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004119 File Offset: 0x00002319
		protected Dictionary<string, string> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x1700001D RID: 29
		public string this[string key]
		{
			get
			{
				if (!this.items.ContainsKey(key))
				{
					throw new WinRMDataKeyNotFoundException(this.Identity, key);
				}
				return this.items[key];
			}
			set
			{
				this.items[key] = value;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004159 File Offset: 0x00002359
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004168 File Offset: 0x00002368
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04000043 RID: 67
		internal static readonly string ItemKeyIdentity = "Item-Identity";

		// Token: 0x04000044 RID: 68
		internal static readonly string ItemKeySessionId = "Item-SessionId";

		// Token: 0x04000045 RID: 69
		internal static readonly string ItemKeyRequestId = "Item-RequestId";

		// Token: 0x04000046 RID: 70
		private static string pipeName;

		// Token: 0x04000047 RID: 71
		private Dictionary<string, string> items = new Dictionary<string, string>();
	}
}
