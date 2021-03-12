using System;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public struct SmtpAddress : IEquatable<SmtpAddress>, IComparable<SmtpAddress>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000621E File Offset: 0x0000441E
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00006226 File Offset: 0x00004426
		internal string Local { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000622F File Offset: 0x0000442F
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00006237 File Offset: 0x00004437
		internal string Domain { get; set; }

		// Token: 0x060000EF RID: 239 RVA: 0x00006240 File Offset: 0x00004440
		public static bool operator ==(SmtpAddress value1, SmtpAddress value2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006247 File Offset: 0x00004447
		public static bool operator !=(SmtpAddress value1, SmtpAddress value2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000624E File Offset: 0x0000444E
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006255 File Offset: 0x00004455
		public override bool Equals(object address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000625C File Offset: 0x0000445C
		public bool Equals(SmtpAddress address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006263 File Offset: 0x00004463
		public int CompareTo(SmtpAddress address)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000B5 RID: 181
		internal static readonly SmtpAddress Empty = default(SmtpAddress);
	}
}
