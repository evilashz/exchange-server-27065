using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002FF RID: 767
	internal sealed class SuccessfulPublicLogonResult : SuccessfulLogonResult
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x0003158A File Offset: 0x0002F78A
		internal SuccessfulPublicLogonResult(IServerObject logonObject, LogonFlags logonFlags, StoreId[] folderIds, LogonExtendedResponseFlags extendedFlags, LocaleInfo? localeInfo, ReplId databaseReplId, Guid databaseGuid, Guid perUserReadGuid) : base(logonObject, logonFlags, folderIds, extendedFlags, localeInfo)
		{
			if (base.IsLogonFlagSet(LogonFlags.Private))
			{
				throw new ArgumentException("Public logon result requires private logon flag to be unset", "logonFlags");
			}
			this.databaseReplId = databaseReplId;
			this.databaseGuid = databaseGuid;
			this.perUserReadGuid = perUserReadGuid;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000315CA File Offset: 0x0002F7CA
		internal SuccessfulPublicLogonResult(Reader reader) : base(reader)
		{
			this.databaseReplId = ReplId.Parse(reader);
			this.databaseGuid = reader.ReadGuid();
			this.perUserReadGuid = reader.ReadGuid();
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x000315F7 File Offset: 0x0002F7F7
		public ReplId DatabaseReplId
		{
			get
			{
				return this.databaseReplId;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x000315FF File Offset: 0x0002F7FF
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00031607 File Offset: 0x0002F807
		public Guid PerUserReadGuid
		{
			get
			{
				return this.perUserReadGuid;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0003160F File Offset: 0x0002F80F
		public override string ToString()
		{
			return "SuccessfulPublicLogonResult: " + this.ToBareString();
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00031624 File Offset: 0x0002F824
		public new string ToBareString()
		{
			return string.Format("{0} DatabaseId[{1}] Database[{2}] PerUserRead[{3}]", new object[]
			{
				base.ToBareString(),
				this.databaseReplId.ToBareString(),
				this.databaseGuid,
				this.perUserReadGuid
			});
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0003167C File Offset: 0x0002F87C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.databaseReplId.Serialize(writer);
			writer.WriteGuid(this.databaseGuid);
			writer.WriteGuid(this.perUserReadGuid);
		}

		// Token: 0x040009B0 RID: 2480
		private readonly ReplId databaseReplId;

		// Token: 0x040009B1 RID: 2481
		private readonly Guid databaseGuid;

		// Token: 0x040009B2 RID: 2482
		private readonly Guid perUserReadGuid;
	}
}
