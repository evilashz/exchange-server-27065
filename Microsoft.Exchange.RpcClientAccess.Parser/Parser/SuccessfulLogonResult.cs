using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002FD RID: 765
	internal class SuccessfulLogonResult : LogonResult
	{
		// Token: 0x060011BF RID: 4543 RVA: 0x00030EF8 File Offset: 0x0002F0F8
		protected SuccessfulLogonResult(IServerObject logonObject, LogonFlags logonFlags, StoreId[] folderIds, LogonExtendedResponseFlags extendedFlags, LocaleInfo? localeInfo) : base(ErrorCode.None, logonObject)
		{
			this.logonFlags = logonFlags;
			this.folderIds = folderIds;
			this.extendedFlags = extendedFlags;
			this.localeInfo = localeInfo;
			if ((long)folderIds.Length != 13L)
			{
				throw new ArgumentException("Must set " + 13U + " folder ids", "folderIds");
			}
			if (extendedFlags != LogonExtendedResponseFlags.None && !this.IsLogonFlagSet(LogonFlags.Extended))
			{
				throw new ArgumentException("Extended response flags are specified but LogonFlags.Extended is not set", "logonFlags");
			}
			if (localeInfo != null && !this.IsExtendedFlagSet(LogonExtendedResponseFlags.LocaleInfo))
			{
				throw new ArgumentException("Locale is specified but LogonExtendedResponseFlags.LocaleInfo is not set", "extendedFlags");
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00030F98 File Offset: 0x0002F198
		protected SuccessfulLogonResult(Reader reader) : base(reader)
		{
			this.logonFlags = (LogonFlags)reader.ReadByte();
			this.folderIds = new StoreId[13];
			int num = 0;
			while ((long)num < 13L)
			{
				this.folderIds[num] = StoreId.Parse(reader);
				num++;
			}
			this.extendedFlags = LogonExtendedResponseFlags.None;
			this.localeInfo = null;
			if (this.IsLogonFlagSet(LogonFlags.Extended))
			{
				this.extendedFlags = (LogonExtendedResponseFlags)reader.ReadUInt32();
				if (this.IsExtendedFlagSet(LogonExtendedResponseFlags.LocaleInfo))
				{
					this.localeInfo = new LocaleInfo?(Microsoft.Exchange.RpcClientAccess.Parser.LocaleInfo.Parse(reader));
				}
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0003102F File Offset: 0x0002F22F
		internal LogonFlags LogonFlags
		{
			get
			{
				return this.logonFlags;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00031037 File Offset: 0x0002F237
		internal StoreId[] FolderIds
		{
			get
			{
				return this.folderIds;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0003103F File Offset: 0x0002F23F
		internal LogonExtendedResponseFlags LogonExtendedResponseFlags
		{
			get
			{
				return this.extendedFlags;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00031047 File Offset: 0x0002F247
		internal LocaleInfo? LocaleInfo
		{
			get
			{
				return this.localeInfo;
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00031050 File Offset: 0x0002F250
		public string ToBareString()
		{
			return string.Format("LogonFlags[{0}] FolderIds[{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}] Extended[{14}] Locale[{15}]", new object[]
			{
				this.logonFlags,
				this.folderIds[0],
				this.folderIds[1],
				this.folderIds[2],
				this.folderIds[3],
				this.folderIds[4],
				this.folderIds[5],
				this.folderIds[6],
				this.folderIds[7],
				this.folderIds[8],
				this.folderIds[9],
				this.folderIds[10],
				this.folderIds[11],
				this.folderIds[12],
				this.extendedFlags,
				(this.localeInfo != null) ? this.localeInfo.Value.ToBareString() : "null"
			});
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0003120C File Offset: 0x0002F40C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.logonFlags);
			foreach (StoreId storeId in this.folderIds)
			{
				storeId.Serialize(writer);
			}
			if (this.IsLogonFlagSet(LogonFlags.Extended))
			{
				writer.WriteUInt32((uint)this.extendedFlags);
				if (this.IsExtendedFlagSet(LogonExtendedResponseFlags.LocaleInfo))
				{
					this.localeInfo.Value.Serialize(writer);
				}
			}
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0003128D File Offset: 0x0002F48D
		protected bool IsLogonFlagSet(LogonFlags flagToTest)
		{
			return (this.logonFlags & flagToTest) == flagToTest;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0003129A File Offset: 0x0002F49A
		protected bool IsExtendedFlagSet(LogonExtendedResponseFlags flagToTest)
		{
			return this.IsLogonFlagSet(LogonFlags.Extended) && (this.extendedFlags & flagToTest) == flagToTest;
		}

		// Token: 0x040009A4 RID: 2468
		private const uint FolderIdsCount = 13U;

		// Token: 0x040009A5 RID: 2469
		private readonly LogonFlags logonFlags;

		// Token: 0x040009A6 RID: 2470
		private readonly StoreId[] folderIds;

		// Token: 0x040009A7 RID: 2471
		private readonly LogonExtendedResponseFlags extendedFlags;

		// Token: 0x040009A8 RID: 2472
		private readonly LocaleInfo? localeInfo;
	}
}
