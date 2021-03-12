using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B09 RID: 2825
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideInvalidVariantNameException : SettingOverrideException
	{
		// Token: 0x060081DF RID: 33247 RVA: 0x001A7471 File Offset: 0x001A5671
		public SettingOverrideInvalidVariantNameException(string componentName, string sectionName, string variantName, string availableVariantNames) : base(DirectoryStrings.ErrorSettingOverrideInvalidVariantName(componentName, sectionName, variantName, availableVariantNames))
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.variantName = variantName;
			this.availableVariantNames = availableVariantNames;
		}

		// Token: 0x060081E0 RID: 33248 RVA: 0x001A74A0 File Offset: 0x001A56A0
		public SettingOverrideInvalidVariantNameException(string componentName, string sectionName, string variantName, string availableVariantNames, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideInvalidVariantName(componentName, sectionName, variantName, availableVariantNames), innerException)
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.variantName = variantName;
			this.availableVariantNames = availableVariantNames;
		}

		// Token: 0x060081E1 RID: 33249 RVA: 0x001A74D4 File Offset: 0x001A56D4
		protected SettingOverrideInvalidVariantNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.sectionName = (string)info.GetValue("sectionName", typeof(string));
			this.variantName = (string)info.GetValue("variantName", typeof(string));
			this.availableVariantNames = (string)info.GetValue("availableVariantNames", typeof(string));
		}

		// Token: 0x060081E2 RID: 33250 RVA: 0x001A756C File Offset: 0x001A576C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("componentName", this.componentName);
			info.AddValue("sectionName", this.sectionName);
			info.AddValue("variantName", this.variantName);
			info.AddValue("availableVariantNames", this.availableVariantNames);
		}

		// Token: 0x17002F12 RID: 12050
		// (get) Token: 0x060081E3 RID: 33251 RVA: 0x001A75C5 File Offset: 0x001A57C5
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F13 RID: 12051
		// (get) Token: 0x060081E4 RID: 33252 RVA: 0x001A75CD File Offset: 0x001A57CD
		public string SectionName
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x17002F14 RID: 12052
		// (get) Token: 0x060081E5 RID: 33253 RVA: 0x001A75D5 File Offset: 0x001A57D5
		public string VariantName
		{
			get
			{
				return this.variantName;
			}
		}

		// Token: 0x17002F15 RID: 12053
		// (get) Token: 0x060081E6 RID: 33254 RVA: 0x001A75DD File Offset: 0x001A57DD
		public string AvailableVariantNames
		{
			get
			{
				return this.availableVariantNames;
			}
		}

		// Token: 0x040055EC RID: 21996
		private readonly string componentName;

		// Token: 0x040055ED RID: 21997
		private readonly string sectionName;

		// Token: 0x040055EE RID: 21998
		private readonly string variantName;

		// Token: 0x040055EF RID: 21999
		private readonly string availableVariantNames;
	}
}
