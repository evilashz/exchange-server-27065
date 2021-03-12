using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200024F RID: 591
	public class MailboxLocationInfo : IMailboxLocationInfo
	{
		// Token: 0x06001D08 RID: 7432 RVA: 0x00078A14 File Offset: 0x00076C14
		public MailboxLocationInfo(Guid mailboxGuid, ADObjectId databaseLocation, MailboxLocationType mailboxLocationType)
		{
			MailboxLocationInfo.ValidateMailboxInfo(mailboxGuid, databaseLocation);
			this.MailboxGuid = mailboxGuid;
			this.databaseLocation = ((databaseLocation != null) ? new ADObjectId(databaseLocation.ObjectGuid, databaseLocation.PartitionFQDN) : null);
			this.MailboxLocationType = mailboxLocationType;
			this.infoToString = null;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00078A60 File Offset: 0x00076C60
		public MailboxLocationInfo(string mailboxLocationString)
		{
			if (string.IsNullOrEmpty(mailboxLocationString))
			{
				throw new ArgumentNullException("mailboxLocationString");
			}
			string[] array = mailboxLocationString.Split(new string[]
			{
				MailboxLocationInfo.MailboxLocationDelimiter
			}, StringSplitOptions.None);
			int minValue = int.MinValue;
			if (array.Length > 1 && (!int.TryParse(array[0], out minValue) || minValue > MailboxLocationInfo.MaxSerializableVersion || minValue < 0))
			{
				throw new ArgumentException("mailboxLocationString");
			}
			Guid guid = Guid.Empty;
			string text = null;
			for (int i = 1; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					try
					{
						switch (i)
						{
						case 1:
							this.MailboxGuid = Guid.Parse(array[1]);
							break;
						case 2:
							this.MailboxLocationType = (MailboxLocationType)Enum.Parse(typeof(MailboxLocationType), array[2]);
							break;
						case 3:
							text = array[3];
							break;
						case 4:
							guid = Guid.Parse(array[4]);
							break;
						}
					}
					catch (Exception innerException)
					{
						throw new ADOperationException(DirectoryStrings.CannotParse(array[i]), innerException);
					}
				}
			}
			if (!guid.Equals(Guid.Empty) && text != null)
			{
				this.databaseLocation = new ADObjectId(guid, text);
			}
			MailboxLocationInfo.ValidateMailboxInfo(this.MailboxGuid, this.databaseLocation);
			this.infoToString = null;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00078BB4 File Offset: 0x00076DB4
		private static void ValidateMailboxInfo(Guid mailboxGuid, ADObjectId databaseLocation)
		{
			if (mailboxGuid.Equals(Guid.Empty))
			{
				throw new ArgumentException("mailboxGuid");
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x00078BCF File Offset: 0x00076DCF
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x00078BD7 File Offset: 0x00076DD7
		public Guid MailboxGuid { get; private set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x00078BE0 File Offset: 0x00076DE0
		public ADObjectId DatabaseLocation
		{
			get
			{
				if (this.databaseLocation == null)
				{
					return null;
				}
				return new ADObjectId(this.databaseLocation.ObjectGuid, this.databaseLocation.PartitionFQDN);
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00078C07 File Offset: 0x00076E07
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x00078C0F File Offset: 0x00076E0F
		public MailboxLocationType MailboxLocationType { get; private set; }

		// Token: 0x06001D10 RID: 7440 RVA: 0x00078C18 File Offset: 0x00076E18
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.infoToString))
			{
				string[] array = new string[MailboxLocationInfo.MaxMailboxLocationIndices];
				array[0] = MailboxLocationInfo.MaxSerializableVersion.ToString();
				array[2] = this.MailboxLocationType.ToString();
				array[1] = this.MailboxGuid.ToString();
				if (this.databaseLocation != null)
				{
					array[3] = this.databaseLocation.PartitionFQDN;
					array[4] = this.databaseLocation.ObjectGuid.ToString();
				}
				this.infoToString = string.Join(MailboxLocationInfo.MailboxLocationDelimiter, array);
			}
			return this.infoToString;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00078CC4 File Offset: 0x00076EC4
		public override bool Equals(object obj)
		{
			MailboxLocationInfo mailboxLocationInfo = obj as MailboxLocationInfo;
			if (mailboxLocationInfo == null)
			{
				return false;
			}
			bool flag = false;
			if (mailboxLocationInfo.DatabaseLocation == null && this.DatabaseLocation == null)
			{
				flag = true;
			}
			else if (mailboxLocationInfo.DatabaseLocation != null && this.DatabaseLocation != null)
			{
				flag = mailboxLocationInfo.DatabaseLocation.Equals(this.DatabaseLocation);
			}
			return mailboxLocationInfo.MailboxGuid.Equals(this.MailboxGuid) && flag && mailboxLocationInfo.MailboxLocationType == this.MailboxLocationType;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00078D3E File Offset: 0x00076F3E
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x04000DC1 RID: 3521
		private static readonly string MailboxLocationDelimiter = ";";

		// Token: 0x04000DC2 RID: 3522
		private static int MaxMailboxLocationIndices = Enum.GetValues(typeof(MailboxLocationInfo.MailboxLocationIndex)).Length;

		// Token: 0x04000DC3 RID: 3523
		private static readonly int MaxSerializableVersion = 1;

		// Token: 0x04000DC4 RID: 3524
		private string infoToString;

		// Token: 0x04000DC5 RID: 3525
		private readonly ADObjectId databaseLocation;

		// Token: 0x02000250 RID: 592
		private enum MailboxLocationIndex
		{
			// Token: 0x04000DC9 RID: 3529
			Version,
			// Token: 0x04000DCA RID: 3530
			MailboxGuid,
			// Token: 0x04000DCB RID: 3531
			MailboxLocationType,
			// Token: 0x04000DCC RID: 3532
			DBForestFqdn,
			// Token: 0x04000DCD RID: 3533
			DBObjectGuid
		}
	}
}
