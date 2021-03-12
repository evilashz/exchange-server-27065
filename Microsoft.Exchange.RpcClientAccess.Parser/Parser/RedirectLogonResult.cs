using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002FC RID: 764
	internal sealed class RedirectLogonResult : RopResult
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x00030E63 File Offset: 0x0002F063
		internal RedirectLogonResult(Reader reader) : base(reader)
		{
			this.logonFlags = (LogonFlags)reader.ReadByte();
			this.serverLegacyDn = reader.ReadAsciiString(StringFlags.IncludeNull | StringFlags.Sized);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00030E85 File Offset: 0x0002F085
		internal RedirectLogonResult(string serverLegacyDn, LogonFlags logonFlags) : base(RopId.Logon, ErrorCode.WrongServer, null)
		{
			this.logonFlags = logonFlags;
			this.serverLegacyDn = serverLegacyDn;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00030EA6 File Offset: 0x0002F0A6
		internal string ServerLegacyDn
		{
			get
			{
				return this.serverLegacyDn;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00030EAE File Offset: 0x0002F0AE
		internal LogonFlags LogonFlags
		{
			get
			{
				return this.logonFlags;
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00030EB6 File Offset: 0x0002F0B6
		public override string ToString()
		{
			return string.Format("RedirectLogonResult: {0} {1}", this.ServerLegacyDn, this.LogonFlags);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00030ED3 File Offset: 0x0002F0D3
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.logonFlags);
			writer.WriteAsciiString(this.ServerLegacyDn, StringFlags.IncludeNull | StringFlags.Sized);
		}

		// Token: 0x040009A2 RID: 2466
		private readonly string serverLegacyDn;

		// Token: 0x040009A3 RID: 2467
		private readonly LogonFlags logonFlags;
	}
}
