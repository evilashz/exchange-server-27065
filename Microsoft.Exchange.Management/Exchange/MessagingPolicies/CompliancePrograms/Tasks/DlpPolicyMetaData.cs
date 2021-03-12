using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000955 RID: 2389
	[Serializable]
	internal class DlpPolicyMetaData
	{
		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06005565 RID: 21861 RVA: 0x0015F634 File Offset: 0x0015D834
		// (set) Token: 0x06005566 RID: 21862 RVA: 0x0015F63C File Offset: 0x0015D83C
		internal string Version { get; set; }

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06005567 RID: 21863 RVA: 0x0015F645 File Offset: 0x0015D845
		// (set) Token: 0x06005568 RID: 21864 RVA: 0x0015F64D File Offset: 0x0015D84D
		internal RuleState State { get; set; }

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06005569 RID: 21865 RVA: 0x0015F656 File Offset: 0x0015D856
		// (set) Token: 0x0600556A RID: 21866 RVA: 0x0015F65E File Offset: 0x0015D85E
		internal RuleMode Mode { get; set; }

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x0600556B RID: 21867 RVA: 0x0015F667 File Offset: 0x0015D867
		// (set) Token: 0x0600556C RID: 21868 RVA: 0x0015F66F File Offset: 0x0015D86F
		internal string ContentVersion { get; set; }

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x0600556D RID: 21869 RVA: 0x0015F678 File Offset: 0x0015D878
		// (set) Token: 0x0600556E RID: 21870 RVA: 0x0015F680 File Offset: 0x0015D880
		internal string PublisherName { get; set; }

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x0015F689 File Offset: 0x0015D889
		// (set) Token: 0x06005570 RID: 21872 RVA: 0x0015F691 File Offset: 0x0015D891
		internal string Name { get; set; }

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06005571 RID: 21873 RVA: 0x0015F69A File Offset: 0x0015D89A
		// (set) Token: 0x06005572 RID: 21874 RVA: 0x0015F6A2 File Offset: 0x0015D8A2
		internal string Description { get; set; }

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06005573 RID: 21875 RVA: 0x0015F6AB File Offset: 0x0015D8AB
		// (set) Token: 0x06005574 RID: 21876 RVA: 0x0015F6B3 File Offset: 0x0015D8B3
		internal List<string> Keywords { get; set; }

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06005575 RID: 21877 RVA: 0x0015F6BC File Offset: 0x0015D8BC
		// (set) Token: 0x06005576 RID: 21878 RVA: 0x0015F6C4 File Offset: 0x0015D8C4
		internal Guid ImmutableId { get; set; }

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06005577 RID: 21879 RVA: 0x0015F6CD File Offset: 0x0015D8CD
		// (set) Token: 0x06005578 RID: 21880 RVA: 0x0015F6D5 File Offset: 0x0015D8D5
		internal List<string> PolicyCommands { get; set; }

		// Token: 0x06005579 RID: 21881 RVA: 0x0015F6DE File Offset: 0x0015D8DE
		internal DlpPolicyMetaData()
		{
			this.Keywords = new List<string>();
			this.PolicyCommands = new List<string>();
			this.ImmutableId = Guid.NewGuid();
			this.Mode = RuleMode.Audit;
			this.State = RuleState.Enabled;
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x0015F72C File Offset: 0x0015D92C
		internal DlpPolicyMetaData(DlpPolicyTemplateMetaData dlpTemplate, CultureInfo culture = null)
		{
			this.Name = DlpPolicyTemplateMetaData.GetLocalizedStringValue(dlpTemplate.LocalizedNames, culture);
			this.Description = DlpPolicyTemplateMetaData.GetLocalizedStringValue(dlpTemplate.LocalizedDescriptions, culture);
			this.ContentVersion = dlpTemplate.ContentVersion;
			this.Version = dlpTemplate.Version;
			this.Mode = dlpTemplate.Mode;
			this.State = dlpTemplate.State;
			this.PublisherName = dlpTemplate.PublisherName;
			this.ImmutableId = Guid.NewGuid();
			this.Keywords = (from keyword in dlpTemplate.LocalizedKeywords
			select DlpPolicyTemplateMetaData.GetLocalizedStringValue(keyword, culture)).ToArray<string>().ToList<string>();
			this.PolicyCommands = dlpTemplate.PolicyCommands;
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x0015F824 File Offset: 0x0015DA24
		internal void Validate()
		{
			if (string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.Version))
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyXmlMissingElements);
			}
			if (new Version(this.Version) > DlpUtils.MaxSupportedVersion)
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyVersionUnsupported);
			}
			if (this.Keywords.Any(new Func<string, bool>(string.IsNullOrEmpty)))
			{
				throw new DlpPolicyParsingException(Strings.DlpPolicyContainsEmptyKeywords);
			}
			DlpPolicyTemplateMetaData.ValidateFieldSize("name", this.Name, 64);
			DlpPolicyTemplateMetaData.ValidateFieldSize("version", this.Version, 16);
			DlpPolicyTemplateMetaData.ValidateFieldSize("contentVersion", this.ContentVersion, 16);
			DlpPolicyTemplateMetaData.ValidateFieldSize("publisherName", this.PublisherName, 256);
			DlpPolicyTemplateMetaData.ValidateFieldSize("description", this.Description, 1024);
			this.Keywords.ForEach(delegate(string x)
			{
				DlpPolicyTemplateMetaData.ValidateFieldSize("keyword", x, 64);
			});
			this.PolicyCommands.ForEach(delegate(string command)
			{
				DlpPolicyTemplateMetaData.ValidateFieldSize("commandBlock", command, 4096);
			});
			this.PolicyCommands.ForEach(delegate(string command)
			{
				DlpPolicyTemplateMetaData.ValidateCmdletParameters(command);
			});
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x0015F984 File Offset: 0x0015DB84
		internal ADComplianceProgram ToAdObject()
		{
			return new ADComplianceProgram
			{
				Name = this.Name,
				Description = this.Description,
				ImmutableId = this.ImmutableId,
				Keywords = this.Keywords.ToArray(),
				PublisherName = this.PublisherName,
				State = DlpUtils.RuleStateToDlpState(this.State, this.Mode),
				TransportRulesXml = new StreamReader(new MemoryStream(DlpPolicyParser.SerializeDlpPolicyInstance(this))).ReadToEnd(),
				Version = this.Version
			};
		}
	}
}
