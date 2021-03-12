﻿using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000004 RID: 4
	public static class NumPropTag
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static uint GetTag(ushort propId, PropertyType propType)
		{
			return (uint)((ushort)((uint)propId << 16) + propType);
		}

		// Token: 0x040006FC RID: 1788
		public const uint NULL = 4354U;

		// Token: 0x040006FD RID: 1789
		public const uint AcknowledgementMode = 65539U;

		// Token: 0x040006FE RID: 1790
		public const uint TestTest = 65794U;

		// Token: 0x040006FF RID: 1791
		public const uint AlternateRecipientAllowed = 131083U;

		// Token: 0x04000700 RID: 1792
		public const uint AuthorizingUsers = 196866U;

		// Token: 0x04000701 RID: 1793
		public const uint AutoForwardComment = 262175U;

		// Token: 0x04000702 RID: 1794
		public const uint AutoForwarded = 327691U;

		// Token: 0x04000703 RID: 1795
		public const uint ContentConfidentialityAlgorithmId = 393474U;

		// Token: 0x04000704 RID: 1796
		public const uint ContentCorrelator = 459010U;

		// Token: 0x04000705 RID: 1797
		public const uint ContentIdentifier = 524319U;

		// Token: 0x04000706 RID: 1798
		public const uint ContentLength = 589827U;

		// Token: 0x04000707 RID: 1799
		public const uint ContentReturnRequested = 655371U;

		// Token: 0x04000708 RID: 1800
		public const uint ConversationKey = 721154U;

		// Token: 0x04000709 RID: 1801
		public const uint ConversionEits = 786690U;

		// Token: 0x0400070A RID: 1802
		public const uint ConversionWithLossProhibited = 851979U;

		// Token: 0x0400070B RID: 1803
		public const uint ConvertedEits = 917762U;

		// Token: 0x0400070C RID: 1804
		public const uint DeferredDeliveryTime = 983104U;

		// Token: 0x0400070D RID: 1805
		public const uint DeliverTime = 1048640U;

		// Token: 0x0400070E RID: 1806
		public const uint DiscardReason = 1114115U;

		// Token: 0x0400070F RID: 1807
		public const uint DisclosureOfRecipients = 1179659U;

		// Token: 0x04000710 RID: 1808
		public const uint DLExpansionHistory = 1245442U;

		// Token: 0x04000711 RID: 1809
		public const uint DLExpansionProhibited = 1310731U;

		// Token: 0x04000712 RID: 1810
		public const uint ExpiryTime = 1376320U;

		// Token: 0x04000713 RID: 1811
		public const uint ImplicitConversionProhibited = 1441803U;

		// Token: 0x04000714 RID: 1812
		public const uint Importance = 1507331U;

		// Token: 0x04000715 RID: 1813
		public const uint IPMID = 1573122U;

		// Token: 0x04000716 RID: 1814
		public const uint LatestDeliveryTime = 1638464U;

		// Token: 0x04000717 RID: 1815
		public const uint MessageClass = 1703967U;

		// Token: 0x04000718 RID: 1816
		public const uint MessageDeliveryId = 1769730U;

		// Token: 0x04000719 RID: 1817
		public const uint MessageSecurityLabel = 1966338U;

		// Token: 0x0400071A RID: 1818
		public const uint ObsoletedIPMS = 2031874U;

		// Token: 0x0400071B RID: 1819
		public const uint OriginallyIntendedRecipientName = 2097410U;

		// Token: 0x0400071C RID: 1820
		public const uint OriginalEITS = 2162946U;

		// Token: 0x0400071D RID: 1821
		public const uint OriginatorCertificate = 2228482U;

		// Token: 0x0400071E RID: 1822
		public const uint DeliveryReportRequested = 2293771U;

		// Token: 0x0400071F RID: 1823
		public const uint OriginatorReturnAddress = 2359554U;

		// Token: 0x04000720 RID: 1824
		public const uint ParentKey = 2425090U;

		// Token: 0x04000721 RID: 1825
		public const uint Priority = 2490371U;

		// Token: 0x04000722 RID: 1826
		public const uint OriginCheck = 2556162U;

		// Token: 0x04000723 RID: 1827
		public const uint ProofOfSubmissionRequested = 2621451U;

		// Token: 0x04000724 RID: 1828
		public const uint ReadReceiptRequested = 2686987U;

		// Token: 0x04000725 RID: 1829
		public const uint ReceiptTime = 2752576U;

		// Token: 0x04000726 RID: 1830
		public const uint RecipientReassignmentProhibited = 2818059U;

		// Token: 0x04000727 RID: 1831
		public const uint RedirectionHistory = 2883842U;

		// Token: 0x04000728 RID: 1832
		public const uint RelatedIPMS = 2949378U;

		// Token: 0x04000729 RID: 1833
		public const uint OriginalSensitivity = 3014659U;

		// Token: 0x0400072A RID: 1834
		public const uint Languages = 3080223U;

		// Token: 0x0400072B RID: 1835
		public const uint ReplyTime = 3145792U;

		// Token: 0x0400072C RID: 1836
		public const uint ReportTag = 3211522U;

		// Token: 0x0400072D RID: 1837
		public const uint ReportTime = 3276864U;

		// Token: 0x0400072E RID: 1838
		public const uint ReturnedIPM = 3342347U;

		// Token: 0x0400072F RID: 1839
		public const uint Security = 3407875U;

		// Token: 0x04000730 RID: 1840
		public const uint IncompleteCopy = 3473419U;

		// Token: 0x04000731 RID: 1841
		public const uint Sensitivity = 3538947U;

		// Token: 0x04000732 RID: 1842
		public const uint Subject = 3604511U;

		// Token: 0x04000733 RID: 1843
		public const uint SubjectIPM = 3670274U;

		// Token: 0x04000734 RID: 1844
		public const uint ClientSubmitTime = 3735616U;

		// Token: 0x04000735 RID: 1845
		public const uint ReportName = 3801119U;

		// Token: 0x04000736 RID: 1846
		public const uint SentRepresentingSearchKey = 3866882U;

		// Token: 0x04000737 RID: 1847
		public const uint X400ContentType = 3932418U;

		// Token: 0x04000738 RID: 1848
		public const uint SubjectPrefix = 3997727U;

		// Token: 0x04000739 RID: 1849
		public const uint NonReceiptReason = 4063235U;

		// Token: 0x0400073A RID: 1850
		public const uint ReceivedByEntryId = 4129026U;

		// Token: 0x0400073B RID: 1851
		public const uint ReceivedByName = 4194335U;

		// Token: 0x0400073C RID: 1852
		public const uint SentRepresentingEntryId = 4260098U;

		// Token: 0x0400073D RID: 1853
		public const uint SentRepresentingName = 4325407U;

		// Token: 0x0400073E RID: 1854
		public const uint ReceivedRepresentingEntryId = 4391170U;

		// Token: 0x0400073F RID: 1855
		public const uint ReceivedRepresentingName = 4456479U;

		// Token: 0x04000740 RID: 1856
		public const uint ReportEntryId = 4522242U;

		// Token: 0x04000741 RID: 1857
		public const uint ReadReceiptEntryId = 4587778U;

		// Token: 0x04000742 RID: 1858
		public const uint MessageSubmissionId = 4653314U;

		// Token: 0x04000743 RID: 1859
		public const uint ProviderSubmitTime = 4718656U;

		// Token: 0x04000744 RID: 1860
		public const uint OriginalSubject = 4784159U;

		// Token: 0x04000745 RID: 1861
		public const uint DiscVal = 4849675U;

		// Token: 0x04000746 RID: 1862
		public const uint OriginalMessageClass = 4915231U;

		// Token: 0x04000747 RID: 1863
		public const uint OriginalAuthorEntryId = 4980994U;

		// Token: 0x04000748 RID: 1864
		public const uint OriginalAuthorName = 5046303U;

		// Token: 0x04000749 RID: 1865
		public const uint OriginalSubmitTime = 5111872U;

		// Token: 0x0400074A RID: 1866
		public const uint ReplyRecipientEntries = 5177602U;

		// Token: 0x0400074B RID: 1867
		public const uint ReplyRecipientNames = 5242911U;

		// Token: 0x0400074C RID: 1868
		public const uint ReceivedBySearchKey = 5308674U;

		// Token: 0x0400074D RID: 1869
		public const uint ReceivedRepresentingSearchKey = 5374210U;

		// Token: 0x0400074E RID: 1870
		public const uint ReadReceiptSearchKey = 5439746U;

		// Token: 0x0400074F RID: 1871
		public const uint ReportSearchKey = 5505282U;

		// Token: 0x04000750 RID: 1872
		public const uint OriginalDeliveryTime = 5570624U;

		// Token: 0x04000751 RID: 1873
		public const uint OriginalAuthorSearchKey = 5636354U;

		// Token: 0x04000752 RID: 1874
		public const uint MessageToMe = 5701643U;

		// Token: 0x04000753 RID: 1875
		public const uint MessageCCMe = 5767179U;

		// Token: 0x04000754 RID: 1876
		public const uint MessageRecipMe = 5832715U;

		// Token: 0x04000755 RID: 1877
		public const uint OriginalSenderName = 5898271U;

		// Token: 0x04000756 RID: 1878
		public const uint OriginalSenderEntryId = 5964034U;

		// Token: 0x04000757 RID: 1879
		public const uint OriginalSenderSearchKey = 6029570U;

		// Token: 0x04000758 RID: 1880
		public const uint OriginalSentRepresentingName = 6094879U;

		// Token: 0x04000759 RID: 1881
		public const uint OriginalSentRepresentingEntryId = 6160642U;

		// Token: 0x0400075A RID: 1882
		public const uint OriginalSentRepresentingSearchKey = 6226178U;

		// Token: 0x0400075B RID: 1883
		public const uint StartDate = 6291520U;

		// Token: 0x0400075C RID: 1884
		public const uint EndDate = 6357056U;

		// Token: 0x0400075D RID: 1885
		public const uint OwnerApptId = 6422531U;

		// Token: 0x0400075E RID: 1886
		public const uint ResponseRequested = 6488075U;

		// Token: 0x0400075F RID: 1887
		public const uint SentRepresentingAddressType = 6553631U;

		// Token: 0x04000760 RID: 1888
		public const uint SentRepresentingEmailAddress = 6619167U;

		// Token: 0x04000761 RID: 1889
		public const uint OriginalSenderAddressType = 6684703U;

		// Token: 0x04000762 RID: 1890
		public const uint OriginalSenderEmailAddress = 6750239U;

		// Token: 0x04000763 RID: 1891
		public const uint OriginalSentRepresentingAddressType = 6815775U;

		// Token: 0x04000764 RID: 1892
		public const uint OriginalSentRepresentingEmailAddress = 6881311U;

		// Token: 0x04000765 RID: 1893
		public const uint ConversationTopic = 7340063U;

		// Token: 0x04000766 RID: 1894
		public const uint ConversationIndex = 7405826U;

		// Token: 0x04000767 RID: 1895
		public const uint OriginalDisplayBcc = 7471135U;

		// Token: 0x04000768 RID: 1896
		public const uint OriginalDisplayCc = 7536671U;

		// Token: 0x04000769 RID: 1897
		public const uint OriginalDisplayTo = 7602207U;

		// Token: 0x0400076A RID: 1898
		public const uint ReceivedByAddressType = 7667743U;

		// Token: 0x0400076B RID: 1899
		public const uint ReceivedByEmailAddress = 7733279U;

		// Token: 0x0400076C RID: 1900
		public const uint ReceivedRepresentingAddressType = 7798815U;

		// Token: 0x0400076D RID: 1901
		public const uint ReceivedRepresentingEmailAddress = 7864351U;

		// Token: 0x0400076E RID: 1902
		public const uint OriginalAuthorAddressType = 7929887U;

		// Token: 0x0400076F RID: 1903
		public const uint OriginalAuthorEmailAddress = 7995423U;

		// Token: 0x04000770 RID: 1904
		public const uint OriginallyIntendedRecipientAddressType = 8126495U;

		// Token: 0x04000771 RID: 1905
		public const uint TransportMessageHeaders = 8192031U;

		// Token: 0x04000772 RID: 1906
		public const uint Delegation = 8257794U;

		// Token: 0x04000773 RID: 1907
		public const uint TNEFCorrelationKey = 8323330U;

		// Token: 0x04000774 RID: 1908
		public const uint ReportDisposition = 8388639U;

		// Token: 0x04000775 RID: 1909
		public const uint ReportDispositionMode = 8454175U;

		// Token: 0x04000776 RID: 1910
		public const uint ReportOriginalSender = 8519711U;

		// Token: 0x04000777 RID: 1911
		public const uint ReportDispositionToNames = 8585247U;

		// Token: 0x04000778 RID: 1912
		public const uint ReportDispositionToEmailAddress = 8650783U;

		// Token: 0x04000779 RID: 1913
		public const uint ReportDispositionOptions = 8716319U;

		// Token: 0x0400077A RID: 1914
		public const uint RichContent = 8781826U;

		// Token: 0x0400077B RID: 1915
		public const uint AdministratorEMail = 16781343U;

		// Token: 0x0400077C RID: 1916
		public const uint ContentIntegrityCheck = 201326850U;

		// Token: 0x0400077D RID: 1917
		public const uint ExplicitConversion = 201392131U;

		// Token: 0x0400077E RID: 1918
		public const uint ReturnRequested = 201457675U;

		// Token: 0x0400077F RID: 1919
		public const uint MessageToken = 201523458U;

		// Token: 0x04000780 RID: 1920
		public const uint NDRReasonCode = 201588739U;

		// Token: 0x04000781 RID: 1921
		public const uint NDRDiagCode = 201654275U;

		// Token: 0x04000782 RID: 1922
		public const uint NonReceiptNotificationRequested = 201719819U;

		// Token: 0x04000783 RID: 1923
		public const uint DeliveryPoint = 201785347U;

		// Token: 0x04000784 RID: 1924
		public const uint NonDeliveryReportRequested = 201850891U;

		// Token: 0x04000785 RID: 1925
		public const uint OriginatorRequestedAlterateRecipient = 201916674U;

		// Token: 0x04000786 RID: 1926
		public const uint PhysicalDeliveryBureauFaxDelivery = 201981963U;

		// Token: 0x04000787 RID: 1927
		public const uint PhysicalDeliveryMode = 202047491U;

		// Token: 0x04000788 RID: 1928
		public const uint PhysicalDeliveryReportRequest = 202113027U;

		// Token: 0x04000789 RID: 1929
		public const uint PhysicalForwardingAddress = 202178818U;

		// Token: 0x0400078A RID: 1930
		public const uint PhysicalForwardingAddressRequested = 202244107U;

		// Token: 0x0400078B RID: 1931
		public const uint PhysicalForwardingProhibited = 202309643U;

		// Token: 0x0400078C RID: 1932
		public const uint PhysicalRenditionAttributes = 202375426U;

		// Token: 0x0400078D RID: 1933
		public const uint ProofOfDelivery = 202440962U;

		// Token: 0x0400078E RID: 1934
		public const uint ProofOfDeliveryRequested = 202506251U;

		// Token: 0x0400078F RID: 1935
		public const uint RecipientCertificate = 202572034U;

		// Token: 0x04000790 RID: 1936
		public const uint RecipientNumberForAdvice = 202637343U;

		// Token: 0x04000791 RID: 1937
		public const uint RecipientType = 202702851U;

		// Token: 0x04000792 RID: 1938
		public const uint RegisteredMailType = 202768387U;

		// Token: 0x04000793 RID: 1939
		public const uint ReplyRequested = 202833931U;

		// Token: 0x04000794 RID: 1940
		public const uint RequestedDeliveryMethod = 202899459U;

		// Token: 0x04000795 RID: 1941
		public const uint SenderEntryId = 202965250U;

		// Token: 0x04000796 RID: 1942
		public const uint SenderName = 203030559U;

		// Token: 0x04000797 RID: 1943
		public const uint SupplementaryInfo = 203096095U;

		// Token: 0x04000798 RID: 1944
		public const uint TypeOfMTSUser = 203161603U;

		// Token: 0x04000799 RID: 1945
		public const uint SenderSearchKey = 203227394U;

		// Token: 0x0400079A RID: 1946
		public const uint SenderAddressType = 203292703U;

		// Token: 0x0400079B RID: 1947
		public const uint SenderEmailAddress = 203358239U;

		// Token: 0x0400079C RID: 1948
		public const uint ParticipantSID = 203686146U;

		// Token: 0x0400079D RID: 1949
		public const uint ParticipantGuid = 203751682U;

		// Token: 0x0400079E RID: 1950
		public const uint ToGroupExpansionRecipients = 203816991U;

		// Token: 0x0400079F RID: 1951
		public const uint CcGroupExpansionRecipients = 203882527U;

		// Token: 0x040007A0 RID: 1952
		public const uint BccGroupExpansionRecipients = 203948063U;

		// Token: 0x040007A1 RID: 1953
		public const uint CurrentVersion = 234881044U;

		// Token: 0x040007A2 RID: 1954
		public const uint DeleteAfterSubmit = 234946571U;

		// Token: 0x040007A3 RID: 1955
		public const uint DisplayBcc = 235012127U;

		// Token: 0x040007A4 RID: 1956
		public const uint DisplayCc = 235077663U;

		// Token: 0x040007A5 RID: 1957
		public const uint DisplayTo = 235143199U;

		// Token: 0x040007A6 RID: 1958
		public const uint ParentDisplay = 235208735U;

		// Token: 0x040007A7 RID: 1959
		public const uint MessageDeliveryTime = 235274304U;

		// Token: 0x040007A8 RID: 1960
		public const uint MessageFlags = 235339779U;

		// Token: 0x040007A9 RID: 1961
		public const uint MessageSize = 235405332U;

		// Token: 0x040007AA RID: 1962
		public const uint MessageSize32 = 235405315U;

		// Token: 0x040007AB RID: 1963
		public const uint ParentEntryId = 235471106U;

		// Token: 0x040007AC RID: 1964
		public const uint ParentEntryIdSvrEid = 235471099U;

		// Token: 0x040007AD RID: 1965
		public const uint SentMailEntryId = 235536642U;

		// Token: 0x040007AE RID: 1966
		public const uint Correlate = 235667467U;

		// Token: 0x040007AF RID: 1967
		public const uint CorrelateMTSID = 235733250U;

		// Token: 0x040007B0 RID: 1968
		public const uint DiscreteValues = 235798539U;

		// Token: 0x040007B1 RID: 1969
		public const uint Responsibility = 235864075U;

		// Token: 0x040007B2 RID: 1970
		public const uint SpoolerStatus = 235929603U;

		// Token: 0x040007B3 RID: 1971
		public const uint TransportStatus = 235995139U;

		// Token: 0x040007B4 RID: 1972
		public const uint MessageRecipients = 236060685U;

		// Token: 0x040007B5 RID: 1973
		public const uint MessageRecipientsMVBin = 236065026U;

		// Token: 0x040007B6 RID: 1974
		public const uint MessageAttachments = 236126221U;

		// Token: 0x040007B7 RID: 1975
		public const uint ItemSubobjectsBin = 236126466U;

		// Token: 0x040007B8 RID: 1976
		public const uint SubmitFlags = 236191747U;

		// Token: 0x040007B9 RID: 1977
		public const uint RecipientStatus = 236257283U;

		// Token: 0x040007BA RID: 1978
		public const uint TransportKey = 236322819U;

		// Token: 0x040007BB RID: 1979
		public const uint MsgStatus = 236388355U;

		// Token: 0x040007BC RID: 1980
		public const uint MessageDownloadTime = 236453891U;

		// Token: 0x040007BD RID: 1981
		public const uint CreationVersion = 236519444U;

		// Token: 0x040007BE RID: 1982
		public const uint ModifyVersion = 236584980U;

		// Token: 0x040007BF RID: 1983
		public const uint HasAttach = 236650507U;

		// Token: 0x040007C0 RID: 1984
		public const uint BodyCRC = 236716035U;

		// Token: 0x040007C1 RID: 1985
		public const uint NormalizedSubject = 236781599U;

		// Token: 0x040007C2 RID: 1986
		public const uint RTFInSync = 236912651U;

		// Token: 0x040007C3 RID: 1987
		public const uint AttachSize = 236978179U;

		// Token: 0x040007C4 RID: 1988
		public const uint AttachSizeInt64 = 236978196U;

		// Token: 0x040007C5 RID: 1989
		public const uint AttachNum = 237043715U;

		// Token: 0x040007C6 RID: 1990
		public const uint Preprocess = 237109259U;

		// Token: 0x040007C7 RID: 1991
		public const uint FolderInternetId = 237174787U;

		// Token: 0x040007C8 RID: 1992
		public const uint HighestFolderInternetId = 237174787U;

		// Token: 0x040007C9 RID: 1993
		public const uint InternetArticleNumber = 237174787U;

		// Token: 0x040007CA RID: 1994
		public const uint OriginatingMTACertificate = 237306114U;

		// Token: 0x040007CB RID: 1995
		public const uint ProofOfSubmission = 237371650U;

		// Token: 0x040007CC RID: 1996
		public const uint NTSecurityDescriptor = 237437186U;

		// Token: 0x040007CD RID: 1997
		public const uint PrimarySendAccount = 237502495U;

		// Token: 0x040007CE RID: 1998
		public const uint NextSendAccount = 237568031U;

		// Token: 0x040007CF RID: 1999
		public const uint TodoItemFlags = 237699075U;

		// Token: 0x040007D0 RID: 2000
		public const uint SwappedTODOStore = 237764866U;

		// Token: 0x040007D1 RID: 2001
		public const uint SwappedTODOData = 237830402U;

		// Token: 0x040007D2 RID: 2002
		public const uint IMAPId = 237961219U;

		// Token: 0x040007D3 RID: 2003
		public const uint OriginalSourceServerVersion = 238092290U;

		// Token: 0x040007D4 RID: 2004
		public const uint ReplFlags = 238551043U;

		// Token: 0x040007D5 RID: 2005
		public const uint MessageDeepAttachments = 238682125U;

		// Token: 0x040007D6 RID: 2006
		public const uint AclTableAndSecurityDescriptor = 239010050U;

		// Token: 0x040007D7 RID: 2007
		public const uint SenderGuid = 239075586U;

		// Token: 0x040007D8 RID: 2008
		public const uint SentRepresentingGuid = 239141122U;

		// Token: 0x040007D9 RID: 2009
		public const uint OriginalSenderGuid = 239206658U;

		// Token: 0x040007DA RID: 2010
		public const uint OriginalSentRepresentingGuid = 239272194U;

		// Token: 0x040007DB RID: 2011
		public const uint ReadReceiptGuid = 239337730U;

		// Token: 0x040007DC RID: 2012
		public const uint ReportGuid = 239403266U;

		// Token: 0x040007DD RID: 2013
		public const uint OriginatorGuid = 239468802U;

		// Token: 0x040007DE RID: 2014
		public const uint ReportDestinationGuid = 239534338U;

		// Token: 0x040007DF RID: 2015
		public const uint OriginalAuthorGuid = 239599874U;

		// Token: 0x040007E0 RID: 2016
		public const uint ReceivedByGuid = 239665410U;

		// Token: 0x040007E1 RID: 2017
		public const uint ReceivedRepresentingGuid = 239730946U;

		// Token: 0x040007E2 RID: 2018
		public const uint CreatorGuid = 239796482U;

		// Token: 0x040007E3 RID: 2019
		public const uint LastModifierGuid = 239862018U;

		// Token: 0x040007E4 RID: 2020
		public const uint SenderSID = 239927554U;

		// Token: 0x040007E5 RID: 2021
		public const uint SentRepresentingSID = 239993090U;

		// Token: 0x040007E6 RID: 2022
		public const uint OriginalSenderSid = 240058626U;

		// Token: 0x040007E7 RID: 2023
		public const uint OriginalSentRepresentingSid = 240124162U;

		// Token: 0x040007E8 RID: 2024
		public const uint ReadReceiptSid = 240189698U;

		// Token: 0x040007E9 RID: 2025
		public const uint ReportSid = 240255234U;

		// Token: 0x040007EA RID: 2026
		public const uint OriginatorSid = 240320770U;

		// Token: 0x040007EB RID: 2027
		public const uint ReportDestinationSid = 240386306U;

		// Token: 0x040007EC RID: 2028
		public const uint OriginalAuthorSid = 240451842U;

		// Token: 0x040007ED RID: 2029
		public const uint RcvdBySid = 240517378U;

		// Token: 0x040007EE RID: 2030
		public const uint RcvdRepresentingSid = 240582914U;

		// Token: 0x040007EF RID: 2031
		public const uint CreatorSID = 240648450U;

		// Token: 0x040007F0 RID: 2032
		public const uint LastModifierSid = 240713986U;

		// Token: 0x040007F1 RID: 2033
		public const uint RecipientCAI = 240779522U;

		// Token: 0x040007F2 RID: 2034
		public const uint ConversationCreatorSID = 240845058U;

		// Token: 0x040007F3 RID: 2035
		public const uint Catalog = 240845058U;

		// Token: 0x040007F4 RID: 2036
		public const uint CISearchEnabled = 240910347U;

		// Token: 0x040007F5 RID: 2037
		public const uint CINotificationEnabled = 240975883U;

		// Token: 0x040007F6 RID: 2038
		public const uint MaxIndices = 241041411U;

		// Token: 0x040007F7 RID: 2039
		public const uint SourceFid = 241106964U;

		// Token: 0x040007F8 RID: 2040
		public const uint PFContactsGuid = 241172738U;

		// Token: 0x040007F9 RID: 2041
		public const uint URLCompNamePostfix = 241238019U;

		// Token: 0x040007FA RID: 2042
		public const uint URLCompNameSet = 241303563U;

		// Token: 0x040007FB RID: 2043
		public const uint SubfolderCount = 241369091U;

		// Token: 0x040007FC RID: 2044
		public const uint DeletedSubfolderCt = 241434627U;

		// Token: 0x040007FD RID: 2045
		public const uint MaxCachedViews = 241696771U;

		// Token: 0x040007FE RID: 2046
		public const uint Read = 241762315U;

		// Token: 0x040007FF RID: 2047
		public const uint NTSecurityDescriptorAsXML = 241827871U;

		// Token: 0x04000800 RID: 2048
		public const uint AdminNTSecurityDescriptorAsXML = 241893407U;

		// Token: 0x04000801 RID: 2049
		public const uint CreatorSidAsXML = 241958943U;

		// Token: 0x04000802 RID: 2050
		public const uint LastModifierSidAsXML = 242024479U;

		// Token: 0x04000803 RID: 2051
		public const uint SenderSIDAsXML = 242090015U;

		// Token: 0x04000804 RID: 2052
		public const uint SentRepresentingSidAsXML = 242155551U;

		// Token: 0x04000805 RID: 2053
		public const uint OriginalSenderSIDAsXML = 242221087U;

		// Token: 0x04000806 RID: 2054
		public const uint OriginalSentRepresentingSIDAsXML = 242286623U;

		// Token: 0x04000807 RID: 2055
		public const uint ReadReceiptSIDAsXML = 242352159U;

		// Token: 0x04000808 RID: 2056
		public const uint ReportSIDAsXML = 242417695U;

		// Token: 0x04000809 RID: 2057
		public const uint OriginatorSidAsXML = 242483231U;

		// Token: 0x0400080A RID: 2058
		public const uint ReportDestinationSIDAsXML = 242548767U;

		// Token: 0x0400080B RID: 2059
		public const uint OriginalAuthorSIDAsXML = 242614303U;

		// Token: 0x0400080C RID: 2060
		public const uint ReceivedBySIDAsXML = 242679839U;

		// Token: 0x0400080D RID: 2061
		public const uint ReceivedRepersentingSIDAsXML = 242745375U;

		// Token: 0x0400080E RID: 2062
		public const uint TrustSender = 242810883U;

		// Token: 0x0400080F RID: 2063
		public const uint MergeMidsetDeleted = 242876674U;

		// Token: 0x04000810 RID: 2064
		public const uint ReserveRangeOfIDs = 242942210U;

		// Token: 0x04000811 RID: 2065
		public const uint SenderSMTPAddress = 243859487U;

		// Token: 0x04000812 RID: 2066
		public const uint SentRepresentingSMTPAddress = 243925023U;

		// Token: 0x04000813 RID: 2067
		public const uint OriginalSenderSMTPAddress = 243990559U;

		// Token: 0x04000814 RID: 2068
		public const uint OriginalSentRepresentingSMTPAddress = 244056095U;

		// Token: 0x04000815 RID: 2069
		public const uint ReadReceiptSMTPAddress = 244121631U;

		// Token: 0x04000816 RID: 2070
		public const uint ReportSMTPAddress = 244187167U;

		// Token: 0x04000817 RID: 2071
		public const uint OriginatorSMTPAddress = 244252703U;

		// Token: 0x04000818 RID: 2072
		public const uint ReportDestinationSMTPAddress = 244318239U;

		// Token: 0x04000819 RID: 2073
		public const uint OriginalAuthorSMTPAddress = 244383775U;

		// Token: 0x0400081A RID: 2074
		public const uint ReceivedBySMTPAddress = 244449311U;

		// Token: 0x0400081B RID: 2075
		public const uint ReceivedRepresentingSMTPAddress = 244514847U;

		// Token: 0x0400081C RID: 2076
		public const uint CreatorSMTPAddress = 244580383U;

		// Token: 0x0400081D RID: 2077
		public const uint LastModifierSMTPAddress = 244645919U;

		// Token: 0x0400081E RID: 2078
		public const uint VirusScannerStamp = 244711682U;

		// Token: 0x0400081F RID: 2079
		public const uint VirusTransportStamp = 244715551U;

		// Token: 0x04000820 RID: 2080
		public const uint AddrTo = 244776991U;

		// Token: 0x04000821 RID: 2081
		public const uint AddrCc = 244842527U;

		// Token: 0x04000822 RID: 2082
		public const uint ExtendedRuleActions = 244908290U;

		// Token: 0x04000823 RID: 2083
		public const uint ExtendedRuleCondition = 244973826U;

		// Token: 0x04000824 RID: 2084
		public const uint ExtendedRuleSizeLimit = 245039107U;

		// Token: 0x04000825 RID: 2085
		public const uint EntourageSentHistory = 245305375U;

		// Token: 0x04000826 RID: 2086
		public const uint ProofInProgress = 245497859U;

		// Token: 0x04000827 RID: 2087
		public const uint SearchAttachmentsOLK = 245694495U;

		// Token: 0x04000828 RID: 2088
		public const uint SearchRecipEmailTo = 245760031U;

		// Token: 0x04000829 RID: 2089
		public const uint SearchRecipEmailCc = 245825567U;

		// Token: 0x0400082A RID: 2090
		public const uint SearchRecipEmailBcc = 245891103U;

		// Token: 0x0400082B RID: 2091
		public const uint SFGAOFlags = 246022147U;

		// Token: 0x0400082C RID: 2092
		public const uint SearchFullTextSubject = 246153247U;

		// Token: 0x0400082D RID: 2093
		public const uint SearchFullTextBody = 246218783U;

		// Token: 0x0400082E RID: 2094
		public const uint FullTextConversationIndex = 246284319U;

		// Token: 0x0400082F RID: 2095
		public const uint SearchAllIndexedProps = 246349855U;

		// Token: 0x04000830 RID: 2096
		public const uint SearchRecipients = 246480927U;

		// Token: 0x04000831 RID: 2097
		public const uint SearchRecipientsTo = 246546463U;

		// Token: 0x04000832 RID: 2098
		public const uint SearchRecipientsCc = 246611999U;

		// Token: 0x04000833 RID: 2099
		public const uint SearchRecipientsBcc = 246677535U;

		// Token: 0x04000834 RID: 2100
		public const uint SearchAccountTo = 246743071U;

		// Token: 0x04000835 RID: 2101
		public const uint SearchAccountCc = 246808607U;

		// Token: 0x04000836 RID: 2102
		public const uint SearchAccountBcc = 246874143U;

		// Token: 0x04000837 RID: 2103
		public const uint SearchEmailAddressTo = 246939679U;

		// Token: 0x04000838 RID: 2104
		public const uint SearchEmailAddressCc = 247005215U;

		// Token: 0x04000839 RID: 2105
		public const uint SearchEmailAddressBcc = 247070751U;

		// Token: 0x0400083A RID: 2106
		public const uint SearchSmtpAddressTo = 247136287U;

		// Token: 0x0400083B RID: 2107
		public const uint SearchSmtpAddressCc = 247201823U;

		// Token: 0x0400083C RID: 2108
		public const uint SearchSmtpAddressBcc = 247267359U;

		// Token: 0x0400083D RID: 2109
		public const uint SearchSender = 247332895U;

		// Token: 0x0400083E RID: 2110
		public const uint IsIRMMessage = 248315915U;

		// Token: 0x0400083F RID: 2111
		public const uint SearchIsPartiallyIndexed = 248381451U;

		// Token: 0x04000840 RID: 2112
		public const uint FreeBusyNTSD = 251658498U;

		// Token: 0x04000841 RID: 2113
		public const uint RenewTime = 251723840U;

		// Token: 0x04000842 RID: 2114
		public const uint DeliveryOrRenewTime = 251789376U;

		// Token: 0x04000843 RID: 2115
		public const uint ConversationFamilyId = 251855106U;

		// Token: 0x04000844 RID: 2116
		public const uint LikeCount = 251920387U;

		// Token: 0x04000845 RID: 2117
		public const uint RichContentDeprecated = 251985922U;

		// Token: 0x04000846 RID: 2118
		public const uint PeopleCentricConversationId = 252051459U;

		// Token: 0x04000847 RID: 2119
		public const uint DiscoveryAnnotation = 252575775U;

		// Token: 0x04000848 RID: 2120
		public const uint Access = 267649027U;

		// Token: 0x04000849 RID: 2121
		public const uint RowType = 267714563U;

		// Token: 0x0400084A RID: 2122
		public const uint InstanceKey = 267780354U;

		// Token: 0x0400084B RID: 2123
		public const uint InstanceKeySvrEid = 267780347U;

		// Token: 0x0400084C RID: 2124
		public const uint AccessLevel = 267845635U;

		// Token: 0x0400084D RID: 2125
		public const uint MappingSignature = 267911426U;

		// Token: 0x0400084E RID: 2126
		public const uint RecordKey = 267976962U;

		// Token: 0x0400084F RID: 2127
		public const uint RecordKeySvrEid = 267976955U;

		// Token: 0x04000850 RID: 2128
		public const uint StoreRecordKey = 268042498U;

		// Token: 0x04000851 RID: 2129
		public const uint StoreEntryId = 268108034U;

		// Token: 0x04000852 RID: 2130
		public const uint MiniIcon = 268173570U;

		// Token: 0x04000853 RID: 2131
		public const uint Icon = 268239106U;

		// Token: 0x04000854 RID: 2132
		public const uint ObjectType = 268304387U;

		// Token: 0x04000855 RID: 2133
		public const uint EntryId = 268370178U;

		// Token: 0x04000856 RID: 2134
		public const uint EntryIdSvrEid = 268370171U;

		// Token: 0x04000857 RID: 2135
		public const uint BodyUnicode = 268435487U;

		// Token: 0x04000858 RID: 2136
		public const uint IsIntegJobMailboxGuid = 268435528U;

		// Token: 0x04000859 RID: 2137
		public const uint ReportText = 268501023U;

		// Token: 0x0400085A RID: 2138
		public const uint IsIntegJobGuid = 268501064U;

		// Token: 0x0400085B RID: 2139
		public const uint OriginatorAndDLExpansionHistory = 268566786U;

		// Token: 0x0400085C RID: 2140
		public const uint IsIntegJobFlags = 268566531U;

		// Token: 0x0400085D RID: 2141
		public const uint ReportingDLName = 268632322U;

		// Token: 0x0400085E RID: 2142
		public const uint IsIntegJobTask = 268632067U;

		// Token: 0x0400085F RID: 2143
		public const uint ReportingMTACertificate = 268697858U;

		// Token: 0x04000860 RID: 2144
		public const uint IsIntegJobState = 268697602U;

		// Token: 0x04000861 RID: 2145
		public const uint IsIntegJobCreationTime = 268763200U;

		// Token: 0x04000862 RID: 2146
		public const uint RtfSyncBodyCrc = 268828675U;

		// Token: 0x04000863 RID: 2147
		public const uint IsIntegJobCompletedTime = 268828736U;

		// Token: 0x04000864 RID: 2148
		public const uint RtfSyncBodyCount = 268894211U;

		// Token: 0x04000865 RID: 2149
		public const uint IsIntegJobLastExecutionTime = 268894272U;

		// Token: 0x04000866 RID: 2150
		public const uint RtfSyncBodyTag = 268959775U;

		// Token: 0x04000867 RID: 2151
		public const uint IsIntegJobCorruptionsDetected = 268959747U;

		// Token: 0x04000868 RID: 2152
		public const uint RtfCompressed = 269025538U;

		// Token: 0x04000869 RID: 2153
		public const uint IsIntegJobCorruptionsFixed = 269025283U;

		// Token: 0x0400086A RID: 2154
		public const uint AlternateBestBody = 269091074U;

		// Token: 0x0400086B RID: 2155
		public const uint IsIntegJobRequestGuid = 269090888U;

		// Token: 0x0400086C RID: 2156
		public const uint IsIntegJobProgress = 269156354U;

		// Token: 0x0400086D RID: 2157
		public const uint IsIntegJobCorruptions = 269222146U;

		// Token: 0x0400086E RID: 2158
		public const uint IsIntegJobSource = 269287426U;

		// Token: 0x0400086F RID: 2159
		public const uint IsIntegJobPriority = 269352962U;

		// Token: 0x04000870 RID: 2160
		public const uint IsIntegJobTimeInServer = 269418501U;

		// Token: 0x04000871 RID: 2161
		public const uint RtfSyncPrefixCount = 269484035U;

		// Token: 0x04000872 RID: 2162
		public const uint IsIntegJobMailboxNumber = 269484035U;

		// Token: 0x04000873 RID: 2163
		public const uint RtfSyncTrailingCount = 269549571U;

		// Token: 0x04000874 RID: 2164
		public const uint IsIntegJobError = 269549571U;

		// Token: 0x04000875 RID: 2165
		public const uint OriginallyIntendedRecipientEntryId = 269615362U;

		// Token: 0x04000876 RID: 2166
		public const uint BodyHtml = 269680898U;

		// Token: 0x04000877 RID: 2167
		public const uint BodyHtmlUnicode = 269680671U;

		// Token: 0x04000878 RID: 2168
		public const uint BodyContentLocation = 269746207U;

		// Token: 0x04000879 RID: 2169
		public const uint BodyContentId = 269811743U;

		// Token: 0x0400087A RID: 2170
		public const uint NativeBodyInfo = 269877251U;

		// Token: 0x0400087B RID: 2171
		public const uint NativeBodyType = 269877250U;

		// Token: 0x0400087C RID: 2172
		public const uint NativeBody = 269877506U;

		// Token: 0x0400087D RID: 2173
		public const uint AnnotationToken = 269943042U;

		// Token: 0x0400087E RID: 2174
		public const uint InternetApproved = 271581215U;

		// Token: 0x0400087F RID: 2175
		public const uint InternetFollowupTo = 271777823U;

		// Token: 0x04000880 RID: 2176
		public const uint InternetMessageId = 271908895U;

		// Token: 0x04000881 RID: 2177
		public const uint InetNewsgroups = 271974431U;

		// Token: 0x04000882 RID: 2178
		public const uint InternetReferences = 272171039U;

		// Token: 0x04000883 RID: 2179
		public const uint PostReplyFolderEntries = 272433410U;

		// Token: 0x04000884 RID: 2180
		public const uint NNTPXRef = 272629791U;

		// Token: 0x04000885 RID: 2181
		public const uint InReplyToId = 272760863U;

		// Token: 0x04000886 RID: 2182
		public const uint OriginalInternetMessageId = 273023007U;

		// Token: 0x04000887 RID: 2183
		public const uint IconIndex = 276824067U;

		// Token: 0x04000888 RID: 2184
		public const uint LastVerbExecuted = 276889603U;

		// Token: 0x04000889 RID: 2185
		public const uint LastVerbExecutionTime = 276955200U;

		// Token: 0x0400088A RID: 2186
		public const uint Relevance = 277086211U;

		// Token: 0x0400088B RID: 2187
		public const uint FlagStatus = 277872643U;

		// Token: 0x0400088C RID: 2188
		public const uint FlagCompleteTime = 277938240U;

		// Token: 0x0400088D RID: 2189
		public const uint FormatPT = 278003715U;

		// Token: 0x0400088E RID: 2190
		public const uint FollowupIcon = 278200323U;

		// Token: 0x0400088F RID: 2191
		public const uint BlockStatus = 278265859U;

		// Token: 0x04000890 RID: 2192
		public const uint ItemTempFlags = 278331395U;

		// Token: 0x04000891 RID: 2193
		public const uint SMTPTempTblData = 281018626U;

		// Token: 0x04000892 RID: 2194
		public const uint SMTPTempTblData2 = 281083907U;

		// Token: 0x04000893 RID: 2195
		public const uint SMTPTempTblData3 = 281149698U;

		// Token: 0x04000894 RID: 2196
		public const uint DAVSubmitData = 281411615U;

		// Token: 0x04000895 RID: 2197
		public const uint ImapCachedMsgSize = 284164354U;

		// Token: 0x04000896 RID: 2198
		public const uint DisableFullFidelity = 284295179U;

		// Token: 0x04000897 RID: 2199
		public const uint URLCompName = 284360735U;

		// Token: 0x04000898 RID: 2200
		public const uint AttrHidden = 284426251U;

		// Token: 0x04000899 RID: 2201
		public const uint AttrSystem = 284491787U;

		// Token: 0x0400089A RID: 2202
		public const uint AttrReadOnly = 284557323U;

		// Token: 0x0400089B RID: 2203
		public const uint PredictedActions = 302256130U;

		// Token: 0x0400089C RID: 2204
		public const uint GroupingActions = 302321666U;

		// Token: 0x0400089D RID: 2205
		public const uint PredictedActionsSummary = 302383107U;

		// Token: 0x0400089E RID: 2206
		public const uint PredictedActionsThresholds = 302448898U;

		// Token: 0x0400089F RID: 2207
		public const uint IsClutter = 302448651U;

		// Token: 0x040008A0 RID: 2208
		public const uint InferencePredictedReplyForwardReasons = 302514434U;

		// Token: 0x040008A1 RID: 2209
		public const uint InferencePredictedDeleteReasons = 302579970U;

		// Token: 0x040008A2 RID: 2210
		public const uint InferencePredictedIgnoreReasons = 302645506U;

		// Token: 0x040008A3 RID: 2211
		public const uint OriginalDeliveryFolderInfo = 302711042U;

		// Token: 0x040008A4 RID: 2212
		public const uint RowId = 805306371U;

		// Token: 0x040008A5 RID: 2213
		public const uint UserInformationGuid = 805306440U;

		// Token: 0x040008A6 RID: 2214
		public const uint DisplayName = 805371935U;

		// Token: 0x040008A7 RID: 2215
		public const uint UserInformationDisplayName = 805371935U;

		// Token: 0x040008A8 RID: 2216
		public const uint AddressType = 805437471U;

		// Token: 0x040008A9 RID: 2217
		public const uint UserInformationCreationTime = 805437504U;

		// Token: 0x040008AA RID: 2218
		public const uint EmailAddress = 805503007U;

		// Token: 0x040008AB RID: 2219
		public const uint UserInformationLastModificationTime = 805503040U;

		// Token: 0x040008AC RID: 2220
		public const uint Comment = 805568543U;

		// Token: 0x040008AD RID: 2221
		public const uint UserInformationChangeNumber = 805568532U;

		// Token: 0x040008AE RID: 2222
		public const uint Depth = 805634051U;

		// Token: 0x040008AF RID: 2223
		public const uint UserInformationLastInteractiveLogonTime = 805634112U;

		// Token: 0x040008B0 RID: 2224
		public const uint ProviderDisplay = 805699615U;

		// Token: 0x040008B1 RID: 2225
		public const uint UserInformationActiveSyncAllowedDeviceIDs = 805703711U;

		// Token: 0x040008B2 RID: 2226
		public const uint CreationTime = 805765184U;

		// Token: 0x040008B3 RID: 2227
		public const uint UserInformationActiveSyncBlockedDeviceIDs = 805769247U;

		// Token: 0x040008B4 RID: 2228
		public const uint LastModificationTime = 805830720U;

		// Token: 0x040008B5 RID: 2229
		public const uint UserInformationActiveSyncDebugLogging = 805830659U;

		// Token: 0x040008B6 RID: 2230
		public const uint ResourceFlags = 805896195U;

		// Token: 0x040008B7 RID: 2231
		public const uint UserInformationActiveSyncEnabled = 805896203U;

		// Token: 0x040008B8 RID: 2232
		public const uint ProviderDllName = 805961759U;

		// Token: 0x040008B9 RID: 2233
		public const uint UserInformationAdminDisplayName = 805961759U;

		// Token: 0x040008BA RID: 2234
		public const uint SearchKey = 806027522U;

		// Token: 0x040008BB RID: 2235
		public const uint SearchKeySvrEid = 806027515U;

		// Token: 0x040008BC RID: 2236
		public const uint UserInformationAggregationSubscriptionCredential = 806031391U;

		// Token: 0x040008BD RID: 2237
		public const uint ProviderUID = 806093058U;

		// Token: 0x040008BE RID: 2238
		public const uint UserInformationAllowArchiveAddressSync = 806092811U;

		// Token: 0x040008BF RID: 2239
		public const uint ProviderOrdinal = 806158339U;

		// Token: 0x040008C0 RID: 2240
		public const uint UserInformationAltitude = 806158339U;

		// Token: 0x040008C1 RID: 2241
		public const uint UserInformationAntispamBypassEnabled = 806223883U;

		// Token: 0x040008C2 RID: 2242
		public const uint UserInformationArchiveDomain = 806289439U;

		// Token: 0x040008C3 RID: 2243
		public const uint TargetEntryId = 806355202U;

		// Token: 0x040008C4 RID: 2244
		public const uint UserInformationArchiveGuid = 806355016U;

		// Token: 0x040008C5 RID: 2245
		public const uint UserInformationArchiveName = 806424607U;

		// Token: 0x040008C6 RID: 2246
		public const uint UserInformationArchiveQuota = 806486047U;

		// Token: 0x040008C7 RID: 2247
		public const uint ConversationId = 806551810U;

		// Token: 0x040008C8 RID: 2248
		public const uint UserInformationArchiveRelease = 806551583U;

		// Token: 0x040008C9 RID: 2249
		public const uint BodyTag = 806617346U;

		// Token: 0x040008CA RID: 2250
		public const uint UserInformationArchiveStatus = 806617091U;

		// Token: 0x040008CB RID: 2251
		public const uint ConversationIndexTrackingObsolete = 806682644U;

		// Token: 0x040008CC RID: 2252
		public const uint UserInformationArchiveWarningQuota = 806682655U;

		// Token: 0x040008CD RID: 2253
		public const uint ConversationIndexTracking = 806748171U;

		// Token: 0x040008CE RID: 2254
		public const uint UserInformationAssistantName = 806748191U;

		// Token: 0x040008CF RID: 2255
		public const uint UserInformationBirthdate = 806813760U;

		// Token: 0x040008D0 RID: 2256
		public const uint ArchiveTag = 806879490U;

		// Token: 0x040008D1 RID: 2257
		public const uint UserInformationBypassNestedModerationEnabled = 806879243U;

		// Token: 0x040008D2 RID: 2258
		public const uint PolicyTag = 806945026U;

		// Token: 0x040008D3 RID: 2259
		public const uint UserInformationC = 806944799U;

		// Token: 0x040008D4 RID: 2260
		public const uint RetentionPeriod = 807010307U;

		// Token: 0x040008D5 RID: 2261
		public const uint UserInformationCalendarLoggingQuota = 807010335U;

		// Token: 0x040008D6 RID: 2262
		public const uint StartDateEtc = 807076098U;

		// Token: 0x040008D7 RID: 2263
		public const uint UserInformationCalendarRepairDisabled = 807075851U;

		// Token: 0x040008D8 RID: 2264
		public const uint RetentionDate = 807141440U;

		// Token: 0x040008D9 RID: 2265
		public const uint UserInformationCalendarVersionStoreDisabled = 807141387U;

		// Token: 0x040008DA RID: 2266
		public const uint RetentionFlags = 807206915U;

		// Token: 0x040008DB RID: 2267
		public const uint UserInformationCity = 807206943U;

		// Token: 0x040008DC RID: 2268
		public const uint ArchivePeriod = 807272451U;

		// Token: 0x040008DD RID: 2269
		public const uint UserInformationCountry = 807272479U;

		// Token: 0x040008DE RID: 2270
		public const uint ArchiveDate = 807338048U;

		// Token: 0x040008DF RID: 2271
		public const uint UserInformationCountryCode = 807337987U;

		// Token: 0x040008E0 RID: 2272
		public const uint UserInformationCountryOrRegion = 807403551U;

		// Token: 0x040008E1 RID: 2273
		public const uint UserInformationDefaultMailTip = 807469087U;

		// Token: 0x040008E2 RID: 2274
		public const uint UserInformationDeliverToMailboxAndForward = 807534603U;

		// Token: 0x040008E3 RID: 2275
		public const uint UserInformationDescription = 807604255U;

		// Token: 0x040008E4 RID: 2276
		public const uint UserInformationDisabledArchiveGuid = 807665736U;

		// Token: 0x040008E5 RID: 2277
		public const uint UserInformationDowngradeHighPriorityMessagesEnabled = 807731211U;

		// Token: 0x040008E6 RID: 2278
		public const uint UserInformationECPEnabled = 807796747U;

		// Token: 0x040008E7 RID: 2279
		public const uint UserInformationEmailAddressPolicyEnabled = 807862283U;

		// Token: 0x040008E8 RID: 2280
		public const uint UserInformationEwsAllowEntourage = 807927819U;

		// Token: 0x040008E9 RID: 2281
		public const uint UserInformationEwsAllowMacOutlook = 807993355U;

		// Token: 0x040008EA RID: 2282
		public const uint UserInformationEwsAllowOutlook = 808058891U;

		// Token: 0x040008EB RID: 2283
		public const uint UserInformationEwsApplicationAccessPolicy = 808124419U;

		// Token: 0x040008EC RID: 2284
		public const uint UserInformationEwsEnabled = 808189955U;

		// Token: 0x040008ED RID: 2285
		public const uint UserInformationEwsExceptions = 808259615U;

		// Token: 0x040008EE RID: 2286
		public const uint UserInformationEwsWellKnownApplicationAccessPolicies = 808325151U;

		// Token: 0x040008EF RID: 2287
		public const uint UserInformationExchangeGuid = 808386632U;

		// Token: 0x040008F0 RID: 2288
		public const uint UserInformationExternalOofOptions = 808452099U;

		// Token: 0x040008F1 RID: 2289
		public const uint UserInformationFirstName = 808517663U;

		// Token: 0x040008F2 RID: 2290
		public const uint UserInformationForwardingSmtpAddress = 808583199U;

		// Token: 0x040008F3 RID: 2291
		public const uint UserInformationGender = 808648735U;

		// Token: 0x040008F4 RID: 2292
		public const uint UserInformationGenericForwardingAddress = 808714271U;

		// Token: 0x040008F5 RID: 2293
		public const uint UserInformationGeoCoordinates = 808779807U;

		// Token: 0x040008F6 RID: 2294
		public const uint UserInformationHABSeniorityIndex = 808845315U;

		// Token: 0x040008F7 RID: 2295
		public const uint UserInformationHasActiveSyncDevicePartnership = 808910859U;

		// Token: 0x040008F8 RID: 2296
		public const uint UserInformationHiddenFromAddressListsEnabled = 808976395U;

		// Token: 0x040008F9 RID: 2297
		public const uint UserInformationHiddenFromAddressListsValue = 809041931U;

		// Token: 0x040008FA RID: 2298
		public const uint UserInformationHomePhone = 809107487U;

		// Token: 0x040008FB RID: 2299
		public const uint UserInformationImapEnabled = 809173003U;

		// Token: 0x040008FC RID: 2300
		public const uint UserInformationImapEnableExactRFC822Size = 809238539U;

		// Token: 0x040008FD RID: 2301
		public const uint UserInformationImapForceICalForCalendarRetrievalOption = 809304075U;

		// Token: 0x040008FE RID: 2302
		public const uint UserInformationImapMessagesRetrievalMimeFormat = 809369603U;

		// Token: 0x040008FF RID: 2303
		public const uint UserInformationImapProtocolLoggingEnabled = 809435139U;

		// Token: 0x04000900 RID: 2304
		public const uint UserInformationImapSuppressReadReceipt = 809500683U;

		// Token: 0x04000901 RID: 2305
		public const uint UserInformationImapUseProtocolDefaults = 809566219U;

		// Token: 0x04000902 RID: 2306
		public const uint UserInformationIncludeInGarbageCollection = 809631755U;

		// Token: 0x04000903 RID: 2307
		public const uint UserInformationInitials = 809697311U;

		// Token: 0x04000904 RID: 2308
		public const uint UserInformationInPlaceHolds = 809766943U;

		// Token: 0x04000905 RID: 2309
		public const uint UserInformationInternalOnly = 809828363U;

		// Token: 0x04000906 RID: 2310
		public const uint UserInformationInternalUsageLocation = 809893919U;

		// Token: 0x04000907 RID: 2311
		public const uint UserInformationInternetEncoding = 809959427U;

		// Token: 0x04000908 RID: 2312
		public const uint UserInformationIsCalculatedTargetAddress = 810024971U;

		// Token: 0x04000909 RID: 2313
		public const uint UserInformationIsExcludedFromServingHierarchy = 810090507U;

		// Token: 0x0400090A RID: 2314
		public const uint UserInformationIsHierarchyReady = 810156043U;

		// Token: 0x0400090B RID: 2315
		public const uint UserInformationIsInactiveMailbox = 810221579U;

		// Token: 0x0400090C RID: 2316
		public const uint UserInformationIsSoftDeletedByDisable = 810287115U;

		// Token: 0x0400090D RID: 2317
		public const uint UserInformationIsSoftDeletedByRemove = 810352651U;

		// Token: 0x0400090E RID: 2318
		public const uint UserInformationIssueWarningQuota = 810418207U;

		// Token: 0x0400090F RID: 2319
		public const uint UserInformationJournalArchiveAddress = 810483743U;

		// Token: 0x04000910 RID: 2320
		public const uint UserInformationLanguages = 810553375U;

		// Token: 0x04000911 RID: 2321
		public const uint UserInformationLastExchangeChangedTime = 810614848U;

		// Token: 0x04000912 RID: 2322
		public const uint UserInformationLastName = 810680351U;

		// Token: 0x04000913 RID: 2323
		public const uint UserInformationLatitude = 810745859U;

		// Token: 0x04000914 RID: 2324
		public const uint UserInformationLEOEnabled = 810811403U;

		// Token: 0x04000915 RID: 2325
		public const uint UserInformationLocaleID = 810881027U;

		// Token: 0x04000916 RID: 2326
		public const uint UserInformationLongitude = 810942467U;

		// Token: 0x04000917 RID: 2327
		public const uint UserInformationMacAttachmentFormat = 811008003U;

		// Token: 0x04000918 RID: 2328
		public const uint UserInformationMailboxContainerGuid = 811073608U;

		// Token: 0x04000919 RID: 2329
		public const uint UserInformationMailboxMoveBatchName = 811139103U;

		// Token: 0x0400091A RID: 2330
		public const uint UserInformationMailboxMoveRemoteHostName = 811204639U;

		// Token: 0x0400091B RID: 2331
		public const uint UserInformationMailboxMoveStatus = 811270147U;

		// Token: 0x0400091C RID: 2332
		public const uint UserInformationMailboxRelease = 811335711U;

		// Token: 0x0400091D RID: 2333
		public const uint UserInformationMailTipTranslations = 811405343U;

		// Token: 0x0400091E RID: 2334
		public const uint UserInformationMAPIBlockOutlookNonCachedMode = 811466763U;

		// Token: 0x0400091F RID: 2335
		public const uint UserInformationMAPIBlockOutlookRpcHttp = 811532299U;

		// Token: 0x04000920 RID: 2336
		public const uint UserInformationMAPIBlockOutlookVersions = 811597855U;

		// Token: 0x04000921 RID: 2337
		public const uint UserInformationMAPIEnabled = 811663371U;

		// Token: 0x04000922 RID: 2338
		public const uint UserInformationMapiRecipient = 811728907U;

		// Token: 0x04000923 RID: 2339
		public const uint UserInformationMaxBlockedSenders = 811794435U;

		// Token: 0x04000924 RID: 2340
		public const uint UserInformationMaxReceiveSize = 811859999U;

		// Token: 0x04000925 RID: 2341
		public const uint UserInformationMaxSafeSenders = 811925507U;

		// Token: 0x04000926 RID: 2342
		public const uint UserInformationMaxSendSize = 811991071U;

		// Token: 0x04000927 RID: 2343
		public const uint UserInformationMemberName = 812056607U;

		// Token: 0x04000928 RID: 2344
		public const uint UserInformationMessageBodyFormat = 812122115U;

		// Token: 0x04000929 RID: 2345
		public const uint UserInformationMessageFormat = 812187651U;

		// Token: 0x0400092A RID: 2346
		public const uint UserInformationMessageTrackingReadStatusDisabled = 812253195U;

		// Token: 0x0400092B RID: 2347
		public const uint UserInformationMobileFeaturesEnabled = 812318723U;

		// Token: 0x0400092C RID: 2348
		public const uint UserInformationMobilePhone = 812384287U;

		// Token: 0x0400092D RID: 2349
		public const uint UserInformationModerationFlags = 812449795U;

		// Token: 0x0400092E RID: 2350
		public const uint UserInformationNotes = 812515359U;

		// Token: 0x0400092F RID: 2351
		public const uint UserInformationOccupation = 812580895U;

		// Token: 0x04000930 RID: 2352
		public const uint UserInformationOpenDomainRoutingDisabled = 812646411U;

		// Token: 0x04000931 RID: 2353
		public const uint UserInformationOtherHomePhone = 812716063U;

		// Token: 0x04000932 RID: 2354
		public const uint UserInformationOtherMobile = 812781599U;

		// Token: 0x04000933 RID: 2355
		public const uint UserInformationOtherTelephone = 812847135U;

		// Token: 0x04000934 RID: 2356
		public const uint UserInformationOWAEnabled = 812908555U;

		// Token: 0x04000935 RID: 2357
		public const uint UserInformationOWAforDevicesEnabled = 812974091U;

		// Token: 0x04000936 RID: 2358
		public const uint UserInformationPager = 813039647U;

		// Token: 0x04000937 RID: 2359
		public const uint UserInformationPersistedCapabilities = 813109251U;

		// Token: 0x04000938 RID: 2360
		public const uint UserInformationPhone = 813170719U;

		// Token: 0x04000939 RID: 2361
		public const uint UserInformationPhoneProviderId = 813236255U;

		// Token: 0x0400093A RID: 2362
		public const uint UserInformationPopEnabled = 813301771U;

		// Token: 0x0400093B RID: 2363
		public const uint UserInformationPopEnableExactRFC822Size = 813367307U;

		// Token: 0x0400093C RID: 2364
		public const uint UserInformationPopForceICalForCalendarRetrievalOption = 813432843U;

		// Token: 0x0400093D RID: 2365
		public const uint UserInformationPopMessagesRetrievalMimeFormat = 813498371U;

		// Token: 0x0400093E RID: 2366
		public const uint UserInformationPopProtocolLoggingEnabled = 813563907U;

		// Token: 0x0400093F RID: 2367
		public const uint UserInformationPopSuppressReadReceipt = 813629451U;

		// Token: 0x04000940 RID: 2368
		public const uint UserInformationPopUseProtocolDefaults = 813694987U;

		// Token: 0x04000941 RID: 2369
		public const uint UserInformationPostalCode = 813760543U;

		// Token: 0x04000942 RID: 2370
		public const uint UserInformationPostOfficeBox = 813830175U;

		// Token: 0x04000943 RID: 2371
		public const uint UserInformationPreviousExchangeGuid = 813891656U;

		// Token: 0x04000944 RID: 2372
		public const uint UserInformationPreviousRecipientTypeDetails = 813957123U;

		// Token: 0x04000945 RID: 2373
		public const uint UserInformationProhibitSendQuota = 814022687U;

		// Token: 0x04000946 RID: 2374
		public const uint UserInformationProhibitSendReceiveQuota = 814088223U;

		// Token: 0x04000947 RID: 2375
		public const uint UserInformationQueryBaseDNRestrictionEnabled = 814153739U;

		// Token: 0x04000948 RID: 2376
		public const uint UserInformationRecipientDisplayType = 814219267U;

		// Token: 0x04000949 RID: 2377
		public const uint UserInformationRecipientLimits = 814284831U;

		// Token: 0x0400094A RID: 2378
		public const uint UserInformationRecipientSoftDeletedStatus = 814350339U;

		// Token: 0x0400094B RID: 2379
		public const uint UserInformationRecoverableItemsQuota = 814415903U;

		// Token: 0x0400094C RID: 2380
		public const uint UserInformationRecoverableItemsWarningQuota = 814481439U;

		// Token: 0x0400094D RID: 2381
		public const uint UserInformationRegion = 814546975U;

		// Token: 0x0400094E RID: 2382
		public const uint UserInformationRemotePowerShellEnabled = 814612491U;

		// Token: 0x0400094F RID: 2383
		public const uint UserInformationRemoteRecipientType = 814678019U;

		// Token: 0x04000950 RID: 2384
		public const uint UserInformationRequireAllSendersAreAuthenticated = 814743563U;

		// Token: 0x04000951 RID: 2385
		public const uint UserInformationResetPasswordOnNextLogon = 814809099U;

		// Token: 0x04000952 RID: 2386
		public const uint UserInformationRetainDeletedItemsFor = 814874644U;

		// Token: 0x04000953 RID: 2387
		public const uint UserInformationRetainDeletedItemsUntilBackup = 814940171U;

		// Token: 0x04000954 RID: 2388
		public const uint UserInformationRulesQuota = 815005727U;

		// Token: 0x04000955 RID: 2389
		public const uint UserInformationShouldUseDefaultRetentionPolicy = 815071243U;

		// Token: 0x04000956 RID: 2390
		public const uint UserInformationSimpleDisplayName = 815136799U;

		// Token: 0x04000957 RID: 2391
		public const uint UserInformationSingleItemRecoveryEnabled = 815202315U;

		// Token: 0x04000958 RID: 2392
		public const uint UserInformationStateOrProvince = 815267871U;

		// Token: 0x04000959 RID: 2393
		public const uint UserInformationStreetAddress = 815333407U;

		// Token: 0x0400095A RID: 2394
		public const uint UserInformationSubscriberAccessEnabled = 815398923U;

		// Token: 0x0400095B RID: 2395
		public const uint UserInformationTextEncodedORAddress = 815464479U;

		// Token: 0x0400095C RID: 2396
		public const uint UserInformationTextMessagingState = 815534111U;

		// Token: 0x0400095D RID: 2397
		public const uint UserInformationTimezone = 815595551U;

		// Token: 0x0400095E RID: 2398
		public const uint UserInformationUCSImListMigrationCompleted = 815661067U;

		// Token: 0x0400095F RID: 2399
		public const uint UserInformationUpgradeDetails = 815726623U;

		// Token: 0x04000960 RID: 2400
		public const uint UserInformationUpgradeMessage = 815792159U;

		// Token: 0x04000961 RID: 2401
		public const uint UserInformationUpgradeRequest = 815857667U;

		// Token: 0x04000962 RID: 2402
		public const uint UserInformationUpgradeStage = 815923203U;

		// Token: 0x04000963 RID: 2403
		public const uint UserInformationUpgradeStageTimeStamp = 815988800U;

		// Token: 0x04000964 RID: 2404
		public const uint UserInformationUpgradeStatus = 816054275U;

		// Token: 0x04000965 RID: 2405
		public const uint UserInformationUsageLocation = 816119839U;

		// Token: 0x04000966 RID: 2406
		public const uint UserInformationUseMapiRichTextFormat = 816185347U;

		// Token: 0x04000967 RID: 2407
		public const uint UserInformationUsePreferMessageFormat = 816250891U;

		// Token: 0x04000968 RID: 2408
		public const uint UserInformationUseUCCAuditConfig = 816316427U;

		// Token: 0x04000969 RID: 2409
		public const uint UserInformationWebPage = 816381983U;

		// Token: 0x0400096A RID: 2410
		public const uint UserInformationWhenMailboxCreated = 816447552U;

		// Token: 0x0400096B RID: 2411
		public const uint UserInformationWhenSoftDeleted = 816513088U;

		// Token: 0x0400096C RID: 2412
		public const uint UserInformationBirthdayPrecision = 816578591U;

		// Token: 0x0400096D RID: 2413
		public const uint UserInformationNameVersion = 816644127U;

		// Token: 0x0400096E RID: 2414
		public const uint UserInformationOptInUser = 816709643U;

		// Token: 0x0400096F RID: 2415
		public const uint UserInformationIsMigratedConsumerMailbox = 816775179U;

		// Token: 0x04000970 RID: 2416
		public const uint UserInformationMigrationDryRun = 816840715U;

		// Token: 0x04000971 RID: 2417
		public const uint UserInformationIsPremiumConsumerMailbox = 816906251U;

		// Token: 0x04000972 RID: 2418
		public const uint UserInformationAlternateSupportEmailAddresses = 816971807U;

		// Token: 0x04000973 RID: 2419
		public const uint UserInformationEmailAddresses = 817041439U;

		// Token: 0x04000974 RID: 2420
		public const uint UserInformationMapiHttpEnabled = 819331083U;

		// Token: 0x04000975 RID: 2421
		public const uint UserInformationMAPIBlockOutlookExternalConnectivity = 819396619U;

		// Token: 0x04000976 RID: 2422
		public const uint FormVersion = 855703583U;

		// Token: 0x04000977 RID: 2423
		public const uint FormCLSID = 855769160U;

		// Token: 0x04000978 RID: 2424
		public const uint FormContactName = 855834655U;

		// Token: 0x04000979 RID: 2425
		public const uint FormCategory = 855900191U;

		// Token: 0x0400097A RID: 2426
		public const uint FormCategorySub = 855965727U;

		// Token: 0x0400097B RID: 2427
		public const uint FormHidden = 856096779U;

		// Token: 0x0400097C RID: 2428
		public const uint FormDesignerName = 856162335U;

		// Token: 0x0400097D RID: 2429
		public const uint FormDesignerGuid = 856227912U;

		// Token: 0x0400097E RID: 2430
		public const uint FormMessageBehavior = 856293379U;

		// Token: 0x0400097F RID: 2431
		public const uint MessageTableTotalPages = 872480771U;

		// Token: 0x04000980 RID: 2432
		public const uint MessageTableAvailablePages = 872546307U;

		// Token: 0x04000981 RID: 2433
		public const uint OtherTablesTotalPages = 872611843U;

		// Token: 0x04000982 RID: 2434
		public const uint OtherTablesAvailablePages = 872677379U;

		// Token: 0x04000983 RID: 2435
		public const uint AttachmentTableTotalPages = 872742915U;

		// Token: 0x04000984 RID: 2436
		public const uint AttachmentTableAvailablePages = 872808451U;

		// Token: 0x04000985 RID: 2437
		public const uint MailboxTypeVersion = 872873987U;

		// Token: 0x04000986 RID: 2438
		public const uint MailboxPartitionMailboxGuids = 872943688U;

		// Token: 0x04000987 RID: 2439
		public const uint StoreSupportMask = 873267203U;

		// Token: 0x04000988 RID: 2440
		public const uint StoreState = 873332739U;

		// Token: 0x04000989 RID: 2441
		public const uint IPMSubtreeSearchKey = 873464066U;

		// Token: 0x0400098A RID: 2442
		public const uint IPMOutboxSearchKey = 873529602U;

		// Token: 0x0400098B RID: 2443
		public const uint IPMWastebasketSearchKey = 873595138U;

		// Token: 0x0400098C RID: 2444
		public const uint IPMSentmailSearchKey = 873660674U;

		// Token: 0x0400098D RID: 2445
		public const uint MdbProvider = 873726210U;

		// Token: 0x0400098E RID: 2446
		public const uint ReceiveFolderSettings = 873791501U;

		// Token: 0x0400098F RID: 2447
		public const uint LocalDirectoryEntryID = 873857282U;

		// Token: 0x04000990 RID: 2448
		public const uint ProviderDisplayIcon = 873922591U;

		// Token: 0x04000991 RID: 2449
		public const uint ProviderDisplayName = 873988127U;

		// Token: 0x04000992 RID: 2450
		public const uint ControlDataForCalendarRepairAssistant = 874512642U;

		// Token: 0x04000993 RID: 2451
		public const uint ControlDataForSharingPolicyAssistant = 874578178U;

		// Token: 0x04000994 RID: 2452
		public const uint ControlDataForElcAssistant = 874643714U;

		// Token: 0x04000995 RID: 2453
		public const uint ControlDataForTopNWordsAssistant = 874709250U;

		// Token: 0x04000996 RID: 2454
		public const uint ControlDataForJunkEmailAssistant = 874774786U;

		// Token: 0x04000997 RID: 2455
		public const uint ControlDataForCalendarSyncAssistant = 874840322U;

		// Token: 0x04000998 RID: 2456
		public const uint ExternalSharingCalendarSubscriptionCount = 874905603U;

		// Token: 0x04000999 RID: 2457
		public const uint ControlDataForUMReportingAssistant = 874971394U;

		// Token: 0x0400099A RID: 2458
		public const uint HasUMReportData = 875036683U;

		// Token: 0x0400099B RID: 2459
		public const uint InternetCalendarSubscriptionCount = 875102211U;

		// Token: 0x0400099C RID: 2460
		public const uint ExternalSharingContactSubscriptionCount = 875167747U;

		// Token: 0x0400099D RID: 2461
		public const uint JunkEmailSafeListDirty = 875233283U;

		// Token: 0x0400099E RID: 2462
		public const uint IsTopNEnabled = 875298827U;

		// Token: 0x0400099F RID: 2463
		public const uint LastSharingPolicyAppliedId = 875364610U;

		// Token: 0x040009A0 RID: 2464
		public const uint LastSharingPolicyAppliedHash = 875430146U;

		// Token: 0x040009A1 RID: 2465
		public const uint LastSharingPolicyAppliedTime = 875495488U;

		// Token: 0x040009A2 RID: 2466
		public const uint OofScheduleStart = 875561024U;

		// Token: 0x040009A3 RID: 2467
		public const uint OofScheduleEnd = 875626560U;

		// Token: 0x040009A4 RID: 2468
		public const uint ControlDataForDirectoryProcessorAssistant = 875692290U;

		// Token: 0x040009A5 RID: 2469
		public const uint NeedsDirectoryProcessor = 875757579U;

		// Token: 0x040009A6 RID: 2470
		public const uint RetentionQueryIds = 875827231U;

		// Token: 0x040009A7 RID: 2471
		public const uint RetentionQueryInfo = 875888660U;

		// Token: 0x040009A8 RID: 2472
		public const uint ControlDataForPublicFolderAssistant = 876019970U;

		// Token: 0x040009A9 RID: 2473
		public const uint ControlDataForInferenceTrainingAssistant = 876085506U;

		// Token: 0x040009AA RID: 2474
		public const uint InferenceEnabled = 876150795U;

		// Token: 0x040009AB RID: 2475
		public const uint ControlDataForContactLinkingAssistant = 876216578U;

		// Token: 0x040009AC RID: 2476
		public const uint ContactLinking = 876281859U;

		// Token: 0x040009AD RID: 2477
		public const uint ControlDataForOABGeneratorAssistant = 876347650U;

		// Token: 0x040009AE RID: 2478
		public const uint ContactSaveVersion = 876412931U;

		// Token: 0x040009AF RID: 2479
		public const uint ControlDataForOrgContactsSyncAssistant = 876478722U;

		// Token: 0x040009B0 RID: 2480
		public const uint OrgContactsSyncTimestamp = 876544064U;

		// Token: 0x040009B1 RID: 2481
		public const uint PushNotificationSubscriptionType = 876609794U;

		// Token: 0x040009B2 RID: 2482
		public const uint OrgContactsSyncADWatermark = 876675136U;

		// Token: 0x040009B3 RID: 2483
		public const uint ControlDataForInferenceDataCollectionAssistant = 876740866U;

		// Token: 0x040009B4 RID: 2484
		public const uint InferenceDataCollectionProcessingState = 876806402U;

		// Token: 0x040009B5 RID: 2485
		public const uint ControlDataForPeopleRelevanceAssistant = 876871938U;

		// Token: 0x040009B6 RID: 2486
		public const uint SiteMailboxInternalState = 876937219U;

		// Token: 0x040009B7 RID: 2487
		public const uint ControlDataForSiteMailboxAssistant = 877003010U;

		// Token: 0x040009B8 RID: 2488
		public const uint InferenceTrainingLastContentCount = 877068291U;

		// Token: 0x040009B9 RID: 2489
		public const uint InferenceTrainingLastAttemptTimestamp = 877133888U;

		// Token: 0x040009BA RID: 2490
		public const uint InferenceTrainingLastSuccessTimestamp = 877199424U;

		// Token: 0x040009BB RID: 2491
		public const uint InferenceUserCapabilityFlags = 877264899U;

		// Token: 0x040009BC RID: 2492
		public const uint ControlDataForMailboxAssociationReplicationAssistant = 877330690U;

		// Token: 0x040009BD RID: 2493
		public const uint MailboxAssociationNextReplicationTime = 877396032U;

		// Token: 0x040009BE RID: 2494
		public const uint MailboxAssociationProcessingFlags = 877461507U;

		// Token: 0x040009BF RID: 2495
		public const uint ControlDataForSharePointSignalStoreAssistant = 877527298U;

		// Token: 0x040009C0 RID: 2496
		public const uint ControlDataForPeopleCentricTriageAssistant = 877592834U;

		// Token: 0x040009C1 RID: 2497
		public const uint NotificationBrokerSubscriptions = 877658115U;

		// Token: 0x040009C2 RID: 2498
		public const uint GroupMailboxPermissionsVersion = 877723651U;

		// Token: 0x040009C3 RID: 2499
		public const uint ElcLastRunTotalProcessingTime = 877789204U;

		// Token: 0x040009C4 RID: 2500
		public const uint ElcLastRunSubAssistantProcessingTime = 877854740U;

		// Token: 0x040009C5 RID: 2501
		public const uint ElcLastRunUpdatedFolderCount = 877920276U;

		// Token: 0x040009C6 RID: 2502
		public const uint ElcLastRunTaggedFolderCount = 877985812U;

		// Token: 0x040009C7 RID: 2503
		public const uint ElcLastRunUpdatedItemCount = 878051348U;

		// Token: 0x040009C8 RID: 2504
		public const uint ElcLastRunTaggedWithArchiveItemCount = 878116884U;

		// Token: 0x040009C9 RID: 2505
		public const uint ElcLastRunTaggedWithExpiryItemCount = 878182420U;

		// Token: 0x040009CA RID: 2506
		public const uint ElcLastRunDeletedFromRootItemCount = 878247956U;

		// Token: 0x040009CB RID: 2507
		public const uint ElcLastRunDeletedFromDumpsterItemCount = 878313492U;

		// Token: 0x040009CC RID: 2508
		public const uint ElcLastRunArchivedFromRootItemCount = 878379028U;

		// Token: 0x040009CD RID: 2509
		public const uint ElcLastRunArchivedFromDumpsterItemCount = 878444564U;

		// Token: 0x040009CE RID: 2510
		public const uint ScheduledISIntegLastFinished = 878510144U;

		// Token: 0x040009CF RID: 2511
		public const uint ControlDataForSearchIndexRepairAssistant = 878575874U;

		// Token: 0x040009D0 RID: 2512
		public const uint ELCLastSuccessTimestamp = 878641216U;

		// Token: 0x040009D1 RID: 2513
		public const uint EventEmailReminderTimer = 878706752U;

		// Token: 0x040009D2 RID: 2514
		public const uint InferenceTruthLoggingLastAttemptTimestamp = 878772288U;

		// Token: 0x040009D3 RID: 2515
		public const uint InferenceTruthLoggingLastSuccessTimestamp = 878837824U;

		// Token: 0x040009D4 RID: 2516
		public const uint ControlDataForGroupMailboxAssistant = 878903554U;

		// Token: 0x040009D5 RID: 2517
		public const uint ItemsPendingUpgrade = 878968835U;

		// Token: 0x040009D6 RID: 2518
		public const uint ConsumerSharingCalendarSubscriptionCount = 879034371U;

		// Token: 0x040009D7 RID: 2519
		public const uint GroupMailboxGeneratedPhotoVersion = 879099907U;

		// Token: 0x040009D8 RID: 2520
		public const uint GroupMailboxGeneratedPhotoSignature = 879165698U;

		// Token: 0x040009D9 RID: 2521
		public const uint GroupMailboxExchangeResourcesPublishedVersion = 879230979U;

		// Token: 0x040009DA RID: 2522
		public const uint ValidFolderMask = 903806979U;

		// Token: 0x040009DB RID: 2523
		public const uint IPMSubtreeEntryId = 903872770U;

		// Token: 0x040009DC RID: 2524
		public const uint IPMOutboxEntryId = 904003842U;

		// Token: 0x040009DD RID: 2525
		public const uint IPMWastebasketEntryId = 904069378U;

		// Token: 0x040009DE RID: 2526
		public const uint IPMSentmailEntryId = 904134914U;

		// Token: 0x040009DF RID: 2527
		public const uint IPMViewsEntryId = 904200450U;

		// Token: 0x040009E0 RID: 2528
		public const uint IPMCommonViewsEntryId = 904265986U;

		// Token: 0x040009E1 RID: 2529
		public const uint IPMConversationsEntryId = 904659202U;

		// Token: 0x040009E2 RID: 2530
		public const uint IPMAllItemsEntryId = 904790274U;

		// Token: 0x040009E3 RID: 2531
		public const uint IPMSharingEntryId = 904855810U;

		// Token: 0x040009E4 RID: 2532
		public const uint AdminDataEntryId = 905773314U;

		// Token: 0x040009E5 RID: 2533
		public const uint UnsearchableItems = 905838850U;

		// Token: 0x040009E6 RID: 2534
		public const uint ContainerFlags = 905969667U;

		// Token: 0x040009E7 RID: 2535
		public const uint IPMFinderEntryId = 905969922U;

		// Token: 0x040009E8 RID: 2536
		public const uint FolderType = 906035203U;

		// Token: 0x040009E9 RID: 2537
		public const uint ContentCount = 906100739U;

		// Token: 0x040009EA RID: 2538
		public const uint ContentCountInt64 = 906100756U;

		// Token: 0x040009EB RID: 2539
		public const uint UnreadCount = 906166275U;

		// Token: 0x040009EC RID: 2540
		public const uint UnreadCountInt64 = 906166292U;

		// Token: 0x040009ED RID: 2541
		public const uint DetailsTable = 906297357U;

		// Token: 0x040009EE RID: 2542
		public const uint Search = 906428429U;

		// Token: 0x040009EF RID: 2543
		public const uint Selectable = 906559499U;

		// Token: 0x040009F0 RID: 2544
		public const uint Subfolders = 906625035U;

		// Token: 0x040009F1 RID: 2545
		public const uint FolderStatus = 906690563U;

		// Token: 0x040009F2 RID: 2546
		public const uint AmbiguousNameResolution = 906756127U;

		// Token: 0x040009F3 RID: 2547
		public const uint ContentsSortOrder = 906825731U;

		// Token: 0x040009F4 RID: 2548
		public const uint ContainerHierarchy = 906887181U;

		// Token: 0x040009F5 RID: 2549
		public const uint ContainerContents = 906952717U;

		// Token: 0x040009F6 RID: 2550
		public const uint FolderAssociatedContents = 907018253U;

		// Token: 0x040009F7 RID: 2551
		public const uint ContainerClass = 907214879U;

		// Token: 0x040009F8 RID: 2552
		public const uint ContainerModifyVersion = 907280404U;

		// Token: 0x040009F9 RID: 2553
		public const uint ABProviderId = 907346178U;

		// Token: 0x040009FA RID: 2554
		public const uint DefaultViewEntryId = 907411714U;

		// Token: 0x040009FB RID: 2555
		public const uint AssociatedContentCount = 907476995U;

		// Token: 0x040009FC RID: 2556
		public const uint AssociatedContentCountInt64 = 907477012U;

		// Token: 0x040009FD RID: 2557
		public const uint PackedNamedProps = 907804930U;

		// Token: 0x040009FE RID: 2558
		public const uint AllowAgeOut = 908001291U;

		// Token: 0x040009FF RID: 2559
		public const uint SearchFolderMsgCount = 910426115U;

		// Token: 0x04000A00 RID: 2560
		public const uint PartOfContentIndexing = 910491659U;

		// Token: 0x04000A01 RID: 2561
		public const uint OwnerLogonUserConfigurationCache = 910557442U;

		// Token: 0x04000A02 RID: 2562
		public const uint SearchFolderAgeOutTimeout = 910622723U;

		// Token: 0x04000A03 RID: 2563
		public const uint SearchFolderPopulationResult = 910688259U;

		// Token: 0x04000A04 RID: 2564
		public const uint SearchFolderPopulationDiagnostics = 910754050U;

		// Token: 0x04000A05 RID: 2565
		public const uint ConversationTopicHashEntries = 912261378U;

		// Token: 0x04000A06 RID: 2566
		public const uint ContentAggregationFlags = 915341315U;

		// Token: 0x04000A07 RID: 2567
		public const uint TransportRulesSnapshot = 915407106U;

		// Token: 0x04000A08 RID: 2568
		public const uint TransportRulesSnapshotId = 915472456U;

		// Token: 0x04000A09 RID: 2569
		public const uint CurrentIPMWasteBasketContainerEntryId = 919535874U;

		// Token: 0x04000A0A RID: 2570
		public const uint IPMAppointmentEntryId = 919601410U;

		// Token: 0x04000A0B RID: 2571
		public const uint IPMContactEntryId = 919666946U;

		// Token: 0x04000A0C RID: 2572
		public const uint IPMJournalEntryId = 919732482U;

		// Token: 0x04000A0D RID: 2573
		public const uint IPMNoteEntryId = 919798018U;

		// Token: 0x04000A0E RID: 2574
		public const uint IPMTaskEntryId = 919863554U;

		// Token: 0x04000A0F RID: 2575
		public const uint REMOnlineEntryId = 919929090U;

		// Token: 0x04000A10 RID: 2576
		public const uint IPMOfflineEntryId = 919994626U;

		// Token: 0x04000A11 RID: 2577
		public const uint IPMDraftsEntryId = 920060162U;

		// Token: 0x04000A12 RID: 2578
		public const uint AdditionalRENEntryIds = 920129794U;

		// Token: 0x04000A13 RID: 2579
		public const uint AdditionalRENEntryIdsExtended = 920191234U;

		// Token: 0x04000A14 RID: 2580
		public const uint AdditionalRENEntryIdsExtendedMV = 920195330U;

		// Token: 0x04000A15 RID: 2581
		public const uint ExtendedFolderFlags = 920256770U;

		// Token: 0x04000A16 RID: 2582
		public const uint ContainerTimestamp = 920322112U;

		// Token: 0x04000A17 RID: 2583
		public const uint AppointmentColorName = 920387842U;

		// Token: 0x04000A18 RID: 2584
		public const uint INetUnread = 920453123U;

		// Token: 0x04000A19 RID: 2585
		public const uint NetFolderFlags = 920518659U;

		// Token: 0x04000A1A RID: 2586
		public const uint FolderWebViewInfo = 920584450U;

		// Token: 0x04000A1B RID: 2587
		public const uint FolderWebViewInfoExtended = 920649986U;

		// Token: 0x04000A1C RID: 2588
		public const uint FolderViewFlags = 920715267U;

		// Token: 0x04000A1D RID: 2589
		public const uint FreeBusyEntryIds = 920916226U;

		// Token: 0x04000A1E RID: 2590
		public const uint DefaultPostMsgClass = 920977439U;

		// Token: 0x04000A1F RID: 2591
		public const uint DefaultPostDisplayName = 921042975U;

		// Token: 0x04000A20 RID: 2592
		public const uint FolderViewList = 921370882U;

		// Token: 0x04000A21 RID: 2593
		public const uint AgingPeriod = 921436163U;

		// Token: 0x04000A22 RID: 2594
		public const uint AgingGranularity = 921567235U;

		// Token: 0x04000A23 RID: 2595
		public const uint DefaultFoldersLocaleId = 921698307U;

		// Token: 0x04000A24 RID: 2596
		public const uint InternalAccess = 921763851U;

		// Token: 0x04000A25 RID: 2597
		public const uint AttachmentX400Parameters = 922747138U;

		// Token: 0x04000A26 RID: 2598
		public const uint Content = 922812674U;

		// Token: 0x04000A27 RID: 2599
		public const uint ContentObj = 922812429U;

		// Token: 0x04000A28 RID: 2600
		public const uint AttachmentEncoding = 922878210U;

		// Token: 0x04000A29 RID: 2601
		public const uint ContentId = 922943519U;

		// Token: 0x04000A2A RID: 2602
		public const uint ContentType = 923009055U;

		// Token: 0x04000A2B RID: 2603
		public const uint AttachMethod = 923074563U;

		// Token: 0x04000A2C RID: 2604
		public const uint MimeUrl = 923205663U;

		// Token: 0x04000A2D RID: 2605
		public const uint AttachmentPathName = 923271199U;

		// Token: 0x04000A2E RID: 2606
		public const uint AttachRendering = 923336962U;

		// Token: 0x04000A2F RID: 2607
		public const uint AttachTag = 923402498U;

		// Token: 0x04000A30 RID: 2608
		public const uint RenderingPosition = 923467779U;

		// Token: 0x04000A31 RID: 2609
		public const uint AttachTransportName = 923533343U;

		// Token: 0x04000A32 RID: 2610
		public const uint AttachmentLongPathName = 923598879U;

		// Token: 0x04000A33 RID: 2611
		public const uint AttachmentMimeTag = 923664415U;

		// Token: 0x04000A34 RID: 2612
		public const uint AttachAdditionalInfo = 923730178U;

		// Token: 0x04000A35 RID: 2613
		public const uint AttachmentMimeSequence = 923795459U;

		// Token: 0x04000A36 RID: 2614
		public const uint AttachContentBase = 923861023U;

		// Token: 0x04000A37 RID: 2615
		public const uint AttachContentId = 923926559U;

		// Token: 0x04000A38 RID: 2616
		public const uint AttachContentLocation = 923992095U;

		// Token: 0x04000A39 RID: 2617
		public const uint AttachmentFlags = 924057603U;

		// Token: 0x04000A3A RID: 2618
		public const uint AttachDisposition = 924188703U;

		// Token: 0x04000A3B RID: 2619
		public const uint AttachPayloadProviderGuidString = 924385311U;

		// Token: 0x04000A3C RID: 2620
		public const uint AttachPayloadClass = 924450847U;

		// Token: 0x04000A3D RID: 2621
		public const uint TextAttachmentCharset = 924516383U;

		// Token: 0x04000A3E RID: 2622
		public const uint SyncEventSuppressGuid = 947912962U;

		// Token: 0x04000A3F RID: 2623
		public const uint DisplayType = 956301315U;

		// Token: 0x04000A40 RID: 2624
		public const uint TemplateId = 956432642U;

		// Token: 0x04000A41 RID: 2625
		public const uint CapabilitiesTable = 956498178U;

		// Token: 0x04000A42 RID: 2626
		public const uint PrimaryCapability = 956563714U;

		// Token: 0x04000A43 RID: 2627
		public const uint EMSABDisplayTypeEx = 956628995U;

		// Token: 0x04000A44 RID: 2628
		public const uint SmtpAddress = 972947487U;

		// Token: 0x04000A45 RID: 2629
		public const uint EMSABDisplayNamePrintable = 973013023U;

		// Token: 0x04000A46 RID: 2630
		public const uint SimpleDisplayName = 973013023U;

		// Token: 0x04000A47 RID: 2631
		public const uint Account = 973078559U;

		// Token: 0x04000A48 RID: 2632
		public const uint AlternateRecipient = 973144322U;

		// Token: 0x04000A49 RID: 2633
		public const uint CallbackTelephoneNumber = 973209631U;

		// Token: 0x04000A4A RID: 2634
		public const uint ConversionProhibited = 973275147U;

		// Token: 0x04000A4B RID: 2635
		public const uint Generation = 973406239U;

		// Token: 0x04000A4C RID: 2636
		public const uint GivenName = 973471775U;

		// Token: 0x04000A4D RID: 2637
		public const uint GovernmentIDNumber = 973537311U;

		// Token: 0x04000A4E RID: 2638
		public const uint BusinessTelephoneNumber = 973602847U;

		// Token: 0x04000A4F RID: 2639
		public const uint HomeTelephoneNumber = 973668383U;

		// Token: 0x04000A50 RID: 2640
		public const uint Initials = 973733919U;

		// Token: 0x04000A51 RID: 2641
		public const uint Keyword = 973799455U;

		// Token: 0x04000A52 RID: 2642
		public const uint Language = 973864991U;

		// Token: 0x04000A53 RID: 2643
		public const uint Location = 973930527U;

		// Token: 0x04000A54 RID: 2644
		public const uint MailPermission = 973996043U;

		// Token: 0x04000A55 RID: 2645
		public const uint MHSCommonName = 974061599U;

		// Token: 0x04000A56 RID: 2646
		public const uint OrganizationalIDNumber = 974127135U;

		// Token: 0x04000A57 RID: 2647
		public const uint SurName = 974192671U;

		// Token: 0x04000A58 RID: 2648
		public const uint OriginalEntryId = 974258434U;

		// Token: 0x04000A59 RID: 2649
		public const uint OriginalDisplayName = 974323743U;

		// Token: 0x04000A5A RID: 2650
		public const uint OriginalSearchKey = 974389506U;

		// Token: 0x04000A5B RID: 2651
		public const uint PostalAddress = 974454815U;

		// Token: 0x04000A5C RID: 2652
		public const uint CompanyName = 974520351U;

		// Token: 0x04000A5D RID: 2653
		public const uint Title = 974585887U;

		// Token: 0x04000A5E RID: 2654
		public const uint DepartmentName = 974651423U;

		// Token: 0x04000A5F RID: 2655
		public const uint OfficeLocation = 974716959U;

		// Token: 0x04000A60 RID: 2656
		public const uint PrimaryTelephoneNumber = 974782495U;

		// Token: 0x04000A61 RID: 2657
		public const uint Business2TelephoneNumber = 974848031U;

		// Token: 0x04000A62 RID: 2658
		public const uint Business2TelephoneNumberMv = 974852127U;

		// Token: 0x04000A63 RID: 2659
		public const uint MobileTelephoneNumber = 974913567U;

		// Token: 0x04000A64 RID: 2660
		public const uint RadioTelephoneNumber = 974979103U;

		// Token: 0x04000A65 RID: 2661
		public const uint CarTelephoneNumber = 975044639U;

		// Token: 0x04000A66 RID: 2662
		public const uint OtherTelephoneNumber = 975110175U;

		// Token: 0x04000A67 RID: 2663
		public const uint TransmitableDisplayName = 975175711U;

		// Token: 0x04000A68 RID: 2664
		public const uint PagerTelephoneNumber = 975241247U;

		// Token: 0x04000A69 RID: 2665
		public const uint UserCertificate = 975307010U;

		// Token: 0x04000A6A RID: 2666
		public const uint PrimaryFaxNumber = 975372319U;

		// Token: 0x04000A6B RID: 2667
		public const uint BusinessFaxNumber = 975437855U;

		// Token: 0x04000A6C RID: 2668
		public const uint HomeFaxNumber = 975503391U;

		// Token: 0x04000A6D RID: 2669
		public const uint Country = 975568927U;

		// Token: 0x04000A6E RID: 2670
		public const uint Locality = 975634463U;

		// Token: 0x04000A6F RID: 2671
		public const uint StateOrProvince = 975699999U;

		// Token: 0x04000A70 RID: 2672
		public const uint StreetAddress = 975765535U;

		// Token: 0x04000A71 RID: 2673
		public const uint PostalCode = 975831071U;

		// Token: 0x04000A72 RID: 2674
		public const uint PostOfficeBox = 975896607U;

		// Token: 0x04000A73 RID: 2675
		public const uint TelexNumber = 975962143U;

		// Token: 0x04000A74 RID: 2676
		public const uint ISDNNumber = 976027679U;

		// Token: 0x04000A75 RID: 2677
		public const uint AssistantTelephoneNumber = 976093215U;

		// Token: 0x04000A76 RID: 2678
		public const uint Home2TelephoneNumber = 976158751U;

		// Token: 0x04000A77 RID: 2679
		public const uint Home2TelephoneNumberMv = 976162847U;

		// Token: 0x04000A78 RID: 2680
		public const uint Assistant = 976224287U;

		// Token: 0x04000A79 RID: 2681
		public const uint SendRichInfo = 977272843U;

		// Token: 0x04000A7A RID: 2682
		public const uint WeddingAnniversary = 977338432U;

		// Token: 0x04000A7B RID: 2683
		public const uint Birthday = 977403968U;

		// Token: 0x04000A7C RID: 2684
		public const uint Hobbies = 977469471U;

		// Token: 0x04000A7D RID: 2685
		public const uint MiddleName = 977535007U;

		// Token: 0x04000A7E RID: 2686
		public const uint DisplayNamePrefix = 977600543U;

		// Token: 0x04000A7F RID: 2687
		public const uint Profession = 977666079U;

		// Token: 0x04000A80 RID: 2688
		public const uint ReferredByName = 977731615U;

		// Token: 0x04000A81 RID: 2689
		public const uint SpouseName = 977797151U;

		// Token: 0x04000A82 RID: 2690
		public const uint ComputerNetworkName = 977862687U;

		// Token: 0x04000A83 RID: 2691
		public const uint CustomerId = 977928223U;

		// Token: 0x04000A84 RID: 2692
		public const uint TTYTDDPhoneNumber = 977993759U;

		// Token: 0x04000A85 RID: 2693
		public const uint FTPSite = 978059295U;

		// Token: 0x04000A86 RID: 2694
		public const uint Gender = 978124802U;

		// Token: 0x04000A87 RID: 2695
		public const uint ManagerName = 978190367U;

		// Token: 0x04000A88 RID: 2696
		public const uint NickName = 978255903U;

		// Token: 0x04000A89 RID: 2697
		public const uint PersonalHomePage = 978321439U;

		// Token: 0x04000A8A RID: 2698
		public const uint BusinessHomePage = 978386975U;

		// Token: 0x04000A8B RID: 2699
		public const uint ContactVersion = 978452552U;

		// Token: 0x04000A8C RID: 2700
		public const uint ContactEntryIds = 978522370U;

		// Token: 0x04000A8D RID: 2701
		public const uint ContactAddressTypes = 978587679U;

		// Token: 0x04000A8E RID: 2702
		public const uint ContactDefaultAddressIndex = 978649091U;

		// Token: 0x04000A8F RID: 2703
		public const uint ContactEmailAddress = 978718751U;

		// Token: 0x04000A90 RID: 2704
		public const uint CompanyMainPhoneNumber = 978780191U;

		// Token: 0x04000A91 RID: 2705
		public const uint ChildrensNames = 978849823U;

		// Token: 0x04000A92 RID: 2706
		public const uint HomeAddressCity = 978911263U;

		// Token: 0x04000A93 RID: 2707
		public const uint HomeAddressCountry = 978976799U;

		// Token: 0x04000A94 RID: 2708
		public const uint HomeAddressPostalCode = 979042335U;

		// Token: 0x04000A95 RID: 2709
		public const uint HomeAddressStateOrProvince = 979107871U;

		// Token: 0x04000A96 RID: 2710
		public const uint HomeAddressStreet = 979173407U;

		// Token: 0x04000A97 RID: 2711
		public const uint HomeAddressPostOfficeBox = 979238943U;

		// Token: 0x04000A98 RID: 2712
		public const uint OtherAddressCity = 979304479U;

		// Token: 0x04000A99 RID: 2713
		public const uint OtherAddressCountry = 979370015U;

		// Token: 0x04000A9A RID: 2714
		public const uint OtherAddressPostalCode = 979435551U;

		// Token: 0x04000A9B RID: 2715
		public const uint OtherAddressStateOrProvince = 979501087U;

		// Token: 0x04000A9C RID: 2716
		public const uint OtherAddressStreet = 979566623U;

		// Token: 0x04000A9D RID: 2717
		public const uint OtherAddressPostOfficeBox = 979632159U;

		// Token: 0x04000A9E RID: 2718
		public const uint UserX509CertificateABSearchPath = 980422914U;

		// Token: 0x04000A9F RID: 2719
		public const uint SendInternetEncoding = 980484099U;

		// Token: 0x04000AA0 RID: 2720
		public const uint PartnerNetworkId = 980811807U;

		// Token: 0x04000AA1 RID: 2721
		public const uint PartnerNetworkUserId = 980877343U;

		// Token: 0x04000AA2 RID: 2722
		public const uint PartnerNetworkThumbnailPhotoUrl = 980942879U;

		// Token: 0x04000AA3 RID: 2723
		public const uint PartnerNetworkProfilePhotoUrl = 981008415U;

		// Token: 0x04000AA4 RID: 2724
		public const uint PartnerNetworkContactType = 981073951U;

		// Token: 0x04000AA5 RID: 2725
		public const uint RelevanceScore = 981139459U;

		// Token: 0x04000AA6 RID: 2726
		public const uint IsDistributionListContact = 981205003U;

		// Token: 0x04000AA7 RID: 2727
		public const uint IsPromotedContact = 981270539U;

		// Token: 0x04000AA8 RID: 2728
		public const uint OrgUnitName = 1006501919U;

		// Token: 0x04000AA9 RID: 2729
		public const uint OrganizationName = 1006567455U;

		// Token: 0x04000AAA RID: 2730
		public const uint TestBlobProperty = 1023410196U;

		// Token: 0x04000AAB RID: 2731
		public const uint StoreProviders = 1023410434U;

		// Token: 0x04000AAC RID: 2732
		public const uint AddressBookProviders = 1023475970U;

		// Token: 0x04000AAD RID: 2733
		public const uint TransportProviders = 1023541506U;

		// Token: 0x04000AAE RID: 2734
		public const uint FilteringHooks = 1023934722U;

		// Token: 0x04000AAF RID: 2735
		public const uint ServiceName = 1024000031U;

		// Token: 0x04000AB0 RID: 2736
		public const uint ServiceDLLName = 1024065567U;

		// Token: 0x04000AB1 RID: 2737
		public const uint ServiceEntryName = 1024131103U;

		// Token: 0x04000AB2 RID: 2738
		public const uint ServiceUid = 1024196866U;

		// Token: 0x04000AB3 RID: 2739
		public const uint ServiceExtraUid = 1024262402U;

		// Token: 0x04000AB4 RID: 2740
		public const uint Services = 1024327938U;

		// Token: 0x04000AB5 RID: 2741
		public const uint ServiceSupportFiles = 1024397343U;

		// Token: 0x04000AB6 RID: 2742
		public const uint ServiceDeleteFiles = 1024462879U;

		// Token: 0x04000AB7 RID: 2743
		public const uint ProfileName = 1024589855U;

		// Token: 0x04000AB8 RID: 2744
		public const uint AdminSecurityDescriptor = 1025573122U;

		// Token: 0x04000AB9 RID: 2745
		public const uint Win32NTSecurityDescriptor = 1025638658U;

		// Token: 0x04000ABA RID: 2746
		public const uint NonWin32ACL = 1025703947U;

		// Token: 0x04000ABB RID: 2747
		public const uint ItemLevelACL = 1025769483U;

		// Token: 0x04000ABC RID: 2748
		public const uint ICSGid = 1026425090U;

		// Token: 0x04000ABD RID: 2749
		public const uint SystemFolderFlags = 1026490371U;

		// Token: 0x04000ABE RID: 2750
		public const uint MaterializedRestrictionSearchRoot = 1033634050U;

		// Token: 0x04000ABF RID: 2751
		public const uint ScheduledISIntegCorruptionCount = 1033699331U;

		// Token: 0x04000AC0 RID: 2752
		public const uint ScheduledISIntegExecutionTime = 1033764867U;

		// Token: 0x04000AC1 RID: 2753
		public const uint MailboxPartitionNumber = 1033830403U;

		// Token: 0x04000AC2 RID: 2754
		public const uint MailboxNumberInternal = 1033895939U;

		// Token: 0x04000AC3 RID: 2755
		public const uint QueryCriteriaInternal = 1033961730U;

		// Token: 0x04000AC4 RID: 2756
		public const uint LastQuotaNotificationTime = 1034027072U;

		// Token: 0x04000AC5 RID: 2757
		public const uint PropertyPromotionInProgressHiddenItems = 1034092555U;

		// Token: 0x04000AC6 RID: 2758
		public const uint PropertyPromotionInProgressNormalItems = 1034158091U;

		// Token: 0x04000AC7 RID: 2759
		public const uint VirtualParentDisplay = 1034223647U;

		// Token: 0x04000AC8 RID: 2760
		public const uint MailboxTypeDetail = 1034289155U;

		// Token: 0x04000AC9 RID: 2761
		public const uint InternalTenantHint = 1034354946U;

		// Token: 0x04000ACA RID: 2762
		public const uint InternalConversationIndexTracking = 1034420235U;

		// Token: 0x04000ACB RID: 2763
		public const uint InternalConversationIndex = 1034486018U;

		// Token: 0x04000ACC RID: 2764
		public const uint ConversationItemConversationId = 1034551554U;

		// Token: 0x04000ACD RID: 2765
		public const uint VirtualUnreadMessageCount = 1034616852U;

		// Token: 0x04000ACE RID: 2766
		public const uint VirtualIsRead = 1034682379U;

		// Token: 0x04000ACF RID: 2767
		public const uint IsReadColumn = 1034747915U;

		// Token: 0x04000AD0 RID: 2768
		public const uint TenantHint = 1034813698U;

		// Token: 0x04000AD1 RID: 2769
		public const uint Internal9ByteChangeNumber = 1034879234U;

		// Token: 0x04000AD2 RID: 2770
		public const uint Internal9ByteReadCnNew = 1034944770U;

		// Token: 0x04000AD3 RID: 2771
		public const uint CategoryHeaderLevelStub1 = 1035010059U;

		// Token: 0x04000AD4 RID: 2772
		public const uint CategoryHeaderLevelStub2 = 1035075595U;

		// Token: 0x04000AD5 RID: 2773
		public const uint CategoryHeaderLevelStub3 = 1035141131U;

		// Token: 0x04000AD6 RID: 2774
		public const uint CategoryHeaderAggregateProp0 = 1035206914U;

		// Token: 0x04000AD7 RID: 2775
		public const uint CategoryHeaderAggregateProp1 = 1035272450U;

		// Token: 0x04000AD8 RID: 2776
		public const uint CategoryHeaderAggregateProp2 = 1035337986U;

		// Token: 0x04000AD9 RID: 2777
		public const uint CategoryHeaderAggregateProp3 = 1035403522U;

		// Token: 0x04000ADA RID: 2778
		public const uint MaintenanceId = 1035665480U;

		// Token: 0x04000ADB RID: 2779
		public const uint MailboxType = 1035730947U;

		// Token: 0x04000ADC RID: 2780
		public const uint MessageFlagsActual = 1035796483U;

		// Token: 0x04000ADD RID: 2781
		public const uint InternalChangeKey = 1035862274U;

		// Token: 0x04000ADE RID: 2782
		public const uint InternalSourceKey = 1035927810U;

		// Token: 0x04000ADF RID: 2783
		public const uint CorrelationId = 1037107272U;

		// Token: 0x04000AE0 RID: 2784
		public const uint IdentityDisplay = 1040187423U;

		// Token: 0x04000AE1 RID: 2785
		public const uint IdentityEntryId = 1040253186U;

		// Token: 0x04000AE2 RID: 2786
		public const uint ResourceMethods = 1040318467U;

		// Token: 0x04000AE3 RID: 2787
		public const uint ResourceType = 1040384003U;

		// Token: 0x04000AE4 RID: 2788
		public const uint StatusCode = 1040449539U;

		// Token: 0x04000AE5 RID: 2789
		public const uint IdentitySearchKey = 1040515330U;

		// Token: 0x04000AE6 RID: 2790
		public const uint OwnStoreEntryId = 1040580866U;

		// Token: 0x04000AE7 RID: 2791
		public const uint ResourcePath = 1040646175U;

		// Token: 0x04000AE8 RID: 2792
		public const uint StatusString = 1040711711U;

		// Token: 0x04000AE9 RID: 2793
		public const uint X400DeferredDeliveryCancel = 1040777227U;

		// Token: 0x04000AEA RID: 2794
		public const uint HeaderFolderEntryId = 1040843010U;

		// Token: 0x04000AEB RID: 2795
		public const uint RemoteProgress = 1040908291U;

		// Token: 0x04000AEC RID: 2796
		public const uint RemoteProgressText = 1040973855U;

		// Token: 0x04000AED RID: 2797
		public const uint RemoteValidateOK = 1041039371U;

		// Token: 0x04000AEE RID: 2798
		public const uint ControlFlags = 1056964611U;

		// Token: 0x04000AEF RID: 2799
		public const uint ControlStructure = 1057030402U;

		// Token: 0x04000AF0 RID: 2800
		public const uint ControlType = 1057095683U;

		// Token: 0x04000AF1 RID: 2801
		public const uint DeltaX = 1057161219U;

		// Token: 0x04000AF2 RID: 2802
		public const uint DeltaY = 1057226755U;

		// Token: 0x04000AF3 RID: 2803
		public const uint XPos = 1057292291U;

		// Token: 0x04000AF4 RID: 2804
		public const uint YPos = 1057357827U;

		// Token: 0x04000AF5 RID: 2805
		public const uint ControlId = 1057423618U;

		// Token: 0x04000AF6 RID: 2806
		public const uint InitialDetailsPane = 1057488899U;

		// Token: 0x04000AF7 RID: 2807
		public const uint AttachmentId = 1065877524U;

		// Token: 0x04000AF8 RID: 2808
		public const uint AttachmentIdBin = 1065877762U;

		// Token: 0x04000AF9 RID: 2809
		public const uint VID = 1065877524U;

		// Token: 0x04000AFA RID: 2810
		public const uint GVid = 1065943298U;

		// Token: 0x04000AFB RID: 2811
		public const uint GDID = 1066008834U;

		// Token: 0x04000AFC RID: 2812
		public const uint XVid = 1066729730U;

		// Token: 0x04000AFD RID: 2813
		public const uint GDefVid = 1066795266U;

		// Token: 0x04000AFE RID: 2814
		public const uint PrimaryMailboxOverQuota = 1069678603U;

		// Token: 0x04000AFF RID: 2815
		public const uint ReplicaChangeNumber = 1070072066U;

		// Token: 0x04000B00 RID: 2816
		public const uint LastConflict = 1070137602U;

		// Token: 0x04000B01 RID: 2817
		public const uint RMI = 1070858498U;

		// Token: 0x04000B02 RID: 2818
		public const uint InternalPostReply = 1070924034U;

		// Token: 0x04000B03 RID: 2819
		public const uint NTSDModificationTime = 1070989376U;

		// Token: 0x04000B04 RID: 2820
		public const uint ACLDataChecksum = 1071054851U;

		// Token: 0x04000B05 RID: 2821
		public const uint PreviewUnread = 1071120415U;

		// Token: 0x04000B06 RID: 2822
		public const uint Preview = 1071185951U;

		// Token: 0x04000B07 RID: 2823
		public const uint InternetCPID = 1071513603U;

		// Token: 0x04000B08 RID: 2824
		public const uint AutoResponseSuppress = 1071579139U;

		// Token: 0x04000B09 RID: 2825
		public const uint ACLData = 1071644930U;

		// Token: 0x04000B0A RID: 2826
		public const uint ACLTable = 1071644685U;

		// Token: 0x04000B0B RID: 2827
		public const uint RulesData = 1071710466U;

		// Token: 0x04000B0C RID: 2828
		public const uint RulesTable = 1071710221U;

		// Token: 0x04000B0D RID: 2829
		public const uint OofHistory = 1071841538U;

		// Token: 0x04000B0E RID: 2830
		public const uint DesignInProgress = 1071906827U;

		// Token: 0x04000B0F RID: 2831
		public const uint SecureOrigination = 1071972363U;

		// Token: 0x04000B10 RID: 2832
		public const uint PublishInAddressBook = 1072037899U;

		// Token: 0x04000B11 RID: 2833
		public const uint ResolveMethod = 1072103427U;

		// Token: 0x04000B12 RID: 2834
		public const uint AddressBookDisplayName = 1072168991U;

		// Token: 0x04000B13 RID: 2835
		public const uint EFormsLocaleId = 1072234499U;

		// Token: 0x04000B14 RID: 2836
		public const uint HasDAMs = 1072300043U;

		// Token: 0x04000B15 RID: 2837
		public const uint DeferredSendNumber = 1072365571U;

		// Token: 0x04000B16 RID: 2838
		public const uint DeferredSendUnits = 1072431107U;

		// Token: 0x04000B17 RID: 2839
		public const uint ExpiryNumber = 1072496643U;

		// Token: 0x04000B18 RID: 2840
		public const uint ExpiryUnits = 1072562179U;

		// Token: 0x04000B19 RID: 2841
		public const uint DeferredSendTime = 1072627776U;

		// Token: 0x04000B1A RID: 2842
		public const uint BackfillTimeout = 1072693506U;

		// Token: 0x04000B1B RID: 2843
		public const uint MessageLocaleId = 1072758787U;

		// Token: 0x04000B1C RID: 2844
		public const uint RuleTriggerHistory = 1072824578U;

		// Token: 0x04000B1D RID: 2845
		public const uint MoveToStoreEid = 1072890114U;

		// Token: 0x04000B1E RID: 2846
		public const uint MoveToFolderEid = 1072955650U;

		// Token: 0x04000B1F RID: 2847
		public const uint StorageQuotaLimit = 1073020931U;

		// Token: 0x04000B20 RID: 2848
		public const uint ExcessStorageUsed = 1073086467U;

		// Token: 0x04000B21 RID: 2849
		public const uint ServerGeneratingQuotaMsg = 1073152031U;

		// Token: 0x04000B22 RID: 2850
		public const uint CreatorName = 1073217567U;

		// Token: 0x04000B23 RID: 2851
		public const uint CreatorEntryId = 1073283330U;

		// Token: 0x04000B24 RID: 2852
		public const uint LastModifierName = 1073348639U;

		// Token: 0x04000B25 RID: 2853
		public const uint LastModifierEntryId = 1073414402U;

		// Token: 0x04000B26 RID: 2854
		public const uint MessageCodePage = 1073545219U;

		// Token: 0x04000B27 RID: 2855
		public const uint QuotaType = 1073610755U;

		// Token: 0x04000B28 RID: 2856
		public const uint ExtendedACLData = 1073611010U;

		// Token: 0x04000B29 RID: 2857
		public const uint RulesSize = 1073676291U;

		// Token: 0x04000B2A RID: 2858
		public const uint IsPublicFolderQuotaMessage = 1073676299U;

		// Token: 0x04000B2B RID: 2859
		public const uint NewAttach = 1073741827U;

		// Token: 0x04000B2C RID: 2860
		public const uint StartEmbed = 1073807363U;

		// Token: 0x04000B2D RID: 2861
		public const uint EndEmbed = 1073872899U;

		// Token: 0x04000B2E RID: 2862
		public const uint StartRecip = 1073938435U;

		// Token: 0x04000B2F RID: 2863
		public const uint EndRecip = 1074003971U;

		// Token: 0x04000B30 RID: 2864
		public const uint EndCcRecip = 1074069507U;

		// Token: 0x04000B31 RID: 2865
		public const uint EndBccRecip = 1074135043U;

		// Token: 0x04000B32 RID: 2866
		public const uint EndP1Recip = 1074200579U;

		// Token: 0x04000B33 RID: 2867
		public const uint DNPrefix = 1074266143U;

		// Token: 0x04000B34 RID: 2868
		public const uint StartTopFolder = 1074331651U;

		// Token: 0x04000B35 RID: 2869
		public const uint StartSubFolder = 1074397187U;

		// Token: 0x04000B36 RID: 2870
		public const uint EndFolder = 1074462723U;

		// Token: 0x04000B37 RID: 2871
		public const uint StartMessage = 1074528259U;

		// Token: 0x04000B38 RID: 2872
		public const uint EndMessage = 1074593795U;

		// Token: 0x04000B39 RID: 2873
		public const uint EndAttach = 1074659331U;

		// Token: 0x04000B3A RID: 2874
		public const uint EcWarning = 1074724867U;

		// Token: 0x04000B3B RID: 2875
		public const uint StartFAIMessage = 1074790403U;

		// Token: 0x04000B3C RID: 2876
		public const uint NewFXFolder = 1074856194U;

		// Token: 0x04000B3D RID: 2877
		public const uint IncrSyncChange = 1074921475U;

		// Token: 0x04000B3E RID: 2878
		public const uint IncrSyncDelete = 1074987011U;

		// Token: 0x04000B3F RID: 2879
		public const uint IncrSyncEnd = 1075052547U;

		// Token: 0x04000B40 RID: 2880
		public const uint IncrSyncMessage = 1075118083U;

		// Token: 0x04000B41 RID: 2881
		public const uint FastTransferDelProp = 1075183619U;

		// Token: 0x04000B42 RID: 2882
		public const uint IdsetGiven = 1075249410U;

		// Token: 0x04000B43 RID: 2883
		public const uint IdsetGivenInt32 = 1075249155U;

		// Token: 0x04000B44 RID: 2884
		public const uint FastTransferErrorInfo = 1075314691U;

		// Token: 0x04000B45 RID: 2885
		public const uint SenderFlags = 1075380227U;

		// Token: 0x04000B46 RID: 2886
		public const uint SentRepresentingFlags = 1075445763U;

		// Token: 0x04000B47 RID: 2887
		public const uint RcvdByFlags = 1075511299U;

		// Token: 0x04000B48 RID: 2888
		public const uint RcvdRepresentingFlags = 1075576835U;

		// Token: 0x04000B49 RID: 2889
		public const uint OriginalSenderFlags = 1075642371U;

		// Token: 0x04000B4A RID: 2890
		public const uint OriginalSentRepresentingFlags = 1075707907U;

		// Token: 0x04000B4B RID: 2891
		public const uint ReportFlags = 1075773443U;

		// Token: 0x04000B4C RID: 2892
		public const uint ReadReceiptFlags = 1075838979U;

		// Token: 0x04000B4D RID: 2893
		public const uint SoftDeletes = 1075904770U;

		// Token: 0x04000B4E RID: 2894
		public const uint CreatorAddressType = 1075970079U;

		// Token: 0x04000B4F RID: 2895
		public const uint CreatorEmailAddr = 1076035615U;

		// Token: 0x04000B50 RID: 2896
		public const uint LastModifierAddressType = 1076101151U;

		// Token: 0x04000B51 RID: 2897
		public const uint LastModifierEmailAddr = 1076166687U;

		// Token: 0x04000B52 RID: 2898
		public const uint ReportAddressType = 1076232223U;

		// Token: 0x04000B53 RID: 2899
		public const uint ReportEmailAddress = 1076297759U;

		// Token: 0x04000B54 RID: 2900
		public const uint ReportDisplayName = 1076363295U;

		// Token: 0x04000B55 RID: 2901
		public const uint ReadReceiptAddressType = 1076428831U;

		// Token: 0x04000B56 RID: 2902
		public const uint ReadReceiptEmailAddress = 1076494367U;

		// Token: 0x04000B57 RID: 2903
		public const uint ReadReceiptDisplayName = 1076559903U;

		// Token: 0x04000B58 RID: 2904
		public const uint IdsetRead = 1076691202U;

		// Token: 0x04000B59 RID: 2905
		public const uint IdsetUnread = 1076756738U;

		// Token: 0x04000B5A RID: 2906
		public const uint IncrSyncRead = 1076822019U;

		// Token: 0x04000B5B RID: 2907
		public const uint SenderSimpleDisplayName = 1076887583U;

		// Token: 0x04000B5C RID: 2908
		public const uint SentRepresentingSimpleDisplayName = 1076953119U;

		// Token: 0x04000B5D RID: 2909
		public const uint OriginalSenderSimpleDisplayName = 1077018655U;

		// Token: 0x04000B5E RID: 2910
		public const uint OriginalSentRepresentingSimpleDisplayName = 1077084191U;

		// Token: 0x04000B5F RID: 2911
		public const uint ReceivedBySimpleDisplayName = 1077149727U;

		// Token: 0x04000B60 RID: 2912
		public const uint ReceivedRepresentingSimpleDisplayName = 1077215263U;

		// Token: 0x04000B61 RID: 2913
		public const uint ReadReceiptSimpleDisplayName = 1077280799U;

		// Token: 0x04000B62 RID: 2914
		public const uint ReportSimpleDisplayName = 1077346335U;

		// Token: 0x04000B63 RID: 2915
		public const uint CreatorSimpleDisplayName = 1077411871U;

		// Token: 0x04000B64 RID: 2916
		public const uint LastModifierSimpleDisplayName = 1077477407U;

		// Token: 0x04000B65 RID: 2917
		public const uint IncrSyncStateBegin = 1077542915U;

		// Token: 0x04000B66 RID: 2918
		public const uint IncrSyncStateEnd = 1077608451U;

		// Token: 0x04000B67 RID: 2919
		public const uint IncrSyncImailStream = 1077673987U;

		// Token: 0x04000B68 RID: 2920
		public const uint SenderOrgAddressType = 1077870623U;

		// Token: 0x04000B69 RID: 2921
		public const uint SenderOrgEmailAddr = 1077936159U;

		// Token: 0x04000B6A RID: 2922
		public const uint SentRepresentingOrgAddressType = 1078001695U;

		// Token: 0x04000B6B RID: 2923
		public const uint SentRepresentingOrgEmailAddr = 1078067231U;

		// Token: 0x04000B6C RID: 2924
		public const uint OriginalSenderOrgAddressType = 1078132767U;

		// Token: 0x04000B6D RID: 2925
		public const uint OriginalSenderOrgEmailAddr = 1078198303U;

		// Token: 0x04000B6E RID: 2926
		public const uint OriginalSentRepresentingOrgAddressType = 1078263839U;

		// Token: 0x04000B6F RID: 2927
		public const uint OriginalSentRepresentingOrgEmailAddr = 1078329375U;

		// Token: 0x04000B70 RID: 2928
		public const uint RcvdByOrgAddressType = 1078394911U;

		// Token: 0x04000B71 RID: 2929
		public const uint RcvdByOrgEmailAddr = 1078460447U;

		// Token: 0x04000B72 RID: 2930
		public const uint RcvdRepresentingOrgAddressType = 1078525983U;

		// Token: 0x04000B73 RID: 2931
		public const uint RcvdRepresentingOrgEmailAddr = 1078591519U;

		// Token: 0x04000B74 RID: 2932
		public const uint ReadReceiptOrgAddressType = 1078657055U;

		// Token: 0x04000B75 RID: 2933
		public const uint ReadReceiptOrgEmailAddr = 1078722591U;

		// Token: 0x04000B76 RID: 2934
		public const uint ReportOrgAddressType = 1078788127U;

		// Token: 0x04000B77 RID: 2935
		public const uint ReportOrgEmailAddr = 1078853663U;

		// Token: 0x04000B78 RID: 2936
		public const uint CreatorOrgAddressType = 1078919199U;

		// Token: 0x04000B79 RID: 2937
		public const uint CreatorOrgEmailAddr = 1078984735U;

		// Token: 0x04000B7A RID: 2938
		public const uint LastModifierOrgAddressType = 1079050271U;

		// Token: 0x04000B7B RID: 2939
		public const uint LastModifierOrgEmailAddr = 1079115807U;

		// Token: 0x04000B7C RID: 2940
		public const uint OriginatorOrgAddressType = 1079181343U;

		// Token: 0x04000B7D RID: 2941
		public const uint OriginatorOrgEmailAddr = 1079246879U;

		// Token: 0x04000B7E RID: 2942
		public const uint ReportDestinationOrgEmailType = 1079312415U;

		// Token: 0x04000B7F RID: 2943
		public const uint ReportDestinationOrgEmailAddr = 1079377951U;

		// Token: 0x04000B80 RID: 2944
		public const uint OriginalAuthorOrgAddressType = 1079443487U;

		// Token: 0x04000B81 RID: 2945
		public const uint OriginalAuthorOrgEmailAddr = 1079509023U;

		// Token: 0x04000B82 RID: 2946
		public const uint CreatorFlags = 1079574531U;

		// Token: 0x04000B83 RID: 2947
		public const uint LastModifierFlags = 1079640067U;

		// Token: 0x04000B84 RID: 2948
		public const uint OriginatorFlags = 1079705603U;

		// Token: 0x04000B85 RID: 2949
		public const uint ReportDestinationFlags = 1079771139U;

		// Token: 0x04000B86 RID: 2950
		public const uint OriginalAuthorFlags = 1079836675U;

		// Token: 0x04000B87 RID: 2951
		public const uint OriginatorSimpleDisplayName = 1079902239U;

		// Token: 0x04000B88 RID: 2952
		public const uint ReportDestinationSimpleDisplayName = 1079967775U;

		// Token: 0x04000B89 RID: 2953
		public const uint OriginalAuthorSimpleDispName = 1080033311U;

		// Token: 0x04000B8A RID: 2954
		public const uint OriginatorSearchKey = 1080099074U;

		// Token: 0x04000B8B RID: 2955
		public const uint ReportDestinationAddressType = 1080164383U;

		// Token: 0x04000B8C RID: 2956
		public const uint ReportDestinationEmailAddress = 1080229919U;

		// Token: 0x04000B8D RID: 2957
		public const uint ReportDestinationSearchKey = 1080295682U;

		// Token: 0x04000B8E RID: 2958
		public const uint IncrSyncImailStreamContinue = 1080426499U;

		// Token: 0x04000B8F RID: 2959
		public const uint IncrSyncImailStreamCancel = 1080492035U;

		// Token: 0x04000B90 RID: 2960
		public const uint IncrSyncImailStream2Continue = 1081147395U;

		// Token: 0x04000B91 RID: 2961
		public const uint IncrSyncProgressMode = 1081344011U;

		// Token: 0x04000B92 RID: 2962
		public const uint SyncProgressPerMsg = 1081409547U;

		// Token: 0x04000B93 RID: 2963
		public const uint ContentFilterSCL = 1081475075U;

		// Token: 0x04000B94 RID: 2964
		public const uint IncrSyncMsgPartial = 1081737219U;

		// Token: 0x04000B95 RID: 2965
		public const uint IncrSyncGroupInfo = 1081802755U;

		// Token: 0x04000B96 RID: 2966
		public const uint IncrSyncGroupId = 1081868291U;

		// Token: 0x04000B97 RID: 2967
		public const uint IncrSyncChangePartial = 1081933827U;

		// Token: 0x04000B98 RID: 2968
		public const uint HierRev = 1082261568U;

		// Token: 0x04000B99 RID: 2969
		public const uint ContentFilterPCL = 1082392579U;

		// Token: 0x04000B9A RID: 2970
		public const uint DeliverAsRead = 1476788235U;

		// Token: 0x04000B9B RID: 2971
		public const uint InetMailOverrideFormat = 1493303299U;

		// Token: 0x04000B9C RID: 2972
		public const uint MessageEditorFormat = 1493762051U;

		// Token: 0x04000B9D RID: 2973
		public const uint SenderSMTPAddressXSO = 1560346655U;

		// Token: 0x04000B9E RID: 2974
		public const uint SentRepresentingSMTPAddressXSO = 1560412191U;

		// Token: 0x04000B9F RID: 2975
		public const uint OriginalSenderSMTPAddressXSO = 1560477727U;

		// Token: 0x04000BA0 RID: 2976
		public const uint OriginalSentRepresentingSMTPAddressXSO = 1560543263U;

		// Token: 0x04000BA1 RID: 2977
		public const uint ReadReceiptSMTPAddressXSO = 1560608799U;

		// Token: 0x04000BA2 RID: 2978
		public const uint OriginalAuthorSMTPAddressXSO = 1560674335U;

		// Token: 0x04000BA3 RID: 2979
		public const uint ReceivedBySMTPAddressXSO = 1560739871U;

		// Token: 0x04000BA4 RID: 2980
		public const uint ReceivedRepresentingSMTPAddressXSO = 1560805407U;

		// Token: 0x04000BA5 RID: 2981
		public const uint RecipientOrder = 1608450051U;

		// Token: 0x04000BA6 RID: 2982
		public const uint RecipientSipUri = 1608843295U;

		// Token: 0x04000BA7 RID: 2983
		public const uint RecipientDisplayName = 1609957407U;

		// Token: 0x04000BA8 RID: 2984
		public const uint RecipientEntryId = 1610023170U;

		// Token: 0x04000BA9 RID: 2985
		public const uint RecipientFlags = 1610416131U;

		// Token: 0x04000BAA RID: 2986
		public const uint RecipientTrackStatus = 1610547203U;

		// Token: 0x04000BAB RID: 2987
		public const uint DotStuffState = 1610678303U;

		// Token: 0x04000BAC RID: 2988
		public const uint InternetMessageIdHash = 1644167171U;

		// Token: 0x04000BAD RID: 2989
		public const uint ConversationTopicHash = 1644232707U;

		// Token: 0x04000BAE RID: 2990
		public const uint MimeSkeleton = 1693450498U;

		// Token: 0x04000BAF RID: 2991
		public const uint ReplyTemplateId = 1707213058U;

		// Token: 0x04000BB0 RID: 2992
		public const uint SecureSubmitFlags = 1707474947U;

		// Token: 0x04000BB1 RID: 2993
		public const uint SourceKey = 1709179138U;

		// Token: 0x04000BB2 RID: 2994
		public const uint ParentSourceKey = 1709244674U;

		// Token: 0x04000BB3 RID: 2995
		public const uint ChangeKey = 1709310210U;

		// Token: 0x04000BB4 RID: 2996
		public const uint PredecessorChangeList = 1709375746U;

		// Token: 0x04000BB5 RID: 2997
		public const uint RuleMsgState = 1709768707U;

		// Token: 0x04000BB6 RID: 2998
		public const uint RuleMsgUserFlags = 1709834243U;

		// Token: 0x04000BB7 RID: 2999
		public const uint RuleMsgProvider = 1709899807U;

		// Token: 0x04000BB8 RID: 3000
		public const uint RuleMsgName = 1709965343U;

		// Token: 0x04000BB9 RID: 3001
		public const uint RuleMsgLevel = 1710030851U;

		// Token: 0x04000BBA RID: 3002
		public const uint RuleMsgProviderData = 1710096642U;

		// Token: 0x04000BBB RID: 3003
		public const uint RuleMsgActions = 1710162178U;

		// Token: 0x04000BBC RID: 3004
		public const uint RuleMsgCondition = 1710227714U;

		// Token: 0x04000BBD RID: 3005
		public const uint RuleMsgConditionLCID = 1710292995U;

		// Token: 0x04000BBE RID: 3006
		public const uint RuleMsgVersion = 1710358530U;

		// Token: 0x04000BBF RID: 3007
		public const uint RuleMsgSequence = 1710424067U;

		// Token: 0x04000BC0 RID: 3008
		public const uint PreventMsgCreate = 1710489611U;

		// Token: 0x04000BC1 RID: 3009
		public const uint IMAPSubscribeList = 1710624799U;

		// Token: 0x04000BC2 RID: 3010
		public const uint LISSD = 1710817538U;

		// Token: 0x04000BC3 RID: 3011
		public const uint ProfileVersion = 1711276035U;

		// Token: 0x04000BC4 RID: 3012
		public const uint ProfileConfigFlags = 1711341571U;

		// Token: 0x04000BC5 RID: 3013
		public const uint ProfileHomeServer = 1711407135U;

		// Token: 0x04000BC6 RID: 3014
		public const uint ProfileUser = 1711472671U;

		// Token: 0x04000BC7 RID: 3015
		public const uint ProfileConnectFlags = 1711538179U;

		// Token: 0x04000BC8 RID: 3016
		public const uint ProfileTransportFlags = 1711603715U;

		// Token: 0x04000BC9 RID: 3017
		public const uint ProfileUIState = 1711669251U;

		// Token: 0x04000BCA RID: 3018
		public const uint ProfileUnresolvedName = 1711734815U;

		// Token: 0x04000BCB RID: 3019
		public const uint ProfileUnresolvedServer = 1711800351U;

		// Token: 0x04000BCC RID: 3020
		public const uint ProfileBindingOrder = 1711865887U;

		// Token: 0x04000BCD RID: 3021
		public const uint ProfileMaxRestrict = 1712128003U;

		// Token: 0x04000BCE RID: 3022
		public const uint ProfileABFilesPath = 1712193567U;

		// Token: 0x04000BCF RID: 3023
		public const uint ProfileFavFolderDisplayName = 1712259103U;

		// Token: 0x04000BD0 RID: 3024
		public const uint ProfileOfflineStorePath = 1712324639U;

		// Token: 0x04000BD1 RID: 3025
		public const uint ProfileOfflineInfo = 1712390402U;

		// Token: 0x04000BD2 RID: 3026
		public const uint ProfileHomeServerDN = 1712455711U;

		// Token: 0x04000BD3 RID: 3027
		public const uint ProfileHomeServerAddrs = 1712525343U;

		// Token: 0x04000BD4 RID: 3028
		public const uint ProfileServerDN = 1712586783U;

		// Token: 0x04000BD5 RID: 3029
		public const uint ProfileAllPubDisplayName = 1712717855U;

		// Token: 0x04000BD6 RID: 3030
		public const uint ProfileAllPubComment = 1712783391U;

		// Token: 0x04000BD7 RID: 3031
		public const uint InTransitState = 1712848907U;

		// Token: 0x04000BD8 RID: 3032
		public const uint InTransitStatus = 1712848899U;

		// Token: 0x04000BD9 RID: 3033
		public const uint UserEntryId = 1712914690U;

		// Token: 0x04000BDA RID: 3034
		public const uint UserName = 1712979999U;

		// Token: 0x04000BDB RID: 3035
		public const uint MailboxOwnerEntryId = 1713045762U;

		// Token: 0x04000BDC RID: 3036
		public const uint MailboxOwnerName = 1713111071U;

		// Token: 0x04000BDD RID: 3037
		public const uint OofState = 1713176587U;

		// Token: 0x04000BDE RID: 3038
		public const uint TestLineSpeed = 1714094338U;

		// Token: 0x04000BDF RID: 3039
		public const uint FavoritesDefaultName = 1714749471U;

		// Token: 0x04000BE0 RID: 3040
		public const uint FolderChildCount = 1714946051U;

		// Token: 0x04000BE1 RID: 3041
		public const uint FolderChildCountInt64 = 1714946068U;

		// Token: 0x04000BE2 RID: 3042
		public const uint SerializedReplidGuidMap = 1714946306U;

		// Token: 0x04000BE3 RID: 3043
		public const uint Rights = 1715011587U;

		// Token: 0x04000BE4 RID: 3044
		public const uint HasRules = 1715077131U;

		// Token: 0x04000BE5 RID: 3045
		public const uint AddressBookEntryId = 1715142914U;

		// Token: 0x04000BE6 RID: 3046
		public const uint HierarchyChangeNumber = 1715339267U;

		// Token: 0x04000BE7 RID: 3047
		public const uint HasModeratorRules = 1715404811U;

		// Token: 0x04000BE8 RID: 3048
		public const uint ModeratorRuleCount = 1715404803U;

		// Token: 0x04000BE9 RID: 3049
		public const uint DeletedMsgCount = 1715470339U;

		// Token: 0x04000BEA RID: 3050
		public const uint DeletedMsgCountInt64 = 1715470356U;

		// Token: 0x04000BEB RID: 3051
		public const uint DeletedFolderCount = 1715535875U;

		// Token: 0x04000BEC RID: 3052
		public const uint DeletedAssocMsgCount = 1715666947U;

		// Token: 0x04000BED RID: 3053
		public const uint DeletedAssocMsgCountInt64 = 1715666964U;

		// Token: 0x04000BEE RID: 3054
		public const uint ReplicaServer = 1715732511U;

		// Token: 0x04000BEF RID: 3055
		public const uint PromotedProperties = 1715798274U;

		// Token: 0x04000BF0 RID: 3056
		public const uint HiddenPromotedProperties = 1715863810U;

		// Token: 0x04000BF1 RID: 3057
		public const uint DAMOriginalEntryId = 1715863810U;

		// Token: 0x04000BF2 RID: 3058
		public const uint LinkedSiteAuthorityUrl = 1715929119U;

		// Token: 0x04000BF3 RID: 3059
		public const uint HasNamedProperties = 1716125707U;

		// Token: 0x04000BF4 RID: 3060
		public const uint FidMid = 1716257026U;

		// Token: 0x04000BF5 RID: 3061
		public const uint ActiveUserEntryId = 1716650242U;

		// Token: 0x04000BF6 RID: 3062
		public const uint ICSChangeKey = 1716846850U;

		// Token: 0x04000BF7 RID: 3063
		public const uint SetPropsCondition = 1716977922U;

		// Token: 0x04000BF8 RID: 3064
		public const uint InternetContent = 1717108994U;

		// Token: 0x04000BF9 RID: 3065
		public const uint OriginatorName = 1717239839U;

		// Token: 0x04000BFA RID: 3066
		public const uint OriginatorEmailAddress = 1717305375U;

		// Token: 0x04000BFB RID: 3067
		public const uint OriginatorAddressType = 1717370911U;

		// Token: 0x04000BFC RID: 3068
		public const uint OriginatorEntryId = 1717436674U;

		// Token: 0x04000BFD RID: 3069
		public const uint RecipientNumber = 1717698563U;

		// Token: 0x04000BFE RID: 3070
		public const uint ReportDestinationName = 1717829663U;

		// Token: 0x04000BFF RID: 3071
		public const uint ReportDestinationEntryId = 1717895426U;

		// Token: 0x04000C00 RID: 3072
		public const uint ProhibitReceiveQuota = 1718222851U;

		// Token: 0x04000C01 RID: 3073
		public const uint MaxSubmitMessageSize = 1718419459U;

		// Token: 0x04000C02 RID: 3074
		public const uint SearchAttachments = 1718419487U;

		// Token: 0x04000C03 RID: 3075
		public const uint ProhibitSendQuota = 1718484995U;

		// Token: 0x04000C04 RID: 3076
		public const uint SubmittedByAdmin = 1718550539U;

		// Token: 0x04000C05 RID: 3077
		public const uint LongTermEntryIdFromTable = 1718616322U;

		// Token: 0x04000C06 RID: 3078
		public const uint MemberId = 1718681620U;

		// Token: 0x04000C07 RID: 3079
		public const uint MemberName = 1718747167U;

		// Token: 0x04000C08 RID: 3080
		public const uint MemberRights = 1718812675U;

		// Token: 0x04000C09 RID: 3081
		public const uint MemberSecurityIdentifier = 1718878466U;

		// Token: 0x04000C0A RID: 3082
		public const uint RuleId = 1718878228U;

		// Token: 0x04000C0B RID: 3083
		public const uint MemberIsGroup = 1718943755U;

		// Token: 0x04000C0C RID: 3084
		public const uint RuleIds = 1718944002U;

		// Token: 0x04000C0D RID: 3085
		public const uint RuleSequence = 1719009283U;

		// Token: 0x04000C0E RID: 3086
		public const uint RuleState = 1719074819U;

		// Token: 0x04000C0F RID: 3087
		public const uint RuleUserFlags = 1719140355U;

		// Token: 0x04000C10 RID: 3088
		public const uint RuleCondition = 1719206141U;

		// Token: 0x04000C11 RID: 3089
		public const uint RuleMsgConditionOld = 1719206146U;

		// Token: 0x04000C12 RID: 3090
		public const uint RuleActions = 1719664898U;

		// Token: 0x04000C13 RID: 3091
		public const uint RuleMsgActionsOld = 1719664898U;

		// Token: 0x04000C14 RID: 3092
		public const uint RuleProvider = 1719730207U;

		// Token: 0x04000C15 RID: 3093
		public const uint RuleName = 1719795743U;

		// Token: 0x04000C16 RID: 3094
		public const uint RuleLevel = 1719861251U;

		// Token: 0x04000C17 RID: 3095
		public const uint RuleProviderData = 1719927042U;

		// Token: 0x04000C18 RID: 3096
		public const uint DeletedOn = 1720647744U;

		// Token: 0x04000C19 RID: 3097
		public const uint ReplicationStyle = 1720713219U;

		// Token: 0x04000C1A RID: 3098
		public const uint ReplicationTIB = 1720779010U;

		// Token: 0x04000C1B RID: 3099
		public const uint ReplicationMsgPriority = 1720844291U;

		// Token: 0x04000C1C RID: 3100
		public const uint WorkerProcessId = 1721171971U;

		// Token: 0x04000C1D RID: 3101
		public const uint MinimumDatabaseSchemaVersion = 1721237507U;

		// Token: 0x04000C1E RID: 3102
		public const uint ReplicaList = 1721237762U;

		// Token: 0x04000C1F RID: 3103
		public const uint OverallAgeLimit = 1721303043U;

		// Token: 0x04000C20 RID: 3104
		public const uint MaximumDatabaseSchemaVersion = 1721303043U;

		// Token: 0x04000C21 RID: 3105
		public const uint CurrentDatabaseSchemaVersion = 1721368579U;

		// Token: 0x04000C22 RID: 3106
		public const uint MailboxDatabaseVersion = 1721368579U;

		// Token: 0x04000C23 RID: 3107
		public const uint DeletedMessageSize = 1721434132U;

		// Token: 0x04000C24 RID: 3108
		public const uint DeletedMessageSize32 = 1721434115U;

		// Token: 0x04000C25 RID: 3109
		public const uint RequestedDatabaseSchemaVersion = 1721434115U;

		// Token: 0x04000C26 RID: 3110
		public const uint DeletedNormalMessageSize = 1721499668U;

		// Token: 0x04000C27 RID: 3111
		public const uint DeletedNormalMessageSize32 = 1721499651U;

		// Token: 0x04000C28 RID: 3112
		public const uint DeletedAssociatedMessageSize = 1721565204U;

		// Token: 0x04000C29 RID: 3113
		public const uint DeletedAssociatedMessageSize32 = 1721565187U;

		// Token: 0x04000C2A RID: 3114
		public const uint SecureInSite = 1721630731U;

		// Token: 0x04000C2B RID: 3115
		public const uint NTUsername = 1721761823U;

		// Token: 0x04000C2C RID: 3116
		public const uint NTUserSid = 1721762050U;

		// Token: 0x04000C2D RID: 3117
		public const uint LocaleId = 1721827331U;

		// Token: 0x04000C2E RID: 3118
		public const uint LastLogonTime = 1721892928U;

		// Token: 0x04000C2F RID: 3119
		public const uint LastLogoffTime = 1721958464U;

		// Token: 0x04000C30 RID: 3120
		public const uint StorageLimitInformation = 1722023939U;

		// Token: 0x04000C31 RID: 3121
		public const uint InternetMdns = 1722089483U;

		// Token: 0x04000C32 RID: 3122
		public const uint MailboxStatus = 1722089474U;

		// Token: 0x04000C33 RID: 3123
		public const uint MailboxFlags = 1722220547U;

		// Token: 0x04000C34 RID: 3124
		public const uint FolderFlags = 1722286083U;

		// Token: 0x04000C35 RID: 3125
		public const uint PreservingMailboxSignature = 1722286091U;

		// Token: 0x04000C36 RID: 3126
		public const uint MRSPreservingMailboxSignature = 1722351627U;

		// Token: 0x04000C37 RID: 3127
		public const uint LastAccessTime = 1722351680U;

		// Token: 0x04000C38 RID: 3128
		public const uint MailboxMessagesPerFolderCountWarningQuota = 1722482691U;

		// Token: 0x04000C39 RID: 3129
		public const uint MailboxMessagesPerFolderCountReceiveQuota = 1722548227U;

		// Token: 0x04000C3A RID: 3130
		public const uint NormalMsgWithAttachCount = 1722613763U;

		// Token: 0x04000C3B RID: 3131
		public const uint NormalMsgWithAttachCountInt64 = 1722613780U;

		// Token: 0x04000C3C RID: 3132
		public const uint DumpsterMessagesPerFolderCountWarningQuota = 1722613763U;

		// Token: 0x04000C3D RID: 3133
		public const uint AssocMsgWithAttachCount = 1722679299U;

		// Token: 0x04000C3E RID: 3134
		public const uint AssocMsgWithAttachCountInt64 = 1722679316U;

		// Token: 0x04000C3F RID: 3135
		public const uint DumpsterMessagesPerFolderCountReceiveQuota = 1722679299U;

		// Token: 0x04000C40 RID: 3136
		public const uint RecipientOnNormalMsgCount = 1722744835U;

		// Token: 0x04000C41 RID: 3137
		public const uint RecipientOnNormalMsgCountInt64 = 1722744852U;

		// Token: 0x04000C42 RID: 3138
		public const uint FolderHierarchyChildrenCountWarningQuota = 1722744835U;

		// Token: 0x04000C43 RID: 3139
		public const uint RecipientOnAssocMsgCount = 1722810371U;

		// Token: 0x04000C44 RID: 3140
		public const uint RecipientOnAssocMsgCountInt64 = 1722810388U;

		// Token: 0x04000C45 RID: 3141
		public const uint FolderHierarchyChildrenCountReceiveQuota = 1722810371U;

		// Token: 0x04000C46 RID: 3142
		public const uint AttachOnNormalMsgCt = 1722875907U;

		// Token: 0x04000C47 RID: 3143
		public const uint AttachOnNormalMsgCtInt64 = 1722875924U;

		// Token: 0x04000C48 RID: 3144
		public const uint FolderHierarchyDepthWarningQuota = 1722875907U;

		// Token: 0x04000C49 RID: 3145
		public const uint AttachOnAssocMsgCt = 1722941443U;

		// Token: 0x04000C4A RID: 3146
		public const uint AttachOnAssocMsgCtInt64 = 1722941460U;

		// Token: 0x04000C4B RID: 3147
		public const uint FolderHierarchyDepthReceiveQuota = 1722941443U;

		// Token: 0x04000C4C RID: 3148
		public const uint NormalMessageSize = 1723006996U;

		// Token: 0x04000C4D RID: 3149
		public const uint NormalMessageSize32 = 1723006979U;

		// Token: 0x04000C4E RID: 3150
		public const uint AssociatedMessageSize = 1723072532U;

		// Token: 0x04000C4F RID: 3151
		public const uint AssociatedMessageSize32 = 1723072515U;

		// Token: 0x04000C50 RID: 3152
		public const uint FolderPathName = 1723138079U;

		// Token: 0x04000C51 RID: 3153
		public const uint FoldersCountWarningQuota = 1723138051U;

		// Token: 0x04000C52 RID: 3154
		public const uint OwnerCount = 1723203587U;

		// Token: 0x04000C53 RID: 3155
		public const uint FoldersCountReceiveQuota = 1723203587U;

		// Token: 0x04000C54 RID: 3156
		public const uint ContactCount = 1723269123U;

		// Token: 0x04000C55 RID: 3157
		public const uint NamedPropertiesCountQuota = 1723269123U;

		// Token: 0x04000C56 RID: 3158
		public const uint CodePageId = 1724055555U;

		// Token: 0x04000C57 RID: 3159
		public const uint RetentionAgeLimit = 1724121091U;

		// Token: 0x04000C58 RID: 3160
		public const uint DisablePerUserRead = 1724186635U;

		// Token: 0x04000C59 RID: 3161
		public const uint UserDN = 1724514335U;

		// Token: 0x04000C5A RID: 3162
		public const uint UserDisplayName = 1724579871U;

		// Token: 0x04000C5B RID: 3163
		public const uint ServerDN = 1725956127U;

		// Token: 0x04000C5C RID: 3164
		public const uint BackfillRanking = 1726021635U;

		// Token: 0x04000C5D RID: 3165
		public const uint LastTransmissionTime = 1726087171U;

		// Token: 0x04000C5E RID: 3166
		public const uint StatusSendTime = 1726152768U;

		// Token: 0x04000C5F RID: 3167
		public const uint BackfillEntryCount = 1726218243U;

		// Token: 0x04000C60 RID: 3168
		public const uint NextBroadcastTime = 1726283840U;

		// Token: 0x04000C61 RID: 3169
		public const uint NextBackfillTime = 1726349376U;

		// Token: 0x04000C62 RID: 3170
		public const uint LastCNBroadcast = 1726415106U;

		// Token: 0x04000C63 RID: 3171
		public const uint BackfillId = 1726677250U;

		// Token: 0x04000C64 RID: 3172
		public const uint LastShortCNBroadcast = 1727267074U;

		// Token: 0x04000C65 RID: 3173
		public const uint AverageTransmissionTime = 1727725632U;

		// Token: 0x04000C66 RID: 3174
		public const uint ReplicationStatus = 1727791124U;

		// Token: 0x04000C67 RID: 3175
		public const uint LastDataReceivalTime = 1727856704U;

		// Token: 0x04000C68 RID: 3176
		public const uint AdminDisplayName = 1727922207U;

		// Token: 0x04000C69 RID: 3177
		public const uint WizardNoPSTPage = 1728053259U;

		// Token: 0x04000C6A RID: 3178
		public const uint WizardNoPABPage = 1728118795U;

		// Token: 0x04000C6B RID: 3179
		public const uint SortLocaleId = 1728380931U;

		// Token: 0x04000C6C RID: 3180
		public const uint MailboxDSGuid = 1728512258U;

		// Token: 0x04000C6D RID: 3181
		public const uint MailboxDSGuidGuid = 1728512072U;

		// Token: 0x04000C6E RID: 3182
		public const uint URLName = 1728512031U;

		// Token: 0x04000C6F RID: 3183
		public const uint DateDiscoveredAbsentInDS = 1728577600U;

		// Token: 0x04000C70 RID: 3184
		public const uint UnifiedMailboxGuidGuid = 1728577608U;

		// Token: 0x04000C71 RID: 3185
		public const uint LocalCommitTime = 1728643136U;

		// Token: 0x04000C72 RID: 3186
		public const uint LocalCommitTimeMax = 1728708672U;

		// Token: 0x04000C73 RID: 3187
		public const uint DeletedCountTotal = 1728774147U;

		// Token: 0x04000C74 RID: 3188
		public const uint DeletedCountTotalInt64 = 1728774164U;

		// Token: 0x04000C75 RID: 3189
		public const uint AutoReset = 1728843848U;

		// Token: 0x04000C76 RID: 3190
		public const uint ScopeFIDs = 1729233154U;

		// Token: 0x04000C77 RID: 3191
		public const uint ELCAutoCopyTag = 1729495298U;

		// Token: 0x04000C78 RID: 3192
		public const uint ELCMoveDate = 1729560834U;

		// Token: 0x04000C79 RID: 3193
		public const uint PFAdminDescription = 1729560607U;

		// Token: 0x04000C7A RID: 3194
		public const uint PFProxy = 1729954050U;

		// Token: 0x04000C7B RID: 3195
		public const uint PFPlatinumHomeMdb = 1730019339U;

		// Token: 0x04000C7C RID: 3196
		public const uint PFProxyRequired = 1730084875U;

		// Token: 0x04000C7D RID: 3197
		public const uint PFOverHardQuotaLimit = 1730215939U;

		// Token: 0x04000C7E RID: 3198
		public const uint QuotaWarningThreshold = 1730215939U;

		// Token: 0x04000C7F RID: 3199
		public const uint PFMsgSizeLimit = 1730281475U;

		// Token: 0x04000C80 RID: 3200
		public const uint QuotaSendThreshold = 1730281475U;

		// Token: 0x04000C81 RID: 3201
		public const uint PFDisallowMdbWideExpiry = 1730347019U;

		// Token: 0x04000C82 RID: 3202
		public const uint QuotaReceiveThreshold = 1730347011U;

		// Token: 0x04000C83 RID: 3203
		public const uint FolderAdminFlags = 1731002371U;

		// Token: 0x04000C84 RID: 3204
		public const uint TimeInServer = 1731002371U;

		// Token: 0x04000C85 RID: 3205
		public const uint TimeInCpu = 1731067907U;

		// Token: 0x04000C86 RID: 3206
		public const uint ProvisionedFID = 1731133460U;

		// Token: 0x04000C87 RID: 3207
		public const uint RopCount = 1731133443U;

		// Token: 0x04000C88 RID: 3208
		public const uint ELCFolderSize = 1731198996U;

		// Token: 0x04000C89 RID: 3209
		public const uint PageRead = 1731198979U;

		// Token: 0x04000C8A RID: 3210
		public const uint ELCFolderQuota = 1731264515U;

		// Token: 0x04000C8B RID: 3211
		public const uint PagePreread = 1731264515U;

		// Token: 0x04000C8C RID: 3212
		public const uint ELCPolicyId = 1731330079U;

		// Token: 0x04000C8D RID: 3213
		public const uint LogRecordCount = 1731330051U;

		// Token: 0x04000C8E RID: 3214
		public const uint ELCPolicyComment = 1731395615U;

		// Token: 0x04000C8F RID: 3215
		public const uint LogRecordBytes = 1731395587U;

		// Token: 0x04000C90 RID: 3216
		public const uint PropertyGroupMappingId = 1731461123U;

		// Token: 0x04000C91 RID: 3217
		public const uint LdapReads = 1731461123U;

		// Token: 0x04000C92 RID: 3218
		public const uint LdapSearches = 1731526659U;

		// Token: 0x04000C93 RID: 3219
		public const uint DigestCategory = 1731592223U;

		// Token: 0x04000C94 RID: 3220
		public const uint SampleId = 1731657731U;

		// Token: 0x04000C95 RID: 3221
		public const uint SampleTime = 1731723328U;

		// Token: 0x04000C96 RID: 3222
		public const uint PropGroupInfo = 1732116738U;

		// Token: 0x04000C97 RID: 3223
		public const uint PropertyGroupChangeMask = 1732116483U;

		// Token: 0x04000C98 RID: 3224
		public const uint ReadCnNewExport = 1732182274U;

		// Token: 0x04000C99 RID: 3225
		public const uint SentMailSvrEID = 1732247803U;

		// Token: 0x04000C9A RID: 3226
		public const uint SentMailSvrEIDBin = 1732247810U;

		// Token: 0x04000C9B RID: 3227
		public const uint LocallyDelivered = 1732575490U;

		// Token: 0x04000C9C RID: 3228
		public const uint MimeSize = 1732640788U;

		// Token: 0x04000C9D RID: 3229
		public const uint MimeSize32 = 1732640771U;

		// Token: 0x04000C9E RID: 3230
		public const uint FileSize = 1732706324U;

		// Token: 0x04000C9F RID: 3231
		public const uint FileSize32 = 1732706307U;

		// Token: 0x04000CA0 RID: 3232
		public const uint Fid = 1732771860U;

		// Token: 0x04000CA1 RID: 3233
		public const uint FidBin = 1732772098U;

		// Token: 0x04000CA2 RID: 3234
		public const uint ParentFid = 1732837396U;

		// Token: 0x04000CA3 RID: 3235
		public const uint ParentFidBin = 1732837634U;

		// Token: 0x04000CA4 RID: 3236
		public const uint Mid = 1732902932U;

		// Token: 0x04000CA5 RID: 3237
		public const uint MidBin = 1732903170U;

		// Token: 0x04000CA6 RID: 3238
		public const uint CategID = 1732968468U;

		// Token: 0x04000CA7 RID: 3239
		public const uint ParentCategID = 1733034004U;

		// Token: 0x04000CA8 RID: 3240
		public const uint InstanceId = 1733099540U;

		// Token: 0x04000CA9 RID: 3241
		public const uint InstanceNum = 1733165059U;

		// Token: 0x04000CAA RID: 3242
		public const uint ChangeType = 1733296130U;

		// Token: 0x04000CAB RID: 3243
		public const uint ArticleNumNext = 1733361667U;

		// Token: 0x04000CAC RID: 3244
		public const uint RequiresRefResolve = 1733361675U;

		// Token: 0x04000CAD RID: 3245
		public const uint ImapLastArticleId = 1733427203U;

		// Token: 0x04000CAE RID: 3246
		public const uint LTID = 1733820674U;

		// Token: 0x04000CAF RID: 3247
		public const uint CnExport = 1733886210U;

		// Token: 0x04000CB0 RID: 3248
		public const uint PclExport = 1733951746U;

		// Token: 0x04000CB1 RID: 3249
		public const uint CnMvExport = 1734017282U;

		// Token: 0x04000CB2 RID: 3250
		public const uint MidsetDeletedExport = 1734082818U;

		// Token: 0x04000CB3 RID: 3251
		public const uint ArticleNumMic = 1734148099U;

		// Token: 0x04000CB4 RID: 3252
		public const uint ArticleNumMost = 1734213635U;

		// Token: 0x04000CB5 RID: 3253
		public const uint RulesSync = 1734344707U;

		// Token: 0x04000CB6 RID: 3254
		public const uint ReplicaListR = 1734410498U;

		// Token: 0x04000CB7 RID: 3255
		public const uint SortOrder = 1734410498U;

		// Token: 0x04000CB8 RID: 3256
		public const uint LocalIdNext = 1734410498U;

		// Token: 0x04000CB9 RID: 3257
		public const uint ReplicaListRC = 1734476034U;

		// Token: 0x04000CBA RID: 3258
		public const uint ReplicaListRBUG = 1734541570U;

		// Token: 0x04000CBB RID: 3259
		public const uint RootFid = 1734606868U;

		// Token: 0x04000CBC RID: 3260
		public const uint FIDC = 1734738178U;

		// Token: 0x04000CBD RID: 3261
		public const uint EventMailboxGuid = 1735000322U;

		// Token: 0x04000CBE RID: 3262
		public const uint MdbDSGuid = 1735000322U;

		// Token: 0x04000CBF RID: 3263
		public const uint MailboxOwnerDN = 1735065631U;

		// Token: 0x04000CC0 RID: 3264
		public const uint MailboxGuid = 1735131394U;

		// Token: 0x04000CC1 RID: 3265
		public const uint MapiEntryIdGuid = 1735131394U;

		// Token: 0x04000CC2 RID: 3266
		public const uint MapiEntryIdGuidGuid = 1735131208U;

		// Token: 0x04000CC3 RID: 3267
		public const uint ImapCachedBodystructure = 1735196930U;

		// Token: 0x04000CC4 RID: 3268
		public const uint Localized = 1735196683U;

		// Token: 0x04000CC5 RID: 3269
		public const uint LCID = 1735262211U;

		// Token: 0x04000CC6 RID: 3270
		public const uint AltRecipientDN = 1735327775U;

		// Token: 0x04000CC7 RID: 3271
		public const uint NoLocalDelivery = 1735393291U;

		// Token: 0x04000CC8 RID: 3272
		public const uint SoftDeleted = 1735393291U;

		// Token: 0x04000CC9 RID: 3273
		public const uint DeliveryContentLength = 1735458819U;

		// Token: 0x04000CCA RID: 3274
		public const uint AutoReply = 1735524363U;

		// Token: 0x04000CCB RID: 3275
		public const uint MailboxOwnerDisplayName = 1735589919U;

		// Token: 0x04000CCC RID: 3276
		public const uint MailboxLastUpdated = 1735655488U;

		// Token: 0x04000CCD RID: 3277
		public const uint AdminSurName = 1735720991U;

		// Token: 0x04000CCE RID: 3278
		public const uint AdminGivenName = 1735786527U;

		// Token: 0x04000CCF RID: 3279
		public const uint ActiveSearchCount = 1735852035U;

		// Token: 0x04000CD0 RID: 3280
		public const uint AdminNickname = 1735917599U;

		// Token: 0x04000CD1 RID: 3281
		public const uint QuotaStyle = 1735983107U;

		// Token: 0x04000CD2 RID: 3282
		public const uint OverQuotaLimit = 1736048643U;

		// Token: 0x04000CD3 RID: 3283
		public const uint StorageQuota = 1736114179U;

		// Token: 0x04000CD4 RID: 3284
		public const uint SubmitContentLength = 1736179715U;

		// Token: 0x04000CD5 RID: 3285
		public const uint ReservedIdCounterRangeUpperLimit = 1736310804U;

		// Token: 0x04000CD6 RID: 3286
		public const uint FolderPropTagArray = 1736311042U;

		// Token: 0x04000CD7 RID: 3287
		public const uint ReservedCnCounterRangeUpperLimit = 1736376340U;

		// Token: 0x04000CD8 RID: 3288
		public const uint MsgFolderPropTagArray = 1736376578U;

		// Token: 0x04000CD9 RID: 3289
		public const uint SetReceiveCount = 1736441859U;

		// Token: 0x04000CDA RID: 3290
		public const uint SubmittedCount = 1736572931U;

		// Token: 0x04000CDB RID: 3291
		public const uint CreatorToken = 1736638722U;

		// Token: 0x04000CDC RID: 3292
		public const uint SearchState = 1736638467U;

		// Token: 0x04000CDD RID: 3293
		public const uint SearchRestriction = 1736704258U;

		// Token: 0x04000CDE RID: 3294
		public const uint SearchFIDs = 1736769794U;

		// Token: 0x04000CDF RID: 3295
		public const uint RecursiveSearchFIDs = 1736835330U;

		// Token: 0x04000CE0 RID: 3296
		public const uint SearchBacklinks = 1736900866U;

		// Token: 0x04000CE1 RID: 3297
		public const uint LCIDRestriction = 1736966147U;

		// Token: 0x04000CE2 RID: 3298
		public const uint CategFIDs = 1737097474U;

		// Token: 0x04000CE3 RID: 3299
		public const uint FolderCDN = 1737294082U;

		// Token: 0x04000CE4 RID: 3300
		public const uint MidSegmentStart = 1737555988U;

		// Token: 0x04000CE5 RID: 3301
		public const uint MidsetDeleted = 1737621762U;

		// Token: 0x04000CE6 RID: 3302
		public const uint FolderIdsetIn = 1737621762U;

		// Token: 0x04000CE7 RID: 3303
		public const uint MidsetExpired = 1737687298U;

		// Token: 0x04000CE8 RID: 3304
		public const uint CnsetIn = 1737752834U;

		// Token: 0x04000CE9 RID: 3305
		public const uint CnsetBackfill = 1737883906U;

		// Token: 0x04000CEA RID: 3306
		public const uint CnsetSeen = 1737883906U;

		// Token: 0x04000CEB RID: 3307
		public const uint MidsetTombstones = 1738014978U;

		// Token: 0x04000CEC RID: 3308
		public const uint GWFolder = 1738145803U;

		// Token: 0x04000CED RID: 3309
		public const uint IPMFolder = 1738211339U;

		// Token: 0x04000CEE RID: 3310
		public const uint PublicFolderPath = 1738276895U;

		// Token: 0x04000CEF RID: 3311
		public const uint MidSegmentIndex = 1738473474U;

		// Token: 0x04000CF0 RID: 3312
		public const uint MidSegmentSize = 1738539010U;

		// Token: 0x04000CF1 RID: 3313
		public const uint CnSegmentStart = 1738604546U;

		// Token: 0x04000CF2 RID: 3314
		public const uint CnSegmentIndex = 1738670082U;

		// Token: 0x04000CF3 RID: 3315
		public const uint CnSegmentSize = 1738735618U;

		// Token: 0x04000CF4 RID: 3316
		public const uint ChangeNumber = 1738801172U;

		// Token: 0x04000CF5 RID: 3317
		public const uint ChangeNumberBin = 1738801410U;

		// Token: 0x04000CF6 RID: 3318
		public const uint PCL = 1738866946U;

		// Token: 0x04000CF7 RID: 3319
		public const uint CnMv = 1738936340U;

		// Token: 0x04000CF8 RID: 3320
		public const uint FolderTreeRootFID = 1738997780U;

		// Token: 0x04000CF9 RID: 3321
		public const uint SourceEntryId = 1739063554U;

		// Token: 0x04000CFA RID: 3322
		public const uint MailFlags = 1739128834U;

		// Token: 0x04000CFB RID: 3323
		public const uint Associated = 1739194379U;

		// Token: 0x04000CFC RID: 3324
		public const uint SubmitResponsibility = 1739259907U;

		// Token: 0x04000CFD RID: 3325
		public const uint SharedReceiptHandling = 1739390987U;

		// Token: 0x04000CFE RID: 3326
		public const uint Inid = 1739587842U;

		// Token: 0x04000CFF RID: 3327
		public const uint ViewRestriction = 1739587837U;

		// Token: 0x04000D00 RID: 3328
		public const uint MessageAttachList = 1739784450U;

		// Token: 0x04000D01 RID: 3329
		public const uint SenderCAI = 1739915522U;

		// Token: 0x04000D02 RID: 3330
		public const uint SentRepresentingCAI = 1739981058U;

		// Token: 0x04000D03 RID: 3331
		public const uint OriginalSenderCAI = 1740046594U;

		// Token: 0x04000D04 RID: 3332
		public const uint OriginalSentRepresentingCAI = 1740112130U;

		// Token: 0x04000D05 RID: 3333
		public const uint ReceivedByCAI = 1740177666U;

		// Token: 0x04000D06 RID: 3334
		public const uint ReceivedRepresentingCAI = 1740243202U;

		// Token: 0x04000D07 RID: 3335
		public const uint ReadReceiptCAI = 1740308738U;

		// Token: 0x04000D08 RID: 3336
		public const uint ReportCAI = 1740374274U;

		// Token: 0x04000D09 RID: 3337
		public const uint CreatorCAI = 1740439810U;

		// Token: 0x04000D0A RID: 3338
		public const uint LastModifierCAI = 1740505346U;

		// Token: 0x04000D0B RID: 3339
		public const uint AnonymousRights = 1740898306U;

		// Token: 0x04000D0C RID: 3340
		public const uint SearchGUID = 1741553922U;

		// Token: 0x04000D0D RID: 3341
		public const uint CnsetRead = 1741816066U;

		// Token: 0x04000D0E RID: 3342
		public const uint CnsetBackfillFAI = 1742340354U;

		// Token: 0x04000D0F RID: 3343
		public const uint CnsetSeenFAI = 1742340354U;

		// Token: 0x04000D10 RID: 3344
		public const uint ReplMsgVersion = 1742602243U;

		// Token: 0x04000D11 RID: 3345
		public const uint IdSetDeleted = 1743061250U;

		// Token: 0x04000D12 RID: 3346
		public const uint FolderMessages = 1743126786U;

		// Token: 0x04000D13 RID: 3347
		public const uint SenderReplid = 1743192322U;

		// Token: 0x04000D14 RID: 3348
		public const uint CnMin = 1743257620U;

		// Token: 0x04000D15 RID: 3349
		public const uint CnMax = 1743323156U;

		// Token: 0x04000D16 RID: 3350
		public const uint ReplMsgType = 1743388675U;

		// Token: 0x04000D17 RID: 3351
		public const uint RgszDNResponders = 1743454466U;

		// Token: 0x04000D18 RID: 3352
		public const uint ViewCoveringPropertyTags = 1743917059U;

		// Token: 0x04000D19 RID: 3353
		public const uint ViewAccessTime = 1743978560U;

		// Token: 0x04000D1A RID: 3354
		public const uint ICSViewFilter = 1744044043U;

		// Token: 0x04000D1B RID: 3355
		public const uint ModifiedCount = 1744175107U;

		// Token: 0x04000D1C RID: 3356
		public const uint DeletedState = 1744240643U;

		// Token: 0x04000D1D RID: 3357
		public const uint OriginatorCAI = 1744306434U;

		// Token: 0x04000D1E RID: 3358
		public const uint ReportDestinationCAI = 1744371970U;

		// Token: 0x04000D1F RID: 3359
		public const uint OriginalAuthorCAI = 1744437506U;

		// Token: 0x04000D20 RID: 3360
		public const uint ReadCnNew = 1744699412U;

		// Token: 0x04000D21 RID: 3361
		public const uint ReadCnNewBin = 1744699650U;

		// Token: 0x04000D22 RID: 3362
		public const uint SenderTelephoneNumber = 1744961567U;

		// Token: 0x04000D23 RID: 3363
		public const uint ShutoffQuota = 1745092611U;

		// Token: 0x04000D24 RID: 3364
		public const uint VoiceMessageAttachmentOrder = 1745158175U;

		// Token: 0x04000D25 RID: 3365
		public const uint MailboxMiscFlags = 1745223683U;

		// Token: 0x04000D26 RID: 3366
		public const uint EventCounter = 1745289236U;

		// Token: 0x04000D27 RID: 3367
		public const uint EventMask = 1745354755U;

		// Token: 0x04000D28 RID: 3368
		public const uint EventFid = 1745420546U;

		// Token: 0x04000D29 RID: 3369
		public const uint EventMid = 1745486082U;

		// Token: 0x04000D2A RID: 3370
		public const uint EventFidParent = 1745551618U;

		// Token: 0x04000D2B RID: 3371
		public const uint MailboxInCreation = 1745551371U;

		// Token: 0x04000D2C RID: 3372
		public const uint EventFidOld = 1745617154U;

		// Token: 0x04000D2D RID: 3373
		public const uint EventMidOld = 1745682690U;

		// Token: 0x04000D2E RID: 3374
		public const uint ObjectClassFlags = 1745682435U;

		// Token: 0x04000D2F RID: 3375
		public const uint EventFidOldParent = 1745748226U;

		// Token: 0x04000D30 RID: 3376
		public const uint ptagMsgHeaderTableFID = 1745747988U;

		// Token: 0x04000D31 RID: 3377
		public const uint EventCreatedTime = 1745813568U;

		// Token: 0x04000D32 RID: 3378
		public const uint EventMessageClass = 1745879071U;

		// Token: 0x04000D33 RID: 3379
		public const uint OOFStateEx = 1745879043U;

		// Token: 0x04000D34 RID: 3380
		public const uint EventItemCount = 1745944579U;

		// Token: 0x04000D35 RID: 3381
		public const uint EventFidRoot = 1746010370U;

		// Token: 0x04000D36 RID: 3382
		public const uint EventUnreadCount = 1746075651U;

		// Token: 0x04000D37 RID: 3383
		public const uint OofStateUserChangeTime = 1746075712U;

		// Token: 0x04000D38 RID: 3384
		public const uint EventTransacId = 1746141187U;

		// Token: 0x04000D39 RID: 3385
		public const uint UserOofSettingsItemId = 1746141442U;

		// Token: 0x04000D3A RID: 3386
		public const uint DocumentId = 1746206723U;

		// Token: 0x04000D3B RID: 3387
		public const uint EventFlags = 1746206723U;

		// Token: 0x04000D3C RID: 3388
		public const uint EventExtendedFlags = 1746403348U;

		// Token: 0x04000D3D RID: 3389
		public const uint EventClientType = 1746468867U;

		// Token: 0x04000D3E RID: 3390
		public const uint SoftDeletedFilter = 1746468875U;

		// Token: 0x04000D3F RID: 3391
		public const uint EventSid = 1746534658U;

		// Token: 0x04000D40 RID: 3392
		public const uint AssociatedFilter = 1746534411U;

		// Token: 0x04000D41 RID: 3393
		public const uint MailboxQuarantined = 1746534411U;

		// Token: 0x04000D42 RID: 3394
		public const uint EventDocId = 1746599939U;

		// Token: 0x04000D43 RID: 3395
		public const uint ConversationsFilter = 1746599947U;

		// Token: 0x04000D44 RID: 3396
		public const uint MailboxQuarantineDescription = 1746599967U;

		// Token: 0x04000D45 RID: 3397
		public const uint MailboxQuarantineLastCrash = 1746665536U;

		// Token: 0x04000D46 RID: 3398
		public const uint InstanceGuid = 1746731080U;

		// Token: 0x04000D47 RID: 3399
		public const uint MailboxQuarantineEnd = 1746731072U;

		// Token: 0x04000D48 RID: 3400
		public const uint MailboxNum = 1746862083U;

		// Token: 0x04000D49 RID: 3401
		public const uint InferenceActivityId = 1746927619U;

		// Token: 0x04000D4A RID: 3402
		public const uint InferenceClientId = 1746993155U;

		// Token: 0x04000D4B RID: 3403
		public const uint InferenceItemId = 1747058946U;

		// Token: 0x04000D4C RID: 3404
		public const uint InferenceTimeStamp = 1747124288U;

		// Token: 0x04000D4D RID: 3405
		public const uint InferenceWindowId = 1747189832U;

		// Token: 0x04000D4E RID: 3406
		public const uint InferenceSessionId = 1747255368U;

		// Token: 0x04000D4F RID: 3407
		public const uint InferenceFolderId = 1747321090U;

		// Token: 0x04000D50 RID: 3408
		public const uint ConversationDocumentId = 1747320835U;

		// Token: 0x04000D51 RID: 3409
		public const uint ConversationIdHash = 1747386371U;

		// Token: 0x04000D52 RID: 3410
		public const uint InferenceOofEnabled = 1747386379U;

		// Token: 0x04000D53 RID: 3411
		public const uint InferenceDeleteType = 1747451907U;

		// Token: 0x04000D54 RID: 3412
		public const uint LocalDirectoryBlob = 1747452162U;

		// Token: 0x04000D55 RID: 3413
		public const uint InferenceBrowser = 1747517471U;

		// Token: 0x04000D56 RID: 3414
		public const uint MemberEmail = 1747517471U;

		// Token: 0x04000D57 RID: 3415
		public const uint InferenceLocaleId = 1747582979U;

		// Token: 0x04000D58 RID: 3416
		public const uint MemberExternalId = 1747583007U;

		// Token: 0x04000D59 RID: 3417
		public const uint InferenceLocation = 1747648543U;

		// Token: 0x04000D5A RID: 3418
		public const uint MemberSID = 1747648770U;

		// Token: 0x04000D5B RID: 3419
		public const uint InferenceConversationId = 1747714306U;

		// Token: 0x04000D5C RID: 3420
		public const uint InferenceIpAddress = 1747779615U;

		// Token: 0x04000D5D RID: 3421
		public const uint MaxMessageSize = 1747779587U;

		// Token: 0x04000D5E RID: 3422
		public const uint InferenceTimeZone = 1747845151U;

		// Token: 0x04000D5F RID: 3423
		public const uint InferenceCategory = 1747910687U;

		// Token: 0x04000D60 RID: 3424
		public const uint InferenceAttachmentId = 1747976450U;

		// Token: 0x04000D61 RID: 3425
		public const uint LastUserAccessTime = 1747976256U;

		// Token: 0x04000D62 RID: 3426
		public const uint InferenceGlobalObjectId = 1748041986U;

		// Token: 0x04000D63 RID: 3427
		public const uint LastUserModificationTime = 1748041792U;

		// Token: 0x04000D64 RID: 3428
		public const uint InferenceModuleSelected = 1748107267U;

		// Token: 0x04000D65 RID: 3429
		public const uint InferenceLayoutType = 1748172831U;

		// Token: 0x04000D66 RID: 3430
		public const uint ViewStyle = 1748238339U;

		// Token: 0x04000D67 RID: 3431
		public const uint InferenceClientActivityFlags = 1748238339U;

		// Token: 0x04000D68 RID: 3432
		public const uint InferenceOWAUserActivityLoggingEnabledDeprecated = 1748303883U;

		// Token: 0x04000D69 RID: 3433
		public const uint InferenceOLKUserActivityLoggingEnabled = 1748369419U;

		// Token: 0x04000D6A RID: 3434
		public const uint FreebusyEMA = 1749614623U;

		// Token: 0x04000D6B RID: 3435
		public const uint WunderbarLinkEntryID = 1749811458U;

		// Token: 0x04000D6C RID: 3436
		public const uint WunderbarLinkStoreEntryId = 1749942530U;

		// Token: 0x04000D6D RID: 3437
		public const uint SchdInfoFreebusyMerged = 1750077698U;

		// Token: 0x04000D6E RID: 3438
		public const uint WunderbarLinkGroupClsId = 1750073602U;

		// Token: 0x04000D6F RID: 3439
		public const uint WunderbarLinkGroupName = 1750138911U;

		// Token: 0x04000D70 RID: 3440
		public const uint WunderbarLinkSection = 1750204419U;

		// Token: 0x04000D71 RID: 3441
		public const uint NavigationNodeCalendarColor = 1750269955U;

		// Token: 0x04000D72 RID: 3442
		public const uint NavigationNodeAddressbookEntryId = 1750335746U;

		// Token: 0x04000D73 RID: 3443
		public const uint AgingDeleteItems = 1750401027U;

		// Token: 0x04000D74 RID: 3444
		public const uint AgingFileName9AndPrev = 1750466591U;

		// Token: 0x04000D75 RID: 3445
		public const uint AgingAgeFolder = 1750532107U;

		// Token: 0x04000D76 RID: 3446
		public const uint AgingDontAgeMe = 1750597643U;

		// Token: 0x04000D77 RID: 3447
		public const uint AgingFileNameAfter9 = 1750663199U;

		// Token: 0x04000D78 RID: 3448
		public const uint AgingWhenDeletedOnServer = 1750794251U;

		// Token: 0x04000D79 RID: 3449
		public const uint AgingWaitUntilExpired = 1750859787U;

		// Token: 0x04000D7A RID: 3450
		public const uint InferenceTrainedModelVersionBreadCrumb = 1752367362U;

		// Token: 0x04000D7B RID: 3451
		public const uint ConversationMvFrom = 1753223199U;

		// Token: 0x04000D7C RID: 3452
		public const uint ConversationMvFromMailboxWide = 1753288735U;

		// Token: 0x04000D7D RID: 3453
		public const uint ConversationMvTo = 1753354271U;

		// Token: 0x04000D7E RID: 3454
		public const uint ConversationMvToMailboxWide = 1753419807U;

		// Token: 0x04000D7F RID: 3455
		public const uint ConversationMessageDeliveryTime = 1753481280U;

		// Token: 0x04000D80 RID: 3456
		public const uint ConversationMessageDeliveryTimeMailboxWide = 1753546816U;

		// Token: 0x04000D81 RID: 3457
		public const uint ConversationCategories = 1753616415U;

		// Token: 0x04000D82 RID: 3458
		public const uint ConversationCategoriesMailboxWide = 1753681951U;

		// Token: 0x04000D83 RID: 3459
		public const uint ConversationFlagStatus = 1753743363U;

		// Token: 0x04000D84 RID: 3460
		public const uint ConversationFlagStatusMailboxWide = 1753808899U;

		// Token: 0x04000D85 RID: 3461
		public const uint ConversationFlagCompleteTime = 1753874496U;

		// Token: 0x04000D86 RID: 3462
		public const uint ConversationFlagCompleteTimeMailboxWide = 1753940032U;

		// Token: 0x04000D87 RID: 3463
		public const uint ConversationHasAttach = 1754005515U;

		// Token: 0x04000D88 RID: 3464
		public const uint ConversationHasAttachMailboxWide = 1754071051U;

		// Token: 0x04000D89 RID: 3465
		public const uint ConversationContentCount = 1754136579U;

		// Token: 0x04000D8A RID: 3466
		public const uint ConversationContentCountMailboxWide = 1754202115U;

		// Token: 0x04000D8B RID: 3467
		public const uint ConversationContentUnread = 1754267651U;

		// Token: 0x04000D8C RID: 3468
		public const uint ConversationContentUnreadMailboxWide = 1754333187U;

		// Token: 0x04000D8D RID: 3469
		public const uint ConversationMessageSize = 1754398723U;

		// Token: 0x04000D8E RID: 3470
		public const uint ConversationMessageSizeMailboxWide = 1754464259U;

		// Token: 0x04000D8F RID: 3471
		public const uint ConversationMessageClasses = 1754533919U;

		// Token: 0x04000D90 RID: 3472
		public const uint ConversationMessageClassesMailboxWide = 1754599455U;

		// Token: 0x04000D91 RID: 3473
		public const uint ConversationReplyForwardState = 1754660867U;

		// Token: 0x04000D92 RID: 3474
		public const uint ConversationReplyForwardStateMailboxWide = 1754726403U;

		// Token: 0x04000D93 RID: 3475
		public const uint ConversationImportance = 1754791939U;

		// Token: 0x04000D94 RID: 3476
		public const uint ConversationImportanceMailboxWide = 1754857475U;

		// Token: 0x04000D95 RID: 3477
		public const uint ConversationMvFromUnread = 1754927135U;

		// Token: 0x04000D96 RID: 3478
		public const uint ConversationMvFromUnreadMailboxWide = 1754992671U;

		// Token: 0x04000D97 RID: 3479
		public const uint CategCount = 1755185155U;

		// Token: 0x04000D98 RID: 3480
		public const uint ConversationMvItemIds = 1755320578U;

		// Token: 0x04000D99 RID: 3481
		public const uint ConversationMvItemIdsMailboxWide = 1755386114U;

		// Token: 0x04000D9A RID: 3482
		public const uint ConversationHasIrm = 1755447307U;

		// Token: 0x04000D9B RID: 3483
		public const uint ConversationHasIrmMailboxWide = 1755512843U;

		// Token: 0x04000D9C RID: 3484
		public const uint PersonCompanyNameMailboxWide = 1755578399U;

		// Token: 0x04000D9D RID: 3485
		public const uint PersonDisplayNameMailboxWide = 1755643935U;

		// Token: 0x04000D9E RID: 3486
		public const uint PersonGivenNameMailboxWide = 1755709471U;

		// Token: 0x04000D9F RID: 3487
		public const uint PersonSurnameMailboxWide = 1755775007U;

		// Token: 0x04000DA0 RID: 3488
		public const uint PersonPhotoContactEntryIdMailboxWide = 1755840770U;

		// Token: 0x04000DA1 RID: 3489
		public const uint ConversationInferredImportanceInternal = 1755971589U;

		// Token: 0x04000DA2 RID: 3490
		public const uint ConversationInferredImportanceOverride = 1756037123U;

		// Token: 0x04000DA3 RID: 3491
		public const uint ConversationInferredUnimportanceInternal = 1756102661U;

		// Token: 0x04000DA4 RID: 3492
		public const uint ConversationInferredImportanceInternalMailboxWide = 1756168197U;

		// Token: 0x04000DA5 RID: 3493
		public const uint ConversationInferredImportanceOverrideMailboxWide = 1756233731U;

		// Token: 0x04000DA6 RID: 3494
		public const uint ConversationInferredUnimportanceInternalMailboxWide = 1756299269U;

		// Token: 0x04000DA7 RID: 3495
		public const uint PersonFileAsMailboxWide = 1756364831U;

		// Token: 0x04000DA8 RID: 3496
		public const uint PersonRelevanceScoreMailboxWide = 1756430339U;

		// Token: 0x04000DA9 RID: 3497
		public const uint PersonIsDistributionListMailboxWide = 1756495883U;

		// Token: 0x04000DAA RID: 3498
		public const uint PersonHomeCityMailboxWide = 1756561439U;

		// Token: 0x04000DAB RID: 3499
		public const uint PersonCreationTimeMailboxWide = 1756627008U;

		// Token: 0x04000DAC RID: 3500
		public const uint PersonGALLinkIDMailboxWide = 1756823624U;

		// Token: 0x04000DAD RID: 3501
		public const uint PersonMvEmailAddressMailboxWide = 1757024287U;

		// Token: 0x04000DAE RID: 3502
		public const uint PersonMvEmailDisplayNameMailboxWide = 1757089823U;

		// Token: 0x04000DAF RID: 3503
		public const uint PersonMvEmailRoutingTypeMailboxWide = 1757155359U;

		// Token: 0x04000DB0 RID: 3504
		public const uint PersonImAddressMailboxWide = 1757216799U;

		// Token: 0x04000DB1 RID: 3505
		public const uint PersonWorkCityMailboxWide = 1757282335U;

		// Token: 0x04000DB2 RID: 3506
		public const uint ConversationGroupingActions = 1757286402U;

		// Token: 0x04000DB3 RID: 3507
		public const uint PersonDisplayNameFirstLastMailboxWide = 1757347871U;

		// Token: 0x04000DB4 RID: 3508
		public const uint ConversationGroupingActionsMailboxWide = 1757351938U;

		// Token: 0x04000DB5 RID: 3509
		public const uint PersonDisplayNameLastFirstMailboxWide = 1757413407U;

		// Token: 0x04000DB6 RID: 3510
		public const uint ConversationPredictedActionsSummary = 1757413379U;

		// Token: 0x04000DB7 RID: 3511
		public const uint ConversationPredictedActionsSummaryMailboxWide = 1757478915U;

		// Token: 0x04000DB8 RID: 3512
		public const uint ConversationHasClutter = 1757544459U;

		// Token: 0x04000DB9 RID: 3513
		public const uint ConversationHasClutterMailboxWide = 1757609995U;

		// Token: 0x04000DBA RID: 3514
		public const uint ConversationLastMemberDocumentId = 1761607683U;

		// Token: 0x04000DBB RID: 3515
		public const uint ConversationPreview = 1761673247U;

		// Token: 0x04000DBC RID: 3516
		public const uint ConversationLastMemberDocumentIdMailboxWide = 1761738755U;

		// Token: 0x04000DBD RID: 3517
		public const uint ConversationInitialMemberDocumentId = 1761804291U;

		// Token: 0x04000DBE RID: 3518
		public const uint ConversationMemberDocumentIds = 1761873923U;

		// Token: 0x04000DBF RID: 3519
		public const uint ConversationMessageDeliveryOrRenewTimeMailboxWide = 1761935424U;

		// Token: 0x04000DC0 RID: 3520
		public const uint NDRFromName = 1761935391U;

		// Token: 0x04000DC1 RID: 3521
		public const uint FamilyId = 1762001154U;

		// Token: 0x04000DC2 RID: 3522
		public const uint ConversationMessageRichContentMailboxWide = 1762070530U;

		// Token: 0x04000DC3 RID: 3523
		public const uint ConversationPreviewMailboxWide = 1762131999U;

		// Token: 0x04000DC4 RID: 3524
		public const uint ConversationMessageDeliveryOrRenewTime = 1762197568U;

		// Token: 0x04000DC5 RID: 3525
		public const uint AttachEXCLIVersion = 1762197507U;

		// Token: 0x04000DC6 RID: 3526
		public const uint ConversationWorkingSetSourcePartition = 1762263071U;

		// Token: 0x04000DC7 RID: 3527
		public const uint SecurityFlags = 1845559299U;

		// Token: 0x04000DC8 RID: 3528
		public const uint SecurityReceiptRequestProcessed = 1845755915U;

		// Token: 0x04000DC9 RID: 3529
		public const uint FavoriteDisplayName = 2080374815U;

		// Token: 0x04000DCA RID: 3530
		public const uint FavoriteDisplayAlias = 2080440351U;

		// Token: 0x04000DCB RID: 3531
		public const uint FavPublicSourceKey = 2080506114U;

		// Token: 0x04000DCC RID: 3532
		public const uint SyncCustomState = 2080506114U;

		// Token: 0x04000DCD RID: 3533
		public const uint SyncFolderSourceKey = 2080571650U;

		// Token: 0x04000DCE RID: 3534
		public const uint SyncFolderChangeKey = 2080637186U;

		// Token: 0x04000DCF RID: 3535
		public const uint SyncFolderLastModificationTime = 2080702528U;

		// Token: 0x04000DD0 RID: 3536
		public const uint UserConfigurationDataType = 2080768003U;

		// Token: 0x04000DD1 RID: 3537
		public const uint UserConfigurationXmlStream = 2080899330U;

		// Token: 0x04000DD2 RID: 3538
		public const uint UserConfigurationStream = 2080964866U;

		// Token: 0x04000DD3 RID: 3539
		public const uint ptagSyncState = 2081030402U;

		// Token: 0x04000DD4 RID: 3540
		public const uint ReplyFwdStatus = 2081095711U;

		// Token: 0x04000DD5 RID: 3541
		public const uint UserPhotoCacheId = 2082078723U;

		// Token: 0x04000DD6 RID: 3542
		public const uint UserPhotoPreviewCacheId = 2082144259U;

		// Token: 0x04000DD7 RID: 3543
		public const uint OscSyncEnabledOnServer = 2082734091U;

		// Token: 0x04000DD8 RID: 3544
		public const uint Processed = 2097217547U;

		// Token: 0x04000DD9 RID: 3545
		public const uint FavLevelMask = 2097348611U;

		// Token: 0x04000DDA RID: 3546
		public const uint HasDlpDetectedAttachmentClassifications = 2146959391U;

		// Token: 0x04000DDB RID: 3547
		public const uint SExceptionReplaceTime = 2147024960U;

		// Token: 0x04000DDC RID: 3548
		public const uint AttachmentLinkId = 2147090435U;

		// Token: 0x04000DDD RID: 3549
		public const uint ExceptionStartTime = 2147155975U;

		// Token: 0x04000DDE RID: 3550
		public const uint ExceptionEndTime = 2147221511U;

		// Token: 0x04000DDF RID: 3551
		public const uint AttachmentFlags2 = 2147287043U;

		// Token: 0x04000DE0 RID: 3552
		public const uint AttachmentHidden = 2147352587U;

		// Token: 0x04000DE1 RID: 3553
		public const uint AttachmentContactPhoto = 2147418123U;
	}
}
