using System;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007A0 RID: 1952
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExternalUser
	{
		// Token: 0x060049A8 RID: 18856 RVA: 0x0013466A File Offset: 0x0013286A
		internal ExternalUser(MemoryPropertyBag propertyBag)
		{
			this.propertyBag = propertyBag;
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x00134679 File Offset: 0x00132879
		public ExternalUser(string externalId, SmtpAddress address) : this(externalId, address, false)
		{
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x00134684 File Offset: 0x00132884
		public static ExternalUser CreateExternalUserForGroupMailbox(string externalUserName, string externalUserId, Guid mailboxGuid, SecurityIdentity.GroupMailboxMemberType groupMailboxMemberType)
		{
			return new ExternalUser(new MemoryPropertyBag())
			{
				Name = externalUserName,
				ExternalId = externalUserId,
				SmtpAddress = SmtpAddress.Parse(externalUserId),
				Sid = SecurityIdentity.GetGroupSecurityIdentifier(mailboxGuid, groupMailboxMemberType)
			};
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x001346C4 File Offset: 0x001328C4
		public ExternalUser(string externalUserName, string externalId, SmtpAddress address, SecurityIdentifier sid) : this(externalId, address, false)
		{
			this.Name = externalUserName;
			this.Sid = sid;
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x001346E0 File Offset: 0x001328E0
		public ExternalUser(string externalId, SmtpAddress address, bool isReachUser) : this(new MemoryPropertyBag())
		{
			if (address.Local.StartsWith("ExchangePublishedUser."))
			{
				throw new ArgumentException(string.Format("Cannot add external user with prefix {0}", "ExchangePublishedUser."), "address");
			}
			this.IsReachUser = isReachUser;
			this.OriginalSmtpAddress = address;
			if (this.IsReachUser)
			{
				this.ExternalId = "ReachUser_" + externalId;
				this.Name = address.ToString() + " (Published)";
				this.SmtpAddress = ExternalUser.ConvertToReachSmtpAddress(address);
				this.Sid = ExternalUser.GenerateSid(this.SmtpAddress.ToString(), true);
				return;
			}
			this.ExternalId = externalId;
			this.Name = address.ToString();
			this.SmtpAddress = address;
			this.Sid = ExternalUser.GenerateSid(this.SmtpAddress.ToString(), false);
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x001347D7 File Offset: 0x001329D7
		// (set) Token: 0x060049AE RID: 18862 RVA: 0x00134802 File Offset: 0x00132A02
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					this.name = (this.propertyBag[InternalSchema.MemberName] as string);
				}
				return this.name;
			}
			private set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException();
				}
				this.propertyBag[InternalSchema.MemberName] = value;
				this.name = value;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x0013482A File Offset: 0x00132A2A
		// (set) Token: 0x060049B0 RID: 18864 RVA: 0x00134855 File Offset: 0x00132A55
		public string ExternalId
		{
			get
			{
				if (this.externalId == null)
				{
					this.externalId = (this.propertyBag[InternalSchema.MemberExternalIdLocalDirectory] as string);
				}
				return this.externalId;
			}
			private set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException();
				}
				this.propertyBag[InternalSchema.MemberExternalIdLocalDirectory] = value;
				this.externalId = value;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x0013487D File Offset: 0x00132A7D
		// (set) Token: 0x060049B2 RID: 18866 RVA: 0x001348BC File Offset: 0x00132ABC
		public SmtpAddress SmtpAddress
		{
			get
			{
				if (this.smtpAddress == null)
				{
					this.smtpAddress = new SmtpAddress?(new SmtpAddress(this.propertyBag[InternalSchema.MemberEmailLocalDirectory] as string));
				}
				return this.smtpAddress.Value;
			}
			private set
			{
				this.propertyBag[InternalSchema.MemberEmailLocalDirectory] = value.ToString();
				this.smtpAddress = new SmtpAddress?(value);
			}
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x001348E7 File Offset: 0x00132AE7
		// (set) Token: 0x060049B4 RID: 18868 RVA: 0x00134920 File Offset: 0x00132B20
		public SecurityIdentifier Sid
		{
			get
			{
				if (this.sid == null)
				{
					this.sid = new SecurityIdentifier(this.propertyBag[InternalSchema.MemberSIDLocalDirectory] as byte[], 0);
				}
				return this.sid;
			}
			private set
			{
				byte[] array = new byte[value.BinaryLength];
				value.GetBinaryForm(array, 0);
				this.propertyBag[InternalSchema.MemberSIDLocalDirectory] = array;
				this.sid = value;
			}
		}

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x060049B5 RID: 18869 RVA: 0x00134959 File Offset: 0x00132B59
		public string LegacyDn
		{
			get
			{
				if (this.legacyDn == null)
				{
					this.legacyDn = string.Format("{0}{1}", "LocalUser:", this.Sid);
				}
				return this.legacyDn;
			}
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x00134984 File Offset: 0x00132B84
		// (set) Token: 0x060049B7 RID: 18871 RVA: 0x001349BA File Offset: 0x00132BBA
		public bool IsReachUser
		{
			get
			{
				if (this.isReachUser == null)
				{
					this.isReachUser = new bool?(this.ExternalId.StartsWith("ReachUser_", StringComparison.Ordinal));
				}
				return this.isReachUser.Value;
			}
			private set
			{
				this.isReachUser = new bool?(value);
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x060049B8 RID: 18872 RVA: 0x001349C8 File Offset: 0x00132BC8
		// (set) Token: 0x060049B9 RID: 18873 RVA: 0x00134A3B File Offset: 0x00132C3B
		public SmtpAddress OriginalSmtpAddress
		{
			get
			{
				if (this.originalSmtpAddress == null)
				{
					if (!this.IsReachUser)
					{
						this.originalSmtpAddress = new SmtpAddress?(this.SmtpAddress);
					}
					else
					{
						this.originalSmtpAddress = new SmtpAddress?(new SmtpAddress(this.SmtpAddress.ToString().Substring("ExchangePublishedUser.".Length)));
					}
				}
				return this.originalSmtpAddress.Value;
			}
			private set
			{
				this.originalSmtpAddress = new SmtpAddress?(value);
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x00134A49 File Offset: 0x00132C49
		internal MemoryPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x00134A51 File Offset: 0x00132C51
		public static bool IsExternalUserSid(SecurityIdentifier sid)
		{
			return 0 == string.Compare("S-1-8-", 0, sid.ToString(), 0, "S-1-8-".Length, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00134A73 File Offset: 0x00132C73
		internal static SmtpAddress ConvertToReachSmtpAddress(SmtpAddress smtpAddress)
		{
			return new SmtpAddress("ExchangePublishedUser." + smtpAddress.ToString());
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00134A94 File Offset: 0x00132C94
		internal static bool IsValidReachSid(SecurityIdentifier sid)
		{
			if (!ExternalUser.IsExternalUserSid(sid))
			{
				return false;
			}
			string text = sid.ToString();
			int num = text.LastIndexOf('-');
			string a = text.Substring(num + 1);
			string input = text.Substring(0, num + 1);
			string b = ExternalUser.ComputeHash(input);
			return string.Equals(a, b, StringComparison.Ordinal);
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x00134AE4 File Offset: 0x00132CE4
		private static SecurityIdentifier GenerateSid(string source, bool forReachUser)
		{
			if (forReachUser)
			{
				source = Guid.NewGuid() + source;
			}
			byte[] sha1Hash = CryptoUtil.GetSha1Hash(Encoding.Unicode.GetBytes(source));
			for (int i = 0; i < 4; i++)
			{
				byte[] array = sha1Hash;
				int num = i;
				array[num] ^= sha1Hash[i + 16];
			}
			BinaryReader binaryReader = null;
			SecurityIdentifier result;
			try
			{
				binaryReader = new BinaryReader(new MemoryStream(sha1Hash));
				StringBuilder stringBuilder = new StringBuilder("S-1-8-");
				for (int j = 0; j < 4; j++)
				{
					if (j == 3 && forReachUser)
					{
						stringBuilder.Append(ExternalUser.ComputeHash(stringBuilder.ToString()));
					}
					else
					{
						stringBuilder.Append(binaryReader.ReadUInt32().ToString(NumberFormatInfo.InvariantInfo));
					}
					if (j < 3)
					{
						stringBuilder.Append('-');
					}
				}
				result = new SecurityIdentifier(stringBuilder.ToString());
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Dispose();
					binaryReader = null;
				}
			}
			return result;
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x00134BD8 File Offset: 0x00132DD8
		private static string ComputeHash(string input)
		{
			byte[] sha1Hash = CryptoUtil.GetSha1Hash(Encoding.Unicode.GetBytes(input));
			string result;
			using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(sha1Hash)))
			{
				result = binaryReader.ReadUInt32().ToString(NumberFormatInfo.InvariantInfo);
			}
			return result;
		}

		// Token: 0x040027A8 RID: 10152
		internal const string ReachUserNamePostFix = " (Published)";

		// Token: 0x040027A9 RID: 10153
		internal const string ExchangeSidPrefix = "S-1-8-";

		// Token: 0x040027AA RID: 10154
		internal const string ReachUserSmtpPrefix = "ExchangePublishedUser.";

		// Token: 0x040027AB RID: 10155
		private const string ReachUserPrefix = "ReachUser_";

		// Token: 0x040027AC RID: 10156
		private readonly MemoryPropertyBag propertyBag;

		// Token: 0x040027AD RID: 10157
		private string externalId;

		// Token: 0x040027AE RID: 10158
		private bool? isReachUser;

		// Token: 0x040027AF RID: 10159
		private string legacyDn;

		// Token: 0x040027B0 RID: 10160
		private string name;

		// Token: 0x040027B1 RID: 10161
		private SmtpAddress? originalSmtpAddress;

		// Token: 0x040027B2 RID: 10162
		private SecurityIdentifier sid;

		// Token: 0x040027B3 RID: 10163
		private SmtpAddress? smtpAddress;
	}
}
