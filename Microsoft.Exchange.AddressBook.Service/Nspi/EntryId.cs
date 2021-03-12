using System;
using System.Text;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.AddressBook.Nspi
{
	// Token: 0x02000025 RID: 37
	internal class EntryId
	{
		// Token: 0x06000148 RID: 328 RVA: 0x000068E2 File Offset: 0x00004AE2
		internal EntryId(EntryId.DisplayType displayType, string dn)
		{
			if (dn == null)
			{
				throw new ArgumentNullException("dn");
			}
			this.displayType = displayType;
			this.dn = dn;
			this.providerGuid = EntryId.ExchangeProviderGuid;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006911 File Offset: 0x00004B11
		internal EntryId(EntryId.DisplayType displayType, Guid providerGuid, int ephemeralId)
		{
			this.displayType = displayType;
			this.providerGuid = providerGuid;
			this.ephemeralId = ephemeralId;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000692E File Offset: 0x00004B2E
		internal string DistinguishedName
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006936 File Offset: 0x00004B36
		internal int EphemeralId
		{
			get
			{
				return this.ephemeralId;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000693E File Offset: 0x00004B3E
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00006946 File Offset: 0x00004B46
		internal Guid ProviderGuid
		{
			get
			{
				return this.providerGuid;
			}
			set
			{
				this.InvalidateBytes();
				this.providerGuid = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006955 File Offset: 0x00004B55
		internal bool IsEphemeral
		{
			get
			{
				return this.dn == null;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006960 File Offset: 0x00004B60
		internal static bool TryParse(byte[] buffer, out EntryId entryId)
		{
			entryId = null;
			if (buffer.Length < 28)
			{
				return false;
			}
			EntryId.EntryIdFlags entryIdFlags = (EntryId.EntryIdFlags)BitConverter.ToInt32(buffer, 0);
			Guid a = ExBitConverter.ReadGuid(buffer, 4);
			int num = BitConverter.ToInt32(buffer, 20);
			EntryId.DisplayType displayType = (EntryId.DisplayType)BitConverter.ToInt32(buffer, 24);
			if (num != 1)
			{
				return false;
			}
			if (entryIdFlags == EntryId.EntryIdFlags.Permanent)
			{
				if (buffer.Length < 29 || buffer[buffer.Length - 1] != 0 || a != EntryId.ExchangeProviderGuid)
				{
					return false;
				}
				string @string = Encoding.ASCII.GetString(buffer, 28, buffer.Length - 28 - 1);
				entryId = new EntryId(displayType, @string);
				return true;
			}
			else
			{
				if (entryIdFlags != EntryId.EntryIdFlags.Ephemeral)
				{
					return false;
				}
				if (buffer.Length != 32)
				{
					return false;
				}
				entryId = new EntryId(displayType, a, BitConverter.ToInt32(buffer, 28));
				entryId.bytes = buffer;
				return true;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006A14 File Offset: 0x00004C14
		internal byte[] ToByteArray()
		{
			if (this.bytes == null)
			{
				if (this.IsEphemeral)
				{
					int num = 32;
					this.bytes = new byte[num];
					this.WriteHeader(EntryId.EntryIdFlags.Ephemeral, this.bytes);
					ExBitConverter.Write(this.ephemeralId, this.bytes, 28);
				}
				else
				{
					byte[] array = Encoding.ASCII.GetBytes(this.dn);
					int num2 = 28 + array.Length + 1;
					this.bytes = new byte[num2];
					this.WriteHeader(EntryId.EntryIdFlags.Permanent, this.bytes);
					Array.Copy(array, 0, this.bytes, 28, array.Length);
				}
			}
			return this.bytes;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006AB8 File Offset: 0x00004CB8
		private void WriteHeader(EntryId.EntryIdFlags flags, byte[] buffer)
		{
			int num = 0;
			num += ExBitConverter.Write((int)flags, buffer, num);
			num += ExBitConverter.Write(this.providerGuid, buffer, num);
			num += ExBitConverter.Write(1, buffer, num);
			num += ExBitConverter.Write((int)this.displayType, buffer, num);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006AFD File Offset: 0x00004CFD
		private void InvalidateBytes()
		{
			this.bytes = null;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006B06 File Offset: 0x00004D06
		public override string ToString()
		{
			return string.Format("DisplayType: {0}, MID: {1}, DN: {2}", this.displayType, this.ephemeralId, this.dn ?? "(null)");
		}

		// Token: 0x040000C3 RID: 195
		private const int Version = 1;

		// Token: 0x040000C4 RID: 196
		private const int BaseSize = 28;

		// Token: 0x040000C5 RID: 197
		private const int EphemeralEntryIdSize = 32;

		// Token: 0x040000C6 RID: 198
		public static readonly Guid ExchangeProviderGuid = new Guid("c840a7dc-42c0-1a10-b4b9-08002b2fe182");

		// Token: 0x040000C7 RID: 199
		public static readonly byte[] ExchangeProviderGuidByteArray = EntryId.ExchangeProviderGuid.ToByteArray();

		// Token: 0x040000C8 RID: 200
		public static readonly EntryId DefaultGalEntryId = new EntryId(EntryId.DisplayType.AbContainer, "/");

		// Token: 0x040000C9 RID: 201
		private EntryId.DisplayType displayType;

		// Token: 0x040000CA RID: 202
		private string dn;

		// Token: 0x040000CB RID: 203
		private int ephemeralId;

		// Token: 0x040000CC RID: 204
		private byte[] bytes;

		// Token: 0x040000CD RID: 205
		private Guid providerGuid;

		// Token: 0x02000026 RID: 38
		internal enum DisplayType
		{
			// Token: 0x040000CF RID: 207
			MailUser,
			// Token: 0x040000D0 RID: 208
			DistList,
			// Token: 0x040000D1 RID: 209
			Forum,
			// Token: 0x040000D2 RID: 210
			Agent,
			// Token: 0x040000D3 RID: 211
			Organization,
			// Token: 0x040000D4 RID: 212
			RemoteMailUser = 6,
			// Token: 0x040000D5 RID: 213
			AbContainer = 256,
			// Token: 0x040000D6 RID: 214
			AbAddressTemplate = 258
		}

		// Token: 0x02000027 RID: 39
		private enum EntryIdFlags
		{
			// Token: 0x040000D8 RID: 216
			Permanent,
			// Token: 0x040000D9 RID: 217
			Ephemeral = 135
		}
	}
}
