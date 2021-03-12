using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D25 RID: 3365
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class SearchObjectId : MailboxStoreIdentity, IEquatable<SearchObjectId>
	{
		// Token: 0x06007427 RID: 29735 RVA: 0x00203CCA File Offset: 0x00201ECA
		public SearchObjectId(ADObjectId mailboxOwnerId, ObjectType objectType, Guid guid) : base(mailboxOwnerId)
		{
			this.objectType = objectType;
			this.guid = guid;
		}

		// Token: 0x06007428 RID: 29736 RVA: 0x00203CE1 File Offset: 0x00201EE1
		public SearchObjectId(SearchObjectId identity, ObjectType objectType) : this(identity.MailboxOwnerId, objectType, identity.Guid)
		{
		}

		// Token: 0x06007429 RID: 29737 RVA: 0x00203CF6 File Offset: 0x00201EF6
		public SearchObjectId(ADObjectId mailboxOwnerId, ObjectType objectType) : this(mailboxOwnerId, objectType, Guid.Empty)
		{
		}

		// Token: 0x0600742A RID: 29738 RVA: 0x00203D05 File Offset: 0x00201F05
		public SearchObjectId(ADObjectId mailboxOwnerId) : this(mailboxOwnerId, ObjectType.SearchObject, Guid.Empty)
		{
		}

		// Token: 0x0600742B RID: 29739 RVA: 0x00203D14 File Offset: 0x00201F14
		public SearchObjectId() : this(null, ObjectType.SearchObject, Guid.Empty)
		{
		}

		// Token: 0x0600742C RID: 29740 RVA: 0x00203D24 File Offset: 0x00201F24
		private SearchObjectId(SerializationInfo info, StreamingContext context)
		{
			this.ObjectType = (ObjectType)info.GetValue("ObjectType", typeof(ObjectType));
			this.Guid = (Guid)info.GetValue("Guid", typeof(Guid));
		}

		// Token: 0x0600742D RID: 29741 RVA: 0x00203D90 File Offset: 0x00201F90
		public static bool TryParse(string input, out SearchObjectId instance)
		{
			instance = null;
			if (string.IsNullOrEmpty(input) || input.Length < 6)
			{
				return false;
			}
			string rawType = null;
			int num = input.IndexOf('\\');
			string g;
			if (num != -1)
			{
				rawType = input.Substring(0, num);
				g = input.Substring(num + 1);
			}
			else
			{
				g = input;
			}
			ObjectType objectType = ObjectType.SearchObject;
			if (!string.IsNullOrEmpty(rawType))
			{
				string text = Enum.GetNames(typeof(ObjectType)).SingleOrDefault((string x) => x.Equals(rawType, StringComparison.OrdinalIgnoreCase));
				if (text == null)
				{
					return false;
				}
				objectType = (ObjectType)Enum.Parse(typeof(ObjectType), text, true);
			}
			Guid guid;
			if (GuidHelper.TryParseGuid(g, out guid))
			{
				instance = new SearchObjectId(null, objectType, guid);
				return true;
			}
			return false;
		}

		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x0600742E RID: 29742 RVA: 0x00203E5F File Offset: 0x0020205F
		// (set) Token: 0x0600742F RID: 29743 RVA: 0x00203E67 File Offset: 0x00202067
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
			internal set
			{
				this.guid = value;
			}
		}

		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x06007430 RID: 29744 RVA: 0x00203E70 File Offset: 0x00202070
		// (set) Token: 0x06007431 RID: 29745 RVA: 0x00203E78 File Offset: 0x00202078
		public ObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
			internal set
			{
				this.objectType = value;
			}
		}

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x06007432 RID: 29746 RVA: 0x00203E84 File Offset: 0x00202084
		public string ConfigurationName
		{
			get
			{
				return this.ObjectType.ToString() + "." + (this.Guid.Equals(Guid.Empty) ? string.Empty : this.Guid.ToString().Replace("-", string.Empty));
			}
		}

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x06007433 RID: 29747 RVA: 0x00203EEC File Offset: 0x002020EC
		public string DistinguishedName
		{
			get
			{
				return string.Concat(new string[]
				{
					base.MailboxOwnerId.DistinguishedName,
					"/",
					this.ObjectType.ToString(),
					"/",
					this.Guid.ToString()
				});
			}
		}

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x06007434 RID: 29748 RVA: 0x00203F50 File Offset: 0x00202150
		public bool IsEmpty
		{
			get
			{
				return this.Guid.Equals(Guid.Empty);
			}
		}

		// Token: 0x06007435 RID: 29749 RVA: 0x00203F70 File Offset: 0x00202170
		public override byte[] GetBytes()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007436 RID: 29750 RVA: 0x00203F78 File Offset: 0x00202178
		public override string ToString()
		{
			return this.ObjectType.ToString() + "\\" + this.Guid.ToString();
		}

		// Token: 0x06007437 RID: 29751 RVA: 0x00203FB3 File Offset: 0x002021B3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SearchObjectId);
		}

		// Token: 0x06007438 RID: 29752 RVA: 0x00203FC4 File Offset: 0x002021C4
		public override int GetHashCode()
		{
			return this.ObjectType.GetHashCode() ^ this.Guid.GetHashCode();
		}

		// Token: 0x06007439 RID: 29753 RVA: 0x00203FF6 File Offset: 0x002021F6
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ObjectType", this.ObjectType);
			info.AddValue("Guid", this.Guid);
		}

		// Token: 0x0600743A RID: 29754 RVA: 0x0020402C File Offset: 0x0020222C
		public bool Equals(SearchObjectId other)
		{
			return this.ObjectType == other.ObjectType && this.Guid.Equals(other.Guid);
		}

		// Token: 0x0400512E RID: 20782
		private ObjectType objectType;

		// Token: 0x0400512F RID: 20783
		private Guid guid;

		// Token: 0x02000D26 RID: 3366
		private static class SerializationMemberNames
		{
			// Token: 0x04005130 RID: 20784
			public const string ObjectType = "ObjectType";

			// Token: 0x04005131 RID: 20785
			public const string Guid = "Guid";
		}
	}
}
