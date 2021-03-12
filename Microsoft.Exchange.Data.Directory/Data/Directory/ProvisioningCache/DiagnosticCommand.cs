using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A4 RID: 1956
	internal class DiagnosticCommand
	{
		// Token: 0x06006133 RID: 24883 RVA: 0x0014ADD4 File Offset: 0x00148FD4
		public DiagnosticCommand(DiagnosticType command, Guid entry) : this(command, null, new Guid[]
		{
			entry
		})
		{
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x0014AE00 File Offset: 0x00149000
		public DiagnosticCommand(DiagnosticType command, Guid orgId, Guid entry) : this(command, new Guid[]
		{
			orgId
		}, new Guid[]
		{
			entry
		})
		{
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x0014AE3E File Offset: 0x0014903E
		public DiagnosticCommand(DiagnosticType command, ICollection<Guid> entries) : this(command, null, entries)
		{
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x0014AE49 File Offset: 0x00149049
		public DiagnosticCommand(DiagnosticType command, ICollection<Guid> orgIds, ICollection<Guid> entries)
		{
			if (entries == null)
			{
				throw new ArgumentNullException("entries");
			}
			this.Command = command;
			this.Organizations = orgIds;
			this.CacheEntries = entries;
		}

		// Token: 0x170022C1 RID: 8897
		// (get) Token: 0x06006137 RID: 24887 RVA: 0x0014AE74 File Offset: 0x00149074
		// (set) Token: 0x06006138 RID: 24888 RVA: 0x0014AE7C File Offset: 0x0014907C
		public DiagnosticType Command { get; private set; }

		// Token: 0x170022C2 RID: 8898
		// (get) Token: 0x06006139 RID: 24889 RVA: 0x0014AE85 File Offset: 0x00149085
		// (set) Token: 0x0600613A RID: 24890 RVA: 0x0014AE8D File Offset: 0x0014908D
		public ICollection<Guid> Organizations { get; private set; }

		// Token: 0x170022C3 RID: 8899
		// (get) Token: 0x0600613B RID: 24891 RVA: 0x0014AE96 File Offset: 0x00149096
		// (set) Token: 0x0600613C RID: 24892 RVA: 0x0014AE9E File Offset: 0x0014909E
		public ICollection<Guid> CacheEntries { get; private set; }

		// Token: 0x170022C4 RID: 8900
		// (get) Token: 0x0600613D RID: 24893 RVA: 0x0014AEA7 File Offset: 0x001490A7
		public bool IsGlobalCacheEntryCommand
		{
			get
			{
				return this.Organizations == null;
			}
		}

		// Token: 0x0600613E RID: 24894 RVA: 0x0014AEB4 File Offset: 0x001490B4
		public static DiagnosticCommand TryFromReceivedData(byte[] buffer, int bufLen, out Exception ex)
		{
			ex = null;
			DiagnosticCommand result = null;
			try
			{
				result = DiagnosticCommand.FromReceivedData(buffer, bufLen);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			return result;
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x0014AEE8 File Offset: 0x001490E8
		public static DiagnosticCommand FromReceivedData(byte[] buffer, int bufLen)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (bufLen <= 0)
			{
				throw new ArgumentException("bufLen is less than zero.");
			}
			if (buffer.Length < bufLen)
			{
				throw new ArgumentException("The buffer is too small.");
			}
			string @string = Encoding.UTF8.GetString(buffer, 0, bufLen);
			string[] array = @string.Split(new char[]
			{
				';'
			});
			if (array == null || array.Length < 3)
			{
				throw new ArgumentException("Received diagnostic command is invalid.");
			}
			if (!array[1].StartsWith("c:", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format("Invalid ProvisioningCache diagnostic command received, failed to parse the command {0}", array[1]));
			}
			DiagnosticType command;
			if (!Enum.TryParse<DiagnosticType>(array[1].Substring(array[1].IndexOf(':') + 1), true, out command))
			{
				throw new ArgumentException(string.Format("Invalid diagnostic command type received: {0}", array[1]));
			}
			List<Guid> orgIds = null;
			if (!array[2].StartsWith("o:global", StringComparison.OrdinalIgnoreCase))
			{
				if (string.IsNullOrWhiteSpace(array[2].Substring("o:".Length)))
				{
					orgIds = new List<Guid>();
				}
				else
				{
					orgIds = DiagnosticCommand.ParseGuidLists(array[2], "o:", "Org Ids");
				}
			}
			List<Guid> entries;
			if (array.Length > 3 && !string.IsNullOrWhiteSpace(array[3]))
			{
				entries = DiagnosticCommand.ParseGuidLists(array[3], "e:", "Cache Entries");
			}
			else
			{
				entries = new List<Guid>();
			}
			return new DiagnosticCommand(command, orgIds, entries);
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x0014B030 File Offset: 0x00149230
		private static List<Guid> ParseGuidLists(string guidList, string prefix, string usageType)
		{
			if (!guidList.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format("Invalid ProvisioningCache diagnostic command received, failed to parse the {0} : {1}", usageType, guidList));
			}
			string[] array = guidList.Substring(guidList.IndexOf(':') + 1).Split(new char[]
			{
				','
			});
			if (array == null || array.Length == 0)
			{
				throw new ArgumentException(string.Format("No {0} received in ProvisioningCache diagnostic command, invalid string: {1}", usageType, guidList));
			}
			List<Guid> list = new List<Guid>();
			foreach (string input in array)
			{
				Guid item;
				if (!Guid.TryParse(input, out item))
				{
					throw new ArgumentException(string.Format("Invalid {0} received in ProvisioningCache diagnostic command: {1}", usageType, guidList));
				}
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x0014B0E0 File Offset: 0x001492E0
		public byte[] ToSendMessage()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("v:").Append(DiagnosticCommand.Version).Append(';');
			stringBuilder.Append("c:").Append(this.Command.ToString()).Append(';');
			if (this.Organizations == null)
			{
				stringBuilder.Append("o:global").Append(';');
			}
			else if (this.Organizations.Count > 0)
			{
				DiagnosticCommand.AppendGuidList(stringBuilder, "o:", this.Organizations);
			}
			else
			{
				stringBuilder.Append("o:").Append(';');
			}
			if (this.CacheEntries.Count > 0)
			{
				DiagnosticCommand.AppendGuidList(stringBuilder, "e:", this.CacheEntries);
			}
			string s = stringBuilder.ToString().TrimEnd(new char[]
			{
				';'
			});
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x0014B1CC File Offset: 0x001493CC
		private static void AppendGuidList(StringBuilder sb, string prefix, ICollection<Guid> guids)
		{
			sb.Append(prefix);
			foreach (Guid guid in guids)
			{
				sb.Append(guid.ToString()).Append(',');
			}
			sb.Replace(',', ';', sb.Length - 1, 1);
		}

		// Token: 0x04004144 RID: 16708
		private const char Colon = ':';

		// Token: 0x04004145 RID: 16709
		private const char Comma = ',';

		// Token: 0x04004146 RID: 16710
		private const char Separator = ';';

		// Token: 0x04004147 RID: 16711
		private const string VersionPrefix = "v:";

		// Token: 0x04004148 RID: 16712
		private const string CommandPrefix = "c:";

		// Token: 0x04004149 RID: 16713
		private const string OrganizationPrefix = "o:";

		// Token: 0x0400414A RID: 16714
		private const string GlobalCachePrefix = "o:global";

		// Token: 0x0400414B RID: 16715
		private const string CacheEntryPrefix = "e:";

		// Token: 0x0400414C RID: 16716
		public static readonly int Version = 1;
	}
}
