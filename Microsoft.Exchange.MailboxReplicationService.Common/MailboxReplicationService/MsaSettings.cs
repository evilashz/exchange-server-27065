using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000A RID: 10
	[DataContract]
	internal sealed class MsaSettings : ItemPropertiesBase
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00002B80 File Offset: 0x00000D80
		public override void Apply(MrsPSHandler psHandler, MailboxSession mailboxSession)
		{
			using (MonadCommand command = psHandler.GetCommand(MrsCmdlet.SetConsumerMailbox))
			{
				int num = 1;
				command.Parameters.AddWithValue("Identity", ConsumerMailboxIdParameter.Parse(mailboxSession.MailboxGuid.ToString()));
				if (this.LastName != null)
				{
					command.Parameters.AddWithValue("LastName", this.LastName);
				}
				if (this.FirstName != null)
				{
					command.Parameters.AddWithValue("FirstName", this.FirstName);
				}
				if (this.BirthdayInt != 0)
				{
					command.Parameters.AddWithValue("Birthdate", this.BirthdayInt);
					command.Parameters.AddWithValue("BirthdayPrecision", this.BirthdayPrecision);
				}
				if (command.Parameters.Count > num)
				{
					command.Execute();
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002C70 File Offset: 0x00000E70
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00002C78 File Offset: 0x00000E78
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002C81 File Offset: 0x00000E81
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00002C89 File Offset: 0x00000E89
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002C92 File Offset: 0x00000E92
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002C9A File Offset: 0x00000E9A
		[DataMember]
		public string MailDomain { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002CA3 File Offset: 0x00000EA3
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002CAB File Offset: 0x00000EAB
		[DataMember]
		public string LanguageCode { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002CB4 File Offset: 0x00000EB4
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002CBC File Offset: 0x00000EBC
		[DataMember]
		public string CountryCode { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002CC5 File Offset: 0x00000EC5
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002CCD File Offset: 0x00000ECD
		[DataMember]
		public int GeoIdInt { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002CD6 File Offset: 0x00000ED6
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002CDE File Offset: 0x00000EDE
		[DataMember]
		public ushort GenderCodeUInt16 { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002CE7 File Offset: 0x00000EE7
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00002CEF File Offset: 0x00000EEF
		[DataMember]
		public ushort OccupationCodeUInt16 { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002CF8 File Offset: 0x00000EF8
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002D00 File Offset: 0x00000F00
		[DataMember]
		public string ZipCode { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002D09 File Offset: 0x00000F09
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00002D11 File Offset: 0x00000F11
		[DataMember]
		public string TimeZone { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002D1A File Offset: 0x00000F1A
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002D22 File Offset: 0x00000F22
		[DataMember]
		public string LcidString { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002D2B File Offset: 0x00000F2B
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002D33 File Offset: 0x00000F33
		[DataMember]
		public int ProfileVersionInt { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002D3C File Offset: 0x00000F3C
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002D44 File Offset: 0x00000F44
		[DataMember]
		public int MiscFlagsInt { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002D4D File Offset: 0x00000F4D
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002D55 File Offset: 0x00000F55
		[DataMember]
		public int FlagsInt { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002D5E File Offset: 0x00000F5E
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002D66 File Offset: 0x00000F66
		[DataMember]
		public int Accessibilty { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002D6F File Offset: 0x00000F6F
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00002D77 File Offset: 0x00000F77
		[DataMember]
		public ushort BirthdayPrecision { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002D80 File Offset: 0x00000F80
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002D88 File Offset: 0x00000F88
		[DataMember]
		public int BirthdayInt { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002D91 File Offset: 0x00000F91
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00002D99 File Offset: 0x00000F99
		[DataMember]
		public int Age { get; set; }
	}
}
