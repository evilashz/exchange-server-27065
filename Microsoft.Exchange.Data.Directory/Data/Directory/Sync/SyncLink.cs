using System;
using System.Text;
using Microsoft.Exchange.Data.Directory.DirSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200082C RID: 2092
	[Serializable]
	internal class SyncLink : ADDirSyncLink
	{
		// Token: 0x060067E6 RID: 26598 RVA: 0x0016E4D0 File Offset: 0x0016C6D0
		public SyncLink(string targetId, LinkState linkState) : base(null, linkState)
		{
			this.targetId = targetId;
		}

		// Token: 0x060067E7 RID: 26599 RVA: 0x0016E4E1 File Offset: 0x0016C6E1
		public SyncLink(string targetId, DirectoryObjectClass targetObjectClass, LinkState linkState) : this(targetId, linkState)
		{
			this.targetObjectClass = targetObjectClass;
		}

		// Token: 0x060067E8 RID: 26600 RVA: 0x0016E4F2 File Offset: 0x0016C6F2
		public SyncLink(ADObjectId link, LinkState state) : base(link, state)
		{
		}

		// Token: 0x060067E9 RID: 26601 RVA: 0x0016E4FC File Offset: 0x0016C6FC
		public static SyncLink ParseFromADString(string adString)
		{
			if (string.IsNullOrEmpty(adString))
			{
				throw new FormatException(DirectoryStrings.InvalidSyncLinkFormat(adString));
			}
			string[] array = adString.Split(new char[]
			{
				','
			});
			if (array.Length != 4)
			{
				throw new FormatException(DirectoryStrings.InvalidSyncLinkFormat(adString));
			}
			SyncLink syncLink = null;
			try
			{
				LinkState linkState = (LinkState)Enum.Parse(typeof(LinkState), array[0]);
				if (!string.IsNullOrEmpty(array[0]))
				{
					string @string = Encoding.UTF8.GetString(Convert.FromBase64String(array[1]));
					syncLink = new SyncLink(@string, linkState);
				}
				else
				{
					string string2 = Encoding.UTF8.GetString(Convert.FromBase64String(array[2]));
					ADObjectId link = ADObjectId.ParseDnOrGuid(string2);
					syncLink = new SyncLink(link, linkState);
				}
				syncLink.targetObjectClass = (DirectoryObjectClass)Enum.Parse(typeof(DirectoryObjectClass), array[3]);
			}
			catch (FormatException innerException)
			{
				throw new FormatException(DirectoryStrings.InvalidSyncLinkFormat(adString), innerException);
			}
			return syncLink;
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x0016E5FC File Offset: 0x0016C7FC
		public void UpdateSyncData(string targetId, DirectoryObjectClass targetObjectClass)
		{
			this.targetId = targetId;
			this.targetObjectClass = targetObjectClass;
		}

		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x060067EB RID: 26603 RVA: 0x0016E60C File Offset: 0x0016C80C
		public string TargetId
		{
			get
			{
				return this.targetId;
			}
		}

		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x060067EC RID: 26604 RVA: 0x0016E614 File Offset: 0x0016C814
		public DirectoryObjectClass TargetObjectClass
		{
			get
			{
				return this.targetObjectClass;
			}
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x0016E61C File Offset: 0x0016C81C
		public override string ToString()
		{
			return this.targetId ?? string.Empty;
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x0016E630 File Offset: 0x0016C830
		public override bool Equals(object obj)
		{
			SyncLink syncLink = obj as SyncLink;
			return syncLink != null && (this.targetId == syncLink.targetId && base.Link == syncLink.Link && base.State == syncLink.State) && this.targetObjectClass == syncLink.targetObjectClass;
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x0016E688 File Offset: 0x0016C888
		public override int GetHashCode()
		{
			return (int)(((this.targetId == null) ? 0 : this.targetId.GetHashCode()) + ((base.Link == null) ? 0 : base.Link.GetHashCode()) + this.targetObjectClass);
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x0016E6C0 File Offset: 0x0016C8C0
		public string ToADString()
		{
			return string.Format("{0},{1},{2},{3}", new object[]
			{
				base.State.ToString(),
				string.IsNullOrEmpty(this.targetId) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(this.targetId)),
				(base.Link == null) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(base.Link.ToGuidOrDNString())),
				this.targetObjectClass.ToString()
			});
		}

		// Token: 0x04004460 RID: 17504
		private string targetId;

		// Token: 0x04004461 RID: 17505
		private DirectoryObjectClass targetObjectClass;
	}
}
