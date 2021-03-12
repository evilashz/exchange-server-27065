using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.MapiTasks
{
	// Token: 0x02000CC8 RID: 3272
	[Serializable]
	public struct PublicFolderAccessRight : IEquatable<PublicFolderAccessRight>, IComparable, IComparable<PublicFolderAccessRight>
	{
		// Token: 0x17002737 RID: 10039
		// (get) Token: 0x06007E16 RID: 32278 RVA: 0x00203964 File Offset: 0x00201B64
		public PublicFolderPermission Permission
		{
			get
			{
				return this.permission;
			}
		}

		// Token: 0x17002738 RID: 10040
		// (get) Token: 0x06007E17 RID: 32279 RVA: 0x0020396C File Offset: 0x00201B6C
		public bool IsRole
		{
			get
			{
				return Enum.IsDefined(typeof(PublicFolderPermissionRole), (int)this.permission);
			}
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x00203988 File Offset: 0x00201B88
		public static MultiValuedProperty<PublicFolderAccessRight> CreatePublicFolderAccessRightCollection(PublicFolderPermission permission)
		{
			permission &= (PublicFolderPermission.ReadItems | PublicFolderPermission.CreateItems | PublicFolderPermission.EditOwnedItems | PublicFolderPermission.DeleteOwnedItems | PublicFolderPermission.EditAllItems | PublicFolderPermission.DeleteAllItems | PublicFolderPermission.CreateSubfolders | PublicFolderPermission.FolderOwner | PublicFolderPermission.FolderContact | PublicFolderPermission.FolderVisible);
			MultiValuedProperty<PublicFolderAccessRight> multiValuedProperty = new MultiValuedProperty<PublicFolderAccessRight>();
			if (Enum.IsDefined(typeof(PublicFolderPermission), permission) || Enum.IsDefined(typeof(PublicFolderPermissionRole), (int)permission))
			{
				multiValuedProperty.Add(new PublicFolderAccessRight(permission));
			}
			else
			{
				int num = (int)permission;
				int num2 = 1;
				int num3 = 0;
				while (24 > num3)
				{
					if ((num2 & num) != 0 && Enum.IsDefined(typeof(PublicFolderPermission), num2))
					{
						multiValuedProperty.Add(new PublicFolderAccessRight((PublicFolderPermission)num2));
					}
					num2 <<= 1;
					num3++;
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x00203A20 File Offset: 0x00201C20
		public static PublicFolderPermission CalculatePublicFolderPermission(ICollection<PublicFolderAccessRight> accessRights)
		{
			int num = 0;
			if (accessRights != null)
			{
				foreach (PublicFolderAccessRight accessRight in accessRights)
				{
					num |= (int)accessRight;
				}
			}
			return (PublicFolderPermission)num;
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x00203A70 File Offset: 0x00201C70
		public override string ToString()
		{
			if (!this.IsRole)
			{
				return this.permission.ToString();
			}
			return ((PublicFolderPermissionRole)this.permission).ToString();
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x00203A9B File Offset: 0x00201C9B
		public static explicit operator int(PublicFolderAccessRight accessRight)
		{
			return (int)accessRight.Permission;
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x00203AA4 File Offset: 0x00201CA4
		public PublicFolderAccessRight(string accessRight)
		{
			if (accessRight == null)
			{
				throw new ArgumentNullException("accessRight");
			}
			try
			{
				this.permission = (PublicFolderPermission)Enum.Parse(typeof(PublicFolderPermission), accessRight, true);
			}
			catch (ArgumentException)
			{
				this.permission = (PublicFolderPermission)((PublicFolderPermissionRole)Enum.Parse(typeof(PublicFolderPermissionRole), accessRight, true));
			}
			if (!Enum.IsDefined(typeof(PublicFolderPermission), this.permission) && !Enum.IsDefined(typeof(PublicFolderPermissionRole), (int)this.permission))
			{
				throw new ArgumentOutOfRangeException(Strings.ErrorUnknownAccessRights(accessRight));
			}
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x00203B58 File Offset: 0x00201D58
		public static PublicFolderAccessRight Parse(string accessRight)
		{
			return new PublicFolderAccessRight(accessRight);
		}

		// Token: 0x06007E1E RID: 32286 RVA: 0x00203B60 File Offset: 0x00201D60
		public PublicFolderAccessRight(PublicFolderPermission permission)
		{
			if (!Enum.IsDefined(typeof(PublicFolderPermission), permission) && !Enum.IsDefined(typeof(PublicFolderPermissionRole), (int)permission))
			{
				throw new ArgumentOutOfRangeException(Strings.ErrorUnknownAccessRights(permission.ToString()));
			}
			this.permission = permission;
		}

		// Token: 0x06007E1F RID: 32287 RVA: 0x00203BBD File Offset: 0x00201DBD
		public override int GetHashCode()
		{
			return this.permission.GetHashCode();
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x00203BCF File Offset: 0x00201DCF
		public override bool Equals(object obj)
		{
			return obj is PublicFolderAccessRight && this.permission == ((PublicFolderAccessRight)obj).permission;
		}

		// Token: 0x06007E21 RID: 32289 RVA: 0x00203BEE File Offset: 0x00201DEE
		public bool Equals(PublicFolderAccessRight other)
		{
			return this.permission == other.permission;
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x00203BFF File Offset: 0x00201DFF
		public static bool operator ==(PublicFolderAccessRight operand1, PublicFolderAccessRight operand2)
		{
			return operand1.permission == operand2.permission;
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x00203C11 File Offset: 0x00201E11
		public static bool operator !=(PublicFolderAccessRight operand1, PublicFolderAccessRight operand2)
		{
			return operand1.permission != operand2.permission;
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x00203C26 File Offset: 0x00201E26
		public int CompareTo(PublicFolderAccessRight other)
		{
			return this.permission.CompareTo(other.permission);
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x00203C44 File Offset: 0x00201E44
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is PublicFolderAccessRight))
			{
				throw new ArgumentException(Strings.ErrorArgumentIsOfWrongType(typeof(PublicFolderAccessRight).ToString()), "obj");
			}
			return this.permission.CompareTo(((PublicFolderAccessRight)obj).permission);
		}

		// Token: 0x04003E19 RID: 15897
		private PublicFolderPermission permission;
	}
}
