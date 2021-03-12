using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000322 RID: 802
	internal sealed class RopRegisterNotification : InputOutputRop
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000336E8 File Offset: 0x000318E8
		internal override RopId RopId
		{
			get
			{
				return RopId.RegisterNotification;
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000336EC File Offset: 0x000318EC
		internal static Rop CreateRop()
		{
			return new RopRegisterNotification();
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000336F4 File Offset: 0x000318F4
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte notificationHandleIndex, NotificationFlags flags, NotificationEventFlags eventFlags, bool wantGlobalScope, StoreId folderId, StoreId messageId)
		{
			if ((byte)(eventFlags & NotificationEventFlags.RowFound) == 4)
			{
				throw new ArgumentException("RowFound is unsupported", "eventFlags");
			}
			base.SetCommonInput(logonIndex, handleTableIndex, notificationHandleIndex);
			this.flags = flags;
			this.eventFlags = eventFlags;
			this.wantGlobalScope = wantGlobalScope;
			this.folderId = folderId;
			this.messageId = messageId;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0003374C File Offset: 0x0003194C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16((ushort)this.flags);
			if ((ushort)(this.flags & NotificationFlags.Extended) == 1024)
			{
				writer.WriteByte((byte)this.eventFlags);
				if ((byte)(this.eventFlags & NotificationEventFlags.RowFound) == 4)
				{
					throw new InvalidOperationException("Cannot serialize NotificationEventFlags.RowFound");
				}
			}
			writer.WriteBool(this.wantGlobalScope);
			if (!this.wantGlobalScope)
			{
				this.folderId.Serialize(writer);
				this.messageId.Serialize(writer);
			}
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000337D0 File Offset: 0x000319D0
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulRegisterNotificationResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000337FE File Offset: 0x000319FE
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new RegisterNotificationResultFactory(base.PeekReturnServerObjectHandleValue);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0003380C File Offset: 0x00031A0C
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (NotificationFlags)reader.ReadUInt16();
			if ((ushort)(this.flags & NotificationFlags.Extended) == 1024)
			{
				this.eventFlags = (NotificationEventFlags)reader.ReadByte();
				if ((byte)(this.eventFlags & NotificationEventFlags.RowFound) == 4)
				{
					reader.ReadUInt16();
					int num = (int)(reader.ReadByte() + reader.ReadByte());
					for (int i = 0; i < num; i++)
					{
						reader.ReadPropertyTag();
					}
				}
			}
			this.wantGlobalScope = reader.ReadBool();
			if (this.wantGlobalScope)
			{
				this.folderId = default(StoreId);
				this.messageId = default(StoreId);
				return;
			}
			this.folderId = StoreId.Parse(reader);
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000338C6 File Offset: 0x00031AC6
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000338DC File Offset: 0x00031ADC
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			RegisterNotificationResultFactory resultFactory = new RegisterNotificationResultFactory(base.PeekReturnServerObjectHandleValue);
			this.result = ropHandler.RegisterNotification(serverObject, this.flags, this.eventFlags, this.wantGlobalScope, this.folderId, this.messageId, resultFactory);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00033924 File Offset: 0x00031B24
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" EventFlags=").Append(this.eventFlags);
			stringBuilder.Append(" Global=").Append(this.wantGlobalScope);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
		}

		// Token: 0x04000A34 RID: 2612
		private const RopId RopType = RopId.RegisterNotification;

		// Token: 0x04000A35 RID: 2613
		private NotificationFlags flags;

		// Token: 0x04000A36 RID: 2614
		private NotificationEventFlags eventFlags;

		// Token: 0x04000A37 RID: 2615
		private bool wantGlobalScope;

		// Token: 0x04000A38 RID: 2616
		private StoreId folderId;

		// Token: 0x04000A39 RID: 2617
		private StoreId messageId;
	}
}
