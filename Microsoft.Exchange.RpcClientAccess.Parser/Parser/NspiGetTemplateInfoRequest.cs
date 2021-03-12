using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001D6 RID: 470
	internal sealed class NspiGetTemplateInfoRequest : MapiHttpRequest
	{
		// Token: 0x060009F0 RID: 2544 RVA: 0x0001F0DA File Offset: 0x0001D2DA
		public NspiGetTemplateInfoRequest(NspiGetTemplateInfoFlags flags, uint displayType, string templateDn, uint codePage, uint localeId, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.displayType = displayType;
			this.templateDn = templateDn;
			this.codePage = codePage;
			this.localeId = localeId;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001F10C File Offset: 0x0001D30C
		public NspiGetTemplateInfoRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiGetTemplateInfoFlags)reader.ReadUInt32();
			this.displayType = reader.ReadUInt32();
			this.templateDn = reader.ReadNullableAsciiString(StringFlags.IncludeNull);
			this.codePage = reader.ReadUInt32();
			this.localeId = reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001F164 File Offset: 0x0001D364
		public NspiGetTemplateInfoFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0001F16C File Offset: 0x0001D36C
		public uint DisplayType
		{
			get
			{
				return this.displayType;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0001F174 File Offset: 0x0001D374
		public string TemplateDn
		{
			get
			{
				return this.templateDn;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0001F17C File Offset: 0x0001D37C
		public uint CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0001F184 File Offset: 0x0001D384
		public uint LocaleId
		{
			get
			{
				return this.localeId;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001F18C File Offset: 0x0001D38C
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteUInt32(this.displayType);
			writer.WriteNullableAsciiString(this.templateDn);
			writer.WriteUInt32(this.codePage);
			writer.WriteUInt32(this.localeId);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000454 RID: 1108
		private readonly NspiGetTemplateInfoFlags flags;

		// Token: 0x04000455 RID: 1109
		private readonly uint displayType;

		// Token: 0x04000456 RID: 1110
		private readonly string templateDn;

		// Token: 0x04000457 RID: 1111
		private readonly uint codePage;

		// Token: 0x04000458 RID: 1112
		private readonly uint localeId;
	}
}
