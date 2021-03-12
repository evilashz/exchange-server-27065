using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DA1 RID: 3489
	[Serializable]
	public class StoreIntegrityCheckJobIdentity : ObjectId, IEquatable<StoreIntegrityCheckJobIdentity>
	{
		// Token: 0x060085B7 RID: 34231 RVA: 0x002235E3 File Offset: 0x002217E3
		public StoreIntegrityCheckJobIdentity()
		{
		}

		// Token: 0x060085B8 RID: 34232 RVA: 0x002235EC File Offset: 0x002217EC
		public StoreIntegrityCheckJobIdentity(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			string[] array = identity.Split(StoreIntegrityCheckJobIdentity.Separator, StringSplitOptions.None);
			if (array == null || array.Length < 2 || array.Length > 3)
			{
				throw new InvalidIntegrityCheckJobIdentity(identity);
			}
			if (!Guid.TryParse(array[0], out this.databaseGuid) || !Guid.TryParse(array[1], out this.requestGuid))
			{
				throw new InvalidIntegrityCheckJobIdentity(identity);
			}
			if (array.Length == 3 && !Guid.TryParse(array[2], out this.jobGuid))
			{
				throw new InvalidIntegrityCheckJobIdentity(identity);
			}
		}

		// Token: 0x060085B9 RID: 34233 RVA: 0x00223674 File Offset: 0x00221874
		public StoreIntegrityCheckJobIdentity(byte[] identity) : this(Encoding.UTF8.GetString(identity))
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
		}

		// Token: 0x060085BA RID: 34234 RVA: 0x00223695 File Offset: 0x00221895
		public StoreIntegrityCheckJobIdentity(Guid databaseGuid, Guid requestGuid)
		{
			this.databaseGuid = databaseGuid;
			this.requestGuid = requestGuid;
		}

		// Token: 0x060085BB RID: 34235 RVA: 0x002236AB File Offset: 0x002218AB
		public StoreIntegrityCheckJobIdentity(Guid databaseGuid, Guid requestGuid, Guid jobGuid)
		{
			this.databaseGuid = databaseGuid;
			this.requestGuid = requestGuid;
			this.jobGuid = jobGuid;
		}

		// Token: 0x1700299F RID: 10655
		// (get) Token: 0x060085BC RID: 34236 RVA: 0x002236C8 File Offset: 0x002218C8
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x170029A0 RID: 10656
		// (get) Token: 0x060085BD RID: 34237 RVA: 0x002236D0 File Offset: 0x002218D0
		public Guid RequestGuid
		{
			get
			{
				return this.requestGuid;
			}
		}

		// Token: 0x170029A1 RID: 10657
		// (get) Token: 0x060085BE RID: 34238 RVA: 0x002236D8 File Offset: 0x002218D8
		public Guid JobGuid
		{
			get
			{
				return this.jobGuid;
			}
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x002236E0 File Offset: 0x002218E0
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.ToString());
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x002236F4 File Offset: 0x002218F4
		public override string ToString()
		{
			if (!(this.databaseGuid != Guid.Empty) || !(this.requestGuid != Guid.Empty))
			{
				return string.Empty;
			}
			if (this.jobGuid != Guid.Empty)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}{4}", new object[]
				{
					this.databaseGuid,
					StoreIntegrityCheckJobIdentity.Separator[0],
					this.requestGuid,
					StoreIntegrityCheckJobIdentity.Separator[0],
					this.jobGuid
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				this.databaseGuid,
				StoreIntegrityCheckJobIdentity.Separator[0],
				this.requestGuid
			});
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x002237E5 File Offset: 0x002219E5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as StoreIntegrityCheckJobIdentity);
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x002237F4 File Offset: 0x002219F4
		public bool Equals(StoreIntegrityCheckJobIdentity other)
		{
			return object.ReferenceEquals(this, other) || (other != null && !(this.jobGuid != other.jobGuid) && !(this.requestGuid != other.requestGuid) && !(this.databaseGuid != other.databaseGuid));
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x0022384C File Offset: 0x00221A4C
		public override int GetHashCode()
		{
			if (this.jobGuid != Guid.Empty)
			{
				return this.jobGuid.GetHashCode();
			}
			if (this.requestGuid != Guid.Empty)
			{
				return this.requestGuid.GetHashCode();
			}
			return 0;
		}

		// Token: 0x040040E3 RID: 16611
		private static char[] Separator = new char[]
		{
			'\\'
		};

		// Token: 0x040040E4 RID: 16612
		private readonly Guid databaseGuid;

		// Token: 0x040040E5 RID: 16613
		private readonly Guid requestGuid;

		// Token: 0x040040E6 RID: 16614
		private readonly Guid jobGuid;
	}
}
