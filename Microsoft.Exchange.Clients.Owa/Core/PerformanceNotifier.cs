using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200020F RID: 527
	internal sealed class PerformanceNotifier : IPendingRequestNotifier
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0006BA18 File Offset: 0x00069C18
		public bool ShouldThrottle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0006BA1C File Offset: 0x00069C1C
		internal void RegisterWithPendingRequestNotifier()
		{
			UserContext userContext = OwaContext.Current.UserContext;
			try
			{
				userContext.PendingRequestManager.AddPendingRequestNotifier(this);
			}
			catch (Exception)
			{
			}
			lock (this.list)
			{
				this.registered = true;
			}
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0006BA88 File Offset: 0x00069C88
		internal void UnregisterWithPendingRequestNotifier()
		{
			this.registered = false;
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060011CB RID: 4555 RVA: 0x0006BA94 File Offset: 0x00069C94
		// (remove) Token: 0x060011CC RID: 4556 RVA: 0x0006BACC File Offset: 0x00069CCC
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x060011CD RID: 4557 RVA: 0x0006BB04 File Offset: 0x00069D04
		public string ReadDataAndResetState()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			lock (this.list)
			{
				int count = this.list.Count;
				for (int i = 0; i < count; i++)
				{
					int serverId = this.GetServerId(i);
					OwaPerformanceData owaPerformanceData = this.list[i];
					if (!this.initialized[i])
					{
						owaPerformanceData.GenerateInitialPayload(stringBuilder, serverId);
						this.initialized[i] = true;
					}
					if (this.dirty[i])
					{
						string value;
						owaPerformanceData.RetrieveJSforPerfData(serverId, out value);
						stringBuilder.Append("excPrfCnsl(\"");
						stringBuilder.Append(value);
						stringBuilder.Append("\");");
						this.dirty[i] = false;
						if (this.finished[i])
						{
							owaPerformanceData.RetrieveFinishJS(serverId, out value);
							stringBuilder.Append("excPrfCnsl(\"");
							stringBuilder.Append(value);
							stringBuilder.Append("\");");
						}
					}
				}
				this.hasData = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0006BC24 File Offset: 0x00069E24
		private int GetServerId(int index)
		{
			if (this.lastInList % 40 > index)
			{
				return this.lastInList + 40 + index - this.lastInList % 40;
			}
			return this.lastInList + index - this.lastInList % 40;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0006BC5C File Offset: 0x00069E5C
		public void ReadDataAsHtml(TextWriter writer)
		{
			lock (this.list)
			{
				this.initialized = new bool[40];
				int count = this.list.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					int serverId = this.GetServerId(i);
					OwaPerformanceData owaPerformanceData = this.list[i];
					this.initialized[i] = true;
					string value;
					owaPerformanceData.RetrieveHtmlForPerfData(serverId, out value, this.finished[i], i + 1);
					writer.Write(value);
				}
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0006BCFC File Offset: 0x00069EFC
		public void ConnectionAliveTimer()
		{
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0006BD00 File Offset: 0x00069F00
		internal void UpdatePerformanceData(OwaPerformanceData performanceData, bool finishedRequest)
		{
			if (performanceData == null)
			{
				throw new ArgumentNullException("performanceData");
			}
			lock (this.list)
			{
				int num = this.list.IndexOf(performanceData);
				if (num < 0)
				{
					this.AddPerformanceData(performanceData, finishedRequest);
				}
				else
				{
					this.dirty[num] = true;
					if (finishedRequest)
					{
						this.finished[num] = true;
					}
					if (this.registered && !this.hasData)
					{
						this.hasData = true;
						this.DataAvailable(this, null);
					}
				}
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0006BDA0 File Offset: 0x00069FA0
		internal void FinishPerformanceData(OwaPerformanceData performanceData)
		{
			lock (this.list)
			{
				int num = this.list.IndexOf(performanceData);
				if (num >= 0)
				{
					this.finished[num] = true;
					if (this.registered && !this.hasData)
					{
						this.hasData = true;
						this.DataAvailable(this, null);
					}
				}
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0006BE1C File Offset: 0x0006A01C
		internal void AddPerformanceData(OwaPerformanceData performanceData)
		{
			this.AddPerformanceData(performanceData, false);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0006BE28 File Offset: 0x0006A028
		internal void AddPerformanceData(OwaPerformanceData performanceData, bool finishedRequest)
		{
			if (performanceData == null)
			{
				throw new ArgumentNullException("performanceData");
			}
			lock (this.list)
			{
				if (this.list.Count >= 40)
				{
					this.list[this.lastInList % 40] = performanceData;
					this.initialized[this.lastInList % 40] = false;
					this.dirty[this.lastInList % 40] = true;
					if (finishedRequest)
					{
						this.finished[this.lastInList % 40] = true;
					}
					else
					{
						this.finished[this.lastInList % 40] = false;
					}
					this.lastInList++;
				}
				else
				{
					this.list.Add(performanceData);
					this.dirty[this.list.Count - 1] = true;
					if (finishedRequest)
					{
						this.finished[this.list.Count - 1] = true;
					}
					else
					{
						this.finished[this.list.Count - 1] = false;
					}
				}
				if (this.registered && !this.hasData)
				{
					this.hasData = true;
					try
					{
						this.DataAvailable(this, null);
					}
					catch (OwaNotificationPipeWriteException)
					{
					}
				}
			}
		}

		// Token: 0x04000C0D RID: 3085
		private const int MaxSize = 40;

		// Token: 0x04000C0E RID: 3086
		private List<OwaPerformanceData> list = new List<OwaPerformanceData>();

		// Token: 0x04000C0F RID: 3087
		private bool hasData;

		// Token: 0x04000C10 RID: 3088
		private int lastInList;

		// Token: 0x04000C11 RID: 3089
		private bool[] initialized = new bool[40];

		// Token: 0x04000C12 RID: 3090
		private bool[] finished = new bool[40];

		// Token: 0x04000C13 RID: 3091
		private bool[] dirty = new bool[40];

		// Token: 0x04000C14 RID: 3092
		private volatile bool registered;
	}
}
