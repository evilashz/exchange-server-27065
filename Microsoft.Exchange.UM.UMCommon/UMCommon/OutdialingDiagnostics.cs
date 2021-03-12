using System;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon.Outdialing;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000D9 RID: 217
	internal class OutdialingDiagnostics
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001C358 File Offset: 0x0001A558
		internal bool HasEvents
		{
			get
			{
				return this.detailBuilder.Length > 0;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001C368 File Offset: 0x0001A568
		internal static void ValidateProperties()
		{
			Type typeFromHandle = typeof(UMDialPlan);
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			string[] names = Enum.GetNames(typeof(OutdialingDiagnostics.DialPlanProperty));
			foreach (string text in names)
			{
				if (typeFromHandle.GetProperty(text, bindingAttr) == null)
				{
					throw new MissingMemberException(typeFromHandle.Name, text);
				}
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		internal void AddSkipDialPlanDetail()
		{
			this.skipDialPlan = true;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001C3D5 File Offset: 0x0001A5D5
		internal void AddPropertyNotSetDetail(UMDialPlan dialPlan, OutdialingDiagnostics.DialPlanProperty property)
		{
			this.detailBuilder.Append("\r\n");
			this.detailBuilder.Append(Strings.DialPlanPropertyNotSet(property.ToString(), dialPlan.Name));
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001C410 File Offset: 0x0001A610
		internal void AddNumberNotInStandardFormat(ADRecipient recipient)
		{
			this.detailBuilder.Append("\r\n");
			if (recipient != null)
			{
				this.detailBuilder.Append(Strings.NumberNotInStandardFormat(recipient.Name));
				return;
			}
			this.detailBuilder.Append(Strings.NumberNotInStandardFormatNoRecipient);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001C464 File Offset: 0x0001A664
		internal void AddInvalidRecipientPhoneLength(IADRecipient recipient, string dialPlanName)
		{
			string recipient2 = (recipient != null) ? recipient.Name : string.Empty;
			this.detailBuilder.Append("\r\n");
			this.detailBuilder.Append(Strings.InvalidRecipientPhoneLength(recipient2, dialPlanName));
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001C4AB File Offset: 0x0001A6AB
		internal void AddInvalidPlayOnPhoneNumber(string phoneNumber)
		{
			this.detailBuilder.Append("\r\n");
			this.detailBuilder.Append(Strings.InvalidPlayOnPhoneNumber(phoneNumber));
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001C4D5 File Offset: 0x0001A6D5
		internal void AddAccessCheckFailed(string phoneNumber)
		{
			this.detailBuilder.Append("\r\n");
			this.detailBuilder.Append(Strings.AccessCheckFailed(phoneNumber));
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001C4FF File Offset: 0x0001A6FF
		internal string GetDetails()
		{
			return this.detailBuilder.ToString();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001C50C File Offset: 0x0001A70C
		internal void LogOutdialingWarning(PhoneNumber inputNumber, PhoneNumber outputNumber)
		{
			if (this.skipDialPlan)
			{
				this.detailBuilder.Append("\r\n");
				this.detailBuilder.Append(Strings.SkippingTargetDialPlan);
			}
			string text;
			if (outputNumber != null)
			{
				text = Strings.CanonicalizationResult(outputNumber.ToDial);
			}
			else
			{
				text = Strings.CanonicalizationFailed;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_OutdialingConfigurationWarning, null, new object[]
			{
				(inputNumber != null) ? inputNumber.ToDial : string.Empty,
				text,
				this.detailBuilder.ToString()
			});
		}

		// Token: 0x04000413 RID: 1043
		private bool skipDialPlan;

		// Token: 0x04000414 RID: 1044
		private StringBuilder detailBuilder = new StringBuilder(64);

		// Token: 0x020000DA RID: 218
		internal enum DialPlanProperty
		{
			// Token: 0x04000416 RID: 1046
			InternationalAccessCode,
			// Token: 0x04000417 RID: 1047
			NationalNumberPrefix,
			// Token: 0x04000418 RID: 1048
			InCountryOrRegionNumberFormat,
			// Token: 0x04000419 RID: 1049
			InternationalNumberFormat,
			// Token: 0x0400041A RID: 1050
			OutsideLineAccessCode,
			// Token: 0x0400041B RID: 1051
			CountryOrRegionCode
		}
	}
}
