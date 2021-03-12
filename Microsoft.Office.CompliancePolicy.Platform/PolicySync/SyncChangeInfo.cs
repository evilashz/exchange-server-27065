using System;
using System.Text;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public sealed class SyncChangeInfo
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		public SyncChangeInfo(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("input");
			}
			string[] array = input.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				if (text.StartsWith("ObjectType:", StringComparison.OrdinalIgnoreCase))
				{
					if (text.Length <= "ObjectType:".Length)
					{
						throw new FormatException("ObjectType is null");
					}
					string text2 = text.Substring("ObjectType:".Length, text.Length - "ObjectType:".Length);
					this.ObjectType = (ConfigurationObjectType)Enum.Parse(typeof(ConfigurationObjectType), text2);
				}
				else if (text.StartsWith("ChangeType:", StringComparison.OrdinalIgnoreCase))
				{
					if (text.Length <= "ChangeType:".Length)
					{
						throw new FormatException("ChangeType is null");
					}
					string text2 = text.Substring("ChangeType:".Length, text.Length - "ChangeType:".Length);
					ChangeType changeType;
					if (!Enum.TryParse<ChangeType>(text2, true, out changeType))
					{
						throw new FormatException("ChangeType is invalid");
					}
					this.ChangeType = changeType;
				}
				else if (text.StartsWith("Version:", StringComparison.OrdinalIgnoreCase))
				{
					if (text.Length <= "Version:".Length)
					{
						throw new FormatException("Version is null");
					}
					string text2 = text.Substring("Version:".Length, text.Length - "Version:".Length);
					this.Version = PolicyVersion.Create(Guid.Parse(text2));
				}
				else
				{
					if (!text.StartsWith("ObjectId:", StringComparison.OrdinalIgnoreCase))
					{
						throw new FormatException("invalid token");
					}
					if (text.Length <= "ObjectId:".Length)
					{
						throw new FormatException("ObjectId is null");
					}
					string text2 = text.Substring("ObjectId:".Length, text.Length - "ObjectId:".Length);
					this.ObjectId = new Guid?(Guid.Parse(text2));
				}
			}
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001F6FC File Offset: 0x0001D8FC
		public SyncChangeInfo(ConfigurationObjectType objectType) : this(objectType, ChangeType.Update, null, null)
		{
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001F71C File Offset: 0x0001D91C
		public SyncChangeInfo(ConfigurationObjectType objectType, ChangeType changeType) : this(objectType, changeType, null, null)
		{
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001F73C File Offset: 0x0001D93C
		public SyncChangeInfo(ConfigurationObjectType objectType, ChangeType changeType, PolicyVersion version) : this(objectType, changeType, version, null)
		{
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001F75B File Offset: 0x0001D95B
		public SyncChangeInfo(ConfigurationObjectType objectType, ChangeType changeType, PolicyVersion version, Guid? objectId)
		{
			this.ObjectType = objectType;
			this.ChangeType = changeType;
			this.Version = version;
			this.ObjectId = objectId;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001F780 File Offset: 0x0001D980
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0001F788 File Offset: 0x0001D988
		public ConfigurationObjectType ObjectType { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001F791 File Offset: 0x0001D991
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0001F799 File Offset: 0x0001D999
		public ChangeType ChangeType { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001F7A2 File Offset: 0x0001D9A2
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0001F7AA File Offset: 0x0001D9AA
		public PolicyVersion Version { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001F7B3 File Offset: 0x0001D9B3
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0001F7BB File Offset: 0x0001D9BB
		public Guid? ObjectId { get; set; }

		// Token: 0x0600093F RID: 2367 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
		public static SyncChangeInfo Parse(string input)
		{
			return new SyncChangeInfo(input);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ObjectType:");
			stringBuilder.Append(this.ObjectType.ToString());
			stringBuilder.Append(";");
			stringBuilder.Append("ChangeType:");
			stringBuilder.Append(this.ChangeType.ToString());
			stringBuilder.Append(";");
			if (this.Version != null)
			{
				stringBuilder.Append("Version:");
				stringBuilder.Append(this.Version.InternalStorage.ToString());
				stringBuilder.Append(";");
			}
			if (this.ObjectId != null && this.ObjectId != null)
			{
				stringBuilder.Append("ObjectId:");
				stringBuilder.Append(this.ObjectId.Value);
				stringBuilder.Append(";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004C3 RID: 1219
		private const string ObjectTypeToken = "ObjectType:";

		// Token: 0x040004C4 RID: 1220
		private const string ChangeTypeToken = "ChangeType:";

		// Token: 0x040004C5 RID: 1221
		private const string VersionToken = "Version:";

		// Token: 0x040004C6 RID: 1222
		private const string ObjectIdToken = "ObjectId:";
	}
}
