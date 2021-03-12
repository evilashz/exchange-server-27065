using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200033B RID: 827
	public sealed class ComplianceContextMenu : ContextMenu
	{
		// Token: 0x06001F44 RID: 8004 RVA: 0x000B3AC0 File Offset: 0x000B1CC0
		public ComplianceContextMenu(UserContext userContext, string id) : base("divCmplM", userContext)
		{
			this.id = id;
			this.shouldScroll = true;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000B3ADC File Offset: 0x000B1CDC
		protected override void RenderExpandoData(TextWriter output)
		{
			output.Write(" sCA=\"");
			output.Write(this.id);
			output.Write("\"");
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x000B3B00 File Offset: 0x000B1D00
		protected override void RenderMenuItems(TextWriter output)
		{
			List<object> list = new List<object>();
			bool flag = false;
			bool flag2 = false;
			if (base.UserContext.IsIrmEnabled)
			{
				foreach (RmsTemplate rmsTemplate in this.userContext.ComplianceReader.RmsTemplateReader.GetRmsTemplates())
				{
					flag = true;
					if (rmsTemplate == RmsTemplate.InternetConfidential)
					{
						flag2 = true;
					}
					else if (rmsTemplate != RmsTemplate.DoNotForward)
					{
						list.Add(rmsTemplate);
					}
				}
				if (base.UserContext.ComplianceReader.RmsTemplateReader.TemplateAcquisitionFailed)
				{
					string additionalAttributes = " iCType=\"-1\"";
					base.RenderMenuItem(output, LocalizedStrings.GetNonEncoded(440044585), ThemeFileId.Clear, "divCPLA0", "0", false, additionalAttributes, null, null, null, null, false);
					return;
				}
			}
			foreach (ClassificationSummary item in base.UserContext.ComplianceReader.MessageClassificationReader.GetClassificationsForLocale(base.UserContext.UserCulture))
			{
				list.Add(item);
			}
			IComparer<object> comparer = new ComplianceContextMenu.DisplayNameComparer(base.UserContext.UserCulture);
			list.Sort(comparer);
			base.RenderMenuItem(output, 440044585, ThemeFileId.Clear, "divCPLA0", "0");
			if (flag)
			{
				this.RenderCompliance(output, RmsTemplate.DoNotForward);
				if (flag2)
				{
					this.RenderCompliance(output, RmsTemplate.InternetConfidential);
				}
				ContextMenu.RenderMenuDivider(output, "divCPLA_0");
			}
			for (int i = 0; i < list.Count; i++)
			{
				this.RenderCompliance(output, list[i]);
			}
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000B3CB4 File Offset: 0x000B1EB4
		private void RenderCompliance(TextWriter output, object compliance)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			ComplianceType complianceType = ComplianceType.Unknown;
			ClassificationSummary classificationSummary = compliance as ClassificationSummary;
			if (classificationSummary != null)
			{
				text = classificationSummary.ClassificationID.ToString();
				text2 = classificationSummary.DisplayName;
				complianceType = ComplianceType.MessageClassification;
			}
			RmsTemplate rmsTemplate = compliance as RmsTemplate;
			if (rmsTemplate != null)
			{
				text = rmsTemplate.Id.ToString();
				text2 = rmsTemplate.GetName(base.UserContext.UserCulture);
				complianceType = ComplianceType.RmsTemplate;
			}
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				string additionalAttributes = " iCType=\"" + (uint)complianceType + "\"";
				base.RenderMenuItem(output, text2, ThemeFileId.Clear, "divCPLA" + text, text, false, additionalAttributes, null, null, null, null, false);
			}
		}

		// Token: 0x040016C4 RID: 5828
		private const string ComplianceAction = "divCPLA";

		// Token: 0x040016C5 RID: 5829
		internal const string NoRestrictionComplianceId = "0";

		// Token: 0x040016C6 RID: 5830
		private string id;

		// Token: 0x0200033C RID: 828
		private class DisplayNameComparer : IComparer<object>
		{
			// Token: 0x06001F48 RID: 8008 RVA: 0x000B3D75 File Offset: 0x000B1F75
			public DisplayNameComparer(CultureInfo locale)
			{
				this.locale = locale;
			}

			// Token: 0x06001F49 RID: 8009 RVA: 0x000B3D84 File Offset: 0x000B1F84
			public int Compare(object object1, object object2)
			{
				return string.Compare(this.GetDisplayName(object1), this.GetDisplayName(object2), true, this.locale);
			}

			// Token: 0x06001F4A RID: 8010 RVA: 0x000B3DA0 File Offset: 0x000B1FA0
			private string GetDisplayName(object obj)
			{
				if (obj is ClassificationSummary)
				{
					return ((ClassificationSummary)obj).DisplayName;
				}
				if (obj is RmsTemplate)
				{
					return ((RmsTemplate)obj).GetName(this.locale);
				}
				return string.Empty;
			}

			// Token: 0x040016C7 RID: 5831
			private readonly CultureInfo locale;
		}
	}
}
