using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001BA RID: 442
	internal sealed class EmsmdbConnectRequest : MapiHttpRequest
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x0001E108 File Offset: 0x0001C308
		public EmsmdbConnectRequest(string userDn, uint flags, uint defaultCodePage, uint sortLocaleId, uint stringLocaleId, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.userDn = userDn;
			this.flags = flags;
			this.defaultCodePage = defaultCodePage;
			this.sortLocaleId = sortLocaleId;
			this.stringLocalId = stringLocaleId;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001E138 File Offset: 0x0001C338
		public EmsmdbConnectRequest(Reader reader) : base(reader)
		{
			this.userDn = reader.ReadAsciiString(StringFlags.IncludeNull);
			this.flags = reader.ReadUInt32();
			this.defaultCodePage = reader.ReadUInt32();
			this.sortLocaleId = reader.ReadUInt32();
			this.stringLocalId = reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0001E190 File Offset: 0x0001C390
		public string UserDn
		{
			get
			{
				return this.userDn;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001E198 File Offset: 0x0001C398
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		public uint DefaultCodePage
		{
			get
			{
				return this.defaultCodePage;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		public uint SortLocaleId
		{
			get
			{
				return this.sortLocaleId;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		public uint StringLocaleId
		{
			get
			{
				return this.stringLocalId;
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001E1B8 File Offset: 0x0001C3B8
		public override void Serialize(Writer writer)
		{
			writer.WriteAsciiString(this.userDn, StringFlags.IncludeNull);
			writer.WriteUInt32(this.flags);
			writer.WriteUInt32(this.defaultCodePage);
			writer.WriteUInt32(this.sortLocaleId);
			writer.WriteUInt32(this.stringLocalId);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000418 RID: 1048
		private readonly string userDn;

		// Token: 0x04000419 RID: 1049
		private readonly uint flags;

		// Token: 0x0400041A RID: 1050
		private readonly uint defaultCodePage;

		// Token: 0x0400041B RID: 1051
		private readonly uint sortLocaleId;

		// Token: 0x0400041C RID: 1052
		private readonly uint stringLocalId;
	}
}
