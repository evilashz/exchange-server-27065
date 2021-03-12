using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000392 RID: 914
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Permission
	{
		// Token: 0x0600285E RID: 10334 RVA: 0x000A19CB File Offset: 0x0009FBCB
		internal Permission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights, long memberId) : this(securityPrincipal, memberRights, PermissionOrigin.Read)
		{
			this.memberId = new long?(memberId);
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000A19E2 File Offset: 0x0009FBE2
		public Permission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights) : this(securityPrincipal, memberRights, PermissionOrigin.New)
		{
			this.ValidateMemberRights(memberRights);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000A19F4 File Offset: 0x0009FBF4
		private Permission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights, PermissionOrigin origin)
		{
			this.securityPrincipal = securityPrincipal;
			this.initialMemberRights = memberRights;
			this.memberRights = memberRights;
			this.origin = origin;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x000A1A31 File Offset: 0x0009FC31
		public PermissionSecurityPrincipal Principal
		{
			get
			{
				return this.securityPrincipal;
			}
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000A1A39 File Offset: 0x0009FC39
		public void CopyFrom(Permission source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.memberRights = source.memberRights;
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002863 RID: 10339 RVA: 0x000A1A55 File Offset: 0x0009FC55
		// (set) Token: 0x06002864 RID: 10340 RVA: 0x000A1A62 File Offset: 0x0009FC62
		public PermissionLevel PermissionLevel
		{
			get
			{
				return Permission.GetPermissionLevel(this.memberRights);
			}
			set
			{
				this.memberRights = (Permission.GetMemberRights(value) | (this.memberRights & (MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed)));
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x000A1A7D File Offset: 0x0009FC7D
		// (set) Token: 0x06002866 RID: 10342 RVA: 0x000A1A86 File Offset: 0x0009FC86
		public bool CanCreateItems
		{
			get
			{
				return this.GetMemberRight(MemberRights.Create);
			}
			set
			{
				this.SetMemberRight(MemberRights.Create, value);
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x000A1A90 File Offset: 0x0009FC90
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x000A1A99 File Offset: 0x0009FC99
		public bool CanReadItems
		{
			get
			{
				return this.GetMemberRight(MemberRights.ReadAny);
			}
			set
			{
				this.SetMemberRight(MemberRights.ReadAny, value);
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x000A1AA3 File Offset: 0x0009FCA3
		// (set) Token: 0x0600286A RID: 10346 RVA: 0x000A1AB0 File Offset: 0x0009FCB0
		public bool CanCreateSubfolders
		{
			get
			{
				return this.GetMemberRight(MemberRights.CreateSubfolder);
			}
			set
			{
				this.SetMemberRight(MemberRights.CreateSubfolder, value);
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x000A1ABE File Offset: 0x0009FCBE
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x000A1ACB File Offset: 0x0009FCCB
		public bool IsFolderOwner
		{
			get
			{
				return this.GetMemberRight(MemberRights.Owner);
			}
			set
			{
				this.SetMemberRight(MemberRights.Owner, value);
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x000A1AD9 File Offset: 0x0009FCD9
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x000A1AE6 File Offset: 0x0009FCE6
		public bool IsFolderContact
		{
			get
			{
				return this.GetMemberRight(MemberRights.Contact);
			}
			set
			{
				this.SetMemberRight(MemberRights.Contact, value);
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x000A1AF4 File Offset: 0x0009FCF4
		// (set) Token: 0x06002870 RID: 10352 RVA: 0x000A1B01 File Offset: 0x0009FD01
		public bool IsFolderVisible
		{
			get
			{
				return this.GetMemberRight(MemberRights.Visible);
			}
			set
			{
				this.SetMemberRight(MemberRights.Visible, value);
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x000A1B0F File Offset: 0x0009FD0F
		// (set) Token: 0x06002872 RID: 10354 RVA: 0x000A1B1A File Offset: 0x0009FD1A
		public ItemPermissionScope EditItems
		{
			get
			{
				return this.GetPermissionScope(MemberRights.EditAny, MemberRights.EditOwned);
			}
			set
			{
				this.SetPermissionScope(MemberRights.EditAny, MemberRights.EditOwned, value);
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x000A1B26 File Offset: 0x0009FD26
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x000A1B32 File Offset: 0x0009FD32
		public ItemPermissionScope DeleteItems
		{
			get
			{
				return this.GetPermissionScope(MemberRights.DeleteAny, MemberRights.DeleteOwned);
			}
			set
			{
				this.SetPermissionScope(MemberRights.DeleteAny, MemberRights.DeleteOwned, value);
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000A1B3F File Offset: 0x0009FD3F
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x000A1B47 File Offset: 0x0009FD47
		public MemberRights MemberRights
		{
			get
			{
				return this.memberRights;
			}
			set
			{
				this.ValidateMemberRights(value);
				this.memberRights = value;
			}
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x000A1B58 File Offset: 0x0009FD58
		public static PermissionLevel GetPermissionLevel(MemberRights memberRights)
		{
			MemberRights memberRights2 = memberRights & ~(MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed);
			MemberRights memberRights3 = memberRights2;
			if (memberRights3 <= (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.Visible))
			{
				if (memberRights3 <= MemberRights.Contact)
				{
					if (memberRights3 != MemberRights.None && memberRights3 != MemberRights.Contact)
					{
						return PermissionLevel.Custom;
					}
				}
				else
				{
					switch (memberRights3)
					{
					case MemberRights.Visible:
						break;
					case MemberRights.ReadAny | MemberRights.Visible:
						return PermissionLevel.Reviewer;
					case MemberRights.Create | MemberRights.Visible:
						return PermissionLevel.Contributor;
					default:
						if (memberRights3 == (MemberRights.ReadAny | MemberRights.Create | MemberRights.DeleteOwned | MemberRights.Visible))
						{
							return PermissionLevel.NonEditingAuthor;
						}
						if (memberRights3 != (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.Visible))
						{
							return PermissionLevel.Custom;
						}
						return PermissionLevel.Author;
					}
				}
			}
			else
			{
				if (memberRights3 > (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Visible))
				{
					if (memberRights3 != (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Owner | MemberRights.Visible))
					{
						if (memberRights3 == (MemberRights.Contact | MemberRights.Visible))
						{
							goto IL_8E;
						}
						if (memberRights3 != (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Owner | MemberRights.Contact | MemberRights.Visible))
						{
							return PermissionLevel.Custom;
						}
					}
					return PermissionLevel.Owner;
				}
				if (memberRights3 == (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.Visible))
				{
					return PermissionLevel.Editor;
				}
				if (memberRights3 == (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.CreateSubfolder | MemberRights.Visible))
				{
					return PermissionLevel.PublishingAuthor;
				}
				if (memberRights3 != (MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Visible))
				{
					return PermissionLevel.Custom;
				}
				return PermissionLevel.PublishingEditor;
			}
			IL_8E:
			if (memberRights == memberRights2)
			{
				return PermissionLevel.None;
			}
			return PermissionLevel.Custom;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x000A1C10 File Offset: 0x0009FE10
		public static MemberRights GetMemberRights(PermissionLevel permissionLevel)
		{
			switch (permissionLevel)
			{
			case PermissionLevel.None:
				return MemberRights.None;
			case PermissionLevel.Owner:
				return MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Owner | MemberRights.Contact | MemberRights.Visible;
			case PermissionLevel.PublishingEditor:
				return MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Visible;
			case PermissionLevel.Editor:
				return MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.Visible;
			case PermissionLevel.PublishingAuthor:
				return MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.CreateSubfolder | MemberRights.Visible;
			case PermissionLevel.Author:
				return MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.Visible;
			case PermissionLevel.NonEditingAuthor:
				return MemberRights.ReadAny | MemberRights.Create | MemberRights.DeleteOwned | MemberRights.Visible;
			case PermissionLevel.Reviewer:
				return MemberRights.ReadAny | MemberRights.Visible;
			case PermissionLevel.Contributor:
				return MemberRights.Create | MemberRights.Visible;
			default:
				throw new EnumOutOfRangeException("PermissionLevel", ServerStrings.BadEnumValue(typeof(PermissionLevel)));
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x000A1C9B File Offset: 0x0009FE9B
		internal PermissionOrigin Origin
		{
			get
			{
				return this.origin;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x000A1CA3 File Offset: 0x0009FEA3
		internal bool IsDirty
		{
			get
			{
				return this.origin == PermissionOrigin.New || this.memberRights != this.initialMemberRights;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x000A1CC0 File Offset: 0x0009FEC0
		internal long MemberId
		{
			get
			{
				return this.memberId.Value;
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x000A1CDB File Offset: 0x0009FEDB
		internal void MarkAsNew()
		{
			this.origin = PermissionOrigin.New;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000A1CE4 File Offset: 0x0009FEE4
		protected virtual void ValidateMemberRights(MemberRights memberRights)
		{
			MemberRights memberRights2 = memberRights & ~Permission.ValidRegularFolderMemberRights;
			if (memberRights2 != MemberRights.None)
			{
				throw new EnumOutOfRangeException("memberRights");
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000A1D08 File Offset: 0x0009FF08
		private bool GetMemberRight(MemberRights memberRight)
		{
			return (this.memberRights & memberRight) == memberRight;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000A1D15 File Offset: 0x0009FF15
		private void SetMemberRight(MemberRights memberRight, bool value)
		{
			if (value)
			{
				this.memberRights |= memberRight;
				return;
			}
			this.memberRights &= ~memberRight;
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000A1D38 File Offset: 0x0009FF38
		private ItemPermissionScope GetPermissionScope(MemberRights anyScope, MemberRights ownerScope)
		{
			if ((this.memberRights & anyScope) == anyScope)
			{
				return ItemPermissionScope.AllItems;
			}
			if ((this.memberRights & ownerScope) == ownerScope)
			{
				return ItemPermissionScope.OwnedItems;
			}
			return ItemPermissionScope.None;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000A1D58 File Offset: 0x0009FF58
		private void SetPermissionScope(MemberRights anyScope, MemberRights ownerScope, ItemPermissionScope value)
		{
			EnumValidator.ThrowIfInvalid<ItemPermissionScope>(value, "value");
			switch (value)
			{
			case ItemPermissionScope.None:
				this.memberRights &= ~(anyScope | ownerScope);
				return;
			case ItemPermissionScope.OwnedItems:
				this.memberRights |= ownerScope;
				this.memberRights &= ~anyScope;
				return;
			case ItemPermissionScope.AllItems:
				this.memberRights |= (anyScope | ownerScope);
				return;
			default:
				return;
			}
		}

		// Token: 0x040017B5 RID: 6069
		private const MemberRights PermissionLevelNone = MemberRights.None;

		// Token: 0x040017B6 RID: 6070
		private const MemberRights PermissionLevelNoneOption1 = MemberRights.Contact;

		// Token: 0x040017B7 RID: 6071
		private const MemberRights PermissionLevelNoneOption2 = MemberRights.Visible;

		// Token: 0x040017B8 RID: 6072
		private const MemberRights PermissionLevelNoneOption3 = MemberRights.Contact | MemberRights.Visible;

		// Token: 0x040017B9 RID: 6073
		private const MemberRights OwnerRightsOption1 = MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Owner | MemberRights.Visible;

		// Token: 0x040017BA RID: 6074
		private const MemberRights OwnerRights = MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Owner | MemberRights.Contact | MemberRights.Visible;

		// Token: 0x040017BB RID: 6075
		private const MemberRights PublishingEditorRights = MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Visible;

		// Token: 0x040017BC RID: 6076
		private const MemberRights EditorRights = MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.Visible;

		// Token: 0x040017BD RID: 6077
		private const MemberRights PublishingAuthorRights = MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.CreateSubfolder | MemberRights.Visible;

		// Token: 0x040017BE RID: 6078
		private const MemberRights AuthorRights = MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.Visible;

		// Token: 0x040017BF RID: 6079
		private const MemberRights NonEditingAuthorRights = MemberRights.ReadAny | MemberRights.Create | MemberRights.DeleteOwned | MemberRights.Visible;

		// Token: 0x040017C0 RID: 6080
		private const MemberRights ReviewerRights = MemberRights.ReadAny | MemberRights.Visible;

		// Token: 0x040017C1 RID: 6081
		private const MemberRights ContributorRights = MemberRights.Create | MemberRights.Visible;

		// Token: 0x040017C2 RID: 6082
		private const MemberRights FreeBusyRights = MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed;

		// Token: 0x040017C3 RID: 6083
		private readonly PermissionSecurityPrincipal securityPrincipal;

		// Token: 0x040017C4 RID: 6084
		private MemberRights memberRights;

		// Token: 0x040017C5 RID: 6085
		private readonly long? memberId = null;

		// Token: 0x040017C6 RID: 6086
		private readonly MemberRights initialMemberRights;

		// Token: 0x040017C7 RID: 6087
		private PermissionOrigin origin;

		// Token: 0x040017C8 RID: 6088
		private static readonly MemberRights ValidRegularFolderMemberRights = MemberRights.ReadAny | MemberRights.Create | MemberRights.EditOwned | MemberRights.DeleteOwned | MemberRights.EditAny | MemberRights.DeleteAny | MemberRights.CreateSubfolder | MemberRights.Owner | MemberRights.Contact | MemberRights.Visible;
	}
}
