using System;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E5 RID: 229
	[DataContract]
	public abstract class RecipientFilter : ViewDropDownFilter
	{
		// Token: 0x06001E2B RID: 7723 RVA: 0x0005B468 File Offset: 0x00059668
		protected RecipientFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0005B48C File Offset: 0x0005968C
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["Properties"] = "PrimarySmtpAddress,DisplayName,ArchiveGuid,AuthenticationType,RecipientType,RecipientTypeDetails,ResourceType,WindowsLiveID,Identity,ExchangeVersion,OrganizationId";
			RecipientTypeDetails[] recipientTypeDetailsParam = this.RecipientTypeDetailsParam;
			if (recipientTypeDetailsParam != null && recipientTypeDetailsParam.Length > 0)
			{
				this.RecipientTypeDetailsList = recipientTypeDetailsParam;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0005B4C0 File Offset: 0x000596C0
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Get-Recipient";
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0005B4C7 File Offset: 0x000596C7
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x0005B4D9 File Offset: 0x000596D9
		public RecipientTypeDetails[] RecipientTypeDetailsList
		{
			get
			{
				return (RecipientTypeDetails[])base["RecipientTypeDetails"];
			}
			protected set
			{
				base["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x06001E30 RID: 7728
		protected abstract RecipientTypeDetails[] RecipientTypeDetailsParam { get; }

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x0005B4E7 File Offset: 0x000596E7
		protected override string[] FilterableProperties
		{
			get
			{
				return RecipientFilter.filterableProperties;
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0005B4EE File Offset: 0x000596EE
		protected override void UpdateFilterProperty()
		{
			base["Filter"] = this.TranslateToAnr();
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0005B504 File Offset: 0x00059704
		private string TranslateToAnr()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.TranslateBasicFilter());
			if (!string.IsNullOrEmpty(base.SearchText))
			{
				string text = base.SearchText.Trim();
				int num = text.Trim().IndexOf(" ");
				if (num > 0)
				{
					string text2 = text.Substring(0, num);
					string text3 = text.Substring(num + 1, text.Length - 1 - num);
					TextFilter textFilter = new TextFilter(OrgPersonPresentationObjectSchema.FirstName, text2, MatchOptions.Prefix, MatchFlags.Default);
					TextFilter textFilter2 = new TextFilter(OrgPersonPresentationObjectSchema.LastName, text3, MatchOptions.Prefix, MatchFlags.Default);
					CompositeFilter compositeFilter = new AndFilter(new QueryFilter[]
					{
						textFilter,
						textFilter2
					});
					QueryFilter queryFilter = new TextFilter(OrgPersonPresentationObjectSchema.FirstName, text3, MatchOptions.Prefix, MatchFlags.Default);
					QueryFilter queryFilter2 = new TextFilter(OrgPersonPresentationObjectSchema.LastName, text2, MatchOptions.Prefix, MatchFlags.Default);
					CompositeFilter compositeFilter2 = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter2
					});
					stringBuilder.Append(" -or ");
					stringBuilder.Append(new OrFilter(new QueryFilter[]
					{
						compositeFilter,
						compositeFilter2
					}).GenerateInfixString(FilterLanguage.Monad));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001C07 RID: 7175
		public new const string RbacParameters = "?ResultSize&Filter&RecipientTypeDetails&Properties";

		// Token: 0x04001C08 RID: 7176
		private static string[] filterableProperties = new string[]
		{
			"DisplayName",
			"Alias",
			OrgPersonPresentationObjectSchema.FirstName.Name,
			OrgPersonPresentationObjectSchema.LastName.Name
		};
	}
}
