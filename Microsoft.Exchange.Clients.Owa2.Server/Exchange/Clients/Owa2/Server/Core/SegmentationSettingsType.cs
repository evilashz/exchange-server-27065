using System;
using System.Runtime.Serialization;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000400 RID: 1024
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SegmentationSettingsType
	{
		// Token: 0x06002176 RID: 8566 RVA: 0x0007A16A File Offset: 0x0007836A
		public SegmentationSettingsType(ulong segmentationFlags)
		{
			this.segmentationFlags = segmentationFlags;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0007A179 File Offset: 0x00078379
		internal SegmentationSettingsType(ConfigurationContext configurationContext)
		{
			this.configurationContext = configurationContext;
			this.segmentationFlags = this.configurationContext.SegmentationFlags;
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x0007A199 File Offset: 0x00078399
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x0007A1A3 File Offset: 0x000783A3
		[DataMember]
		public bool GlobalAddressList
		{
			get
			{
				return this.IsFeatureEnabled(Feature.GlobalAddressList);
			}
			set
			{
				this.SetFeatureEnabled(Feature.GlobalAddressList, value);
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x0007A1B0 File Offset: 0x000783B0
		// (set) Token: 0x0600217B RID: 8571 RVA: 0x0007A209 File Offset: 0x00078409
		[DataMember]
		public bool ReportJunkEmailEnabled
		{
			get
			{
				if (this.configurationContext == null)
				{
					return false;
				}
				UserContext userContext = UserContextManager.GetMailboxContext(HttpContext.Current, null, true) as UserContext;
				return userContext != null && userContext.FeaturesManager.ServerSettings.ReportJunk.Enabled && this.IsFeatureEnabled(Feature.ReportJunkEmail);
			}
			set
			{
				this.SetFeatureEnabled(Feature.ReportJunkEmail, value);
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x0007A21B File Offset: 0x0007841B
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x0007A225 File Offset: 0x00078425
		[DataMember]
		public bool Calendar
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Calendar);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Calendar, value);
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0007A230 File Offset: 0x00078430
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x0007A23A File Offset: 0x0007843A
		[DataMember]
		public bool Contacts
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Contacts);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Contacts, value);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0007A245 File Offset: 0x00078445
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x0007A24F File Offset: 0x0007844F
		[DataMember]
		public bool Tasks
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Tasks);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Tasks, value);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0007A25A File Offset: 0x0007845A
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x0007A264 File Offset: 0x00078464
		[DataMember]
		public bool Todos
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Tasks);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Tasks, value);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0007A26F File Offset: 0x0007846F
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x0007A27A File Offset: 0x0007847A
		[DataMember]
		public bool Journal
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Journal);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Journal, value);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0007A286 File Offset: 0x00078486
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x0007A291 File Offset: 0x00078491
		[DataMember]
		public bool StickyNotes
		{
			get
			{
				return this.IsFeatureEnabled(Feature.StickyNotes);
			}
			set
			{
				this.SetFeatureEnabled(Feature.StickyNotes, value);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0007A29D File Offset: 0x0007849D
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0007A2A8 File Offset: 0x000784A8
		[DataMember]
		public bool PublicFolders
		{
			get
			{
				return this.IsFeatureEnabled(Feature.PublicFolders);
			}
			set
			{
				this.SetFeatureEnabled(Feature.PublicFolders, value);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0007A2B4 File Offset: 0x000784B4
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0007A2C2 File Offset: 0x000784C2
		[DataMember]
		public bool Organization
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Organization);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Organization, value);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0007A2D1 File Offset: 0x000784D1
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0007A2DF File Offset: 0x000784DF
		[DataMember]
		public bool Notifications
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Notifications);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Notifications, value);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0007A2EE File Offset: 0x000784EE
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x0007A2FC File Offset: 0x000784FC
		[DataMember]
		public bool SpellChecker
		{
			get
			{
				return this.IsFeatureEnabled(Feature.SpellChecker);
			}
			set
			{
				this.SetFeatureEnabled(Feature.SpellChecker, value);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x0007A30B File Offset: 0x0007850B
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x0007A319 File Offset: 0x00078519
		[DataMember]
		public bool SMime
		{
			get
			{
				return this.IsFeatureEnabled(Feature.SMime);
			}
			set
			{
				this.SetFeatureEnabled(Feature.SMime, value);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x0007A328 File Offset: 0x00078528
		// (set) Token: 0x06002193 RID: 8595 RVA: 0x0007A336 File Offset: 0x00078536
		[DataMember]
		public bool SearchFolders
		{
			get
			{
				return this.IsFeatureEnabled(Feature.SearchFolders);
			}
			set
			{
				this.SetFeatureEnabled(Feature.SearchFolders, value);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002194 RID: 8596 RVA: 0x0007A345 File Offset: 0x00078545
		// (set) Token: 0x06002195 RID: 8597 RVA: 0x0007A353 File Offset: 0x00078553
		[DataMember]
		public bool Signature
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Signature);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Signature, value);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0007A362 File Offset: 0x00078562
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x0007A370 File Offset: 0x00078570
		[DataMember]
		public bool Rules
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Rules);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Rules, value);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002198 RID: 8600 RVA: 0x0007A37F File Offset: 0x0007857F
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x0007A38D File Offset: 0x0007858D
		[DataMember]
		public bool Themes
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Themes);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Themes, value);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0007A39C File Offset: 0x0007859C
		// (set) Token: 0x0600219B RID: 8603 RVA: 0x0007A3AA File Offset: 0x000785AA
		[DataMember]
		public bool JunkEMail
		{
			get
			{
				return this.IsFeatureEnabled(Feature.JunkEMail);
			}
			set
			{
				this.SetFeatureEnabled(Feature.JunkEMail, value);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0007A3B9 File Offset: 0x000785B9
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x0007A3C7 File Offset: 0x000785C7
		[DataMember]
		public bool UmIntegration
		{
			get
			{
				return this.IsFeatureEnabled(Feature.UMIntegration);
			}
			set
			{
				this.SetFeatureEnabled(Feature.UMIntegration, value);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600219E RID: 8606 RVA: 0x0007A3D6 File Offset: 0x000785D6
		// (set) Token: 0x0600219F RID: 8607 RVA: 0x0007A3E4 File Offset: 0x000785E4
		[DataMember]
		public bool EasMobileOptions
		{
			get
			{
				return this.IsFeatureEnabled(Feature.EasMobileOptions);
			}
			set
			{
				this.SetFeatureEnabled(Feature.EasMobileOptions, value);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x0007A3F3 File Offset: 0x000785F3
		// (set) Token: 0x060021A1 RID: 8609 RVA: 0x0007A401 File Offset: 0x00078601
		[DataMember]
		public bool ExplicitLogon
		{
			get
			{
				return this.IsFeatureEnabled(Feature.ExplicitLogon);
			}
			set
			{
				this.SetFeatureEnabled(Feature.ExplicitLogon, value);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0007A410 File Offset: 0x00078610
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x0007A41E File Offset: 0x0007861E
		[DataMember]
		public bool AddressLists
		{
			get
			{
				return this.IsFeatureEnabled(Feature.AddressLists);
			}
			set
			{
				this.SetFeatureEnabled(Feature.AddressLists, value);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060021A4 RID: 8612 RVA: 0x0007A42D File Offset: 0x0007862D
		// (set) Token: 0x060021A5 RID: 8613 RVA: 0x0007A43B File Offset: 0x0007863B
		[DataMember]
		public bool Dumpster
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Dumpster);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Dumpster, value);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060021A6 RID: 8614 RVA: 0x0007A44A File Offset: 0x0007864A
		// (set) Token: 0x060021A7 RID: 8615 RVA: 0x0007A458 File Offset: 0x00078658
		[DataMember]
		public bool ChangePassword
		{
			get
			{
				return this.IsFeatureEnabled(Feature.ChangePassword);
			}
			set
			{
				this.SetFeatureEnabled(Feature.ChangePassword, value);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0007A467 File Offset: 0x00078667
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x0007A475 File Offset: 0x00078675
		[DataMember]
		public bool InstantMessage
		{
			get
			{
				return this.IsFeatureEnabled(Feature.InstantMessage);
			}
			set
			{
				this.SetFeatureEnabled(Feature.InstantMessage, value);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0007A484 File Offset: 0x00078684
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x0007A492 File Offset: 0x00078692
		[DataMember]
		public bool TextMessage
		{
			get
			{
				return this.IsFeatureEnabled(Feature.TextMessage);
			}
			set
			{
				this.SetFeatureEnabled(Feature.TextMessage, value);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x0007A4A1 File Offset: 0x000786A1
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x0007A4AF File Offset: 0x000786AF
		[DataMember]
		public bool DelegateAccess
		{
			get
			{
				return this.IsFeatureEnabled(Feature.DelegateAccess);
			}
			set
			{
				this.SetFeatureEnabled(Feature.DelegateAccess, value);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x0007A4BE File Offset: 0x000786BE
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x0007A4CC File Offset: 0x000786CC
		[DataMember]
		public bool Irm
		{
			get
			{
				return this.IsFeatureEnabled((Feature)int.MinValue);
			}
			set
			{
				this.SetFeatureEnabled((Feature)int.MinValue, value);
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0007A4DB File Offset: 0x000786DB
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x0007A4EC File Offset: 0x000786EC
		[DataMember]
		public bool ForceSaveAttachmentFiltering
		{
			get
			{
				return this.IsFeatureEnabled(Feature.ForceSaveAttachmentFiltering);
			}
			set
			{
				this.SetFeatureEnabled(Feature.ForceSaveAttachmentFiltering, value);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0007A4FE File Offset: 0x000786FE
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x0007A50F File Offset: 0x0007870F
		[DataMember]
		public bool Silverlight
		{
			get
			{
				return this.IsFeatureEnabled(Feature.Silverlight);
			}
			set
			{
				this.SetFeatureEnabled(Feature.Silverlight, value);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0007A521 File Offset: 0x00078721
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0007A532 File Offset: 0x00078732
		[DataMember]
		public bool DisplayPhotos
		{
			get
			{
				return this.IsFeatureEnabled(Feature.DisplayPhotos);
			}
			set
			{
				this.SetFeatureEnabled(Feature.DisplayPhotos, value);
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0007A544 File Offset: 0x00078744
		// (set) Token: 0x060021B7 RID: 8631 RVA: 0x0007A555 File Offset: 0x00078755
		[DataMember]
		public bool SetPhoto
		{
			get
			{
				return this.IsFeatureEnabled(Feature.SetPhoto);
			}
			set
			{
				this.SetFeatureEnabled(Feature.SetPhoto, value);
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0007A567 File Offset: 0x00078767
		// (set) Token: 0x060021B9 RID: 8633 RVA: 0x0007A578 File Offset: 0x00078778
		[DataMember]
		public bool PredictedActions
		{
			get
			{
				return this.IsFeatureEnabled(Feature.PredictedActions);
			}
			set
			{
				this.SetFeatureEnabled(Feature.PredictedActions, value);
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0007A58A File Offset: 0x0007878A
		// (set) Token: 0x060021BB RID: 8635 RVA: 0x0007A59B File Offset: 0x0007879B
		[DataMember]
		public bool UserDiagnosticEnabled
		{
			get
			{
				return this.IsFeatureEnabled(Feature.UserDiagnosticEnabled);
			}
			set
			{
				this.SetFeatureEnabled(Feature.UserDiagnosticEnabled, value);
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x0007A5AD File Offset: 0x000787AD
		// (set) Token: 0x060021BD RID: 8637 RVA: 0x0007A5BE File Offset: 0x000787BE
		[DataMember]
		public bool SkipCreateUnifiedGroupCustomSharepointClassification
		{
			get
			{
				return this.IsFeatureEnabled(Feature.SkipCreateUnifiedGroupCustomSharepointClassification);
			}
			set
			{
				this.SetFeatureEnabled(Feature.SkipCreateUnifiedGroupCustomSharepointClassification, value);
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0007A5D0 File Offset: 0x000787D0
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x0007A5E1 File Offset: 0x000787E1
		[DataMember]
		public bool GroupCreationEnabled
		{
			get
			{
				return this.IsFeatureEnabled(Feature.GroupCreationEnabled);
			}
			set
			{
				this.SetFeatureEnabled(Feature.GroupCreationEnabled, value);
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0007A5F3 File Offset: 0x000787F3
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x0007A604 File Offset: 0x00078804
		[DataMember]
		public bool FacebookEnabled
		{
			get
			{
				return this.IsFeatureEnabled(Feature.FacebookEnabled);
			}
			set
			{
				this.SetFeatureEnabled(Feature.FacebookEnabled, value);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0007A616 File Offset: 0x00078816
		// (set) Token: 0x060021C3 RID: 8643 RVA: 0x0007A627 File Offset: 0x00078827
		[DataMember]
		public bool LinkedInEnabled
		{
			get
			{
				return this.IsFeatureEnabled(Feature.LinkedInEnabled);
			}
			set
			{
				this.SetFeatureEnabled(Feature.LinkedInEnabled, value);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0007A639 File Offset: 0x00078839
		// (set) Token: 0x060021C5 RID: 8645 RVA: 0x0007A647 File Offset: 0x00078847
		[DataMember]
		public bool OWALightEnabled
		{
			get
			{
				return this.IsFeatureEnabled(Feature.OWALight);
			}
			set
			{
				this.SetFeatureEnabled(Feature.OWALight, value);
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0007A656 File Offset: 0x00078856
		private bool IsFeatureEnabled(Feature feature)
		{
			return (feature & (Feature)this.segmentationFlags) == feature && (this.configurationContext == null || this.configurationContext.IsFeatureNotRestricted((ulong)feature));
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0007A67B File Offset: 0x0007887B
		private void SetFeatureEnabled(Feature feature, bool enabled)
		{
			if (enabled)
			{
				this.segmentationFlags |= (ulong)feature;
				return;
			}
			this.segmentationFlags &= (ulong)(~(ulong)feature);
		}

		// Token: 0x040012BA RID: 4794
		private ulong segmentationFlags;

		// Token: 0x040012BB RID: 4795
		private ConfigurationContext configurationContext;
	}
}
