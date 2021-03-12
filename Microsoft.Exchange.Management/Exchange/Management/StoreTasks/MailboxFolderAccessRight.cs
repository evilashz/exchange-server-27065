using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007AA RID: 1962
	[Serializable]
	public struct MailboxFolderAccessRight : IEquatable<MailboxFolderAccessRight>, IComparable, IComparable<MailboxFolderAccessRight>
	{
		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x0011C32C File Offset: 0x0011A52C
		internal int Permission
		{
			get
			{
				return this.permission;
			}
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x0600452A RID: 17706 RVA: 0x0011C334 File Offset: 0x0011A534
		internal bool IsRole
		{
			get
			{
				return Enum.IsDefined(typeof(MailboxFolderPermissionRole), this.permission);
			}
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x0011C350 File Offset: 0x0011A550
		internal static Collection<MailboxFolderAccessRight> CreateMailboxFolderAccessRightCollection(int permission)
		{
			Collection<MailboxFolderAccessRight> collection = new Collection<MailboxFolderAccessRight>();
			int num = permission & 8187;
			MailboxFolderAccessRight? mailboxFolderAccessRight;
			if (MailboxFolderAccessRight.TryGetMailboxFolderPermissionRole(num, out mailboxFolderAccessRight))
			{
				collection.Add(mailboxFolderAccessRight.Value);
			}
			else
			{
				int num2 = permission & 2043;
				if (num != num2 && MailboxFolderAccessRight.TryGetMailboxFolderPermissionRole(num2, out mailboxFolderAccessRight))
				{
					collection.Add(mailboxFolderAccessRight.Value);
				}
				else if (Enum.IsDefined(typeof(MailboxFolderMemberRights), num2))
				{
					collection.Add(new MailboxFolderAccessRight(num2));
				}
				else
				{
					foreach (object obj in Enum.GetValues(typeof(MailboxFolderMemberRights)))
					{
						int num3 = (int)obj;
						if ((num3 & num2) != 0)
						{
							collection.Add(new MailboxFolderAccessRight(num3));
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x0011C43C File Offset: 0x0011A63C
		private static bool TryGetMailboxFolderPermissionRole(int permission, out MailboxFolderAccessRight? role)
		{
			role = null;
			if (Enum.IsDefined(typeof(MailboxFolderPermissionRole), permission))
			{
				role = new MailboxFolderAccessRight?(new MailboxFolderAccessRight(permission));
			}
			else if (MailboxFolderAccessRight.RoleOptionalMap.Value.ContainsKey(permission))
			{
				role = new MailboxFolderAccessRight?(new MailboxFolderAccessRight(MailboxFolderAccessRight.RoleOptionalMap.Value[permission]));
			}
			return role != null;
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x0011C4B4 File Offset: 0x0011A6B4
		internal static int CalculateMemberRights(IEnumerable<MailboxFolderAccessRight> accessRights, bool isCalendarFolder)
		{
			int num = 0;
			if (accessRights != null)
			{
				bool flag = isCalendarFolder && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.IncludeFBOnlyForCalendarContributor.Enabled;
				foreach (MailboxFolderAccessRight accessRight in accessRights)
				{
					if (isCalendarFolder && accessRight.IsRole && (int)accessRight != 2048 && (int)accessRight != 6144)
					{
						if (flag && (int)accessRight == 1026)
						{
							num |= ((int)accessRight | 2048);
						}
						else
						{
							num |= ((int)accessRight | 6144);
						}
					}
					else
					{
						num |= (int)accessRight;
					}
				}
			}
			return num;
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x0011C58C File Offset: 0x0011A78C
		public override string ToString()
		{
			if (!this.IsRole)
			{
				return ((MailboxFolderMemberRights)(this.permission & -6145)).ToString();
			}
			return ((MailboxFolderPermissionRole)this.permission).ToString();
		}

		// Token: 0x0600452F RID: 17711 RVA: 0x0011C5BD File Offset: 0x0011A7BD
		public static explicit operator int(MailboxFolderAccessRight accessRight)
		{
			return accessRight.Permission;
		}

		// Token: 0x06004530 RID: 17712 RVA: 0x0011C5C8 File Offset: 0x0011A7C8
		public MailboxFolderAccessRight(string accessRight)
		{
			if (accessRight == null)
			{
				throw new ArgumentNullException("accessRight");
			}
			try
			{
				this.permission = (int)Enum.Parse(typeof(MailboxFolderMemberRights), accessRight, true);
			}
			catch (ArgumentException)
			{
				this.permission = (int)((MailboxFolderPermissionRole)Enum.Parse(typeof(MailboxFolderPermissionRole), accessRight, true));
			}
			if (!Enum.IsDefined(typeof(MailboxFolderMemberRights), this.permission) && !Enum.IsDefined(typeof(MailboxFolderPermissionRole), this.permission))
			{
				throw new ArgumentOutOfRangeException(Strings.ErrorUnknownMailboxFolderAccessRights(accessRight));
			}
		}

		// Token: 0x06004531 RID: 17713 RVA: 0x0011C67C File Offset: 0x0011A87C
		public MailboxFolderAccessRight(MailboxFolderPermissionRole mailboxFolderPermissionRole)
		{
			this.permission = (int)mailboxFolderPermissionRole;
		}

		// Token: 0x06004532 RID: 17714 RVA: 0x0011C685 File Offset: 0x0011A885
		public static MailboxFolderAccessRight Parse(string accessRight)
		{
			return new MailboxFolderAccessRight(accessRight);
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x0011C690 File Offset: 0x0011A890
		internal MailboxFolderAccessRight(int permission)
		{
			if (!Enum.IsDefined(typeof(MailboxFolderMemberRights), permission) && !Enum.IsDefined(typeof(MailboxFolderPermissionRole), permission))
			{
				throw new ArgumentOutOfRangeException(Strings.ErrorUnknownMailboxFolderAccessRights(permission.ToString()));
			}
			this.permission = permission;
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x0011C6E9 File Offset: 0x0011A8E9
		public override int GetHashCode()
		{
			return this.permission.GetHashCode();
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x0011C6F6 File Offset: 0x0011A8F6
		public override bool Equals(object obj)
		{
			return obj is MailboxFolderAccessRight && this.permission == ((MailboxFolderAccessRight)obj).permission;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x0011C715 File Offset: 0x0011A915
		public bool Equals(MailboxFolderAccessRight other)
		{
			return this.permission == other.permission;
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x0011C726 File Offset: 0x0011A926
		public static bool operator ==(MailboxFolderAccessRight operand1, MailboxFolderAccessRight operand2)
		{
			return operand1.permission == operand2.permission;
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x0011C738 File Offset: 0x0011A938
		public static bool operator !=(MailboxFolderAccessRight operand1, MailboxFolderAccessRight operand2)
		{
			return operand1.permission != operand2.permission;
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x0011C74D File Offset: 0x0011A94D
		public int CompareTo(MailboxFolderAccessRight other)
		{
			return this.permission.CompareTo(other.permission);
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x0011C764 File Offset: 0x0011A964
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is MailboxFolderAccessRight))
			{
				throw new ArgumentException(Strings.ErrorArgumentIsOfWrongType(typeof(MailboxFolderAccessRight).ToString()), "obj");
			}
			return this.permission.CompareTo(((MailboxFolderAccessRight)obj).permission);
		}

		// Token: 0x04002AA7 RID: 10919
		private int permission;

		// Token: 0x04002AA8 RID: 10920
		private static LazilyInitialized<Dictionary<int, int>> RoleOptionalMap = new LazilyInitialized<Dictionary<int, int>>(() => new Dictionary<int, int>
		{
			{
				3072,
				2048
			},
			{
				2560,
				2048
			},
			{
				3584,
				2048
			},
			{
				7168,
				6144
			},
			{
				6656,
				6144
			},
			{
				7680,
				6144
			},
			{
				1531,
				2043
			},
			{
				1787,
				1275
			},
			{
				1659,
				1147
			},
			{
				1691,
				1179
			},
			{
				1563,
				1051
			},
			{
				1555,
				1043
			},
			{
				1537,
				1025
			},
			{
				1538,
				1026
			}
		});
	}
}
