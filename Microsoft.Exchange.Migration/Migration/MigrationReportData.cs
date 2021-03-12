using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000135 RID: 309
	internal class MigrationReportData
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x000428FB File Offset: 0x00040AFB
		public MigrationReportData(MigrationJobReportingCursor reportingContext, string emailSubject, string templateName, string licensingHelpUrl)
		{
			this.ReportingCursor = reportingContext;
			this.EmailSubject = emailSubject;
			this.TemplateName = templateName;
			this.LicensingHelpUrl = licensingHelpUrl;
			this.ReportUrls = null;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00042927 File Offset: 0x00040B27
		public MigrationReportData(MigrationJobReportingCursor reportingContext, MigrationJobTemplateDataGeneratorDelegate templateDataGenerator, string emailSubject, string templateName, string licensingHelpUrl)
		{
			this.ReportingCursor = reportingContext;
			this.TemplateDataGenerator = templateDataGenerator;
			this.EmailSubject = emailSubject;
			this.TemplateName = templateName;
			this.LicensingHelpUrl = licensingHelpUrl;
			this.ReportUrls = null;
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0004295B File Offset: 0x00040B5B
		// (set) Token: 0x06000F90 RID: 3984 RVA: 0x00042963 File Offset: 0x00040B63
		public MigrationJobReportingCursor ReportingCursor { get; private set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0004296C File Offset: 0x00040B6C
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x00042974 File Offset: 0x00040B74
		public string EmailSubject { get; private set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0004297D File Offset: 0x00040B7D
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x00042985 File Offset: 0x00040B85
		public string TemplateName { get; private set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0004298E File Offset: 0x00040B8E
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00042996 File Offset: 0x00040B96
		public string LicensingHelpUrl { get; private set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0004299F File Offset: 0x00040B9F
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x000429A7 File Offset: 0x00040BA7
		public MigrationReportSet ReportUrls { get; internal set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000429B0 File Offset: 0x00040BB0
		public bool IsIncludeFailureReportLink
		{
			get
			{
				return this.ReportingCursor.MigrationErrorCount.GetTotal() > 0;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x000429C5 File Offset: 0x00040BC5
		public bool IsIncludeSuccessReportLink
		{
			get
			{
				return this.ReportingCursor.MigrationSuccessCount.GetTotal() > 0;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x000429DA File Offset: 0x00040BDA
		public int PartialMigrationCount
		{
			get
			{
				return this.ReportingCursor.PartialMigrationCounts;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x000429E7 File Offset: 0x00040BE7
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x000429EF File Offset: 0x00040BEF
		public MigrationJobTemplateDataGeneratorDelegate TemplateDataGenerator { get; set; }

		// Token: 0x06000F9E RID: 3998 RVA: 0x000429F8 File Offset: 0x00040BF8
		public string ComposeBodyFromTemplate(IEnumerable<KeyValuePair<string, string>> bodyData)
		{
			return MigrationReportData.ComposeBodyFromTemplate(this.TemplateName, bodyData);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00042A08 File Offset: 0x00040C08
		private static string ComposeBodyFromTemplate(string templateName, IEnumerable<KeyValuePair<string, string>> bodyData)
		{
			StringBuilder stringBuilder = new StringBuilder(MigrationReportGenerator.GetTemplate(templateName));
			foreach (KeyValuePair<string, string> keyValuePair in bodyData)
			{
				stringBuilder.Replace(keyValuePair.Key, keyValuePair.Value);
			}
			string text = stringBuilder.ToString();
			if (MigrationReportData.TemplateMarkerRegex.IsMatch(text))
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "The body data specified did not have enough information to fill out the template. Template was: {0}. The body looks like this: {1}", new object[]
				{
					templateName,
					text
				});
				MigrationApplication.NotifyOfCriticalError(new InvalidOperationException(), text2);
				throw new MigrationDataCorruptionException(text2);
			}
			return text;
		}

		// Token: 0x0400057D RID: 1405
		private static readonly Regex TemplateMarkerRegex = new Regex("{.*}", RegexOptions.Compiled);
	}
}
