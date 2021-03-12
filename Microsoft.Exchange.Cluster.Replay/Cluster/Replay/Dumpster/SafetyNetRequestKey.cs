using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay.Dumpster
{
	// Token: 0x0200017E RID: 382
	internal class SafetyNetRequestKey
	{
		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00042442 File Offset: 0x00040642
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0004244A File Offset: 0x0004064A
		public string ServerName { get; private set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00042453 File Offset: 0x00040653
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0004245B File Offset: 0x0004065B
		public DateTime RequestCreationTimeUtc { get; private set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00042464 File Offset: 0x00040664
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0004246C File Offset: 0x0004066C
		public string UniqueStr { get; private set; }

		// Token: 0x06000F65 RID: 3941 RVA: 0x00042475 File Offset: 0x00040675
		public SafetyNetRequestKey(SafetyNetInfo info) : this(info.SourceServerName, info.FailoverTimeUtc, info.UniqueStr)
		{
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0004248F File Offset: 0x0004068F
		public SafetyNetRequestKey(string serverName, DateTime creationTimeUtc, string uniqueString)
		{
			this.ServerName = serverName;
			this.RequestCreationTimeUtc = creationTimeUtc;
			this.UniqueStr = uniqueString;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000424AC File Offset: 0x000406AC
		public static SafetyNetRequestKey Parse(string requestKeyName)
		{
			string[] array = requestKeyName.Split(new char[]
			{
				'*'
			});
			return new SafetyNetRequestKey(array[0], DateTimeHelper.ParseIntoDateTime(array[1]), array[2]);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000424E4 File Offset: 0x000406E4
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				this.m_toString = string.Join(SafetyNetRequestKey.FieldSeparatorStr, new string[]
				{
					this.ServerName,
					DateTimeHelper.ToPersistedString(this.RequestCreationTimeUtc),
					this.UniqueStr
				});
			}
			return this.m_toString;
		}

		// Token: 0x04000653 RID: 1619
		private const char FieldSeparator = '*';

		// Token: 0x04000654 RID: 1620
		private static readonly string FieldSeparatorStr = new string('*', 1);

		// Token: 0x04000655 RID: 1621
		private string m_toString;
	}
}
