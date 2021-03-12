using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F0 RID: 496
	internal class TelephoneNumberPrompt : VariablePrompt<PhoneNumber>
	{
		// Token: 0x06000E91 RID: 3729 RVA: 0x000414D0 File Offset: 0x0003F6D0
		public TelephoneNumberPrompt()
		{
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000414D8 File Offset: 0x0003F6D8
		internal TelephoneNumberPrompt(string promptName, CultureInfo culture, PhoneNumber value) : base(promptName, culture, value)
		{
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000414E4 File Offset: 0x0003F6E4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"telephone",
				base.Config.PromptName,
				string.Empty,
				this.phoneNumber
			});
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0004152F File Offset: 0x0003F72F
		internal override string ToSsml()
		{
			return this.innerPrompt.ToSsml();
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0004153C File Offset: 0x0003F73C
		protected override void InternalInitialize()
		{
			this.phoneNumber = base.InitVal.ToDial;
			if (-1 != this.phoneNumber.IndexOf("@", StringComparison.InvariantCulture))
			{
				this.innerPrompt = new EmailAddressPrompt();
				this.innerPrompt.Initialize(base.Config, base.Culture, this.phoneNumber);
			}
			else if (TelephoneNumberPrompt.AtleastOneValidDigitRegex.IsMatch(this.phoneNumber))
			{
				this.innerPrompt = new DigitPrompt();
				this.innerPrompt.Initialize(base.Config, base.Culture, this.phoneNumber);
				PhoneNumber phoneNumber = null;
				if (PhoneNumber.TryParse(this.phoneNumber, out phoneNumber))
				{
					this.phoneNumber = phoneNumber.Number;
				}
			}
			else
			{
				this.innerPrompt = new EmailAddressPrompt();
				this.innerPrompt.Initialize(base.Config, base.Culture, this.phoneNumber);
			}
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, this.phoneNumber);
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, data, "TelephoneNumberPromp successfully intialized with text _PhoneNumber.", new object[0]);
		}

		// Token: 0x04000AF9 RID: 2809
		private static readonly Regex AtleastOneValidDigitRegex = new Regex("\\d", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000AFA RID: 2810
		private string phoneNumber;

		// Token: 0x04000AFB RID: 2811
		private VariablePrompt<string> innerPrompt;
	}
}
