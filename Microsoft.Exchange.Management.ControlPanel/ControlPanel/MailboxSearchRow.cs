using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.DDIService;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003D7 RID: 983
	[DataContract]
	public class MailboxSearchRow : BaseRow
	{
		// Token: 0x06003259 RID: 12889 RVA: 0x0009CA8A File Offset: 0x0009AC8A
		public MailboxSearchRow(MailboxSearchObject searchObject) : base(new Identity(searchObject.Identity, searchObject.Name), searchObject)
		{
			this.MailboxSearch = searchObject;
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x0009CAAB File Offset: 0x0009ACAB
		// (set) Token: 0x0600325B RID: 12891 RVA: 0x0009CAB3 File Offset: 0x0009ACB3
		public MailboxSearchObject MailboxSearch { get; set; }

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x0009CABC File Offset: 0x0009ACBC
		// (set) Token: 0x0600325D RID: 12893 RVA: 0x0009CAC9 File Offset: 0x0009ACC9
		[DataMember]
		public string Name
		{
			get
			{
				return this.MailboxSearch.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x0009CAD0 File Offset: 0x0009ACD0
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x0009CAE2 File Offset: 0x0009ACE2
		[DataMember]
		public string LastStartTime
		{
			get
			{
				return this.MailboxSearch.LastStartTime.ToUserDateTimeString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x0009CAE9 File Offset: 0x0009ACE9
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x0009CB0A File Offset: 0x0009AD0A
		[DataMember]
		public string Status
		{
			get
			{
				return LocalizedDescriptionAttribute.FromEnum(typeof(SearchState), this.MailboxSearch.Status);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x0009CB14 File Offset: 0x0009AD14
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x0009CB86 File Offset: 0x0009AD86
		[DataMember]
		public string DisplayStatus
		{
			get
			{
				string text = string.Empty;
				text = LocalizedDescriptionAttribute.FromEnum(typeof(SearchState), this.MailboxSearch.Status);
				if (!this.MailboxSearch.Status.Equals(SearchState.InProgress))
				{
					return text;
				}
				return string.Format(Strings.MailboxSearchInProgressStatus, text, this.MailboxSearch.PercentComplete);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x0009CB8D File Offset: 0x0009AD8D
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x0009CBAC File Offset: 0x0009ADAC
		[DataMember]
		public bool IsStoppable
		{
			get
			{
				return this.MailboxSearch.Status == SearchState.InProgress || SearchState.EstimateInProgress == this.MailboxSearch.Status;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x0009CBB4 File Offset: 0x0009ADB4
		// (set) Token: 0x06003267 RID: 12903 RVA: 0x0009CC10 File Offset: 0x0009AE10
		[DataMember]
		public bool IsStartable
		{
			get
			{
				if (this.MailboxSearch.EstimateOnly)
				{
					return SearchState.EstimateInProgress != this.MailboxSearch.Status && SearchState.EstimateStopping != this.MailboxSearch.Status;
				}
				return this.MailboxSearch.Status != SearchState.InProgress && SearchState.Stopping != this.MailboxSearch.Status;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x0009CC17 File Offset: 0x0009AE17
		// (set) Token: 0x06003269 RID: 12905 RVA: 0x0009CC4D File Offset: 0x0009AE4D
		[DataMember]
		public bool IsFullStatsSearchAllowed
		{
			get
			{
				return !this.IsKeywordStatisticsDisabled && !string.IsNullOrEmpty(this.MailboxSearch.SearchQuery) && !this.MailboxSearch.IncludeKeywordStatistics && this.MailboxSearch.EstimateOnly;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x0009CC54 File Offset: 0x0009AE54
		// (set) Token: 0x0600326B RID: 12907 RVA: 0x0009CC61 File Offset: 0x0009AE61
		[DataMember]
		public bool IsKeywordStatisticsDisabled
		{
			get
			{
				return this.MailboxSearch.KeywordStatisticsDisabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x0009CC68 File Offset: 0x0009AE68
		// (set) Token: 0x0600326D RID: 12909 RVA: 0x0009CC7A File Offset: 0x0009AE7A
		[DataMember]
		public string Icon
		{
			get
			{
				return MailboxSearchRow.FromEnum(this.MailboxSearch.Status);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x0009CC81 File Offset: 0x0009AE81
		// (set) Token: 0x0600326F RID: 12911 RVA: 0x0009CC91 File Offset: 0x0009AE91
		[DataMember]
		public bool IsResumable
		{
			get
			{
				return this.MailboxSearch.Status == SearchState.PartiallySucceeded;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x0009CC98 File Offset: 0x0009AE98
		// (set) Token: 0x06003271 RID: 12913 RVA: 0x0009CCA5 File Offset: 0x0009AEA5
		[DataMember]
		public bool IsEstimateOnly
		{
			get
			{
				return this.MailboxSearch.EstimateOnly;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x0009CCAC File Offset: 0x0009AEAC
		// (set) Token: 0x06003273 RID: 12915 RVA: 0x0009CCBC File Offset: 0x0009AEBC
		[DataMember]
		public bool IsPreviewable
		{
			get
			{
				return !string.IsNullOrEmpty(this.PreviewResultsLink);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x0009CCC3 File Offset: 0x0009AEC3
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x0009CCD0 File Offset: 0x0009AED0
		[DataMember]
		public string PreviewResultsLink
		{
			get
			{
				return this.MailboxSearch.PreviewResultsLink;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x0009CCD7 File Offset: 0x0009AED7
		// (set) Token: 0x06003277 RID: 12919 RVA: 0x0009CD0E File Offset: 0x0009AF0E
		[DataMember]
		public string HoldStatusDescription
		{
			get
			{
				if (RbacPrincipal.Current.IsInRole("LegalHold"))
				{
					return this.MailboxSearch.InPlaceHoldEnabled ? Strings.DiscoveryHoldHoldStatusYes : Strings.DiscoveryHoldHoldStatusNo;
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x0009CD15 File Offset: 0x0009AF15
		// (set) Token: 0x06003279 RID: 12921 RVA: 0x0009CD27 File Offset: 0x0009AF27
		[DataMember]
		public string CreatedByDisplayName
		{
			get
			{
				return DiscoveryHoldPropertiesHelper.GetCreatedByUserDisplayName(this.MailboxSearch.CreatedBy);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x0009CD2E File Offset: 0x0009AF2E
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x0009CD40 File Offset: 0x0009AF40
		[DataMember]
		public string LastModifiedTimeDisplay
		{
			get
			{
				return this.MailboxSearch.LastModifiedTime.ToUserDateTimeString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x0009CD48 File Offset: 0x0009AF48
		// (set) Token: 0x0600327D RID: 12925 RVA: 0x0009CD8B File Offset: 0x0009AF8B
		[DataMember]
		public DateTime LastModifiedUTCDateTime
		{
			get
			{
				if (this.MailboxSearch.LastModifiedTime == null)
				{
					return DateTime.MinValue;
				}
				return this.MailboxSearch.LastModifiedTime.Value.UniversalTime;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x0009CD94 File Offset: 0x0009AF94
		// (set) Token: 0x0600327F RID: 12927 RVA: 0x0009CDF4 File Offset: 0x0009AFF4
		[DataMember]
		public string ExportToolUrl
		{
			get
			{
				string newValue = string.Empty;
				if (MailboxSearchRow.LocalServer != null && !string.IsNullOrEmpty(MailboxSearchRow.LocalServer.Fqdn))
				{
					newValue = MailboxSearchRow.LocalServer.Fqdn;
				}
				return ThemeResource.ExportToolPath.Replace("{0}", newValue) + "microsoft.exchange.ediscovery.exporttool.application?name=" + Uri.EscapeDataString(this.Name) + "&ews=";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x0009CDFC File Offset: 0x0009AFFC
		internal static string FromEnum(SearchState status)
		{
			string result = string.Empty;
			switch (status)
			{
			case SearchState.InProgress:
			case SearchState.EstimateInProgress:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.MailboxSearchInProgress);
				break;
			case SearchState.Failed:
			case SearchState.EstimateFailed:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.MailboxSearchFailed);
				break;
			case SearchState.Stopping:
			case SearchState.EstimateStopping:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.MailboxSearchStopping);
				break;
			case SearchState.Stopped:
			case SearchState.EstimateStopped:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.MailboxSearchStopped);
				break;
			case SearchState.Succeeded:
			case SearchState.EstimateSucceeded:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.MailboxSearchSucceeded);
				break;
			case SearchState.PartiallySucceeded:
			case SearchState.EstimatePartiallySucceeded:
				result = CommandSprite.GetCssClass(CommandSprite.SpriteId.MailboxSearchPartiallySucceeded);
				break;
			}
			return result;
		}

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x0009CE90 File Offset: 0x0009B090
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x0009CECA File Offset: 0x0009B0CA
		[DataMember]
		public string ResultSize
		{
			get
			{
				if (this.MailboxSearch.EstimateOnly)
				{
					return this.MailboxSearch.ResultSizeEstimate.ToAppropriateUnitFormatString("{0:0.##}");
				}
				return this.MailboxSearch.ResultSize.ToAppropriateUnitFormatString("{0:0.##}");
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x0009CED1 File Offset: 0x0009B0D1
		private static Server LocalServer
		{
			get
			{
				if (MailboxSearchRow.localServer == null)
				{
					MailboxSearchRow.localServer = MailboxSearchRow.GetLocalServer();
				}
				return MailboxSearchRow.localServer;
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x0009CEEC File Offset: 0x0009B0EC
		private static Server GetLocalServer()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 396, "GetLocalServer", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Reporting\\MailboxSearches.cs");
			topologyConfigurationSession.UseConfigNC = true;
			topologyConfigurationSession.UseGlobalCatalog = true;
			return topologyConfigurationSession.FindLocalServer();
		}

		// Token: 0x0400248C RID: 9356
		private static Server localServer;
	}
}
