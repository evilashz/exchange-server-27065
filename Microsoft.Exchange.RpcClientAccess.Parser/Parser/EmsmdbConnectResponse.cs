using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001BD RID: 445
	internal sealed class EmsmdbConnectResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x0001E375 File Offset: 0x0001C575
		public EmsmdbConnectResponse(uint returnCode, uint maxPollingInterval, uint retryCount, uint retryDelay, string dnPrefix, string displayName, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.maxPollingInterval = maxPollingInterval;
			this.retryCount = retryCount;
			this.retryDelay = retryDelay;
			this.dnPrefix = dnPrefix;
			this.displayName = displayName;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001E3A8 File Offset: 0x0001C5A8
		public EmsmdbConnectResponse(Reader reader) : base(reader)
		{
			this.maxPollingInterval = reader.ReadUInt32();
			this.retryCount = reader.ReadUInt32();
			this.retryDelay = reader.ReadUInt32();
			this.dnPrefix = reader.ReadAsciiString(StringFlags.IncludeNull);
			this.displayName = reader.ReadUnicodeString(StringFlags.IncludeNull);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001E401 File Offset: 0x0001C601
		public uint MaxPollingInterval
		{
			get
			{
				return this.maxPollingInterval;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0001E409 File Offset: 0x0001C609
		public uint RetryCount
		{
			get
			{
				return this.retryCount;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001E411 File Offset: 0x0001C611
		public uint RetryDelay
		{
			get
			{
				return this.retryDelay;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001E419 File Offset: 0x0001C619
		public string DnPrefix
		{
			get
			{
				return this.dnPrefix;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001E421 File Offset: 0x0001C621
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001E42C File Offset: 0x0001C62C
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.maxPollingInterval);
			writer.WriteUInt32(this.retryCount);
			writer.WriteUInt32(this.retryDelay);
			writer.WriteAsciiString(this.dnPrefix, StringFlags.IncludeNull);
			writer.WriteUnicodeString(this.displayName, StringFlags.IncludeNull);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000420 RID: 1056
		private readonly uint maxPollingInterval;

		// Token: 0x04000421 RID: 1057
		private readonly uint retryCount;

		// Token: 0x04000422 RID: 1058
		private readonly uint retryDelay;

		// Token: 0x04000423 RID: 1059
		private readonly string dnPrefix;

		// Token: 0x04000424 RID: 1060
		private readonly string displayName;
	}
}
