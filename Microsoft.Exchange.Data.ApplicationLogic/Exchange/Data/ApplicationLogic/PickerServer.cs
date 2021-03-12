using System;
using System.Net;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020001A8 RID: 424
	internal class PickerServer : IDisposable
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x00042340 File Offset: 0x00040540
		internal PickerServer(Server server, PickerServerList pickerServerList)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			if (pickerServerList == null)
			{
				throw new ArgumentNullException("pickerServerList");
			}
			this.stateLock = new object();
			this.machineName = server.Name;
			this.serverGuid = server.Guid;
			this.exchangeLegacyDN = server.ExchangeLegacyDN;
			this.fqdn = server.Fqdn;
			this.versionNumber = server.VersionNumber;
			this.serverRole = server.CurrentServerRole;
			this.messageTrackingLogSubjectLoggingEnabled = server.MessageTrackingLogSubjectLoggingEnabled;
			this.cachedHashCode = (StringComparer.OrdinalIgnoreCase.GetHashCode(this.LegacyDN ?? string.Empty) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(this.FQDN ?? string.Empty) ^ StringComparer.OrdinalIgnoreCase.GetHashCode(this.MachineName ?? string.Empty));
			this.pickerServerList = pickerServerList;
			this.active = true;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0004242F File Offset: 0x0004062F
		public string LegacyDN
		{
			get
			{
				return this.exchangeLegacyDN;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00042437 File Offset: 0x00040637
		public string FQDN
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0004243F File Offset: 0x0004063F
		public int VersionNumber
		{
			get
			{
				return this.versionNumber;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00042447 File Offset: 0x00040647
		public string SystemAttendantDN
		{
			get
			{
				return this.LegacyDN + "/cn=Microsoft System Attendant";
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00042459 File Offset: 0x00040659
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00042461 File Offset: 0x00040661
		public ServerRole ServerRole
		{
			get
			{
				return this.serverRole;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00042469 File Offset: 0x00040669
		public Guid ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00042471 File Offset: 0x00040671
		public bool MessageTrackingLogSubjectLoggingEnabled
		{
			get
			{
				return this.messageTrackingLogSubjectLoggingEnabled;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00042479 File Offset: 0x00040679
		public bool IsActive
		{
			get
			{
				return this.active;
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00042481 File Offset: 0x00040681
		protected virtual void ServerDeactivated()
		{
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00042483 File Offset: 0x00040683
		public virtual void Dispose()
		{
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00042488 File Offset: 0x00040688
		internal bool IsEligibleForUse()
		{
			bool result;
			lock (this.stateLock)
			{
				if (this.active)
				{
					result = true;
				}
				else if (DateTime.UtcNow >= this.nextRetryTime)
				{
					this.pickerServerList.Tracer.TraceDebug<string>(0L, "Retry timeout expires. Server {0} will be retried again.", this.LegacyDN);
					this.nextRetryTime = DateTime.UtcNow.Add(PickerServer.RetryInterval);
					this.retryAttempts++;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0004252C File Offset: 0x0004072C
		public override bool Equals(object serverObj)
		{
			PickerServer pickerServer = serverObj as PickerServer;
			return pickerServer != null && (string.Equals(this.LegacyDN, pickerServer.LegacyDN, StringComparison.OrdinalIgnoreCase) && string.Equals(this.FQDN, pickerServer.FQDN, StringComparison.OrdinalIgnoreCase)) && string.Equals(this.MachineName, pickerServer.MachineName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00042584 File Offset: 0x00040784
		public virtual bool ArePropertiesEqual(Server server, ServerRole serverRole)
		{
			return string.Equals(this.machineName, server.Name) && string.Equals(this.exchangeLegacyDN, server.ExchangeLegacyDN) && string.Equals(this.fqdn, server.Fqdn) && this.versionNumber == server.VersionNumber && (this.serverRole & serverRole) == (server.CurrentServerRole & serverRole) && this.messageTrackingLogSubjectLoggingEnabled == server.MessageTrackingLogSubjectLoggingEnabled;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000425FA File Offset: 0x000407FA
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00042604 File Offset: 0x00040804
		public XElement GetDiagnosticInfo(string argument)
		{
			return new XElement("Server", new object[]
			{
				new XElement("active", this.active),
				new XElement("machineName", this.machineName),
				new XElement("version", this.versionNumber),
				new XElement("serverRole", this.serverRole),
				new XElement("retryAttempts", this.retryAttempts),
				new XElement("nextRetryTime", this.nextRetryTime)
			});
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000426D4 File Offset: 0x000408D4
		public override string ToString()
		{
			return string.Format("Name: {0}, Active: {1}, RetryAttempts: {2}, NextRetry: {3}", new object[]
			{
				this.MachineName,
				this.active,
				this.retryAttempts,
				this.nextRetryTime
			});
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00042728 File Offset: 0x00040928
		internal void InternalUpdateServerHealth(bool? isHealthy)
		{
			lock (this.stateLock)
			{
				if (isHealthy == null)
				{
					this.nextRetryTime = DateTime.MinValue;
				}
				else if (this.active)
				{
					if (!isHealthy.Value)
					{
						this.active = false;
						this.ServerDeactivated();
						this.pickerServerList.IncrementServersInRetryCount();
						this.nextRetryTime = DateTime.UtcNow.Add(PickerServer.RetryInterval);
						this.pickerServerList.Tracer.TraceDebug<string, DateTime>(0L, "Server {0} is no longer healthy, will try again at {1}.", this.LegacyDN, this.nextRetryTime);
					}
				}
				else if (isHealthy.Value)
				{
					this.active = true;
					this.retryAttempts = 0;
					this.pickerServerList.DecrementServersInRetryCount();
					this.pickerServerList.Tracer.TraceDebug<string>(0L, "Server {0} is healthy again.", this.LegacyDN);
				}
				else
				{
					this.nextRetryTime = DateTime.UtcNow.Add(PickerServer.RetryInterval);
					this.pickerServerList.Tracer.TraceDebug<string, DateTime>(0L, "Server {0} is still unhealthy, will try again at {1}.", this.LegacyDN, this.nextRetryTime);
				}
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0004286C File Offset: 0x00040A6C
		internal void CopyStatusTo(PickerServer newServer)
		{
			if (!this.active)
			{
				lock (this.stateLock)
				{
					newServer.active = this.active;
					newServer.nextRetryTime = this.nextRetryTime;
					newServer.retryAttempts = this.retryAttempts;
				}
			}
		}

		// Token: 0x040008A0 RID: 2208
		protected static readonly TimeSpan RetryInterval = new TimeSpan(0, 5, 0);

		// Token: 0x040008A1 RID: 2209
		protected static NetworkCredential localSystemCredential = new NetworkCredential(Environment.MachineName + "$", string.Empty, string.Empty);

		// Token: 0x040008A2 RID: 2210
		protected DateTime nextRetryTime;

		// Token: 0x040008A3 RID: 2211
		protected bool active;

		// Token: 0x040008A4 RID: 2212
		protected PickerServerList pickerServerList;

		// Token: 0x040008A5 RID: 2213
		private readonly string machineName;

		// Token: 0x040008A6 RID: 2214
		private readonly string exchangeLegacyDN;

		// Token: 0x040008A7 RID: 2215
		private readonly string fqdn;

		// Token: 0x040008A8 RID: 2216
		private readonly int versionNumber;

		// Token: 0x040008A9 RID: 2217
		private readonly ServerRole serverRole;

		// Token: 0x040008AA RID: 2218
		private readonly bool messageTrackingLogSubjectLoggingEnabled;

		// Token: 0x040008AB RID: 2219
		private readonly int cachedHashCode;

		// Token: 0x040008AC RID: 2220
		private readonly Guid serverGuid;

		// Token: 0x040008AD RID: 2221
		private int retryAttempts;

		// Token: 0x040008AE RID: 2222
		private object stateLock;
	}
}
