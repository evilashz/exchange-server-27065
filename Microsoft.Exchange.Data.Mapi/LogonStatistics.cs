using System;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public sealed class LogonStatistics : LogonStatisticsEntry
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000B068 File Offset: 0x00009268
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return LogonStatistics.schema;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000B06F File Offset: 0x0000926F
		public uint? AdapterSpeed
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.AdapterSpeed];
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B081 File Offset: 0x00009281
		public string ClientIPAddress
		{
			get
			{
				return (string)this[LogonStatisticsSchema.ClientIPAddress];
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000B093 File Offset: 0x00009293
		public ClientMode ClientMode
		{
			get
			{
				return (ClientMode)this[LogonStatisticsSchema.ClientMode];
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B0A5 File Offset: 0x000092A5
		public string ClientName
		{
			get
			{
				return (string)this[LogonStatisticsSchema.ClientName];
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000B0B7 File Offset: 0x000092B7
		public string ClientVersion
		{
			get
			{
				return (string)this[LogonStatisticsSchema.ClientVersion];
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000B0C9 File Offset: 0x000092C9
		public uint? CodePage
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.CodePage];
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000B0DB File Offset: 0x000092DB
		public uint? CurrentOpenAttachments
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.CurrentOpenAttachments];
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000B0ED File Offset: 0x000092ED
		public uint? CurrentOpenFolders
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.CurrentOpenFolders];
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000B0FF File Offset: 0x000092FF
		public uint? CurrentOpenMessages
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.CurrentOpenMessages];
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000B111 File Offset: 0x00009311
		public uint? FolderOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.FolderOperationCount];
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B123 File Offset: 0x00009323
		public string FullMailboxDirectoryName
		{
			get
			{
				return (string)this[LogonStatisticsSchema.FullMailboxDirectoryName];
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000B135 File Offset: 0x00009335
		public string FullUserDirectoryName
		{
			get
			{
				return (string)this[LogonStatisticsSchema.FullUserDirectoryName];
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000B147 File Offset: 0x00009347
		public string HostAddress
		{
			get
			{
				return (string)this[LogonStatisticsSchema.HostAddress];
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000B159 File Offset: 0x00009359
		public DateTime? LastAccessTime
		{
			get
			{
				return (DateTime?)this[LogonStatisticsSchema.LastAccessTime];
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000B16B File Offset: 0x0000936B
		public uint? Latency
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.Latency];
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000B17D File Offset: 0x0000937D
		public uint? LocaleID
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.LocaleID];
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000B18F File Offset: 0x0000938F
		public DateTime? LogonTime
		{
			get
			{
				return (DateTime?)this[LogonStatisticsSchema.LogonTime];
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000B1A1 File Offset: 0x000093A1
		public string MACAddress
		{
			get
			{
				return (string)this[LogonStatisticsSchema.MACAddress];
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000B1B3 File Offset: 0x000093B3
		public uint? MessagingOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.MessagingOperationCount];
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000B1C5 File Offset: 0x000093C5
		public uint? OtherOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.OtherOperationCount];
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000B1D7 File Offset: 0x000093D7
		public uint? ProgressOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.ProgressOperationCount];
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000B1E9 File Offset: 0x000093E9
		public uint? RPCCallsSucceeded
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.RPCCallsSucceeded];
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000B1FB File Offset: 0x000093FB
		public uint? StreamOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.StreamOperationCount];
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000B20D File Offset: 0x0000940D
		public uint? TableOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.TableOperationCount];
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000B21F File Offset: 0x0000941F
		public uint? TotalOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.TotalOperationCount];
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000B231 File Offset: 0x00009431
		public uint? TransferOperationCount
		{
			get
			{
				return (uint?)this[LogonStatisticsSchema.TransferOperationCount];
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000B243 File Offset: 0x00009443
		public string UserName
		{
			get
			{
				return (string)this[LogonStatisticsSchema.UserName];
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000B255 File Offset: 0x00009455
		public string Windows2000Account
		{
			get
			{
				return (string)this[LogonStatisticsSchema.Windows2000Account];
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000B267 File Offset: 0x00009467
		public string ApplicationId
		{
			get
			{
				return (string)this[LogonStatisticsSchema.ApplicationId];
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000B27C File Offset: 0x0000947C
		public string SessionId
		{
			get
			{
				long? num = (long?)this[LogonStatisticsSchema.SessionId];
				if (num != null)
				{
					return num.Value.ToString("x");
				}
				return string.Empty;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000B2BD File Offset: 0x000094BD
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000B2C5 File Offset: 0x000094C5
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
			internal set
			{
				this.serverName = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000B2CE File Offset: 0x000094CE
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000B2D6 File Offset: 0x000094D6
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
			internal set
			{
				this.databaseName = value;
			}
		}

		// Token: 0x0400011F RID: 287
		private string serverName;

		// Token: 0x04000120 RID: 288
		private string databaseName;

		// Token: 0x04000121 RID: 289
		private static MapiObjectSchema schema = ObjectSchema.GetInstance<LogonStatisticsSchema>();
	}
}
