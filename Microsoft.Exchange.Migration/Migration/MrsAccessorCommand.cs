using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000146 RID: 326
	internal class MrsAccessorCommand
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x00045522 File Offset: 0x00043722
		protected MrsAccessorCommand(string cmdletName, ICollection<Type> ignoredExceptions, ICollection<Type> transientExceptions)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(cmdletName, "name");
			this.arguments = new Dictionary<string, object>();
			this.cmdletName = cmdletName;
			this.IgnoreExceptions = ignoredExceptions;
			this.TransientExceptions = transientExceptions;
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00045560 File Offset: 0x00043760
		public PSCommand Command
		{
			get
			{
				PSCommand pscommand = new PSCommand().AddCommand(this.cmdletName);
				foreach (KeyValuePair<string, object> keyValuePair in this.arguments)
				{
					pscommand.AddParameter(keyValuePair.Key, keyValuePair.Value);
				}
				foreach (string parameterName in this.switchParameters)
				{
					pscommand.AddParameter(parameterName);
				}
				return pscommand;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00045618 File Offset: 0x00043818
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0004562C File Offset: 0x0004382C
		public object Identity
		{
			get
			{
				return this.arguments["Identity"];
			}
			set
			{
				object value2 = value;
				MRSSubscriptionId mrssubscriptionId = value as MRSSubscriptionId;
				if (mrssubscriptionId != null)
				{
					value2 = mrssubscriptionId.GetIdParameter();
				}
				this.AddParameter("Identity", value2);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00045658 File Offset: 0x00043858
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x00045660 File Offset: 0x00043860
		public ICollection<Type> IgnoreExceptions { get; protected set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00045669 File Offset: 0x00043869
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x00045671 File Offset: 0x00043871
		public ICollection<Type> TransientExceptions { get; protected set; }

		// Token: 0x170004EE RID: 1262
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x0004567A File Offset: 0x0004387A
		public bool IncludeReport
		{
			set
			{
				this.AddParameter("IncludeReport", value);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x0004568D File Offset: 0x0004388D
		internal bool WhatIf
		{
			set
			{
				this.AddParameter("WhatIf", value);
			}
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000456A0 File Offset: 0x000438A0
		public override string ToString()
		{
			return MigrationRunspaceProxy.GetCommandString(this.Command);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000456B0 File Offset: 0x000438B0
		protected virtual void UpdateSubscriptionSettings(ExchangeOutlookAnywhereEndpoint endpoint, ExchangeJobSubscriptionSettings jobSettings, ExchangeJobItemSubscriptionSettings jobItemSettings)
		{
			this.AddParameter("RemoteCredential", endpoint.Credentials);
			string value = string.IsNullOrEmpty(jobItemSettings.RPCProxyServer) ? endpoint.RpcProxyServer.ToString() : jobItemSettings.RPCProxyServer;
			this.AddParameter("OutlookAnywhereHostName", value);
			this.AddParameter("RemoteSourceMailboxServerLegacyDN", jobItemSettings.ExchangeServerDN);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004570C File Offset: 0x0004390C
		protected void AddParameter(string parameterName)
		{
			this.switchParameters.Add(parameterName);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0004571A File Offset: 0x0004391A
		protected void AddParameter(string parameterName, object value)
		{
			this.arguments[parameterName] = value;
		}

		// Token: 0x040005C2 RID: 1474
		internal const string IdentityParameter = "Identity";

		// Token: 0x040005C3 RID: 1475
		internal const string IncludeReportParameter = "IncludeReport";

		// Token: 0x040005C4 RID: 1476
		internal const string MRSInitialConnectionValidation = "InitialConnectionValidation";

		// Token: 0x040005C5 RID: 1477
		internal const string MRSServerParameter = "MRSServer";

		// Token: 0x040005C6 RID: 1478
		internal const string NameParameter = "Name";

		// Token: 0x040005C7 RID: 1479
		internal const string OutlookAnywhereHostNameParameter = "OutlookAnywhereHostName";

		// Token: 0x040005C8 RID: 1480
		internal const string RemoteCredentialsParameter = "RemoteCredential";

		// Token: 0x040005C9 RID: 1481
		internal const string RemoteSourceMailboxLegacyDNParameter = "RemoteSourceMailboxLegacyDN";

		// Token: 0x040005CA RID: 1482
		internal const string RemoteSourceServerLegacyDNParameter = "RemoteSourceMailboxServerLegacyDN";

		// Token: 0x040005CB RID: 1483
		internal const string SkipMergingParameter = "SkipMerging";

		// Token: 0x040005CC RID: 1484
		internal const string SuspendWhenReadyToCompleteParameter = "SuspendWhenReadyToComplete";

		// Token: 0x040005CD RID: 1485
		internal const string WhatIfParameter = "WhatIf";

		// Token: 0x040005CE RID: 1486
		private readonly Dictionary<string, object> arguments;

		// Token: 0x040005CF RID: 1487
		private readonly List<string> switchParameters = new List<string>();

		// Token: 0x040005D0 RID: 1488
		private readonly string cmdletName;
	}
}
