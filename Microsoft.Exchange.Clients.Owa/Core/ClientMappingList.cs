using System;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000EC RID: 236
	internal sealed class ClientMappingList
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0003B328 File Offset: 0x00039528
		internal int Count
		{
			get
			{
				if (this.items == null)
				{
					return 0;
				}
				return this.items.Length;
			}
		}

		// Token: 0x1700022D RID: 557
		internal ClientMapping this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0003B346 File Offset: 0x00039546
		internal ClientMappingList(ClientMapping[] items)
		{
			this.items = items;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0003B358 File Offset: 0x00039558
		internal bool FindMatchingRange(string application, string platform, ClientControl control, UserAgentParser.UserAgentVersion minVersion, out int firstMatch, out int lastMatch)
		{
			firstMatch = 0;
			lastMatch = this.Count - 1;
			if (this.Count == 0)
			{
				return false;
			}
			ClientMappingList.MatchingParameters parameters = new ClientMappingList.MatchingParameters(application, platform, control, minVersion);
			for (ClientMappingList.ClientAttribute clientAttribute = ClientMappingList.ClientAttribute.First; clientAttribute < ClientMappingList.ClientAttribute.Last; clientAttribute++)
			{
				if (!this.MatchClientAttribute(ref firstMatch, ref lastMatch, clientAttribute, parameters))
				{
					return false;
				}
			}
			return this.MatchClientMinimumVersion(firstMatch, ref lastMatch, parameters);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0003B3B4 File Offset: 0x000395B4
		private bool MatchClientAttribute(ref int start, ref int end, ClientMappingList.ClientAttribute attribute, ClientMappingList.MatchingParameters parameters)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug((long)this.GetHashCode(), "ClientMappingList.MatchClientAttribute attribute = {0}, application = {1}, platform = {2}, control = {3}, start = {4}, end = {5}", new object[]
			{
				(int)attribute,
				parameters.Application,
				parameters.Platform,
				parameters.Control,
				start,
				end
			});
			if (start == end)
			{
				return this.IsMatchOrWildcard(start, attribute, parameters);
			}
			int num = end;
			if (!this.FindFirstMatch(start, ref num, attribute, parameters))
			{
				return false;
			}
			start = num;
			this.FindLastMatch(start, ref end, attribute, parameters);
			return true;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0003B458 File Offset: 0x00039658
		private bool FindFirstMatch(int first, ref int last, ClientMappingList.ClientAttribute attribute, ClientMappingList.MatchingParameters parameters)
		{
			int num = last;
			int num2 = first;
			first--;
			last++;
			while (last - first > 1)
			{
				int num3 = (first + last) / 2;
				bool flag = false;
				switch (attribute)
				{
				case ClientMappingList.ClientAttribute.First:
					flag = (string.CompareOrdinal(this.items[num3].Application, parameters.Application) < 0);
					break;
				case ClientMappingList.ClientAttribute.Platform:
					flag = (string.CompareOrdinal(this.items[num3].Platform, parameters.Platform) < 0);
					break;
				case ClientMappingList.ClientAttribute.Control:
					flag = (this.items[num3].Control < parameters.Control);
					break;
				}
				if (flag)
				{
					first = num3;
				}
				else
				{
					last = num3;
				}
			}
			if (last == num + 1 || !this.IsMatch(last, attribute, parameters))
			{
				if (!this.AttemptWildcardSearch(num2, attribute, parameters))
				{
					return false;
				}
				last = num2;
			}
			return true;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0003B52C File Offset: 0x0003972C
		private void FindLastMatch(int first, ref int last, ClientMappingList.ClientAttribute attribute, ClientMappingList.MatchingParameters parameters)
		{
			if (attribute == ClientMappingList.ClientAttribute.Control)
			{
				while (first <= last && this.items[first].Control == parameters.Control)
				{
					first++;
				}
				last = first - 1;
				return;
			}
			first--;
			last++;
			while (last - first > 1)
			{
				int num = (first + last) / 2;
				bool flag = false;
				switch (attribute)
				{
				case ClientMappingList.ClientAttribute.First:
					flag = (string.CompareOrdinal(this.items[num].Application, parameters.Application) > 0);
					break;
				case ClientMappingList.ClientAttribute.Platform:
					flag = (string.CompareOrdinal(this.items[num].Platform, parameters.Platform) > 0);
					break;
				}
				if (flag)
				{
					last = num;
				}
				else
				{
					first = num;
				}
			}
			last = first;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0003B5E0 File Offset: 0x000397E0
		private bool IsMatch(int index, ClientMappingList.ClientAttribute attribute, ClientMappingList.MatchingParameters parameters)
		{
			switch (attribute)
			{
			case ClientMappingList.ClientAttribute.First:
				return this.items[index].Application == parameters.Application;
			case ClientMappingList.ClientAttribute.Platform:
				return this.items[index].Platform == parameters.Platform;
			case ClientMappingList.ClientAttribute.Control:
				return this.items[index].Control == parameters.Control;
			default:
				return false;
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0003B650 File Offset: 0x00039850
		private bool IsMatchOrWildcard(int index, ClientMappingList.ClientAttribute attribute, ClientMappingList.MatchingParameters parameters)
		{
			switch (attribute)
			{
			case ClientMappingList.ClientAttribute.First:
				return this.IsMatch(index, attribute, parameters) || this.items[index].Application.Length == 0;
			case ClientMappingList.ClientAttribute.Platform:
				return this.IsMatch(index, attribute, parameters) || this.items[index].Platform.Length == 0;
			case ClientMappingList.ClientAttribute.Control:
				return this.IsMatch(index, attribute, parameters) || this.items[index].Control == ClientControl.None;
			default:
				return false;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0003B6D8 File Offset: 0x000398D8
		private bool AttemptWildcardSearch(int start, ClientMappingList.ClientAttribute attribute, ClientMappingList.MatchingParameters parameters)
		{
			ExTraceGlobals.FormsRegistryTracer.TraceDebug((long)this.GetHashCode(), "Attempting wildcard search");
			switch (attribute)
			{
			case ClientMappingList.ClientAttribute.First:
				if (this.items[start].Application.Length != 0)
				{
					return false;
				}
				parameters.Application = string.Empty;
				break;
			case ClientMappingList.ClientAttribute.Platform:
				if (this.items[start].Platform.Length != 0)
				{
					return false;
				}
				parameters.Platform = string.Empty;
				break;
			case ClientMappingList.ClientAttribute.Control:
				if (this.items[start].Control != ClientControl.None)
				{
					return false;
				}
				parameters.Control = ClientControl.None;
				break;
			}
			return true;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0003B774 File Offset: 0x00039974
		private bool MatchClientMinimumVersion(int start, ref int end, ClientMappingList.MatchingParameters parameters)
		{
			ExTraceGlobals.FormsRegistryCallTracer.TraceDebug<UserAgentParser.UserAgentVersion, int, int>((long)this.GetHashCode(), "ClientMappingList.MatchClientMinimumVersion version = {0}, start = {1}, end = {2}", parameters.Version, start, end);
			if (this.items[start].MinimumVersion.CompareTo(parameters.Version) > 0)
			{
				return false;
			}
			int num = start + 1;
			while (num <= end && this.items[num].MinimumVersion.CompareTo(parameters.Version) <= 0)
			{
				num++;
			}
			end = num - 1;
			return true;
		}

		// Token: 0x0400059A RID: 1434
		private ClientMapping[] items;

		// Token: 0x020000ED RID: 237
		private struct MatchingParameters
		{
			// Token: 0x060007EF RID: 2031 RVA: 0x0003B7F8 File Offset: 0x000399F8
			public MatchingParameters(string application, string platform, ClientControl control, UserAgentParser.UserAgentVersion version)
			{
				this.Application = application;
				this.Platform = platform;
				this.Control = control;
				this.Version = version;
			}

			// Token: 0x0400059B RID: 1435
			public string Application;

			// Token: 0x0400059C RID: 1436
			public string Platform;

			// Token: 0x0400059D RID: 1437
			public ClientControl Control;

			// Token: 0x0400059E RID: 1438
			public UserAgentParser.UserAgentVersion Version;
		}

		// Token: 0x020000EE RID: 238
		internal enum ClientAttribute
		{
			// Token: 0x040005A0 RID: 1440
			First,
			// Token: 0x040005A1 RID: 1441
			Application = 0,
			// Token: 0x040005A2 RID: 1442
			Platform,
			// Token: 0x040005A3 RID: 1443
			Control,
			// Token: 0x040005A4 RID: 1444
			Last
		}
	}
}
