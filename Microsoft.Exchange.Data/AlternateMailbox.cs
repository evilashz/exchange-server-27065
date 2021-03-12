using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class AlternateMailbox
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000BA30 File Offset: 0x00009C30
		public AlternateMailbox(Guid identity, Guid databaseGuid, AlternateMailbox.AlternateMailboxFlags flags, string name, IList<SmtpAddress> smtpAddresses, string userName)
		{
			if (!AlternateMailbox.IsValidName(name))
			{
				throw new ArgumentException(name, "name");
			}
			if (!AlternateMailbox.IsValidUserName(userName))
			{
				throw new ArgumentException(userName, "userName");
			}
			this.identity = identity;
			this.databaseGuid = databaseGuid;
			this.flags = flags;
			this.name = name;
			if (smtpAddresses != null)
			{
				this.smtpAddresses = new List<SmtpAddress>(smtpAddresses);
			}
			else
			{
				this.smtpAddresses = new List<SmtpAddress>(0);
			}
			this.userName = (userName ?? string.Empty);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000BABC File Offset: 0x00009CBC
		private AlternateMailbox()
		{
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public Guid Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000BACC File Offset: 0x00009CCC
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
			internal set
			{
				this.databaseGuid = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000BADD File Offset: 0x00009CDD
		public AlternateMailbox.AlternateMailboxFlags Type
		{
			get
			{
				if ((this.flags & AlternateMailbox.AlternateMailboxFlags.Archive) == AlternateMailbox.AlternateMailboxFlags.Archive)
				{
					return AlternateMailbox.AlternateMailboxFlags.Archive;
				}
				if ((this.flags & AlternateMailbox.AlternateMailboxFlags.Subscription) == AlternateMailbox.AlternateMailboxFlags.Subscription)
				{
					return AlternateMailbox.AlternateMailboxFlags.Subscription;
				}
				return AlternateMailbox.AlternateMailboxFlags.Unknown;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000BAFA File Offset: 0x00009CFA
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000BB12 File Offset: 0x00009D12
		public bool RetentionPolicyEnabled
		{
			get
			{
				return (this.flags & AlternateMailbox.AlternateMailboxFlags.RetentionPolicyEnabled) == AlternateMailbox.AlternateMailboxFlags.RetentionPolicyEnabled;
			}
			internal set
			{
				this.flags = (value ? (this.flags | AlternateMailbox.AlternateMailboxFlags.RetentionPolicyEnabled) : (this.flags & ~AlternateMailbox.AlternateMailboxFlags.RetentionPolicyEnabled));
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000BB37 File Offset: 0x00009D37
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000BB3F File Offset: 0x00009D3F
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000BB48 File Offset: 0x00009D48
		public IList<SmtpAddress> SmtpAddresses
		{
			get
			{
				return new ReadOnlyCollection<SmtpAddress>(this.smtpAddresses);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BB55 File Offset: 0x00009D55
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000BB5D File Offset: 0x00009D5D
		public string UserName
		{
			get
			{
				return this.userName;
			}
			internal set
			{
				this.userName = value;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BB68 File Offset: 0x00009D68
		public bool Equals(AlternateMailbox value)
		{
			return object.ReferenceEquals(this, value) || (value != null && this.identity == value.identity && this.databaseGuid == value.databaseGuid && this.flags == value.flags && this.name == value.name && this.userName == value.userName);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public override bool Equals(object comparand)
		{
			AlternateMailbox alternateMailbox = comparand as AlternateMailbox;
			return alternateMailbox != null && this.Equals(alternateMailbox);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BC00 File Offset: 0x00009E00
		public static bool operator ==(AlternateMailbox left, AlternateMailbox right)
		{
			if (left != null)
			{
				return left.Equals(right);
			}
			return right == null;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000BC11 File Offset: 0x00009E11
		public static bool operator !=(AlternateMailbox left, AlternateMailbox right)
		{
			return !(left == right);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BC1D File Offset: 0x00009E1D
		public override int GetHashCode()
		{
			return this.identity.GetHashCode();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BC30 File Offset: 0x00009E30
		public override string ToString()
		{
			string separator = ';'.ToString();
			string[] array = new string[7];
			array[0] = this.identity.ToString();
			array[1] = "1.0";
			array[2] = this.databaseGuid.ToString();
			string[] array2 = array;
			int num = 3;
			int num2 = (int)this.flags;
			array2[num] = num2.ToString();
			array[4] = ((this.name == null) ? string.Empty : this.name.ToString());
			array[5] = AlternateMailbox.GetEmailAddressesString(this.smtpAddresses);
			array[6] = ((this.userName == null) ? string.Empty : this.userName.ToString());
			string text = string.Join(separator, array);
			if (this.unknownProperties != null)
			{
				text += this.unknownProperties;
			}
			return text;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		public static AlternateMailbox Parse(string blob)
		{
			AlternateMailbox result;
			if (!AlternateMailbox.TryParse(blob, out result))
			{
				throw new ArgumentException(DataStrings.InvalidAlternateMailboxString(blob, ';'), "blob");
			}
			return result;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000BD24 File Offset: 0x00009F24
		public static bool TryParse(string blob, out AlternateMailbox alternateMailbox)
		{
			alternateMailbox = new AlternateMailbox();
			return AlternateMailbox.TryParse(blob, ref alternateMailbox.identity, ref alternateMailbox.databaseGuid, ref alternateMailbox.flags, ref alternateMailbox.name, ref alternateMailbox.smtpAddresses, ref alternateMailbox.userName, ref alternateMailbox.unknownProperties);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BD64 File Offset: 0x00009F64
		private static bool TryParse(string blob, ref Guid identity, ref Guid databaseGuid, ref AlternateMailbox.AlternateMailboxFlags flags, ref string name, ref List<SmtpAddress> smtpAddresses, ref string userName, ref string unknownProperties)
		{
			if (string.IsNullOrEmpty(blob))
			{
				return false;
			}
			string[] array = blob.Split(new char[]
			{
				';'
			});
			if (array == null || array.Length < 7)
			{
				return false;
			}
			int i = 0;
			identity = new Guid(array[i++]);
			if (array[i++] != "1.0")
			{
				return false;
			}
			databaseGuid = new Guid(array[i++]);
			flags = (AlternateMailbox.AlternateMailboxFlags)int.Parse(array[i++]);
			name = array[i++];
			smtpAddresses = AlternateMailbox.ParseEmailAddressesString(array[i++]);
			userName = array[i++];
			while (i < array.Length)
			{
				unknownProperties = string.Join(';'.ToString(), new string[]
				{
					unknownProperties,
					array[i]
				});
				i++;
			}
			return true;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BE40 File Offset: 0x0000A040
		private static List<SmtpAddress> ParseEmailAddressesString(string blob)
		{
			string[] array = blob.Split(new char[]
			{
				','
			});
			List<SmtpAddress> list = new List<SmtpAddress>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(new SmtpAddress(array[i]));
			}
			return list;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000BE88 File Offset: 0x0000A088
		private static string GetEmailAddressesString(List<SmtpAddress> smtpAddresses)
		{
			if (smtpAddresses == null || smtpAddresses.Count == 0)
			{
				return string.Empty;
			}
			string[] array = new string[smtpAddresses.Count];
			for (int i = 0; i < smtpAddresses.Count; i++)
			{
				array[i] = smtpAddresses[i].ToString();
			}
			return string.Join(','.ToString(), array);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000BEEB File Offset: 0x0000A0EB
		internal static bool IsValidName(string name)
		{
			return name != null && name.IndexOf(';') < 0;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000BEFD File Offset: 0x0000A0FD
		internal static bool IsValidUserName(string userName)
		{
			return userName == null || userName.IndexOf(';') < 0;
		}

		// Token: 0x040000EF RID: 239
		internal const char ComponentSeparator = ';';

		// Token: 0x040000F0 RID: 240
		private const char EmailSeparator = ',';

		// Token: 0x040000F1 RID: 241
		private const string FormatVersion = "1.0";

		// Token: 0x040000F2 RID: 242
		private Guid identity;

		// Token: 0x040000F3 RID: 243
		private Guid databaseGuid;

		// Token: 0x040000F4 RID: 244
		private AlternateMailbox.AlternateMailboxFlags flags;

		// Token: 0x040000F5 RID: 245
		private string name;

		// Token: 0x040000F6 RID: 246
		private List<SmtpAddress> smtpAddresses;

		// Token: 0x040000F7 RID: 247
		private string userName;

		// Token: 0x040000F8 RID: 248
		private string unknownProperties;

		// Token: 0x02000054 RID: 84
		[Flags]
		public enum AlternateMailboxFlags
		{
			// Token: 0x040000FA RID: 250
			Unknown = 0,
			// Token: 0x040000FB RID: 251
			Archive = 1,
			// Token: 0x040000FC RID: 252
			Subscription = 2,
			// Token: 0x040000FD RID: 253
			RetentionPolicyEnabled = 256,
			// Token: 0x040000FE RID: 254
			Default = 257
		}
	}
}
