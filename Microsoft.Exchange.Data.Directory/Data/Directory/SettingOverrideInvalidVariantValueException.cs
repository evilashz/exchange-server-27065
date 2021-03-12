using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B0A RID: 2826
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideInvalidVariantValueException : SettingOverrideException
	{
		// Token: 0x060081E7 RID: 33255 RVA: 0x001A75E5 File Offset: 0x001A57E5
		public SettingOverrideInvalidVariantValueException(string componentName, string sectionName, string variantName, string variantType, string format) : base(DirectoryStrings.ErrorSettingOverrideInvalidVariantValue(componentName, sectionName, variantName, variantType, format))
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.variantName = variantName;
			this.variantType = variantType;
			this.format = format;
		}

		// Token: 0x060081E8 RID: 33256 RVA: 0x001A761E File Offset: 0x001A581E
		public SettingOverrideInvalidVariantValueException(string componentName, string sectionName, string variantName, string variantType, string format, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideInvalidVariantValue(componentName, sectionName, variantName, variantType, format), innerException)
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.variantName = variantName;
			this.variantType = variantType;
			this.format = format;
		}

		// Token: 0x060081E9 RID: 33257 RVA: 0x001A765C File Offset: 0x001A585C
		protected SettingOverrideInvalidVariantValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.sectionName = (string)info.GetValue("sectionName", typeof(string));
			this.variantName = (string)info.GetValue("variantName", typeof(string));
			this.variantType = (string)info.GetValue("variantType", typeof(string));
			this.format = (string)info.GetValue("format", typeof(string));
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x001A7714 File Offset: 0x001A5914
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("componentName", this.componentName);
			info.AddValue("sectionName", this.sectionName);
			info.AddValue("variantName", this.variantName);
			info.AddValue("variantType", this.variantType);
			info.AddValue("format", this.format);
		}

		// Token: 0x17002F16 RID: 12054
		// (get) Token: 0x060081EB RID: 33259 RVA: 0x001A777E File Offset: 0x001A597E
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F17 RID: 12055
		// (get) Token: 0x060081EC RID: 33260 RVA: 0x001A7786 File Offset: 0x001A5986
		public string SectionName
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x17002F18 RID: 12056
		// (get) Token: 0x060081ED RID: 33261 RVA: 0x001A778E File Offset: 0x001A598E
		public string VariantName
		{
			get
			{
				return this.variantName;
			}
		}

		// Token: 0x17002F19 RID: 12057
		// (get) Token: 0x060081EE RID: 33262 RVA: 0x001A7796 File Offset: 0x001A5996
		public string VariantType
		{
			get
			{
				return this.variantType;
			}
		}

		// Token: 0x17002F1A RID: 12058
		// (get) Token: 0x060081EF RID: 33263 RVA: 0x001A779E File Offset: 0x001A599E
		public string Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040055F0 RID: 22000
		private readonly string componentName;

		// Token: 0x040055F1 RID: 22001
		private readonly string sectionName;

		// Token: 0x040055F2 RID: 22002
		private readonly string variantName;

		// Token: 0x040055F3 RID: 22003
		private readonly string variantType;

		// Token: 0x040055F4 RID: 22004
		private readonly string format;
	}
}
