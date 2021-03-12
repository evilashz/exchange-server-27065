using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000121 RID: 289
	internal class DisambiguatedNamePrompt : VariablePrompt<DisambiguatedName>
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x000224BC File Offset: 0x000206BC
		public override string ToString()
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", new object[]
			{
				this.disambiguatedName.Name,
				this.disambiguatedName.DisambiguationText
			});
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"disambiguatedName",
				base.Config.PromptName,
				string.Empty,
				text
			});
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00022536 File Offset: 0x00020736
		internal override string ToSsml()
		{
			return this.namePrompt.ToSsml() + this.disambiguationTextPrompt.ToSsml();
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00022554 File Offset: 0x00020754
		protected override void InternalInitialize()
		{
			this.disambiguatedName = base.InitVal;
			if (this.disambiguatedName == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Wrong type = {0}", new object[]
				{
					(base.InitVal == null) ? "<null>" : base.InitVal.GetType().ToString()
				}), "initValue");
			}
			this.namePrompt = new NamePrompt();
			this.namePrompt.Initialize(base.Config, base.Culture, Strings.DisambiguatedNamePrefix(this.disambiguatedName.Name).ToString(base.Culture));
			this.disambiguationTextPrompt = null;
			switch (this.disambiguatedName.DisambiguationField)
			{
			case DisambiguationFieldEnum.Title:
				this.disambiguationTextPrompt = new TextPrompt();
				break;
			case DisambiguationFieldEnum.Department:
				this.disambiguationTextPrompt = new TextPrompt();
				break;
			case DisambiguationFieldEnum.Location:
				this.disambiguationTextPrompt = new AddressPrompt();
				break;
			default:
				CallIdTracer.TraceError(ExTraceGlobals.StateMachineTracer, this, "Invalid value for initValue.DisambiguationField = {0}.", new object[]
				{
					this.disambiguatedName.DisambiguationField
				});
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid value = {0}", new object[]
				{
					this.disambiguatedName.DisambiguationField
				}), "initValue.DisambiguationField");
			}
			this.disambiguationTextPrompt.Initialize(base.Config, base.Culture, this.disambiguatedName.DisambiguationText);
		}

		// Token: 0x04000887 RID: 2183
		private DisambiguatedName disambiguatedName;

		// Token: 0x04000888 RID: 2184
		private NamePrompt namePrompt;

		// Token: 0x04000889 RID: 2185
		private TextPrompt disambiguationTextPrompt;
	}
}
