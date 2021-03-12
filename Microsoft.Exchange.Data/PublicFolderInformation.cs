using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200026A RID: 618
	[Serializable]
	public class PublicFolderInformation
	{
		// Token: 0x060014AB RID: 5291 RVA: 0x00042627 File Offset: 0x00040827
		private PublicFolderInformation(int majorVersion, int minorVersion, string rawValue)
		{
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
			this.type = PublicFolderInformation.HierarchyType.Unknown;
			this.rawValue = rawValue;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0004264B File Offset: 0x0004084B
		private PublicFolderInformation(int majorVersion, int minorVersion, PublicFolderInformation.HierarchyType type, Guid hierarchyMailboxGuid, string rawValue)
		{
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
			this.type = type;
			this.hierarchyMailboxGuid = new Guid?(hierarchyMailboxGuid);
			this.rawValue = rawValue;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0004267D File Offset: 0x0004087D
		public bool IsValid
		{
			get
			{
				return this.type != PublicFolderInformation.HierarchyType.Unknown;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0004268B File Offset: 0x0004088B
		public bool CanUpdate
		{
			get
			{
				return this.majorVersion < 1 || (this.majorVersion == 1 && this.minorVersion <= 1);
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x000426AF File Offset: 0x000408AF
		internal PublicFolderInformation.HierarchyType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x000426B7 File Offset: 0x000408B7
		public Guid HierarchyMailboxGuid
		{
			get
			{
				if (this.hierarchyMailboxGuid == null)
				{
					return Guid.Empty;
				}
				return this.hierarchyMailboxGuid.Value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x000426D7 File Offset: 0x000408D7
		public bool LockedForMigration
		{
			get
			{
				return this.type == PublicFolderInformation.HierarchyType.InTransitMailboxGuid;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x000426E4 File Offset: 0x000408E4
		internal int ItemSize
		{
			get
			{
				switch (this.type)
				{
				case PublicFolderInformation.HierarchyType.Unknown:
					return 9 + this.rawValue.Length;
				case PublicFolderInformation.HierarchyType.MailboxGuid:
				case PublicFolderInformation.HierarchyType.InTransitMailboxGuid:
					return 25 + this.rawValue.Length;
				}
				throw new InvalidOperationException("How did we get here?");
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0004273C File Offset: 0x0004093C
		public void SetHierarchyMailbox(Guid hierarchyMailboxGuid, PublicFolderInformation.HierarchyType hierarchyType)
		{
			if (hierarchyType != PublicFolderInformation.HierarchyType.MailboxGuid && hierarchyType != PublicFolderInformation.HierarchyType.InTransitMailboxGuid)
			{
				throw new ArgumentException(string.Format("hierarchyType must be either MailboxGuid or InTransitMailboxGuid: {0}", hierarchyType), "hierarchyType");
			}
			this.EnsureWritable();
			this.type = hierarchyType;
			this.hierarchyMailboxGuid = new Guid?(hierarchyMailboxGuid);
			this.hierarchySmtpAddress = SmtpAddress.Empty;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00042790 File Offset: 0x00040990
		public string Serialize()
		{
			switch (this.type)
			{
			case PublicFolderInformation.HierarchyType.MailboxGuid:
			case PublicFolderInformation.HierarchyType.InTransitMailboxGuid:
			{
				string text = this.hierarchyMailboxGuid.ToString();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.majorVersion);
				stringBuilder.Append(PublicFolderInformation.Separator);
				stringBuilder.Append(this.minorVersion);
				stringBuilder.Append(PublicFolderInformation.Separator);
				stringBuilder.Append((byte)this.type);
				stringBuilder.Append(PublicFolderInformation.Separator);
				stringBuilder.Append(text.Length);
				stringBuilder.Append(PublicFolderInformation.Separator);
				stringBuilder.Append(text);
				return stringBuilder.ToString();
			}
			default:
				return this.rawValue;
			}
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0004284C File Offset: 0x00040A4C
		public PublicFolderInformation Clone()
		{
			return new PublicFolderInformation(this.majorVersion, this.minorVersion, this.rawValue)
			{
				type = this.type,
				hierarchyMailboxGuid = this.hierarchyMailboxGuid,
				hierarchySmtpAddress = this.hierarchySmtpAddress
			};
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00042898 File Offset: 0x00040A98
		public override string ToString()
		{
			switch (this.type)
			{
			case PublicFolderInformation.HierarchyType.MailboxGuid:
			case PublicFolderInformation.HierarchyType.InTransitMailboxGuid:
				return this.hierarchyMailboxGuid.ToString();
			default:
				return this.rawValue;
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x000428D6 File Offset: 0x00040AD6
		private void EnsureWritable()
		{
			if (!this.CanUpdate)
			{
				throw new InvalidOperationException("This instance is not writable");
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x000428EC File Offset: 0x00040AEC
		public static PublicFolderInformation Deserialize(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				return new PublicFolderInformation(1, 1, string.Empty);
			}
			int num = 0;
			int num2 = input.IndexOf(PublicFolderInformation.Separator);
			if (num2 == -1 || !int.TryParse(input.Substring(0, num2), out num) || num > 1)
			{
				return new PublicFolderInformation(num, 1, input);
			}
			int num3 = 0;
			string text = input.Substring(num2 + 1);
			int num4 = text.IndexOf(PublicFolderInformation.Separator);
			if (num4 == -1 || !int.TryParse(text.Substring(0, num4), out num3))
			{
				return new PublicFolderInformation(num, 1, input);
			}
			text = text.Substring(num4 + 1);
			int num5 = text.IndexOf(PublicFolderInformation.Separator);
			byte b = 0;
			PublicFolderInformation.HierarchyType hierarchyType;
			if (num5 == -1 || !byte.TryParse(text.Substring(0, num5), out b) || !Enum.IsDefined(typeof(PublicFolderInformation.HierarchyType), b) || (hierarchyType = (PublicFolderInformation.HierarchyType)b) == PublicFolderInformation.HierarchyType.Unknown)
			{
				return new PublicFolderInformation(num, num3, input);
			}
			int num6 = 0;
			text = text.Substring(num5 + 1);
			int num7 = text.IndexOf(PublicFolderInformation.Separator);
			if (num7 == -1 || !int.TryParse(text.Substring(0, num7), out num6) || num6 <= 0 || text.Length < num7 + 1 + num6)
			{
				return new PublicFolderInformation(num, num3, input);
			}
			string text2 = text.Substring(num7 + 1, num6);
			if (string.IsNullOrWhiteSpace(text2))
			{
				return new PublicFolderInformation(num, num3, input);
			}
			switch (hierarchyType)
			{
			case PublicFolderInformation.HierarchyType.MailboxGuid:
			case PublicFolderInformation.HierarchyType.InTransitMailboxGuid:
			{
				Guid empty = Guid.Empty;
				if (GuidHelper.TryParseGuid(text2, out empty))
				{
					return new PublicFolderInformation(num, num3, hierarchyType, empty, input);
				}
				return new PublicFolderInformation(num, num3, input);
			}
			default:
				throw new InvalidOperationException("Unknown HierarchyType! How did we get here if parsing was successful?");
			}
		}

		// Token: 0x04000C14 RID: 3092
		private const int CurrentMajorVersion = 1;

		// Token: 0x04000C15 RID: 3093
		private const int CurrentMinorVersion = 1;

		// Token: 0x04000C16 RID: 3094
		internal static PublicFolderInformation InvalidPublicFolderInformation = new PublicFolderInformation(1, 1, string.Empty);

		// Token: 0x04000C17 RID: 3095
		private static char Separator = ';';

		// Token: 0x04000C18 RID: 3096
		private readonly string rawValue;

		// Token: 0x04000C19 RID: 3097
		private readonly int majorVersion;

		// Token: 0x04000C1A RID: 3098
		private readonly int minorVersion;

		// Token: 0x04000C1B RID: 3099
		private PublicFolderInformation.HierarchyType type;

		// Token: 0x04000C1C RID: 3100
		private Guid? hierarchyMailboxGuid;

		// Token: 0x04000C1D RID: 3101
		private SmtpAddress hierarchySmtpAddress;

		// Token: 0x0200026B RID: 619
		public enum HierarchyType : byte
		{
			// Token: 0x04000C1F RID: 3103
			Unknown = 1,
			// Token: 0x04000C20 RID: 3104
			MailboxGuid = 3,
			// Token: 0x04000C21 RID: 3105
			InTransitMailboxGuid
		}
	}
}
