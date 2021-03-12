using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200082E RID: 2094
	[Serializable]
	public class SyncObjectId : ObjectId, IEquatable<SyncObjectId>
	{
		// Token: 0x060067F1 RID: 26609 RVA: 0x0016E760 File Offset: 0x0016C960
		public SyncObjectId(string contextId, string objectId, DirectoryObjectClass objectClass)
		{
			if (contextId == null)
			{
				throw new ArgumentNullException("contextId");
			}
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (objectClass == DirectoryObjectClass.Company && !contextId.Equals(objectId, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(DirectoryStrings.InvalidSyncCompanyId(string.Format("{0}_Company_{1}", contextId, objectId)), "objectId");
			}
			this.ContextId = contextId;
			this.ObjectId = objectId;
			this.ObjectClass = objectClass;
		}

		// Token: 0x170024CA RID: 9418
		// (get) Token: 0x060067F2 RID: 26610 RVA: 0x0016E7D3 File Offset: 0x0016C9D3
		// (set) Token: 0x060067F3 RID: 26611 RVA: 0x0016E7DB File Offset: 0x0016C9DB
		public string ContextId { get; private set; }

		// Token: 0x170024CB RID: 9419
		// (get) Token: 0x060067F4 RID: 26612 RVA: 0x0016E7E4 File Offset: 0x0016C9E4
		// (set) Token: 0x060067F5 RID: 26613 RVA: 0x0016E7EC File Offset: 0x0016C9EC
		public string ObjectId { get; private set; }

		// Token: 0x170024CC RID: 9420
		// (get) Token: 0x060067F6 RID: 26614 RVA: 0x0016E7F5 File Offset: 0x0016C9F5
		// (set) Token: 0x060067F7 RID: 26615 RVA: 0x0016E7FD File Offset: 0x0016C9FD
		public DirectoryObjectClass ObjectClass { get; private set; }

		// Token: 0x060067F8 RID: 26616 RVA: 0x0016E806 File Offset: 0x0016CA06
		public static bool operator ==(SyncObjectId left, SyncObjectId right)
		{
			if (object.ReferenceEquals(left, right))
			{
				return true;
			}
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x0016E822 File Offset: 0x0016CA22
		public static bool operator !=(SyncObjectId left, SyncObjectId right)
		{
			return !(left == right);
		}

		// Token: 0x060067FA RID: 26618 RVA: 0x0016E830 File Offset: 0x0016CA30
		public static SyncObjectId Parse(string identity)
		{
			string[] identityElements = SyncObjectId.GetIdentityElements(identity);
			if (identityElements.Length != 3)
			{
				throw new ArgumentException(DirectoryStrings.InvalidSyncObjectId(identity), "Identity");
			}
			string contextId = identityElements[0];
			DirectoryObjectClass objectClass = (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), identityElements[1]);
			string objectId = identityElements[2];
			return new SyncObjectId(contextId, objectId, objectClass);
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x0016E888 File Offset: 0x0016CA88
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SyncObjectId);
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x0016E898 File Offset: 0x0016CA98
		public bool Equals(SyncObjectId syncObjectId)
		{
			return !(syncObjectId == null) && (this.ObjectClass == syncObjectId.ObjectClass && StringComparer.OrdinalIgnoreCase.Equals(this.ObjectId, syncObjectId.ObjectId)) && StringComparer.OrdinalIgnoreCase.Equals(this.ContextId, syncObjectId.ContextId);
		}

		// Token: 0x060067FD RID: 26621 RVA: 0x0016E8F0 File Offset: 0x0016CAF0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", new object[]
			{
				this.ContextId,
				this.ObjectClass,
				this.ObjectId
			});
		}

		// Token: 0x060067FE RID: 26622 RVA: 0x0016E934 File Offset: 0x0016CB34
		public override int GetHashCode()
		{
			return this.ObjectClass.GetHashCode() ^ this.ObjectId.ToLowerInvariant().GetHashCode() ^ this.ContextId.ToLowerInvariant().GetHashCode();
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x0016E968 File Offset: 0x0016CB68
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.ToString());
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x0016E97C File Offset: 0x0016CB7C
		public DirectoryObjectIdentity ToMsoIdentity()
		{
			return new DirectoryObjectIdentity
			{
				ContextId = this.ContextId,
				ObjectId = this.ObjectId,
				ObjectClass = this.ObjectClass
			};
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x0016E9B4 File Offset: 0x0016CBB4
		internal static string[] GetIdentityElements(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			string[] array = identity.Split(new char[]
			{
				'_'
			});
			if (array.Length > 3)
			{
				throw new ArgumentException(DirectoryStrings.InvalidSyncObjectId(identity), "Identity");
			}
			return array;
		}
	}
}
