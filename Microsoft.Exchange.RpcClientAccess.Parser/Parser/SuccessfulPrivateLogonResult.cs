using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002FE RID: 766
	internal sealed class SuccessfulPrivateLogonResult : SuccessfulLogonResult
	{
		// Token: 0x060011C9 RID: 4553 RVA: 0x000312B4 File Offset: 0x0002F4B4
		internal SuccessfulPrivateLogonResult(IServerObject logonObject, LogonFlags logonFlags, StoreId[] folderIds, LogonExtendedResponseFlags extendedFlags, LocaleInfo? localeInfo, LogonResponseFlags logonResponseFlags, Guid mailboxInstanceGuid, ReplId databaseReplId, Guid databaseGuid, ExDateTime serverTime, ulong routingConfigurationTimestamp, StoreState storeState) : base(logonObject, logonFlags, folderIds, extendedFlags, localeInfo)
		{
			if (!base.IsLogonFlagSet(LogonFlags.Private))
			{
				throw new ArgumentException("Private logon result requires private logon flag to be set", "logonFlags");
			}
			this.logonResponseFlags = logonResponseFlags;
			this.mailboxInstanceGuid = mailboxInstanceGuid;
			this.databaseReplId = databaseReplId;
			this.databaseGuid = databaseGuid;
			this.serverTime = serverTime;
			this.routingConfigurationTimestamp = routingConfigurationTimestamp;
			this.storeState = storeState;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00031320 File Offset: 0x0002F520
		internal SuccessfulPrivateLogonResult(Reader reader) : base(reader)
		{
			this.logonResponseFlags = (LogonResponseFlags)reader.ReadByte();
			this.mailboxInstanceGuid = reader.ReadGuid();
			this.databaseReplId = ReplId.Parse(reader);
			this.databaseGuid = reader.ReadGuid();
			int second = (int)reader.ReadByte();
			int minute = (int)reader.ReadByte();
			int hour = (int)reader.ReadByte();
			reader.ReadByte();
			int day = (int)reader.ReadByte();
			int month = (int)reader.ReadByte();
			int year = (int)reader.ReadUInt16();
			this.serverTime = new ExDateTime(ExTimeZone.UtcTimeZone, year, month, day, hour, minute, second);
			this.routingConfigurationTimestamp = reader.ReadUInt64();
			this.storeState = (StoreState)reader.ReadUInt32();
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x000313C7 File Offset: 0x0002F5C7
		internal LogonResponseFlags LogonResponseFlags
		{
			get
			{
				return this.logonResponseFlags;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x000313CF File Offset: 0x0002F5CF
		internal Guid MailboxInstanceGuid
		{
			get
			{
				return this.mailboxInstanceGuid;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x000313D7 File Offset: 0x0002F5D7
		internal ReplId DatabaseReplId
		{
			get
			{
				return this.databaseReplId;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x000313DF File Offset: 0x0002F5DF
		internal Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x000313E7 File Offset: 0x0002F5E7
		internal ulong RoutingConfigurationTimestamp
		{
			get
			{
				return this.routingConfigurationTimestamp;
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000313EF File Offset: 0x0002F5EF
		public override string ToString()
		{
			return "SuccessfulPrivateLogonResult: " + this.ToBareString();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00031404 File Offset: 0x0002F604
		public new string ToBareString()
		{
			return string.Format("{0} LogonResponse[{1}] MailboxInstance[{2}] DatabaseId[{3}] Database[{4}] Time[{5}] Gwart[{6:X}] State[{7}]", new object[]
			{
				base.ToBareString(),
				this.logonResponseFlags,
				this.mailboxInstanceGuid,
				this.databaseReplId.ToBareString(),
				this.databaseGuid,
				this.serverTime,
				this.routingConfigurationTimestamp,
				this.storeState
			});
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00031494 File Offset: 0x0002F694
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.logonResponseFlags);
			writer.WriteGuid(this.mailboxInstanceGuid);
			this.databaseReplId.Serialize(writer);
			writer.WriteGuid(this.databaseGuid);
			writer.WriteByte((byte)this.serverTime.Second);
			writer.WriteByte((byte)this.serverTime.Minute);
			writer.WriteByte((byte)this.serverTime.Hour);
			writer.WriteByte((byte)this.serverTime.DayOfWeek);
			writer.WriteByte((byte)this.serverTime.Day);
			writer.WriteByte((byte)this.serverTime.Month);
			writer.WriteUInt16((ushort)this.serverTime.Year);
			writer.WriteUInt64(this.routingConfigurationTimestamp);
			writer.WriteUInt32((uint)this.storeState);
		}

		// Token: 0x040009A9 RID: 2473
		private readonly LogonResponseFlags logonResponseFlags;

		// Token: 0x040009AA RID: 2474
		private readonly Guid mailboxInstanceGuid;

		// Token: 0x040009AB RID: 2475
		private readonly ReplId databaseReplId;

		// Token: 0x040009AC RID: 2476
		private readonly Guid databaseGuid;

		// Token: 0x040009AD RID: 2477
		private readonly ExDateTime serverTime;

		// Token: 0x040009AE RID: 2478
		private readonly ulong routingConfigurationTimestamp;

		// Token: 0x040009AF RID: 2479
		private readonly StoreState storeState;
	}
}
