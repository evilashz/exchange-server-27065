using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200005F RID: 95
	internal sealed class MessageHeader
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000A5EF File Offset: 0x000087EF
		public MessageHeader(bool hasNamedProperties, bool useUnicode, string subjectPrefix, string normalizedSubject, ushort messageRecipientsCount)
		{
			this.useUnicode = useUnicode;
			this.hasNamedProperties = hasNamedProperties;
			this.subjectPrefix = subjectPrefix;
			this.normalizedSubject = normalizedSubject;
			this.messageRecipientsCount = messageRecipientsCount;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A61C File Offset: 0x0000881C
		internal MessageHeader(Reader reader, Encoding string8Encoding)
		{
			this.hasNamedProperties = reader.ReadBool();
			StringFormatType stringFormatType = (StringFormatType)reader.PeekByte(0L);
			if (stringFormatType != StringFormatType.String8)
			{
				this.useUnicode = true;
			}
			this.subjectPrefix = reader.ReadFormattedString(string8Encoding);
			this.normalizedSubject = reader.ReadFormattedString(string8Encoding);
			this.messageRecipientsCount = reader.ReadUInt16();
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A675 File Offset: 0x00008875
		internal bool HasNamedProperties
		{
			get
			{
				return this.hasNamedProperties;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000A67D File Offset: 0x0000887D
		internal string SubjectPrefix
		{
			get
			{
				return this.subjectPrefix;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000A685 File Offset: 0x00008885
		internal string NormalizedSubject
		{
			get
			{
				return this.normalizedSubject;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A68D File Offset: 0x0000888D
		internal ushort MessageRecipientsCount
		{
			get
			{
				return this.messageRecipientsCount;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A698 File Offset: 0x00008898
		internal void Serialize(Writer writer, Encoding string8Encoding)
		{
			writer.WriteBool(this.hasNamedProperties, 1);
			writer.WriteFormattedString(this.subjectPrefix, this.useUnicode, string8Encoding);
			writer.WriteFormattedString(this.normalizedSubject, this.useUnicode, string8Encoding);
			writer.WriteUInt16(this.messageRecipientsCount);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A6E4 File Offset: 0x000088E4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A708 File Offset: 0x00008908
		internal void AppendToString(StringBuilder stringBuilder)
		{
			stringBuilder.Append("[NamedProps=").Append(this.hasNamedProperties);
			stringBuilder.Append(" SubjectPrefix=[").Append(this.subjectPrefix).Append("]");
			stringBuilder.Append(" NormalizedSubject=[").Append(this.normalizedSubject).Append("]");
			stringBuilder.Append(" TotalRecipients=").Append(this.messageRecipientsCount);
			stringBuilder.Append("]");
		}

		// Token: 0x0400012E RID: 302
		private readonly bool hasNamedProperties;

		// Token: 0x0400012F RID: 303
		private readonly bool useUnicode;

		// Token: 0x04000130 RID: 304
		private readonly string subjectPrefix;

		// Token: 0x04000131 RID: 305
		private readonly string normalizedSubject;

		// Token: 0x04000132 RID: 306
		private readonly ushort messageRecipientsCount;
	}
}
