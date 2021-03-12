using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200001F RID: 31
	internal class MdbItemIdentity : IIdentity, IEquatable<IIdentity>, IEquatable<MdbItemIdentity>
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00006D28 File Offset: 0x00004F28
		internal MdbItemIdentity(byte[] persistableTenantId, Guid mdbGuid, Guid mailboxGuid, int mailboxNumber, StoreObjectId itemId, int documentId, bool isPublicFolder) : this(1, persistableTenantId, mdbGuid, mailboxGuid, mailboxNumber, itemId, documentId, isPublicFolder)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006D48 File Offset: 0x00004F48
		private MdbItemIdentity(int version, byte[] persistableTenantId, Guid mdbGuid, Guid mailboxGuid, int mailboxNumber, StoreObjectId itemId, int documentId, bool isPublicFolder)
		{
			Util.ThrowOnNullArgument(itemId, "itemId");
			this.Version = version;
			this.PersistableTenantId = persistableTenantId;
			this.fastMdbGuid = mdbGuid;
			this.MailboxGuid = mailboxGuid;
			this.MailboxNumber = mailboxNumber;
			this.ItemId = itemId;
			this.DocumentId = documentId;
			this.IsPublicFolder = isPublicFolder;
			this.RemapIdsForCatalogRestoreScenario();
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00006DAA File Offset: 0x00004FAA
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00006DB2 File Offset: 0x00004FB2
		internal int Version { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00006DBB File Offset: 0x00004FBB
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00006DC3 File Offset: 0x00004FC3
		internal byte[] PersistableTenantId { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00006DCC File Offset: 0x00004FCC
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00006DD4 File Offset: 0x00004FD4
		internal Guid MailboxGuid { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006DDD File Offset: 0x00004FDD
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00006DE5 File Offset: 0x00004FE5
		internal int MailboxNumber { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006DEE File Offset: 0x00004FEE
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00006DF6 File Offset: 0x00004FF6
		internal StoreObjectId ItemId { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006DFF File Offset: 0x00004FFF
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00006E07 File Offset: 0x00005007
		internal int DocumentId { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00006E10 File Offset: 0x00005010
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00006E18 File Offset: 0x00005018
		internal bool IsPublicFolder { get; private set; }

		// Token: 0x060000DE RID: 222 RVA: 0x00006E24 File Offset: 0x00005024
		public static MdbItemIdentity Parse(string mdbItemIdentity)
		{
			Util.ThrowOnNullOrEmptyArgument(mdbItemIdentity, "mdbItemIdentity");
			string[] array = mdbItemIdentity.Split(new char[]
			{
				','
			});
			int num = 0;
			if (array[0].Length < 11)
			{
				num = int.Parse(array[0]);
			}
			if (num == 0)
			{
				string text = array[3];
				return new MdbItemIdentity(1, null, new Guid(array[0]), new Guid(array[1]), int.Parse(array[2]), text.Equals(MdbItemIdentity.DummyStoreObjectId) ? StoreObjectId.DummyId : StoreObjectId.Deserialize(text), int.Parse(array[4]), false);
			}
			if (num == 1)
			{
				string text2 = array[5];
				return new MdbItemIdentity(num, Convert.FromBase64String(array[1]), new Guid(array[2]), new Guid(array[3]), int.Parse(array[4]), text2.Equals(MdbItemIdentity.DummyStoreObjectId) ? StoreObjectId.DummyId : StoreObjectId.Deserialize(text2), int.Parse(array[6]), bool.Parse(array[7]));
			}
			throw new ArgumentException(string.Format("MdbItemIdentity is not a supported version.  Version: {0}", num));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006F22 File Offset: 0x00005122
		public Guid GetMdbGuid(MdbItemIdentity.Location location)
		{
			if (location == MdbItemIdentity.Location.ExchangeMdb)
			{
				return this.exchangeMdbGuid;
			}
			return this.fastMdbGuid;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006F35 File Offset: 0x00005135
		public override bool Equals(object other)
		{
			return this.Equals(other as IIdentity);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006F43 File Offset: 0x00005143
		public virtual bool Equals(IIdentity other)
		{
			return this.Equals(other as MdbItemIdentity);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006F54 File Offset: 0x00005154
		public override int GetHashCode()
		{
			return this.MailboxGuid.GetHashCode() ^ this.DocumentId;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006F7C File Offset: 0x0000517C
		public override string ToString()
		{
			if (this.stringizedId == null)
			{
				this.stringizedId = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}", new object[]
				{
					',',
					1,
					(this.PersistableTenantId == null) ? string.Empty : Convert.ToBase64String(this.PersistableTenantId),
					this.fastMdbGuid,
					this.MailboxGuid,
					this.MailboxNumber,
					this.ItemId,
					this.DocumentId,
					this.IsPublicFolder
				});
			}
			return this.stringizedId;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007034 File Offset: 0x00005234
		public bool Equals(MdbItemIdentity other)
		{
			return other != null && (object.ReferenceEquals(other, this) || (this.fastMdbGuid.Equals(other.fastMdbGuid) && this.MailboxGuid.Equals(other.MailboxGuid) && this.DocumentId.Equals(other.DocumentId)));
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007093 File Offset: 0x00005293
		private void RemapIdsForCatalogRestoreScenario()
		{
			this.exchangeMdbGuid = this.fastMdbGuid;
			if (CatalogRestoreHelper.IsEnabled)
			{
				this.exchangeMdbGuid = CatalogRestoreHelper.GetExchangeMapping(this.fastMdbGuid);
				this.fastMdbGuid = CatalogRestoreHelper.GetFastMapping(this.exchangeMdbGuid);
			}
		}

		// Token: 0x04000086 RID: 134
		private const int CurrentVersion = 1;

		// Token: 0x04000087 RID: 135
		private const char SeparatorChar = ',';

		// Token: 0x04000088 RID: 136
		private static readonly string DummyStoreObjectId = StoreObjectId.DummyId.ToString();

		// Token: 0x04000089 RID: 137
		private string stringizedId;

		// Token: 0x0400008A RID: 138
		private Guid exchangeMdbGuid;

		// Token: 0x0400008B RID: 139
		private Guid fastMdbGuid;

		// Token: 0x02000020 RID: 32
		public enum Location
		{
			// Token: 0x04000094 RID: 148
			FastCatalog,
			// Token: 0x04000095 RID: 149
			ExchangeMdb
		}

		// Token: 0x02000021 RID: 33
		private enum Segments
		{
			// Token: 0x04000097 RID: 151
			MdbGuid,
			// Token: 0x04000098 RID: 152
			MailboxGuid,
			// Token: 0x04000099 RID: 153
			MailboxNumber,
			// Token: 0x0400009A RID: 154
			ItemId,
			// Token: 0x0400009B RID: 155
			DocumentId,
			// Token: 0x0400009C RID: 156
			Size
		}

		// Token: 0x02000022 RID: 34
		private enum Version1Segments
		{
			// Token: 0x0400009E RID: 158
			Version,
			// Token: 0x0400009F RID: 159
			PersistableTenantId,
			// Token: 0x040000A0 RID: 160
			MdbGuid,
			// Token: 0x040000A1 RID: 161
			MailboxGuid,
			// Token: 0x040000A2 RID: 162
			MailboxNumber,
			// Token: 0x040000A3 RID: 163
			ItemId,
			// Token: 0x040000A4 RID: 164
			DocumentId,
			// Token: 0x040000A5 RID: 165
			IsPublicFolder,
			// Token: 0x040000A6 RID: 166
			Size
		}
	}
}
