using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Office.CompliancePolicy.PolicyEvaluation
{
	// Token: 0x020000C4 RID: 196
	public class PolicyRule
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0000EB0D File Offset: 0x0000CD0D
		public PolicyRule()
		{
			this.InitializeProperties();
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0000EB26 File Offset: 0x0000CD26
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0000EB2E File Offset: 0x0000CD2E
		public Condition Condition { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0000EB37 File Offset: 0x0000CD37
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x0000EB3F File Offset: 0x0000CD3F
		public IList<Action> Actions { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000EB48 File Offset: 0x0000CD48
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public string Name { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000EB59 File Offset: 0x0000CD59
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0000EB61 File Offset: 0x0000CD61
		public Guid ImmutableId { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000EB6A File Offset: 0x0000CD6A
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0000EB72 File Offset: 0x0000CD72
		public string Comments { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0000EB7B File Offset: 0x0000CD7B
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0000EB83 File Offset: 0x0000CD83
		public RuleState Enabled { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		public Version MinimumVersion
		{
			get
			{
				Version version = this.Condition.MinimumVersion;
				foreach (Action action in this.Actions)
				{
					Version minimumVersion = action.MinimumVersion;
					if (version < minimumVersion)
					{
						version = minimumVersion;
					}
				}
				return version;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		public DateTime? WhenChangedUtc { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000EC05 File Offset: 0x0000CE05
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000EC0D File Offset: 0x0000CE0D
		public bool IsTooAdvancedToParse { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000EC16 File Offset: 0x0000CE16
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000EC1E File Offset: 0x0000CE1E
		public DateTime? ExpiryDate { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000EC27 File Offset: 0x0000CE27
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000EC2F File Offset: 0x0000CE2F
		public DateTime? ActivationDate { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000EC38 File Offset: 0x0000CE38
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000EC40 File Offset: 0x0000CE40
		public RuleMode Mode { get; set; }

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000EC52 File Offset: 0x0000CE52
		public IEnumerable<RuleTag> GetTags()
		{
			return this.tags.SelectMany((KeyValuePair<string, List<RuleTag>> tagItem) => tagItem.Value);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000EC7C File Offset: 0x0000CE7C
		public IEnumerable<RuleTag> GetTags(string tagType)
		{
			List<RuleTag> result;
			if (this.tags.TryGetValue(tagType, out result))
			{
				return result;
			}
			return new List<RuleTag>();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000ECA0 File Offset: 0x0000CEA0
		public void AddTag(RuleTag tag)
		{
			List<RuleTag> list;
			if (this.tags.TryGetValue(tag.TagType, out list))
			{
				list.Add(tag);
				return;
			}
			this.tags.Add(tag.TagType, new List<RuleTag>
			{
				tag
			});
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000ECE9 File Offset: 0x0000CEE9
		public void RemoveAllTags(string tagType)
		{
			this.tags.Remove(tagType);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		public void GetSupplementalData(SupplementalData data)
		{
			if (this.Condition != null)
			{
				this.Condition.GetSupplementalData(data);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000ED10 File Offset: 0x0000CF10
		private void InitializeProperties()
		{
			this.Condition = null;
			this.Actions = new List<Action>();
			this.Name = string.Empty;
			this.ImmutableId = Guid.NewGuid();
			this.Comments = string.Empty;
			this.Enabled = RuleState.Enabled;
			this.IsTooAdvancedToParse = false;
			this.Mode = RuleMode.Enforce;
		}

		// Token: 0x040002FD RID: 765
		public static readonly Version BaseVersion = new Version("1.00.0000.000");

		// Token: 0x040002FE RID: 766
		public static readonly Version HighestHonoredVersion = new Version("1.00.0002.000");

		// Token: 0x040002FF RID: 767
		private readonly Dictionary<string, List<RuleTag>> tags = new Dictionary<string, List<RuleTag>>();
	}
}
