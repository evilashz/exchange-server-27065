using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A2F RID: 2607
	[Serializable]
	public class MigrationObjectsCount
	{
		// Token: 0x06005FAA RID: 24490 RVA: 0x00194368 File Offset: 0x00192568
		internal MigrationObjectsCount(int? mailboxes)
		{
			this.Mailboxes = mailboxes;
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x00194377 File Offset: 0x00192577
		internal MigrationObjectsCount(int? mailboxes, int? groups, int? contacts, bool publicFodlers)
		{
			this.Mailboxes = mailboxes;
			this.Groups = groups;
			this.Contacts = contacts;
			this.PublicFolders = publicFodlers;
		}

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x06005FAC RID: 24492 RVA: 0x0019439C File Offset: 0x0019259C
		// (set) Token: 0x06005FAD RID: 24493 RVA: 0x001943A4 File Offset: 0x001925A4
		public int? Mailboxes { get; internal set; }

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06005FAE RID: 24494 RVA: 0x001943AD File Offset: 0x001925AD
		// (set) Token: 0x06005FAF RID: 24495 RVA: 0x001943B5 File Offset: 0x001925B5
		public int? Groups { get; internal set; }

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x001943BE File Offset: 0x001925BE
		// (set) Token: 0x06005FB1 RID: 24497 RVA: 0x001943C6 File Offset: 0x001925C6
		public int? Contacts { get; internal set; }

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x001943CF File Offset: 0x001925CF
		// (set) Token: 0x06005FB3 RID: 24499 RVA: 0x001943D7 File Offset: 0x001925D7
		public bool PublicFolders { get; internal set; }

		// Token: 0x06005FB4 RID: 24500 RVA: 0x001943E0 File Offset: 0x001925E0
		public static MigrationObjectsCount operator +(MigrationObjectsCount value1, MigrationObjectsCount value2)
		{
			return new MigrationObjectsCount(MigrationObjectsCount.Add(value1.Mailboxes, value2.Mailboxes), MigrationObjectsCount.Add(value1.Groups, value2.Groups), MigrationObjectsCount.Add(value1.Contacts, value2.Contacts), value1.PublicFolders || value2.PublicFolders);
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x00194438 File Offset: 0x00192638
		public static MigrationObjectsCount operator -(MigrationObjectsCount value1, MigrationObjectsCount value2)
		{
			return new MigrationObjectsCount(MigrationObjectsCount.Subtract(value1.Mailboxes, value2.Mailboxes), MigrationObjectsCount.Subtract(value1.Groups, value2.Groups), MigrationObjectsCount.Subtract(value1.Contacts, value2.Contacts), value1.PublicFolders || value2.PublicFolders);
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x00194490 File Offset: 0x00192690
		public override string ToString()
		{
			if (this.GetTotal() == 0)
			{
				return ServerStrings.MigrationObjectsCountStringNone;
			}
			string text = ServerStrings.MigrationObjectsCountStringMailboxes(((this.Mailboxes != null) ? this.Mailboxes.Value : 0).ToString(CultureInfo.InvariantCulture));
			if (this.Groups != null && this.Groups.Value > 0)
			{
				text += ServerStrings.MigrationObjectsCountStringGroups(this.Groups.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (this.Contacts != null && this.Contacts.Value > 0)
			{
				text += ServerStrings.MigrationObjectsCountStringContacts(this.Contacts.Value.ToString(CultureInfo.InvariantCulture));
			}
			if (this.PublicFolders)
			{
				text += ServerStrings.MigrationObjectsCountStringPFs;
			}
			return text;
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x001945A8 File Offset: 0x001927A8
		public override bool Equals(object obj)
		{
			MigrationObjectsCount migrationObjectsCount = obj as MigrationObjectsCount;
			return migrationObjectsCount != null && (this.Mailboxes == migrationObjectsCount.Mailboxes && this.Groups == migrationObjectsCount.Groups && this.Contacts == migrationObjectsCount.Contacts) && (!this.PublicFolders ^ !migrationObjectsCount.PublicFolders);
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x00194672 File Offset: 0x00192872
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x0019467C File Offset: 0x0019287C
		internal static MigrationObjectsCount Max(MigrationObjectsCount obj1, MigrationObjectsCount obj2)
		{
			return new MigrationObjectsCount(MigrationObjectsCount.Max(obj1.Mailboxes, obj2.Mailboxes), MigrationObjectsCount.Max(obj1.Groups, obj2.Groups), MigrationObjectsCount.Max(obj1.Contacts, obj2.Contacts), obj1.PublicFolders || obj2.PublicFolders);
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x001946D4 File Offset: 0x001928D4
		internal static MigrationObjectsCount FromValue(string value)
		{
			string[] array = value.Split(new string[]
			{
				":"
			}, StringSplitOptions.None);
			if (array.Length != 4)
			{
				throw new InvalidDataException("Invalid value : " + value, null);
			}
			return new MigrationObjectsCount(MigrationObjectsCount.FromString(array[0]), MigrationObjectsCount.FromString(array[1]), MigrationObjectsCount.FromString(array[2]), bool.Parse(array[3]));
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x00194738 File Offset: 0x00192938
		internal string ToValue()
		{
			string[] value = new string[]
			{
				MigrationObjectsCount.ToValue(this.Mailboxes),
				MigrationObjectsCount.ToValue(this.Groups),
				MigrationObjectsCount.ToValue(this.Contacts),
				this.PublicFolders.ToString()
			};
			return string.Join(":", value);
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x00194794 File Offset: 0x00192994
		internal int GetTotal()
		{
			int num = 0;
			if (this.Mailboxes != null && this.Mailboxes.Value > 0)
			{
				num += this.Mailboxes.Value;
			}
			if (this.Groups != null && this.Groups.Value > 0)
			{
				num += this.Groups.Value;
			}
			if (this.Contacts != null && this.Contacts.Value > 0)
			{
				num += this.Contacts.Value;
			}
			if (this.PublicFolders)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x0019484C File Offset: 0x00192A4C
		private static int? Max(int? int1, int? int2)
		{
			if (int1 != null || int2 != null)
			{
				return new int?(Math.Max((int1 != null) ? int1.Value : int.MinValue, (int2 != null) ? int2.Value : int.MinValue));
			}
			return null;
		}

		// Token: 0x06005FBE RID: 24510 RVA: 0x001948B0 File Offset: 0x00192AB0
		private static int? Add(int? int1, int? int2)
		{
			if (int1 == null && int2 == null)
			{
				return null;
			}
			return new int?(((int1 != null) ? int1.Value : 0) + ((int2 != null) ? int2.Value : 0));
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x00194908 File Offset: 0x00192B08
		private static int? Subtract(int? int1, int? int2)
		{
			if (int1 == null && int2 == null)
			{
				return null;
			}
			return new int?(((int1 != null) ? int1.Value : 0) - ((int2 != null) ? int2.Value : 0));
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x00194960 File Offset: 0x00192B60
		private static int? FromString(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return new int?(int.Parse(value));
			}
			return null;
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x0019498C File Offset: 0x00192B8C
		private static string ToValue(int? value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			return value.Value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04003637 RID: 13879
		private const string ValueSeparation = ":";
	}
}
