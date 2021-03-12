using System;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200008C RID: 140
	internal static class DocumentLibraryUtility
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00028BF3 File Offset: 0x00026DF3
		private static MultiValuedProperty<string> RemoteDocumentsAllowedServers
		{
			get
			{
				return GlobalSettings.RemoteDocumentsAllowedServers;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00028BFA File Offset: 0x00026DFA
		private static MultiValuedProperty<string> RemoteDocumentsBlockedServers
		{
			get
			{
				return GlobalSettings.RemoteDocumentsBlockedServers;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00028C01 File Offset: 0x00026E01
		private static MultiValuedProperty<string> RemoteDocumentsInternalDomainSuffixList
		{
			get
			{
				return GlobalSettings.RemoteDocumentsInternalDomainSuffixList;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00028C08 File Offset: 0x00026E08
		private static RemoteDocumentsActions RemoteDocumentsActionForUnknownServers
		{
			get
			{
				if (GlobalSettings.RemoteDocumentsActionForUnknownServers == null)
				{
					return RemoteDocumentsActions.Allow;
				}
				return GlobalSettings.RemoteDocumentsActionForUnknownServers.Value;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00028C34 File Offset: 0x00026E34
		public static bool IsTrustedProtocol(string protocol)
		{
			AirSyncDiagnostics.Assert(!string.IsNullOrEmpty(protocol));
			for (int i = 0; i < DocumentLibraryUtility.trustedProtocols.Length; i++)
			{
				if (string.Equals(protocol, DocumentLibraryUtility.trustedProtocols[i], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00028C74 File Offset: 0x00026E74
		public static bool IsInternalUri(Uri uri)
		{
			AirSyncDiagnostics.Assert(uri != null);
			string host = uri.Host;
			if (DocumentLibraryUtility.RemoteDocumentsInternalDomainSuffixList != null)
			{
				foreach (string value in DocumentLibraryUtility.RemoteDocumentsInternalDomainSuffixList)
				{
					if (host.EndsWith(value, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			IPAddress ipaddress = new IPAddress(0L);
			bool result;
			try
			{
				result = (!IPAddress.TryParse(host, out ipaddress) && host.IndexOf('.') < 0);
			}
			catch (ArgumentException)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.AlgorithmTracer, null, "Invalid Uri Format in internal URI determination");
				result = false;
			}
			return result;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00028D30 File Offset: 0x00026F30
		public static bool IsUncAccessEnabled(IAirSyncUser user)
		{
			AirSyncDiagnostics.Assert(user != null);
			PolicyData policyData = ADNotificationManager.GetPolicyData(user);
			return policyData == null || policyData.UNCAccessEnabled;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00028D5C File Offset: 0x00026F5C
		public static bool IsWssAccessEnabled(IAirSyncUser user)
		{
			AirSyncDiagnostics.Assert(user != null);
			PolicyData policyData = ADNotificationManager.GetPolicyData(user);
			return policyData == null || policyData.WSSAccessEnabled;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00028D88 File Offset: 0x00026F88
		public static bool IsBlockedHostName(string hostName)
		{
			AirSyncDiagnostics.Assert(!string.IsNullOrEmpty(hostName));
			if (DocumentLibraryUtility.RemoteDocumentsBlockedServers != null)
			{
				foreach (string b in DocumentLibraryUtility.RemoteDocumentsBlockedServers)
				{
					if (string.Equals(hostName, b, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			if (DocumentLibraryUtility.RemoteDocumentsAllowedServers != null)
			{
				foreach (string b2 in DocumentLibraryUtility.RemoteDocumentsAllowedServers)
				{
					if (string.Equals(hostName, b2, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}
			return DocumentLibraryUtility.RemoteDocumentsActionForUnknownServers == RemoteDocumentsActions.Block;
		}

		// Token: 0x04000508 RID: 1288
		private static readonly string[] trustedProtocols = new string[]
		{
			"http",
			"https",
			"file"
		};
	}
}
